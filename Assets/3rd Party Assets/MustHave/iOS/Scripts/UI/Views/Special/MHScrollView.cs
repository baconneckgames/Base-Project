using UnityEngine;
using System;
using System.Collections;

public class MHScrollView : MHView {
	#region functions
	public MHScrollView(){}
	
	public MHScrollView(bool autoInitialize)
	{
		if(Application.isPlaying && autoInitialize)
			init();
	}
	
	public MHScrollView(Rect aRect)
	{
		if(Application.isPlaying)
			initWithFrame(aRect);
	}
	
	new public MHScrollView init()
	{
		return MHiOSManager.Instance.init_scrollview(tag);
	}
	
	new public MHScrollView initWithFrame(Rect aRect)
	{
		return MHiOSManager.Instance.initWithFrame_scrollview(tag, aRect);
	}
	
	public void setContentOffset(Vector2 contentOffset, bool animated)
	{
		MHiOSManager.Instance.setContentOffset(tag, contentOffset, animated);
	}
	
	public Vector2 contentOffset {
		get {
			return MHiOSManager.Instance.contentOffset(tag);
		} set {
			MHiOSManager.Instance.contentOffset(tag, value);
		}
	}
	
	public Vector2 contentSize {
		get {
			return MHiOSManager.Instance.contentSize(tag);
		} set {
			MHiOSManager.Instance.contentSize(tag, value);
		}
	}
	
	public Vector4 contentInset {
		get {
			return MHiOSManager.Instance.contentInset(tag);
		} set {
			MHiOSManager.Instance.contentInset(tag, value);
		}
	}
	
	public bool scrollEnabled {
		get {
			return MHiOSManager.Instance.scrollEnabled(tag);
		} set {
			MHiOSManager.Instance.scrollEnabled(tag, value);
		}
	}
	
	public bool directionalLockEnabled {
		get {
			return MHiOSManager.Instance.directionalLockEnabled(tag);
		} set {
			MHiOSManager.Instance.directionalLockEnabled(tag, value);
		}
	}
	
	public bool scrollsToTop {
		get {
			return MHiOSManager.Instance.scrollsToTop(tag);
		} set {
			MHiOSManager.Instance.scrollsToTop(tag, value);
		}
	}
	
	public void scrollRectToVisible(Rect rect, bool animated)
	{
		
	}
	
	public bool pagingEnabled {
		get {
			return MHiOSManager.Instance.pagingEnabled(tag);
		} set {
			MHiOSManager.Instance.pagingEnabled(tag, value);
		}
	}
	
	public bool bounces {
		get {
			return MHiOSManager.Instance.bounces(tag);
		} set {
			MHiOSManager.Instance.bounces(tag, value);
		}
	}
	
	public bool alwaysBounceVertical {
		get {
			return MHiOSManager.Instance.alwaysBounceVertical(tag);
		} set {
			MHiOSManager.Instance.alwaysBounceVertical(tag, value);
		}
	}
	
	public bool alwaysBounceHorizontal {
		get {
			return MHiOSManager.Instance.alwaysBounceHorizontal(tag);
		} set {
			MHiOSManager.Instance.alwaysBounceHorizontal(tag, value);
		}
	}
   
	public float decelerationRate {
		get {
			return MHiOSManager.Instance.decelerationRate(tag);
		} set {
			MHiOSManager.Instance.decelerationRate(tag, value);
		}
	}
	
	public bool dragging {
		get {
			return MHiOSManager.Instance.dragging(tag);
		}
	}
	
	public bool tracking {
		get {
			return MHiOSManager.Instance.tracking(tag);
		}
	}
	
	public bool decelerating {
		get {
			return MHiOSManager.Instance.decelerating(tag);
		}
	}
	
	public MHScrollViewIndicatorStyle indicatorStyle {
		get {
			return MHiOSManager.Instance.indicatorStyle(tag);
		} set {
			MHiOSManager.Instance.indicatorStyle(tag, value);
		}
	}
	
	public Vector4 scrollIndicatorInsets {
		get {
			return MHiOSManager.Instance.scrollIndicatorInsets(tag);
		} set {
			MHiOSManager.Instance.scrollIndicatorInsets(tag, value);
		}
	}
	
	public bool showsHorizontalScrollIndicator {
		get {
			return MHiOSManager.Instance.showsHorizontalScrollIndicator(tag);
		} set {
			MHiOSManager.Instance.showsHorizontalScrollIndicator(tag, value);
		}
	}
	
	public bool showsVerticalScrollIndicator {
		get {
			return MHiOSManager.Instance.showsVerticalScrollIndicator(tag);
		} set {
			MHiOSManager.Instance.showsVerticalScrollIndicator(tag, value);
		}
	}
	
	public void flashScrollIndicators()
	{
		MHiOSManager.Instance.flashScrollIndicators(tag);
	}
	
	public void zoomToRect(Rect rect, bool animated)
	{
		MHiOSManager.Instance.zoomToRect(tag, rect, animated);
	}
	
	public float zoomScale {
		get {
			return MHiOSManager.Instance.zoomScale(tag);
		} set {
			MHiOSManager.Instance.zoomScale(tag, value);
		}
	}
	
