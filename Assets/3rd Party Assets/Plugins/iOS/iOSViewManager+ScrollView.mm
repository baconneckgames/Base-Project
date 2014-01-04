//
//  iOSViewManager+ScrollView.m
//  Unity-iPhone
//
//  Created by Matthew Scuderi on 9/12/13.
//
//

#import "iOSViewManager+ScrollView.h"
#define CANNOT_FIND_ERROR @"ERROR: Cannot find UIScrollView"

@implementation iOSViewManager (ScrollView)

@end

extern "C"
{
#pragma mark - scrollview_xcode_output
	int _init_scrollview(int tag)
	{
		if(![MHTools objectExists:tag])
        {
            ALBScrollView *scrollview = [[[ALBScrollView alloc] init] autorelease];
            scrollview.delegate = scrollview;
            
            [MHTools addreplaceObject:scrollview key:tag actualUI:scrollview];
        }
        else
        {
            NSLog(@"ERROR: This tag is already taken, cannot initiate this object (SCROLLVIEW)");
        }
        return tag;
	}
	
	int _initWithFrame_scrollview(int tag, const char*  aRect)
	{
		if(![MHTools objectExists:tag])
        {
            CGRect frame = [MHTools convertRectToCGRect:aRect];
            ALBScrollView *scrollview = [[[ALBScrollView alloc] initWithFrame:frame] autorelease];
            scrollview.delegate = scrollview;
            
            [MHTools addreplaceObject:scrollview key:tag actualUI:scrollview];
        }
        else
        {
            NSLog(@"ERROR: This tag is already taken, cannot initiate this object (SCROLLVIEW)");
        }
        return tag;
	}
	
	void _setContentOffset(int tag, const char*  contentOffset, bool animated)
	{
		UIScrollView *scrollview = [MHTools getActualObject:tag];
        if(scrollview)
        {
            [scrollview setContentOffset:[MHTools convertVector2ToCGPoint:contentOffset] animated:animated];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	const char*  _contentOffset(int tag)
	{
        UIScrollView *scrollview = [MHTools getActualObject:tag];
        if(scrollview)
        {
            return [MHTools convertCGPointToVector2:scrollview.contentOffset];
        }
        
        NSLog(CANNOT_FIND_ERROR);
		return "";
	}
	
	void _contentOffset_set(int tag, const char*  offset)
	{
		UIScrollView *scrollview = [MHTools getActualObject:tag];
        if(scrollview)
        {
            scrollview.contentOffset = [MHTools convertVector2ToCGPoint:offset];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	const char* _contentSize(int tag)
	{
		UIScrollView *scrollview = [MHTools getActualObject:tag];
        if(scrollview)
        {
            return [MHTools convertCGSizeToVector2:scrollview.contentSize];
        }
        
        NSLog(CANNOT_FIND_ERROR);
		return "";
	}
	
	void _contentSize_set(int tag, const char*  size)
	{
		UIScrollView *scrollview = [MHTools getActualObject:tag];
        if(scrollview)
        {
            scrollview.contentSize = [MHTools convertVector2ToCGSize:size];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	const char*  _contentInset(int tag)
	{
		UIScrollView *scrollview = [MHTools getActualObject:tag];
        if(scrollview)
        {
            return [MHTools convertUIEdgeInsetToVector4:scrollview.contentInset];
        }
        
        NSLog(CANNOT_FIND_ERROR);
		return "";
	}
	
	void _contentInset_set(int tag, const char* inset)
	{
		UIScrollView *scrollview = [MHTools getActualObject:tag];
        if(scrollview)
        {
            scrollview.contentInset = [MHTools convertVector4ToUIEdgeInset:inset];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	bool _scrollEnabled(int tag)
	{
		UIScrollView *scrollview = [MHTools getActualObject:tag];
        if(scrollview)
        {
            return scrollview.scrollEnabled;
        }
        
        NSLog(CANNOT_FIND_ERROR);
		return false;
	}
	
	void _scrollEnabled_set(int tag, bool enable)
	{
		UIScrollView *scrollview = [MHTools getActualObject:tag];
        if(scrollview)
        {
            scrollview.scrollEnabled = enable;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	bool _directionalLockEnabled(int tag)
	{
		UIScrollView *scrollview = [MHTools getActualObject:tag];
        if(scrollview)
        {
            return scrollview.directionalLockEnabled;
        }
        
        NSLog(CANNOT_FIND_ERROR);
		return false;
	}
	
	void _directionalLockEnabled_set(int tag, bool enable)
	{
		UIScrollView *scrollview = [MHTools getActualObject:tag];
        if(scrollview)
        {
            scrollview.directionalLockEnabled = enable;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	bool _scrollsToTop(int tag)
	{
		UIScrollView *scrollview = [MHTools getActualObject:tag];
        if(scrollview)
        {
            return scrollview.scrollsToTop;
        }
        
        NSLog(CANNOT_FIND_ERROR);
		return false;
	}
	
	void _scrollsToTop_set(int tag, bool scrolls)
	{
		UIScrollView *scrollview = [MHTools getActualObject:tag];
        if(scrollview)
        {
            scrollview.scrollsToTop = scrolls;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _scrollRectToVisible(int tag, const char*  rect, bool animated)
	{
		UIScrollView *scrollview = [MHTools getActualObject:tag];
        if(scrollview)
        {
            [scrollview scrollRectToVisible:[MHTools convertRectToCGRect:rect] animated:animated];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	bool _pagingEnabled(int tag)
	{
		UIScrollView *scrollview = [MHTools getActualObject:tag];
        if(scrollview)
        {
            return scrollview.pagingEnabled;
        }
        
        NSLog(CANNOT_FIND_ERROR);
		return false;
	}
	
	void _pagingEnabled_set(int tag, bool enable)
	{
		UIScrollView *scrollview = [MHTools getActualObject:tag];
        if(scrollview)
        {
            scrollview.pagingEnabled = enable;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	bool _bounces(int tag)
	{
		UIScrollView *scrollview = [MHTools getActualObject:tag];
        if(scrollview)
        {
            return scrollview.bounces;
        }
        
        NSLog(CANNOT_FIND_ERROR);
		return false;
	}
	
	void _bounces_set(int tag, bool bounce)
	{
		UIScrollView *scrollview = [MHTools getActualObject:tag];
        if(scrollview)
        {
            scrollview.bounces = bounce;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	bool _alwaysBounceVertical(int tag)
	{
		UIScrollView *scrollview = [MHTools getActualObject:tag];
        if(scrollview)
        {
            return scrollview.alwaysBounceVertical;
        }
        
        NSLog(CANNOT_FIND_ERROR);
		return false;
	}
	
	void _alwaysBounceVertical_set(int tag, bool always)
	{
		UIScrollView *scrollview = [MHTools getActualObject:tag];
        if(scrollview)
        {
            scrollview.alwaysBounceVertical = always;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	bool _alwaysBounceHorizontal(int tag)
	{
		UIScrollView *scrollview = [MHTools getActualObject:tag];
        if(scrollview)
        {
            return scrollview.alwaysBounceHorizontal;
        }
        
        NSLog(CANNOT_FIND_ERROR);
		return false;
	}
	
	void _alwaysBounceHorizontal_set(int tag, bool always)
	{
		UIScrollView *scrollview = [MHTools getActualObject:tag];
        if(scrollview)
        {
            scrollview.alwaysBounceHorizontal = always;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
    
	float _decelerationRate(int tag)
	{
		UIScrollView *scrollview = [MHTools getActualObject:tag];
        if(scrollview)
        {
            return scrollview.decelerationRate;
        }
        
        NSLog(CANNOT_FIND_ERROR);
		return 0.0f;
	}
	
	void _decelerationRate_set(int tag, float rate)
	{
		
	}
	
	bool _dragging(int tag)
	{
		UIScrollView *scrollview = [MHTools getActualObject:tag];
        if(scrollview)
        {
            return scrollview.dragging;
        }
        
        NSLog(CANNOT_FIND_ERROR);
		return false;
	}
	
    bool _tracking(int tag)
	{
		UIScrollView *scrollview = [MHTools getActualObject:tag];
        if(scrollview)
        {
            return scrollview.tracking;
        }
        
        NSLog(CANNOT_FIND_ERROR);
		return false;
	}
	
	bool _decelerating(int tag)
	{
		UIScrollView *scrollview = [MHTools getActualObject:tag];
        if(scrollview)
        {
            return scrollview.decelerating;
        }
        
        NSLog(CANNOT_FIND_ERROR);
		return false;
	}
	
	int _indicatorStyle(int tag)
	{
		UIScrollView *scrollview = [MHTools getActualObject:tag];
        if(scrollview)
        {
            return (int)scrollview.indicatorStyle;
        }
        
        NSLog(CANNOT_FIND_ERROR);
		return (int)UIScrollViewIndicatorStyleDefault;
	}
	
	void _indicatorStyle_set(int tag, int style)
	{
		UIScrollView *scrollview = [MHTools getActualObject:tag];
        if(scrollview)
        {
            scrollview.indicatorStyle = (UIScrollViewIndicatorStyle)style;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	const char* _scrollIndicatorInsets(int tag)
	{
		UIScrollView *scrollview = [MHTools getActualObject:tag];
        if(scrollview)
        {
            return [MHTools convertUIEdgeInsetToVector4:scrollview.scrollIndicatorInsets];
        }
        
        NSLog(CANNOT_FIND_ERROR);
		return "";
	}
	
	void _scrollIndicatorInsets_set(int tag, const char* insets)
	{
		UIScrollView *scrollview = [MHTools getActualObject:tag];
        if(scrollview)
        {
            scrollview.scrollIndicatorInsets = [MHTools convertVector4ToUIEdgeInset:insets];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	bool _showsHorizontalScrollIndicator(int tag)
	{
		UIScrollView *scrollview = [MHTools getActualObject:tag];
        if(scrollview)
        {
            return scrollview.showsHorizontalScrollIndicator;
        }
        
        NSLog(CANNOT_FIND_ERROR);
		return false;
	}
	
	void _showsHorizontalScrollIndicator_set(int tag, bool shows)
	{
		UIScrollView *scrollview = [MHTools getActualObject:tag];
        if(scrollview)
        {
            scrollview.showsHorizontalScrollIndicator = shows;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	bool _showsVerticalScrollIndicator(int tag)
	{
		UIScrollView *scrollview = [MHTools getActualObject:tag];
        if(scrollview)
        {
            return scrollview.showsVerticalScrollIndicator;
        }
        
        NSLog(CANNOT_FIND_ERROR);
		return false;
	}
	
	void _showsVerticalScrollIndicator_set(int tag, bool shows)
	{
		UIScrollView *scrollview = [MHTools getActualObject:tag];
        if(scrollview)
        {
            scrollview.showsVerticalScrollIndicator = shows;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _flashScrollIndicators(int tag)
	{
		UIScrollView *scrollview = [MHTools getActualObject:tag];
        if(scrollview)
        {
            [scrollview flashScrollIndicators];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _zoomToRect(int tag, const char* rect, bool animated)
	{
		UIScrollView *scrollview = [MHTools getActualObject:tag];
        if(scrollview)
        {
            [scrollview zoomToRect:[MHTools convertRectToCGRect:rect] animated:animated];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	float _zoomScale(int tag)
	{
        UIScrollView *scrollview = [MHTools getActualObject:tag];
        if(scrollview)
        {
            return scrollview.zoomScale;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0.0f;
	}
	
	void _zoomScale_set(int tag, float scale)
	{
		UIScrollView *scrollview = [MHTools getActualObject:tag];
        if(scrollview)
        {
            scrollview.zoomScale = scale;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _setZoomScale(int tag, float scale, bool animated)
	{
		UIScrollView *scrollview = [MHTools getActualObject:tag];
        if(scrollview)
        {
            [scrollview setZoomScale:scale animated:animated];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	float _maximumZoomScale(int tag)
	{
		UIScrollView *scrollview = [MHTools getActualObject:tag];
        if(scrollview)
        {
            return scrollview.maximumZoomScale;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0.0f;
	}
	
	void _maximumZoomScale_set(int tag, float scale)
	{
		UIScrollView *scrollview = [MHTools getActualObject:tag];
        if(scrollview)
        {
            scrollview.maximumZoomScale = scale;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	float _minimumZoomScale(int tag)
	{
		UIScrollView *scrollview = [MHTools getActualObject:tag];
        if(scrollview)
        {
            return scrollview.minimumZoomScale;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0.0f;
	}
	
	void _minimumZoomScale_set(int tag, float scale)
	{
        UIScrollView *scrollview = [MHTools getActualObject:tag];
        if(scrollview)
        {
            scrollview.minimumZoomScale = scale;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	bool _zoomBouncing(int tag)
	{
		UIScrollView *scrollview = [MHTools getActualObject:tag];
        if(scrollview)
        {
            return scrollview.zoomBouncing;
        }
        
        NSLog(CANNOT_FIND_ERROR);
		return false;
	}
	
	bool _zooming(int tag)
	{
		UIScrollView *scrollview = [MHTools getActualObject:tag];
        if(scrollview)
        {
            return scrollview.zooming;
        }
        
        NSLog(CANNOT_FIND_ERROR);
		return false;
	}
	
	bool _bouncesZoom(int tag)
	{
		UIScrollView *scrollview = [MHTools getActualObject:tag];
        if(scrollview)
        {
            return scrollview.bouncesZoom;
        }
        
        NSLog(CANNOT_FIND_ERROR);
		return false;
	}
	
	void _bouncesZoom_set(int tag, bool bounce)
	{
		UIScrollView *scrollview = [MHTools getActualObject:tag];
        if(scrollview)
        {
            scrollview.bouncesZoom = bounce;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
}
