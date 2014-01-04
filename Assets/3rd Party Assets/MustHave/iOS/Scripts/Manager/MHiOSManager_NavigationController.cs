using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public partial class MHiOSManager : Singleton<MHiOSManager> {
	#region navigationController_xcode_output
	[DllImport("__Internal")]
    private static extern int _init_navigationcontroller(int tag);
	public MHNavigationController init_navigationcontroller(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return GetObjectByUniqueTag(_init_navigationcontroller(tag)) as MHNavigationController;
		}
		else
			return new MHNavigationController();
	}
	
	[DllImport("__Internal")]
    private static extern int _initWithRootViewController(int tag, int rootViewController);
	public MHNavigationController initWithRootViewController(int tag, MHViewController rootViewController)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int rootViewControllerID = MHTools.GetObjectTag(rootViewController);

            int newTag = _initWithRootViewController(tag, rootViewControllerID);
			return GetObjectByUniqueTag(newTag) as MHNavigationController;
        }
		else
			return new MHNavigationController();
	}
	
	[DllImport("__Internal")]
    private static extern int _topViewController(int tag);
	public MHViewController topViewController(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int topViewControllerID = _topViewController(tag);
			
			return GetObjectByUniqueTag(topViewControllerID) as MHViewController;
		}
		else
			return new MHViewController();
	}
	
	[DllImport("__Internal")]
    private static extern int _visibleViewController(int tag);
	public MHViewController visibleViewController(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int viewTag = _visibleViewController(tag);
			
			return GetObjectByUniqueTag(viewTag) as MHViewController;
		}
		else
			return new MHViewController();
	}
	
	[DllImport("__Internal")]
    private static extern string _viewControllers(int tag);
	public MHViewController[] viewControllers(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int[] viewTags = MHTools.ConvertJSONToIntArray(_viewControllers(tag));
			
			return GetObjectsByUniqueTags(viewTags) as MHViewController[];
		}
		else
			return new MHViewController[0];
	}
	
	[DllImport("__Internal")]
    private static extern void _viewControllers_set(int tag, int[] viewControllers);
	public void viewControllers(int tag, MHViewController[] viewControllers)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int[] viewTags = MHTools.GetObjectTags(viewControllers);
			
			_viewControllers_set(tag, viewTags);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _setViewControllers(int tag, int[] viewControllers, bool animated);
	public void setViewControllers(int tag, MHViewController[] viewControllers, bool animated)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int[] viewTags = MHTools.GetObjectTags(viewControllers);
			
			_setViewControllers(tag, viewTags, animated);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _pushViewController(int tag, int viewController, bool animated);
	public void pushViewController(int tag, MHViewController viewController, bool animated)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int viewTag = viewController.tag;
			
			_pushViewController(tag, viewTag, animated);
		}
	}
	
	[DllImport("__Internal")]
    private static extern int _popViewControllerAnimated(int tag, bool animated);
	public MHViewController popViewControllerAnimated(int tag, bool animated)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int viewTag = _popViewControllerAnimated(tag, animated);
			
			return GetObjectByUniqueTag(viewTag) as MHViewController;
		}
		else
			return new MHViewController();
	}
	
	[DllImport("__Internal")]
    private static extern string _popToRootViewControllerAnimated(int tag, bool animated);
	public MHViewController[] popToRootViewControllerAnimated(int tag, bool animated)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int[] viewTags = MHTools.ConvertJSONToIntArray(_popToRootViewControllerAnimated(tag, animated));
			
			return GetObjectsByUniqueTags(viewTags) as MHViewController[];
		}
		else
			return new MHViewController[0];
	}
	
	[DllImport("__Internal")]
    private static extern string _popToViewController(int tag, int viewController, bool animated);
	public MHViewController[] popToViewController(int tag, MHViewController viewController, bool animated)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int viewTag = viewController.tag;
			
			int[] viewTags = MHTools.ConvertJSONToIntArray(_popToViewController(tag, viewTag, animated));
			
			return GetObjectsByUniqueTags(viewTags) as MHViewController[];
		}
		else
			return new MHViewController[0];
	}
	
	[DllImport("__Internal")]
    private static extern int _navigationBar(int tag);
	public MHNavigationBar navigationBar(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int barTag = _navigationBar(tag);
			
			return GetObjectByUniqueTag(barTag) as MHNavigationBar;
		}
		else
			return new MHNavigationBar();
	}
	
	[DllImport("__Internal")]
    private static extern bool _navigationBarHidden(int tag);
	public bool navigationBarHidden(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _navigationBarHidden(tag);
		}
		else
			return false;
	}
	
	[DllImport("__Internal")]
    private static extern void _navigationBarHidden_set(int tag, bool hidden);
	public void navigationBarHidden(int tag, bool hidden)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_navigationBarHidden_set(tag, hidden);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _setNavigationBarHidden(int tag, bool hidden, bool animated);
	public void setNavigationBarHidden(int tag, bool hidden, bool animated)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_setNavigationBarHidden(tag, hidden, animated);
		}
	}
	
	[DllImport("__Internal")]
    private static extern int _toolbar(int tag);
	public MHToolbar toolbar(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int barTag = _toolbar(tag);
			
			return GetObjectByUniqueTag(barTag) as MHToolbar;
		}
		else
			return new MHToolbar();
	}
	
	[DllImport("__Internal")]
    private static extern void _setToolbarHidden(int tag, bool hidden, bool animated);
	public void setToolbarHidden(int tag, bool hidden, bool animated)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_setToolbarHidden(tag, hidden, animated);
		}
	}
	
	[DllImport("__Internal")]
    private static extern bool _toolbarHidden(int tag);
	public bool toolbarHidden(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _toolbarHidden(tag);
		}
		else
			return false;
	}
	
	[DllImport("__Internal")]
    private static extern void _toolbarHidden_set(int tag, bool hidden);
	public void toolbarHidden(int tag, bool hidden)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_toolbarHidden_set(tag, hidden);
		}
	}
	#endregion
			
	#region navigationController_xcode_input
	void willShowViewController(string jsonString)
	{
		int tag;
		MHNavigationController navigationController;
		MHViewController viewController;
		bool animated;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			navigationController = GetObjectByUniqueTag(Convert.ToInt32(result["navigationController"])) as MHNavigationController;
			viewController = GetObjectByUniqueTag(Convert.ToInt32(result["viewController"])) as MHViewController;
			animated = Convert.ToBoolean(result["animated"]);
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHNavigationController)
				(obj as MHNavigationController)._willShowViewController(navigationController, viewController, animated);
		}
	}
	
	void didShowViewController(string jsonString)
	{
		int tag;
		MHNavigationController navigationController;
		MHViewController viewController;
		bool animated;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			navigationController = GetObjectByUniqueTag(Convert.ToInt32(result["navigationController"])) as MHNavigationController;
			viewController = GetObjectByUniqueTag(Convert.ToInt32(result["viewController"])) as MHViewController;
			animated = Convert.ToBoolean(result["animated"]);
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHNavigationController)
				(obj as MHNavigationController)._didShowViewController(navigationController, viewController, animated);
		}
	}
	#endregion
}