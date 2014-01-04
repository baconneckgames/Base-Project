using UnityEngine;
using System.Collections;

public class MHActivityIndicatorView : MHView
{
	public MHActivityIndicatorView(){}
	
	public MHActivityIndicatorView(bool autoInitialize)
	{
		if(Application.isPlaying && autoInitialize)
			init();
	}
	
	public MHActivityIndicatorView(Rect aRect)
	{
		if(Application.isPlaying)
			initWithFrame(aRect);
	}
	
	public MHActivityIndicatorView(MHActivityIndicatorStyle style)
	{
		if(Application.isPlaying)
			initWithActivityIndicatorStyle(style);
	}
	
	new public MHActivityIndicatorView init()
	{
		return MHiOSManager.Instance.init_activityindicator(tag);
	}
	
	new public MHActivityIndicatorView initWithFrame(Rect aRect)
	{
		return MHiOSManager.Instance.initWithFrame_activityindicator(tag, aRect);
	}
	
	public MHActivityIndicatorView initWithActivityIndicatorStyle(MHActivityIndicatorStyle style)
	{
		return MHiOSManager.Instance.initWithActivityIndicatorStyle_activityindicator(tag, style);
	}
	
	public void startAnimating()
	{
		MHiOSManager.Instance.startAnimating(tag);
	}
	
	public void stopAnimating()
	{
		MHiOSManager.Instance.stopAnimating(tag);
	}
	
	public bool isAnimating()
	{
		return MHiOSManager.Instance.isAnimating(tag);
	}
	
	public bool hidesWhenStopped {
		get {
			return MHiOSManager.Instance.hidesWhenStopped(tag);
		} set {
			MHiOSManager.Instance.hidesWhenStopped(tag, value);
		}
	}
	
	public MHActivityIndicatorStyle activityIndicatorViewStyle {
		get {
			return MHiOSManager.Instance.activityIndicatorViewStyle(tag);
		} set {
			MHiOSManager.Instance.activityIndicatorViewStyle(tag, value);
		}
	}
	
	public Color color {
		get {
			return MHiOSManager.Instance.color(tag);
		} set {
			MHiOSManager.Instance.color(tag, value);
		}
	}
}
