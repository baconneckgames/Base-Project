using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public partial class MHiOSManager : Singleton<MHiOSManager> {
	#region nav_item
	[DllImport("__Internal")]
    private static extern int _init_navigationitem(int tag);
	public MHNavigationItem init_navigationitem(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return GetObjectByUniqueTag(_init_navigationitem(tag)) as MHNavigationItem;
		}
		else
			return new MHNavigationItem();
	}
	
	[DllImport("__Internal")]
    private static extern int _initWithTitle(int tag, string newTitle);
	public MHNavigationItem initWithTitle(int tag, string newTitle)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int newTag = _initWithTitle(tag, newTitle);
			
			return GetObjectByUniqueTag(newTag) as MHNavigationItem;
		}
		else
			return new MHNavigationItem();
	}
	
	[DllImport("__Internal")]
    private static extern string _prompt(int tag);
	public string prompt(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _prompt(tag);
		}
		else
			return "";
	}
	
	[DllImport("__Internal")]
    private static extern void _prompt_set(int tag, string newPrompt);
	public void prompt(int tag, string newPrompt)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_prompt_set(tag, newPrompt);
		}
	}
	
	[DllImport("__Internal")]
    private static extern int _backBarButtonItem(int tag);
	public MHBarButtonItem backBarButtonItem(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int itemTag = _backBarButtonItem(tag);
			
			return GetObjectByUniqueTag(itemTag) as MHBarButtonItem;
		}
		else
			return new MHBarButtonItem();
	}
	
	[DllImport("__Internal")]
    private static extern void _backBarButtonItem_set(int tag, int item);
	public void backBarButtonItem(int tag, MHBarButtonItem item)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int itemTag = item.tag;
			
			_backBarButtonItem_set(tag, itemTag);
		}
	}
	
	[DllImport("__Internal")]
    private static extern bool _hidesBackButton(int tag);
	public bool hidesBackButton(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _hidesBackButton(tag);
		}
		else
			return false;
	}
	
	[DllImport("__Internal")]
    private static extern void _hidesBackButton_set(int tag, bool hides);
	public void hidesBackButton(int tag, bool hides)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_hidesBackButton_set(tag, hides);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _setHidesBackButton(int tag, bool hidesBackButton, bool animated);
	public void setHidesBackButton(int tag, bool hidesBackButton, bool animated)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_setHidesBackButton(tag, hidesBackButton, animated);
		}
	}
	
	[DllImport("__Internal")]
    private static extern bool _leftItemsSupplementBackButton(int tag);
	public bool leftItemsSupplementBackButton(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _leftItemsSupplementBackButton(tag);
		}
		else
			return false;
	}
	
	[DllImport("__Internal")]
    private static extern void _leftItemsSupplementBackButton_set(int tag, bool button);
	public void leftItemsSupplementBackButton(int tag, bool button)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_leftItemsSupplementBackButton_set(tag, button);
		}
	}
	
	[DllImport("__Internal")]
    private static extern int _titleView(int tag);
	public MHView titleView(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int viewTag = _titleView(tag);
			
			return GetObjectByUniqueTag(viewTag) as MHView;
		}
		else
			return new MHView();
	}
	
	[DllImport("__Internal")]
    private static extern void _titleView_set(int tag, int view);
	public void titleView(int tag, MHView view)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int viewTag = view.tag;
			
			_titleView_set(tag, viewTag);
		}
	}
	
	[DllImport("__Internal")]
    private static extern string _leftBarButtonItems(int tag);
	public MHBarButtonItem[] leftBarButtonItems(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int[] itemTags = MHTools.ConvertJSONToIntArray(_leftBarButtonItems(tag));
			
			return GetObjectsByUniqueTags(itemTags) as MHBarButtonItem[];
		}
		else
			return new MHBarButtonItem[0];
	}
	
	[DllImport("__Internal")]
    private static extern void _leftBarButtonItems_set(int tag, int[] items);
	public void leftBarButtonItems(int tag, MHBarButtonItem[] items)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int[] itemTags = MHTools.GetObjectTags(items);
			
			_leftBarButtonItems_set(tag, itemTags);
		}
	}
	
	[DllImport("__Internal")]
    private static extern int _leftBarButtonItem(int tag);
	public MHBarButtonItem leftBarButtonItem(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int itemTag = _leftBarButtonItem(tag);
			
			return GetObjectByUniqueTag(itemTag) as MHBarButtonItem;
		}
		else
			return new MHBarButtonItem();
	}
	
	[DllImport("__Internal")]
    private static extern void _leftBarButtonItem_set(int tag, int item);
	public void leftBarButtonItem(int tag, MHBarButtonItem item)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int itemTag = item.tag;
			
			_leftBarButtonItem_set(tag, itemTag);
		}
	}
	
	[DllImport("__Internal")]
    private static extern string _rightBarButtonItems(int tag);
	public MHBarButtonItem[] rightBarButtonItems(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int[] itemTags = MHTools.ConvertJSONToIntArray(_rightBarButtonItems(tag));
			
			return GetObjectsByUniqueTags(itemTags) as MHBarButtonItem[];
		}
		else
			return new MHBarButtonItem[0];
	}
	
	[DllImport("__Internal")]
    private static extern void _rightBarButtonItems_set(int tag, int[] items);
	public void rightBarButtonItems(int tag, MHBarButtonItem[] items)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int[] itemTags = MHTools.GetObjectTags(items);
			
			_rightBarButtonItems_set(tag, itemTags);
		}
	}
	
	[DllImport("__Internal")]
    private static extern int _rightBarButtonItem(int tag);
	public MHBarButtonItem rightBarButtonItem(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int itemTag = _rightBarButtonItem(tag);
			
			return GetObjectByUniqueTag(itemTag) as MHBarButtonItem;
		}
		else
			return new MHBarButtonItem();
	}
	
	[DllImport("__Internal")]
    private static extern void _rightBarButtonItem_set(int tag, int item);
	public void rightBarButtonItem(int tag, MHBarButtonItem item)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int itemTag = item.tag;
			
			_rightBarButtonItem_set(tag, itemTag);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _setLeftBarButtonItems(int tag, int[] items, bool animated);
	public void setLeftBarButtonItems(int tag, MHBarButtonItem[] items, bool animated)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int[] itemTags = MHTools.GetObjectTags(items);
			
			_setLeftBarButtonItems(tag, itemTags, animated);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _setLeftBarButtonItem(int tag, int item, bool animated);
	public void setLeftBarButtonItem(int tag, MHBarButtonItem item, bool animated)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int itemTag = item.tag;
			
			_setLeftBarButtonItem(tag, itemTag, animated);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _setRightBarButtonItems(int tag, int[] items, bool animated);
	public void setRightBarButtonItems(int tag, MHBarButtonItem[] items, bool animated)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int[] itemTags = MHTools.GetObjectTags(items);
			
			_setRightBarButtonItems(tag, itemTags, animated);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _setRightBarButtonItem(int tag, int item, bool animated);
	public void setRightBarButtonItem(int tag, MHBarButtonItem item, bool animated)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int itemTag = item.tag;
			
			_setRightBarButtonItem(tag, itemTag, animated);
		}
	}
	#endregion
}