using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public partial class MHiOSManager : Singleton<MHiOSManager> {
	#region toolbar
	[DllImport("__Internal")]
    private static extern int _init_toolbar(int tag);
	public MHToolbar init_toolbar(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return GetObjectByUniqueTag(_init_toolbar(tag)) as MHToolbar;
		}
		else
			return new MHToolbar();
	}
	
	[DllImport("__Internal")]
    private static extern int _initWithFrame_toolbar(int tag, string aRect);
	public MHToolbar initWithFrame_toolbar(int tag, Rect aRect)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string rectJsonString = MHTools.ConvertRectToJSON(aRect);
			
			return GetObjectByUniqueTag(_initWithFrame_toolbar(tag, rectJsonString)) as MHToolbar;
		}
		else
			return new MHToolbar();
	}
	
	[DllImport("__Internal")]
    private static extern string _items_toolbar(int tag);
	public MHBarButtonItem[] items_toolbar(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int[] itemTags = MHTools.ConvertJSONToIntArray(_items_toolbar(tag));
			
			return GetObjectsByUniqueTags(itemTags) as MHBarButtonItem[];
		}
		else
			return new MHBarButtonItem[0];
	}
	
	[DllImport("__Internal")]
    private static extern void _items_toolbar_set(int tag, int[] items);
	public void items_toolbar(int tag, MHBarButtonItem[] items)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int[] itemTags = MHTools.GetObjectTags(items);
			
			_items_toolbar_set(tag, itemTags);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _setItems_toolbar(int tag, int[] items, bool animated);
	public void setItems(int tag, MHBarButtonItem[] items, bool animated)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int[] itemTags = MHTools.GetObjectTags(items);
			
			_setItems_toolbar(tag, itemTags, animated);
		}
	}
	
	[DllImport("__Internal")]
    private static extern string _backgroundImageForToolbarPosition(int tag, int topOrBottom, int barMetrics);
	public Texture2D backgroundImageForToolbarPosition(int tag, MHToolbarPosition topOrBottom, MHBarMetrics barMetrics)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string textureJsonString = _backgroundImageForToolbarPosition(tag, (int)topOrBottom, (int)barMetrics);
			
			return MHTools.ConvertUIImageToTexture(textureJsonString);
		}
		else
			return null;
	}
	
	[DllImport("__Internal")]
    private static extern void _setBackgroundImage_pos(int tag, string backgroundImage, int topOrBottom, int barMetrics);
	public void setBackgroundImage_pos(int tag, Texture2D backgroundImage, MHToolbarPosition topOrBottom, MHBarMetrics barMetrics)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string textureJsonString = MHTools.ConvertTextureToUIImage(backgroundImage);
			
			_setBackgroundImage_pos(tag, textureJsonString, (int)topOrBottom, (int)barMetrics);
		}
	}
	
	[DllImport("__Internal")]
    private static extern string _shadowImageForToolbarPosition(int tag, int topOrBottom);
	public Texture2D shadowImageForToolbarPosition(int tag, MHToolbarPosition topOrBottom)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string textureJsonString = _shadowImageForToolbarPosition(tag, (int)topOrBottom);
			
			return MHTools.ConvertUIImageToTexture(textureJsonString);
		}
		else
			return null;
	}
	
	[DllImport("__Internal")]
    private static extern void _setShadowImage(int tag, string shadowImage, int topOrBottom);
	public void setShadowImage(int tag, Texture2D shadowImage, MHToolbarPosition topOrBottom)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string textureJsonString = MHTools.ConvertTextureToUIImage(shadowImage);
			
			_setShadowImage(tag, textureJsonString, (int)topOrBottom);
		}
	}
	#endregion
}