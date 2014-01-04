using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public partial class MHiOSManager : Singleton<MHiOSManager> {
	#region popoverController_xcode_output
	[DllImport("__Internal")]
    private static extern int _init_popovercontroller(int tag);
	public MHPopoverController init_popovercontroller(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return GetObjectByUniqueTag(_init_popovercontroller(tag)) as MHPopoverController;
		}
		else
			return new MHPopoverController();
	}
	
	[DllImport("__Internal")]
    private static extern int _initWithContentViewController(int tag, int viewController);
	public MHPopoverController initWithContentViewController(int tag, MHViewController viewController)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int viewControllerID = MHTools.GetObjectTag(viewController);

            int newTag = _initWithContentViewController(tag, viewControllerID);
			return GetObjectByUniqueTag(newTag) as MHPopoverController;
        }
		else
			return new MHPopoverController();
	}
	
	[DllImport("__Internal")]
    private static extern int _contentViewController(int tag);
	public MHViewController contentViewController(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int contentTag = _contentViewController(tag);
			
			return GetObjectByUniqueTag(contentTag) as MHViewController;
		}
		else
			return new MHViewController();
	}
	
	[DllImport("__Internal")]
    private static extern void _contentViewController_set(int tag, int viewController);
	public void contentViewController(int tag, MHViewController viewController)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int viewTag = viewController.tag;
			
			_contentViewController_set(tag, viewTag);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _setContentViewController(int tag, int viewController, bool animated);
	public void setContentViewController(int tag, MHViewController viewController, bool animated)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int viewTag = viewController.tag;
			
			_setContentViewController(tag, viewTag, animated);
		}
	}
	
	[DllImport("__Internal")]
    private static extern string _popoverContentSize(int tag);
	public Vector2 popoverContentSize(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string vectorJsonString = _popoverContentSize(tag);
			
			return MHTools.ConvertJSONToVector2(vectorJsonString);
		}
		else
			return Vector2.zero;
	}
	
	[DllImport("__Internal")]
    private static extern void _popoverContentSize_set(int tag, string size);
	public void popoverContentSize(int tag, Vector2 size)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string vectorJsonString = MHTools.ConvertVector2ToJSON(size);
			
			_popoverContentSize_set(tag, vectorJsonString);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _setPopoverContentSize(int tag, string size, bool animated);
	public void setPopoverContentSize(int tag, Vector2 size, bool animated)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string vectorJsonString = MHTools.ConvertVector2ToJSON(size);
			
			_setPopoverContentSize(tag, vectorJsonString, animated);
		}
	}
	
	[DllImport("__Internal")]
    private static extern string _passthroughViews(int tag);
	public MHView[] passthroughViews(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int[] viewTags = MHTools.ConvertJSONToIntArray(_passthroughViews(tag));
			
			return GetObjectsByUniqueTags(viewTags) as MHView[];
		}
		else
			return new MHView[0];
	}
	
	[DllImport("__Internal")]
    private static extern void _passthroughViews_set(int tag, int[] views);
	public void passthroughViews(int tag, MHView[] views)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int[] viewTags = MHTools.GetObjectTags(views);
			
			_passthroughViews_set(tag, viewTags);
		}
	}
	
	[DllImport("__Internal")]
    private static extern bool _popoverVisible(int tag);
   	public bool popoverVisible(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _popoverVisible(tag);
		}
		else
			return false;
	}
	
	[DllImport("__Internal")]
    private static extern int _popoverArrowDirection(int tag);
	public MHPopoverArrowDirection popoverArrowDirection(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return (MHPopoverArrowDirection)_popoverArrowDirection(tag);
		}
		else
			return MHPopoverArrowDirection.MHPopoverArrowDirectionAny;
	}

	[DllImport("__Internal")]
    private static extern void _presentPopoverFromRect(int tag, string rect, int inView, int permittedArrowDirections, bool animated);
	public void presentPopoverFromRect(int tag, Rect rect, MHView inView, MHPopoverArrowDirection permittedArrowDirections, bool animated)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
            int inViewID = MHTools.GetObjectTag(inView);
			
			string rectJSON = MHTools.ConvertRectToJSON(rect);
			
            _presentPopoverFromRect(tag, rectJSON, inViewID, (int)permittedArrowDirections, animated);
        }
	}
	
	[DllImport("__Internal")]
    private static extern void _presentPopoverFromBarButtonItem(int tag, int item, int permittedArrowDirections, bool animated);
	public void presentPopoverFromBarButtonItem(int tag, MHBarButtonItem item, MHPopoverArrowDirection permittedArrowDirections, bool animated)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int barTag = item.tag;
			
			_presentPopoverFromBarButtonItem(tag, barTag, (int)permittedArrowDirections, animated);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _dismissPopoverAnimated(int tag, bool animated);
	public void dismissPopoverAnimated(int tag, bool animated)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_dismissPopoverAnimated(tag, animated);
		}
	}
	
	[DllImport("__Internal")]
    private static extern string _popoverLayoutMargins(int tag);
	public Vector4 popoverLayoutMargins(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string vectorJsonString = _popoverLayoutMargins(tag);
			
			return MHTools.ConvertJSONToVector4(vectorJsonString);
		}
		else
			return Vector4.zero;
	}
	
	[DllImport("__Internal")]
    private static extern void _popoverLayoutMargins_set(int tag, string edgeInsets);
	public void popoverLayoutMargins(int tag, Vector4 edgeInsets)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string vectorJsonString = MHTools.ConvertVector4ToJSON(edgeInsets);
			
			_popoverLayoutMargins_set(tag, vectorJsonString);
		}
	}
	#endregion
	
	#region popoverController_xcode_input
	void popoverControllerShouldDismissPopover(string jsonString)
	{
		int tag;
		MHPopoverController popoverController;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			popoverController = GetObjectByUniqueTag(Convert.ToInt32(result["popoverController"])) as MHPopoverController;
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHPopoverController)
				(obj as MHPopoverController)._popoverControllerShouldDismissPopover(popoverController);
		}
	}
	
	void popoverControllerDidDismissPopover(string jsonString)
	{
		int tag;
		MHPopoverController popoverController;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			popoverController = GetObjectByUniqueTag(Convert.ToInt32(result["popoverController"])) as MHPopoverController;
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHPopoverController)
				(obj as MHPopoverController)._popoverControllerDidDismissPopover(popoverController);
		}
	}
	#endregion
}