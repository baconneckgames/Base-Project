using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public partial class MHiOSManager : Singleton<MHiOSManager> {
	#region view_xcode_output
	[DllImport("__Internal")]
    private static extern int _init_view(int tag);
	public MHView init_view(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return GetObjectByUniqueTag(_init_view(tag)) as MHView;
		}
		else
			return new MHView();
	}
	
	[DllImport("__Internal")]
    private static extern int _initWithFrame(int tag, string aRect);
	public MHView initWithFrame(int tag, Rect aRect)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string rectJsonString = MHTools.ConvertRectToJSON(aRect);
			
			return GetObjectByUniqueTag(_initWithFrame(tag, rectJsonString)) as MHView;
		}
		else
			return new MHView();
	}
	
	[DllImport("__Internal")]
    private static extern string _backgroundColor(int tag);
	public Color backgroundColor(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string colorJsonString = _backgroundColor(tag);
			
			return MHTools.ConvertUIColorToColor(colorJsonString);
		}
		else
			return Color.black;
	}
	
	[DllImport("__Internal")]
    private static extern void _backgroundColor_set(int tag, string color);
	public void backgroundColor(int tag, Color color)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string colorJsonString = MHTools.ConvertColorToUIColor(color);
			
			_backgroundColor_set(tag, colorJsonString);
		}
	}
	
	[DllImport("__Internal")]
    private static extern bool _hidden(int tag);
	public bool hidden(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _hidden(tag);
		}
		else
			return false;
	}
	
	[DllImport("__Internal")]
    private static extern void _hidden_set(int tag, bool hid);
	public void hidden(int tag, bool hid)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_hidden_set(tag, hid);
		}
	}
	
	[DllImport("__Internal")]
    private static extern float _alpha(int tag);
	public float alpha(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _alpha(tag);
		}
		else
			return 0f;
	}
	
	[DllImport("__Internal")]
    private static extern void _alpha_set(int tag, float a);
	public void alpha(int tag, float a)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_alpha_set(tag, a);
		}
	}
	
	[DllImport("__Internal")]
    private static extern bool _opaque(int tag);
	public bool opaque(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _opaque(tag);
		}
		else
			return false;
	}
	
	[DllImport("__Internal")]
    private static extern void _opaque_set(int tag, bool o);
	public void opaque(int tag, bool o)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_opaque_set(tag, o);
		}
	}
	
	[DllImport("__Internal")]
    private static extern bool _clipsToBounds(int tag);
	public bool clipsToBounds(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _clipsToBounds(tag);
		}
		else
			return false;
	}
	
	[DllImport("__Internal")]
    private static extern void _clipsToBounds_set(int tag, bool clip);
	public void clipsToBounds(int tag, bool clip)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_clipsToBounds_set(tag, clip);
		}
	}
	
	[DllImport("__Internal")]
    private static extern bool _clearsContextBeforeDrawing(int tag);
	public bool clearsContextBeforeDrawing(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _clearsContextBeforeDrawing(tag);
		}
		else
			return false;
	}
	
	[DllImport("__Internal")]
    private static extern void _clearsContextBeforeDrawing_set(int tag, bool clear);
	public void clearsContextBeforeDrawing(int tag, bool clear)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_clearsContextBeforeDrawing_set(tag, clear);
		}
	}
	
	[DllImport("__Internal")]
    private static extern bool _userInteractionEnabled(int tag);
	public bool userInteractionEnabled(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _userInteractionEnabled(tag);
		}
		else
			return false;
	}
	
	[DllImport("__Internal")]
    private static extern void _userInteractionEnabled_set(int tag, bool enabled);
	public void userInteractionEnabled(int tag, bool enabled)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_userInteractionEnabled_set(tag, enabled);
		}
	}
	
	[DllImport("__Internal")]
    private static extern bool _multipleTouchEnabled(int tag);
	public bool multipleTouchEnabled(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _multipleTouchEnabled(tag);
		}
		else
			return false;
	}
	
	[DllImport("__Internal")]
    private static extern void _multipleTouchEnabled_set(int tag, bool enabled);
	public void multipleTouchEnabled(int tag, bool enabled)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_multipleTouchEnabled_set(tag, enabled);
		}
	}
	
	[DllImport("__Internal")]
    private static extern bool _exclusiveTouch(int tag);
	public bool exclusiveTouch(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _exclusiveTouch(tag);
		}
		else
			return false;
	}
	
	[DllImport("__Internal")]
    private static extern void _exclusiveTouch_set(int tag, bool exclusive);
	public void exclusiveTouch(int tag, bool exclusive)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_exclusiveTouch_set(tag, exclusive);
		}
	}
	
	[DllImport("__Internal")]
    private static extern string _frame(int tag);
	public Rect frame(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string frameJsonString = _frame(tag);
			
			return MHTools.ConvertJSONToRect(frameJsonString);
		}
		else
			return MHTools.RectZero;
	}
	
	[DllImport("__Internal")]
    private static extern void _frame_set(int tag, string rect);
	public void frame(int tag, Rect rect)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string frameJsonString = MHTools.ConvertRectToJSON(rect);
			
			_frame_set(tag, frameJsonString);
		}
	}
	
	[DllImport("__Internal")]
    private static extern string _bounds(int tag);
	public Rect bounds(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string boundsJsonString = _bounds(tag);
			
			return MHTools.ConvertJSONToRect(boundsJsonString);
		}
		else
			return MHTools.RectZero;
	}
	
	[DllImport("__Internal")]
    private static extern void _bounds_set(int tag, string rect);
	public void bounds(int tag, Rect rect)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string boundsJsonString = MHTools.ConvertRectToJSON(rect);
			
			_bounds_set(tag, boundsJsonString);
		}
	}
	
	[DllImport("__Internal")]
    private static extern string _center(int tag);
	public Vector2 center(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string centerJsonString = _center(tag);
			
			return MHTools.ConvertJSONToVector2(centerJsonString);
		}
		else
			return Vector2.zero;
	}
	
	[DllImport("__Internal")]
    private static extern void _center_set(int tag, string point);
	public void center(int tag, Vector2 point)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string centerJsonString = MHTools.ConvertVector2ToJSON(point);
			
			_center_set(tag, centerJsonString);
		}
	}
	
	[DllImport("__Internal")]
    private static extern int _superview(int tag);
	public MHView superview(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int viewTag = _superview(tag);
			
			if(viewTag == 0)
				return MHView.unityView;
			return GetObjectByUniqueTag(viewTag) as MHView;
		}
		else
			return new MHView();
	}
	
	[DllImport("__Internal")]
    private static extern string _subviews(int tag);
	public MHView[] subviews(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int[] viewTags = MHTools.ConvertJSONToIntArray(_subviews(tag));
			
			return GetObjectsByUniqueTags(viewTags) as MHView[];
		}
		else
			return new MHView[]{};
	}
	
	[DllImport("__Internal")]
    private static extern void _addSubview(int tag, int subview);
	public void addSubview(int tag, MHView subview)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int subviewTag = subview.tag;
			
			_addSubview(tag, subviewTag);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _bringSubviewToFront(int tag, int subview);
	public void bringSubviewToFront(int tag, MHView subview)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int subviewTag = subview.tag;
			
			_bringSubviewToFront(tag, subviewTag);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _sendSubviewToBack(int tag, int subview);
	public void sendSubviewToBack(int tag, MHView subview)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int subviewTag = subview.tag;
			
			_sendSubviewToBack(tag, subviewTag);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _removeFromSuperview(int tag);
	public void removeFromSuperview(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_removeFromSuperview(tag);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _insertSubviewAtIndex(int tag, int view, int index);
	public void insertSubviewAtIndex(int tag, MHView view, int index)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int viewTag = view.tag;
			
			_insertSubviewAtIndex(tag, viewTag, index);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _insertSubviewAboveSubview(int tag, int view, int siblingSubview);
	public void insertSubviewAboveSubview(int tag, MHView view, MHView siblingSubview)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int viewTag = view.tag;
			int siblingsubviewTag = siblingSubview.tag;
			
			_insertSubviewAboveSubview(tag, viewTag, siblingsubviewTag);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _insertSubviewBelowSubview(int tag, int view, int siblingSubview);
	public void insertSubviewBelowSubview(int tag, MHView view, MHView siblingSubview)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int viewTag = view.tag;
			int siblingsubviewTag = siblingSubview.tag;
			
			_insertSubviewBelowSubview(tag, viewTag, siblingsubviewTag);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _exchangeSubviewAtIndex(int tag, int index1, int index2);
	public void exchangeSubviewAtIndex(int tag, int index1, int index2)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_exchangeSubviewAtIndex(tag, index1, index2);
		}
	}
	
	[DllImport("__Internal")]
    private static extern bool _isDescendantOfView(int tag, int view);
	public bool isDescendantOfView(int tag, MHView view)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int viewTag = view.tag;
			
			return _isDescendantOfView(tag, viewTag);
		}
		else
			return false;
	}
	
	[DllImport("__Internal")]
    private static extern int _autoresizingMask(int tag);
	public MHViewAutoresizing autoresizingMask(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return (MHViewAutoresizing)_autoresizingMask(tag);
		}
		else
			return MHViewAutoresizing.MHViewAutoresizingNone;
	}
	
	[DllImport("__Internal")]
    private static extern void _autoresizingMask_set(int tag, int mask);
	public void autoresizingMask(int tag, MHViewAutoresizing mask)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_autoresizingMask_set(tag, (int)mask);
		}
	}
	
	[DllImport("__Internal")]
    private static extern bool _autoresizesSubviews(int tag);
	public bool autoresizesSubviews(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _autoresizesSubviews(tag);
		}
		else
			return false;
	}
	
	[DllImport("__Internal")]
    private static extern void _autoresizesSubviews_set(int tag, bool autoresize);
	public void autoresizesSubviews(int tag, bool autoresize)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_autoresizesSubviews_set(tag, autoresize);
		}
	}
	
	[DllImport("__Internal")]
    private static extern int _contentMode(int tag);
	public MHViewContentMode contentMode(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return (MHViewContentMode)_contentMode(tag);
		}
		else
			return MHViewContentMode.MHViewContentModeScaleToFill;
	}
	
	[DllImport("__Internal")]
    private static extern void _contentMode_set(int tag, int mode);
	public void contentMode(int tag, MHViewContentMode mode)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_contentMode_set(tag, (int)mode);
		}
	}
	
	[DllImport("__Internal")]
    private static extern string _sizeThatFits(int tag, string size);
	public Vector2 sizeThatFits(int tag, Vector2 size)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string sizeJsonString = MHTools.ConvertVector2ToJSON(size);
			
			sizeJsonString = _sizeThatFits(tag, sizeJsonString);
			
			return MHTools.ConvertJSONToVector2(sizeJsonString);
		}
		else
			return Vector2.zero;
	}
	
	[DllImport("__Internal")]
    private static extern void _sizeToFit(int tag);
	public void sizeToFit(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_sizeToFit(tag);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _layoutSubviews(int tag);
	public void layoutSubviews(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_layoutSubviews(tag);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _setNeedsLayout(int tag);
	public void setNeedsLayout(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_setNeedsLayout(tag);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _layoutIfNeeded(int tag);
	public void layoutIfNeeded(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_layoutIfNeeded(tag);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _drawRect(int tag, string rect);
	public void drawRect(int tag, Rect rect)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string rectJsonString = MHTools.ConvertRectToJSON(rect);
			
			_drawRect(tag, rectJsonString);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _setNeedsDisplay(int tag);
	public void setNeedsDisplay(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_setNeedsDisplay(tag);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _setNeedsDisplayInRect(int tag, string invalidRect);
	public void setNeedsDisplayInRect(int tag, Rect invalidRect)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string rectJsonString = MHTools.ConvertRectToJSON(invalidRect);
			
			_setNeedsDisplayInRect(tag, rectJsonString);
		}
	}
	
	[DllImport("__Internal")]
    private static extern float _contentScaleFactor(int tag);
	public float contentScaleFactor(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _contentScaleFactor(tag);
		}
		else
			return 0f;
	}
	
	[DllImport("__Internal")]
    private static extern void _contentScaleFactor_set(int tag, float factor);
	public void contentScaleFactor(int tag, float factor)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_contentScaleFactor_set(tag, factor);
		}
	}
	/*
	public void addGestureRecognizer(int tag, MHGestureRecognizer gestureRecognizer)
	{
		// TO BE IMPLEMENTED IN FUTURE
	}
	
	public void removeGestureRecognizer(int tag, MHGestureRecognizer gestureRecognizer)
	{
		// TO BE IMPLEMENTED IN FUTURE
	}
	
	public MHGestureRecognizer[] gestureRecognizers(int tag)
	{
		// TO BE IMPLEMENTED IN FUTURE
		return null;
	}
	
	public void gestureRecognizers(int tag, MHGestureRecognizer[] gestureRecognizers)
	{
		// TO BE IMPLEMENTED IN FUTURE
	}
	
	public bool gestureRecognizerShouldBegin(int tag, MHGestureRecognizer gestureRecognizer)
	{
		// TO BE IMPLEMENTED IN FUTURE
		return false;
	}
	*/
	[DllImport("__Internal")]
    private static extern void _animateWithDuration_1(int tag, float duration, float delay, int options, int completion);
	public void animateWithDuration(int tag, float duration, float delay, MHViewAnimationOptions options, Action<bool> completion)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int completionID = SaveActionToUniqueID(completion);
			
			_animateWithDuration_1(tag, duration, delay, (int)options, completionID);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _animateWithDuration_2(int tag, float duration, int completion);
	public void animateWithDuration(int tag, float duration, Action<bool> completion)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int completionID = SaveActionToUniqueID(completion);
			
			_animateWithDuration_2(tag, duration, completionID);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _animateWithDuration_3(int tag, float duration);
	public void animateWithDuration(int tag, float duration)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_animateWithDuration_3(tag, duration);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _transitionWithView_1(int tag, int view, float duration, int options, int completion);
	public void transitionWithView(int tag, MHView view, float duration, MHViewAnimationOptions options, Action<bool> completion)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int completionID = SaveActionToUniqueID(completion);
			int viewTag = view.tag;
			
			_transitionWithView_1(tag, viewTag, duration, (int)options, completionID);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _transitionFromView_2(int tag, int fromView, int toView, float duration, int options, int completion);
	public void transitionFromView(int tag, MHView fromView, MHView toView, float duration, MHViewAnimationOptions options, Action<bool> completion)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int completionID = SaveActionToUniqueID(completion);
			int fromviewTag = fromView.tag;
			int toviewTag = toView.tag;
			
			_transitionFromView_2(tag, fromviewTag, toviewTag, duration, (int)options, completionID);
		}
	}
	
	// context is always 'this'
	[DllImport("__Internal")]
    private static extern void _beginAnimations(int tag, string animationID, int context);
	public void beginAnimations(int tag, string animationID, MHObject context)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			if(context != null)
				_beginAnimations(tag, animationID, context.tag);
			else
				_beginAnimations(tag, animationID, 0);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _commitAnimations(int tag);
	public void commitAnimations(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_commitAnimations(tag);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _setAnimationStartDate(int tag, string dte);
	public void setAnimationStartDate(int tag, DateTime dte)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string dateJsonString = MHTools.ConvertDateTimeToJSON(dte);
			
			_setAnimationStartDate(tag, dateJsonString);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _setAnimationsEnabled(int tag, bool enable);
	public void setAnimationsEnabled(int tag, bool enable)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_setAnimationsEnabled(tag, enable);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _setAnimationDuration(int tag, float duration);
	public void setAnimationDuration(int tag, float duration)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_setAnimationDuration(tag, duration);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _setAnimationDelay(int tag, float delay);
	public void setAnimationDelay(int tag, float delay)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_setAnimationDelay(tag, delay);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _setAnimationCurve(int tag, int curve);
	public void setAnimationCurve(int tag, MHViewAnimationCurve curve)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_setAnimationCurve(tag, (int)curve);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _setAnimationRepeatCount(int tag, float count);
	public void setAnimationRepeatCount(int tag, float count)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_setAnimationRepeatCount(tag, count);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _setAnimationRepeatAutoreverses(int tag, bool repeatAutoreverses);
	public void setAnimationRepeatAutoreverses(int tag, bool repeatAutoreverses)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_setAnimationRepeatAutoreverses(tag, repeatAutoreverses);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _setAnimationBeginsFromCurrentState(int tag, bool fromCurrentState);
	public void setAnimationBeginsFromCurrentState(int tag, bool fromCurrentState)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_setAnimationBeginsFromCurrentState(tag, fromCurrentState);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _setAnimationTransition(int tag, int transition, int view, bool cache);
	public void setAnimationTransition(int tag, MHViewAnimationTransition transition, MHView view, bool cache)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int viewTag = view.tag;
			
			_setAnimationTransition(tag, (int)transition, viewTag, cache);
		}
	}
	
	[DllImport("__Internal")]
    private static extern bool _areAnimationsEnabled(int tag);
	public bool areAnimationsEnabled(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _areAnimationsEnabled(tag);
		}
		else
			return false;
	}
	
	/*
	public int tag(int tag)
	{
		return 0;
	}
	
	public void tag(int tag, int tagValue)
	{
		
	}
	*/
	
	[DllImport("__Internal")]
    private static extern int _viewWithTag(int tag, int tagOfView);
	public MHView viewWithTag(int tag, int tagOfView)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int viewTag = _viewWithTag(tag, tagOfView);
			
			if(viewTag == 0)
				return MHView.unityView;
			return GetObjectByUniqueTag(viewTag) as MHView;
		}
		else
			return new MHView();
	}
	
	[DllImport("__Internal")]
    private static extern string _convertPointToView(int tag, string point, int toView);
	public Vector2 convertPointToView(int tag, Vector2 point, MHView toView)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string pointJsonString = MHTools.ConvertVector2ToJSON(point);
			int viewTag = toView.tag;
			
			pointJsonString = _convertPointToView(tag, pointJsonString, viewTag);
			
			return MHTools.ConvertJSONToVector2(pointJsonString);
		}
		else
			return Vector2.zero;
	}
	
	[DllImport("__Internal")]
    private static extern string _convertPointFromView(int tag, string point, int fromView);
	public Vector2 convertPointFromView(int tag, Vector2 point, MHView fromView)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string pointJsonString = MHTools.ConvertVector2ToJSON(point);
			int viewTag = fromView.tag;
			
			pointJsonString = _convertPointFromView(tag, pointJsonString, viewTag);
			
			return MHTools.ConvertJSONToVector2(pointJsonString);
		}
		else
			return Vector2.zero;
	}
	
	[DllImport("__Internal")]
    private static extern string _convertRectToView(int tag, string rect, int toView);
	public Rect convertRectToView(int tag, Rect rect, MHView toView)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string rectJsonString = MHTools.ConvertRectToJSON(rect);
			int viewTag = toView.tag;
			
			rectJsonString = _convertRectToView(tag, rectJsonString, viewTag);
			
			return MHTools.ConvertJSONToRect(rectJsonString);
		}
		else
			return MHTools.RectZero;
	}
	
	[DllImport("__Internal")]
    private static extern string _convertRectFromView(int tag, string rect, int fromView);
	public Rect convertRectFromView(int tag, Rect rect, MHView fromView)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string rectJsonString = MHTools.ConvertRectToJSON(rect);
			int viewTag = fromView.tag;
			
			rectJsonString = _convertRectFromView(tag, rectJsonString, viewTag);
			
			return MHTools.ConvertJSONToRect(rectJsonString);
		}
		else
			return MHTools.RectZero;
	}
	
	[DllImport("__Internal")]
    private static extern bool _endEditing(int tag, bool force);
	public bool endEditing(int tag, bool force)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _endEditing(tag, force);
		}
		else
			return false;
	}
	#endregion
	
	#region view_xcode_input
	void animationDidStart(string jsonString)
	{
		int tag;
		string animationID;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			animationID = Convert.ToString(result["animationID"]);
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHView)
				(obj as MHView)._animationDidStart(animationID);
		}
	}
	
	void animationDidStop(string jsonString)
	{
		int tag;
		string animationID;
		MHObject context;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			animationID = Convert.ToString(result["animationID"]);
			context = GetObjectByUniqueTag(Convert.ToInt32(result["context"])) as MHObject;
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHView)
				(obj as MHView)._animationDidStop(animationID, context);
		}
	}
	
	void didAddSubview(string jsonString)
	{
		int tag;
		MHView subview;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			subview = GetObjectByUniqueTag(Convert.ToInt32(result["subview"])) as MHView;
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHView)
				(obj as MHView)._didAddSubview(subview);
		}
	}
	
	void willRemoveSubview(string jsonString)
	{
		int tag;
		MHView subview;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			subview = GetObjectByUniqueTag(Convert.ToInt32(result["subview"])) as MHView;
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHView)
				(obj as MHView)._willRemoveSubview(subview);
		}
	}
	
	void willMoveToSuperview(string jsonString)
	{
		int tag;
		MHView newSuperview;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			newSuperview = GetObjectByUniqueTag(Convert.ToInt32(result["newSuperview"])) as MHView;
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHView)
				(obj as MHView)._willMoveToSuperview(newSuperview);
		}
	}
	
	void didMoveToSuperview(string jsonString)
	{
		int tag;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHView)
				(obj as MHView)._didMoveToSuperview();
		}
	}
	#endregion
}
