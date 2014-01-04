//-----------------------------------------------------------------
//  Copyright 2013 Alex McAusland and Ballater Creations
//  All rights reserved
//  www.outlinegames.com
//-----------------------------------------------------------------
using System;
using UnityEngine;
using UnityEditor;
using Unibill;
using Unibill.Impl;
using System.IO;

public class InventoryPostProcessor : AssetPostprocessor {
	
	private const string UNIBILL_INVENTORY_PATH = "Assets/Plugins/unibill/resources/unibillInventory.xml";
	
    static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromPath) {
	
		CreateInventoryIfNecessary ();

        foreach (var s in importedAssets) {
			try {
	            if (s.Contains("unibillInventory.xml")) {
					UnibillInjector.GetStorekitGenerator ().writeFile ();
					UnibillInjector.GetGooglePlayCSVGenerator ().writeCSV ();
					UnibillInjector.GetAmazonGenerator ().encodeAll ();
	            }
			} catch (NullReferenceException) {
				// Unity insists on throwing this on first import.
			}
        }
    }

	public static void CreateInventoryIfNecessary() {
		if (!File.Exists(UNIBILL_INVENTORY_PATH) && ShouldWriteInventory()) {
			AssetDatabase.CopyAsset("Assets/Plugins/unibill/static/InventoryTemplate.xml", UNIBILL_INVENTORY_PATH);
		}
	}

    /// <summary>
    /// You may be wondering what on earth this is for.
    /// This is to finally solve the problem of people's 
    /// inventory being overwritten when they update the plugin.
    /// Given that a unitypackage is a dumb directory of files
    /// that is imported, blatting anything already there, and there
    /// is no way of excluding files when uploading to the asset store,
    /// I have to stop them existing in the directory on my machine only!
    /// 
    /// One day Unity may build a proper package management system.
    /// </summary>
    public static bool ShouldWriteInventory() {
        try {
            if (File.Exists("/tmp/B1R5SxGBA7UnmxSaW5U6qlUdOfVoa7oDV")) {
                return false;
            }
        } catch (Exception) {
        }

        return true;
    }
}
