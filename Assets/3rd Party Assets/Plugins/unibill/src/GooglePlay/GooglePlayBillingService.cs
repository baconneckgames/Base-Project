//-----------------------------------------------------------------
//  Copyright 2013 Alex McAusland and Ballater Creations
//  All rights reserved
//  www.outlinegames.com
//-----------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using Uniject;
using Unibill;
using UnityEngine;


namespace Unibill.Impl {
    public class GooglePlayBillingService : IBillingService {

        private string publicKey;
        private IRawGooglePlayInterface rawInterface;
        private IBillingServiceCallback callback;
        private ProductIdRemapper remapper;
        private InventoryDatabase db;
        private ILogger logger;

        private HashSet<string> unknownAmazonProducts = new HashSet<string>();

        public GooglePlayBillingService (IRawGooglePlayInterface rawInterface,
                                         UnibillConfiguration config,
                                         ProductIdRemapper remapper,
                                         InventoryDatabase db,
                                         ILogger logger) {
            this.rawInterface = rawInterface;
            this.publicKey = config.GooglePlayPublicKey;
            this.remapper = remapper;
            this.db = db;
            this.logger = logger;
        }

        public void initialise (IBillingServiceCallback callback) {
            this.callback = callback;
            if (null == publicKey || publicKey.Equals ("[Your key]")) {
                callback.logError (UnibillError.GOOGLEPLAY_PUBLICKEY_NOTCONFIGURED, publicKey);
                callback.onSetupComplete (false);
                return;
            }

            var encoder = new Hashtable ();
            encoder.Add ("publicKey", this.publicKey);
            ArrayList products = new ArrayList ();
            foreach (var item in db.AllPurchasableItems) {
                Hashtable product = new Hashtable ();
                product.Add ("productId", remapper.mapItemIdToPlatformSpecificId (item));
                product.Add ("consumable", item.PurchaseType == PurchaseType.Consumable);
                products.Add (product);
            }
            encoder.Add("products", products);

            var json = encoder.toJson();
            rawInterface.initialise(this, json);
        }

        public void restoreTransactions () {
            rawInterface.restoreTransactions();
        }

        public void purchase (string item) {
            if (unknownAmazonProducts.Contains (item)) {
                callback.logError(UnibillError.GOOGLEPLAY_ATTEMPTING_TO_PURCHASE_PRODUCT_NOT_RETURNED_BY_GOOGLEPLAY, item);
                callback.onPurchaseFailedEvent(item);
                return;
            }

            rawInterface.purchase(item);
        }


        // Callbacks
        public void onBillingNotSupported() {
            callback.logError(UnibillError.GOOGLEPLAY_BILLING_UNAVAILABLE);
            callback.onSetupComplete(false);
        }

        public void onPurchaseSucceeded(string json) {
            Hashtable response = (Hashtable)MiniJSON.jsonDecode (json);

            callback.onPurchaseSucceeded ((string)response["productId"], (string) response ["signature"]);
        }

        public void onPurchaseCancelled (string item) {
            callback.onPurchaseCancelledEvent(item);
        }

        public void onPurchaseRefunded(string item) {
            callback.onPurchaseRefundedEvent(item);
        }

        public void onPurchaseFailed(string item) {
            callback.onPurchaseFailedEvent(item);
        }

        public void onTransactionsRestored (string success) {
            if (bool.Parse (success)) {
                callback.onTransactionsRestoredSuccess ();
            } else {
                callback.onTransactionsRestoredFail("");
            }
        }

        public void onInvalidPublicKey(string key) {
            callback.logError(UnibillError.GOOGLEPLAY_PUBLICKEY_INVALID, key);
            callback.onSetupComplete(false);
        }

        public void onProductListReceived (string productListString) {

            Hashtable response = (Hashtable)MiniJSON.jsonDecode (productListString);

            if (response.Count == 0) {
                callback.logError (UnibillError.GOOGLEPLAY_NO_PRODUCTS_RETURNED);
                callback.onSetupComplete (false);
                return;
            }

            HashSet<PurchasableItem> productsReceived = new HashSet<PurchasableItem>();
            foreach (var identifier in response.Keys) {
                if (remapper.canMapProductSpecificId(identifier.ToString())) {
                    var item = remapper.getPurchasableItemFromPlatformSpecificId(identifier.ToString());
                    Hashtable details = (Hashtable) response[identifier];

                    PurchasableItem.Writer.setLocalizedPrice(item,  details["price"].ToString());
                    PurchasableItem.Writer.setLocalizedTitle(item, (string) details["localizedTitle"]);
                    PurchasableItem.Writer.setLocalizedDescription(item, (string) details["localizedDescription"]);
                    productsReceived.Add(item);
                } else {
                    logger.LogError("Warning: Unknown product identifier: {0}", identifier.ToString());
                }
            }

            HashSet<PurchasableItem> productsNotReceived = new HashSet<PurchasableItem> (db.AllPurchasableItems);
            productsNotReceived.ExceptWith (productsReceived);
            if (productsNotReceived.Count > 0) {
                foreach (PurchasableItem product in productsNotReceived) {
                    this.unknownAmazonProducts.Add(remapper.mapItemIdToPlatformSpecificId(product));
                    callback.logError(UnibillError.GOOGLEPLAY_MISSING_PRODUCT, product.Id, remapper.mapItemIdToPlatformSpecificId(product));
                }
            }
			
			logger.Log("Received product list, polling for consumables...");
			rawInterface.pollForConsumables();
        }
		
		public void onPollForConsumablesFinished() {
			logger.Log("Finished poll for consumables, completing init.");
	        callback.onSetupComplete(true);
		}
    }
}

