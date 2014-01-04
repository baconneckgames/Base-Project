using UnityEngine;
using System;
using System.Collections;

public class MHAlertView : MHView {
	#region functions
	public MHAlertView(){}
	
	public MHAlertView(bool autoInitialize)
	{
		if(Application.isPlaying && autoInitialize)
			init();
	}
	
	public MHAlertView(Rect aRect)
	{
		if(Application.isPlaying)
			initWithFrame(aRect);
	}
	
	public MHAlertView(string title, string message, string cancelButtonTitle, params string[] otherButtonTitles)
	{
		if(Application.isPlaying)
			initWithTitle(title, message, cancelButtonTitle, otherButtonTitles);
	}
	
	new public MHAlertView init()
	{
		return MHiOSManager.Instance.init_alertview(tag);
	}
	
	new public MHAlertView initWithFrame(Rect aRect)
	{
		return MHiOSManager.Instance.initWithFrame_alertview(tag, aRect);
	}
	
	public MHAlertView initWithTitle(string title, string message, string cancelButtonTitle, params string[] otherButtonTitles)
	{
		return MHiOSManager.Instance.initWithTitle_alertview(tag, title, message, cancelButtonTitle, otherButtonTitles);
	}
	
	public MHAlertViewStyle alertViewStyle {
		get {
			return MHiOSManager.Instance.alertViewStyle(tag);
		} set {
			MHiOSManager.Instance.alertViewStyle(tag, value);
		}
	}
	
	public string title {
		get {
			return MHiOSManager.Instance.title(tag);
		} set {
			MHiOSManager.Instance.title(tag, value);
		}
	}
	
	public string message {
		get {
			return MHiOSManager.Instance.message(tag);
		} set {
			MHiOSManager.Instance.message(tag, value);
		}
	}
	
	public bool visible {
		get {
			return MHiOSManager.Instance.visible(tag);
		}
	}
	
	public int addButtonWithTitle(string title)
	{
		return MHiOSManager.Instance.addButtonWithTitle_alertview(tag, title);
	}
	
	public int numberOfButtons {
		get {
			return MHiOSManager.Instance.numberOfButtons(tag);
		}
	}
	
	public string buttonTitleAtIndex(int index)
	{
		return MHiOSManager.Instance.buttonTitleAtIndex_alertview(tag, index);
	}
	
	public MHTextField textFieldAtIndex(int textFieldIndex)
	{
		return MHiOSManager.Instance.textFieldAtIndex(tag, textFieldIndex);
	}
	
	public int cancelButtonIndex {
		get {
			return MHiOSManager.Instance.cancelButtonIndex(tag);
		} set {
			MHiOSManager.Instance.cancelButtonIndex(tag, value);
		}
	}
	
	public int firstOtherButtonIndex {
		get {
			return MHiOSManager.Instance.firstOtherButtonIndex(tag);
		}
	}
	
	public void show()
	{
		MHiOSManager.Instance.show(tag);
	}
	
	public void dismissWithClickedButtonIndex(int buttonIndex, bool animated)
	{
		MHiOSManager.Instance.dismissWithClickedButtonIndex_alertview(tag, buttonIndex, animated);
	}
	#endregion
	
	#region delegate
	public event Action<MHAlertView, int> alertViewClickedButtonAtIndex;
	public void _alertViewClickedButtonAtIndex(MHAlertView alertView, int buttonIndex)
	{
		if(alertViewClickedButtonAtIndex != null)
			alertViewClickedButtonAtIndex(alertView, buttonIndex);
	}
	
	public event Func<MHAlertView, bool> alertViewShouldEnableFirstOtherButton;
	public void _alertViewShouldEnableFirstOtherButton(MHAlertView alertView)
	{
		if(alertViewShouldEnableFirstOtherButton != null)
			MHTools.ReturnResultToXCode((alertViewShouldEnableFirstOtherButton(alertView)).ToString());
		else
			MHTools.ReturnResultToXCode("true");
	}
	
	public event Action<MHAlertView> willPresentAlertView;
	public void _willPresentAlertView(MHAlertView alertView)
	{
		if(willPresentAlertView != null)
			willPresentAlertView(alertView);
	}
	
	public event Action<MHAlertView> didPresentAlertView;
	public void _didPresentAlertView(MHAlertView alertView)
	{
		if(didPresentAlertView != null)
			didPresentAlertView(alertView);
	}
	
	public event Action<MHAlertView, int> alertViewWillDismissWithButtonIndex;
	public void _alertViewWillDismissWithButtonIndex(MHAlertView alertView, int buttonIndex)
	{
		if(alertViewWillDismissWithButtonIndex != null)
			alertViewWillDismissWithButtonIndex(alertView, buttonIndex);
	}
	
	public event Action<MHAlertView, int> alertViewDidDismissWithButtonIndex;
	public void _alertViewDidDismissWithButtonIndex(MHAlertView alertView, int buttonIndex)
	{
		if(alertViewDidDismissWithButtonIndex != null)
			alertViewDidDismissWithButtonIndex(alertView, buttonIndex);
	}
	
	public event Action<MHAlertView> alertViewCancel;
	public void _alertViewCancel(MHAlertView alertView)
	{
		if(alertViewCancel != null)
			alertViewCancel(alertView);
	}
	#endregion
}
