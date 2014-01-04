using UnityEngine;
using System.Collections;

public partial class MHNavigationItem : MHObject {
	#region functions
	public MHNavigationItem(){}
	
	public MHNavigationItem(bool autoInitialize)
	{
		if(Application.isPlaying && autoInitialize)
			init();
	}
	
	public MHNavigationItem(string newTitle)
	{
		if(Application.isPlaying)
			initWithTitle(newTitle);
	}
	
	new public MHNavigationItem init()
	{
		return MHiOSManager.Instance.init_navigationitem(tag);
	}
	
	public MHNavigationItem initWithTitle(string newTitle)
	{
		return MHiOSManager.Instance.initWithTitle(tag, newTitle);
	}
	
	public string title {
		get {
			return MHiOSManager.Instance.title(tag);
		} set {
			MHiOSManager.Instance.title(tag, value);
		}
	}
	
	public string prompt {
		get {
			return MHiOSManager.Instance.prompt(tag);
		} set {
			MHiOSManager.Instance.prompt(tag, value);
		}
	}
	
	public MHBarButtonItem backBarButtonItem {
		get {
			return MHiOSManager.Instance.backBarButtonItem(tag);
		} set {
			MHiOSManager.Instance.backBarButtonItem(tag, value);
		}
	}
	
	public bool hidesBackButton {
		get {
			return MHiOSManager.Instance.hidesBackButton(tag);
		} set {
			MHiOSManager.Instance.hidesBackButton(tag, value);
		}
	}
	
	public void setHidesBackButton(bool hidesBackButton, bool animated)
	{
		MHiOSManager.Instance.setHidesBackButton(tag, hidesBackButton, animated);
	}
	
	public bool leftItemsSupplementBackButton {
		get {
			return MHiOSManager.Instance.leftItemsSupplementBackButton(tag);
		} set {
			MHiOSManager.Instance.leftItemsSupplementBackButton(tag, value);
		}
	}
	
	public MHView titleView {
		get {
			return MHiOSManager.Instance.titleView(tag);
		} set {
			MHiOSManager.Instance.titleView(tag, value);
		}
	}
	
	public MHBarButtonItem[] leftBarButtonItems {
		get {
			return MHiOSManager.Instance.leftBarButtonItems(tag);
		} set {
			MHiOSManager.Instance.leftBarButtonItems(tag, value);
		}
	}
	
	public MHBarButtonItem leftBarButtonItem {
		get {
			return MHiOSManager.Instance.leftBarButtonItem(tag);
		} set {
			MHiOSManager.Instance.leftBarButtonItem(tag, value);
		}
	}
	
	public MHBarButtonItem[] rightBarButtonItems {
		get {
			return MHiOSManager.Instance.rightBarButtonItems(tag);
		} set {
			MHiOSManager.Instance.rightBarButtonItems(tag, value);
		}
	}
	
	public MHBarButtonItem rightBarButtonItem {
		get {
			return MHiOSManager.Instance.rightBarButtonItem(tag);
		} set {
			MHiOSManager.Instance.rightBarButtonItem(tag, value);
		}
	}
	
	public void setLeftBarButtonItems(MHBarButtonItem[] items, bool animated)
	{
		MHiOSManager.Instance.setLeftBarButtonItems(tag, items, animated);
	}
	
	public void setLeftBarButtonItem(MHBarButtonItem item, bool animated)
	{
		MHiOSManager.Instance.setLeftBarButtonItem(tag, item, animated);
	}
	
	public void setRightBarButtonItems(MHBarButtonItem[] items, bool animated)
	{
		MHiOSManager.Instance.setRightBarButtonItems(tag, items, animated);
	}
	
	public void setRightBarButtonItem(MHBarButtonItem item, bool animated)
	{
		MHiOSManager.Instance.setRightBarButtonItem(tag, item, animated);
	}
	#endregion
}
