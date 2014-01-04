//-----------------------------------------------------------------
//  Copyright 2013 Alex McAusland and Ballater Creations
//  All rights reserved
//  www.outlinegames.com
//-----------------------------------------------------------------
using System;
using System.Collections.Generic;
using Unibill.Impl;
using Uniject;
using Uniject.Impl;
using UnityEngine;


namespace Unibill.Impl {
    public enum BillerState {
        INITIALISING,
        INITIALISED,
        INITIALISED_WITH_ERROR,
        INITIALISED_WITH_CRITICAL_ERROR,
    }
}

namespace Unibill {
    /// <summary>
    /// Singleton that composes the various components of Unibill.
    /// All billing events are routed through the Biller for recording.
    /// Purchase events are logged in the transaction database.
    /// </summary>
    public class Biller : IBillingServiceCallback {
        public InventoryDatabase InventoryDatabase { get; private set; }
        private TransactionDatabase transactionDatabase;
        private ILogger logger;
        private HelpCentre help;
        private ProductIdRemapper remapper;
        private Dictionary<PurchasableItem, List<string>> receiptMap = new Dictionary<PurchasableItem, List<string>>();
		private CurrencyManager currencyManager;
        public IBillingService billingSubsystem { get; private set; }

        public event Action<bool> onBillerReady;
        public event Action<PurchasableItem> onPurchaseComplete;
        public event Action<bool> onTransactionsRestored;
        public event Action<PurchasableItem> onPurchaseCancelled;
        public event Action<PurchasableItem> onPurchaseRefunded;
        public event Action<PurchasableItem> onPurchaseFailed;

        public BillerState State { get; private set; }
        public List<UnibillError> Errors { get; private set; }
        public bool Ready {
            get { return State == BillerState.INITIALISED || State == BillerState.INITIALISED_WITH_ERROR; }
        }

		public string[] CurrencyIdentifiers {
			get {
				return getCurrencyManager ().Currencies;
			}
		}

        public Biller (InventoryDatabase db, TransactionDatabase tDb, IBillingService billingSubsystem, ILogger logger, HelpCentre help, ProductIdRemapper remapper, CurrencyManager currencyManager) {
            this.InventoryDatabase = db;
            this.transactionDatabase = tDb;
            this.billingSubsystem = billingSubsystem;
            this.logger = logger;
            logger.prefix = "UnibillBiller";
            this.help = help;
            this.Errors = new List<UnibillError> ();
            this.remapper = remapper;
			this.currencyManager = currencyManager;
        }

        public void Initialise () {
            if (InventoryDatabase.AllPurchasableItems.Count == 0) {
                logError(UnibillError.UNIBILL_NO_PRODUCTS_DEFINED);
                onSetupComplete(false);
                return;
            }
            
            billingSubsystem.initialise(this);
        }

        public int getPurchaseHistory (PurchasableItem item) {
            return transactionDatabase.getPurchaseHistory(item);
        }

        public int getPurchaseHistory (string purchasableId) {
            return getPurchaseHistory(InventoryDatabase.getItemById(purchasableId));
        }

		public decimal getCurrencyBalance(string identifier) {
			return getCurrencyManager ().GetCurrencyBalance (identifier);
		}

		public void creditCurrencyBalance(string identifier, decimal amount) {
			getCurrencyManager ().CreditBalance (identifier, amount);
		}

		public bool debitCurrencyBalance(string identifier, decimal amount) {
			return getCurrencyManager ().DebitBalance (identifier, amount);
		}

        public string[] getReceiptsForPurchasable (PurchasableItem item) {
            if (receiptMap.ContainsKey (item)) {
                return receiptMap[item].ToArray();
            }

            return new string[0];
        }

        public void purchase (PurchasableItem item) {
            if (State == BillerState.INITIALISING) {
                logError (UnibillError.BILLER_NOT_READY);
                return;
            } else if (State == BillerState.INITIALISED_WITH_CRITICAL_ERROR) {
                logError (UnibillError.UNIBILL_INITIALISE_FAILED_WITH_CRITICAL_ERROR);
                return;
            }

            if (null == item) {
                logger.LogError ("Trying to purchase null PurchasableItem");
                return;
            }

            if (item.PurchaseType == PurchaseType.NonConsumable && transactionDatabase.getPurchaseHistory (item) > 0) {
                logError(UnibillError.UNIBILL_ATTEMPTING_TO_PURCHASE_ALREADY_OWNED_NON_CONSUMABLE);
                return;
            }
            
            billingSubsystem.purchase(remapper.mapItemIdToPlatformSpecificId(item));
            logger.Log("purchase({0})", item.Id);
        }

