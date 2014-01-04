//-----------------------------------------------------------------
//  Copyright 2013 Alex McAusland and Ballater Creations
//  All rights reserved
//  www.outlinegames.com
//-----------------------------------------------------------------
using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Collections.Generic;
using Uniject.Editor;
using Uniject;

namespace Unibill.Impl {
    public class StorekitMassImportTemplateGenerator {

        private ProductIdRemapper remapper;
        private IEditorUtil util;
        private InventoryDatabase db;
        private string sku;
        private XDocument inventoryDocument;
        public StorekitMassImportTemplateGenerator(IResourceLoader loader,
                                                   InventoryDatabase db, ProductIdRemapper remapper, IEditorUtil util) {
            this.db = db;
            this.remapper = remapper;
            this.util = util;
            XDocument inventory = XDocument.Load(loader.openTextFile("unibillInventory"));
            this.inventoryDocument = inventory;
            this.sku = (string) inventory.XPathSelectElement("//iOSSKU");
        }

        public void writeFile () {
            string directory = Path.Combine (util.getAssetsDirectoryPath (), "Plugins/unibill/generated/storekit");
            if (!Directory.Exists (directory)) {
                Directory.CreateDirectory(directory);
            }
            string path = Path.Combine (directory, "MassImportTemplate.txt");
            using (StreamWriter writer = new StreamWriter(path, false)) {
                writer.WriteLine (getHeaderLine ());
                foreach (PurchasableItem item in db.AllPurchasableItems) {
                    if (PurchaseType.Subscription != item.PurchaseType) {
                        writer.WriteLine(serialisePurchasable(item));
                    }
                }
            }
        }

        public string getHeaderLine () {
            string[] headers = new string[] {
                "SKU",
                "Product ID",
                "Reference Name",
                "Type",
                "Cleared For Sale",
                "Wholesale Price Tier",
                "Displayed Name",
                "Description",
                "Screenshot Path",
            };
            return string.Join("\t", headers);
        }

        public string serialisePurchasable (PurchasableItem item) {
            XElement element = inventoryDocument.XPathSelectElement(string.Format ("//item[@id='{0}']", item.Id));
            XElement screenshotElement = element.XPathSelectElement("platforms/AppleAppStore/screenshotPath");
            string screenshotPath = string.Empty;
            if (screenshotElement == null) {
//                logger.Log ("Missing screenshot for purchasable:{0}", item.id);
            } else {
                string templatePath = "Assets/Plugins/unibill/generated/storekit/MassImportTemplate";
                templatePath = new FileInfo (templatePath).FullName;
                screenshotPath = new FileInfo (util.guidToAssetPath((string)screenshotElement)).FullName;
            }
            var records = new string[] {
                sku,
                remapper.mapItemIdToPlatformSpecificId(item),
                remapper.mapItemIdToPlatformSpecificId(item), // This is the 'reference' field that is used to refer to the product within iTunes connect.
                item.PurchaseType == PurchaseType.Consumable ? "Consumable" : "Non-Consumable",
                "yes",
                (string) element.XPathSelectElement("platforms/AppleAppStore/appleAppStorePriceTier"),
                item.name,
                item.description,
                screenshotPath,
            };

            return string.Join("\t", records);
        }
    }
}
