//-----------------------------------------------------------------
//  Copyright 2013 Alex McAusland and Ballater Creations
//  All rights reserved
//  www.outlinegames.com
//-----------------------------------------------------------------
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using Unibill;
using Unibill.Impl;
using System.Diagnostics;

public enum GooglePlayLocale {
    zh_TW,
    cs_CZ,
    da_DK,
    nl_NL,
    en_US,
    fr_FR,
    fi_FI,
    de_DE,
    iw_IL,
    hi_IN,
    it_IT,
    ja_JP,
    ko_KR,
    no_NO,
    pl_PL,
    pt_PT,
    ru_RU,
    es_ES,
    sv_SE,
    en_GB,
}

public class InventoryEditor : EditorWindow {
    bool groupEnabled;

    private const string SCREENSHOT_PATH = "Assets/Plugins/unibill/screenshots";
    private XPathDocument doc;
    private static List<GUIPurchasable> items = new List<GUIPurchasable>();
    private static List<GUIPurchasable> toRemove = new List<GUIPurchasable>();
    private string[] androidBillingPlatforms = new string[] { "GooglePlay", "AmazonAppstore" };
	private UnibillCurrencyEditor currencyEditor;
    private string iosSKU;
    private string googlePlayPublicKey;
    private int androidBillingPlatform;
    private bool useAmazonSandbox;
    private bool UseWP8MockingFramework;
	private bool useHostedConfig;
	private string hostedConfigUrl;

    public InventoryEditor () {
		items = new List<GUIPurchasable> ();
		toRemove = new List<GUIPurchasable> ();
		InventoryPostProcessor.CreateInventoryIfNecessary ();
		AndroidManifestGenerator.CreateManifestIfNecessary ();
        XDocument doc = XDocument.Load ("Assets/Plugins/unibill/resources/unibillInventory.xml");
        iosSKU = (string)doc.XPathSelectElement ("//iOSSKU");
        iosSKU = iosSKU == null ? string.Empty : iosSKU;
        googlePlayPublicKey = (string)doc.XPathSelectElement ("//GooglePlayPublicKey");
        {
            XElement sandbox = doc.XPathSelectElement("//useAmazonSandbox");
            useAmazonSandbox = sandbox == null ? false: (bool) sandbox;
        }
        androidBillingPlatform = ((string)doc.XPathSelectElement("//androidBillingPlatform")) == "GooglePlay" ? 0 : 1;

        var useWP8SandboxElement = doc.XPathSelectElement("//UseWP8MockingFramework");
        UseWP8MockingFramework = useWP8SandboxElement == null ? false : (bool)useWP8SandboxElement;
		
		var useHostedConfigElement = doc.XPathSelectElement("//useHostedConfig");
        useHostedConfig = useHostedConfigElement == null ? false : (bool)useHostedConfigElement;
		
		var hostedConfigUrlElement = doc.XPathSelectElement("//hostedConfigUrl");
        hostedConfigUrl = hostedConfigUrlElement == null ? "https://" : (string) hostedConfigUrlElement;
		
        foreach (XElement element in doc.XPathSelectElements("inventory/item")) {
            PurchaseType type = (PurchaseType) Enum.Parse(typeof(PurchaseType), (string) element.Attribute("purchaseType"));
            string id = (string) element.Attribute ("id");

            string name = (string) element.XPathSelectElement("name");
            string description = (string) element.XPathSelectElement("description");

            List<IPlatformEditor> editors = new List<IPlatformEditor>();
            editors.Add(new GooglePlayEditor(element));
            editors.Add(new DefaultPlatformEditor(element, BillingPlatform.AmazonAppstore));
            editors.Add(new AppleAppStoreEditor(element));
            editors.Add(new DefaultPlatformEditor(element, BillingPlatform.MacAppStore));
            editors.Add(new DefaultPlatformEditor(element, BillingPlatform.WindowsPhone8));
            items.Add(new GUIPurchasable(type, id, name, description, editors));
        }

		currencyEditor = new UnibillCurrencyEditor (doc.XPathSelectElement("inventory/currencies"));
    }
    
    // Add menu item named "My Window" to the Window menu
    [MenuItem("Window/Unibill/Inventory Editor")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        EditorWindow.GetWindow(typeof(InventoryEditor));
    }

