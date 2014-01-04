using System;
using System.Threading;
using Uniject;
using UnityEngine;

namespace Unibill.Impl
{
	public class RemoteConfigManager
	{
		private const string CACHED_CONFIG_PATH = "com.outlinegames.unibill.cached.config";
		private IStorage storage;
		public UnibillConfiguration Config { get; private set; }
		public InventoryDatabase DB { get; private set; }
		public string XML;

		public RemoteConfigManager (IResourceLoader loader, UnibillXmlParser parser, IStorage storage, ILogger logger) {
			this.storage = storage;
			logger.prefix = "Unibill.RemoteConfigManager";
			this.XML = loader.openTextFile ("unibillInventory").ReadToEnd ();
			Config = new UnibillConfiguration(XML, parser, logger);
			if (Config.UseHostedConfig) {
				string val = storage.GetString (CACHED_CONFIG_PATH, string.Empty);
				if (string.IsNullOrEmpty (val)) {
					logger.Log ("No cached config available. Using bundled");
					DB = new InventoryDatabase (XML, parser, logger);
				} else {
					logger.Log ("Cached config found, attempting to parse");
					try {
						DB = new InventoryDatabase (val, parser, logger);
						if (DB.AllPurchasableItems.Count == 0) {
							logger.LogError ("No purchasable items in cached config, ignoring.");
							DB = new InventoryDatabase (XML, parser, logger);
						} else {
							logger.Log (string.Format ("Using cached config with {0} purchasable items", DB.AllPurchasableItems.Count));
							XML = val;
						}
					} catch (Exception e) {
						logger.LogError ("Error parsing inventory: {0}", e.Message);
						DB = new InventoryDatabase (XML, parser, logger);
					}
				}
				refreshCachedConfig (Config.HostedConfigUrl, logger);
			} else {
				logger.Log ("Not using cached inventory, using bundled.");
				DB = new InventoryDatabase (XML, parser, logger);
			}
		}
		
		private void refreshCachedConfig(string url, ILogger logger) {
			logger.Log("Trying to fetch remote config...");
			new GameObject ().AddComponent<RemoteConfigFetcher> ().Fetch (storage, Config.HostedConfigUrl, CACHED_CONFIG_PATH);
		}
	}
}

