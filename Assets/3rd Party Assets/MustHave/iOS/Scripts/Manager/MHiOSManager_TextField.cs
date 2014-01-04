using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public partial class MHiOSManager : Singleton<MHiOSManager> {
	#region textfield_xcode_output
	[DllImport("__Internal")]
    private static extern int _init_textfield(int tag);
	public MHTextField init_textfield(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return GetObjectByUniqueTag(_init_textfield(tag)) as MHTextField;
		}
		else
			return new MHTextField();
	}
	
	[DllImport("__Internal")]
    private static extern int _initWithFrame_textfield(int tag, string aRect);
	public MHTextField initWithFrame_textfield(int tag, Rect aRect)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string rectJsonString = MHTools.ConvertRectToJSON(aRect);
			
			return GetObjectByUniqueTag(_initWithFrame_textfield(tag, rectJsonString)) as MHTextField;
		}
		else
			return new MHTextField();
	}
	
	[DllImport("__Internal")]
    private static extern string _placeholder(int tag);
	public string placeholder(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
		{
			return _placeholder(tag);
		}
		else
			return "";
	}
	
	[DllImport("__Internal")]
    private static extern void _placeholder_set(int tag, string holder);
	public void placeholder(int tag, string holder)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
		{
			_placeholder_set(tag, holder);
		}
	}
	
	[DllImport("__Internal")]
    private static extern float _minimumFontSize(int tag);
	public float minimumFontSize(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
		{
			return _minimumFontSize(tag);
		}
		else
			return 0f;
	}
	
	[DllImport("__Internal")]
    private static extern void _minimumFontSize_set(int tag, float size);
	public void minimumFontSize(int tag, float size)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
		{
			_minimumFontSize_set(tag, size);
		}
	}
	
	[DllImport("__Internal")]
    private static extern bool _clearsOnBeginEditing(int tag);
	public bool clearsOnBeginEditing(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
		{
			return _clearsOnBeginEditing(tag);
		}
		else
			return false;
	}
	
	[DllImport("__Internal")]
    private static extern void _clearsOnBeginEditing_set(int tag, bool clears);
	public void clearsOnBeginEditing(int tag, bool clears)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
		{
			_clearsOnBeginEditing_set(tag, clears);
		}
	}
	
	[DllImport("__Internal")]
    private static extern bool _clearsOnInsertion(int tag);
	public bool clearsOnInsertion(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
		{
			return _clearsOnInsertion(tag);
		}
		else
			return false;
	}
	
	[DllImport("__Internal")]
    private static extern void _clearsOnInsertion_set(int tag, bool clears);
	public void clearsOnInsertion(int tag, bool clears)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
		{
			_clearsOnInsertion_set(tag, clears);
		}
	}
	
	[DllImport("__Internal")]
    private static extern int _borderStyle(int tag);
	public MHTextBorderStyle borderStyle(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
		{
			return (MHTextBorderStyle)_borderStyle(tag);
		}
		else
			return MHTextBorderStyle.MHTextBorderStyleNone;
	}
	
	[DllImport("__Internal")]
    private static extern void _borderStyle_set(int tag, int style);
	public void borderStyle(int tag, MHTextBorderStyle style)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
		{
			_borderStyle_set(tag, (int)style);
		}
	}
	
	[DllImport("__Internal")]
    private static extern string _background(int tag);
	public Texture2D background(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
		{
			string textureJsonString = _background(tag);
			
			return MHTools.ConvertUIImageToTexture(textureJsonString);
		}
		else
			return null;
	}
	
	[DllImport("__Internal")]
    private static extern void _background_set(int tag, string image);
	public void background(int tag, Texture2D image)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
		{
			string textureJsonString = MHTools.ConvertTextureToUIImage(image);
			
			_background_set(tag, textureJsonString);
		}
	}
	
	[DllImport("__Internal")]
    private static extern string _disabledBackground(int tag);
	public Texture2D disabledBackground(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
		{
			string textureJsonString = _disabledBackground(tag);
			
			return MHTools.ConvertUIImageToTexture(textureJsonString);
		}
		else
			return null;
	}
	
	[DllImport("__Internal")]
    private static extern void _disabledBackground_set(int tag, string image);
	public void disabledBackground(int tag, Texture2D image)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
		{
			string textureJsonString = MHTools.ConvertTextureToUIImage(image);
			
			_disabledBackground_set(tag, textureJsonString);
		}
	}
	
	[DllImport("__Internal")]
    private static extern int _clearButtonMode(int tag);
	public MHTextFieldViewMode clearButtonMode(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
		{
			return (MHTextFieldViewMode)_clearButtonMode(tag);
		}
		else
			return MHTextFieldViewMode.MHTextFieldViewModeNever;
	}
	
	[DllImport("__Internal")]
    private static extern void _clearButtonMode_set(int tag, int mode);
	public void clearButtonMode(int tag, MHTextFieldViewMode mode)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
		{
			_clearButtonMode_set(tag, (int)mode);
		}
	}
	
	[DllImport("__Internal")]
    private static extern int _leftView(int tag);
	public MHView leftView(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
		{
			int viewTag = _leftView(tag);
			
			return GetObjectByUniqueTag(viewTag) as MHView;
		}
		else
			return new MHView();
	}
	
	[DllImport("__Internal")]
    private static extern void _leftView_set(int tag, int view);
	public void leftView(int tag, MHView view)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
		{
			int viewTag = view.tag;
			
			_leftView_set(tag, viewTag);
		}
	}
	
	[DllImport("__Internal")]
    private static extern int _leftViewMode(int tag);
	public MHTextFieldViewMode leftViewMode(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
		{
			return (MHTextFieldViewMode)_leftViewMode(tag);
		}
		else
			return MHTextFieldViewMode.MHTextFieldViewModeNever;
	}
	
	[DllImport("__Internal")]
    private static extern void _leftViewMode_set(int tag, int mode);
	public void leftViewMode(int tag, MHTextFieldViewMode mode)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
		{
			_leftViewMode_set(tag, (int)mode);
		}
	}
	
	[DllImport("__Internal")]
    private static extern int _rightView(int tag);
	public MHView rightView(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
		{
			int viewTag = _rightView(tag);
			
			return GetObjectByUniqueTag(viewTag) as MHView;
		}
		else
			return new MHView();
	}
	
	[DllImport("__Internal")]
    private static extern void _rightView_set(int tag, int view);
	public void rightView(int tag, MHView view)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
		{
			int viewTag = view.tag;
			
			_rightView_set(tag, viewTag);
		}
	}
	
	[DllImport("__Internal")]
    private static extern int _rightViewMode(int tag);
	public MHTextFieldViewMode rightViewMode(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
		{
			return (MHTextFieldViewMode)_rightViewMode(tag);
		}
		else
			return MHTextFieldViewMode.MHTextFieldViewModeNever;
	}
	
	[DllImport("__Internal")]
    private static extern void _rightViewMode_set(int tag, int mode);
	public void rightViewMode(int tag, MHTextFieldViewMode mode)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
		{
			_rightViewMode_set(tag, (int)mode);
		}
	}
	
	[DllImport("__Internal")]
    private static extern string _textRectForBounds_textfield(int tag, string bounds);
	public Rect textRectForBounds_textfield(int tag, Rect bounds)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
		{
			string rectJsonString = MHTools.ConvertRectToJSON(bounds);
			
			string rectJsonString2 = _textRectForBounds_textfield(tag, rectJsonString);
			
			return MHTools.ConvertJSONToRect(rectJsonString2);
		}
		else
			return MHTools.RectZero;
	}
	
	[DllImport("__Internal")]
    private static extern void _drawTextInRect_textfield(int tag, string rect);
	public void drawTextInRect_textfield(int tag, Rect rect)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
		{
			string rectJsonString = MHTools.ConvertRectToJSON(rect);
			
			_drawTextInRect_textfield(tag, rectJsonString);
		}
	}
	
	[DllImport("__Internal")]
    private static extern string _placeholderRectForBounds(int tag, string bounds);
	public Rect placeholderRectForBounds(int tag, Rect bounds)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
		{
			string rectJsonString = MHTools.ConvertRectToJSON(bounds);
			
			string rectJsonString2 = _placeholderRectForBounds(tag, rectJsonString);
			
			return MHTools.ConvertJSONToRect(rectJsonString2);
		}
		else
			return MHTools.RectZero;
	}
	
	[DllImport("__Internal")]
    private static extern void _drawPlaceholderInRect(int tag, string rect);
	public void drawPlaceholderInRect(int tag, Rect rect)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
		{
			string rectJsonString = MHTools.ConvertRectToJSON(rect);
			
			_drawPlaceholderInRect(tag, rectJsonString);
		}
	}
	
	[DllImport("__Internal")]
    private static extern string _borderRectForBounds(int tag, string bounds);
	public Rect borderRectForBounds(int tag, Rect bounds)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
		{
			string rectJsonString = MHTools.ConvertRectToJSON(bounds);
			
			string rectJsonString2 = _borderRectForBounds(tag, rectJsonString);
			
			return MHTools.ConvertJSONToRect(rectJsonString2);
		}
		else
			return MHTools.RectZero;
	}
	
	[DllImport("__Internal")]
    private static extern string _editingRectForBounds(int tag, string bounds);
	public Rect editingRectForBounds(int tag, Rect bounds)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
		{
			string rectJsonString = MHTools.ConvertRectToJSON(bounds);
			
			string rectJsonString2 = _editingRectForBounds(tag, rectJsonString);
			
			return MHTools.ConvertJSONToRect(rectJsonString2);
		}
		else
			return MHTools.RectZero;
	}
	
	[DllImport("__Internal")]
    private static extern string _clearButtonRectForBounds(int tag, string bounds);
	public Rect clearButtonRectForBounds(int tag, Rect bounds)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
		{
			string rectJsonString = MHTools.ConvertRectToJSON(bounds);
			
			string rectJsonString2 = _clearButtonRectForBounds(tag, rectJsonString);
			
			return MHTools.ConvertJSONToRect(rectJsonString2);
		}
		else
			return MHTools.RectZero;
	}
	
	[DllImport("__Internal")]
    private static extern string _leftViewRectForBounds(int tag, string bounds);
	public Rect leftViewRectForBounds(int tag, Rect bounds)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
		{
			string rectJsonString = MHTools.ConvertRectToJSON(bounds);
			
			string rectJsonString2 = _leftViewRectForBounds(tag, rectJsonString);
			
			return MHTools.ConvertJSONToRect(rectJsonString2);
		}
		else
			return MHTools.RectZero;
	}
	
	[DllImport("__Internal")]
    private static extern string _rightViewRectForBounds(int tag, string bounds);
	public Rect rightViewRectForBounds(int tag, Rect bounds)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
		{
			string rectJsonString = MHTools.ConvertRectToJSON(bounds);
			
			string rectJsonString2 = _rightViewRectForBounds(tag, rectJsonString);
			
			return MHTools.ConvertJSONToRect(rectJsonString2);
		}
		else
			return MHTools.RectZero;
	}
	#endregion
	
	#region textfield_xcode_input
	void textFieldShouldBeginEditing(string jsonString)
	{
		int tag;
		MHTextField textField;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			textField = GetObjectByUniqueTag(Convert.ToInt32(result["textField"])) as MHTextField;
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHTextField)
				(obj as MHTextField)._textFieldShouldBeginEditing(textField);
		}
	}
	
	void textFieldDidBeginEditing(string jsonString)
	{
		int tag;
		MHTextField textField;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			textField = GetObjectByUniqueTag(Convert.ToInt32(result["textField"])) as MHTextField;
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHTextField)
				(obj as MHTextField)._textFieldDidBeginEditing(textField);
		}
	}
	
	void textFieldShouldEndEditing(string jsonString)
	{
		int tag;
		MHTextField textField;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			textField = GetObjectByUniqueTag(Convert.ToInt32(result["textField"])) as MHTextField;
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHTextField)
				(obj as MHTextField)._textFieldShouldEndEditing(textField);
		}
	}
	
	void textFieldDidEndEditing(string jsonString)
	{
		int tag;
		MHTextField textField;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			textField = GetObjectByUniqueTag(Convert.ToInt32(result["textField"])) as MHTextField;
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHTextField)
				(obj as MHTextField)._textFieldDidEndEditing(textField);
		}
	}
	
	void textFieldShouldClear(string jsonString)
	{
		int tag;
		MHTextField textField;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			textField = GetObjectByUniqueTag(Convert.ToInt32(result["textField"])) as MHTextField;
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHTextField)
				(obj as MHTextField)._textFieldShouldClear(textField);
		}
	}
	
	void textFieldShouldReturn(string jsonString)
	{
		int tag;
		MHTextField textField;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			textField = GetObjectByUniqueTag(Convert.ToInt32(result["textField"])) as MHTextField;
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHTextField)
				(obj as MHTextField)._textFieldShouldReturn(textField);
		}
	}
	#endregion
}