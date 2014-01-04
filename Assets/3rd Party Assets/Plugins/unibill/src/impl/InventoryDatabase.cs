//-----------------------------------------------------------------
//  Copyright 2013 Alex McAusland and Ballater Creations
//  All rights reserved
//  www.outlinegames.com
//-----------------------------------------------------------------
using System;
using System.Collections.Generic;
using Uniject;
using Unibill;
using Unibill.Impl;

/// <summary>
/// Consumable PurchaseTypes may be purchased more than once.
/// They are therefore suitable for implementing virtual currencies.
/// 
/// The number of times a Consumable PurchasableItem has been
/// purchased is tracked by Unibill using local storage,
/// but not tracked by Google, Apple or Amazon.
/// 
/// Thus, Consumable PurchaseTypes cannot be transferred automatically
/// between devices by Unibill using restoreTransactions.
/// 
/// 
/// NonConsumable PurchaseTypes cannot be purchased more than once.
/// They are suitable for one off purchases, such as unlocking a specific
/// item or level.
/// 
/// Purchased NonConsumables are tracked by Unibill using local storage.
/// They are also linked to User's accounts by Apple, Google and Amazon,
/// and may be restored using Unibiller.restorePurchases().
/// 
/// 
/// Subscription PurchasableItems are as the name suggests; they may be purchased
/// for a finite quantity of time.
/// </summary>
public enum PurchaseType {
    Consumable,
    NonConsumable,
    Subscription,
}

/// <summary>
/// Represents an item that may be purchased as an In App Purchase.
/// </summary>
public partial class PurchasableItem : IEquatable<PurchasableItem> {
    /// <summary>
    /// A platform independent unique identifier for this PurchasableItem.
    /// 
    /// Ids should be structured similarly to bundle identifiers,
    /// eg com.companyname.productname.
    /// </summary>
    public string Id { get; private set; }

    ///
    /// The type of this PurchasableItem; Consumable, Non-Consumable or Subscription.
    ///
    public PurchaseType PurchaseType { get; private set; }
    
    /// <summary>
    /// Name of the item as displayed to users.
    /// </summary>
    public string name { get; private set; }
    
    /// <summary>
    /// Description of the item as displayed to users.
    /// </summary>
    public string description { get; private set; }

    /// <summary>
    /// !!!!DEPRECATED!!!!
    /// This property is not supported on Google Play and will be NULL on that platform.
    /// Use localizedPriceString instead.
    /// !!!!DEPRECATED!!!!
    /// Gets the localized price in local currency units, as retrieved from the billing subsystem;
    /// Apple, Google etc.
    /// </summary>
    public decimal localizedPrice { get; private set; }

    /// <summary>
    /// Gets the localized price.
    /// The precise formatting of this string varies across platforms.
    /// On Google Play, formatting includes the currency symbol.
    /// On other platforms, this is formatted as a decimal number.
    /// </summary>
    /// <value>The localized price string.</value>
    public string localizedPriceString { get; private set; }

    /// <summary>
    /// Gets the localized title, as retrieved from the billing subsystem;
    /// Apple, Google etc.
    /// </summary>
    public string localizedTitle { get; private set; }

    /// <summary>
    /// Gets the localized description, as retrieved from the billing subsystem;
    /// Apple, Google etc.
    /// </summary>
    public string localizedDescription { get; private set; }

    internal PurchasableItem(string id, PurchaseType purchaseType, string name, string description) {
        this.Id = id;
        this.PurchaseType = purchaseType;
        this.name = name;
        this.description = description;
		this.localizedTitle = name;
		this.localizedDescription = description;
    }

    public bool Equals (PurchasableItem other) {
        return other.Id == this.Id;
    }
}

/// <summary>
/// Inventory database.
/// </summary>
public class InventoryDatabase {

    private List<PurchasableItem> items;
    private ILogger logger;

	public InventoryDatabase (string xml, UnibillXmlParser parser, ILogger logger) {
        this.logger = logger;
        items = new List<PurchasableItem> ();
		foreach (var item in parser.ParseString(xml, "item")) {
            string id;
            item.attributes.TryGetValue("id", out id);
            PurchaseType type = (PurchaseType)Enum.Parse (typeof(PurchaseType), (string)item.attributes["purchaseType"]);
            string name = item.TryGetFirstElement("name");
            string description = item.TryGetFirstElement("description");

            items.Add (new PurchasableItem (id, type, name, description));
        }
    }

    public PurchasableItem getItemById (string id) {
        PurchasableItem result = items.Find (x => x.Id == id);
        if (null == result) {
            logger.LogWarning("Unknown purchasable item:{0}. Check your Unibill inventory configuration.", id);
        }
        return result;
    }

    public List<PurchasableItem> AllPurchasableItems {
        get { return new List<PurchasableItem> (items); } // Defensive copy to prevent modification.
    }

    public List<PurchasableItem> AllNonConsumablePurchasableItems {
        get { return items.FindAll (x => x.PurchaseType == PurchaseType.NonConsumable); }
    }

    public List<PurchasableItem> AllConsumablePurchasableItems {
        get { return items.FindAll (x => x.PurchaseType == PurchaseType.Consumable); }
    }

    public List<PurchasableItem> AllSubscriptions {
        get { return items.FindAll (x => x.PurchaseType == PurchaseType.Subscription); }
    }

    public List<PurchasableItem> AllNonSubscriptionPurchasableItems {
        get { return items.FindAll (x => x.PurchaseType != PurchaseType.Subscription); }
    }
}
