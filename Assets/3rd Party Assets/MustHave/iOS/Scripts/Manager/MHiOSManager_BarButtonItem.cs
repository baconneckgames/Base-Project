using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public partial class MHiOSManager : Singleton<MHiOSManager> {
	#region bar_button_item
	[DllImport("__Internal")]
    private static extern int _init_barbuttonitem(int tag);
	public MHBarButtonItem init_barbuttonitem(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return GetObjectByUniqueTag(_init_barbuttonitem(tag)) as MHBarButtonItem;
		}
		else
			return new MHBarButtonItem();
	}
	
	[DllImport("__Internal")]
    private static extern int _initWithBarButtonSystemItem(int tag, int systemItem, int action);
	public MHBarButtonItem initWithBarButtonSystemItem(int tag, MHBarButtonSystemItem systemItem, Action action)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int actionTag = SaveActionToUniqueID(action);
			int newTag = _initWithBarButtonSystemItem(tag, (int)systemItem, actionTag);
			
			return GetObjectByUniqueTag(newTag) as MHBarButtonItem;
		}
		else 
			return new MHBarButtonItem();
	}
	
	[DllImport("__Internal")]
    private static extern int _initWithCustomView(int tag, int customView);
	public MHBarButtonItem initWithCustomView(int tag, MHView customView)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int viewTag = customView.tag;
			int newTag = _initWithCustomView(tag, viewTag);
			
			return GetObjectByUniqueTag(newTag) as MHBarButtonItem;
		}
		else 
			return new MHBarButtonItem();
	}
	
	[DllImport("__Internal")]
    private static extern int _initWithImage_barbutton(int tag, string image, int style, int action);
	public MHBarButtonItem initWithImage_barbutton(int tag, Texture2D image, MHBarButtonItemStyle style, Action action)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string imageJsonString = MHTools.ConvertTextureToUIImage(image);
			int actionTag = SaveActionToUniqueID(action);
			int newTag = _initWithImage_barbutton(tag, imageJsonString, (int)style, actionTag);
			
			return GetObjectByUniqueTag(newTag) as MHBarButtonItem;
		}
		else 
			return new MHBarButtonItem();
	}
	
	[DllImport("__Internal")]
    private static extern int _initWithTitle_style(int tag, string newTitle, int style, int action);
	public MHBarButtonItem initWithTitle(int tag, string newTitle, MHBarButtonItemStyle style, Action action)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int actionTag = SaveActionToUniqueID(action);
			int newTag = _initWithTitle_style(tag, newTitle, (int)style, actionTag);
			
			return GetObjectByUniqueTag(newTag) as MHBarButtonItem;
		}
		else 
			return new MHBarButtonItem();
	}
	
	[DllImport("__Internal")]
    private static extern int _initWithImage_landscape(int tag, string image, string landscapeImagePhone, int style, int action);
	public MHBarButtonItem initWithImage(int tag, Texture2D image, Texture2D landscapeImagePhone, MHBarButtonItemStyle style, Action action)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string imageJsonString = MHTools.ConvertTextureToUIImage(image);
			string landscapeImageJsonString = MHTools.ConvertTextureToUIImage(landscapeImagePhone);
			int actionTag = SaveActionToUniqueID(action);
			int newTag = _initWithImage_landscape(tag, imageJsonString, landscapeImageJsonString, (int)style, actionTag);
			
			return GetObjectByUniqueTag(newTag) as MHBarButtonItem;
		}
		else 
			return new MHBarButtonItem();
	}
	
	[DllImport("__Internal")]
    private static extern int _action(int tag);
	public Action action(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int actionTag = _action(tag);
			
			return actionsByID[actionTag] as Action;
		}
		else 
			return null;
	}
	
	[DllImport("__Internal")]
    private static extern void _action_set(int tag, int action);
	public void action(int tag, Action action)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int actionTag = SaveActionToUniqueID(action);
			
			_action_set(tag, actionTag);
		}
	}
	
	[DllImport("__Internal")]
    private static extern int _style(int tag);
	public MHBarButtonItemStyle style(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return (MHBarButtonItemStyle)_style(tag);
		}
		else 
			return MHBarButtonItemStyle.MHBarButtonItemStylePlain;
	}
	
	[DllImport("__Internal")]
    private static extern void _style_set(int tag, int style);
	public void style(int tag, MHBarButtonItemStyle style)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_style_set(tag, (int)style);
		}
	}
	
	[DllImport("__Internal")]
    private static extern string _possibleTitles(int tag);
	public string[] possibleTitles(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return MHTools.ConvertJsonToStringArray(_possibleTitles(tag));
		}
		else 
			return null;
	}
	
	[DllImport("__Internal")]
    private static extern void _possibleTitles_set(int tag, string titles);
	public void possibleTitles(int tag, string[] titles)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_possibleTitles_set(tag, new ArrayList(titles).toJson());
		}
	}
	
	[DllImport("__Internal")]
    private static extern float _width(int tag);
	public float width(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _width(tag);
		}
		else 
			return 0f;
	}
	
	[DllImport("__Internal")]
    private static extern void _width_set(int tag, float width);
	public void width(int tag, float width)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_width_set(tag, width);
		}
	}
	
	[DllImport("__Internal")]
    private static extern int _customView(int tag);
	public MHView customView(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int viewTag = _customView(tag);
			
			return GetObjectByUniqueTag(viewTag) as MHView;
		}
		else 
			return new MHView();
	}
	
	[DllImport("__Internal")]
    private static extern void _customView_set(int tag, int view);
	public void customView(int tag, MHView view)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int viewTag = view.tag;
			
			_customView_set(tag, viewTag);
		}
	}
	
	[DllImport("__Internal")]
    private static extern string _backButtonBackgroundImageForState(int tag, int state, int barMetrics);
	public Texture2D backButtonBackgroundImageForState(int tag, MHControlState state, MHBarMetrics barMetrics)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string textureJsonString = _backButtonBackgroundImageForState(tag, (int)state, (int)barMetrics);
			
			return MHTools.ConvertUIImageToTexture(textureJsonString);
		}
		else 
			return null;
	}
	
	[DllImport("__Internal")]
    private static extern void _setBackButtonBackgroundImage(int tag, string backgroundImage, int state, int barMetrics);
	public void setBackButtonBackgroundImage(int tag, Texture2D backgroundImage, MHControlState state, MHBarMetrics barMetrics)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string textureJsonString = MHTools.ConvertTextureToUIImage(backgroundImage);
			
			_setBackButtonBackgroundImage(tag, textureJsonString, (int)state, (int)barMetrics);
		}
	}
	
	[DllImport("__Internal")]
    private static extern string _backButtonTitlePositionAdjustmentForBarMetrics(int tag, int barMetrics);
	public Vector2 backButtonTitlePositionAdjustmentForBarMetrics(int tag, MHBarMetrics barMetrics)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string vectorJsonString = _backButtonTitlePositionAdjustmentForBarMetrics(tag, (int)barMetrics);
			
			return MHTools.ConvertJSONToVector2(vectorJsonString);
		}
		else 
			return Vector2.zero;
	}
	
	[DllImport("__Internal")]
    private static extern void _setBackButtonTitlePositionAdjustment(int tag, string adjustment, int barMetrics);
	public void setBackButtonTitlePositionAdjustment(int tag, Vector2 adjustment, MHBarMetrics barMetrics)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string vectorJsonString = MHTools.ConvertVector2ToJSON(adjustment);
			
			_setBackButtonTitlePositionAdjustment(tag, vectorJsonString, (int)barMetrics);
		}
	}
	
	[DllImport("__Internal")]
    private static extern float _backButtonBackgroundVerticalPositionAdjustmentForBarMetrics(int tag, int barMetrics);
	public float backButtonBackgroundVerticalPositionAdjustmentForBarMetrics(int tag, MHBarMetrics barMetrics)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _backButtonBackgroundVerticalPositionAdjustmentForBarMetrics(tag, (int)barMetrics);
		}
		else 
			return 0f;
	}
	
	[DllImport("__Internal")]
    private static extern void _setBackButtonBackgroundVerticalPositionAdjustment(int tag, float adjustment, int barMetrics);
	public void setBackButtonBackgroundVerticalPositionAdjustment(int tag, float adjustment, MHBarMetrics barMetrics)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_setBackButtonBackgroundVerticalPositionAdjustment(tag, adjustment, (int)barMetrics);
		}
	}
	
	[DllImport("__Internal")]
    private static extern float _backgroundVerticalPositionAdjustmentForBarMetrics(int tag, int barMetrics);
	public float backgroundVerticalPositionAdjustmentForBarMetrics(int tag, MHBarMetrics barMetrics)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _backgroundVerticalPositionAdjustmentForBarMetrics(tag, (int)barMetrics);
		}
		else 
			return 0f;
	}
	
	[DllImport("__Internal")]
    private static extern void _setBackgroundVerticalPositionAdjustment(int tag, float adjustment, int barMetrics);
	public void setBackgroundVerticalPositionAdjustment(int tag, float adjustment, MHBarMetrics barMetrics)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_setBackgroundVerticalPositionAdjustment(tag, adjustment, (int)barMetrics);
		}
	}
	
	[DllImport("__Internal")]
    private static extern string _backgroundImageForState_barbutton(int tag, int state, int barMetrics);
	public Texture2D backgroundImageForState_barbutton(int tag, MHControlState state, MHBarMetrics barMetrics)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string textureJsonString = _backgroundImageForState_barbutton(tag, (int)state, (int)barMetrics);
			
			return MHTools.ConvertUIImageToTexture(textureJsonString);
		}
		else 
			return null;
	}
	
	[DllImport("__Internal")]
    private static extern void _setBackgroundImage_control(int tag, string image, int state, int barMetrics);
	public void setBackgroundImage_control(int tag, Texture2D image, MHControlState state, MHBarMetrics barMetrics)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string textureJsonString = MHTools.ConvertTextureToUIImage(image);
			
			_setBackgroundImage_control(tag, textureJsonString, (int)state, (int)barMetrics);
		}
	}
	
	[DllImport("__Internal")]
    private static extern string _backgroundImageForState_style(int tag, int state, int style, int barMetrics);
	public Texture2D backgroundImageForState(int tag, MHControlState state, MHBarButtonItemStyle style, MHBarMetrics barMetrics)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string textureJsonString = _backgroundImageForState_style(tag, (int)state, (int)style, (int)barMetrics);
			
			return MHTools.ConvertUIImageToTexture(textureJsonString);
		}
		else 
			return null;
	}
	
	[DllImport("__Internal")]
    private static extern void _setBackgroundImage_style(int tag, string image, int state, int style, int barMetrics);
	public void setBackgroundImage_style(int tag, Texture2D image, MHControlState state, MHBarButtonItemStyle style, MHBarMetrics barMetrics)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string textureJsonString = MHTools.ConvertTextureToUIImage(image);
			
			_setBackgroundImage_style(tag, textureJsonString, (int)state, (int)style, (int)barMetrics);
		}
	}
	
	[DllImport("__Internal")]
    private static extern string _titlePositionAdjustmentForBarMetrics(int tag, int barMetrics);
	public Vector2 titlePositionAdjustmentForBarMetrics(int tag, MHBarMetrics barMetrics)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string vectorJsonString = _titlePositionAdjustmentForBarMetrics(tag, (int)barMetrics);
			
			return MHTools.ConvertJSONToVector2(vectorJsonString);
		}
		else 
			return Vector2.zero;
	}
	
	[DllImport("__Internal")]
    private static extern void _setTitlePositionAdjustment(int tag, string adjustment, int barMetrics);
	public void setTitlePositionAdjustment(int tag, Vector2 adjustment, MHBarMetrics barMetrics)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string vectorJsonString = MHTools.ConvertVector2ToJSON(adjustment);
			
			_setTitlePositionAdjustment(tag, vectorJsonString, (int)barMetrics);
		}
	}
	#endregion
}