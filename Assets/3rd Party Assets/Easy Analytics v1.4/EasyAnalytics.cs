using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;

public class EasyAnalytics : MonoBehaviour{

    #if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport ("__Internal")] public static extern void trackerWithTrackingIdImpl(string trackingId);
		[DllImport ("__Internal")] public static extern IntPtr getClientIdImpl();
        [DllImport ("__Internal")] public static extern void setCustomDimensionImpl(int index, string dimension);
        [DllImport ("__Internal")] public static extern void setCustomMetricImpl(int index, long metric);
        [DllImport ("__Internal")] public static extern void sendEventWithImpl(string category, string action, string buttonName, long value);
        [DllImport ("__Internal")] public static extern void sendViewImpl(string page);
        [DllImport ("__Internal")] public static extern void setStartSessionImpl(bool startSession);
        [DllImport ("__Internal")] public static extern void sendExceptionImpl(string description, bool fatal);
        [DllImport ("__Internal")] public static extern void sendSocialImpl(string network, string action, string target);
        [DllImport ("__Internal")] public static extern void sendTimingImpl(string category, string nameStr, string label, long intervalInMilliseconds);
        [DllImport ("__Internal")] public static extern void sendTransactionImpl(string transactionId,
            long orderTotal,
            string affiliation,
            long totalTax,
            long totalShipping,
            string currencyCode,
            string productSKU,
            string productName,
            long productPrice,
            int quantity,
            string productCategory);
        [DllImport ("__Internal")] public static extern void setCampaignUrlImpl(string campaignUrl);
		[DllImport ("__Internal")] public static extern void setReferrerImpl(string url);
		[DllImport ("__Internal")] public static extern void setDispatchPeriodImpl(int seconds);
		[DllImport ("__Internal")] public static extern void dispatchImpl();
    #endif

    #if UNITY_WEBPLAYER && !UNITY_EDITOR
		//Create a wrapper in the HTML host file and use directly the analytics.js
		//http://docs.unity3d.com/Documentation/ScriptReference/Application.ExternalCall.html
		//Line to include in the html file before head : 
		//<script type="text/javascript" src="gawebwrapper.js"></script>
    #endif
    

