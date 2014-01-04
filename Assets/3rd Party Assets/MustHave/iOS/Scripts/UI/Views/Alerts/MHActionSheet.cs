using UnityEngine;
using System;
using System.Collections;

public class MHActionSheet : MHView {
	#region functions
	public MHActionSheet(){}
	
	public MHActionSheet(bool autoInitialize)
	{
		if(Application.isPlaying && autoInitialize)
			init();
	}
	
	public MHActionSheet(Rect aRect)
	{
		if(Application.isPlaying)
			initWithFrame(aRect);
	}
	
	public MHActionSheet(string title, string cancelButtonTitle, string destructiveButtonTitle, params string[] otherButtonTitles)
	{
		if(Application.isPlaying)
			initWithTitle(title, cancelButtonTitle, destructiveButtonTitle, otherButtonTitles);
	}
	
	new public MHActionSheet init()
	{
		return MHiOSManager.Instance.init_actionsheet(tag);
	}
	
	new public MHActionSheet initWithFrame(Rect aRect)
	{
		return MHiOSManager.Instance.initWithFrame_actionsheet(tag, aRect);
	}
	
	public MHActionSheet initWithTitle(string title, string cancelButtonTitle, string destructiveButtonTitle, params string[] otherButtonTitles)
	{
		return MHiOSManager.Instance.initWithTitle_actionsheet(tag, title, cancelButtonTitle, destructiveButtonTitle, otherButtonTitles);
	}
	
	public string title {
		get {
			return MHiOSManager.Instance.title(tag);
		} set {
			MHiOSManager.Instance.title(tag, value);
		}
	}
	
	public bool visible {
		get {
			return MHiOSManager.Instance.visible(tag);
		}
	}
	
	public MHActionSheetStyle actionSheetStyle{
		get {
			return MHiOSManager.Instance.actionSheetStyle(tag);
		} set {
			MHiOSManager.Instance.actionSheetStyle(tag, value);
		}
	}
	
	public int addButtonWithTitle(string title)
	{
		return MHiOSManager.Instance.addButtonWithTitle_actionsheet(tag, title);
	}
	
	public int numberOfButtons {
		get {
			return MHiOSManager.Instance.numberOfButtons(tag);
		}
	}
	
	public string buttonTitleAtIndex(int buttonIndex)
	{
		return MHiOSManager.Instance.buttonTitleAtIndex_actionsheet(tag, buttonIndex);
	}
	
	public int cancelButtonIndex {
		get {
			return MHiOSManager.Instance.cancelButtonIndex(tag);
		} set {
			MHiOSManager.Instance.cancelButtonIndex(tag, value);
		}
	}
	
	public int destructiveButtonIndex {
		get {
			return MHiOSManager.Instance.destructiveButtonIndex(tag);
		} set {
			MHiOSManager.Instance.destructiveButtonIndex(tag, value);
		}
	}
	
	public int firstOtherButtonIndex {
		get {
			return MHiOSManager.Instance.firstOtherButtonIndex(tag);
		}
	}
	
	public void showFromToolbar(MHToolbar view)
	{
		MHiOSManager.Instance.showFromToolbar(tag, view);
	}
	
	public void showInView(MHView view)
	{
		MHiOSManager.Instance.showInView(tag, view);
	}
	
	public void showFromBarButtonItem(MHBarButtonItem item, bool animated)
	{
		MHiOSManager.Instance.showFromBarButtonItem(tag, item, animated);
	}
	
	public void showFromRect(Rect rect, MHView view, bool animated)
	{
		MHiOSManager.Instance.showFromRect(tag, rect, view, animated);
	}
	
	public void dismissWithClickedButtonIndex(int buttonIndex, bool animated)
	{
		MHiOSManager.Instance.dismissWithClickedButtonIndex_actionsheet(tag, buttonIndex, animated);
	}
	#endregion
	
	#region delegate
	public event Action<MHActionSheet, int> actionSheetClickedButtonAtIndex;
	public void _actionSheetClickedButtonAtIndex(MHActionSheet actionSheet, int buttonIndex)
	{
		if(actionSheetClickedButtonAtIndex != null)
			actionSheetClickedButtonAtIndex(actionSheet, buttonIndex);
	}
	
	public event Action<MHActionSheet> willPresentActionSheet;
	public void _willPresentActionSheet(MHActionSheet actionSheet)
	{
		if(willPresentActionSheet != null)
			willPresentActionSheet(actionSheet);
	}

	public event Action<MHActionSheet> didPresentActionSheet;
	public void _didPresentActionSheet(MHActionSheet actionSheet)
	{
		if(didPresentActionSheet != null)
			didPresentActionSheet(actionSheet);
	}
	
	public event Action<MHActionSheet, int> actionSheetWillDismissWithButtonIndex;
	public void _actionSheetWillDismissWithButtonIndex(MHActionSheet actionSheet, int buttonIndex)
	{
		if(actionSheetWillDismissWithButtonIndex != null)
			actionSheetWillDismissWithButtonIndex(actionSheet, buttonIndex);
	}
	
	public event Action<MHActionSheet, int> actionSheetDidDismissWithButtonIndex;
	public void _actionSheetDidDismissWithButtonIndex(MHActionSheet actionSheet, int buttonIndex)
	{
		if(actionSheetDidDismissWithButtonIndex != null)
			actionSheetDidDismissWithButtonIndex(actionSheet, buttonIndex);
	}
	
	public event Action<MHActionSheet> actionSheetCancel;
	public void _actionSheetCancel(MHActionSheet actionSheet)
	{
		if(actionSheetCancel != null)
			actionSheetCancel(actionSheet);
	}
	#endregion
}
