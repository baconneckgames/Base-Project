using UnityEngine;
using System.Collections;

public class MHProgressView : MHView
{
	public MHProgressView(){}
	
	public MHProgressView(bool autoInitialize)
	{
		if(Application.isPlaying && autoInitialize)
			init();
	}
	
	public MHProgressView(Rect aRect)
	{
		if(Application.isPlaying)
			initWithFrame(aRect);
	}
	
	public MHProgressView(MHProgressViewStyle style)
	{
		if(Application.isPlaying)
			initWithProgressViewStyle(style);
	}
	
	new public MHProgressView init()
	{
		return MHiOSManager.Instance.init_progressview(tag);
	}
	
	new public MHProgressView initWithFrame(Rect aRect)
	{
		return MHiOSManager.Instance.initWithFrame_progressview(tag, aRect);
	}
	
	public MHProgressView initWithProgressViewStyle(MHProgressViewStyle style)
	{
		return MHiOSManager.Instance.initWithProgressViewStyle_progressview(tag, style);
	}
	
	public float progress {
		get {
			return MHiOSManager.Instance.progress(tag);
		} set {
			MHiOSManager.Instance.progress(tag, value);
		}
	}
	
	public void setProgress(float progress, bool animated)
	{
		MHiOSManager.Instance.setProgress(tag, progress, animated);
	}
	
	public MHProgressViewStyle progressViewStyle {
		get {
			return MHiOSManager.Instance.progressViewStyle(tag);
		} set {
			MHiOSManager.Instance.progressViewStyle(tag, value);
		}
	}
	
	public Color progressTintColor {
		get {
			return MHiOSManager.Instance.progressTintColor(tag);
		} set {
			MHiOSManager.Instance.progressTintColor(tag, value);
		}
	}
	
	public Texture2D progressImage {
		get {
			return MHiOSManager.Instance.progressImage(tag);
		} set {
			MHiOSManager.Instance.progressImage(tag, value);
		}
	}
	
	public Color trackTintColor {
		get {
			return MHiOSManager.Instance.trackTintColor(tag);
		} set {
			MHiOSManager.Instance.trackTintColor(tag, value);
		}
	}
	
	public Texture2D trackImage {
		get {
			return MHiOSManager.Instance.trackImage(tag);
		} set {
			MHiOSManager.Instance.trackImage(tag, value);
		}
	}
}