    //Singleton
    static EasyAnalytics instance;
	#if !UNITY_EDITOR
    	private string _trackinId = "";
		private string _clientId = "";
	#endif
    public static EasyAnalytics Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject eaGameObject = new GameObject("EasyAnalytics");
                instance = eaGameObject.AddComponent<EasyAnalytics>();
                DontDestroyOnLoad(eaGameObject);
            }
            return instance;
        }
    }

    void Awake()
    {
        Debug.Log("Awake");
    }
	
	void Start()
    {
        Debug.Log("Start");
	}

    // --------------------------------------
    // ~~ API EasyAnalytics ~~
    // trackingId : The Google property Id
    // --------------------------------------
	public void trackerWithTrackingId(string trackingId)
    {
        StartCoroutine(trackerWithTrackingIdAsync(trackingId==null?"":trackingId));
    }
    IEnumerator trackerWithTrackingIdAsync(string trackingId)
    {
        yield return new WaitForEndOfFrame();
        #if UNITY_IPHONE && !UNITY_EDITOR
            _trackinId = trackingId;
            trackerWithTrackingIdImpl(_trackinId);
        #endif
		
        #if UNITY_ANDROID && !UNITY_EDITOR
            _trackinId = trackingId;
            AndroidJavaClass ajc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject ajo = ajc.GetStatic<AndroidJavaObject>("currentActivity");
            var jc = new AndroidJavaClass("com.c4m.gawrapper.GAITrackerWrapper");
            jc.CallStatic("trackerWithTrackingId", ajo, _trackinId);
        #endif

        #if UNITY_WEBPLAYER && !UNITY_EDITOR
			_trackinId = trackingId;
			var random = new System.Random();
			_clientId = random.Next( 1000000000 ).ToString();
			Application.ExternalCall("trackerWithTrackingId", _trackinId, _clientId);
        #endif
    }
	
	// --------------------------------------
    // Client Id (Since V3)
	// The method is blocking until the client ID is available
    // --------------------------------------
	public String getClientId()
    {
        #if UNITY_IPHONE && !UNITY_EDITOR
            _clientId = Marshal.PtrToStringAnsi(getClientIdImpl());
        #endif
		
        #if UNITY_ANDROID && !UNITY_EDITOR
            var jc = new AndroidJavaClass("com.c4m.gawrapper.GAITrackerWrapper");
            _clientId = jc.CallStatic<string>("getClientId");
        #endif
		
		#if !UNITY_EDITOR
    		return _clientId;
		#else
			return null;
		#endif
    }
	
    // --------------------------------------
    // Custom Dimensions
    // index : The index of the custom dimension defintion. This index is 1-based.
    // dimension : The value of the custom dimension.
    // --------------------------------------
	public void setCustomDimension(int index, string dimension)
    {
        StartCoroutine(setCustomDimensionAsync(index, dimension==null?"":dimension));
    }
    IEnumerator setCustomDimensionAsync(int index, string dimension)
    {
        yield return new WaitForEndOfFrame();
        #if UNITY_IPHONE && !UNITY_EDITOR
            setCustomDimensionImpl(index, dimension);
        #endif
		
        #if UNITY_ANDROID && !UNITY_EDITOR
            var jc = new AndroidJavaClass("com.c4m.gawrapper.GAITrackerWrapper");
            jc.CallStatic("setCustomDimension", index, dimension);
        #endif
		
		#if UNITY_WEBPLAYER && !UNITY_EDITOR
			Application.ExternalCall("setCustomDimension", index, dimension);
        #endif
    }

    // --------------------------------------
    // Custom Metrics
    // index : The index of the custom metric definition.
    // metric : The value of the custom metric, interpreted as a 64-bit integer. Values can be negative.
    // --------------------------------------
	public void setCustomMetric(int index, long metric) 
    {
        StartCoroutine(setCustomMetricAsync(index, metric));
    }
    IEnumerator setCustomMetricAsync(int index, long metric) 
    {
        yield return new WaitForEndOfFrame();
        #if UNITY_IPHONE && !UNITY_EDITOR
            setCustomMetricImpl(index, metric);
        #endif
		
        #if UNITY_ANDROID && !UNITY_EDITOR
            var jc = new AndroidJavaClass("com.c4m.gawrapper.GAITrackerWrapper");
            jc.CallStatic("setCustomMetric", index, metric);
        #endif
		
		#if UNITY_WEBPLAYER && !UNITY_EDITOR
			Application.ExternalCall("setCustomMetric", index, metric);
        #endif
    }

    // --------------------------------------
    // Event Tracking
    // category : The category of the event, ex : "UI_Action"
    // action : The action related to the event, ex : "Button_pressed"
    // buttonName : The name button responsible of this event
    // aValue : The event value if any
    // --------------------------------------
	public void sendEventWith(string category, string action, string buttonName, long aValue)
    {
        StartCoroutine(sendEventWithAsync(
			category==null?"":category, 
			action==null?"":action, 
			buttonName==null?"":buttonName, 
			aValue));
    }
    IEnumerator sendEventWithAsync(string category, string action, string buttonName, long aValue)
    {
        yield return new WaitForEndOfFrame();
        #if UNITY_IPHONE && !UNITY_EDITOR
            sendEventWithImpl(category, action, buttonName, aValue);
        #endif
		
        #if UNITY_ANDROID && !UNITY_EDITOR
            var jc = new AndroidJavaClass("com.c4m.gawrapper.GAITrackerWrapper");
            jc.CallStatic("sendEventWith", category, action, buttonName, aValue);
        #endif
		
		#if UNITY_WEBPLAYER && !UNITY_EDITOR
			Application.ExternalCall("sendEventWith", category, action, buttonName, aValue);
        #endif
    }

    // --------------------------------------
    // Screen Tracking
    // page : The page name, ex : "/gawrapper"
    // --------------------------------------
	public void sendView(string page)
    {
        StartCoroutine(sendViewAsync(page==null?"":page));
    }
    IEnumerator sendViewAsync(string page)
    {
        yield return new WaitForEndOfFrame();
        #if UNITY_IPHONE && !UNITY_EDITOR
            sendViewImpl(page);
        #endif
        #if UNITY_ANDROID && !UNITY_EDITOR
            var jc = new AndroidJavaClass("com.c4m.gawrapper.GAITrackerWrapper");
            jc.CallStatic("sendView", page);
        #endif
		
		#if UNITY_WEBPLAYER && !UNITY_EDITOR
			Application.ExternalCall("sendView", page);
        #endif
    }

    // --------------------------------------
    // Sessions
    // startSession : Indicates when the session starts and ends
    // --------------------------------------
	public void setStartSession(bool startSession)
    {
        StartCoroutine(setStartSessionAsync(startSession));
    }
    IEnumerator setStartSessionAsync(bool startSession)
    {
        yield return new WaitForEndOfFrame();
        #if UNITY_IPHONE && !UNITY_EDITOR
            setStartSessionImpl(startSession);
        #endif
		
        #if UNITY_ANDROID && !UNITY_EDITOR
            var jc = new AndroidJavaClass("com.c4m.gawrapper.GAITrackerWrapper");
            jc.CallStatic("setStartSession", startSession);
        #endif
		
		#if UNITY_WEBPLAYER && !UNITY_EDITOR
			Application.ExternalCall("setStartSession", startSession);
        #endif
    }

    // --------------------------------------
    // Crashes & Exceptions
    // description (Optional) : a description of the exception (up to 100 characters). Accepts null .
    // fatal : indicates whether the exception was fatal. true indicates fatal.
    // --------------------------------------
	public void sendException(string description, bool fatal)
    {
        StartCoroutine(sendExceptionAsync(description==null?"":description, fatal));
    }
    IEnumerator sendExceptionAsync(string description, bool fatal)
    {
        yield return new WaitForEndOfFrame();
        #if UNITY_IPHONE && !UNITY_EDITOR
            sendExceptionImpl(description, fatal);
        #endif
		
        #if UNITY_ANDROID && !UNITY_EDITOR
            var jc = new AndroidJavaClass("com.c4m.gawrapper.GAITrackerWrapper");
            jc.CallStatic("sendException", description, fatal);
        #endif
		
		#if UNITY_WEBPLAYER && !UNITY_EDITOR
			Application.ExternalCall("sendException", description, fatal);
        #endif
    }

    // --------------------------------------
    // Social Interactions
    // network : represents the social network with which the user is interacting (e.g. Google+, Facebook, Twitter, etc.).
    // action : represents the social action taken (e.g. Like, Share, +1, etc.).
    // target (optional) : represents the content on which the social action is being taken (i.e. a specific article or video).
    // --------------------------------------
	public void sendSocial(string network, string action, string target)
    {
        StartCoroutine(sendSocialAsync(
			network==null?"":network, 
			action==null?"":action, 
			target==null?"":target));
    }
    IEnumerator sendSocialAsync(string network, string action, string target)
    {
        yield return new WaitForEndOfFrame();
        #if UNITY_IPHONE && !UNITY_EDITOR
            sendSocialImpl(network, action, target);
        #endif
		
        #if UNITY_ANDROID && !UNITY_EDITOR
            var jc = new AndroidJavaClass("com.c4m.gawrapper.GAITrackerWrapper");
            jc.CallStatic("sendSocial", network, action, target);
        #endif
		
		#if UNITY_WEBPLAYER && !UNITY_EDITOR
			Application.ExternalCall("sendSocial", network, action, target);
        #endif
    }

    // --------------------------------------
    // User Timings
    // category : the category of the timed event
    // intervalInMilliseconds : the timing measurement in milliseconds
    // nameStr (Optional) : the name of the timed event
    // Label (Optional) : the label of the timed event
    // --------------------------------------
	public void sendTiming(string category, long intervalInMilliseconds, string nameStr, string label)
    {
        StartCoroutine(sendTimingAsync(
			category==null?"":category, 
			intervalInMilliseconds, 
			nameStr==null?"":nameStr, 
			label==null?"":label));
    }
    IEnumerator sendTimingAsync(string category, long intervalInMilliseconds, string nameStr, string label)
    {
		yield return new WaitForEndOfFrame();
        #if UNITY_IPHONE && !UNITY_EDITOR
            sendTimingImpl(category, nameStr, label, intervalInMilliseconds);
        #endif
		
        #if UNITY_ANDROID && !UNITY_EDITOR
            var jc = new AndroidJavaClass("com.c4m.gawrapper.GAITrackerWrapper");
            jc.CallStatic("sendTiming", category, intervalInMilliseconds, nameStr, label);
        #endif
		
		#if UNITY_WEBPLAYER && !UNITY_EDITOR
			Application.ExternalCall("sendTiming", category, intervalInMilliseconds, nameStr, label);
        #endif
    }

    // --------------------------------------
    // Ecommerce Tracking
    // transactionId : Transaction Id, should be unique.
    // orderTotal : Order total (in micros)
    // affiliation : Affiliation
    // totalTax : Total tax (in micros)
    // totalShipping : Total shipping cost (in micros)
    // currencyCode : Set currency code For the complete list of supported currencies and currency codes, see the Supported Currencies Reference.
    // productSKU : Product SKU
    // productName : Product name
    // productPrice : Product price (in micros)
    // quantity : Product quantity
    // productCategory : Product category
    // --------------------------------------
	public void sendTransaction(string transactionId,
            long orderTotal,
            string affiliation,
            long totalTax,
            long totalShipping,
            string currencyCode,
            string productSKU,
            string productName,
            long productPrice,
            int quantity,
            string productCategory
        )
    {
        StartCoroutine(sendTransactionAsync(transactionId,
            orderTotal,
            affiliation==null?"":affiliation,
            totalTax,
            totalShipping,
            currencyCode,
            productSKU==null?"":productSKU,
            productName==null?"":productName,
            productPrice,
            quantity,
            productCategory==null?"":productCategory
        ));
    }
    IEnumerator sendTransactionAsync(string transactionId,
            long orderTotal,
            string affiliation,
            long totalTax,
            long totalShipping,
            string currencyCode,
            string productSKU,
            string productName,
            long productPrice,
            int quantity,
            string productCategory
        )
    {
		yield return new WaitForEndOfFrame();
        #if UNITY_IPHONE && !UNITY_EDITOR
            sendTransactionImpl(transactionId,
                orderTotal,
                affiliation,
                totalTax,
                totalShipping,
                currencyCode,
                productSKU,
                productName,
                productPrice,
                quantity,
                productCategory);
        #endif
		
        #if UNITY_ANDROID && !UNITY_EDITOR
            var jc = new AndroidJavaClass("com.c4m.gawrapper.GAITrackerWrapper");
            jc.CallStatic("sendTransaction", transactionId,
               orderTotal,
               affiliation,
               totalTax,
               totalShipping,
               currencyCode,
               productSKU,
               productName,
               productPrice,
               quantity,
               productCategory);
        #endif
		
		#if UNITY_WEBPLAYER && !UNITY_EDITOR
			Application.ExternalCall("sendTransaction", transactionId,
               orderTotal,
               affiliation,
               totalTax,
               totalShipping,
               currencyCode,
               productSKU,
               productName,
               productPrice,
               quantity,
               productCategory);
        #endif
    }
    // --------------------------------------
    // Campaigns
    // campaignUrl : the campaign url. Be careful, on iOS only it has to be a well formatted url such as "http://www.anyname.com?utm_campaign=my_campaign&utm_source=google&utm_medium=cpc&utm_term=my_keyword&utm_content=ad_variation1"
    // --------------------------------------
    public void setCampaignUrl(string campaignUrl)
    {
        StartCoroutine(setCampaignUrlAsync(campaignUrl==null?"":campaignUrl));
    }
    IEnumerator setCampaignUrlAsync(string campaignUrl)
    {
		yield return new WaitForEndOfFrame();
        #if UNITY_IPHONE && !UNITY_EDITOR
            setCampaignUrlImpl(campaignUrl);
        #endif
		
        #if UNITY_ANDROID && !UNITY_EDITOR
            var jc = new AndroidJavaClass("com.c4m.gawrapper.GAITrackerWrapper");
            jc.CallStatic("setCampaignUrl", campaignUrl);
        #endif
		
		#if UNITY_WEBPLAYER && !UNITY_EDITOR
			Application.ExternalCall("setCampaignUrl", campaignUrl);
        #endif
    }
	
	// --------------------------------------
    // Referrer
    // url : the referree url
    // --------------------------------------
    public void setReferrer(string url)
    {
        StartCoroutine(setReferrerAsync(url==null?"":url));
    }
    IEnumerator setReferrerAsync(string url)
    {
		yield return new WaitForEndOfFrame();
        #if UNITY_IPHONE && !UNITY_EDITOR
            setReferrerImpl(url);
        #endif
		
        #if UNITY_ANDROID && !UNITY_EDITOR
            var jc = new AndroidJavaClass("com.c4m.gawrapper.GAITrackerWrapper");
            jc.CallStatic("setReferrer", url);
        #endif
		
		#if UNITY_WEBPLAYER && !UNITY_EDITOR
			Application.ExternalCall("setReferrer", url);
        #endif
    }
	
	
	// --------------------------------------
    // Dispatch period
    // seconds : the dispatch period in seconds
	//The default dispatch period is 2 minutes on iOS.
	//The default dispatch period is 30 minutes on Android.
    // --------------------------------------
    public void setDispatchPeriod(int seconds)
    {
        StartCoroutine(setDispatchPeriodAsync(seconds));
    }
    IEnumerator setDispatchPeriodAsync(int seconds)
    {
		yield return new WaitForEndOfFrame();
        #if UNITY_IPHONE && !UNITY_EDITOR
            setDispatchPeriodImpl(seconds);
        #endif
		
        #if UNITY_ANDROID && !UNITY_EDITOR
            var jc = new AndroidJavaClass("com.c4m.gawrapper.GAITrackerWrapper");
            jc.CallStatic("setDispatchPeriod", seconds);
        #endif
		
		#if UNITY_WEBPLAYER && !UNITY_EDITOR
			Application.ExternalCall("setDispatchPeriod", seconds);
        #endif
    }
	
	// --------------------------------------
    // Manual dispatch 
    // --------------------------------------
    public void dispatch()
    {
        StartCoroutine(dispatchAsync());
    }
    IEnumerator dispatchAsync()
    {
		yield return new WaitForEndOfFrame();
        #if UNITY_IPHONE && !UNITY_EDITOR
            dispatchImpl();
        #endif
		
        #if UNITY_ANDROID && !UNITY_EDITOR
            var jc = new AndroidJavaClass("com.c4m.gawrapper.GAITrackerWrapper");
            jc.CallStatic("dispatch");
        #endif
		
		#if UNITY_WEBPLAYER && !UNITY_EDITOR
			Application.ExternalCall("dispatch");
        #endif
    }
}