using UnityEngine;
using System;
using System.Collections;

public class MHTextField : MHControl {
	#region functions
	public MHTextField(){}
	
	public MHTextField(bool autoInitialize)
	{
		if(Application.isPlaying && autoInitialize)
			init();
	}
	
	public MHTextField(Rect aRect)
	{
		if(Application.isPlaying)
			initWithFrame(aRect);
	}
	
	new public MHTextField init()
	{
		return MHiOSManager.Instance.init_textfield(tag);
	}
	
	new public MHTextField initWithFrame(Rect aRect)
	{
		return MHiOSManager.Instance.initWithFrame_textfield(tag, aRect);
	}
	
	public string text {
		get {
			return MHiOSManager.Instance.text(tag);
		} set {
			MHiOSManager.Instance.text(tag, value);
		}
	}
	
	public string placeholder {
		get {
			return MHiOSManager.Instance.placeholder(tag);
		} set {
			MHiOSManager.Instance.placeholder(tag, value);
		}
	}
	
	public Color textColor {
		get {
			return MHiOSManager.Instance.textColor(tag);
		} set {
			MHiOSManager.Instance.textColor(tag, value);
		}
	}
	
	public MHTextAlignment textAlignment {
		get {
			return MHiOSManager.Instance.textAlignment(tag);
		} set {
			MHiOSManager.Instance.textAlignment(tag, value);
		}
	}
	
	public bool adjustsFontSizeToFitWidth {
		get {
			return MHiOSManager.Instance.adjustsFontSizeToFitWidth(tag);
		} set {
			MHiOSManager.Instance.adjustsFontSizeToFitWidth(tag, value);
		}
	}
	
	public float minimumFontSize {
		get {
			return MHiOSManager.Instance.minimumFontSize(tag);
		} set {
			MHiOSManager.Instance.minimumFontSize(tag, value);
		}
	}
	
	public bool editing {
		get {
			return MHiOSManager.Instance.editing(tag);
		}
	}
	
	public bool clearsOnBeginEditing {
		get {
			return MHiOSManager.Instance.clearsOnBeginEditing(tag);
		} set {
			MHiOSManager.Instance.clearsOnBeginEditing(tag, value);
		}
	}
	
	public bool clearsOnInsertion {
		get {
			return MHiOSManager.Instance.clearsOnInsertion(tag);
		} set {
			MHiOSManager.Instance.clearsOnInsertion(tag, value);
		}
	}
	
	public MHTextBorderStyle borderStyle {
		get {
			return MHiOSManager.Instance.borderStyle(tag);
		} set {
			MHiOSManager.Instance.borderStyle(tag, value);
		}
	}
	
	public Texture2D background {
		get {
			return MHiOSManager.Instance.background(tag);
		} set {
			MHiOSManager.Instance.background(tag, value);
		}
	}
	
	public Texture2D disabledBackground {
		get {
			return MHiOSManager.Instance.disabledBackground(tag);
		} set {
			MHiOSManager.Instance.disabledBackground(tag, value);
		}
	}
	
	public MHTextFieldViewMode clearButtonMode {
		get {
			return MHiOSManager.Instance.clearButtonMode(tag);
		} set {
			MHiOSManager.Instance.clearButtonMode(tag, value);
		}
	}
	
	public MHView leftView {
		get {
			return MHiOSManager.Instance.leftView(tag);
		} set {
			MHiOSManager.Instance.leftView(tag, value);
		}
	}
	
	public MHTextFieldViewMode leftViewMode {
		get {
			return MHiOSManager.Instance.leftViewMode(tag);
		} set {
			MHiOSManager.Instance.leftViewMode(tag, value);
		}
	}
	
	public MHView rightView {
		get {
			return MHiOSManager.Instance.rightView(tag);
		} set {
			MHiOSManager.Instance.rightView(tag, value);
		}
	}
	
	public MHTextFieldViewMode rightViewMode {
		get {
			return MHiOSManager.Instance.rightViewMode(tag);
		} set {
			MHiOSManager.Instance.rightViewMode(tag, value);
		}
	}
	
	public Rect textRectForBounds(Rect bounds)
	{
		return MHiOSManager.Instance.textRectForBounds_textfield(tag, bounds);
	}
	
	public void drawTextInRect(Rect rect)
	{
		MHiOSManager.Instance.drawTextInRect_textfield(tag, rect);
	}
	
	public Rect placeholderRectForBounds(Rect bounds)
	{
		return MHiOSManager.Instance.placeholderRectForBounds(tag, bounds);
	}
	
	public void drawPlaceholderInRect(Rect rect)
	{
		MHiOSManager.Instance.drawPlaceholderInRect(tag, rect);
	}
	
	public Rect borderRectForBounds(Rect bounds)
	{
		return MHiOSManager.Instance.borderRectForBounds(tag, bounds);
	}
	
	public Rect editingRectForBounds(Rect bounds)
	{
		return MHiOSManager.Instance.editingRectForBounds(tag, bounds);
	}
	
	public Rect clearButtonRectForBounds(Rect bounds)
	{
		return MHiOSManager.Instance.clearButtonRectForBounds(tag, bounds);
	}
	
	public Rect leftViewRectForBounds(Rect bounds)
	{
		return MHiOSManager.Instance.leftViewRectForBounds(tag, bounds);
	}
	
	public Rect rightViewRectForBounds(Rect bounds)
	{
		return MHiOSManager.Instance.rightViewRectForBounds(tag, bounds);
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
	public event Func<MHTextField, bool> textFieldShouldBeginEditing;
	public void _textFieldShouldBeginEditing(MHTextField textField)
	{
		if(textFieldShouldBeginEditing != null)
			MHTools.ReturnResultToXCode((textFieldShouldBeginEditing(textField)).ToString());
		else
			MHTools.ReturnResultToXCode("true");
	}
	
	public event Action<MHTextField> textFieldDidBeginEditing;
	public void _textFieldDidBeginEditing(MHTextField textField)
	{
		if(textFieldDidBeginEditing != null)
			textFieldDidBeginEditing(textField);
	}
	
	public event Func<MHTextField, bool> textFieldShouldEndEditing;
	public void _textFieldShouldEndEditing(MHTextField textField)
	{
		if(textFieldShouldEndEditing != null)
			MHTools.ReturnResultToXCode((textFieldShouldEndEditing(textField)).ToString());
		else
			MHTools.ReturnResultToXCode("true");
	}
	
	public event Action<MHTextField> textFieldDidEndEditing;
	public void _textFieldDidEndEditing(MHTextField textField)
	{
		if(textFieldDidEndEditing != null)
			textFieldDidEndEditing(textField);
	}
	
	public event Func<MHTextField, bool> textFieldShouldClear;
	public void _textFieldShouldClear(MHTextField textField)
	{
		if(textFieldShouldClear != null)
			MHTools.ReturnResultToXCode((textFieldShouldClear(textField)).ToString());
		else
			MHTools.ReturnResultToXCode("true");
	}
	
	public event Func<MHTextField, bool> textFieldShouldReturn;
	public void _textFieldShouldReturn(MHTextField textField)
	{
		if(textFieldShouldReturn != null)
			MHTools.ReturnResultToXCode((textFieldShouldReturn(textField)).ToString());
		else
			MHTools.ReturnResultToXCode("true");
	}
	#endregion
}