        public void purchase (string purchasableId) {
            PurchasableItem item = InventoryDatabase.getItemById (purchasableId);
            if (null == item) {
                logger.LogWarning("Unable to purchase unknown item with id: {0}", purchasableId);
            }
            purchase(item);
        }

        public void restoreTransactions () {
            logger.Log("restoreTransactions()");
            if (!Ready) {
                logError(UnibillError.BILLER_NOT_READY);
                return;
            }

            billingSubsystem.restoreTransactions ();
        }

        public void onPurchaseSucceeded (string id) {
            if (!verifyPlatformId (id)) {
                return;
            }
            PurchasableItem item = remapper.getPurchasableItemFromPlatformSpecificId(id);
            logger.Log("onPurchaseSucceeded({0})", item.Id);
            transactionDatabase.onPurchase (item);
			currencyManager.OnPurchased (item.Id);
            if (null != onPurchaseComplete) {
                onPurchaseComplete (item);
            }
        }

        #region IBillingServiceCallback implementation

        public void onPurchaseSucceeded (string platformSpecificId, string receipt) {
            if (receipt != null && receipt.Length > 0) {
                // Take a note of the receipt.
                PurchasableItem item = remapper.getPurchasableItemFromPlatformSpecificId (platformSpecificId);
                if (!receiptMap.ContainsKey (item)) {
                    receiptMap.Add (item, new List<string> ());
                }

                receiptMap [item].Add (receipt);
            }

            // Then trigger our normal purchase routine.
            onPurchaseSucceeded(platformSpecificId);
        }

        #endregion
        
        public void onSetupComplete (bool available) {
            logger.Log("onSetupComplete({0})", available);
            this.State = available ? (Errors.Count > 0 ? BillerState.INITIALISED_WITH_ERROR : BillerState.INITIALISED) : BillerState.INITIALISED_WITH_CRITICAL_ERROR;
            if (onBillerReady != null) {
                onBillerReady(Ready);
            }
        }

        public void onPurchaseCancelledEvent (string id) {
            if (!verifyPlatformId (id)) {
                return;
            }
            PurchasableItem item = remapper.getPurchasableItemFromPlatformSpecificId(id);
            logger.Log("onPurchaseCancelledEvent({0})", item.Id);
            if (onPurchaseCancelled != null) {
                onPurchaseCancelled(item);
            }
        }
        public void onPurchaseRefundedEvent (string id) {
            if (!verifyPlatformId (id)) {
                return;
            }
            PurchasableItem item = remapper.getPurchasableItemFromPlatformSpecificId(id);
            logger.Log("onPurchaseRefundedEvent({0})", item.Id);
            transactionDatabase.onRefunded(item);
            if (onPurchaseRefunded != null) {
                onPurchaseRefunded(item);
            }
        }

        public void onPurchaseFailedEvent (string id) {
            if (!verifyPlatformId (id)) {
                return;
            }
            PurchasableItem item = remapper.getPurchasableItemFromPlatformSpecificId(id);
            logger.Log("onPurchaseFailedEvent({0})", item.Id);
            if (null != onPurchaseFailed) {
                onPurchaseFailed(item);
            }
        }
        public void onTransactionsRestoredSuccess () {
            logger.Log("onTransactionsRestoredSuccess()");
            if (onTransactionsRestored != null) {
                onTransactionsRestored(true);
            }
        }

        public void ClearPurchases() {
            foreach (var item in InventoryDatabase.AllPurchasableItems) {
                transactionDatabase.clearPurchases (item);
            }
        }

        public void onTransactionsRestoredFail(string error) {
            logger.Log("onTransactionsRestoredFail({0})", error);
            onTransactionsRestored(false);
        }

        public void logError (UnibillError error) {
            logError(error, new object[0]);
        }

        public void logError (UnibillError error, params object[] args) {
            Errors.Add(error);
            logger.LogError(help.getMessage(error), args);
        }

        public static Biller instantiate() {
            IBillingService svc = instantiateBillingSubsystem();

            var biller = new Biller(getInventory(), getTransactionDatabase(), svc, getLogger(), getHelp(), getMapper(), getCurrencyManager());
            return biller;
        }

