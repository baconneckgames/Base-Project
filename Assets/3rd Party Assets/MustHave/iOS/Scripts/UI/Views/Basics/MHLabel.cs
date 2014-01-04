using UnityEngine;
using System.Collections;

public class MHLabel : MHView {
	public MHLabel(){}
	
	public MHLabel(bool autoInitialize)
	{
		if(Application.isPlaying && autoInitialize)
			init();
	}
	
	public MHLabel(Rect aRect)
	{
		if(Application.isPlaying)
			initWithFrame(aRect);
	}
	
	/// <summary>
	/// PRIVATE CONSTRUCTOR. Syncs an instance of the <see cref="MHLabel"/> class for MHButton.
	/// </summary>
	public MHLabel(MHButton button)
	{
		if(Application.isPlaying)
			MHiOSManager.Instance.syncLabel(tag, button);
	}
	
	new public MHLabel init()
	{
		return MHiOSManager.Instance.init_label(tag);
	}
	
	new public MHLabel initWithFrame(Rect aRect)
	{
		return MHiOSManager.Instance.initWithFrame_label(tag, aRect);
	}
	
	public string text {
		get {
			return MHiOSManager.Instance.text(tag);
		} set {
			MHiOSManager.Instance.text(tag, value);
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
	
	public MHLineBreakMode lineBreakMode {
		get {
			return MHiOSManager.Instance.lineBreakMode(tag);
		} set {
			MHiOSManager.Instance.lineBreakMode(tag, value);
		}
	}
	
	public bool enabled {
		get {
			return MHiOSManager.Instance.enabled(tag);
		} set {
			MHiOSManager.Instance.enabled(tag, value);
		}
	}
	
	public bool adjustsFontSizeToFitWidth {
		get {
			return MHiOSManager.Instance.adjustsFontSizeToFitWidth(tag);
		} set {
			MHiOSManager.Instance.adjustsFontSizeToFitWidth(tag, value);
		}
	}
	
	public bool adjustsLetterSpacingToFitWidth {
		get {
			return MHiOSManager.Instance.adjustsLetterSpacingToFitWidth(tag);
		} set {
			MHiOSManager.Instance.adjustsLetterSpacingToFitWidth(tag, value);
		}
	}
	
	public MHBaselineAdjustment baselineAdjustment {
		get {
			return MHiOSManager.Instance.baselineAdjustment(tag);
		} set {
			MHiOSManager.Instance.baselineAdjustment(tag, value);
		}
	}
	
	public float minimumScaleFactor {
		get {
			return MHiOSManager.Instance.minimumScaleFactor(tag);
		} set {
			MHiOSManager.Instance.minimumScaleFactor(tag, value);
		}
	}
	
	public int numberOfLines {
		get {
			return MHiOSManager.Instance.numberOfLines(tag);
		} set {
			MHiOSManager.Instance.numberOfLines(tag, value);
		}
	}
	
	public Color highlightedTextColor {
		get {
			return MHiOSManager.Instance.highlightedTextColor(tag);
		} set {
			MHiOSManager.Instance.highlightedTextColor(tag, value);
		}
	}
	
	public bool highlighted {
		get {
			return MHiOSManager.Instance.highlighted(tag);
		} set {
			MHiOSManager.Instance.highlighted(tag, value);
		}
	}
	
	public Color shadowColor {
		get {
			return MHiOSManager.Instance.shadowColor(tag);
		} set {
			MHiOSManager.Instance.shadowColor(tag, value);
		}
	}
	
	public Vector2 shadowOffset {
		get {
			return MHiOSManager.Instance.shadowOffset(tag);
		} set {
			MHiOSManager.Instance.shadowOffset(tag, value);
		}
	}
	
	public Rect textRectForBounds(Rect bounds, int numberOfLines)
	{
		return MHiOSManager.Instance.textRectForBounds_label(tag, bounds, numberOfLines);
	}
	
	public void drawTextInRect(Rect rect)
	{
		MHiOSManager.Instance.drawTextInRect_label(tag, rect);
	}
	
	public override bool userInteractionEnabled {
		get {
			return base.userInteractionEnabled;
		} set {
			base.userInteractionEnabled = value;
		}
	}
	
	public float preferredMaxLayoutWidth {
		get {
			return MHiOSManager.Instance.preferredMaxLayoutWidth(tag);
		} set {
			MHiOSManager.Instance.preferredMaxLayoutWidth(tag, value);
		}
	}
}
