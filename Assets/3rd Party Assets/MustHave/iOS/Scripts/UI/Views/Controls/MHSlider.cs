using UnityEngine;
using System.Collections;

public class MHSlider : MHControl {

	#region functions
	public MHSlider(){}
	
	public MHSlider(bool autoInitialize)
	{
		if(Application.isPlaying && autoInitialize)
			init();
	}
	
	public MHSlider(Rect aRect)
	{
		if(Application.isPlaying)
			initWithFrame(aRect);
	}
	
	new public MHSlider init()
	{
		return MHiOSManager.Instance.init_slider(tag);
	}
	
	new public MHSlider initWithFrame(Rect aRect)
	{
		return MHiOSManager.Instance.initWithFrame_slider(tag, aRect);
	}
	
	public float val {
		get {
			return MHiOSManager.Instance.val(tag);
		} set {
			MHiOSManager.Instance.val(tag, value);
		}
	}
	
	public void setVal(float val, bool animated)
	{
		MHiOSManager.Instance.setVal(tag, val, animated);
	}
	
	public float minimumValue {
		get {
			return MHiOSManager.Instance.minimumValue(tag);
		} set {
			MHiOSManager.Instance.minimumValue(tag, value);
		}
	}
	
	public float maximumValue {
		get {
			return MHiOSManager.Instance.maximumValue(tag);
		} set {
			MHiOSManager.Instance.maximumValue(tag, value);
		}
	}
	
	public bool continuous {
		get {
			return MHiOSManager.Instance.continuous(tag);
		} set {
			MHiOSManager.Instance.continuous(tag, value);
		}
	}
	
	public Texture2D minimumValueImage {
		get {
			return MHiOSManager.Instance.minimumValueImage(tag);
		} set {
			MHiOSManager.Instance.minimumValueImage(tag, value);
		}
	}
	
	public Texture2D maximumValueImage {
		get {
			return MHiOSManager.Instance.maximumValueImage(tag);
		} set {
			MHiOSManager.Instance.maximumValueImage(tag, value);
		}
	}
	
	public Color minimumTrackTintColor {
		get {
			return MHiOSManager.Instance.minimumTrackTintColor(tag);
		} set {
			MHiOSManager.Instance.minimumTrackTintColor(tag, value);
		}
	}
	
	public Texture2D currentMinimumTrackImage {
		get {
			return MHiOSManager.Instance.currentMinimumTrackImage(tag);
		}
	}
	
	public Texture2D minimumTrackImageForState(MHControlState state)
	{
		return MHiOSManager.Instance.minimumTrackImageForState(tag, state);
	}
	
	public void setMinimumTrackImageForState(Texture2D image, MHControlState state)
	{
		MHiOSManager.Instance.setMinimumTrackImageForState(tag, image, state);
	}
	
	public Color maximumTrackTintColor {
		get {
			return MHiOSManager.Instance.maximumTrackTintColor(tag);
		} set {
			MHiOSManager.Instance.maximumTrackTintColor(tag, value);
		}
	}
	
	public Texture2D currentMaximumTrackImage {
		get {
			return MHiOSManager.Instance.currentMaximumTrackImage(tag);
		}
	}
	
	public Texture2D maximumTrackImageForState(MHControlState state)
	{
		return MHiOSManager.Instance.maximumTrackImageForState(tag, state);
	}
	
	public void setMaximumTrackImageForState(Texture2D image, MHControlState state)
	{
		MHiOSManager.Instance.setMaximumTrackImageForState(tag, image, state);
	}
	
	public Color thumbTintColor {
		get {
			return MHiOSManager.Instance.thumbTintColor(tag);
		} set {
			MHiOSManager.Instance.thumbTintColor(tag, value);
		}
	}
	
	public Texture2D currentThumbImage {
		get {
			return MHiOSManager.Instance.currentThumbImage(tag);
		}
	}
	
	public Texture2D thumbImageForState(MHControlState state)
	{
		return MHiOSManager.Instance.thumbImageForState(tag, state);
	}
	
	public void setThumbImageForState(Texture2D image, MHControlState state)
	{
		MHiOSManager.Instance.setThumbImageForState(tag, image, state);
	}
	#endregion
}