		private static CurrencyManager _currencyManager;
		private static CurrencyManager getCurrencyManager() {
			if (null == _currencyManager) {
				_currencyManager = new CurrencyManager (getConfigManager(), getParser (), getStorage (), getLogger ());
			}
			return _currencyManager;
		}


        private static TransactionDatabase _tDb;
        private static TransactionDatabase getTransactionDatabase () {
            if (null == _tDb) {
                _tDb = new TransactionDatabase(getStorage(), getLogger());
            }
            return _tDb;
        }

        private static IStorage _storage;
        private static IStorage getStorage () {
            if (null == _storage) {
                _storage = new UnityPlayerPrefsStorage();
            }
            return _storage;
        }

        private bool verifyPlatformId (string platformId) {
            if (!remapper.canMapProductSpecificId (platformId)) {
                logError(UnibillError.UNIBILL_UNKNOWN_PRODUCTID, platformId);
                return false;
            }
            return true;
        }

        private static IBillingService instantiateBillingSubsystem () {
            if (Application.platform == RuntimePlatform.WindowsPlayer || Application.isEditor) {
                return new Tests.FakeBillingService(getMapper());
            }
            switch (getConfig ().CurrentPlatform) {
            case BillingPlatform.AppleAppStore:
                return new AppleAppStoreBillingService(getInventory(), getMapper(), getStorekit());
            case BillingPlatform.AmazonAppstore:
                return new AmazonAppStoreBillingService(getAmazon(), getMapper(), getInventory(), getTransactionDatabase(), getLogger());
            case BillingPlatform.GooglePlay:
                return new GooglePlayBillingService(getGooglePlay(), getConfig(), getMapper(), getInventory(), getLogger());
            case BillingPlatform.MacAppStore:
                return new AppleAppStoreBillingService(getInventory(), getMapper(), getStorekit());
            case BillingPlatform.WindowsPhone8:
                var result = new WP8BillingService(unibill.WP8.Factory.Create(getConfig().WP8SandboxEnabled), getInventory(), getMapper(), getTransactionDatabase(), getLogger());
                new GameObject().AddComponent<WP8Eventhook>().callback = result;
                return result;
            }

            throw new ArgumentException(getConfig().CurrentPlatform.ToString());
        }

        private static IRawGooglePlayInterface getGooglePlay () {
            return new RawGooglePlayInterface();
        }

        private static IRawAmazonAppStoreBillingInterface getAmazon () {
            return new RawAmazonAppStoreBillingInterface(getConfig());
        }

        private static IStoreKitPlugin getStorekit() {
            if (Application.platform == RuntimePlatform.IPhonePlayer) {
                return new StoreKitPluginImpl();
            }
            
            return new OSXStoreKitPluginImpl();
        }

        private static HelpCentre _helpCentre;
        private static HelpCentre getHelp () {
            if (null == _helpCentre) {
                _helpCentre = new HelpCentre(getParser());
            }

            return _helpCentre;
        }

        private static InventoryDatabase getInventory () {
			return getConfigManager ().DB;
        }

        private static ProductIdRemapper _remapper;
        private static ProductIdRemapper getMapper () {
            if (null == _remapper) {
				_remapper = new ProductIdRemapper(getConfigManager().XML, getConfigManager().DB, getParser(), getConfig());
            }

            return _remapper;
        }

        private static ILogger getLogger () {
            return new UnityLogger();
        }

        private static UnibillXmlParser getParser() {
            return new UnibillXmlParser(new Mono.Xml.SmallXmlParser(), getResourceLoader());
        }

        private static UnibillConfiguration getConfig () {
            return getConfigManager().Config;
        }

		private static RemoteConfigManager _configManager;
		private static RemoteConfigManager getConfigManager() {
			if (null == _configManager) {
				_configManager = new RemoteConfigManager (getResourceLoader(), getParser (), getStorage (), getLogger ());
			}

			return _configManager;
		}

        private static IUtil getUtil () {
            return new UnityUtil();
        }

        private static IResourceLoader _resourceLoader;
        private static IResourceLoader getResourceLoader () {
            if (null == _resourceLoader) {
                _resourceLoader = new UnityResourceLoader();
            }
            return _resourceLoader;
        }
    }
}
