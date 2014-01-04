//
//  iOSViewManager+Switch.m
//  Unity-iPhone
//
//  Created by Sammy on 11/5/13.
//
//

#import "iOSViewManager+Switch.h"
#define CANNOT_FIND_ERROR @"ERROR: Cannot find UISwitch"

@implementation iOSViewManager (Switch)

@end

extern "C"
{
#pragma mark - switch
    int _init_switch(int tag)
    {
        if(![MHTools objectExists:tag])
        {
            ALBSwitch *swtch = [[[ALBSwitch alloc] init] autorelease];
            
            [MHTools addreplaceObject:swtch key:tag actualUI:swtch];
        }
        else
        {
            NSLog(@"ERROR: This tag is already taken, cannot initiate this object (SWITCH)");
        }
        
        return tag;
	}
	
	int _initWithFrame_switch(int tag, const char * aRect)
    {
        if(![MHTools objectExists:tag])
        {
            CGRect cgRect = [MHTools convertRectToCGRect:aRect];
            
            ALBSwitch *swtch = [[[ALBSwitch alloc] initWithFrame:cgRect] autorelease];
            
            [MHTools addreplaceObject:swtch key:tag actualUI:swtch];
        }
        else
        {
            NSLog(@"ERROR: This tag is already taken, cannot initiate this object (SWITCH)");
        }
        
        return tag;
    }
	
	bool _on(int tag)
	{
		UISwitch *sw = [MHTools getActualObject:tag];
        
        if(sw)
        {
            return sw.on;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return false;
	}
	
	void _on_set(int tag, bool isOn)
	{
		UISwitch *sw = [MHTools getActualObject:tag];
        
        if(sw)
        {
            sw.on = isOn;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _setOn(int tag, bool isOn, bool animated)
	{
		UISwitch *sw = [MHTools getActualObject:tag];
        
        if(sw)
        {
            [sw setOn:isOn animated:animated];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	const char * _onTintColor(int tag)
	{
		UISwitch *sw = [MHTools getActualView:tag];
        if(sw)
        {
            return [MHTools convertUIColorToColor:sw.onTintColor];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	void _onTintColor_set(int tag, const char * color)
	{
		UISwitch *sw = [MHTools getActualView:tag];
        if(sw)
        {
            sw.onTintColor = [MHTools convertColorToUIColor:color];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	const char * _onImage(int tag)
	{
		UISwitch *sw = [MHTools getActualObject:tag];
        if(sw)
        {
            return [MHTools convertUIImageToImage:sw.onImage];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	void _onImage_set(int tag, const char * img)
    {
		UISwitch *sw = [MHTools getActualObject:tag];
        if(sw)
        {
            sw.onImage = [MHTools convertImageToUIImage:img];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	const char * _offImage(int tag)
    {
		UISwitch *sw = [MHTools getActualObject:tag];
        if(sw)
        {
            return [MHTools convertUIImageToImage:sw.offImage];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	void _offImage_set(int tag, const char * img)
    {
		UISwitch *sw = [MHTools getActualObject:tag];
        if(sw)
        {
            sw.offImage = [MHTools convertImageToUIImage:img];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
}