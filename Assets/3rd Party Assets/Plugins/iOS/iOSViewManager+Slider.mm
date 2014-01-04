//
//  iOSViewManager+Slider.m
//  Unity-iPhone
//
//  Created by Sammy on 12/25/13.
//
//

#import "iOSViewManager+Slider.h"
#define CANNOT_FIND_ERROR @"ERROR: Cannot find UISlider"

@implementation iOSViewManager (Slider)

@end

extern "C"
{
#pragma mark - slider
    int _init_slider(int tag)
    {
		if(![MHTools objectExists:tag])
        {
            ALBSlider *slider = [[[ALBSlider alloc] init] autorelease];
            
            [MHTools addreplaceObject:slider key:tag actualUI:slider];
        }
        else
        {
            NSLog(@"ERROR: This tag is already taken, cannot initiate this object (SLIDER)");
        }
        
        return tag;
	}
	
	int _initWithFrame_slider(int tag, const char * aRect)
	{
		if(![MHTools objectExists:tag])
        {
            CGRect cgRect = [MHTools convertRectToCGRect:aRect];
            
            ALBSlider *slider = [[[ALBSlider alloc] initWithFrame:cgRect] autorelease];
            
            [MHTools addreplaceObject:slider key:tag actualUI:slider];
        }
        else
        {
            NSLog(@"ERROR: This tag is already taken, cannot initiate this object (SLIDER)");
        }
        
        return tag;
	}
	
	float _val(int tag)
	{
		UISlider *sl = [MHTools getActualObject:tag];
        
        if(sl)
        {
            return sl.value;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0.0f;
	}
	
	void _val_set(int tag, float val)
	{
		UISlider *sl = [MHTools getActualObject:tag];
        
        if(sl)
        {
            sl.value = val;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _setVal(int tag, float val, bool animated)
	{
		UISlider *sl = [MHTools getActualObject:tag];
        
        if(sl)
        {
            [sl setValue:val animated:animated];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	float _minimumValue(int tag)
	{
		UISlider *sl = [MHTools getActualObject:tag];
        
        if(sl)
        {
            return sl.minimumValue;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0.0f;
	}
	
	void _minimumValue_set(int tag, float val)
	{
		UISlider *sl = [MHTools getActualObject:tag];
        
        if(sl)
        {
            sl.minimumValue = val;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	float _maximumValue(int tag)
	{
		UISlider *sl = [MHTools getActualObject:tag];
        
        if(sl)
        {
            return sl.maximumValue;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0.0f;
	}
	
	void _maximumValue_set(int tag, float val)
	{
		UISlider *sl = [MHTools getActualObject:tag];
        
        if(sl)
        {
            sl.maximumValue = val;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	bool _continuous(int tag)
	{
		UISlider *sl = [MHTools getActualObject:tag];
        
        if(sl)
        {
            return sl.continuous;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return false;
	}
	
	void _continuous_set(int tag, bool cont)
	{
		UISlider *sl = [MHTools getActualObject:tag];
        
        if(sl)
        {
            sl.continuous = cont;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	const char * _minimumValueImage(int tag)
	{
		UISlider *sl = [MHTools getActualObject:tag];
        if(sl)
        {
            return [MHTools convertUIImageToImage:sl.minimumValueImage];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	void _minimumValueImage_set(int tag, const char * image)
	{
		UISlider *sl = [MHTools getActualObject:tag];
        if(sl)
        {
            sl.minimumValueImage = [MHTools convertImageToUIImage:image];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	const char * _maximumValueImage(int tag)
	{
		UISlider *sl = [MHTools getActualObject:tag];
        if(sl)
        {
            return [MHTools convertUIImageToImage:sl.minimumValueImage];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	void _maximumValueImage_set(int tag, const char * image)
	{
		UISlider *sl = [MHTools getActualObject:tag];
        if(sl)
        {
            sl.maximumValueImage = [MHTools convertImageToUIImage:image];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	const char * _minimumTrackTintColor(int tag)
	{
		UISlider *sl = [MHTools getActualView:tag];
        if(sl)
        {
            return [MHTools convertUIColorToColor:sl.minimumTrackTintColor];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	void _minimumTrackTintColor_set(int tag, const char * col)
	{
		UISlider *sl = [MHTools getActualView:tag];
        if(sl)
        {
            sl.minimumTrackTintColor = [MHTools convertColorToUIColor:col];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	const char * _currentMinimumTrackImage(int tag)
	{
		UISlider *sl = [MHTools getActualObject:tag];
        if(sl)
        {
            return [MHTools convertUIImageToImage:sl.currentMinimumTrackImage];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	const char * _minimumTrackImageForState(int tag, int state)
	{
		UISlider *sl = [MHTools getActualObject:tag];
        if(sl)
        {
            return [MHTools convertUIImageToImage:[sl minimumTrackImageForState:(UIControlState)state]];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	void _setMinimumTrackImageForState(int tag, const char * image, int state)
	{
		UISlider *sl = [MHTools getActualObject:tag];
        if(sl)
        {
            [sl setMinimumTrackImage:[MHTools convertImageToUIImage:image] forState:(UIControlState)state];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	const char * _maximumTrackTintColor(int tag)
	{
		UISlider *sl = [MHTools getActualView:tag];
        if(sl)
        {
            return [MHTools convertUIColorToColor:sl.maximumTrackTintColor];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	void _maximumTrackTintColor_set(int tag, const char * col)
	{
		UISlider *sl = [MHTools getActualView:tag];
        if(sl)
        {
            sl.maximumTrackTintColor = [MHTools convertColorToUIColor:col];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	const char * _currentMaximumTrackImage(int tag)
	{
		UISlider *sl = [MHTools getActualObject:tag];
        if(sl)
        {
            return [MHTools convertUIImageToImage:sl.currentMaximumTrackImage];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	const char * _maximumTrackImageForState(int tag, int state)
	{
		UISlider *sl = [MHTools getActualObject:tag];
        if(sl)
        {
            return [MHTools convertUIImageToImage:[sl maximumTrackImageForState:(UIControlState)state]];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	void _setMaximumTrackImageForState(int tag, const char * image, int state)
	{
		UISlider *sl = [MHTools getActualObject:tag];
        if(sl)
        {
            [sl setMaximumTrackImage:[MHTools convertImageToUIImage:image] forState:(UIControlState)state];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	const char * _currentThumbImage(int tag)
	{
		UISlider *sl = [MHTools getActualObject:tag];
        if(sl)
        {
            return [MHTools convertUIImageToImage:sl.currentThumbImage];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	const char * _thumbImageForState(int tag, int state)
	{
		UISlider *sl = [MHTools getActualObject:tag];
        if(sl)
        {
            return [MHTools convertUIImageToImage:[sl thumbImageForState:(UIControlState)state]];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	void _setThumbImageForState(int tag, const char * image, int state)
    {
        UISlider *sl = [MHTools getActualObject:tag];
        if(sl)
        {
            [sl setThumbImage:[MHTools convertImageToUIImage:image] forState:(UIControlState)state];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
    }
}