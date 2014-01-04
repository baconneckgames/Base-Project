using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public partial class MHiOSManager : Singleton<MHiOSManager> {
	#region alertview_xcode_output
	[DllImport("__Internal")]
    private static extern int _init_alertview(int tag);
	public MHAlertView init_alertview(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return GetObjectByUniqueTag(_init_alertview(tag)) as MHAlertView;
		}
		else
			return new MHAlertView();
	}
	
	[DllImport("__Internal")]
    private static extern int _initWithFrame_alertview(int tag, string aRect);
	public MHAlertView initWithFrame_alertview(int tag, Rect aRect)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string rectJsonString = MHTools.ConvertRectToJSON(aRect);
			
			return GetObjectByUniqueTag(_initWithFrame_alertview(tag, rectJsonString)) as MHAlertView;
		}
		else
			return new MHAlertView();
	}
	
	[DllImport("__Internal")]
    private static extern int _initWithTitle_alertview(int tag, string title, string message, string cancelButtonTitle, string otherButtonTitles);
	public MHAlertView initWithTitle_alertview(int tag, string title, string message, string cancelButtonTitle, params string[] otherButtonTitles)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int viewTag = _initWithTitle_alertview(tag, title, message, cancelButtonTitle, new ArrayList(otherButtonTitles).toJson());
			
			return GetObjectByUniqueTag(viewTag) as MHAlertView;
		}
		else
			return new MHAlertView();
	}
	
	[DllImport("__Internal")]
    private static extern int _alertViewStyle(int tag);
	public MHAlertViewStyle alertViewStyle(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return (MHAlertViewStyle)_alertViewStyle(tag);
		}
		else
			return MHAlertViewStyle.MHAlertViewStyleDefault;
	}
	
	[DllImport("__Internal")]
    private static extern void _alertViewStyle_set(int tag, int style);
	public void alertViewStyle(int tag, MHAlertViewStyle style)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_alertViewStyle_set(tag, (int)style);
		}
	}
	
	[DllImport("__Internal")]
    private static extern string _message(int tag);
	public string message(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _message(tag);
		}
		else
			return "";
	}
	
	[DllImport("__Internal")]
    private static extern void _message_set(int tag, string msg);
	public void message(int tag, string msg)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_message_set(tag, msg);
		}
	}
	
	[DllImport("__Internal")]
    private static extern int _addButtonWithTitle_alertview(int tag, string title);
	public int addButtonWithTitle_alertview(int tag, string title)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _addButtonWithTitle_alertview(tag, title);
		}
		else
			return 0;
	}
	
	[DllImport("__Internal")]
    private static extern string _buttonTitleAtIndex_alertview(int tag, int index);
	public string buttonTitleAtIndex_alertview(int tag, int index)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _buttonTitleAtIndex_alertview(tag, index);
		}
		else
			return "";
	}
	
	[DllImport("__Internal")]
    private static extern int _textFieldAtIndex(int tag, int textFieldIndex);
	public MHTextField textFieldAtIndex(int tag, int textFieldIndex)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int fieldTag = _textFieldAtIndex(tag, textFieldIndex);
			
			return GetObjectByUniqueTag(fieldTag) as MHTextField;
		}
		else
			return new MHTextField();
	}
	
	[DllImport("__Internal")]
    private static extern void _show(int tag);
	public void show(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_show(tag);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _dismissWithClickedButtonIndex_alertview(int tag, int buttonIndex, bool animated);
	public void dismissWithClickedButtonIndex_alertview(int tag, int buttonIndex, bool animated)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_dismissWithClickedButtonIndex_alertview(tag, buttonIndex, animated);
		}
	}
	#endregion
	
	#region alertview_xcode_input
	void alertViewClickedButtonAtIndex(string jsonString)
	{
		int tag;
		MHAlertView alertView;
		int buttonIndex;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			alertView = GetObjectByUniqueTag(Convert.ToInt32(result["alertView"])) as MHAlertView;
			buttonIndex = Convert.ToInt32(result["buttonIndex"]);
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHAlertView)
				(obj as MHAlertView)._alertViewClickedButtonAtIndex(alertView, buttonIndex);
		}
	}
	
	void alertViewShouldEnableFirstOtherButton(string jsonString)
	{
		int tag;
		MHAlertView alertView;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			alertView = GetObjectByUniqueTag(Convert.ToInt32(result["alertView"])) as MHAlertView;
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHAlertView)
				(obj as MHAlertView)._alertViewShouldEnableFirstOtherButton(alertView);
		}
	}
	
	void willPresentAlertView(string jsonString)
	{
		int tag;
		MHAlertView alertView;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			alertView = GetObjectByUniqueTag(Convert.ToInt32(result["alertView"])) as MHAlertView;
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHAlertView)
				(obj as MHAlertView)._willPresentAlertView(alertView);
		}
	}
	
	void didPresentAlertView(string jsonString)
	{
		int tag;
		MHAlertView alertView;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			alertView = GetObjectByUniqueTag(Convert.ToInt32(result["alertView"])) as MHAlertView;
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHAlertView)
				(obj as MHAlertView)._didPresentAlertView(alertView);
		}
	}
	
	void alertViewWillDismissWithButtonIndex(string jsonString)
	{
		int tag;
		MHAlertView alertView;
		int buttonIndex;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			alertView = GetObjectByUniqueTag(Convert.ToInt32(result["alertView"])) as MHAlertView;
			buttonIndex = Convert.ToInt32(result["buttonIndex"]);
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHAlertView)
				(obj as MHAlertView)._alertViewWillDismissWithButtonIndex(alertView, buttonIndex);
		}
	}
	
	void alertViewDidDismissWithButtonIndex(string jsonString)
	{
		int tag;
		MHAlertView alertView;
		int buttonIndex;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			alertView = GetObjectByUniqueTag(Convert.ToInt32(result["alertView"])) as MHAlertView;
			buttonIndex = Convert.ToInt32(result["buttonIndex"]);
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHAlertView)
				(obj as MHAlertView)._alertViewDidDismissWithButtonIndex(alertView, buttonIndex);
		}
	}
	
	void alertViewCancel(string jsonString)
	{
		int tag;
		MHAlertView alertView;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			alertView = GetObjectByUniqueTag(Convert.ToInt32(result["alertView"])) as MHAlertView;
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHAlertView)
				(obj as MHAlertView)._alertViewCancel(alertView);
		}
	}
	#endregion
}
