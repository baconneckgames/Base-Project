using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using unibill.WP8;
using Uniject;

namespace Unibill.Impl {
    class WP8BillingService : IBillingService, IWP8Callback {

        private IWP8 wp8;
        private IBillingServiceCallback callback;
        private InventoryDatabase db;
        private TransactionDatabase tDb;
        private ProductIdRemapper remapper;
        private ILogger logger;

        private HashSet<string> unknownProducts = new HashSet<string>();

        public WP8BillingService(IWP8 wp8,
                                 InventoryDatabase db,
                                 ProductIdRemapper remapper,
                                 TransactionDatabase tDb,
                                 ILogger logger) {
            this.wp8 = wp8;
            this.db = db;
            this.tDb = tDb;
            this.remapper = remapper;
            this.logger = logger;
        }

        public void initialise(IBillingServiceCallback biller) {
            this.callback = biller;
            init(0);
        }

        private void init(int delay) {
            var products = db.AllPurchasableItems.Where((x) => x.PurchaseType != PurchaseType.Subscription).Select((x) => {
                return new Product() {
                    Consumable = x.PurchaseType == PurchaseType.Consumable,
                    Description = x.description,
                    Id = remapper.mapItemIdToPlatformSpecificId(x),
                    Price = "$123.45",
                    Title = x.name
                };
            });

            wp8.Initialise(this, products.ToArray(), delay);
        }

        public void purchase(string item) {
            if (unknownProducts.Contains (item)) {
                callback.logError(UnibillError.WP8_ATTEMPTING_TO_PURCHASE_PRODUCT_NOT_RETURNED_BY_MICROSOFT, item);
                callback.onPurchaseFailedEvent(item);
                return;
            }

            wp8.Purchase (item);
        }

        public void restoreTransactions() {
            enumerateLicenses();
            callback.onTransactionsRestoredSuccess();
        }

        public void enumerateLicenses() {
            wp8.EnumerateLicenses();
        }

        public void logError (string error)
        {
            // Uncomment to get diagnostics printed on screen.
            logger.LogError (error);
        }

        public void OnProductListReceived(Product[] products) {
            if (products.Length == 0) {
                callback.logError (UnibillError.WP8_NO_PRODUCTS_RETURNED);
                callback.onSetupComplete (false);
                return;
            }

            HashSet<string> productsReceived = new HashSet<string> ();

            foreach (var product in products) {
                if (remapper.canMapProductSpecificId(product.Id)) {
                    productsReceived.Add (product.Id);
                    var item = remapper.getPurchasableItemFromPlatformSpecificId(product.Id);
                    
                    PurchasableItem.Writer.setLocalizedPrice(item, product.Price);
                    PurchasableItem.Writer.setLocalizedTitle(item, product.Title);
                    PurchasableItem.Writer.setLocalizedDescription(item, product.Description);
                } else {
                    logger.LogError("Warning: Unknown product identifier: {0}", product.Id);
                }
            }

            unknownProducts = new HashSet<string>(db.AllNonSubscriptionPurchasableItems.Select(x => remapper.mapItemIdToPlatformSpecificId(x)));
            unknownProducts.ExceptWith (productsReceived);
            if (unknownProducts.Count > 0) {
                foreach (var id in unknownProducts) {
                    callback.logError(UnibillError.WP8_MISSING_PRODUCT, id, remapper.getPurchasableItemFromPlatformSpecificId(id).Id);
                }
            }

            callback.onSetupComplete(true);
        }

        public void OnPurchaseFailed(string productId, string error) {
            logger.LogError("Purchase failed: {0}, {1}", productId, error);
            callback.onPurchaseFailedEvent(productId);
        }

        public void OnPurchaseCancelled(string productId) {
            callback.onPurchaseCancelledEvent(productId);
        }

        private static int count;
        public void OnPurchaseSucceded(string productId, string receipt) {
            logger.LogError("PURCHASE SUCCEEDED!:{0}", count++);
            var details = remapper.getPurchasableItemFromPlatformSpecificId(productId);
            switch (details.PurchaseType) {
                case PurchaseType.Consumable:
                    callback.onPurchaseSucceeded(productId, receipt);
                break;
                case PurchaseType.NonConsumable:
                case PurchaseType.Subscription:
                    var item = remapper.getPurchasableItemFromPlatformSpecificId(productId);
                    // We should only provision non consumables if they're not owned.
                    if (0 == tDb.getPurchaseHistory(item)) {
                        callback.onPurchaseSucceeded(productId, receipt);
                    }
                break;
            }
        }

        public void OnPurchaseSucceeded(string productId) {
            this.OnPurchaseSucceded(productId, string.Empty);
        }

        // When using an incorrect product id:
        // "Exception from HRESULT: 0x805A0194"
        public void OnProductListError(string message) {
            if (message.Contains("0x805A0194")) {
                callback.logError(UnibillError.WP8_APP_ID_NOT_KNOWN);
                callback.onSetupComplete(false);
            } else {
                logError("Unable to retrieve product listings. Unibill will automatically retry...");
                logError(message);
                init(3000);
            }
        }
    }
}
