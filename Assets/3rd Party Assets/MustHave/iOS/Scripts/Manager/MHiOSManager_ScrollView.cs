using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public partial class MHiOSManager : Singleton<MHiOSManager> {
	#region scrollview_xcode_output
	[DllImport("__Internal")]
    private static extern int _init_scrollview(int tag);
	public MHScrollView init_scrollview(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return GetObjectByUniqueTag(_init_scrollview(tag)) as MHScrollView;
		}
		else
			return new MHScrollView();
	}
	
	[DllImport("__Internal")]
    private static extern int _initWithFrame_scrollview(int tag, string aRect);
	public MHScrollView initWithFrame_scrollview(int tag, Rect aRect)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string rectJsonString = MHTools.ConvertRectToJSON(aRect);
			
			return GetObjectByUniqueTag(_initWithFrame_scrollview(tag, rectJsonString)) as MHScrollView;
		}
		else
			return new MHScrollView();
	}
	
	[DllImport("__Internal")]
    private static extern void _setContentOffset(int tag, string contentOffset, bool animated);
	public void setContentOffset(int tag, Vector2 contentOffset, bool animated)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string vectorJsonString = MHTools.ConvertVector2ToJSON(contentOffset);
			
			_setContentOffset(tag, vectorJsonString, animated);
		}
	}
	
	[DllImport("__Internal")]
    private static extern string _contentOffset(int tag);
	public Vector2 contentOffset(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string vectorJsonString = _contentOffset(tag);
			
			return MHTools.ConvertJSONToVector2(vectorJsonString);
		}
		else
			return Vector2.zero;
	}
	
	[DllImport("__Internal")]
    private static extern void _contentOffset_set(int tag, string offset);
	public void contentOffset(int tag, Vector2 offset)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string vectorJsonString = MHTools.ConvertVector2ToJSON(offset);
			
			_contentOffset_set(tag, vectorJsonString);
		}
	}
	
	[DllImport("__Internal")]
    private static extern string _contentSize(int tag);
	public Vector2 contentSize(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string vectorJsonString = _contentSize(tag);
			
			return MHTools.ConvertJSONToVector2(vectorJsonString);
		}
		else
			return Vector2.zero;
	}
	
	[DllImport("__Internal")]
    private static extern void _contentSize_set(int tag, string size);
	public void contentSize(int tag, Vector2 size)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string vectorJsonString = MHTools.ConvertVector2ToJSON(size);
			
			_contentSize_set(tag, vectorJsonString);
		}
	}
	
	[DllImport("__Internal")]
    private static extern string _contentInset(int tag);
	public Vector4 contentInset(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string vectorJsonString = _contentInset(tag);
			
			return MHTools.ConvertJSONToVector4(vectorJsonString);
		}
		else
			return Vector4.zero;
	}
	
	[DllImport("__Internal")]
    private static extern void _contentInset_set(int tag, string inset);
	public void contentInset(int tag, Vector4 inset)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string vectorJsonString = MHTools.ConvertVector4ToJSON(inset);
			
			_contentInset_set(tag, vectorJsonString);
		}
	}
	
	[DllImport("__Internal")]
    private static extern bool _scrollEnabled(int tag);
	public bool scrollEnabled(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _scrollEnabled(tag);
		}
		else
			return false;
	}
	
	[DllImport("__Internal")]
    private static extern void _scrollEnabled_set(int tag, bool enable);
	public void scrollEnabled(int tag, bool enable)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_scrollEnabled_set(tag, enable);
		}
	}
	
	[DllImport("__Internal")]
    private static extern bool _directionalLockEnabled(int tag);
	public bool directionalLockEnabled(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _directionalLockEnabled(tag);
		}
		else
			return false;
	}
	
	[DllImport("__Internal")]
    private static extern void _directionalLockEnabled_set(int tag, bool enable);
	public void directionalLockEnabled(int tag, bool enable)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_directionalLockEnabled_set(tag, enable);
		}
	}
	
	[DllImport("__Internal")]
    private static extern bool _scrollsToTop(int tag);
	public bool scrollsToTop(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _scrollsToTop(tag);
		}
		else
			return false;
	}
	
	[DllImport("__Internal")]
    private static extern void _scrollsToTop_set(int tag, bool scrolls);
	public void scrollsToTop(int tag, bool scrolls)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_scrollsToTop_set(tag, scrolls);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _scrollRectToVisible(int tag, string rect, bool animated);
	public void scrollRectToVisible(int tag, Rect rect, bool animated)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string rectJsonString = MHTools.ConvertRectToJSON(rect);
			
			_scrollRectToVisible(tag, rectJsonString, animated);
		}
	}
	
	[DllImport("__Internal")]
    private static extern bool _pagingEnabled(int tag);
	public bool pagingEnabled(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _pagingEnabled(tag);
		}
		else
			return false;
	}
	
	[DllImport("__Internal")]
    private static extern void _pagingEnabled_set(int tag, bool enable);
	public void pagingEnabled(int tag, bool enable)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_pagingEnabled_set(tag, enable);
		}
	}
	
	[DllImport("__Internal")]
    private static extern bool _bounces(int tag);
	public bool bounces(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _bounces(tag);
		}
		else
			return false;
	}
	
	[DllImport("__Internal")]
    private static extern void _bounces_set(int tag, bool bounce);
	public void bounces(int tag, bool bounce)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_bounces_set(tag, bounce);
		}
	}
	
	[DllImport("__Internal")]
    private static extern bool _alwaysBounceVertical(int tag);
	public bool alwaysBounceVertical(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _alwaysBounceVertical(tag);
		}
		else
			return false;
	}
	
	[DllImport("__Internal")]
    private static extern void _alwaysBounceVertical_set(int tag, bool always);
	public void alwaysBounceVertical(int tag, bool always)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_alwaysBounceVertical_set(tag, always);
		}
	}
	
	[DllImport("__Internal")]
    private static extern bool _alwaysBounceHorizontal(int tag);
	public bool alwaysBounceHorizontal(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _alwaysBounceHorizontal(tag);
		}
		else
			return false;
	}
	
	[DllImport("__Internal")]
    private static extern void _alwaysBounceHorizontal_set(int tag, bool always);
	public void alwaysBounceHorizontal(int tag, bool always)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_alwaysBounceHorizontal_set(tag, always);
		}
	}
   
	[DllImport("__Internal")]
    private static extern float _decelerationRate(int tag);
	public float decelerationRate(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _decelerationRate(tag);
		}
		else
			return 0f;
	}
	
	[DllImport("__Internal")]
    private static extern void _decelerationRate_set(int tag, float rate);
	public void decelerationRate(int tag, float rate)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_decelerationRate_set(tag, rate);
		}
	}
	
	[DllImport("__Internal")]
    private static extern bool _dragging(int tag);
	public bool dragging(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _dragging(tag);
		}
		else
			return false;
	}
	
	[DllImport("__Internal")]
    private static extern bool _tracking(int tag);
	public bool tracking(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _tracking(tag);
		}
		else
			return false;
	}
	
	[DllImport("__Internal")]
    private static extern bool _decelerating(int tag);
	public bool decelerating(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _decelerating(tag);
		}
		else
			return false;
	}
	
	[DllImport("__Internal")]
    private static extern int _indicatorStyle(int tag);
	public MHScrollViewIndicatorStyle indicatorStyle(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return (MHScrollViewIndicatorStyle)_indicatorStyle(tag);
		}
		else
			return MHScrollViewIndicatorStyle.MHScrollViewIndicatorStyleDefault;
	}
	
	[DllImport("__Internal")]
    private static extern void _indicatorStyle_set(int tag, int style);
	public void indicatorStyle(int tag, MHScrollViewIndicatorStyle style)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_indicatorStyle_set(tag, (int)style);
		}
	}
	
	[DllImport("__Internal")]
    private static extern string _scrollIndicatorInsets(int tag);
	public Vector4 scrollIndicatorInsets(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string vectorJsonString = _scrollIndicatorInsets(tag);
			
			return MHTools.ConvertJSONToVector4(vectorJsonString);
		}
		else
			return Vector4.zero;
	}
	
	[DllImport("__Internal")]
    private static extern void _scrollIndicatorInsets_set(int tag, string insets);
	public void scrollIndicatorInsets(int tag, Vector4 insets)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string vectorJsonString = MHTools.ConvertVector4ToJSON(insets);
			
			_scrollIndicatorInsets_set(tag, vectorJsonString);
		}
	}
	
	[DllImport("__Internal")]
    private static extern bool _showsHorizontalScrollIndicator(int tag);
	public bool showsHorizontalScrollIndicator(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _showsHorizontalScrollIndicator(tag);
		}
		else
			return false;
	}
	
	[DllImport("__Internal")]
    private static extern void _showsHorizontalScrollIndicator_set(int tag, bool shows);
	public void showsHorizontalScrollIndicator(int tag, bool shows)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_showsHorizontalScrollIndicator_set(tag, shows);
		}
	}
	
	[DllImport("__Internal")]
    private static extern bool _showsVerticalScrollIndicator(int tag);
	public bool showsVerticalScrollIndicator(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _showsVerticalScrollIndicator(tag);
		}
		else
			return false;
	}
	
	[DllImport("__Internal")]
    private static extern void _showsVerticalScrollIndicator_set(int tag, bool shows);
	public void showsVerticalScrollIndicator(int tag, bool shows)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_showsVerticalScrollIndicator_set(tag, shows);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _flashScrollIndicators(int tag);
	public void flashScrollIndicators(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_flashScrollIndicators(tag);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _zoomToRect(int tag, string rect, bool animated);
	public void zoomToRect(int tag, Rect rect, bool animated)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string rectJsonString = MHTools.ConvertRectToJSON(rect);
			
			_zoomToRect(tag, rectJsonString, animated);
		}
	}
	
	[DllImport("__Internal")]
    private static extern float _zoomScale(int tag);
	public float zoomScale(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _zoomScale(tag);
		}
		else
			return 0f;
	}
	
	[DllImport("__Internal")]
    private static extern void _zoomScale_set(int tag, float scale);
	public void zoomScale(int tag, float scale)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_zoomScale_set(tag, scale);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _setZoomScale(int tag, float scale, bool animated);
	public void setZoomScale(int tag, float scale, bool animated)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_setZoomScale(tag, scale, animated);
		}
	}
	
	[DllImport("__Internal")]
    private static extern float _maximumZoomScale(int tag);
	public float maximumZoomScale(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _maximumZoomScale(tag);
		}
		else
			return 0f;
	}
	
	[DllImport("__Internal")]
    private static extern void _maximumZoomScale_set(int tag, float scale);
	public void maximumZoomScale(int tag, float scale)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_maximumZoomScale_set(tag, scale);
		}
	}
	
	[DllImport("__Internal")]
    private static extern float _minimumZoomScale(int tag);
	public float minimumZoomScale(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _minimumZoomScale(tag);
		}
		else
			return 0f;
	}
	
	[DllImport("__Internal")]
    private static extern void _minimumZoomScale_set(int tag, float scale);
	public void minimumZoomScale(int tag, float scale)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_minimumScaleFactor_set(tag, scale);
		}
	}
	
	[DllImport("__Internal")]
    private static extern bool _zoomBouncing(int tag);
	public bool zoomBouncing(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _zoomBouncing(tag);
		}
		else
			return false;
	}
	
	[DllImport("__Internal")]
    private static extern bool _zooming(int tag);
	public bool zooming(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _zooming(tag);
		}
		else
			return false;
	}
	
	[DllImport("__Internal")]
    private static extern bool _bouncesZoom(int tag);
	public bool bouncesZoom(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _bouncesZoom(tag);
		}
		else
			return false;
	}
	
	[DllImport("__Internal")]
    private static extern void _bouncesZoom_set(int tag, bool bounce);
	public void bouncesZoom(int tag, bool bounce)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_bouncesZoom_set(tag, bounce);
		}
	}
	#endregion
	
	#region scrollview_xcode_input
	void scrollViewDidScroll(string jsonString)
	{
		int tag;
		MHScrollView scrollView;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			scrollView = GetObjectByUniqueTag(Convert.ToInt32(result["scrollView"])) as MHScrollView;
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHScrollView)
				(obj as MHScrollView)._scrollViewDidScroll(scrollView);
		}
	}
	
	void scrollViewWillBeginDragging(string jsonString)
	{
		int tag;
		MHScrollView scrollView;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			scrollView = GetObjectByUniqueTag(Convert.ToInt32(result["scrollView"])) as MHScrollView;
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHScrollView)
				(obj as MHScrollView)._scrollViewWillBeginDragging(scrollView);
		}
	}
	
	void scrollViewWillEndDragging(string jsonString)
	{
		int tag;
		MHScrollView scrollView;
		Vector2 velocity;
		Vector2 targetContentOffset;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			scrollView = GetObjectByUniqueTag(Convert.ToInt32(result["scrollView"])) as MHScrollView;
			velocity = MHTools.ConvertJSONToVector2(Convert.ToString(result["velocity"]));
			targetContentOffset = MHTools.ConvertJSONToVector2(Convert.ToString(result["targetContentOffset"]));
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHScrollView)
				(obj as MHScrollView)._scrollViewWillEndDragging(scrollView, velocity, targetContentOffset);
		}
	}
	
	void scrollViewDidEndDragging(string jsonString)
	{
		int tag;
		MHScrollView scrollView;
		bool decelerate;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			scrollView = GetObjectByUniqueTag(Convert.ToInt32(result["scrollView"])) as MHScrollView;
			decelerate = Convert.ToBoolean(result["decelerate"]);
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHScrollView)
				(obj as MHScrollView)._scrollViewDidEndDragging(scrollView, decelerate);
		}
	}
	
	void scrollViewShouldScrollToTop(string jsonString)
	{
		int tag;
		MHScrollView scrollView;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			scrollView = GetObjectByUniqueTag(Convert.ToInt32(result["scrollView"])) as MHScrollView;
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHScrollView)
				(obj as MHScrollView)._scrollViewShouldScrollToTop(scrollView);
		}
	}
	
	void scrollViewDidScrollToTop(string jsonString)
	{
		int tag;
		MHScrollView scrollView;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			scrollView = GetObjectByUniqueTag(Convert.ToInt32(result["scrollView"])) as MHScrollView;
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHScrollView)
				(obj as MHScrollView)._scrollViewDidScrollToTop(scrollView);
		}
	}
	
	void scrollViewWillBeginDecelerating(string jsonString)
	{
		int tag;
		MHScrollView scrollView;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			scrollView = GetObjectByUniqueTag(Convert.ToInt32(result["scrollView"])) as MHScrollView;
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHScrollView)
				(obj as MHScrollView)._scrollViewWillBeginDecelerating(scrollView);
		}
	}
	
	void scrollViewDidEndDecelerating(string jsonString)
	{
		int tag;
		MHScrollView scrollView;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			scrollView = GetObjectByUniqueTag(Convert.ToInt32(result["scrollView"])) as MHScrollView;
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHScrollView)
				(obj as MHScrollView)._scrollViewDidEndDecelerating(scrollView);
		}
	}
	
	void scrollViewWillBeginZooming(string jsonString)
	{
		int tag;
		MHScrollView scrollView;
		MHView view;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			scrollView = GetObjectByUniqueTag(Convert.ToInt32(result["scrollView"])) as MHScrollView;
			view = GetObjectByUniqueTag(Convert.ToInt32(result["view"])) as MHView;
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHScrollView)
				(obj as MHScrollView)._scrollViewWillBeginZooming(scrollView, view);
		}
	}
	
	void scrollViewDidEndZooming(string jsonString)
	{
		int tag;
		MHScrollView scrollView;
		MHView view;
		float scale;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			scrollView = GetObjectByUniqueTag(Convert.ToInt32(result["scrollView"])) as MHScrollView;
			view = GetObjectByUniqueTag(Convert.ToInt32(result["view"])) as MHView;
			scale = Convert.ToSingle(result["scale"]);
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHScrollView)
				(obj as MHScrollView)._scrollViewDidEndZooming(scrollView, view, scale);
		}
	}
	
	void scrollViewDidZoom(string jsonString)
	{
		int tag;
		MHScrollView scrollView;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			scrollView = GetObjectByUniqueTag(Convert.ToInt32(result["scrollView"])) as MHScrollView;
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHScrollView)
				(obj as MHScrollView)._scrollViewDidZoom(scrollView);
		}
	}
	
	void scrollViewDidEndScrollingAnimation(string jsonString)
	{
		int tag;
		MHScrollView scrollView;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			scrollView = GetObjectByUniqueTag(Convert.ToInt32(result["scrollView"])) as MHScrollView;
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHScrollView)
				(obj as MHScrollView)._scrollViewDidEndScrollingAnimation(scrollView);
		}
	}
	
	void viewForZoomingInScrollView(string jsonString)
	{
		int tag;
		MHScrollView scrollView;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			scrollView = GetObjectByUniqueTag(Convert.ToInt32(result["scrollView"])) as MHScrollView;
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHScrollView)
				(obj as MHScrollView)._viewForZoomingInScrollView(scrollView);
		}
	}
	#endregion
}