//
//  iOSViewManager+ActivityIndicatorView.m
//  Unity-iPhone
//
//  Created by Patrick Antoine on 1/3/14.
//
//

#import "iOSViewManager+ActivityIndicatorView.h"
#define CANNOT_FIND_ERROR @"ERROR: Cannot find UIActivityIndicatorView"

@implementation iOSViewManager (ActivityIndicatorView)

@end

extern "C"
{
    int _init_activityindicator(int tag)
	{
		if(![MHTools objectExists:tag])
        {
            ALBActivityIndicatorView *aview = [[[ALBActivityIndicatorView alloc] init] autorelease];
            
            [MHTools addreplaceObject:aview key:tag actualUI:aview];
        }
        else
        {
            NSLog(@"ERROR: This tag is already taken, cannot initiate this object (ACTIVITYINDICATOR)");
        }
        return tag;
	}
    
	int _initWithFrame_activityindicator(int tag, const char * aRect)
	{
		if(![MHTools objectExists:tag])
        {
            CGRect frame = [MHTools convertRectToCGRect:aRect];
            ALBActivityIndicatorView *aview = [[[ALBActivityIndicatorView alloc] initWithFrame:frame] autorelease];
            
            [MHTools addreplaceObject:aview key:tag actualUI:aview];
        }
        else
        {
            NSLog(@"ERROR: This tag is already taken, cannot initiate this object (ACTIVITYINDICATOR)");
        }
        return tag;
	}
	
	int _initWithActivityIndicatorStyle_activityindicator(int tag, int style)
	{
		if(![MHTools objectExists:tag])
        {
            ALBActivityIndicatorView *aview = [[[ALBActivityIndicatorView alloc] initWithActivityIndicatorStyle:(UIActivityIndicatorViewStyle)style] autorelease];
            
            [MHTools addreplaceObject:aview key:tag actualUI:aview];
        }
        else
        {
            NSLog(@"ERROR: This tag is already taken, cannot initiate this object (ACTIVITYINDICATOR)");
        }
        return tag;
	}
	
	bool _hidesWhenStopped(int tag)
	{
		UIActivityIndicatorView *aview = [MHTools getActualObject:tag];
        if(aview)
        {
            return aview.hidesWhenStopped;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return false;
	}
	
	void _hidesWhenStopped_set(int tag, bool hides)
	{
		UIActivityIndicatorView *aview = [MHTools getActualObject:tag];
        if(aview)
        {
            aview.hidesWhenStopped = hides;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	int _activityIndicatorViewStyle(int tag)
	{
		UIActivityIndicatorView *aview = [MHTools getActualObject:tag];
        if(aview)
        {
            return (int)aview.activityIndicatorViewStyle;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return UIActivityIndicatorViewStyleWhiteLarge;
	}
	
	void _activityIndicatorViewStyle_set(int tag, int style)
	{
		UIActivityIndicatorView *aview = [MHTools getActualObject:tag];
        if(aview)
        {
            aview.activityIndicatorViewStyle = (UIActivityIndicatorViewStyle)style;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	const char * _color(int tag)
	{
		UIActivityIndicatorView *aview = [MHTools getActualObject:tag];
        if(aview)
        {
            return [MHTools convertUIColorToColor:aview.color];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	void _color_set(int tag, const char * col)
	{
		UIActivityIndicatorView *aview = [MHTools getActualObject:tag];
        if(aview)
        {
            aview.color = [MHTools convertColorToUIColor:col];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
}