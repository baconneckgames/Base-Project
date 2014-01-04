//
//  iOSViewManager+Toolbar.m
//  Unity-iPhone
//
//  Created by Patrick Antoine on 9/19/13.
//
//

#import "iOSViewManager+Toolbar.h"
#define CANNOT_FIND_ERROR @"ERROR: Cannot find UIToolbar"

@implementation iOSViewManager (Toolbar)

@end

extern "C"
{
    int _init_toolbar(int tag)
	{
        if(![MHTools objectExists:tag])
        {
            ALBToolbar *toolbar = [[[ALBToolbar alloc] init] autorelease];
            
            [MHTools addreplaceObject:toolbar key:tag actualUI:toolbar];
        }
        else
        {
            NSLog(@"ERROR: This tag is already taken, cannot initiate this object (TOOLBAR)");
        }
        return tag;
	}
	
	int _initWithFrame_toolbar(int tag, const char * aRect)
	{
        if(![MHTools objectExists:tag])
        {
            CGRect frame = [MHTools convertRectToCGRect:aRect];
            ALBToolbar *toolbar = [[[ALBToolbar alloc] initWithFrame:frame] autorelease];
            
            [MHTools addreplaceObject:toolbar key:tag actualUI:toolbar];
        }
        else
        {
            NSLog(@"ERROR: This tag is already taken, cannot initiate this object (TOOLBAR)");
        }
        return tag;
	}
	
	const char * _items_toolbar(int tag)
	{
        UIToolbar *toolbar = [MHTools getActualObject:tag];
        if(toolbar)
        {
            return [MHTools convertNSArrayToMultipleTags:toolbar.items];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return nil;
	}
	
	void _items_toolbar_set(int tag, int * items)
	{
        UIToolbar *toolbar = [MHTools getActualObject:tag];
        if(toolbar)
        {
            toolbar.items = [MHTools convertMultipleTagsToNSArray:items];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _setItems_toolbar(int tag, int * items, bool animated)
	{
        UIToolbar *toolbar = [MHTools getActualObject:tag];
        if(toolbar)
        {
            [toolbar setItems:[MHTools convertMultipleTagsToNSArray:items] animated:animated];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	const char * _backgroundImageForToolbarPosition(int tag, int topOrBottom, int barMetrics)
	{
        UIToolbar *toolbar = [MHTools getActualObject:tag];
        if(toolbar)
        {
            return [MHTools convertUIImageToImage:[toolbar backgroundImageForToolbarPosition:(UIToolbarPosition)topOrBottom barMetrics:(UIBarMetrics)barMetrics]];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	void _setBackgroundImage_pos(int tag, const char * backgroundImage, int topOrBottom, int barMetrics)
	{
        UIToolbar *toolbar = [MHTools getActualObject:tag];
        if(toolbar)
        {
            [toolbar setBackgroundImage:[MHTools convertImageToUIImage:backgroundImage] forToolbarPosition: (UIToolbarPosition)topOrBottom barMetrics:(UIBarMetrics)barMetrics];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	const char * _shadowImageForToolbarPosition(int tag, int topOrBottom)
	{
        UIToolbar *toolbar = [MHTools getActualObject:tag];
        if(toolbar)
        {
            return [MHTools convertUIImageToImage:[toolbar shadowImageForToolbarPosition:(UIToolbarPosition)topOrBottom]];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	void _setShadowImage(int tag, const char * shadowImage, int topOrBottom)
	{
        UIToolbar *toolbar = [MHTools getActualObject:tag];
        if(toolbar)
        {
            [toolbar setShadowImage:[MHTools convertImageToUIImage:shadowImage] forToolbarPosition: (UIToolbarPosition)topOrBottom];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
}