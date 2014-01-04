//
//  iOSViewManager+View.m
//  Unity-iPhone
//
//  Created by Patrick Antoine on 8/22/13.
//
//

#import "iOSViewManager+View.h"
#define CANNOT_FIND_ERROR @"ERROR: Cannot find UIView"

@implementation iOSViewManager (View)

@end

extern "C"
{
    int _init_view(int tag)
    {
        if(![MHTools objectExists:tag])
        {
            ALBView *vw = [[[ALBView alloc] init] autorelease];
            
            [MHTools addreplaceObject:vw key:tag actualUI:vw];
        }
        else
        {
            NSLog(@"ERROR: This tag is already taken, cannot initiate this object (VIEW)");
        }
        return tag;
    }
    
    int _initWithFrame(int tag, const char * aRect)
    {
        if(![MHTools objectExists:tag])
        {
            CGRect frame = [MHTools convertRectToCGRect:aRect];
            ALBView *vw = [[[ALBView alloc] initWithFrame:frame] autorelease];
            
            [MHTools addreplaceObject:vw key:tag actualUI:vw];
        }
        else
        {
            NSLog(@"ERROR: This tag is already taken, cannot initiate this object (VIEW)");
        }
        return tag;
    }
    
    const char * _backgroundColor(int tag)
	{
        UIView *vw = [MHTools getActualView:tag];
        if(vw)
        {
            return [MHTools convertUIColorToColor:vw.backgroundColor];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	void _backgroundColor_set(int tag, const char * color)
	{
        UIView *vw = [MHTools getActualView:tag];
        if(vw)
        {
            vw.backgroundColor = [MHTools convertColorToUIColor:color];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	bool _hidden(int tag)
	{
        UIView *vw = [MHTools getActualView:tag];
        if(vw)
        {
            return vw.hidden;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return false;
	}
	
	void _hidden_set(int tag, bool hid)
	{
        UIView *vw = [MHTools getActualView:tag];
        if(vw)
        {
            vw.hidden = hid;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	float _alpha(int tag)
	{
        UIView *vw = [MHTools getActualView:tag];
        if(vw)
        {
            return vw.alpha;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0.0f;
	}
	
	void _alpha_set(int tag, float a)
	{
        UIView *vw = [MHTools getActualView:tag];
        if(vw)
        {
            vw.alpha = a;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	bool _opaque(int tag)
	{
        UIView *vw = [MHTools getActualView:tag];
        if(vw)
        {
            return vw.opaque;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return false;
	}
	
	void _opaque_set(int tag, bool o)
	{
        UIView *vw = [MHTools getActualView:tag];
        if(vw)
        {
            vw.opaque = o;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	bool _clipsToBounds(int tag)
	{
        UIView *vw = [MHTools getActualView:tag];
        if(vw)
        {
            return vw.clipsToBounds;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return false;
	}
	
	void _clipsToBounds_set(int tag, bool clip)
	{
        UIView *vw = [MHTools getActualView:tag];
        if(vw)
        {
            vw.clipsToBounds = clip;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	bool _clearsContextBeforeDrawing(int tag)
	{
        UIView *vw = [MHTools getActualView:tag];
        if(vw)
        {
            return vw.clearsContextBeforeDrawing;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return false;
	}
	
	void _clearsContextBeforeDrawing_set(int tag, bool clear)
	{
        UIView *vw = [MHTools getActualView:tag];
        if(vw)
        {
            vw.clearsContextBeforeDrawing = clear;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	bool _userInteractionEnabled(int tag)
	{
        UIView *vw = [MHTools getActualView:tag];
        if(vw)
        {
            return vw.userInteractionEnabled;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return false;
	}
	
	void _userInteractionEnabled_set(int tag, bool enabled)
	{
        UIView *vw = [MHTools getActualView:tag];
        if(vw)
        {
            vw.userInteractionEnabled = enabled;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	bool _multipleTouchEnabled(int tag)
	{
        UIView *vw = [MHTools getActualView:tag];
        if(vw)
        {
            return vw.multipleTouchEnabled;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return false;
	}
	
	void _multipleTouchEnabled_set(int tag, bool enabled)
	{
        UIView *vw = [MHTools getActualView:tag];
        if(vw)
        {
            vw.multipleTouchEnabled = enabled;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	bool _exclusiveTouch(int tag)
	{
        UIView *vw = [MHTools getActualView:tag];
        if(vw)
        {
            return vw.exclusiveTouch;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return false;
	}
	
	void _exclusiveTouch_set(int tag, bool exclusive)
	{
        UIView *vw = [MHTools getActualView:tag];
        if(vw)
        {
            vw.exclusiveTouch = exclusive;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	const char * _frame(int tag)
	{
        UIView *vw = [MHTools getActualView:tag];
        if(vw)
        {
            return [MHTools convertCGRectToRect:vw.frame];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	void _frame_set(int tag, const char * rect)
	{
        UIView *vw = [MHTools getActualView:tag];
        if(vw)
        {
            vw.frame = [MHTools convertRectToCGRect:rect];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	const char * _bounds(int tag)
	{
        UIView *vw = [MHTools getActualView:tag];
        if(vw)
        {
            return [MHTools convertCGRectToRect:vw.bounds];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	void _bounds_set(int tag, const char * rect)
	{
        UIView *vw = [MHTools getActualView:tag];
        if(vw)
        {
            vw.bounds = [MHTools convertRectToCGRect:rect];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	const char * _center(int tag)
	{
        UIView *vw = [MHTools getActualView:tag];
        if(vw)
        {
            return [MHTools convertCGPointToVector2:vw.center];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	void _center_set(int tag, const char * point)
	{
        UIView *vw = [MHTools getActualView:tag];
        if(vw)
        {
            vw.center = [MHTools convertVector2ToCGPoint:point];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	int _superview(int tag)
	{
        UIView *vw = [MHTools getActualView:tag];
        if(vw)
        {
            return [MHTools getKeyFromActual:vw.superview];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0;
	}
	
	const char * _subviews(int tag)
	{
        UIView *vw = [MHTools getActualView:tag];
        if(vw)
        {
            return [MHTools convertNSArrayToMultipleTags:vw.subviews];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return nil;
	}
	
	void _addSubview(int tag, int subview)
	{
        UIView *vw = [MHTools getActualView:tag];
        if(vw)
        {
            [vw addSubview:[MHTools getActualView:subview]];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _bringSubviewToFront(int tag, int subview)
	{
        UIView *vw = [MHTools getActualView:tag];
        if(vw)
        {
            [vw bringSubviewToFront:[MHTools getActualView:subview]];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _sendSubviewToBack(int tag, int subview)
	{
        UIView *vw = [MHTools getActualView:tag];
        if(vw)
        {
            [vw sendSubviewToBack:[MHTools getActualView:subview]];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _removeFromSuperview(int tag)
	{
        UIView *vw = [MHTools getActualView:tag];
        if(vw)
        {
            [vw removeFromSuperview];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _insertSubviewAtIndex(int tag, int view, int index)
	{
        UIView *vw = [MHTools getActualView:tag];
        if(vw)
        {
            [vw insertSubview:[MHTools getActualView:view] atIndex:index];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _insertSubviewAboveSubview(int tag, int view, int siblingSubview)
	{
        UIView *vw = [MHTools getActualView:tag];
        if(vw)
        {
            [vw insertSubview:[MHTools getActualView:view] aboveSubview:[MHTools getActualView:siblingSubview]];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _insertSubviewBelowSubview(int tag, int view, int siblingSubview)
	{
        UIView *vw = [MHTools getActualView:tag];
        if(vw)
        {
            [vw insertSubview:[MHTools getActualView:view] belowSubview:[MHTools getActualView:siblingSubview]];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _exchangeSubviewAtIndex(int tag, int index1, int index2)
	{
        UIView *vw = [MHTools getActualView:tag];
        if(vw)
        {
            [vw exchangeSubviewAtIndex:index1 withSubviewAtIndex:index2];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	bool _isDescendantOfView(int tag, int view)
	{
        UIView *vw = [MHTools getActualView:tag];
        if(vw)
        {
            return [vw isDescendantOfView:[MHTools getActualView:view]];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return false;
	}
	
	int _autoresizingMask(int tag)
	{
        UIView *vw = [MHTools getActualView:tag];
        if(vw)
        {
            return (int)vw.autoresizingMask;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return (int)UIViewAutoresizingNone;
	}
	
	void _autoresizingMask_set(int tag, int mask)
	{
        UIView *vw = [MHTools getActualView:tag];
        if(vw)
        {
            vw.autoresizingMask = (UIViewAutoresizing)mask;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	bool _autoresizesSubviews(int tag)
	{
        UIView *vw = [MHTools getActualView:tag];
        if(vw)
        {
            return vw.autoresizesSubviews;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return false;
	}
	
	void _autoresizesSubviews_set(int tag, bool autoresize)
	{
        UIView *vw = [MHTools getActualView:tag];
        if(vw)
        {
            vw.autoresizesSubviews = autoresize;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	int _contentMode(int tag)
	{
        UIView *vw = [MHTools getActualView:tag];
        if(vw)
        {
            return (int)vw.contentMode;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return (int)UIViewContentModeCenter;
	}
	
	void _contentMode_set(int tag, int mode)
	{
        UIView *vw = [MHTools getActualView:tag];
        if(vw)
        {
            vw.contentMode = (UIViewContentMode)mode;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	const char * _sizeThatFits(int tag, const char * size)
	{
        UIView *vw = [MHTools getActualView:tag];
        if(vw)
        {
            return [MHTools convertCGSizeToVector2:[vw sizeThatFits:[MHTools convertVector2ToCGSize:size]]];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	void _sizeToFit(int tag)
	{
        UIView *vw = [MHTools getActualView:tag];
        if(vw)
        {
            [vw sizeToFit];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _layoutSubviews(int tag)
	{
        UIView *vw = [MHTools getActualView:tag];
        if(vw)
        {
            [vw layoutSubviews];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _setNeedsLayout(int tag)
	{
        UIView *vw = [MHTools getActualView:tag];
        if(vw)
        {
            [vw setNeedsLayout];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _layoutIfNeeded(int tag)
	{
        UIView *vw = [MHTools getActualView:tag];
        if(vw)
        {
            [vw layoutIfNeeded];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _drawRect(int tag, const char * rect)
	{
        UIView *vw = [MHTools getActualView:tag];
        if(vw)
        {
            [vw drawRect:[MHTools convertRectToCGRect:rect]];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _setNeedsDisplay(int tag)
	{
        UIView *vw = [MHTools getActualView:tag];
        if(vw)
        {
            [vw setNeedsDisplay];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _setNeedsDisplayInRect(int tag, const char * invalidRect)
	{
        UIView *vw = [MHTools getActualView:tag];
        if(vw)
        {
            [vw setNeedsDisplayInRect:[MHTools convertRectToCGRect:invalidRect]];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	float _contentScaleFactor(int tag)
	{
        UIView *vw = [MHTools getActualView:tag];
        if(vw)
        {
            return vw.contentScaleFactor;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0.0f;
	}
	
	void _contentScaleFactor_set(int tag, float factor)
	{
        UIView *vw = [MHTools getActualView:tag];
        if(vw)
        {
            vw.contentScaleFactor = factor;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}

	void _animateWithDuration_1(int tag, float duration, float delay, int options, int completion)
	{
        ALBView *vw = [MHTools getObject:tag];
        if(vw)
        {
            [vw animateWithDuration:(double)duration delay:(double)delay options:(UIViewAnimationOptions)options completion:completion];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _animateWithDuration_2(int tag, float duration, int completion)
	{
        ALBView *vw = [MHTools getObject:tag];
        if(vw)
        {
            [vw animateWithDuration:(double)duration completion:completion];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _animateWithDuration_3(int tag, float duration)
	{
        ALBView *vw = [MHTools getObject:tag];
        if(vw)
        {
            [vw animateWithDuration:(double)duration];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _transitionWithView_1(int tag, int view, float duration, int options, int completion)
	{
        ALBView *vw = [MHTools getObject:tag];
        if(vw)
        {
            [vw transitionWithView:[MHTools getActualView:view] duration:(double)duration options:(UIViewAnimationOptions)options completion:completion];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _transitionFromView_2(int tag, int fromView, int toView, float duration, int options, int completion)
	{
        ALBView *vw = [MHTools getObject:tag];
        if(vw)
        {
            [vw transitionFromView:[MHTools getActualView:fromView] toView:[MHTools getActualView:toView] duration:(double)duration options:(UIViewAnimationOptions)options completion:completion];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	// context is always 'this'
	void _beginAnimations(int tag, const char * animationID, int context)
	{
        ALBView *vw = [MHTools getObject:tag];
        if(vw)
        {
            [vw beginAnimations:GetStringParamOrNil(animationID) context:[MHTools getActualObject:context]];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _commitAnimations(int tag)
	{
        ALBView *vw = [MHTools getObject:tag];
        if(vw)
        {
            [vw commitAnimations];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _setAnimationStartDate(int tag, const char * dte)
	{
        ALBView *vw = [MHTools getObject:tag];
        if(vw)
        {
            [vw setAnimationStartDate:[MHTools convertDateTimeToNSDate:dte]];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _setAnimationsEnabled(int tag, bool enable)
	{
        ALBView *vw = [MHTools getObject:tag];
        if(vw)
        {
            [vw setAnimationsEnabled:enable];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _setAnimationDuration(int tag, float duration)
	{
        ALBView *vw = [MHTools getObject:tag];
        if(vw)
        {
            [vw setAnimationDuration:(double)duration];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _setAnimationDelay(int tag, float delay)
	{
        ALBView *vw = [MHTools getObject:tag];
        if(vw)
        {
            [vw setAnimationDelay:(double)delay];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _setAnimationCurve(int tag, int curve)
	{
        ALBView *vw = [MHTools getObject:tag];
        if(vw)
        {
            [vw setAnimationCurve:(UIViewAnimationCurve)curve];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _setAnimationRepeatCount(int tag, float count)
	{
        ALBView *vw = [MHTools getObject:tag];
        if(vw)
        {
            [vw setAnimationRepeatCount:count];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _setAnimationRepeatAutoreverses(int tag, bool repeatAutoreverses)
	{
        ALBView *vw = [MHTools getObject:tag];
        if(vw)
        {
            [vw setAnimationRepeatAutoreverses:repeatAutoreverses];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _setAnimationBeginsFromCurrentState(int tag, bool fromCurrentState)
	{
        ALBView *vw = [MHTools getObject:tag];
        if(vw)
        {
            [vw setAnimationBeginsFromCurrentState:fromCurrentState];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _setAnimationTransition(int tag, int transition, int view, bool cache)
	{
        ALBView *vw = [MHTools getObject:tag];
        if(vw)
        {
            [vw setAnimationTransition:(UIViewAnimationTransition)transition forView:[MHTools getActualView:view] cache:cache];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	bool _areAnimationsEnabled(int tag)
	{
        ALBView *vw = [MHTools getObject:tag];
        if(vw)
        {
            return [vw areAnimationsEnabled];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return false;
	}
	
	int _viewWithTag(int tag, int tagOfView)
	{
        UIView *vw = [MHTools getActualView:tag];
        if(vw)
        {
            return [MHTools getKeyFromActual:[vw viewWithTag:tagOfView]];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0;
	}
	
	const char * _convertPointToView(int tag, const char * point, int toView)
	{
        UIView *vw = [MHTools getActualView:tag];
        if(vw)
        {
            return [MHTools convertCGPointToVector2:[vw convertPoint:[MHTools convertVector2ToCGPoint:point] toView:[MHTools getActualView:toView]]];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	const char * _convertPointFromView(int tag, const char * point, int fromView)
	{
        UIView *vw = [MHTools getActualView:tag];
        if(vw)
        {
            return [MHTools convertCGPointToVector2:[vw convertPoint:[MHTools convertVector2ToCGPoint:point] fromView:[MHTools getActualView:fromView]]];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	const char * _convertRectToView(int tag, const char * rect, int toView)
	{
        UIView *vw = [MHTools getActualView:tag];
        if(vw)
        {
            return [MHTools convertCGRectToRect:[vw convertRect:[MHTools convertRectToCGRect:rect] toView:[MHTools getActualView:toView]]];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	const char * _convertRectFromView(int tag, const char * rect, int fromView)
	{
        UIView *vw = [MHTools getActualView:tag];
        if(vw)
        {
            return [MHTools convertCGRectToRect:[vw convertRect:[MHTools convertRectToCGRect:rect] fromView:[MHTools getActualView:fromView]]];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	bool _endEditing(int tag, bool force)
	{
        UIView *vw = [MHTools getActualView:tag];
        if(vw)
        {
            return [vw endEditing:force];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return false;
	}
}