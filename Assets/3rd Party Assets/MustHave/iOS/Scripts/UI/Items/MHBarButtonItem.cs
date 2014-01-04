using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MHBarButtonItem : MHObject {
	#region functions
	public MHBarButtonItem(){}
	
	public MHBarButtonItem(bool autoInitialize)
	{
		if(Application.isPlaying && autoInitialize)
			init();
	}
	
	public MHBarButtonItem(MHBarButtonSystemItem systemItem, Action action)
	{
		if(Application.isPlaying)
			initWithBarButtonSystemItem(systemItem, action);
	}
	
	public MHBarButtonItem(MHView customView)
	{
		if(Application.isPlaying && customView != null)
			initWithCustomView(customView);
	}
	
	public MHBarButtonItem(Texture2D image, MHBarButtonItemStyle style, Action action)
	{
		if(Application.isPlaying && image != null)
			initWithImage(image, style, action);
	}
	
	public MHBarButtonItem(string newTitle, MHBarButtonItemStyle style, Action action)
	{
		if(Application.isPlaying && !string.IsNullOrEmpty(newTitle))
			initWithTitle(newTitle, style, action);
	}
	
	public MHBarButtonItem(Texture2D image, Texture2D landscapeImagePhone, MHBarButtonItemStyle style, Action action)
	{
		if(Application.isPlaying && image != null && landscapeImagePhone != null)
			initWithImage(image, landscapeImagePhone, style, action);
	}
	
	new public MHBarButtonItem init()
	{
		return MHiOSManager.Instance.init_barbuttonitem(tag);
	}
	
	public MHBarButtonItem initWithBarButtonSystemItem(MHBarButtonSystemItem systemItem, Action action)
	{
		return MHiOSManager.Instance.initWithBarButtonSystemItem(tag, systemItem, action);
	}
	
	public MHBarButtonItem initWithCustomView(MHView customView)
	{
		if(customView != null)
			return MHiOSManager.Instance.initWithCustomView(tag, customView);
		return null;
	}
	
	public MHBarButtonItem initWithImage(Texture2D image, MHBarButtonItemStyle style, Action action)
	{
		if(image != null)
			return MHiOSManager.Instance.initWithImage_barbutton(tag, image, style, action);
		return null;
	}
	
	public MHBarButtonItem initWithTitle(string newTitle, MHBarButtonItemStyle style, Action action)
	{
		return MHiOSManager.Instance.initWithTitle(tag, newTitle, style, action);
	}
	
	public MHBarButtonItem initWithImage(Texture2D image, Texture2D landscapeImagePhone, MHBarButtonItemStyle style, Action action)
	{
		if(image != null && landscapeImagePhone != null)
			return MHiOSManager.Instance.initWithImage(tag, image, landscapeImagePhone, style, action);
		return null;
	}
	
	public Action action {
		get {
			return MHiOSManager.Instance.action(tag);
		} set {
			MHiOSManager.Instance.action(tag, value);
		}
	}
	
	public MHBarButtonItemStyle style {
		get {
			return MHiOSManager.Instance.style(tag);
		} set {
			MHiOSManager.Instance.style(tag, value);
		}
	}
	
	public string[] possibleTitles {
		get {
			return MHiOSManager.Instance.possibleTitles(tag);
		} set {
			MHiOSManager.Instance.possibleTitles(tag, value);
		}
	}
	
	public float width {
		get {
			return MHiOSManager.Instance.width(tag);
		} set {
			MHiOSManager.Instance.width(tag, value);
		}
	}
	
	public MHView customView {
		get {
			return MHiOSManager.Instance.customView(tag);
		} set {
			MHiOSManager.Instance.customView(tag, value);
		}
	}
	
	public Color tintColor {
		get {
			return MHiOSManager.Instance.tintColor(tag);
		} set {
			MHiOSManager.Instance.tintColor(tag, value);
		}
	}
	
	public Texture2D backButtonBackgroundImageForState(MHControlState state, MHBarMetrics barMetrics)
	{
		return MHiOSManager.Instance.backButtonBackgroundImageForState(tag, state, barMetrics);
	}
	
	public void setBackButtonBackgroundImage(Texture2D backgroundImage, MHControlState state, MHBarMetrics barMetrics)
	{
		MHiOSManager.Instance.setBackButtonBackgroundImage(tag, backgroundImage, state, barMetrics);
	}
	
	public Vector2 backButtonTitlePositionAdjustmentForBarMetrics(MHBarMetrics barMetrics)
	{
		return MHiOSManager.Instance.backButtonTitlePositionAdjustmentForBarMetrics(tag, barMetrics);
	}
	
	public void setBackButtonTitlePositionAdjustment(Vector2 adjustment, MHBarMetrics barMetrics)
	{
		MHiOSManager.Instance.setBackButtonTitlePositionAdjustment(tag, adjustment, barMetrics);
	}
	
	public float backButtonBackgroundVerticalPositionAdjustmentForBarMetrics(MHBarMetrics barMetrics)
	{
		return MHiOSManager.Instance.backButtonBackgroundVerticalPositionAdjustmentForBarMetrics(tag, barMetrics);
	}
	
	public void setBackButtonBackgroundVerticalPositionAdjustment(float adjustment, MHBarMetrics barMetrics)
	{
		MHiOSManager.Instance.setBackButtonBackgroundVerticalPositionAdjustment(tag, adjustment, barMetrics);
	}
	
	public float backgroundVerticalPositionAdjustmentForBarMetrics(MHBarMetrics barMetrics)
	{
		return MHiOSManager.Instance.backgroundVerticalPositionAdjustmentForBarMetrics(tag, barMetrics);
	}
	
	public void setBackgroundVerticalPositionAdjustment(float adjustment, MHBarMetrics barMetrics)
	{
		MHiOSManager.Instance.setBackgroundVerticalPositionAdjustment(tag, adjustment, barMetrics);
	}
	
	public Texture2D backgroundImageForState(MHControlState state, MHBarMetrics barMetrics)
	{
		return MHiOSManager.Instance.backgroundImageForState_barbutton(tag, state, barMetrics);
	}
	
	public void setBackgroundImage(Texture2D image, MHControlState state, MHBarMetrics barMetrics)
	{
		MHiOSManager.Instance.setBackgroundImage_control(tag, image, state, barMetrics);
	}
	
	public Texture2D backgroundImageForState(MHControlState state, MHBarButtonItemStyle style, MHBarMetrics barMetrics)
	{
		return MHiOSManager.Instance.backgroundImageForState(tag, state, style, barMetrics);
	}
	
	public void setBackgroundImage(Texture2D image, MHControlState state, MHBarButtonItemStyle style, MHBarMetrics barMetrics)
	{
		MHiOSManager.Instance.setBackgroundImage_style(tag, image, state, style, barMetrics);
	}
	
	public Vector2 titlePositionAdjustmentForBarMetrics(MHBarMetrics barMetrics)
	{
		return MHiOSManager.Instance.titlePositionAdjustmentForBarMetrics(tag, barMetrics);
	}
	
	public void setTitlePositionAdjustment(Vector2 adjustment, MHBarMetrics barMetrics)
	{
		MHiOSManager.Instance.setTitlePositionAdjustment(tag, adjustment, barMetrics);
	}
	#endregion
}
