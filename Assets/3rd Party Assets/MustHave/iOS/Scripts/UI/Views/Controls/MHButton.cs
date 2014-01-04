using UnityEngine;
using System.Collections;

public class MHButton : MHControl {
	#region functions
	public MHButton(){}
	
	public MHButton(bool autoInitialize)
	{
		if(Application.isPlaying && autoInitialize)
			init();
	}
	
	public MHButton(Rect aRect)
	{
		if(Application.isPlaying)
			initWithFrame(aRect);
	}
	
	public MHButton(MHButtonType type)
	{
		if(Application.isPlaying)
			buttonWithType(type);
	}
	
	new public MHButton init()
	{
		MHButton btn = MHiOSManager.Instance.init_button(tag);
		new MHLabel(btn);
		new MHImageView(btn);
		return btn;
	}
	
	new public MHButton initWithFrame(Rect aRect)
	{
		MHButton btn = MHiOSManager.Instance.initWithFrame_button(tag, aRect);
		new MHLabel(btn);
		new MHImageView(btn);
		return btn;
	}
	
	public MHButton buttonWithType(MHButtonType type)
	{
		MHButton btn = MHiOSManager.Instance.buttonWithType(tag, type);
		new MHLabel(btn);
		new MHImageView(btn);
		return btn;
	}
	
	public MHLabel titleLabel {
		get {
			return MHiOSManager.Instance.titleLabel(tag);
		}
	}
	
	public bool reversesTitleShadowWhenHighlighted {
		get {
			return MHiOSManager.Instance.reversesTitleShadowWhenHighlighted(tag);
		} set {
			MHiOSManager.Instance.reversesTitleShadowWhenHighlighted(tag, value);
		}
	}
	
	public void setTitle(string title, MHControlState state)
	{
		MHiOSManager.Instance.setTitle(tag, title, state);
	}
	
	public void setTitleColor(Color color, MHControlState state)
	{
		MHiOSManager.Instance.setTitleColor(tag, color, state);
	}
	
	public void setTitleShadowColor(Color color, MHControlState state)
	{
		MHiOSManager.Instance.setTitleShadowColor(tag, color, state);
	}
	
	public Color titleColorForState(MHControlState state)
	{
		return MHiOSManager.Instance.titleColorForState(tag, state);
	}
	
	public string titleForState(MHControlState state)
	{
		return MHiOSManager.Instance.titleForState(tag, state);
	}
	
	public Color titleShadowColorForState(MHControlState state)
	{
		return MHiOSManager.Instance.titleShadowColorForState(tag, state);
	}
	
	public bool adjustsImageWhenHighlighted {
		get {
			return MHiOSManager.Instance.adjustsImageWhenHighlighted(tag);
		} set {
			MHiOSManager.Instance.adjustsImageWhenHighlighted(tag, value);
		}
	}
	
	public bool adjustsImageWhenDisabled {
		get {
			return MHiOSManager.Instance.adjustsImageWhenDisabled(tag);
		} set {
			MHiOSManager.Instance.adjustsImageWhenDisabled(tag, value);
		}
	}
	
	public bool showsTouchWhenHighlighted {
		get {
			return MHiOSManager.Instance.showsTouchWhenHighlighted(tag);
		} set {
			MHiOSManager.Instance.showsTouchWhenHighlighted(tag, value);
		}
	}
	
	public Texture2D backgroundImageForState(MHControlState state)
	{
		return MHiOSManager.Instance.backgroundImageForState_button(tag, state);
	}
	
	public Texture2D imageForState(MHControlState state)
	{
		return MHiOSManager.Instance.imageForState(tag, state);
	}
	
	public void setBackgroundImage(Texture2D image, MHControlState state)
	{
		MHiOSManager.Instance.setBackgroundImage_button(tag, image, state);
	}
	
	public void setImage(Texture2D image, MHControlState state)
	{
		MHiOSManager.Instance.setImage(tag, image, state);
	}
	
	public Color tintColor {
		get {
			return MHiOSManager.Instance.tintColor(tag);
		} set {
			MHiOSManager.Instance.tintColor(tag, value);
		}
	}
	
	public Vector4 contentEdgeInsets {
		get {
			return MHiOSManager.Instance.contentEdgeInsets(tag);
		} set {
			MHiOSManager.Instance.contentEdgeInsets(tag, value);
		}
	}
	
	public Vector4 titleEdgeInsets {
		get {
			return MHiOSManager.Instance.titleEdgeInsets(tag);
		} set {
			MHiOSManager.Instance.titleEdgeInsets(tag, value);
		}
	}
	
	public Vector4 imageEdgeInsets {
		get {
			return MHiOSManager.Instance.imageEdgeInsets(tag);
		} set {
			MHiOSManager.Instance.imageEdgeInsets(tag, value);
		}
	}
	
	public MHButtonType buttonType {
		get {
			return MHiOSManager.Instance.buttonType(tag);
		}
	}
	
	public string currentTitle {
		get {
			return MHiOSManager.Instance.currentTitle(tag);
		}
	}
	
	public Color currentTitleColor {
		get {
			return MHiOSManager.Instance.currentTitleColor(tag);
		}
	}
	
	public Color currentTitleShadowColor {
		get {
			return MHiOSManager.Instance.currentTitleShadowColor(tag);
		}
	}
	
	public Texture2D currentImage {
		get {
			return MHiOSManager.Instance.currentImage(tag);
		}
	}
	
	public Texture2D currentBackgroundImage {
		get {
			return MHiOSManager.Instance.currentBackgroundImage(tag);
		}
	}
	
	public MHImageView imageView {
		get {
			return MHiOSManager.Instance.imageView(tag);
		}
	}
	
	public Rect backgroundRectForBounds(Rect bounds)
	{
		return MHiOSManager.Instance.backgroundRectForBounds(tag, bounds);
	}
	
	public Rect contentRectForBounds(Rect bounds)
	{
		return MHiOSManager.Instance.contentRectForBounds(tag, bounds);
	}
	
	public Rect titleRectForContentRect(Rect contentRect)
	{
		return MHiOSManager.Instance.titleRectForContentRect(tag, contentRect);
	}
	
	public Rect imageRectForContentRect(Rect contentRect)
	{
		return MHiOSManager.Instance.imageRectForContentRect(tag, contentRect);
	}
	#endregion
}
