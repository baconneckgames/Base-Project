using UnityEngine;
using System;
using System.Collections;

public class MHTextView : MHScrollView {
	#region functions
	public MHTextView(){}
	
	public MHTextView(bool autoInitialize)
	{
		if(Application.isPlaying && autoInitialize)
			init();
	}
	
	public MHTextView(Rect aRect)
	{
		if(Application.isPlaying)
			initWithFrame(aRect);
	}
	
	new public MHTextView init()
	{
		return MHiOSManager.Instance.init_textview(tag);
	}
	
	new public MHTextView initWithFrame(Rect aRect)
	{
		return MHiOSManager.Instance.initWithFrame_textview(tag, aRect);
	}
	
	public string text {
		get {
			return MHiOSManager.Instance.text(tag);
		} set {
			MHiOSManager.Instance.text(tag, value);
		}
	}
	
	public Color textColor {
		get {
			return MHiOSManager.Instance.textColor(tag);
		} set {
			MHiOSManager.Instance.textColor(tag, value);
		}
	}
	
	public bool editable {
		get {
			return MHiOSManager.Instance.editable(tag);
		} set {
			MHiOSManager.Instance.editable(tag, value);
		}
	}
	
	public MHDataDetectorType dataDetectorTypes {
		get {
			return MHiOSManager.Instance.dataDetectorTypes(tag);
		} set {
			MHiOSManager.Instance.dataDetectorTypes(tag, value);
		}
	}
	
	public MHTextAlignment textAlignment {
		get {
			return MHiOSManager.Instance.textAlignment(tag);
		} set {
			MHiOSManager.Instance.textAlignment(tag, value);
		}
	}
	
	public bool hasText()
	{
		return MHiOSManager.Instance.hasText(tag);
	}
	
	public MHView inputView {
		get {
			return MHiOSManager.Instance.inputView(tag);
		} set {
			MHiOSManager.Instance.inputView(tag, value);
		}
	}
   
	public MHView inputAccessoryView {
		get {
			return MHiOSManager.Instance.inputAccessoryView(tag);
		} set {
			MHiOSManager.Instance.inputAccessoryView(tag, value);
		}
	}
	#endregion
	
	#region delegate
	public event Func<MHTextView, bool> textViewShouldBeginEditing;
	public void _textViewShouldBeginEditing(MHTextView textView)
	{
		if(textViewShouldBeginEditing != null)
			MHTools.ReturnResultToXCode((textViewShouldBeginEditing(textView)).ToString());
		else
			MHTools.ReturnResultToXCode("true");
	}
	
	public event Action<MHTextView> textViewDidBeginEditing;
	public void _textViewDidBeginEditing(MHTextView textView)
	{
		if(textViewDidBeginEditing != null)
			textViewDidBeginEditing(textView);
	}
	
	public event Func<MHTextView, bool> textViewShouldEndEditing;
	public void _textViewShouldEndEditing(MHTextView textView)
	{
		if(textViewShouldEndEditing != null)
			MHTools.ReturnResultToXCode((textViewShouldEndEditing(textView)).ToString());
		else
			MHTools.ReturnResultToXCode("true");
	}
	
	public event Action<MHTextView> textViewDidEndEditing;
	public void _textViewDidEndEditing(MHTextView textView)
	{
		if(textViewDidEndEditing != null)
			textViewDidEndEditing(textView);
	}
	
	public event Action<MHTextView> textViewDidChange;
	public void _textViewDidChange(MHTextView textView)
	{
		if(textViewDidChange != null)
			textViewDidChange(textView);
	}
	#endregion
}
