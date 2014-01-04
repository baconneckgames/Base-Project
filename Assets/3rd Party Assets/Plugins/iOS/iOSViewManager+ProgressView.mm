//
//  iOSViewManager+ProgressView.m
//  Unity-iPhone
//
//  Created by Patrick Antoine on 1/3/14.
//
//

#import "iOSViewManager+ProgressView.h"
#define CANNOT_FIND_ERROR @"ERROR: Cannot find UIProgressView"

@implementation iOSViewManager (ProgressView)

@end

extern "C"
{
    int _init_progressview(int tag)
	{
		if(![MHTools objectExists:tag])
        {
            ALBProgressView *pview = [[[ALBProgressView alloc] init] autorelease];
            
            [MHTools addreplaceObject:pview key:tag actualUI:pview];
        }
        else
        {
            NSLog(@"ERROR: This tag is already taken, cannot initiate this object (PROGRESSVIEW)");
        }
        return tag;
	}
    
	int _initWithFrame_progressview(int tag, const char * aRect)
	{
		if(![MHTools objectExists:tag])
        {
            CGRect frame = [MHTools convertRectToCGRect:aRect];
            ALBProgressView *pview = [[[ALBProgressView alloc] initWithFrame:frame] autorelease];
            
            [MHTools addreplaceObject:pview key:tag actualUI:pview];
        }
        else
        {
            NSLog(@"ERROR: This tag is already taken, cannot initiate this object (PROGRESSVIEW)");
        }
        return tag;
	}
	
	int _initWithProgressViewStyle_progressview(int tag, int style)
	{
		if(![MHTools objectExists:tag])
        {
            ALBProgressView *pview = [[[ALBProgressView alloc] initWithProgressViewStyle:(UIProgressViewStyle)style] autorelease];
            
            [MHTools addreplaceObject:pview key:tag actualUI:pview];
        }
        else
        {
            NSLog(@"ERROR: This tag is already taken, cannot initiate this object (PROGRESSVIEW)");
        }
        return tag;
	}
	
	float _progress(int tag)
	{
		UIProgressView *pview = [MHTools getActualObject:tag];
        if(pview)
        {
            return pview.progress;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0.0f;
	}
	
	void _progress_set(int tag, float prog)
	{
		UIProgressView *pview = [MHTools getActualObject:tag];
        if(pview)
        {
            pview.progress = prog;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _setProgress(int tag, float progress, bool animated)
	{
		UIProgressView *pview = [MHTools getActualObject:tag];
        if(pview)
        {
            [pview setProgress:progress animated:animated];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	int _progressViewStyle(int tag)
	{
		UIProgressView *pview = [MHTools getActualObject:tag];
        if(pview)
        {
            return (int)pview.progressViewStyle;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return UIProgressViewStyleDefault;
	}
	
	void _progressViewStyle_set(int tag, int style)
	{
		UIProgressView *pview = [MHTools getActualObject:tag];
        if(pview)
        {
            pview.progressViewStyle = (UIProgressViewStyle)style;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	const char * _progressTintColor(int tag)
	{
		UIProgressView *pview = [MHTools getActualObject:tag];
        if(pview)
        {
            return [MHTools convertUIColorToColor:pview.progressTintColor];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	void _progressTintColor_set(int tag, const char * col)
	{
		UIProgressView *pview = [MHTools getActualObject:tag];
        if(pview)
        {
            pview.progressTintColor = [MHTools convertColorToUIColor:col];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	const char * _progressImage(int tag)
	{
		UIProgressView *pview = [MHTools getActualObject:tag];
        if(pview)
        {
            return [MHTools convertUIImageToImage:pview.progressImage];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	void _progressImage_set(int tag, const char * image)
	{
		UIProgressView *pview = [MHTools getActualObject:tag];
        if(pview)
        {
            pview.progressImage = [MHTools convertImageToUIImage:image];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	const char * _trackTintColor(int tag)
	{
		UIProgressView *pview = [MHTools getActualObject:tag];
        if(pview)
        {
            return [MHTools convertUIColorToColor:pview.trackTintColor];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	void _trackTintColor_set(int tag, const char * col)
	{
		UIProgressView *pview = [MHTools getActualObject:tag];
        if(pview)
        {
            pview.trackTintColor = [MHTools convertColorToUIColor:col];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	const char * _trackImage(int tag)
	{
		UIProgressView *pview = [MHTools getActualObject:tag];
        if(pview)
        {
            return [MHTools convertUIImageToImage:pview.trackImage];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	void _trackImage_set(int tag, const char * image)
	{
		UIProgressView *pview = [MHTools getActualObject:tag];
        if(pview)
        {
            pview.trackImage = [MHTools convertImageToUIImage:image];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
}