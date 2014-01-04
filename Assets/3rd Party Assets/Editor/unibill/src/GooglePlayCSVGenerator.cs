//-----------------------------------------------------------------
//  Copyright 2013 Alex McAusland and Ballater Creations
//  All rights reserved
//  www.outlinegames.com
//-----------------------------------------------------------------
using System;
using Unibill;
using Unibill.Impl;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using Uniject;
using Uniject.Impl;
using Uniject.Editor;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Unibill.Impl {
    public class GooglePlayCSVGenerator {

        private IEditorUtil util;
        private ProductIdRemapper remapper;
        private InventoryDatabase db;
        private XDocument inventory;

        public GooglePlayCSVGenerator (IEditorUtil util, ProductIdRemapper remapper, InventoryDatabase db, IResourceLoader loader) {
            this.util = util;
            this.remapper = remapper;
            this.db = db;
            remapper.initialiseForPlatform(BillingPlatform.GooglePlay);
            this.inventory = XDocument.Load(loader.openTextFile("unibillInventory"));
        }

        public string getHeaderRow() {
            return string.Join(",", new string[] {
                "Product ID",
                "Published State",
                "Purchase Type",
                "Auto Translate",
                "Locale; Title; Description",
                "Auto Fill Prices",
                "Price",
            });
        }

        public void writeCSV() {
			var directory = Path.Combine(util.getAssetsDirectoryPath(), "Plugins/unibill/generated/googleplay");
			if (!Directory.Exists(directory)) {
				Directory.CreateDirectory(directory);
			}
            string path = Path.Combine (directory, "MassImportCSV.txt");
            using (StreamWriter writer = new StreamWriter(path, false)) {
                writer.WriteLine (getHeaderRow ());
                foreach (PurchasableItem item in db.AllPurchasableItems) {
                    if (PurchaseType.Subscription == item.PurchaseType) {
                        continue;
                    }
                    string[] fields = serialiseItem(item);
                    writer.WriteLine(string.Join(",", fields));
                }
            }
        }

        public string[] serialiseItem(PurchasableItem item) {
            XElement rawElement = inventory.XPathSelectElement(string.Format ("//item[@id='{0}']", item.Id));
            decimal priceInLocalCurrency = (decimal) rawElement.XPathSelectElement("platforms/GooglePlay/priceInLocalCurrency");
            string defaultLocale = (string) rawElement.XPathSelectElement("platforms/GooglePlay/defaultLocale");

            HashSet<string> otherLocales = new HashSet<string>(Enum.GetNames(typeof(GooglePlayLocale)));
            otherLocales.Remove(defaultLocale);

            // If we auto translate we must specify every other locale to translate to.
            string localesToTranslateTo = "";

            return new string[] {
                remapper.mapItemIdToPlatformSpecificId(item),
                "published",
                "managed_by_android",
                "false", // Auto translate no longer supported.
                string.Format ("\"{0};{1};{2}{3}\"", defaultLocale, escape(item.name), escape(item.description), localesToTranslateTo),
                "true", // Auto fill prices.
                string.Format("{0}", (int) (1000000 * priceInLocalCurrency)),
            };
        }

        private static string escape (string str) {
            if (null == str) {
                return null;
            }

            str = str.Replace(";", "\\;");
            str = str.Replace("\\", "\\\\");
            return str;
        }
    }
}
