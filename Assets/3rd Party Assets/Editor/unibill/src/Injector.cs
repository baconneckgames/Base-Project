using System;
using Uniject;
using Uniject.Editor;
using Uniject.Impl;
using Unibill;
using Unibill.Impl;


public class UnibillInjector {

	public static IResourceLoader GetResourceLoader() {
		return new UnityResourceLoader ();
	}

	public static InventoryDatabase GetInventoryDatabase() {
		return new InventoryDatabase(getXml(), GetParser(), GetLogger());
	}

	public static UnibillXmlParser GetParser() {
		return new UnibillXmlParser (new Mono.Xml.SmallXmlParser (), GetResourceLoader ());
	}

	public static UnibillConfiguration GetConfig() {
		return new UnibillConfiguration (getXml(), GetParser (), GetLogger ());
	}

	private static string getXml() {
		return new UnityResourceLoader ().openTextFile ("unibillInventory").ReadToEnd();
	}

	public static ProductIdRemapper GetRemapper() {
		return new ProductIdRemapper (getXml(), GetInventoryDatabase(), GetParser (), GetConfig ());
	}

	private static RemoteConfigManager getConfigManager() {
		return new RemoteConfigManager (GetResourceLoader (), GetParser (), new UnityPlayerPrefsStorage (), GetLogger ());
	}

	public static ILogger GetLogger() {
		return new UnityLogger ();
	}


	public static IUtil GetUtil() {
		return new UnityUtil ();
	}

	public static IEditorUtil GetEditorUtil() {
		return new UnityEditorUtil ();
	}

	public static AmazonJSONGenerator GetAmazonGenerator() {
		return new AmazonJSONGenerator (GetRemapper ());
	}

	public static GooglePlayCSVGenerator GetGooglePlayCSVGenerator() {
		return new GooglePlayCSVGenerator (GetEditorUtil (), GetRemapper (), GetInventoryDatabase (), GetResourceLoader ());
	}

	public static StorekitMassImportTemplateGenerator GetStorekitGenerator() {
		return new StorekitMassImportTemplateGenerator (GetResourceLoader (), GetInventoryDatabase (), GetRemapper(), GetEditorUtil ());
	}
}

