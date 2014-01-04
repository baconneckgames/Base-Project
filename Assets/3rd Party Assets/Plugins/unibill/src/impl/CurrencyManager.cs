using System;
using System.Collections.Generic;
using Uniject;

namespace Unibill.Impl {
	

	public class CurrencyManager {

		private IStorage storage;

		public string[] Currencies { get; private set; }
		public Dictionary<string, decimal> exchangeRates;
		public Dictionary<string, string> itemCurrencyMap;

		public CurrencyManager(RemoteConfigManager manager, UnibillXmlParser parser, IStorage storage, ILogger logger) {
			this.storage = storage;

			var ids = new List<string> ();
			exchangeRates = new Dictionary<string, decimal> ();
			itemCurrencyMap = new Dictionary<string, string> ();
			foreach (var item in parser.ParseString(manager.XML, "currency")) {
				var currencyId = item.TryGetFirstElement ("currencyId");
				ids.Add (currencyId);

				var mappingIds = item.kvps["id"];
				var amounts = item.kvps["amount"];
				for (int t = 0; t < mappingIds.Count; t++) {
					var mappingId = mappingIds [t];
					var amount = amounts [t];
					itemCurrencyMap [mappingId] = currencyId;
					exchangeRates [mappingId] = decimal.Parse(amount);
				}
			}

			Currencies = ids.ToArray ();
		}

		public void OnPurchased(string id) {
			if (itemCurrencyMap.ContainsKey (id)) {
				var currencyId = itemCurrencyMap [id];
				var amount = exchangeRates [id];
				CreditBalance (currencyId, amount);
			}
		}

		public decimal GetCurrencyBalance(string id) {
			return storage.GetInt (getKey(id), 0);
		}

		public void CreditBalance(string id, decimal amount) {
			storage.SetInt (getKey (id), (int) (GetCurrencyBalance (id) + amount));
		}

		public bool DebitBalance(string id, decimal amount) {
			var balance = GetCurrencyBalance (id);
			if ((balance - amount) >= 0) {
				storage.SetInt (getKey (id), (int) (balance - amount));
				return true;
			}

			return false;
		}

		private string getKey(string id) {
			return string.Format ("com.outlinegames.unibill.currencies.{0}.balance", id);
		}
	}
}

