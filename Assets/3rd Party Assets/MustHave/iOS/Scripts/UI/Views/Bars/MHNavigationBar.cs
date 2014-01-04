using UnityEngine;
using System;
using System.Collections;

public class MHNavigationBar : MHView {
	#region functions
	public MHNavigationBar(){}
	
	public MHNavigationBar(bool autoInitialize)
	{
		if(Application.isPlaying && autoInitialize)
			init();
	}
	
	public MHNavigationBar(Rect aRect)
	{
		if(Application.isPlaying)
			initWithFrame(aRect);
	}
	
	public MHNavigationBar(MHNavigationController navController)
	{
		if(navController != null)
			MHiOSManager.Instance.syncNavbar(tag, navController);
	}
	
	new public MHNavigationBar init()
	{
		return MHiOSManager.Instance.init_navigationbar(tag);
	}
	
	new public MHNavigationBar initWithFrame(Rect aRect)
	{
		return MHiOSManager.Instance.initWithFrame_navigationbar(tag, aRect);
	}
	
	public MHBarStyle barStyle {
		get {
			return MHiOSManager.Instance.barStyle(tag);
		} set {
			MHiOSManager.Instance.barStyle(tag, value);
		}
	}
	
	public Texture2D shadowImage {
		get {
			return MHiOSManager.Instance.shadowImage(tag);
		} set {
			MHiOSManager.Instance.shadowImage(tag, value);
		}
	}
	
	public bool translucent {
		get {
			return MHiOSManager.Instance.translucent(tag);
		} set {
			MHiOSManager.Instance.translucent(tag, value);
		}
	}
	
	public void pushNavigationItem(MHNavigationItem item, bool animated)
	{
		MHiOSManager.Instance.pushNavigationItem(tag, item, animated);
	}
	
	public MHNavigationItem popNavigationItem(bool animated)
	{
		return MHiOSManager.Instance.popNavigationItem(tag, animated);
	}
	
	public void setItems(MHNavigationItem[] items, bool animated)
	{
		MHiOSManager.Instance.setItems(tag, items, animated);
	}
	
	public MHNavigationItem[] items {
		get {
			return MHiOSManager.Instance.items_navbar(tag);
		} set {
			MHiOSManager.Instance.items_navbar(tag, value);
		}
	}
	
	public MHNavigationItem topItem {
		get {
			return MHiOSManager.Instance.topItem(tag);
		}
	}
	
	public MHNavigationItem backItem {
		get {
			return MHiOSManager.Instance.backItem(tag);
		}
	}
	
	public Color tintColor {
		get {
			return MHiOSManager.Instance.tintColor(tag);
		} set {
			MHiOSManager.Instance.tintColor(tag, value);
		}
	}
	
	public Texture2D backgroundImageForBarMetrics(MHBarMetrics barMetrics)
	{
		return MHiOSManager.Instance.backgroundImageForBarMetrics(tag, barMetrics);
	}
	
	public void setBackgroundImage(Texture2D backgroundImage, MHBarMetrics barMetrics)
	{
		MHiOSManager.Instance.setBackgroundImage_metrics(tag, backgroundImage, barMetrics);
	}
	
	public float titleVerticalPositionAdjustmentForBarMetrics(MHBarMetrics barMetrics)
	{
		return MHiOSManager.Instance.titleVerticalPositionAdjustmentForBarMetrics(tag, barMetrics);
	}
	
	public void setTitleVerticalPositionAdjustment(float adjustment, MHBarMetrics barMetrics)
	{
		MHiOSManager.Instance.setTitleVerticalPositionAdjustment(tag, adjustment, barMetrics);
	}
	
	public Hashtable titleTextAttributes {
		get {
			return MHiOSManager.Instance.titleTextAttributes(tag);
		} set {
			MHiOSManager.Instance.titleTextAttributes(tag, value);
		}
	}
	#endregion
	
	#region delegate
	public event Func<MHNavigationBar, MHNavigationItem, bool> shouldPushItem;
	public void _shouldPushItem(MHNavigationBar navigationBar, MHNavigationItem item)
	{
		if(shouldPushItem != null)
			MHTools.ReturnResultToXCode((shouldPushItem(navigationBar, item)).ToString());
		else
			MHTools.ReturnResultToXCode("true");
	}
	
	public event Action<MHNavigationBar, MHNavigationItem> didPushItem;
	public void _didPushItem(MHNavigationBar navigationBar, MHNavigationItem item)
	{
		if(didPushItem != null)
			didPushItem(navigationBar, item);
	}
	
	public event Func<MHNavigationBar, MHNavigationItem, bool> shouldPopItem;
	public void _shouldPopItem(MHNavigationBar navigationBar, MHNavigationItem item)
	{
		if(shouldPopItem != null)
			MHTools.ReturnResultToXCode((shouldPopItem(navigationBar, item)).ToString());
		else
			MHTools.ReturnResultToXCode("true");
	}
	
	public event Action<MHNavigationBar, MHNavigationItem> didPopItem;
	public void _didPopItem(MHNavigationBar navigationBar, MHNavigationItem item)
	{
		if(didPopItem != null)
			didPopItem(navigationBar, item);
	}
	#endregion
}
