using UnityEngine;
using System;
using System.Collections;
/* WARNING: Just a base class */
public class MHControl : MHView {
	#region functions
	public MHControl(){}
	
	public MHControl(bool autoInitialize)
	{
		if(Application.isPlaying && autoInitialize)
			init();
	}
	
	public MHControl(Rect aRect)
	{
		if(Application.isPlaying)
			initWithFrame(aRect);
	}
	
	new public MHControl init()
	{
		return MHiOSManager.Instance.init_control(tag);
	}
	
	new public MHControl initWithFrame(Rect aRect)
	{
		return MHiOSManager.Instance.initWithFrame_control(tag, aRect);
	}
	
	public void sendActionsForControlEvents(MHControlEvents controlEvents)
	{
		MHiOSManager.Instance.sendActionsForControlEvents(tag, controlEvents);
	}
	
	public void addActionForControlEvents(Action action, MHControlEvents controlEvents)
	{
		MHiOSManager.Instance.addActionForControlEvents(tag, action, controlEvents);
	}
	
	public void removeActionForControlEvents(Action action, MHControlEvents controlEvents)
	{
		MHiOSManager.Instance.removeActionForControlEvents(tag, action, controlEvents);
	}
	
	public Action[] actionsForControlEvents(MHControlEvents controlEvents)
	{
		return MHiOSManager.Instance.actionsForControlEvents(tag, controlEvents);
	}
	
	public MHControlEvents allControlEvents()
	{
		return MHiOSManager.Instance.allControlEvents(tag);
	}
	
	public MHControlState state {
		get {
			return MHiOSManager.Instance.state(tag);
		}
	}
	
	public bool enabled {
		get {
			return MHiOSManager.Instance.enabled(tag);
		} set {
			MHiOSManager.Instance.enabled(tag, value);
		}
	}
	
	public bool selected {
		get {
			return MHiOSManager.Instance.selected(tag);
		} set {
			MHiOSManager.Instance.selected(tag, value);
		}
	}
	
	public bool highlighted {
		get {
			return MHiOSManager.Instance.highlighted(tag);
		} set {
			MHiOSManager.Instance.highlighted(tag);
		}
	}
	
	public MHControlContentVerticalAlignment contentVerticalAlignment {
		get {
			return MHiOSManager.Instance.contentVerticalAlignment(tag);
		} set {
			MHiOSManager.Instance.contentVerticalAlignment(tag, value);
		}
	}
	
	public MHControlContentHorizontalAlignment contentHorizontalAlignment {
		get {
			return MHiOSManager.Instance.contentHorizontalAlignment(tag);
		} set {
			MHiOSManager.Instance.contentHorizontalAlignment(tag, value);
		}
	}
	#endregion
}
