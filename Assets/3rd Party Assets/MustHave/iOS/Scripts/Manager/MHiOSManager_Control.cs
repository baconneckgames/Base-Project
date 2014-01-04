using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public partial class MHiOSManager : Singleton<MHiOSManager> {
	#region control_base
	[DllImport("__Internal")]
    private static extern int _init_control(int tag);
	public MHControl init_control(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return GetObjectByUniqueTag(_init_control(tag)) as MHControl;
		}
		else
			return new MHControl();
	}
	
	[DllImport("__Internal")]
    private static extern int _initWithFrame_control(int tag, string aRect);
	public MHControl initWithFrame_control(int tag, Rect aRect)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string rectJsonString = MHTools.ConvertRectToJSON(aRect);
			
			return GetObjectByUniqueTag(_initWithFrame_control(tag, rectJsonString)) as MHControl;
		}
		else
			return new MHControl();
	}
	
	[DllImport("__Internal")]
    private static extern void _sendActionsForControlEvents(int tag, int controlEvents);
	public void sendActionsForControlEvents(int tag, MHControlEvents controlEvents)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_sendActionsForControlEvents(tag, (int)controlEvents);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _addActionForControlEvents(int tag, int action, int controlEvents);
	public void addActionForControlEvents(int tag, Action action, MHControlEvents controlEvents)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int actionTag = SaveActionToUniqueID(action);
			
			_addActionForControlEvents(tag, actionTag, (int)controlEvents);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _removeActionForControlEvents(int tag, int action, int controlEvents);
	public void removeActionForControlEvents(int tag, Action action, MHControlEvents controlEvents)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int actionTag = GetIDForAction(action);
			
			_removeActionForControlEvents(tag, actionTag, (int)controlEvents);
		}
	}
	
	[DllImport("__Internal")]
    private static extern string _actionsForControlEvents(int tag, int controlEvents);
	public Action[] actionsForControlEvents(int tag, MHControlEvents controlEvents)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {	
			int[] actions = MHTools.ConvertJSONToIntArray(_actionsForControlEvents(tag, (int)controlEvents));
			
			return GetActionsForUniqueID(actions) as Action[];
		}
		else
			return null;
	}
	
	[DllImport("__Internal")]
    private static extern int _allControlEvents(int tag);
	public MHControlEvents allControlEvents(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return (MHControlEvents)_allControlEvents(tag);
		}
		else
			return MHControlEvents.MHControlEventAllTouchEvents;
	}
	
	[DllImport("__Internal")]
    private static extern int _state(int tag);
	public MHControlState state(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return (MHControlState)_state(tag);
		}
		else
			return MHControlState.MHControlStateNormal;
	}
	
	[DllImport("__Internal")]
    private static extern bool _selected(int tag);
	public bool selected(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _selected(tag);
		}
		else
			return false;
	}
	
	[DllImport("__Internal")]
    private static extern void _selected_set(int tag, bool sel);
	public void selected(int tag, bool sel)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_selected_set(tag, sel);
		}
	}
	
	[DllImport("__Internal")]
    private static extern int _contentVerticalAlignment(int tag);
	public MHControlContentVerticalAlignment contentVerticalAlignment(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return (MHControlContentVerticalAlignment)_contentVerticalAlignment(tag);
		}
		else
			return MHControlContentVerticalAlignment.MHControlContentVerticalAlignmentCenter;
	}
	
	[DllImport("__Internal")]
    private static extern void _contentVerticalAlignment_set(int tag, int alignment);
	public void contentVerticalAlignment(int tag, MHControlContentVerticalAlignment alignment)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_contentVerticalAlignment_set(tag, (int)alignment);
		}
	}
	
	[DllImport("__Internal")]
    private static extern int _contentHorizontalAlignment(int tag);
	public MHControlContentHorizontalAlignment contentHorizontalAlignment(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return (MHControlContentHorizontalAlignment)_contentHorizontalAlignment(tag);
		}
		else
			return MHControlContentHorizontalAlignment.MHControlContentHorizontalAlignmentCenter;
	}
	
	[DllImport("__Internal")]
    private static extern void _contentHorizontalAlignment_set(int tag, int alignment);
	public void contentHorizontalAlignment(int tag, MHControlContentHorizontalAlignment alignment)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_contentHorizontalAlignment_set(tag, (int)alignment);
		}
	}
	#endregion
}
