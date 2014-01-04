using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public partial class MHiOSManager : Singleton<MHiOSManager> {
	#region button
	[DllImport("__Internal")]
    private static extern int _init_button(int tag);
	public MHButton init_button(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return GetObjectByUniqueTag(_init_button(tag)) as MHButton;
		}
		else
			return new MHButton();
	}
	
	[DllImport("__Internal")]
    private static extern int _initWithFrame_button(int tag, string aRect);
	public MHButton initWithFrame_button(int tag, Rect aRect)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string rectJsonString = MHTools.ConvertRectToJSON(aRect);
			
			return GetObjectByUniqueTag(_initWithFrame_button(tag, rectJsonString)) as MHButton;
		}
		else
			return new MHButton();
	}
	
	[DllImport("__Internal")]
    private static extern int _buttonWithType(int tag, int type);
	public MHButton buttonWithType(int tag, MHButtonType type)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int buttonTag = _buttonWithType(tag, (int)type);
			
			return GetObjectByUniqueTag(buttonTag) as MHButton;
		}
		else
			return new MHButton();
	}
	
	[DllImport("__Internal")]
    private static extern int _titleLabel(int tag);
	public MHLabel titleLabel(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int labelTag = _titleLabel(tag);
			
			return GetObjectByUniqueTag(labelTag) as MHLabel;
		}
		else 
			return new MHLabel();
	}
	
	[DllImport("__Internal")]
    private static extern bool _reversesTitleShadowWhenHighlighted(int tag);
	public bool reversesTitleShadowWhenHighlighted(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _reversesTitleShadowWhenHighlighted(tag);
		}
		else
			return false;
	}
	
	[DllImport("__Internal")]
    private static extern void _reversesTitleShadowWhenHighlighted_set(int tag, bool reverse);
	public void reversesTitleShadowWhenHighlighted(int tag, bool reverse)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_reversesTitleShadowWhenHighlighted_set(tag, reverse);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _setTitle(int tag, string title, int state);
	public void setTitle(int tag, string title, MHControlState state)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_setTitle(tag, title, (int)state);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _setTitleColor(int tag, string color, int state);
	public void setTitleColor(int tag, Color color, MHControlState state)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string colorJsonString = MHTools.ConvertColorToUIColor(color);
			
			_setTitleColor(tag, colorJsonString, (int)state);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _setTitleShadowColor(int tag, string color, int state);
	public void setTitleShadowColor(int tag, Color color, MHControlState state)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string colorJsonString = MHTools.ConvertColorToUIColor(color);
			
			_setTitleShadowColor(tag, colorJsonString, (int)state);
		}
	}
	
	[DllImport("__Internal")]
    private static extern string _titleColorForState(int tag, int state);
	public Color titleColorForState(int tag, MHControlState state)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string colorJsonString = _titleColorForState(tag, (int)state);
			
			return MHTools.ConvertUIColorToColor(colorJsonString);
		}
		else
			return Color.black;
	}
	
	[DllImport("__Internal")]
    private static extern string _titleForState(int tag, int state);
	public string titleForState(int tag, MHControlState state)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _titleForState(tag, (int)state);
		}
		else
			return "";
	}
	
	[DllImport("__Internal")]
    private static extern string _titleShadowColorForState(int tag, int state);
	public Color titleShadowColorForState(int tag, MHControlState state)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string colorJsonString = _titleShadowColorForState(tag, (int)state);
			
			return MHTools.ConvertUIColorToColor(colorJsonString);
		}
		else
			return Color.black;
	}
	
	[DllImport("__Internal")]
    private static extern bool _adjustsImageWhenHighlighted(int tag);
	public bool adjustsImageWhenHighlighted(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _adjustsImageWhenHighlighted(tag);
		}
		else
			return false;
	}
	
	[DllImport("__Internal")]
    private static extern void _adjustsImageWhenHighlighted_set(int tag, bool adjust);
	public void adjustsImageWhenHighlighted(int tag, bool adjust)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_adjustsImageWhenHighlighted_set(tag, adjust);
		}
	}
	
	[DllImport("__Internal")]
    private static extern bool _adjustsImageWhenDisabled(int tag);
	public bool adjustsImageWhenDisabled(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _adjustsImageWhenDisabled(tag);
		}
		else
			return false;
	}
	
	[DllImport("__Internal")]
    private static extern void _adjustsImageWhenDisabled_set(int tag, bool adjust);
	public void adjustsImageWhenDisabled(int tag, bool adjust)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_adjustsImageWhenDisabled_set(tag, adjust);
		}
	}
	
	[DllImport("__Internal")]
    private static extern bool _showsTouchWhenHighlighted(int tag);
	public bool showsTouchWhenHighlighted(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _showsTouchWhenHighlighted(tag);
		}
		else
			return false;
	}
	
	[DllImport("__Internal")]
    private static extern void _showsTouchWhenHighlighted_set(int tag, bool show);
	public void showsTouchWhenHighlighted(int tag, bool show)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_showsTouchWhenHighlighted_set(tag, show);
		}
	}
	
	[DllImport("__Internal")]
    private static extern string _backgroundImageForState_button(int tag, int state);
	public Texture2D backgroundImageForState_button(int tag, MHControlState state)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string textureJsonString = _backgroundImageForState_button(tag, (int)state);
			
			return MHTools.ConvertUIImageToTexture(textureJsonString);
		}
		else
			return null;
	}
	
	[DllImport("__Internal")]
    private static extern string _imageForState(int tag, int state);
	public Texture2D imageForState(int tag, MHControlState state)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string textureJsonString = _imageForState(tag, (int)state);
			
			return MHTools.ConvertUIImageToTexture(textureJsonString);
		}
		else
			return null;
	}
	
	[DllImport("__Internal")]
    private static extern void _setBackgroundImage_button(int tag, string image, int state);
	public void setBackgroundImage_button(int tag, Texture2D image, MHControlState state)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string textureJsonString = MHTools.ConvertTextureToUIImage(image);
			
			_setBackgroundImage_button(tag, textureJsonString, (int)state);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _setImage(int tag, string image, int state);
	public void setImage(int tag, Texture2D image, MHControlState state)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string textureJsonString = MHTools.ConvertTextureToUIImage(image);
			
			_setImage(tag, textureJsonString, (int)state);
		}
	}
	
	[DllImport("__Internal")]
    private static extern string _contentEdgeInsets(int tag);
	public Vector4 contentEdgeInsets(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string vectorJsonString = _contentEdgeInsets(tag);
			
			return MHTools.ConvertJSONToVector4(vectorJsonString);
		}
		else
			return Vector4.zero;
	}
	
	[DllImport("__Internal")]
    private static extern void _contentEdgeInsets_set(int tag, string edgeInsets);
	public void contentEdgeInsets(int tag, Vector4 edgeInsets)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string vectorJsonString = MHTools.ConvertVector4ToJSON(edgeInsets);
			
			_contentEdgeInsets_set(tag, vectorJsonString);
		}
	}
	
	[DllImport("__Internal")]
    private static extern string _titleEdgeInsets(int tag);
	public Vector4 titleEdgeInsets(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string vectorJsonString = _titleEdgeInsets(tag);
			
			return MHTools.ConvertJSONToVector4(vectorJsonString);
		}
		else
			return Vector4.zero;
	}
	
	[DllImport("__Internal")]
    private static extern void _titleEdgeInsets_set(int tag, string edgeInsets);
	public void titleEdgeInsets(int tag, Vector4 edgeInsets)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string vectorJsonString = MHTools.ConvertVector4ToJSON(edgeInsets);
			
			_titleEdgeInsets_set(tag, vectorJsonString);
		}
	}
	
	[DllImport("__Internal")]
    private static extern string _imageEdgeInsets(int tag);
	public Vector4 imageEdgeInsets(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string vectorJsonString = _imageEdgeInsets(tag);
			
			return MHTools.ConvertJSONToVector4(vectorJsonString);
		}
		else
			return Vector4.zero;
	}
	
	[DllImport("__Internal")]
    private static extern void _imageEdgeInsets_set(int tag, string edgeInsets);
	public void imageEdgeInsets(int tag, Vector4 edgeInsets)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string vectorJsonString = MHTools.ConvertVector4ToJSON(edgeInsets);
			
			_imageEdgeInsets_set(tag, vectorJsonString);
		}
	}
	
	[DllImport("__Internal")]
    private static extern int _buttonType(int tag);
	public MHButtonType buttonType(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return (MHButtonType)_buttonType(tag);
		}
		else
			return MHButtonType.MHButtonTypeCustom;
	}
	
	[DllImport("__Internal")]
    private static extern string _currentTitle(int tag);
	public string currentTitle(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _currentTitle(tag);
		}
		else
			return "";
	}
	
	[DllImport("__Internal")]
    private static extern string _currentTitleColor(int tag);
	public Color currentTitleColor(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string colorJsonString = _currentTitleColor(tag);
			
			return MHTools.ConvertUIColorToColor(colorJsonString);
		}
		else
			return Color.black;
	}
	
	[DllImport("__Internal")]
    private static extern string _currentTitleShadowColor(int tag);
	public Color currentTitleShadowColor(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string colorJsonString = _currentTitleShadowColor(tag);
			
			return MHTools.ConvertUIColorToColor(colorJsonString);
		}
		else
			return Color.black;
	}
	
	[DllImport("__Internal")]
    private static extern string _currentImage(int tag);
	public Texture2D currentImage(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string textureJsonString = _currentImage(tag);
			
			return MHTools.ConvertUIImageToTexture(textureJsonString);
		}
		else
			return null;
	}
	
	[DllImport("__Internal")]
    private static extern string _currentBackgroundImage(int tag);
	public Texture2D currentBackgroundImage(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string textureJsonString = _currentBackgroundImage(tag);
			
			return MHTools.ConvertUIImageToTexture(textureJsonString);
		}
		else
			return null;
	}
	
	[DllImport("__Internal")]
    private static extern int _imageView(int tag);
	public MHImageView imageView(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int viewTag = _imageView(tag);
			
			return GetObjectByUniqueTag(viewTag) as MHImageView;
		}
		else
			return new MHImageView();
	}
	
	[DllImport("__Internal")]
    private static extern string _backgroundRectForBounds(int tag, string bounds);
	public Rect backgroundRectForBounds(int tag, Rect bounds)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string boundsJsonString = MHTools.ConvertRectToJSON(bounds);
			
			string rectJsonString = _backgroundRectForBounds(tag, boundsJsonString);
			
			return MHTools.ConvertJSONToRect(rectJsonString);
		}
		else
			return MHTools.RectZero;
	}
	
	[DllImport("__Internal")]
    private static extern string _contentRectForBounds(int tag, string bounds);
	public Rect contentRectForBounds(int tag, Rect bounds)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string boundsJsonString = MHTools.ConvertRectToJSON(bounds);
			
			string rectJsonString = _contentRectForBounds(tag, boundsJsonString);
			
			return MHTools.ConvertJSONToRect(rectJsonString);
		}
		else
			return MHTools.RectZero;
	}
	
	[DllImport("__Internal")]
    private static extern string _titleRectForContentRect(int tag, string contentRect);
	public Rect titleRectForContentRect(int tag, Rect contentRect)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string contentJsonString = MHTools.ConvertRectToJSON(contentRect);
			
			string rectJsonString = _titleRectForContentRect(tag, contentJsonString);
			
			return MHTools.ConvertJSONToRect(rectJsonString);
		}
		else
			return MHTools.RectZero;
	}
	
	[DllImport("__Internal")]
    private static extern string _imageRectForContentRect(int tag, string contentRect);
	public Rect imageRectForContentRect(int tag, Rect contentRect)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string contentJsonString = MHTools.ConvertRectToJSON(contentRect);
			
			string rectJsonString = _imageRectForContentRect(tag, contentJsonString);
			
			return MHTools.ConvertJSONToRect(rectJsonString);
		}
		else
			return MHTools.RectZero;
	}
	#endregion
}