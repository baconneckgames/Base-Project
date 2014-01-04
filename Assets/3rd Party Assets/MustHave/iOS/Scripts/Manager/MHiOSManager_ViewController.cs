using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public partial class MHiOSManager : Singleton<MHiOSManager> {
	#region viewController_xcode_output
	[DllImport("__Internal")]
    private static extern void _syncNavitem(int tag, int viewController);
	public void syncNavitem(int tag, MHViewController viewController)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int viewTag = viewController.tag;
			
			_syncNavitem(tag, viewTag);
		}
	}
	
	[DllImport("__Internal")]
    private static extern int _init_viewcontroller(int tag);
	public MHViewController init_viewcontroller(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int newTag = _init_viewcontroller(tag);
			
			return GetObjectByUniqueTag(newTag) as MHViewController;
        }
		else
			return new MHViewController();
	}
	
	[DllImport("__Internal")]
    private static extern int _view(int tag);
	public MHView view(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int viewTag = _view(tag);
			
			if(viewTag == 0)
				return MHView.unityView;
			return GetObjectByUniqueTag(viewTag) as MHView;
        }
		else
			return new MHView();
	}
	
	[DllImport("__Internal")]
    private static extern void _view_set(int tag, int viewTag);
	public void view(int tag, MHView newView)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int newViewTag = newView.tag;
			
			_view_set(tag, newViewTag);
        }
	}
	
	[DllImport("__Internal")]
    private static extern bool _isViewLoaded(int tag);
	public bool isViewLoaded(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _isViewLoaded(tag);
		}
		else
			return false;
	}
	
	[DllImport("__Internal")]
    private static extern bool _isMovingFromParentViewController(int tag);
	public bool isMovingFromParentViewController(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _isMovingFromParentViewController(tag);
		}
		else
			return false;
	}
	
	[DllImport("__Internal")]
    private static extern bool _isMovingToParentViewController(int tag);
	public bool isMovingToParentViewController(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _isMovingFromParentViewController(tag);
		}
		else
			return false;
	}
	
	[DllImport("__Internal")]
    private static extern bool _isBeingPresented(int tag);
	public bool isBeingPresented(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _isBeingPresented(tag);
		}
		else
			return false;
	}
	
	[DllImport("__Internal")]
    private static extern bool _isBeingDismissed(int tag);
	public bool isBeingDismissed(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _isBeingDismissed(tag);
		}
		else
			return false;
	}
	
	[DllImport("__Internal")]
    private static extern bool _wantsFullScreenLayout(int tag);
	public bool wantsFullScreenLayout(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _wantsFullScreenLayout(tag);
		}
		else
			return false;
	}
	
	[DllImport("__Internal")]
    private static extern void _wantsFullScreenLayout_set(int tag, bool fullscreen);
	public void wantsFullScreenLayout(int tag, bool fullscreen)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_wantsFullScreenLayout_set(tag, fullscreen);
		}
	}
	
	[DllImport("__Internal")]
    private static extern bool _shouldAutorotate(int tag);
	public bool shouldAutorotate(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _shouldAutorotate(tag);
		}
		else
			return false;
	}
	
	[DllImport("__Internal")]
    private static extern int _supportedInterfaceOrientations(int tag);
	public MHInterfaceOrientationMask supportedInterfaceOrientations(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int orientations = _supportedInterfaceOrientations(tag);
			
			return (MHInterfaceOrientationMask)orientations;
		}
		else
			return 0;
	}
	
	[DllImport("__Internal")]
    private static extern int _preferredInterfaceOrientationForPresentation(int tag);
	public MHInterfaceOrientation preferredInterfaceOrientationForPresentation(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return (MHInterfaceOrientation)_preferredInterfaceOrientationForPresentation(tag);
		}
		else
			return MHInterfaceOrientation.MHInterfaceOrientationPortrait;
	}
	
	[DllImport("__Internal")]
    private static extern int _interfaceOrientation(int tag);
    public MHInterfaceOrientation interfaceOrientation(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return (MHInterfaceOrientation)_interfaceOrientation(tag);
		}
		else
			return MHInterfaceOrientation.MHInterfaceOrientationPortrait;
	}
	
	[DllImport("__Internal")]
    private static extern void _attemptRotationToDeviceOrientation(int tag);
	public void attemptRotationToDeviceOrientation(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_attemptRotationToDeviceOrientation(tag);
		}
	}
	
	[DllImport("__Internal")]
    private static extern int _rotatingHeaderView(int tag);
	public MHView rotatingHeaderView(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int viewTag = _rotatingHeaderView(tag);
			
			return GetObjectByUniqueTag(viewTag) as MHView;
		}
		else
			return new MHView();
	}
	
	[DllImport("__Internal")]
    private static extern int _rotatingFooterView(int tag);
	public MHView rotatingFooterView(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int viewTag = _rotatingFooterView(tag);
			
			return GetObjectByUniqueTag(viewTag) as MHView;
		}
		else
			return new MHView();
	}
	
	[DllImport("__Internal")]
    private static extern void _editing_set(int tag, bool edit);
	public void editing(int tag, bool edit)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_editing_set(tag, edit);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _setEditing(int tag, bool editing, bool animated);
	public void setEditing(int tag, bool editing, bool animated)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_setEditing(tag, editing, animated);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _presentViewController(int tag, int viewControllerToPresent, bool animated, int completion);
	public void presentViewController(int tag, MHViewController viewControllerToPresent, bool animated, Action completion)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int controllerTag = viewControllerToPresent.tag;
			int completionID = SaveActionToUniqueID(completion);
			
			_presentViewController(tag, controllerTag, animated, completionID);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _dismissViewControllerAnimated(int tag, bool animated, int completion);
	public void dismissViewControllerAnimated(int tag, bool animated, Action completion)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int completionID = SaveActionToUniqueID(completion);
			
			_dismissViewControllerAnimated(tag, animated, completionID);
		}
	}
	
	[DllImport("__Internal")]
    private static extern int _modalTransitionStyle(int tag);
	public MHModalTransitionStyle modalTransitionStyle(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return (MHModalTransitionStyle)_modalTransitionStyle(tag);
		}
		else
			return MHModalTransitionStyle.MHModalTransitionStyleCoverVertical;
	}
	
	[DllImport("__Internal")]
    private static extern void _modalTransitionStyle_set(int tag, int style);
	public void modalTransitionStyle(int tag, MHModalTransitionStyle style)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_modalTransitionStyle_set(tag, (int)style);
		}
	}
	
	[DllImport("__Internal")]
    private static extern int _modalPresentationStyle(int tag);
	public MHModalPresentationStyle modalPresentationStyle(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return (MHModalPresentationStyle)_modalPresentationStyle(tag);
		}
		else
			return MHModalPresentationStyle.MHModalPresentationCurrentContext;
	}
	
	[DllImport("__Internal")]
    private static extern void _modalPresentationStyle_set(int tag, int style);
	public void modalPresentationStyle(int tag, MHModalPresentationStyle style)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_modalPresentationStyle_set(tag, (int)style);
		}
	}
	
	[DllImport("__Internal")]
    private static extern bool _definesPresentationContext(int tag);
	public bool definesPresentationContext(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _definesPresentationContext(tag);
		}
		else
			return false;
	}
	
	[DllImport("__Internal")]
    private static extern void _definesPresentationContext_set(int tag, bool context);
	public void definesPresentationContext(int tag, bool context)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_definesPresentationContext_set(tag, context);
		}
	}
	
	[DllImport("__Internal")]
    private static extern bool _providesPresentationContextTransitionStyle(int tag);
	public bool providesPresentationContextTransitionStyle(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _providesPresentationContextTransitionStyle(tag);
		}
		else
			return false;
	}
	
	[DllImport("__Internal")]
    private static extern void _providesPresentationContextTransitionStyle_set(int tag, bool context);
	public void providesPresentationContextTransitionStyle(int tag, bool context)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_providesPresentationContextTransitionStyle_set(tag, context);
		}
	}
	
	[DllImport("__Internal")]
    private static extern bool _disablesAutomaticKeyboardDismissal(int tag);
	public bool disablesAutomaticKeyboardDismissal(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _disablesAutomaticKeyboardDismissal(tag);
		}
		else
			return false;
	}
	
	[DllImport("__Internal")]
    private static extern int _presentingViewController(int tag);
	public MHViewController presentingViewController(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int presentingTag = _presentingViewController(tag);
		
			return GetObjectByUniqueTag(presentingTag) as MHViewController;
		}
		else
			return new MHViewController();
	}
	
	[DllImport("__Internal")]
    private static extern int _presentedViewController(int tag);
	public MHViewController presentedViewController(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int presentedTag = _presentedViewController(tag);
		
			return GetObjectByUniqueTag(presentedTag) as MHViewController;
		}
		else
			return new MHViewController();
	}
	
	[DllImport("__Internal")]
    private static extern int _parentViewController(int tag);
	public MHViewController parentViewController(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int parentTag = _parentViewController(tag);
		
			return GetObjectByUniqueTag(parentTag) as MHViewController;
		}
		else
			return new MHViewController();
	}
	
	[DllImport("__Internal")]
    private static extern string _childViewControllers(int tag);
	public MHViewController[] childViewControllers(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int[] childTags = MHTools.ConvertJSONToIntArray(_childViewControllers(tag));
		
			return GetObjectsByUniqueTags(childTags) as MHViewController[];
		}
		else
			return new MHViewController[0];
	}
	
	[DllImport("__Internal")]
    private static extern void _addChildViewController(int tag, int childController);
	public void addChildViewController(int tag, MHViewController childController)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int viewTag = childController.tag;
			
			_addChildViewController(tag, viewTag);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _removeFromParentViewController(int tag);
	public void removeFromParentViewController(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_removeFromParentViewController(tag);
		}
	}
	
	[DllImport("__Internal")]
    private static extern bool _shouldAutomaticallyForwardRotationMethods(int tag);
	public bool shouldAutomaticallyForwardRotationMethods(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _shouldAutomaticallyForwardRotationMethods(tag);
		}
		else
			return false;
	}
	
	[DllImport("__Internal")]
    private static extern bool _shouldAutomaticallyForwardAppearanceMethods(int tag);
	public bool shouldAutomaticallyForwardAppearanceMethods(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _shouldAutomaticallyForwardAppearanceMethods(tag);
		}
		else
			return false;
	}
	
	//animations is always empty block
	[DllImport("__Internal")]
    private static extern void _transitionFromViewController(int tag, int fromViewController, int toViewController, float duration, int options, int completion);
	public void transitionFromViewController(int tag, MHViewController fromViewController, MHViewController toViewController, float duration, MHViewAnimationOptions options, Action<bool> completion)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int fromView = fromViewController.tag;
			int toView = toViewController.tag;
			int completionID = SaveActionToUniqueID(completion);
			
			_transitionFromViewController(tag, fromView, toView, duration, (int)options, completionID);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _beginAppearanceTransition(int tag, bool isAppearing, bool animated);
	public void beginAppearanceTransition(int tag, bool isAppearing, bool animated)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_beginAppearanceTransition(tag, isAppearing, animated);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _endAppearanceTransition(int tag);
	public void endAppearanceTransition(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_endAppearanceTransition(tag);
		}
	}
	
	[DllImport("__Internal")]
    private static extern int _editButtonItem(int tag);
	public MHBarButtonItem editButtonItem(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int buttonTag = _editButtonItem(tag);
			
			return GetObjectByUniqueTag(buttonTag) as MHBarButtonItem;
		}
		else
			return new MHBarButtonItem();
	}
	
	[DllImport("__Internal")]
    private static extern void _setToolbarItems(int tag, int[] toolbarItems, bool animated);
	public void setToolbarItems(int tag, MHBarButtonItem[] toolbarItems, bool animated)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int[] buttonTags = MHTools.GetObjectTags(toolbarItems);
			
			_setToolbarItems(tag, buttonTags, animated);
		}
	}
	
	[DllImport("__Internal")]
    private static extern string _toolbarItems(int tag);
	public MHBarButtonItem[] toolbarItems(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int[] toolItems = MHTools.ConvertJSONToIntArray(_toolbarItems(tag));
			
			return GetObjectsByUniqueTags(toolItems) as MHBarButtonItem[];
		}
		else
			return new MHBarButtonItem[0];
	}
	
	[DllImport("__Internal")]
    private static extern void _toolbarItems_set(int tag, int[] items);
	public void toolbarItems(int tag, MHBarButtonItem[] items)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int[] buttomItems = MHTools.GetObjectTags(items);
			
			_toolbarItems_set(tag, buttomItems);
		}
	}
	#endregion
	
	#region viewController_xcode_input
	void viewDidLoad(string jsonString)
	{
		int tag;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHViewController)
				(obj as MHViewController)._viewDidLoad();
		}
	}
	
	void viewWillAppear(string jsonString)
	{
		int tag;
		bool animated;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			animated = Convert.ToBoolean(result["animated"]);
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHViewController)
				(obj as MHViewController)._viewWillAppear(animated);
		}
	}
	
	void viewDidAppear(string jsonString)
	{
		int tag;
		bool animated;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			animated = Convert.ToBoolean(result["animated"]);
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHViewController)
				(obj as MHViewController)._viewDidAppear(animated);
		}
	}
	
	void viewWillDisappear(string jsonString)
	{
		int tag;
		bool animated;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			animated = Convert.ToBoolean(result["animated"]);
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHViewController)
				(obj as MHViewController)._viewWillDisappear(animated);
		}
	}
	
	void viewDidDisappear(string jsonString)
	{
		int tag;
		bool animated;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			animated = Convert.ToBoolean(result["animated"]);
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHViewController)
				(obj as MHViewController)._viewDidDisappear(animated);
		}
	}
	
	void viewWillLayoutSubviews(string jsonString)
	{
		int tag;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHViewController)
				(obj as MHViewController)._viewWillLayoutSubviews();
		}
	}
	
	void viewDidLayoutSubviews(string jsonString)
	{
		int tag;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHViewController)
				(obj as MHViewController)._viewDidLayoutSubviews();
		}
	}
	
	void didReceiveMemoryWarning(string jsonString)
	{
		int tag;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHViewController)
				(obj as MHViewController)._didReceiveMemoryWarning();
		}
	}
	
	void willRotateToInterfaceOrientation(string jsonString)
	{
		int tag;
		MHInterfaceOrientation interfaceOrientation;
		float duration;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			interfaceOrientation = (MHInterfaceOrientation)(Convert.ToInt32(result["orientation"]));
			duration = Convert.ToSingle(result["duration"]);
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHViewController)
				(obj as MHViewController)._willRotateToInterfaceOrientation(interfaceOrientation, duration);
		}
	}
	
	void willAnimateRotationToInterfaceOrientation(string jsonString)
	{
		int tag;
		MHInterfaceOrientation interfaceOrientation;
		float duration;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			interfaceOrientation = (MHInterfaceOrientation)(Convert.ToInt32(result["orientation"]));
			duration = Convert.ToSingle(result["duration"]);
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHViewController)
				(obj as MHViewController)._willAnimateRotationToInterfaceOrientation(interfaceOrientation, duration);
		} 
	}
	
	void didRotateFromInterfaceOrientation(string jsonString)
	{
		int tag;
		MHInterfaceOrientation fromInterfaceOrientation;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			fromInterfaceOrientation = (MHInterfaceOrientation)(Convert.ToInt32(result["orientation"]));
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHViewController)
				(obj as MHViewController)._didRotateFromInterfaceOrientation(fromInterfaceOrientation);
		}
	}
	
	void willMoveToParentViewController(string jsonString)
	{
		int tag;
		MHViewController parent;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			parent = GetObjectByUniqueTag(Convert.ToInt32(result["parent"])) as MHViewController;
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHViewController)
				(obj as MHViewController)._willMoveToParentViewController(parent);
		}
	}
	
	void didMoveToParentViewController(string jsonString)
	{
		int tag;
		MHViewController parent;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			parent = GetObjectByUniqueTag(Convert.ToInt32(result["parent"])) as MHViewController;
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHViewController)
				(obj as MHViewController)._didMoveToParentViewController(parent);
		}
	}
	#endregion
}
