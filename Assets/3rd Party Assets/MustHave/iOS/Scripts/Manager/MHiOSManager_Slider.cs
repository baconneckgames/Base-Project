using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public partial class MHiOSManager : Singleton<MHiOSManager> {
	#region slider
	[DllImport("__Internal")]
    private static extern int _init_slider(int tag);
	public MHSlider init_slider(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return GetObjectByUniqueTag(_init_slider(tag)) as MHSlider;
		}
		else
			return new MHSlider();
	}
	
	[DllImport("__Internal")]
    private static extern int _initWithFrame_slider(int tag, string aRect);
	public MHSlider initWithFrame_slider(int tag, Rect aRect)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string rectJsonString = MHTools.ConvertRectToJSON(aRect);
			
			return GetObjectByUniqueTag(_initWithFrame_slider(tag, rectJsonString)) as MHSlider;
		}
		else
			return new MHSlider();
	}
	
	[DllImport("__Internal")]
    private static extern float _val(int tag);
	public float val(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _val(tag);
		}
		else
			return 0f;
	}
	
	[DllImport("__Internal")]
    private static extern void _val_set(int tag, float val);
	public void val(int tag, float val)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_val_set(tag, val);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _setVal(int tag, float val, bool animated);
	public void setVal(int tag, float val, bool animated)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_setVal(tag, val, animated);
		}
	}
	
	[DllImport("__Internal")]
    private static extern float _minimumValue(int tag);
	public float minimumValue(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _minimumValue(tag);
		}
		else
			return 0f;
	}
	
	[DllImport("__Internal")]
    private static extern void _minimumValue_set(int tag, float val);
	public void minimumValue(int tag, float val)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_minimumValue_set(tag, val);
		}
	}
	
	[DllImport("__Internal")]
    private static extern float _maximumValue(int tag);
	public float maximumValue(int tag) 
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _maximumValue(tag);
		}
		else
			return 0f;
	}
	
	[DllImport("__Internal")]
    private static extern void _maximumValue_set(int tag, float val);
	public void maximumValue(int tag, float val)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_maximumValue_set(tag, val);
		}
	}
	
	[DllImport("__Internal")]
    private static extern bool _continuous(int tag);
	public bool continuous(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _continuous(tag);
		}
		else
			return false;
	}
	
	[DllImport("__Internal")]
    private static extern void _continuous_set(int tag, bool cont);
	public void continuous(int tag, bool cont)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_continuous_set(tag, cont);
		}
	}
	
	[DllImport("__Internal")]
    private static extern string _minimumValueImage(int tag);
	public Texture2D minimumValueImage(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string textureJsonString = _minimumValueImage(tag);
			
			return MHTools.ConvertUIImageToTexture(textureJsonString);
		}
		else
			return null;
	}
	
	[DllImport("__Internal")]
    private static extern void _minimumValueImage_set(int tag, string image);
	public void minimumValueImage(int tag, Texture2D img)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string textureJsonString = MHTools.ConvertTextureToUIImage(img);
			
			_minimumValueImage_set(tag, textureJsonString);
		}
	}
	
	[DllImport("__Internal")]
    private static extern string _maximumValueImage(int tag);
	public Texture2D maximumValueImage(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string textureJsonString = _maximumValueImage(tag);
			
			return MHTools.ConvertUIImageToTexture(textureJsonString);
		}
		else
			return null;
	}
	
	[DllImport("__Internal")]
    private static extern void _maximumValueImage_set(int tag, string image);
	public void maximumValueImage(int tag, Texture2D img)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string textureJsonString = MHTools.ConvertTextureToUIImage(img);
			
			_maximumValueImage_set(tag, textureJsonString);
		}
	}
	
	[DllImport("__Internal")]
    private static extern string _minimumTrackTintColor(int tag);
	public Color minimumTrackTintColor(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string colorJsonString = _minimumTrackTintColor(tag);
			
			return MHTools.ConvertUIColorToColor(colorJsonString);
		}
		else
			return Color.black;
	}
	
	[DllImport("__Internal")]
    private static extern void _minimumTrackTintColor_set(int tag, string col);
	public void minimumTrackTintColor(int tag, Color col)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string colorJsonString = MHTools.ConvertColorToUIColor(col);
			
			_minimumTrackTintColor_set(tag, colorJsonString);
		}
	}
	
	[DllImport("__Internal")]
    private static extern string _currentMinimumTrackImage(int tag);
	public Texture2D currentMinimumTrackImage(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string textureJsonString = _currentMinimumTrackImage(tag);
			
			return MHTools.ConvertUIImageToTexture(textureJsonString);
		}
		else
			return null;
	}
	
	[DllImport("__Internal")]
    private static extern string _minimumTrackImageForState(int tag, int state);
	public Texture2D minimumTrackImageForState(int tag, MHControlState state)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string textureJsonString = _minimumTrackImageForState(tag, (int)state);
			
			return MHTools.ConvertUIImageToTexture(textureJsonString);
		}
		else
			return null;
	}
	
	[DllImport("__Internal")]
    private static extern void _setMinimumTrackImageForState(int tag, string image, int state);
	public void setMinimumTrackImageForState(int tag, Texture2D img, MHControlState state)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string textureJsonString = MHTools.ConvertTextureToUIImage(img);
			
			_setMinimumTrackImageForState(tag, textureJsonString, (int)state);
		}
	}
	
	[DllImport("__Internal")]
    private static extern string _maximumTrackTintColor(int tag);
	public Color maximumTrackTintColor(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string colorJsonString = _maximumTrackTintColor(tag);
			
			return MHTools.ConvertUIColorToColor(colorJsonString);
		}
		else
			return Color.black;
	}
	
	[DllImport("__Internal")]
    private static extern void _maximumTrackTintColor_set(int tag, string col);
	public void maximumTrackTintColor(int tag, Color col)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string colorJsonString = MHTools.ConvertColorToUIColor(col);
			
			_maximumTrackTintColor_set(tag, colorJsonString);
		}
	}
	
	[DllImport("__Internal")]
    private static extern string _currentMaximumTrackImage(int tag);
	public Texture2D currentMaximumTrackImage(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string textureJsonString = _currentMaximumTrackImage(tag);
			
			return MHTools.ConvertUIImageToTexture(textureJsonString);
		}
		else
			return null;
	}
	
	[DllImport("__Internal")]
    private static extern string _maximumTrackImageForState(int tag, int state);
	public Texture2D maximumTrackImageForState(int tag, MHControlState state)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string textureJsonString = _maximumTrackImageForState(tag, (int)state);
			
			return MHTools.ConvertUIImageToTexture(textureJsonString);
		}
		else
			return null;
	}
	
	[DllImport("__Internal")]
    private static extern void _setMaximumTrackImageForState(int tag, string image, int state);
	public void setMaximumTrackImageForState(int tag, Texture2D img, MHControlState state)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string textureJsonString = MHTools.ConvertTextureToUIImage(img);
			
			_setMaximumTrackImageForState(tag, textureJsonString, (int)state);
		}
	}
	
	[DllImport("__Internal")]
    private static extern string _currentThumbImage(int tag);
	public Texture2D currentThumbImage(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string textureJsonString = _currentThumbImage(tag);
			
			return MHTools.ConvertUIImageToTexture(textureJsonString);
		}
		else
			return null;
	}
	
	[DllImport("__Internal")]
    private static extern string _thumbImageForState(int tag, int state);
	public Texture2D thumbImageForState(int tag, MHControlState state)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string textureJsonString = _thumbImageForState(tag, (int)state);
			
			return MHTools.ConvertUIImageToTexture(textureJsonString);
		}
		else
			return null;
	}
	
	[DllImport("__Internal")]
    private static extern void _setThumbImageForState(int tag, string image, int state);
	public void setThumbImageForState(int tag, Texture2D img, MHControlState state)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string textureJsonString = MHTools.ConvertTextureToUIImage(img);
			
			_setThumbImageForState(tag, textureJsonString, (int)state);
		}
	}
	#endregion
}
