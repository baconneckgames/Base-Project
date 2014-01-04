using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public partial class MHiOSManager : Singleton<MHiOSManager> {
	#region imageview
	[DllImport("__Internal")]
    private static extern int _init_imageview(int tag);
	public MHImageView init_imageview(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return GetObjectByUniqueTag(_init_imageview(tag)) as MHImageView;
		}
		else
			return new MHImageView();
	}
	
	[DllImport("__Internal")]
    private static extern int _initWithFrame_imageview(int tag, string aRect);
	public MHImageView initWithFrame_imageview(int tag, Rect aRect)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string rectJsonString = MHTools.ConvertRectToJSON(aRect);
			
			return GetObjectByUniqueTag(_initWithFrame_imageview(tag, rectJsonString)) as MHImageView;
		}
		else
			return new MHImageView();
	}
	
	[DllImport("__Internal")]
    private static extern int _initWithImage_imageview(int tag, string image);
	public MHImageView initWithImage_imageview(int tag, Texture2D image)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string textureJsonString = MHTools.ConvertTextureToUIImage(image);
			
			int viewTag = _initWithImage_imageview(tag, textureJsonString);
			
			return GetObjectByUniqueTag(viewTag) as MHImageView;
		}
		else
			return new MHImageView();
	}
	
	[DllImport("__Internal")]
    private static extern int _initWithImageHighlightedImage(int tag, string image, string highlightedImage);
	public MHImageView initWithImageHighlightedImage(int tag, Texture2D image, Texture2D highlightedImage)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string textureJsonString = MHTools.ConvertTextureToUIImage(image);
			string textureJsonStringHighlighted = MHTools.ConvertTextureToUIImage(image);
			
			int viewTag = _initWithImageHighlightedImage(tag, textureJsonString, textureJsonStringHighlighted);
			
			return GetObjectByUniqueTag(viewTag) as MHImageView;
		}
		else
			return new MHImageView();
	}
	
	[DllImport("__Internal")]
    private static extern string _image(int tag);
	public Texture2D image(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string textureJsonString = _image(tag);
			
			return MHTools.ConvertUIImageToTexture(textureJsonString);
		}
		else
			return null;
	}
	
	[DllImport("__Internal")]
    private static extern void _image_set(int tag, string img);
	public void image(int tag, Texture2D img)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string textureJsonString = MHTools.ConvertTextureToUIImage(img);
			
			_image_set(tag, textureJsonString);
		}
	}
	
	[DllImport("__Internal")]
    private static extern string _highlightedImage(int tag);
	public Texture2D highlightedImage(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string textureJsonString = _highlightedImage(tag);
			
			return MHTools.ConvertUIImageToTexture(textureJsonString);
		}
		else
			return null;
	}
	
	[DllImport("__Internal")]
    private static extern void _highlightedImage_set(int tag, string img);
	public void highlightedImage(int tag, Texture2D img)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string textureJsonString = MHTools.ConvertTextureToUIImage(img);
			
			_highlightedImage_set(tag, textureJsonString);
		}
	}
	
	[DllImport("__Internal")]
    private static extern string _animationImages(int tag);
	public Texture2D[] animationImages(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string[] textureJsonStrings = MHTools.ConvertJsonToStringArray(_animationImages(tag));
			
			Texture2D[] images = new Texture2D[textureJsonStrings.Length];
			
			for(int i = 0; i < textureJsonStrings.Length; i++)
				images[i] = MHTools.ConvertUIImageToTexture(textureJsonStrings[i]);
			
			return images;
		}
		else
			return null;
	}
	
	[DllImport("__Internal")]
    private static extern void _animationImages_set(int tag, string animation);
	public void animationImages(int tag, Texture2D[] animation)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string[] textureJsonStrings = new string[animation.Length];
			
			for(int i = 0; i < animation.Length; i++)
				textureJsonStrings[i] = MHTools.ConvertTextureToUIImage(animation[i]);
			
			_animationImages_set(tag, new ArrayList(textureJsonStrings).toJson());
		}
	}
	
	[DllImport("__Internal")]
    private static extern string _highlightedAnimationImages(int tag);
	public Texture2D[] highlightedAnimationImages(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string[] textureJsonStrings = MHTools.ConvertJsonToStringArray(_highlightedAnimationImages(tag));
			
			Texture2D[] images = new Texture2D[textureJsonStrings.Length];
			
			for(int i = 0; i < textureJsonStrings.Length; i++)
				images[i] = MHTools.ConvertUIImageToTexture(textureJsonStrings[i]);
			
			return images;
		}
		else
			return null;
	}
	
	[DllImport("__Internal")]
    private static extern void _highlightedAnimationImages_set(int tag, string animation);
	public void highlightedAnimationImages(int tag, Texture2D[] animation)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string[] textureJsonStrings = new string[animation.Length];
			
			for(int i = 0; i < animation.Length; i++)
				textureJsonStrings[i] = MHTools.ConvertTextureToUIImage(animation[i]);
			
			_highlightedAnimationImages_set(tag, new ArrayList(textureJsonStrings).toJson());
		}
	}
	
	[DllImport("__Internal")]
    private static extern float _animationDuration(int tag);
	public float animationDuration(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _animationDuration(tag);
		}
		else
			return 0f;
	}
	
	[DllImport("__Internal")]
    private static extern void _animationDuration_set(int tag, float duration);
	public void animationDuration(int tag, float duration)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_animationDuration_set(tag, duration);
		}
	}
	
	[DllImport("__Internal")]
    private static extern int _animationRepeatCount(int tag);
	public int animationRepeatCount(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _animationRepeatCount(tag);
		}
		else
			return 0;
	}
	
	[DllImport("__Internal")]
    private static extern void _animationRepeatCount_set(int tag, int count);
	public void animationRepeatCount(int tag, int count)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_animationRepeatCount_set(tag, count);
		}
	}
	#endregion
}