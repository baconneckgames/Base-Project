using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public partial class MHiOSManager : Singleton<MHiOSManager> {
	#region activityindicator
	[DllImport("__Internal")]
    private static extern int _init_activityindicator(int tag);
	public MHActivityIndicatorView init_activityindicator(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return GetObjectByUniqueTag(_init_activityindicator(tag)) as MHActivityIndicatorView;
		}
		else
			return new MHActivityIndicatorView();
	}

	[DllImport("__Internal")]
    private static extern int _initWithFrame_activityindicator(int tag, string aRect);
	public MHActivityIndicatorView initWithFrame_activityindicator(int tag, Rect aRect)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string rectJsonString = MHTools.ConvertRectToJSON(aRect);
			
			return GetObjectByUniqueTag(_initWithFrame_activityindicator(tag, rectJsonString)) as MHActivityIndicatorView;
		}
		else
			return new MHActivityIndicatorView();
	}
	
	[DllImport("__Internal")]
    private static extern int _initWithActivityIndicatorStyle_activityindicator(int tag, int style);
	public MHActivityIndicatorView initWithActivityIndicatorStyle_activityindicator(int tag, MHActivityIndicatorStyle style)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return GetObjectByUniqueTag(_initWithActivityIndicatorStyle_activityindicator(tag, (int)style)) as MHActivityIndicatorView;
		}
		else
			return new MHActivityIndicatorView();
	}
	
	[DllImport("__Internal")]
    private static extern bool _hidesWhenStopped(int tag);
	public bool hidesWhenStopped(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _hidesWhenStopped(tag);
		}
		else
			return false;
	}
	
	[DllImport("__Internal")]
    private static extern void _hidesWhenStopped_set(int tag, bool hides);
	public void hidesWhenStopped(int tag, bool hides)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_hidesWhenStopped_set(tag, hides);
		}
	}
	
	[DllImport("__Internal")]
    private static extern int _activityIndicatorViewStyle(int tag);
	public MHActivityIndicatorStyle activityIndicatorViewStyle(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return (MHActivityIndicatorStyle)_activityIndicatorViewStyle(tag);
		}
		else
			return MHActivityIndicatorStyle.MHActivityIndicatorStyleWhiteLarge;
	}
	
	[DllImport("__Internal")]
    private static extern void _activityIndicatorViewStyle_set(int tag, int style);
	public void activityIndicatorViewStyle(int tag, MHActivityIndicatorStyle style)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_activityIndicatorViewStyle_set(tag, (int)style);
		}
	}
	
	[DllImport("__Internal")]
    private static extern string _color(int tag);
	public Color color(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string colorJsonString = _color(tag);
			
			return MHTools.ConvertUIColorToColor(colorJsonString);
		}
		else
			return Color.black;
	}
	
	[DllImport("__Internal")]
    private static extern void _color_set(int tag, string col);
	public void color(int tag, Color col)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string colorJsonString = MHTools.ConvertColorToUIColor(col);
			
			_color_set(tag, colorJsonString);
		}
	}
	#endregion
}
