using UnityEngine;
using System.Collections;

public class MHSwitch : MHControl {

	#region functions
	public MHSwitch(){}
	
	public MHSwitch(bool autoInitialize)
	{
		if(Application.isPlaying && autoInitialize)
			init();
	}
	
	public MHSwitch(Rect aRect)
	{
		if(Application.isPlaying)
			initWithFrame(aRect);
	}
	
	new public MHSwitch init()
	{
		return MHiOSManager.Instance.init_switch(tag);
	}
	
	new public MHSwitch initWithFrame(Rect aRect)
	{
		return MHiOSManager.Instance.initWithFrame_switch(tag, aRect);
	}
	
	public bool on {
		get {
			return MHiOSManager.Instance.on(tag);
		} set {
			MHiOSManager.Instance.on(tag, value);
		}
	}
	
	public void setOn(bool on, bool animated)
	{
		MHiOSManager.Instance.setOn(tag, on, animated);
	}
	
	public Color onTintColor {
		get {
			return MHiOSManager.Instance.onTintColor(tag);
		} set {
			MHiOSManager.Instance.onTintColor(tag, value);
		}
	}
	
	public Color tintColor {
		get {
			return MHiOSManager.Instance.tintColor(tag);
		} set {
			MHiOSManager.Instance.tintColor(tag, value);
		}
	}
	
	public Color thumbTintColor {
		get {
			return MHiOSManager.Instance.thumbTintColor(tag);
		} set {
			MHiOSManager.Instance.thumbTintColor(tag, value);
		}
	}
	
	public Texture2D onImage {
		get {
			return MHiOSManager.Instance.onImage(tag);
		} set {
			MHiOSManager.Instance.onImage(tag, value);
		}
	}
	
	public Texture2D offImage {
		get {
			return MHiOSManager.Instance.offImage(tag);
		} set {
			MHiOSManager.Instance.offImage(tag, value);
		}
	}
	#endregion
}