    [MenuItem("Window/Unibill/Take Screenshot")]   
    static void screenShot () {
        DirectoryInfo d = new DirectoryInfo(SCREENSHOT_PATH);
        if (!d.Exists) {
            d.Create();
        }
        string path = Path.Combine(SCREENSHOT_PATH, ((long) new TimeSpan(DateTime.Now.Ticks).TotalSeconds).ToString() + ".png");
        Application.CaptureScreenshot(path);
        AssetDatabase.ImportAsset(path);
    }

#if UNITY_ANDROID
    [MenuItem("Window/Unibill/Install Amazon Test client")]   
    static void root () {
        Process p = new Process ();
        
        string adbLocation = Path.Combine(EditorPrefs.GetString ("AndroidSdkRoot"), "platform-tools/adb");
        
        FileInfo adb = new FileInfo (adbLocation);
        
        if (!adb.Exists) {
            adb = new FileInfo(adbLocation + ".exe");
            if (!adb.Exists) {
                UnityEngine.Debug.LogError("Unable to find adb. Verify that your Android SDK location is set correctly in Unity.");
                return;
            }
        }

        p.StartInfo.FileName = adb.FullName;
        string apkPath = new FileInfo(string.Format("Assets{0}Plugins{0}unibill{0}static{0}AmazonSDKTester.apk", Path.DirectorySeparatorChar)).FullName;

        p.StartInfo.Arguments = string.Format("install {0}", apkPath);
        p.Start();
    }
#endif

    private Vector2 scrollPosition = new Vector2();
    void OnGUI () {

		scrollPosition = EditorGUILayout.BeginScrollView (scrollPosition, false, false, 
		                                                  GUI.skin.horizontalScrollbar, GUI.skin.verticalScrollbar, GUI.skin.box);

        if (GUILayout.Button ("Save")) {
            serialise ();
        }
        EditorGUILayout.Space();

        EditorGUILayout.BeginVertical (GUI.skin.box);
        EditorGUILayout.LabelField ("Configuration settings:");
        EditorGUILayout.Space();
        
        EditorGUI.BeginChangeCheck();
        androidBillingPlatform = EditorGUILayout.Popup ("Android billing platform:", androidBillingPlatform, androidBillingPlatforms, new GUILayoutOption[0]);

        if (androidBillingPlatforms[androidBillingPlatform] == BillingPlatform.AmazonAppstore.ToString ()) {
            useAmazonSandbox = EditorGUILayout.Toggle("Use Amazon sandbox:", useAmazonSandbox);
        }

        UseWP8MockingFramework = EditorGUILayout.Toggle("Use mock Windows Phone environment:", UseWP8MockingFramework);

        if (EditorGUI.EndChangeCheck()) {
            serialise();
        }

		EditorGUILayout.BeginHorizontal();
		useHostedConfig = EditorGUILayout.Toggle("Use hosted config", useHostedConfig);
		hostedConfigUrl = EditorGUILayout.TextField(hostedConfigUrl);
		EditorGUILayout.EndHorizontal();
        
        googlePlayPublicKey = EditorGUILayout.TextField ("Google play public key:", googlePlayPublicKey);
        iosSKU = EditorGUILayout.TextField ("iOS SKU:", iosSKU);

        EditorGUILayout.EndVertical();
        EditorGUILayout.Space();

        EditorGUILayout.LabelField ("Purchasable items:");
        EditorGUILayout.Space();

        foreach (GUIPurchasable item in items) {
            EditorGUILayout.BeginVertical (GUI.skin.box);
            item.OnGUI ();
            EditorGUILayout.EndVertical ();
        }
        if (GUILayout.Button ("Add item...")) {
            items.Add (GUIPurchasable.CreateInstance ());
        }

		currencyEditor.onGUI ();

        EditorGUILayout.EndScrollView ();

        items.RemoveAll(x => toRemove.Contains(x));
        toRemove.Clear();
    }

