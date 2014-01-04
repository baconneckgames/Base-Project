using UnityEngine;
using System.Collections;

public class MHImageView : MHView {
	#region functions
	public MHImageView(){}
	
	public MHImageView(bool autoInitialize)
	{
		if(Application.isPlaying && autoInitialize)
			init();
	}
	
	public MHImageView(Rect aRect)
	{
		if(Application.isPlaying)
			initWithFrame(aRect);
	}
	
	/// <summary>
	/// PRIVATE CONSTRUCTOR. Syncs an instance of the <see cref="MHImageView"/> class for MHButton.
	/// </summary>
	public MHImageView(MHButton button)
	{
		if(Application.isPlaying)
			MHiOSManager.Instance.syncImageView(tag, button);
	}
	
	public MHImageView(Texture2D image)
	{
		if(Application.isPlaying)
			initWithImage(image);
	}
	
	public MHImageView(Texture2D image, Texture2D highlightedImage)
	{
		if(Application.isPlaying)
			initWithImage(image, highlightedImage);
	}
	
	new public MHImageView init()
	{
		return MHiOSManager.Instance.init_imageview(tag);
	}
	
	new public MHImageView initWithFrame(Rect aRect)
	{
		return MHiOSManager.Instance.initWithFrame_imageview(tag, aRect);
	}
	
	public MHImageView initWithImage(Texture2D image)
	{
		return MHiOSManager.Instance.initWithImage_imageview(tag, image);
	}
	
	public MHImageView initWithImage(Texture2D image, Texture2D highlightedImage)
	{
		return MHiOSManager.Instance.initWithImageHighlightedImage(tag, image, highlightedImage);
	}
	
	public Texture2D image {
		get {
			return MHiOSManager.Instance.image(tag);
		} set {
			MHiOSManager.Instance.image(tag, value);
		}
	}
	
	public Texture2D highlightedImage {
		get {
			return MHiOSManager.Instance.highlightedImage(tag);
		} set {
			MHiOSManager.Instance.highlightedImage(tag, value);
		}
	}
	
	public Texture2D[] animationImages {
		get {
			return MHiOSManager.Instance.animationImages(tag);
		} set {
			MHiOSManager.Instance.animationImages(tag, value);
		}
	}
	
	public Texture2D[] highlightedAnimationImages {
		get {
			return MHiOSManager.Instance.highlightedAnimationImages(tag);
		} set {
			MHiOSManager.Instance.highlightedAnimationImages(tag, value);
		}
	}
	
	public float animationDuration {
		get {
			return MHiOSManager.Instance.animationDuration(tag);
		} set {
			MHiOSManager.Instance.animationDuration(tag, value);
		}
	}
	
	public int animationRepeatCount {
		get {
			return MHiOSManager.Instance.animationRepeatCount(tag);
		} set {
			MHiOSManager.Instance.animationRepeatCount(tag, value);
		}
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
	
	public override bool userInteractionEnabled {
		get {
			return base.userInteractionEnabled;
		} set {
			base.userInteractionEnabled = value;
		}
	}
	
	public bool highlighted {
		get {
			return MHiOSManager.Instance.highlighted(tag);
		} set {
			MHiOSManager.Instance.highlighted(tag, value);
		}
	}
	#endregion
}
