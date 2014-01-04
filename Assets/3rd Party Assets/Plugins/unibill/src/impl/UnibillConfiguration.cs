//-----------------------------------------------------------------
//  Copyright 2013 Alex McAusland and Ballater Creations
//  All rights reserved
//  www.outlinegames.com
//-----------------------------------------------------------------
using System;
using Uniject;
using Mono.Xml;
using UnityEngine;

namespace Unibill.Impl {

    /// <summary>
    /// The underlying platform be it from Google, Amazon, Apple etc.
    /// </summary>
    public enum BillingPlatform {
        GooglePlay,
        AmazonAppstore,
        AppleAppStore,
		MacAppStore,
        WindowsPhone8,
    }

    public class UnibillConfiguration {

        public string iOSSKU { get; private set; }
		public BillingPlatform CurrentPlatform { get; private set; }
        public string GooglePlayPublicKey { get; private set; }
        public bool AmazonSandboxEnabled { get; private set; }
        public bool WP8SandboxEnabled { get; private set; }
		public bool UseHostedConfig { get; private set; }
		public string HostedConfigUrl { get; private set; }

        public UnibillConfiguration (string xml, UnibillXmlParser parser, ILogger logger) {
            var element = parser.ParseString (xml, "inventory") [0];
            iOSSKU = element.TryGetFirstElement("iOSSKU");

			GooglePlayPublicKey = element.TryGetFirstElement ("GooglePlayPublicKey");
            this.AmazonSandboxEnabled = tryGetBoolean("useAmazonSandbox", element);
            this.WP8SandboxEnabled = tryGetBoolean("UseWP8MockingFramework", element);
			this.UseHostedConfig = tryGetBoolean("useHostedConfig", element);
			this.HostedConfigUrl = element.TryGetFirstElement("hostedConfigUrl");

			CurrentPlatform = BillingPlatform.MacAppStore;
			#if UNITY_ANDROID
			CurrentPlatform = (BillingPlatform) Enum.Parse(typeof(BillingPlatform), element.TryGetFirstElement("androidBillingPlatform"));
			#endif
			#if UNITY_IPHONE
			CurrentPlatform = BillingPlatform.AppleAppStore;
			#endif
			#if UNITY_WP8
			CurrentPlatform = BillingPlatform.WindowsPhone8;
			#endif
        }
		
		private bool tryGetBoolean(string name, UnibillXmlParser.UnibillXElement root) {
			var element = root.TryGetFirstElement(name);
			bool result = false;
			Boolean.TryParse(element, out result);
			return result;
		}
    }
}