    private void serialise () {
        XElement inventory = new XElement ("inventory");
        XElement sku = new XElement("iOSSKU", iosSKU);
        inventory.Add(sku);
        inventory.Add (new XElement("androidBillingPlatform", androidBillingPlatforms[androidBillingPlatform]));
        inventory.Add(new XElement("GooglePlayPublicKey", googlePlayPublicKey));
        inventory.Add(new XElement("useAmazonSandbox", useAmazonSandbox));
        inventory.Add(new XElement("UseWP8MockingFramework", UseWP8MockingFramework));
		inventory.Add(new XElement("useHostedConfig", useHostedConfig));
		inventory.Add(new XElement("hostedConfigUrl", hostedConfigUrl));
        foreach (GUIPurchasable item in items) {
                XElement e = new XElement ("item");
                e.Add (new XAttribute ("id", item.id));
                e.Add (new XAttribute ("purchaseType", item.type));
                e.Add (new XElement ("name", item.displayName));
                e.Add (new XElement ("description", item.description));

            XElement platformsElement = new XElement("platforms");
            foreach (IPlatformEditor editor in item.editors) {
                platformsElement.Add(editor.serialise());
            }
            e.Add(platformsElement);

                inventory.Add (e);
        }

		inventory.Add (currencyEditor.serialise ());

        XDocument doc = new XDocument (inventory);
        using (StreamWriter o = new StreamWriter("Assets/Plugins/unibill/resources/unibillInventory.xml")) {
            o.WriteLine("<?xml version=\"1.0\"?>");
            o.WriteLine(doc);
        }
		try {
	        AssetDatabase.ImportAsset("Assets/Plugins/unibill/resources/unibillInventory.xml");
		} catch(Exception) {
		}

		UnibillInjector.GetStorekitGenerator ().writeFile ();
		UnibillInjector.GetGooglePlayCSVGenerator ().writeCSV ();
		UnibillInjector.GetAmazonGenerator ().encodeAll ();

        AssetDatabase.ImportAsset("Assets/Plugins/unibill/resources/amazon.sdktester.json.txt");
        AndroidManifestGenerator.mergeManifest();
    }

    private class GUIPurchasable {
        public string id { get; private set; }
        public string displayName { get; private set; }
        public string description { get; private set; }
        public PurchaseType type { get; private set; }
        public bool visible { get; private set; }

        private bool[] platformVisibility = new bool[Enum.GetNames(typeof(BillingPlatform)).Length];
        public List<IPlatformEditor> editors { get; private set; }

        public static GUIPurchasable CreateInstance() {
            List<IPlatformEditor> editors = new List<IPlatformEditor>();
            XElement blank = new XElement("blank");
            editors.Add(new GooglePlayEditor(blank));
            editors.Add(new DefaultPlatformEditor(blank, BillingPlatform.AmazonAppstore));
            editors.Add(new AppleAppStoreEditor(blank));
            return new GUIPurchasable(PurchaseType.Consumable, UnityEditor.PlayerSettings.bundleIdentifier + "." + new System.Random().Next(), "[Name]", "[Description]", editors);
        }

        public GUIPurchasable(PurchaseType type, string id, string name, string description, List<IPlatformEditor> editors) {
            this.type = type;
            this.id = id;
            this.displayName = name;
            this.description = description;
            this.editors = editors;
        }

        public void OnGUI () {
            GUIStyle s = new GUIStyle (EditorStyles.foldout);
            var box = EditorGUILayout.BeginVertical ();

            Rect rect = new Rect (box.xMax - 20, box.yMin - 2, 20, 20);
            if (GUI.Button (rect, "x")) {
                toRemove.Add(this);
            }

            this.visible = EditorGUILayout.Foldout (visible, displayName, s);

            if (visible) {
                this.type = (PurchaseType)EditorGUILayout.EnumPopup ("Purchase type:", type, new GUILayoutOption[0]);
                id = EditorGUILayout.TextField ("Id:", id);
                displayName = EditorGUILayout.TextField ("Name:", displayName);
                description = EditorGUILayout.TextField ("Description:", description);

                foreach (BillingPlatform platform in Enum.GetValues(typeof(BillingPlatform))) {
                    EditorGUILayout.BeginVertical (GUI.skin.box);
                    platformVisibility [(int)platform] = EditorGUILayout.Foldout (platformVisibility [(int)platform], platform.ToString ());
                    if (platformVisibility [(int)platform]) {
                        editors [(int)platform].onGUI ();
                    }
                    EditorGUILayout.EndVertical ();
                }
            }

            EditorGUILayout.EndVertical ();
        }
    }

