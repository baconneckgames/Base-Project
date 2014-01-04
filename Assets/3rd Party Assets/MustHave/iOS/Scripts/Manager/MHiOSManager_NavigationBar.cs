using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public partial class MHiOSManager : Singleton<MHiOSManager> {
	#region navbar_xcode_output
	[DllImport("__Internal")]
    private static extern int _init_navigationbar(int tag);
	public MHNavigationBar init_navigationbar(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return GetObjectByUniqueTag(_init_navigationbar(tag)) as MHNavigationBar;
		}
		else
			return new MHNavigationBar();
	}
	
	[DllImport("__Internal")]
    private static extern int _initWithFrame_navigationbar(int tag, string aRect);
	public MHNavigationBar initWithFrame_navigationbar(int tag, Rect aRect)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string rectJsonString = MHTools.ConvertRectToJSON(aRect);
			
			return GetObjectByUniqueTag(_initWithFrame_navigationbar(tag, rectJsonString)) as MHNavigationBar;
		}
		else
			return new MHNavigationBar();
	}
	
	[DllImport("__Internal")]
	private static extern string _shadowImage(int tag);
	public Texture2D shadowImage(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string textureJsonString = _shadowImage(tag);
			
			return MHTools.ConvertUIImageToTexture(textureJsonString);
		}
		else
			return null;
	}
	
	[DllImport("__Internal")]
	private static extern void _shadowImage_set(int tag, string image);
	public void shadowImage(int tag, Texture2D image)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string textureJsonString = MHTools.ConvertTextureToUIImage(image);
			
			_shadowImage_set(tag, textureJsonString);
		}
	}
	
	[DllImport("__Internal")]
	private static extern void _pushNavigationItem(int tag, int item, bool animated);
	public void pushNavigationItem(int tag, MHNavigationItem item, bool animated)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int itemTag = item.tag;
			
			_pushNavigationItem(tag, itemTag, animated);
		}
	}
	
	[DllImport("__Internal")]
	private static extern int _popNavigationItem(int tag, bool animated);
	public MHNavigationItem popNavigationItem(int tag, bool animated)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int itemTag = _popNavigationItem(tag, animated);
			
			return GetObjectByUniqueTag(itemTag) as MHNavigationItem;
		}
		else 
			return new MHNavigationItem();
	}
	
	[DllImport("__Internal")]
	private static extern void _setItems_navbar(int tag, int[] items, bool animated);
	public void setItems(int tag, MHNavigationItem[] items, bool animated)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int[] itemTags = MHTools.GetObjectTags(items);
			
			_setItems_navbar(tag, itemTags, animated);
		}
	}
	
	[DllImport("__Internal")]
	private static extern string _items_navbar(int tag);
	public MHNavigationItem[] items_navbar(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int[] itemTags = MHTools.ConvertJSONToIntArray(_items_navbar(tag));
			
			return GetObjectsByUniqueTags(itemTags) as MHNavigationItem[];
		}
		else
			return new MHNavigationItem[0];
	}
	
	[DllImport("__Internal")]
	private static extern void _items_navbar_set(int tag, int[] items);
	public void items_navbar(int tag, MHNavigationItem[] items)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int[] itemTags = MHTools.GetObjectTags(items);
			
			_items_navbar_set(tag, itemTags);
		}
	}
	
	[DllImport("__Internal")]
	private static extern int _topItem(int tag);
	public MHNavigationItem topItem(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int itemTag = _topItem(tag);
			
			return GetObjectByUniqueTag(itemTag) as MHNavigationItem;
		}
		else
			return new MHNavigationItem();
	}
	
	[DllImport("__Internal")]
	private static extern int _backItem(int tag);
	public MHNavigationItem backItem(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int itemTag = _backItem(tag);
			
			return GetObjectByUniqueTag(itemTag) as MHNavigationItem;
		}
		else
			return new MHNavigationItem();
	}
	
	[DllImport("__Internal")]
	private static extern string _backgroundImageForBarMetrics(int tag, int barMetrics);
	public Texture2D backgroundImageForBarMetrics(int tag, MHBarMetrics barMetrics)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string textureJsonString = _backgroundImageForBarMetrics(tag, (int)barMetrics);
			
			return MHTools.ConvertUIImageToTexture(textureJsonString);
		}
		else
			return null;
	}
	
	[DllImport("__Internal")]
	private static extern void _setBackgroundImage_metrics(int tag, string backgroundImage, int barMetrics);
	public void setBackgroundImage_metrics(int tag, Texture2D backgroundImage, MHBarMetrics barMetrics)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string textureJsonString = MHTools.ConvertTextureToUIImage(backgroundImage);
			
			_setBackgroundImage_metrics(tag, textureJsonString, (int)barMetrics);
		}
	}
	
	[DllImport("__Internal")]
	private static extern float _titleVerticalPositionAdjustmentForBarMetrics(int tag, int barMetrics);
	public float titleVerticalPositionAdjustmentForBarMetrics(int tag, MHBarMetrics barMetrics)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _titleVerticalPositionAdjustmentForBarMetrics(tag, (int)barMetrics);
		}
		else
			return 0f;
	}
	
	[DllImport("__Internal")]
	private static extern void _setTitleVerticalPositionAdjustment(int tag, float adjustment, int barMetrics);
	public void setTitleVerticalPositionAdjustment(int tag, float adjustment, MHBarMetrics barMetrics)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_setTitleVerticalPositionAdjustment(tag, adjustment, (int)barMetrics);
		}
	}
	
	[DllImport("__Internal")]
	private static extern string _titleTextAttributes(int tag);
	public Hashtable titleTextAttributes(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string attributesJsonString = _titleTextAttributes(tag);
			
			return attributesJsonString.hashtableFromJson();
		}
		else
			return null;
	}
	
	[DllImport("__Internal")]
	private static extern void _titleTextAttributes_set(int tag, string attributes);
	public void titleTextAttributes(int tag, Hashtable attributes)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string attributesJsonString = attributes.toJson();
			
			_titleTextAttributes_set(tag, attributesJsonString);
		}
	}
	#endregion
	
	#region navbar_xcode_input
	void shouldPushItem(string jsonString)
	{
		int tag;
		MHNavigationBar navigationBar;
		MHNavigationItem item;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			navigationBar = GetObjectByUniqueTag(Convert.ToInt32(result["navigationBar"])) as MHNavigationBar;
			item = GetObjectByUniqueTag(Convert.ToInt32(result["item"])) as MHNavigationItem;
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHNavigationBar)
				(obj as MHNavigationBar)._shouldPushItem(navigationBar, item);
		}
	}
	
	void didPushItem(string jsonString)
	{
		int tag;
		MHNavigationBar navigationBar;
		MHNavigationItem item;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			navigationBar = GetObjectByUniqueTag(Convert.ToInt32(result["navigationBar"])) as MHNavigationBar;
			item = GetObjectByUniqueTag(Convert.ToInt32(result["item"])) as MHNavigationItem;
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHNavigationBar)
				(obj as MHNavigationBar)._didPushItem(navigationBar, item);
		}
	}
	
	void shouldPopItem(string jsonString)
	{
		int tag;
		MHNavigationBar navigationBar;
		MHNavigationItem item;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			navigationBar = GetObjectByUniqueTag(Convert.ToInt32(result["navigationBar"])) as MHNavigationBar;
			item = GetObjectByUniqueTag(Convert.ToInt32(result["item"])) as MHNavigationItem;
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHNavigationBar)
				(obj as MHNavigationBar)._shouldPopItem(navigationBar, item);
		}
	}
	
	void didPopItem(string jsonString)
	{
		int tag;
		MHNavigationBar navigationBar;
		MHNavigationItem item;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			navigationBar = GetObjectByUniqueTag(Convert.ToInt32(result["navigationBar"])) as MHNavigationBar;
			item = GetObjectByUniqueTag(Convert.ToInt32(result["item"])) as MHNavigationItem;
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHNavigationBar)
				(obj as MHNavigationBar)._didPopItem(navigationBar, item);
		}
	}
	#endregion
}