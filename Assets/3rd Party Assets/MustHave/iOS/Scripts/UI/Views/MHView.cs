using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class MHView : MHObject {
	#region functions
	public MHView(){}
	
	public MHView(bool autoInitialize)
	{
		if(Application.isPlaying && autoInitialize)
			init();
	}
	
	public MHView(Rect aRect)
	{
		if(Application.isPlaying)
			initWithFrame(aRect);
	}
	
	new public MHView init()
	{
		return MHiOSManager.Instance.init_view(tag);
	}
	
	public MHView initWithFrame(Rect aRect)
	{
		return MHiOSManager.Instance.initWithFrame(tag, aRect);
	}
	
	public Color backgroundColor {
		get {
			return MHiOSManager.Instance.backgroundColor(tag);
		} set {
			MHiOSManager.Instance.backgroundColor(tag, value);
		}
	}

	public bool hidden {
		get {
			return MHiOSManager.Instance.hidden(tag);
		} set {
			MHiOSManager.Instance.hidden(tag, value);
		}
	}
	
	public float alpha {
		get {
			return MHiOSManager.Instance.alpha(tag);
		} set {
			MHiOSManager.Instance.alpha(tag, value);
		}
	}

	public bool opaque {
		get {
			return MHiOSManager.Instance.opaque(tag);
		} set {
			MHiOSManager.Instance.opaque(tag, value);
		}
	}
	
	public bool clipsToBounds {
		get {
			return MHiOSManager.Instance.clipsToBounds(tag);
		} set {
			MHiOSManager.Instance.clipsToBounds(tag, value);
		}
	}
	
	public bool clearsContextBeforeDrawing {
		get {
			return MHiOSManager.Instance.clearsContextBeforeDrawing(tag);
		} set {
			MHiOSManager.Instance.clearsContextBeforeDrawing(tag, value);
		}
	}
	
	public virtual bool userInteractionEnabled {
		get {
			return MHiOSManager.Instance.userInteractionEnabled(tag);
		} set {
			MHiOSManager.Instance.userInteractionEnabled(tag, value);
		}
	}
	
	public bool multipleTouchEnabled {
		get {
			return MHiOSManager.Instance.multipleTouchEnabled(tag);
		} set {
			MHiOSManager.Instance.multipleTouchEnabled(tag, value);
		}
	}
	
	public bool exclusiveTouch {
		get {
			return MHiOSManager.Instance.exclusiveTouch(tag);
		} set {
			MHiOSManager.Instance.exclusiveTouch(tag, value);
		}
	}
	
	public Rect frame {
		get {
			return MHiOSManager.Instance.frame(tag);
		} set {
			MHiOSManager.Instance.frame(tag, value);
		}
	}
	
	public Rect bounds {
		get {
			return MHiOSManager.Instance.bounds(tag);
		} set {
			MHiOSManager.Instance.bounds(tag, value);
		}
	}
	
	public Vector2 center {
		get {
			return MHiOSManager.Instance.center(tag);
		} set {
			MHiOSManager.Instance.center(tag, value);
		}
	}

	public MHView superview {
		get {
			return MHiOSManager.Instance.superview(tag);
		}
	}
	
	public MHView[] subviews {
		get {
			return MHiOSManager.Instance.subviews(tag);
		}
	}

	public void addSubview(MHView subview)
	{
		MHiOSManager.Instance.addSubview(tag, subview);
	}
	
	public void bringSubviewToFront(MHView subview)
	{
		MHiOSManager.Instance.bringSubviewToFront(tag, subview);
	}
	
	public void sendSubviewToBack(MHView subview)
	{
		MHiOSManager.Instance.sendSubviewToBack(tag, subview);
	}
	
	public void removeFromSuperview()
	{
		MHiOSManager.Instance.removeFromSuperview(tag);
	}
	
	public void insertSubviewAtIndex(MHView view, int index)
	{
		MHiOSManager.Instance.insertSubviewAtIndex(tag, view, index);
	}
	
	public void insertSubviewAboveSubview(MHView view, MHView siblingSubview)
	{
		MHiOSManager.Instance.insertSubviewAboveSubview(tag, view, siblingSubview);
	}
	
	public void insertSubviewBelowSubview(MHView view, MHView siblingSubview)
	{
		MHiOSManager.Instance.insertSubviewBelowSubview(tag, view, siblingSubview);
	}
	
	public void exchangeSubviewAtIndex(int index1, int index2)
	{
		MHiOSManager.Instance.exchangeSubviewAtIndex(tag, index1, index2);
	}
	
	public bool isDescendantOfView(MHView view)
	{
		return MHiOSManager.Instance.isDescendantOfView(tag, view);
	}
	
	public MHViewAutoresizing autoresizingMask {
		get {
			return MHiOSManager.Instance.autoresizingMask(tag);
		} set {
			MHiOSManager.Instance.autoresizingMask(tag, value);
		}
	}
	
	public bool autoresizesSubviews {
		get {
			return MHiOSManager.Instance.autoresizesSubviews(tag);
		} set {
			MHiOSManager.Instance.autoresizesSubviews(tag, value);
		}
	}
	
	public MHViewContentMode contentMode {
		get {
			return MHiOSManager.Instance.contentMode(tag);
		} set {
			MHiOSManager.Instance.contentMode(tag, value);
		}
	}
	
	public Vector2 sizeThatFits(Vector2 size)
	{
		return MHiOSManager.Instance.sizeThatFits(tag, size);
	}
	
	public void sizeToFit()
	{
		MHiOSManager.Instance.sizeToFit(tag);
	}

	public void layoutSubviews()
	{
		MHiOSManager.Instance.layoutSubviews(tag);
	}
	
	public void setNeedsLayout()
	{
		MHiOSManager.Instance.setNeedsLayout(tag);
	}
	
	public void layoutIfNeeded()
	{
		MHiOSManager.Instance.layoutIfNeeded(tag);
	}
	
	public void drawRect(Rect rect)
	{
		MHiOSManager.Instance.drawRect(tag, rect);
	}
	
	public void setNeedsDisplay()
	{
		MHiOSManager.Instance.setNeedsDisplay(tag);
	}
	
	public void setNeedsDisplayInRect(Rect invalidRect)
	{
		MHiOSManager.Instance.setNeedsDisplayInRect(tag, invalidRect);
	}
	
	public float contentScaleFactor {
		get {
			return MHiOSManager.Instance.contentScaleFactor(tag);
		} set {
			MHiOSManager.Instance.contentScaleFactor(tag, value);
		}
	}
	/*
	public void addGestureRecognizer(MHGestureRecognizer gestureRecognizer)
	{
		//To Be Supported Soon
	}
	
	public void removeGestureRecognizer(MHGestureRecognizer gestureRecognizer)
	{
		//To Be Supported Soon
	}
	
	public MHGestureRecognizer[] gestureRecognizers {
		//To Be Supported Soon
		get {
			return MHiOSManager.Instance.gestureRecognizers(tag);
		} set {
			MHiOSManager.Instance.gestureRecognizers(tag, value);
		}
	}
	
	public bool gestureRecognizerShouldBegin(MHGestureRecognizer gestureRecognizer)
	{
		//To Be Supported Soon
		return false;
	}
	*/
	public void animateWithDuration(float duration, float delay, MHViewAnimationOptions options, Action<bool> completion)
	{
		MHiOSManager.Instance.animateWithDuration(tag, duration, delay, options, completion);
	}
	
	public void animateWithDuration(float duration, Action<bool> completion)
	{
		MHiOSManager.Instance.animateWithDuration(tag, duration, completion);
	}
	
	public void animateWithDuration(float duration)
	{
		MHiOSManager.Instance.animateWithDuration(tag, duration);
	}

	public void transitionWithView(MHView view, float duration, MHViewAnimationOptions options, Action<bool> completion)
	{
		MHiOSManager.Instance.transitionWithView(tag, view, duration, options, completion);
	}
	
	public void transitionFromView(MHView fromView, MHView toView, float duration, MHViewAnimationOptions options, Action<bool> completion)
	{
		MHiOSManager.Instance.transitionFromView(tag, fromView, toView, duration, options, completion);
	}
	
	public void beginAnimations(string animationID, MHObject context)
	{
		MHiOSManager.Instance.beginAnimations(tag, animationID, context);
	}
	
	public void commitAnimations()
	{
		MHiOSManager.Instance.commitAnimations(tag);
	}
	
	public void setAnimationStartDate(DateTime dte)
	{
		MHiOSManager.Instance.setAnimationStartDate(tag, dte);
	}
	
	public void setAnimationsEnabled(bool enable)
	{
		MHiOSManager.Instance.setAnimationsEnabled(tag, enable);
	}
	
	public void setAnimationDuration(float duration)
	{
		MHiOSManager.Instance.setAnimationDuration(tag, duration);
	}
	
	public void setAnimationDelay(float delay)
	{
		MHiOSManager.Instance.setAnimationDelay(tag, delay);
	}
	
	public void setAnimationCurve(MHViewAnimationCurve curve)
	{
		MHiOSManager.Instance.setAnimationCurve(tag, curve);
	}
	
	public void setAnimationRepeatCount(float count)
	{
		MHiOSManager.Instance.setAnimationRepeatCount(tag, count);
	}
	
	public void setAnimationRepeatAutoreverses(bool repeatAutoreverses)
	{
		MHiOSManager.Instance.setAnimationRepeatAutoreverses(tag, repeatAutoreverses);
	}
	
	public void setAnimationBeginsFromCurrentState(bool fromCurrentState)
	{
		MHiOSManager.Instance.setAnimationBeginsFromCurrentState(tag, fromCurrentState);
	}
	
	public void setAnimationTransition(MHViewAnimationTransition transition, MHView view, bool cache)
	{
		MHiOSManager.Instance.setAnimationTransition(tag, transition, view, cache);
	}
	
	public bool areAnimationsEnabled()
	{
		return MHiOSManager.Instance.areAnimationsEnabled(tag);
	}

	/*
	public int tag {
		get {
			return MHiOSManager.Instance.tag(tag);
		} set {
			MHiOSManager.Instance.tag(tag, value);
		}
	}
	*/
	public MHView viewWithTag(int tagOfView)
	{
		return MHiOSManager.Instance.viewWithTag(tag, tagOfView);
	}
	
	public Vector2 convertPointToView(Vector2 point, MHView toView)
	{
		return MHiOSManager.Instance.convertPointToView(tag, point, toView);
	}
	
	public Vector2 convertPointFromView(Vector2 point, MHView fromView)
	{
		return MHiOSManager.Instance.convertPointFromView(tag, point, fromView);
	}
	
	public Rect convertRectToView(Rect rect, MHView toView)
	{
		return MHiOSManager.Instance.convertRectToView(tag, rect, toView);
	}
	
	public Rect convertRectFromView(Rect rect, MHView fromView)
	{
		return MHiOSManager.Instance.convertRectFromView(tag, rect, fromView);
	}
	
	public bool endEditing(bool force)
	{
		return MHiOSManager.Instance.endEditing(tag, force);
	}
	#endregion
	
	#region callbacks
	public event Action<string> animationDidStart;
	public void _animationDidStart(string animationID)
	{
		if(animationDidStart != null)
			animationDidStart(animationID);
	}
	
	public event Action<string, MHObject> animationDidStop;
	public void _animationDidStop(string animationID, MHObject context)
	{
		if(animationDidStop != null)
			animationDidStop(animationID, context);
	}
	
	public event Action<MHView> didAddSubview;
	public void _didAddSubview(MHView subview)
	{
		if(didAddSubview != null)
			didAddSubview(subview);
	}
	
	public event Action<MHView> willRemoveSubview;
	public void _willRemoveSubview(MHView subview)
	{
		if(willRemoveSubview != null)
			willRemoveSubview(subview);
	}
	
	public event Action<MHView> willMoveToSuperview;
	public void _willMoveToSuperview(MHView subview)
	{
		if(willMoveToSuperview != null)
			willMoveToSuperview(subview);
	}
	
	public event Action didMoveToSuperview;
	public void _didMoveToSuperview()
	{
		if(didMoveToSuperview != null)
			didMoveToSuperview();
	}
	#endregion
}
