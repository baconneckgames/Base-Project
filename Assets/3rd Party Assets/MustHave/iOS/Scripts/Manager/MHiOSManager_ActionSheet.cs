using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public partial class MHiOSManager : Singleton<MHiOSManager> {
	#region actionsheet_xcode_output
	[DllImport("__Internal")]
    private static extern int _init_actionsheet(int tag);
	public MHActionSheet init_actionsheet(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return GetObjectByUniqueTag(_init_actionsheet(tag)) as MHActionSheet;
		}
		else
			return new MHActionSheet();
	}
	
	[DllImport("__Internal")]
    private static extern int _initWithFrame_actionsheet(int tag, string aRect);
	public MHActionSheet initWithFrame_actionsheet(int tag, Rect aRect)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string rectJsonString = MHTools.ConvertRectToJSON(aRect);
			
			return GetObjectByUniqueTag(_initWithFrame_actionsheet(tag, rectJsonString)) as MHActionSheet;
		}
		else
			return new MHActionSheet();
	}
	
	[DllImport("__Internal")]
    private static extern int _initWithTitle_actionsheet(int tag, string title, string cancelButtonTitle, string destructiveButtonTitle, string otherButtonTitles);
	public MHActionSheet initWithTitle_actionsheet(int tag, string title, string cancelButtonTitle, string destructiveButtonTitle, params string[] otherButtonTitles)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int sheetTag = _initWithTitle_actionsheet(tag, title, cancelButtonTitle, destructiveButtonTitle, new ArrayList(otherButtonTitles).toJson());
			
			return GetObjectByUniqueTag(sheetTag) as MHActionSheet;
		}
		else
			return new MHActionSheet();
	}
	
	[DllImport("__Internal")]
    private static extern int _actionSheetStyle(int tag);
	public MHActionSheetStyle actionSheetStyle(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return (MHActionSheetStyle)_actionSheetStyle(tag);
		}
		else
			return MHActionSheetStyle.MHActionSheetStyleDefault;
	}
	
	[DllImport("__Internal")]
    private static extern void _actionSheetStyle_set(int tag, int style);
	public void actionSheetStyle(int tag, MHActionSheetStyle style)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_actionSheetStyle_set(tag, (int)style);
		}
	}
	
	[DllImport("__Internal")]
    private static extern int _addButtonWithTitle_actionsheet(int tag, string title);
	public int addButtonWithTitle_actionsheet(int tag, string title)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _addButtonWithTitle_actionsheet(tag, title);
		}
		else
			return 0;
	}
	
	[DllImport("__Internal")]
    private static extern string _buttonTitleAtIndex_actionsheet(int tag, int buttonIndex);
	public string buttonTitleAtIndex_actionsheet(int tag, int buttonIndex)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _buttonTitleAtIndex_actionsheet(tag, buttonIndex);
		}
		else
			return "";
	}
	
	[DllImport("__Internal")]
    private static extern int _destructiveButtonIndex(int tag);
	public int destructiveButtonIndex(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _destructiveButtonIndex(tag);
		}
		else
			return 0;
	}
	
	[DllImport("__Internal")]
    private static extern void _destructiveButtonIndex_set(int tag, int index);
	public void destructiveButtonIndex(int tag, int index)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_destructiveButtonIndex_set(tag, index);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _showFromToolbar(int tag, int view);
	public void showFromToolbar(int tag, MHToolbar view)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int viewTag = view.tag;
			
			_showFromToolbar(tag, viewTag);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _showInView(int tag, int view);
	public void showInView(int tag, MHView view)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int viewTag = view.tag;
			
			_showInView(tag, viewTag);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _showFromBarButtonItem(int tag, int item, bool animated);
	public void showFromBarButtonItem(int tag, MHBarButtonItem item, bool animated)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int itemTag = item.tag;
			
			_showFromBarButtonItem(tag, itemTag, animated);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _showFromRect(int tag, string rect, int view, bool animated);
	public void showFromRect(int tag, Rect rect, MHView view, bool animated)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int viewTag = view.tag;
			
			string rectJsonString = MHTools.ConvertRectToJSON(rect);
			
			_showFromRect(tag, rectJsonString, viewTag, animated);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _dismissWithClickedButtonIndex_actionsheet(int tag, int buttonIndex, bool animated);
	public void dismissWithClickedButtonIndex_actionsheet(int tag, int buttonIndex, bool animated)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			dismissWithClickedButtonIndex_actionsheet(tag, buttonIndex, animated);
		}
	}
	#endregion
	
	#region actionsheet_xcode_input
	void actionSheetClickedButtonAtIndex(string jsonString)
	{
		int tag;
		MHActionSheet actionSheet;
		int buttonIndex;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			actionSheet = GetObjectByUniqueTag(Convert.ToInt32(result["actionSheet"])) as MHActionSheet;
			buttonIndex = Convert.ToInt32(result["buttonIndex"]);
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHActionSheet)
				(obj as MHActionSheet)._actionSheetClickedButtonAtIndex(actionSheet, buttonIndex);
		}
	}
	
	void willPresentActionSheet(string jsonString)
	{
		int tag;
		MHActionSheet actionSheet;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			actionSheet = GetObjectByUniqueTag(Convert.ToInt32(result["actionSheet"])) as MHActionSheet;
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHActionSheet)
				(obj as MHActionSheet)._willPresentActionSheet(actionSheet);
		}
	}

	void didPresentActionSheet(string jsonString)
	{
		int tag;
		MHActionSheet actionSheet;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			actionSheet = GetObjectByUniqueTag(Convert.ToInt32(result["actionSheet"])) as MHActionSheet;
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHActionSheet)
				(obj as MHActionSheet)._didPresentActionSheet(actionSheet);
		}
	}
	
	void actionSheetWillDismissWithButtonIndex(string jsonString)
	{
		int tag;
		MHActionSheet actionSheet;
		int buttonIndex;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			actionSheet = GetObjectByUniqueTag(Convert.ToInt32(result["actionSheet"])) as MHActionSheet;
			buttonIndex = Convert.ToInt32(result["buttonIndex"]);
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHActionSheet)
				(obj as MHActionSheet)._actionSheetWillDismissWithButtonIndex(actionSheet, buttonIndex);
		}
	}
	
	void actionSheetDidDismissWithButtonIndex(string jsonString)
	{
		int tag;
		MHActionSheet actionSheet;
		int buttonIndex;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			actionSheet = GetObjectByUniqueTag(Convert.ToInt32(result["actionSheet"])) as MHActionSheet;
			buttonIndex = Convert.ToInt32(result["buttonIndex"]);
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHActionSheet)
				(obj as MHActionSheet)._actionSheetDidDismissWithButtonIndex(actionSheet, buttonIndex);
		}
	}
	
	void actionSheetCancel(string jsonString)
	{
		int tag;
		MHActionSheet actionSheet;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			actionSheet = GetObjectByUniqueTag(Convert.ToInt32(result["actionSheet"])) as MHActionSheet;
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHActionSheet)
				(obj as MHActionSheet)._actionSheetCancel(actionSheet);
		}
	}
	#endregion
}
