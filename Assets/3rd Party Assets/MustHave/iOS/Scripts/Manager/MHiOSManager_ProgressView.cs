using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public partial class MHiOSManager : Singleton<MHiOSManager> {
	#region progressview
	[DllImport("__Internal")]
    private static extern int _init_progressview(int tag);
	public MHProgressView init_progressview(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return GetObjectByUniqueTag(_init_progressview(tag)) as MHProgressView;
		}
		else
			return new MHProgressView();
	}

	[DllImport("__Internal")]
    private static extern int _initWithFrame_progressview(int tag, string aRect);
	public MHProgressView initWithFrame_progressview(int tag, Rect aRect)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string rectJsonString = MHTools.ConvertRectToJSON(aRect);
			
			return GetObjectByUniqueTag(_initWithFrame_progressview(tag, rectJsonString)) as MHProgressView;
		}
		else
			return new MHProgressView();
	}
	
	[DllImport("__Internal")]
    private static extern int _initWithProgressViewStyle_progressview(int tag, int style);
	public MHProgressView initWithProgressViewStyle_progressview(int tag, MHProgressViewStyle style)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return GetObjectByUniqueTag(_initWithProgressViewStyle_progressview(tag, (int)style)) as MHProgressView;
		}
		else
			return new MHProgressView();
	}
	
	[DllImport("__Internal")]
    private static extern float _progress(int tag);
	public float progress(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _progress(tag);
		}
		else
			return 0f;
	}
	
	[DllImport("__Internal")]
    private static extern void _progress_set(int tag, float prog);
	public void progress(int tag, float prog)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_progress_set(tag, prog);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _setProgress(int tag, float progress, bool animated);
	public void setProgress(int tag, float progress, bool animated)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_setProgress(tag, progress, animated);
		}
	}
	
	[DllImport("__Internal")]
    private static extern int _progressViewStyle(int tag);
	public MHProgressViewStyle progressViewStyle(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return (MHProgressViewStyle)_progressViewStyle(tag);
		}
		else
			return MHProgressViewStyle.MHProgressViewStyleDefault;
	}
	
	[DllImport("__Internal")]
    private static extern void _progressViewStyle_set(int tag, int style);
	public void progressViewStyle(int tag, MHProgressViewStyle style)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_progressViewStyle_set(tag, (int)style);
		}
	}
	
	[DllImport("__Internal")]
    private static extern string _progressTintColor(int tag);
	public Color progressTintColor(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string colorJsonString = _progressTintColor(tag);
			
			return MHTools.ConvertUIColorToColor(colorJsonString);
		}
		else
			return Color.black;
	}
	
	[DllImport("__Internal")]
    private static extern void _progressTintColor_set(int tag, string col);
	public void progressTintColor(int tag, Color col)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string colorJsonString = MHTools.ConvertColorToUIColor(col);
			
			_progressTintColor_set(tag, colorJsonString);
		}
	}
	
	[DllImport("__Internal")]
    private static extern string _progressImage(int tag);
	public Texture2D progressImage(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string textureJsonString = _progressImage(tag);
			
			return MHTools.ConvertUIImageToTexture(textureJsonString);
		}
		else 
			return null;
	}
	
	[DllImport("__Internal")]
    private static extern void _progressImage_set(int tag, string image);
	public void progressImage(int tag, Texture2D image)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string textureJsonString = MHTools.ConvertTextureToUIImage(image);
			
			_progressImage_set(tag, textureJsonString);
		}
	}
	
	[DllImport("__Internal")]
    private static extern string _trackTintColor(int tag);
	public Color trackTintColor(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string colorJsonString = _trackTintColor(tag);
			
			return MHTools.ConvertUIColorToColor(colorJsonString);
		}
		else
			return Color.black;
	}
	
	[DllImport("__Internal")]
    private static extern void _trackTintColor_set(int tag, string col);
	public void trackTintColor(int tag, Color col)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string colorJsonString = MHTools.ConvertColorToUIColor(col);
			
			_trackTintColor_set(tag, colorJsonString);
		}
	}
	
	[DllImport("__Internal")]
    private static extern string _trackImage(int tag);
	public Texture2D trackImage(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string textureJsonString = _trackImage(tag);
			
			return MHTools.ConvertUIImageToTexture(textureJsonString);
		}
		else 
			return null;
	}
	
	[DllImport("__Internal")]
    private static extern void _trackImage_set(int tag, string image);
	public void trackImage(int tag, Texture2D image)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string textureJsonString = MHTools.ConvertTextureToUIImage(image);
			
			_trackImage_set(tag, textureJsonString);
		}
	}
	#endregion
}