    public interface IPlatformEditor {
        void onGUI();

        XElement serialise();
    }

	public static string[] consumableIds() {
		return (from item in items
			   where item.type == PurchaseType.Consumable
			   select item.id).ToArray();
	}

    private class DefaultPlatformEditor : IPlatformEditor {
        private bool overridden;
        private string id;
        private BillingPlatform platform;
        public DefaultPlatformEditor(XElement rootItem, BillingPlatform platform) {
            this.platform = platform;
            XElement element = rootItem.XPathSelectElement(string.Format ("platforms/{0}/{0}.Id", platform));
            if (null != element) {
                overridden = true;
                id = (string) element;
            } else {
                id = (string) rootItem.Attribute("id");
            }
        }

        public virtual void onGUI () {
            overridden = EditorGUILayout.BeginToggleGroup ("Override", overridden);
            id = id == null ? "" : id;
            id = EditorGUILayout.TextField("Id:", id);
            EditorGUILayout.EndToggleGroup ();
        }

        public virtual XElement serialise () {
            XElement result = new XElement (platform.ToString());
            if (overridden) {
                result.Add(new XElement(string.Format ("{0}.Id", platform), id));
            }

            return result;
        }
    }

    private class AppleAppStoreEditor : DefaultPlatformEditor {
        private Texture2D screenshot;
        private int priceTier = 1;
        public AppleAppStoreEditor(XElement rootItem) : base(rootItem, BillingPlatform.AppleAppStore) {
            XElement screenshotPath = rootItem.XPathSelectElement("platforms/AppleAppStore/screenshotPath");
            if (null != screenshotPath) {
                screenshot = (Texture2D) AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath((string) screenshotPath), typeof(Texture2D));
            }

            XElement tier = rootItem.XPathSelectElement("platforms/AppleAppStore/appleAppStorePriceTier");
            if (null != tier) {
                this.priceTier = (int) tier;
            }
        }

        public override void onGUI () {
            base.onGUI ();
            priceTier = EditorGUILayout.IntSlider ("Price tier:", priceTier, 1, 85);
            screenshot = (Texture2D)EditorGUILayout.ObjectField ("Screenshot:", screenshot, typeof(Texture2D), false);
        }

        public override XElement serialise () {
            XElement element = base.serialise ();
            if (screenshot != null) {
                element.Add(new XElement("screenshotPath", AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(screenshot))));
            }
            element.Add(new XElement("appleAppStorePriceTier", Math.Max(1, priceTier)));
            return element;
        }
    }

    private class GooglePlayEditor : DefaultPlatformEditor {

        private decimal priceInLocalCurrency;
        private GooglePlayLocale defaultLocale;

        public GooglePlayEditor(XElement rootItem) : base(rootItem, BillingPlatform.GooglePlay) {
            {
                XElement priceInLocalCurrency = rootItem.XPathSelectElement("platforms/GooglePlay/priceInLocalCurrency");
                this.priceInLocalCurrency = 1;
                if (null != priceInLocalCurrency) {
                    this.priceInLocalCurrency = (decimal) priceInLocalCurrency;
                }
            }

            {
                XElement defaultLocale = rootItem.XPathSelectElement("platforms/GooglePlay/defaultLocale");
                if (null != defaultLocale) {
                    this.defaultLocale = (GooglePlayLocale) Enum.Parse(typeof(GooglePlayLocale), (string) defaultLocale);
                } else {
                    this.defaultLocale = GooglePlayLocale.en_US;
                }
            }
        }

        public override void onGUI () {
            base.onGUI ();
            priceInLocalCurrency = (decimal) EditorGUILayout.FloatField ("Price in your local currency:", (float) priceInLocalCurrency);
            this.defaultLocale = (GooglePlayLocale) EditorGUILayout.EnumPopup ("Default locale:", defaultLocale);
        }
        
        public override XElement serialise () {
            XElement element = base.serialise ();
            element.Add(new XElement("priceInLocalCurrency", priceInLocalCurrency));
            element.Add(new XElement("defaultLocale", defaultLocale));
            return element;
        }
    }
}
