using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public partial class MHiOSManager : Singleton<MHiOSManager> {
	#region label
	[DllImport("__Internal")]
    private static extern int _init_label(int tag);
	public MHLabel init_label(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return GetObjectByUniqueTag(_init_label(tag)) as MHLabel;
		}
		else
			return new MHLabel();
	}
	
	[DllImport("__Internal")]
    private static extern int _initWithFrame_label(int tag, string aRect);
	public MHLabel initWithFrame_label(int tag, Rect aRect)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string rectJsonString = MHTools.ConvertRectToJSON(aRect);
			
			return GetObjectByUniqueTag(_initWithFrame_label(tag, rectJsonString)) as MHLabel;
		}
		else
			return new MHLabel();
	}
	
	[DllImport("__Internal")]
    private static extern int _lineBreakMode(int tag);
	public MHLineBreakMode lineBreakMode(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return (MHLineBreakMode)_lineBreakMode(tag);
		}
		else
			return MHLineBreakMode.MHLineBreakByTruncatingTail;
	}
	
	[DllImport("__Internal")]
    private static extern void _lineBreakMode_set(int tag, int mode);
	public void lineBreakMode(int tag, MHLineBreakMode mode)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_lineBreakMode_set(tag, (int)mode);
		}
	}
	
	[DllImport("__Internal")]
    private static extern bool _adjustsLetterSpacingToFitWidth(int tag);
	public bool adjustsLetterSpacingToFitWidth(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _adjustsLetterSpacingToFitWidth(tag);
		}
		else
			return false;
	}
	
	[DllImport("__Internal")]
    private static extern void _adjustsLetterSpacingToFitWidth_set(int tag, bool adjusts);
	public void adjustsLetterSpacingToFitWidth(int tag, bool adjusts)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_adjustsLetterSpacingToFitWidth_set(tag, adjusts);
		}
	}
	
	[DllImport("__Internal")]
    private static extern int _baselineAdjustment(int tag);
	public MHBaselineAdjustment baselineAdjustment(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return (MHBaselineAdjustment)_baselineAdjustment(tag);
		}
		else
			return MHBaselineAdjustment.MHBaselineAdjustmentAlignBaselines;
	}
	
	[DllImport("__Internal")]
    private static extern void _baselineAdjustment_set(int tag, int adjustment);
	public void baselineAdjustment(int tag, MHBaselineAdjustment adjustment)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_baselineAdjustment_set(tag, (int)adjustment);
		}
	}
	
	[DllImport("__Internal")]
    private static extern float _minimumScaleFactor(int tag);
	public float minimumScaleFactor(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _minimumScaleFactor(tag);
		}
		else
			return 0f;
	}
	
	[DllImport("__Internal")]
    private static extern void _minimumScaleFactor_set(int tag, float factor);
	public void minimumScaleFactor(int tag, float factor)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_minimumScaleFactor_set(tag, factor);
		}
	}
	
	[DllImport("__Internal")]
    private static extern int _numberOfLines(int tag);
	public int numberOfLines(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _numberOfLines(tag);
		}
		else
			return 0;
	}
	
	[DllImport("__Internal")]
    private static extern void _numberOfLines_set(int tag, int lines);
	public void numberOfLines(int tag, int lines)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_numberOfLines_set(tag, lines);
		}
	}
	
	[DllImport("__Internal")]
    private static extern string _highlightedTextColor(int tag);
	public Color highlightedTextColor(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string colorJsonString = _highlightedTextColor(tag);
			
			return MHTools.ConvertUIColorToColor(colorJsonString);
		}
		else
			return Color.black;
	}
	
	[DllImport("__Internal")]
    private static extern void _highlightedTextColor_set(int tag, string color);
	public void highlightedTextColor(int tag, Color color)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string colorJsonString = MHTools.ConvertColorToUIColor(color);
			
			_highlightedTextColor_set(tag, colorJsonString);
		}
	}
	
	[DllImport("__Internal")]
    private static extern string _shadowColor(int tag);
	public Color shadowColor(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string colorJsonString = _shadowColor(tag);
			
			return MHTools.ConvertUIColorToColor(colorJsonString);
		}
		else
			return Color.black;
	}
	
	[DllImport("__Internal")]
    private static extern void _shadowColor_set(int tag, string color);
	public void shadowColor(int tag, Color color)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string colorJsonString = MHTools.ConvertColorToUIColor(color);
			
			_shadowColor_set(tag, colorJsonString);
		}
	}
	
	[DllImport("__Internal")]
    private static extern string _shadowOffset(int tag);
	public Vector2 shadowOffset(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string vectorJsonString = _shadowOffset(tag);
			
			return MHTools.ConvertJSONToVector2(vectorJsonString);
		}
		else
			return Vector2.zero;
	}
	
	[DllImport("__Internal")]
    private static extern void _shadowOffset_set(int tag, string offset);
	public void shadowOffset(int tag, Vector2 offset)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string vectorJsonString = MHTools.ConvertVector2ToJSON(offset);
			
			_shadowOffset_set(tag, vectorJsonString);
		}
	}
	
	[DllImport("__Internal")]
    private static extern string _textRectForBounds_label(int tag, string bounds, int numberOfLines);
	public Rect textRectForBounds_label(int tag, Rect bounds, int numberOfLines)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string rectJsonString = MHTools.ConvertRectToJSON(bounds);
			
			string rectJsonString2 = _textRectForBounds_label(tag, rectJsonString, numberOfLines);
			
			return MHTools.ConvertJSONToRect(rectJsonString2);
		}
		else
			return MHTools.RectZero;
	}
	
	[DllImport("__Internal")]
    private static extern void _drawTextInRect_label(int tag, string rect);
	public void drawTextInRect_label(int tag, Rect rect)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string rectJsonString = MHTools.ConvertRectToJSON(rect);
			
			_drawTextInRect_label(tag, rectJsonString);
		}
	}
	
	[DllImport("__Internal")]
    private static extern float _preferredMaxLayoutWidth(int tag);
	public float preferredMaxLayoutWidth(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _preferredMaxLayoutWidth(tag);
		}
		else
			return 0f;
	}
	
	[DllImport("__Internal")]
    private static extern void _preferredMaxLayoutWidth_set(int tag, float width);
	public void preferredMaxLayoutWidth(int tag, float width)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_preferredMaxLayoutWidth_set(tag, width);
		}
	}
	#endregion
}