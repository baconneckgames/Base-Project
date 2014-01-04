using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public partial class MHiOSManager : Singleton<MHiOSManager> {
	#region textview_xcode_output
	[DllImport("__Internal")]
    private static extern int _init_textview(int tag);
	public MHTextView init_textview(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return GetObjectByUniqueTag(_init_textview(tag)) as MHTextView;
		}
		else
			return new MHTextView();
	}
	
	[DllImport("__Internal")]
    private static extern int _initWithFrame_textview(int tag, string aRect);
	public MHTextView initWithFrame_textview(int tag, Rect aRect)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string rectJsonString = MHTools.ConvertRectToJSON(aRect);
			
			return GetObjectByUniqueTag(_initWithFrame_textview(tag, rectJsonString)) as MHTextView;
		}
		else
			return new MHTextView();
	}
	
	[DllImport("__Internal")]
    private static extern bool _editable(int tag);
	public bool editable(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
		{
			return _editable(tag);
		}
		else
			return false;
	}
	
	[DllImport("__Internal")]
    private static extern void _editable_set(int tag, bool edit);
	public void editable(int tag, bool edit)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
		{
			_editable_set(tag, edit);
		}
	}
	
	[DllImport("__Internal")]
    private static extern int _dataDetectorTypes(int tag);
	public MHDataDetectorType dataDetectorTypes(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
		{
			return (MHDataDetectorType)_dataDetectorTypes(tag);
		}
		else
			return MHDataDetectorType.MHDataDetectorTypeNone;
	}
	
	[DllImport("__Internal")]
    private static extern void _dataDetectorTypes_set(int tag, int dataDetector);
	public void dataDetectorTypes(int tag, MHDataDetectorType dataDetector)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
		{
			_dataDetectorTypes_set(tag, (int)dataDetector);
		}
	}
	
	[DllImport("__Internal")]
    private static extern bool _hasText(int tag);
	public bool hasText(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
		{
			return _hasText(tag);
		}
		else
			return false;
	}
	#endregion
	
	#region textview_xcode_input
	void textViewShouldBeginEditing(string jsonString)
	{
		int tag;
		MHTextView textView;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			textView = GetObjectByUniqueTag(Convert.ToInt32(result["textView"])) as MHTextView;
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHTextView)
				(obj as MHTextView)._textViewShouldBeginEditing(textView);
		}
	}
	
	void textViewDidBeginEditing(string jsonString)
	{
		int tag;
		MHTextView textView;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			textView = GetObjectByUniqueTag(Convert.ToInt32(result["textView"])) as MHTextView;
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHTextView)
				(obj as MHTextView)._textViewDidBeginEditing(textView);
		}
	}
	
	void textViewShouldEndEditing(string jsonString)
	{
		int tag;
		MHTextView textView;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			textView = GetObjectByUniqueTag(Convert.ToInt32(result["textView"])) as MHTextView;
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHTextView)
				(obj as MHTextView)._textViewShouldEndEditing(textView);
		}
	}
	
	void textViewDidEndEditing(string jsonString)
	{
		int tag;
		MHTextView textView;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			textView = GetObjectByUniqueTag(Convert.ToInt32(result["textView"])) as MHTextView;
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHTextView)
				(obj as MHTextView)._textViewDidEndEditing(textView);
		}
	}
	
	void textViewDidChange(string jsonString)
	{
		int tag;
		MHTextView textView;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			textView = GetObjectByUniqueTag(Convert.ToInt32(result["textView"])) as MHTextView;
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHTextView)
				(obj as MHTextView)._textViewDidChange(textView);
		}
	}
	#endregion
}