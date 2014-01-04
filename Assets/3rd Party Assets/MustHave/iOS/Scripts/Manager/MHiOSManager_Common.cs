using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public partial class MHiOSManager : Singleton<MHiOSManager> {
	#region navigationitem_viewcontroller_alertview_
	[DllImport("__Internal")]
    private static extern string _title(int tag);
	public string title(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _title(tag);
		}
		else
			return "";
	}
	
	[DllImport("__Internal")]
    private static extern void _title_set(int tag, string newTitle);
	public void title(int tag, string newTitle)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_title_set(tag, newTitle);
		}
	}
	#endregion
	
	#region barbuttonitem_toolbar_navbar_button_switch_
	[DllImport("__Internal")]
    private static extern string _tintColor(int tag);
	public Color tintColor(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string colorJsonString = _tintColor(tag);
			
			return MHTools.ConvertUIColorToColor(colorJsonString);
		}
		else 
			return Color.black;
	}
	
	[DllImport("__Internal")]
    private static extern void _tintColor_set(int tag, string color);
	public void tintColor(int tag, Color color)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string colorJsonString = MHTools.ConvertColorToUIColor(color);
			
			_tintColor_set(tag, colorJsonString);
		}
	}
	#endregion
	
	#region navbar_toolbar_
	[DllImport("__Internal")]
    private static extern int _barStyle(int tag);
	public MHBarStyle barStyle(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return (MHBarStyle)_barStyle(tag);
		}
		else
			return MHBarStyle.MHBarStyleDefault;
	}
	
	[DllImport("__Internal")]
    private static extern void _barStyle_set(int tag, int style);
	public void barStyle(int tag, MHBarStyle style)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_barStyle_set(tag, (int)style);
		}
	}
	
	
	[DllImport("__Internal")]
    private static extern bool _translucent(int tag);
	public bool translucent(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _translucent(tag);
		}
		else
			return false;
	}
	
	[DllImport("__Internal")]
    private static extern void _translucent_set(int tag, bool trans);
	public void translucent(int tag, bool trans)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_translucent_set(tag, trans);
		}
	}
	#endregion
	
	#region control_imageview_label_
	[DllImport("__Internal")]
    private static extern bool _highlighted(int tag);
	public bool highlighted(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _highlighted(tag);
		}
		else
			return false;
	}
	
	[DllImport("__Internal")]
    private static extern void _highlighted_set(int tag, bool highlight);
	public void highlighted(int tag, bool highlight)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_highlighted_set(tag, highlight);
		}
	}
	#endregion
	
	#region control_label_
	[DllImport("__Internal")]
    private static extern bool _enabled(int tag);
	new public bool enabled(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _enabled(tag);
		}
		else 
			return false;
	}
	
	[DllImport("__Internal")]
    private static extern void _enabled_set(int tag, bool enable);
	new public void enabled(int tag, bool enable)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_enabled_set(tag, enable);
		}
	}
	#endregion
	
	#region label_textfield_textview_
	[DllImport("__Internal")]
    private static extern string _text(int tag);
	public string text(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _text(tag);
		}
		else
			return "";
	}
	
	[DllImport("__Internal")]
    private static extern void _text_set(int tag, string txt);
	public void text(int tag, string txt)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_text_set(tag, txt);
		}
	}
	
	[DllImport("__Internal")]
    private static extern string _textColor(int tag);
	public Color textColor(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string colorJsonString = _textColor(tag);
			
			return MHTools.ConvertUIColorToColor(colorJsonString);
		}
		else
			return Color.black;
	}
	
	[DllImport("__Internal")]
    private static extern void _textColor_set(int tag, string color);
	public void textColor(int tag, Color color)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string colorJsonString = MHTools.ConvertColorToUIColor(color);
			
			_textColor_set(tag, colorJsonString);
		}
	}
	
	[DllImport("__Internal")]
    private static extern int _textAlignment(int tag);
	public MHTextAlignment textAlignment(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return (MHTextAlignment)_textAlignment(tag);
		}
		else
			return MHTextAlignment.MHTextAlignmentLeft;
	}
	
	[DllImport("__Internal")]
    private static extern void _textAlignment_set(int tag, int alignment);
	public void textAlignment(int tag, MHTextAlignment alignment)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_textAlignment_set(tag, (int)alignment);
		}
	}
	#endregion
	
	#region label_textfield_
	[DllImport("__Internal")]
    private static extern bool _adjustsFontSizeToFitWidth(int tag);
	public bool adjustsFontSizeToFitWidth(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _adjustsFontSizeToFitWidth(tag);
		}
		else
			return false;
	}
	
	[DllImport("__Internal")]
    private static extern void _adjustsFontSizeToFitWidth_set(int tag, bool adjusts);
	public void adjustsFontSizeToFitWidth(int tag, bool adjusts)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_adjustsFontSizeToFitWidth_set(tag, adjusts);
		}
	}
	#endregion
	
	#region viewcontroller_textfield_
	[DllImport("__Internal")]
    private static extern bool _editing(int tag);
	public bool editing(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _editing(tag);
		}
		else
			return false;
	}
	#endregion
	
	#region textfield_textview_
	[DllImport("__Internal")]
    private static extern int _inputView(int tag);
	public MHView inputView(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int viewTag = _inputView(tag);
			
			return GetObjectByUniqueTag(viewTag) as MHView;
		}
		else
			return new MHView();
	}
	
	[DllImport("__Internal")]
    private static extern void _inputView_set(int tag, int view);
	public void inputView(int tag, MHView view)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int viewTag = view.tag;
			
			_inputView_set(tag, viewTag);
		}
	}
   
	[DllImport("__Internal")]
    private static extern int _inputAccessoryView(int tag);
	public MHView inputAccessoryView(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int viewTag = _inputAccessoryView(tag);
			
			return GetObjectByUniqueTag(viewTag) as MHView;
		}
		else
			return new MHView();
	}
	
	[DllImport("__Internal")]
    private static extern void _inputAccessoryView_set(int tag, int view);
	public void inputAccessoryView(int tag, MHView view)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int viewTag = view.tag;
			
			_inputAccessoryView_set(tag, viewTag);
		}
	}
	#endregion
	
	#region alertview_actionsheet_
	[DllImport("__Internal")]
    private static extern bool _visible(int tag);
	public bool visible(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _visible(tag);
		}
		else
			return false;
	}
	
	[DllImport("__Internal")]
    private static extern int _numberOfButtons(int tag);
	public int numberOfButtons(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _numberOfButtons(tag);
		}
		else
			return 0;
	}
	
	[DllImport("__Internal")]
    private static extern int _cancelButtonIndex(int tag);
	public int cancelButtonIndex(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _cancelButtonIndex(tag);
		}
		else
			return 0;
	}
	
	[DllImport("__Internal")]
    private static extern void _cancelButtonIndex_set(int tag, int index);
	public void cancelButtonIndex(int tag, int index)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_cancelButtonIndex_set(tag, index);
		}
	}
	
	[DllImport("__Internal")]
    private static extern int _firstOtherButtonIndex(int tag);
	public int firstOtherButtonIndex(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _firstOtherButtonIndex(tag);
		}
		else
			return 0;
	}
	#endregion
	
	#region switch_slider_
	[DllImport("__Internal")]
    private static extern string _thumbTintColor(int tag);
	public Color thumbTintColor(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string colorJsonString = _thumbTintColor(tag);
			
			return MHTools.ConvertUIColorToColor(colorJsonString);
		}
		else
			return Color.black;
	}
	
	[DllImport("__Internal")]
    private static extern void _thumbTintColor_set(int tag, string color);
	public void thumbTintColor(int tag, Color color)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string colorJsonString = MHTools.ConvertColorToUIColor(color);
			
			_thumbTintColor_set(tag, colorJsonString);
		}
	}
	#endregion
	
	#region imageview_activityindicator_
	[DllImport("__Internal")]
    private static extern void _startAnimating(int tag);
	public void startAnimating(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_startAnimating(tag);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _stopAnimating(int tag);
	public void stopAnimating(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_stopAnimating(tag);
		}
	}
	
	[DllImport("__Internal")]
    private static extern bool _isAnimating(int tag);
	public bool isAnimating(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _isAnimating(tag);
		}
		else
			return false;
	}
	#endregion
}
