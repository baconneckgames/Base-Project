using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public partial class MHiOSManager : Singleton<MHiOSManager> {
	#region switch
	[DllImport("__Internal")]
    private static extern int _init_switch(int tag);
	public MHSwitch init_switch(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return GetObjectByUniqueTag(_init_switch(tag)) as MHSwitch;
		}
		else
			return new MHSwitch();
	}
	
	[DllImport("__Internal")]
    private static extern int _initWithFrame_switch(int tag, string aRect);
	public MHSwitch initWithFrame_switch(int tag, Rect aRect)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string rectJsonString = MHTools.ConvertRectToJSON(aRect);
			
			return GetObjectByUniqueTag(_initWithFrame_switch(tag, rectJsonString)) as MHSwitch;
		}
		else
			return new MHSwitch();
	}
	
	[DllImport("__Internal")]
    private static extern bool _on(int tag);
	public bool on(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _on(tag);
		}
		else
			return false;
	}
	
	[DllImport("__Internal")]
    private static extern void _on_set(int tag, bool isOn);
	public void on(int tag, bool isOn)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_on_set(tag, isOn);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _setOn(int tag, bool isOn, bool animated);
	public void setOn(int tag, bool isOn, bool animated)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_setOn(tag, isOn, animated);
		}
	}
	
	[DllImport("__Internal")]
    private static extern string _onTintColor(int tag);
	public Color onTintColor(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string colorJsonString = _onTintColor(tag);
			
			return MHTools.ConvertUIColorToColor(colorJsonString);
		}
		else
			return Color.black;
	}
	
	[DllImport("__Internal")]
    private static extern void _onTintColor_set(int tag, string color);
	public void onTintColor(int tag, Color color)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string colorJsonString = MHTools.ConvertColorToUIColor(color);
			
			_onTintColor_set(tag, colorJsonString);
		}
	}
	
	[DllImport("__Internal")]
    private static extern string _onImage(int tag);
	public Texture2D onImage(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string textureJsonString = _onImage(tag);
			
			return MHTools.ConvertUIImageToTexture(textureJsonString);
		}
		else
			return null;
	}
	
	[DllImport("__Internal")]
    private static extern void _onImage_set(int tag, string img);
	public void onImage(int tag, Texture2D img)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string textureJsonString = MHTools.ConvertTextureToUIImage(img);
			
			_onImage_set(tag, textureJsonString);
		}
	}
	
	[DllImport("__Internal")]
    private static extern string _offImage(int tag);
	public Texture2D offImage(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string textureJsonString = _offImage(tag);
			
			return MHTools.ConvertUIImageToTexture(textureJsonString);
		}
		else
			return null;
	}
	
	[DllImport("__Internal")]
    private static extern void _offImage_set(int tag, string img);
	public void offImage(int tag, Texture2D img)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string textureJsonString = MHTools.ConvertTextureToUIImage(img);
			
			_offImage_set(tag, textureJsonString);
		}
	}
	#endregion
}
