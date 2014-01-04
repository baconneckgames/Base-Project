using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public partial class MHiOSManager : Singleton<MHiOSManager> {
	#region viewController_xcode_output_expansion
	[DllImport("__Internal")]
    private static extern int _navigationController(int tag);
	public MHNavigationController navigationController(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int navTag = _navigationController(tag);
		
			return GetObjectByUniqueTag(navTag) as MHNavigationController;
		}
		else
			return new MHNavigationController();
	}
	
	[DllImport("__Internal")]
    private static extern int _navigationItem(int tag);
	public MHNavigationItem navigationItem(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int navTag = _navigationItem(tag);
			
			return GetObjectByUniqueTag(navTag) as MHNavigationItem;
		}
		else
			return new MHNavigationItem();
	}
	
	[DllImport("__Internal")]
    private static extern string _contentSizeForViewInPopover(int tag);
	public Vector2 contentSizeForViewInPopover(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string vectorJsonString = _contentSizeForViewInPopover(tag);
			
			return MHTools.ConvertJSONToVector2(vectorJsonString);
		}
		else
			return Vector2.zero;
	}
	
	[DllImport("__Internal")]
    private static extern void _contentSizeForViewInPopover_set(int tag, string size);
	public void contentSizeForViewInPopover(int tag, Vector2 size)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string vectorJsonString = MHTools.ConvertVector2ToJSON(size);
			
			_contentSizeForViewInPopover_set(tag, vectorJsonString);
		}
	}
	
	[DllImport("__Internal")]
    private static extern bool _modalInPopover(int tag);
	public bool modalInPopover(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _modalInPopover(tag);
		}
		else
			return false;
	}
	
	[DllImport("__Internal")]
    private static extern void _modalInPopover_set(int tag, bool isModal);
	public void modalInPopover(int tag, bool isModal)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_modalInPopover_set(tag, isModal);
		}
	}
	#endregion
}
