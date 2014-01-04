using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;

public class EasyAnalyticsExample : MonoBehaviour {

	void Start () {
		//Initialize the plugin with a Google Analytics tracking Id
		//App
		EasyAnalytics.Instance.trackerWithTrackingId("UA-11001829-23");
		//universal
		//EasyAnalytics.Instance.trackerWithTrackingId("UA-11001829-26");
		
		EasyAnalytics.Instance.setDispatchPeriod(30);
	}

	void OnGUI ()
	{
		//Create a button for each actions
		if (GUI.Button(new Rect (10, 125, 200, 70), "sendView hit"))
		{
			EasyAnalytics.Instance.sendView("/gaexample");
		}
		
		if (GUI.Button(new Rect (230, 125, 200, 70), "setCustomDimension"))
		{
			EasyAnalytics.Instance.setCustomDimension(1, "SOME_DIMENSION_VALUE_1");
			EasyAnalytics.Instance.setCustomDimension(2, "SOME_DIMENSION_VALUE_2");
		}
		
		if (GUI.Button(new Rect (10, 225, 200, 70), "sendEventWith"))
		{
			EasyAnalytics.Instance.sendEventWith("EVENT_CATEGORIE", "EVENT_ACTION", "EVENT_BUTTON", 123);
		}
		
		if (GUI.Button(new Rect (230, 225, 200, 70), "setCustomMetric"))
		{
			EasyAnalytics.Instance.setCustomMetric(1, 12345);
		}
		
		if (GUI.Button(new Rect (10, 325, 200, 70), "setStartSession"))
		{
			EasyAnalytics.Instance.setStartSession(true);
		}
		
		if (GUI.Button(new Rect (230, 325, 200, 70), "sendException"))
		{
			EasyAnalytics.Instance.sendException("AN_EXCEPTION_1", true);
			EasyAnalytics.Instance.sendException("AN_EXCEPTION_2", false);
		}
		
		if (GUI.Button(new Rect (10, 425, 200, 70), "sendSocial"))
		{
			EasyAnalytics.Instance.sendSocial("Twitter", "Tweet", "https://developers.google.com/analytics");
		}
		
		if (GUI.Button(new Rect (230, 425, 200, 70), "sendTiming"))
		{
			EasyAnalytics.Instance.sendTiming("A_CATEGORY", 12345, "CAT_NAME", "CAT_LABEL");
		}
		
		if (GUI.Button(new Rect (10, 525, 200, 70), "sendTransaction"))
		{
			EasyAnalytics.Instance.sendTransaction("TRANSACTION_ID_12345",
												(long)(2.16 * 1000000),
												"In-App Store",
												(long)(0.17 * 1000000),
	                                       		0,
												null, //null for auto currency, otherwise currency code only available on Android (ie : "EUR")
												"PRODUCT_SKU_1",
                                       			"PRODUCT_NAME",
                                       			(long)(1.99 * 1000000),
                                       			1,
												"PRODUCT_CAT");
			
		}
		
		if (GUI.Button(new Rect (230, 525, 200, 70), "setCampaign"))
		{
			EasyAnalytics.Instance.setCampaignUrl("http://www.anyname.com?utm_campaign=my_campaign&utm_source=google_test&utm_medium=cpc&utm_term=my_keyword&utm_content=ad_variation1");
		}
		
		if (GUI.Button(new Rect (10, 625, 200, 70), "setReferrer"))
		{
			EasyAnalytics.Instance.setReferrer("http://www.anyname.com");
		}
		
		if (GUI.Button(new Rect (230, 625, 200, 70), "dispatch now!"))
		{
			EasyAnalytics.Instance.dispatch();
		}
		
		if (GUI.Button(new Rect (10, 725, 200, 70), "setStopSession"))
		{
			EasyAnalytics.Instance.setStartSession(false);
		}
		
		if (GUI.Button(new Rect (230, 725, 200, 70), "getClientId"))
		{
			Debug.Log("EasyAnalytics - getClientId: "+EasyAnalytics.Instance.getClientId());
		}
		
	}
	
	public void Update() {
			
		if (Input.GetKeyDown(KeyCode.Escape)) { 
			Application.Quit();
		}
		
	}
	
	
}