	public void setZoomScale(float scale, bool animated)
	{
		MHiOSManager.Instance.setZoomScale(tag, scale, animated);
	}
	
	public float maximumZoomScale {
		get {
			return MHiOSManager.Instance.maximumZoomScale(tag);
		} set {
			MHiOSManager.Instance.maximumZoomScale(tag, value);
		}
	}
	
	public float minimumZoomScale {
		get {
			return MHiOSManager.Instance.minimumZoomScale(tag);
		} set {
			MHiOSManager.Instance.minimumZoomScale(tag, value);
		}
	}
	
	public bool zoomBouncing {
		get {
			return MHiOSManager.Instance.zoomBouncing(tag);
		}
	}
	
	public bool zooming {
		get {
			return MHiOSManager.Instance.zooming(tag);
		}
	}
	
	public bool bouncesZoom {
		get {
			return MHiOSManager.Instance.bouncesZoom(tag);
		} set {
			MHiOSManager.Instance.bouncesZoom(tag, value);
		}
	}
	#endregion
			
	#region delegate
	public event Action<MHScrollView> scrollViewDidScroll;
	public void _scrollViewDidScroll(MHScrollView scrollView)
	{
		if(scrollViewDidScroll != null)
			scrollViewDidScroll(scrollView);
	}
	
	public event Action<MHScrollView> scrollViewWillBeginDragging;
	public void _scrollViewWillBeginDragging(MHScrollView scrollView)
	{
		if(scrollViewWillBeginDragging != null)
			scrollViewWillBeginDragging(scrollView);
	}
	
	public event Action<MHScrollView, Vector2, Vector2> scrollViewWillEndDragging;
	public void _scrollViewWillEndDragging(MHScrollView scrollView, Vector2 velocity, Vector2 targetContentOffset)
	{
		if(scrollViewWillEndDragging != null)
			scrollViewWillEndDragging(scrollView, velocity, targetContentOffset);
	}

	public event Action<MHScrollView, bool> scrollViewDidEndDragging;
	public void _scrollViewDidEndDragging(MHScrollView scrollView, bool decelerate)
	{
		if(scrollViewDidEndDragging != null)
			scrollViewDidEndDragging(scrollView, decelerate);
	}
	
	public event Func<MHScrollView, bool> scrollViewShouldScrollToTop;
	public void _scrollViewShouldScrollToTop(MHScrollView scrollView)
	{
		if(scrollViewDidScrollToTop != null)
			MHTools.ReturnResultToXCode((scrollViewShouldScrollToTop(scrollView)).ToString());
		else
			MHTools.ReturnResultToXCode("true");
	}
	
	public event Action<MHScrollView> scrollViewDidScrollToTop;
	public void _scrollViewDidScrollToTop(MHScrollView scrollView)
	{
		if(scrollViewDidScrollToTop != null)
			scrollViewDidScrollToTop(scrollView);
	}
	
	public event Action<MHScrollView> scrollViewWillBeginDecelerating;
	public void _scrollViewWillBeginDecelerating(MHScrollView scrollView)
	{
		if(scrollViewWillBeginDecelerating != null)
			scrollViewWillBeginDecelerating(scrollView);
	}
	
	public event Action<MHScrollView> scrollViewDidEndDecelerating;
	public void _scrollViewDidEndDecelerating(MHScrollView scrollView)
	{
		if(scrollViewDidEndDecelerating != null)
			scrollViewDidEndDecelerating(scrollView);
	}
	
	public event Action<MHScrollView, MHView> scrollViewWillBeginZooming;
	public void _scrollViewWillBeginZooming(MHScrollView scrollView, MHView view)
	{
		if(scrollViewWillBeginZooming != null)
			scrollViewWillBeginZooming(scrollView, view);
	}
	
	public event Action<MHScrollView, MHView, float> scrollViewDidEndZooming;
	public void _scrollViewDidEndZooming(MHScrollView scrollView, MHView view, float scale)
	{
		if(scrollViewDidEndZooming != null)
			scrollViewDidEndZooming(scrollView, view, scale);
	}
	
	public event Action<MHScrollView> scrollViewDidZoom;
	public void _scrollViewDidZoom(MHScrollView scrollView)
	{
		if(scrollViewDidZoom != null)
			scrollViewDidZoom(scrollView);
	}
	
	public event Action<MHScrollView> scrollViewDidEndScrollingAnimation;
	public void _scrollViewDidEndScrollingAnimation(MHScrollView scrollView)
	{
		if(scrollViewDidEndScrollingAnimation != null)
			scrollViewDidEndScrollingAnimation(scrollView);
	}
	
	public event Func<MHScrollView, MHView> viewForZoomingInScrollView;
	public void _viewForZoomingInScrollView(MHScrollView scrollView)
	{
		if(viewForZoomingInScrollView != null)
			MHTools.ReturnResultToXCode(((viewForZoomingInScrollView(scrollView)).tag).ToString());
		else
			MHTools.ReturnResultToXCode(null);
	}
	#endregion
}
