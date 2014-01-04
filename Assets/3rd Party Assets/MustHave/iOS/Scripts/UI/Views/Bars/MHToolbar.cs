using UnityEngine;
using System.Collections;

public partial class MHToolbar : MHView {
	#region functions
	public MHToolbar(){}
	
	public MHToolbar(bool autoInitialize)
	{
		if(Application.isPlaying && autoInitialize)
			init();
	}
	
	public MHToolbar(Rect aRect)
	{
		if(Application.isPlaying)
			initWithFrame(aRect);
	}
	
	new public MHToolbar init()
	{
		return MHiOSManager.Instance.init_toolbar(tag);
	}
	
	new public MHToolbar initWithFrame(Rect aRect)
	{
		return MHiOSManager.Instance.initWithFrame_toolbar(tag, aRect);
	}
	
	public MHBarStyle barStyle {
		get {
			return MHiOSManager.Instance.barStyle(tag);
		} set {
			MHiOSManager.Instance.barStyle(tag, value);
		}
	}
	
	public bool translucent {
		get {
			return MHiOSManager.Instance.translucent(tag);
		} set {
			MHiOSManager.Instance.translucent(tag, value);
		}
	}
	
	public MHBarButtonItem[] items {
		get {
			return MHiOSManager.Instance.items_toolbar(tag);
		} set {
			MHiOSManager.Instance.items_toolbar(tag, value);
		}
	}
	
	public void setItems(MHBarButtonItem[] items, bool animated)
	{
		MHiOSManager.Instance.setItems(tag, items, animated);
	}
	
	public Texture2D backgroundImageForToolbarPosition(MHToolbarPosition topOrBottom, MHBarMetrics barMetrics)
	{
		return MHiOSManager.Instance.backgroundImageForToolbarPosition(tag, topOrBottom, barMetrics);
	}
	
	public void setBackgroundImage(Texture2D backgroundImage, MHToolbarPosition topOrBottom, MHBarMetrics barMetrics)
	{
		MHiOSManager.Instance.setBackgroundImage_pos(tag, backgroundImage, topOrBottom, barMetrics);
	}
	
	public Texture2D shadowImageForToolbarPosition(MHToolbarPosition topOrBottom)
	{
		return MHiOSManager.Instance.shadowImageForToolbarPosition(tag, topOrBottom);
	}
	
	public void setShadowImage(Texture2D shadowImage, MHToolbarPosition topOrBottom)
	{
		MHiOSManager.Instance.setShadowImage(tag, shadowImage, topOrBottom);
	}
	
	public Color tintColor {
		get {
			return MHiOSManager.Instance.tintColor(tag);
		} set {
			MHiOSManager.Instance.tintColor(tag, value);
		}
	}
	#endregion
}
