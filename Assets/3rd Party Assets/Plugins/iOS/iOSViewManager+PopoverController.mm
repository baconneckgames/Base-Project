//
//  iOSViewManager+PopoverController.m
//  Unity-iPhone
//
//  Created by Patrick Antoine on 9/19/13.
//
//

#import "iOSViewManager+PopoverController.h"
#define CANNOT_FIND_ERROR @"ERROR: Cannot find UIPopoverController"

@implementation iOSViewManager (PopoverController)

@end

extern "C"
{
    int _init_popovercontroller(int tag)
	{
        if(![MHTools objectExists:tag])
        {
            ALBPopoverController *popcontroller = [[[ALBPopoverController alloc] init] autorelease];
            popcontroller.delegate = popcontroller;
            
            [MHTools addreplaceObject:popcontroller key:tag actualUI:popcontroller];
        }
        else
        {
            NSLog(@"ERROR: This tag is already taken, cannot initiate this object (POPOVER)");
        }
        return tag;
	}
	
	int _initWithContentViewController(int tag, int viewController)
	{
        if(![MHTools objectExists:tag])
        {
            UIViewController *vc = [MHTools getActualViewController:viewController];
            ALBPopoverController *popcontroller = [[[ALBPopoverController alloc] initWithContentViewController:vc] autorelease];
            popcontroller.delegate = popcontroller;
            
            [MHTools addreplaceObject:popcontroller key:tag actualUI:popcontroller];
        }
        else
        {
            NSLog(@"ERROR: This tag is already taken, cannot initiate this object (POPOVER)");
        }
        return tag;
	}
	
	int _contentViewController(int tag)
	{
        UIPopoverController *popcontroller = [MHTools getActualObject:tag];
        if(popcontroller)
        {
            UIViewController *vc = popcontroller.contentViewController;
            return [MHTools getKeyFromActual:vc];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0;
	}
	
	void _contentViewController_set(int tag, int viewController)
	{
        UIPopoverController *popcontroller = [MHTools getActualObject:tag];
        if(popcontroller)
        {
            UIViewController *vc = [MHTools getActualViewController:viewController];
            popcontroller.contentViewController = vc;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _setContentViewController(int tag, int viewController, bool animated)
	{
        UIPopoverController *popcontroller = [MHTools getActualObject:tag];
        if(popcontroller)
        {
            UIViewController *vc = [MHTools getActualViewController:viewController];
            [popcontroller setContentViewController:vc animated:animated];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);

	}
	
	const char * _popoverContentSize(int tag)
	{
        UIPopoverController *popcontroller = [MHTools getActualObject:tag];
        if(popcontroller)
        {
            return [MHTools convertCGSizeToVector2:popcontroller.popoverContentSize];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	void _popoverContentSize_set(int tag, const char * size)
	{
        UIPopoverController *popcontroller = [MHTools getActualObject:tag];
        if(popcontroller)
        {
            popcontroller.popoverContentSize = [MHTools convertVector2ToCGSize:size];   
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _setPopoverContentSize(int tag, const char * size, bool animated)
	{
        UIPopoverController *popcontroller = [MHTools getActualObject:tag];
        if(popcontroller)
        {
            [popcontroller setPopoverContentSize:[MHTools convertVector2ToCGSize:size] animated:animated];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	const char * _passthroughViews(int tag)
	{
        UIPopoverController *popcontroller = [MHTools getActualObject:tag];
        if(popcontroller)
        {
            return [MHTools convertNSArrayToMultipleTags:popcontroller.passthroughViews];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return nil;
	}
	
	void _passthroughViews_set(int tag, int * views)
	{
        UIPopoverController *popcontroller = [MHTools getActualObject:tag];
        if(popcontroller)
        {
            popcontroller.passthroughViews = [MHTools convertMultipleTagsToNSArray:views];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	bool _popoverVisible(int tag)
	{
        UIPopoverController *popcontroller = [MHTools getActualObject:tag];
        if(popcontroller)
        {
            return popcontroller.popoverVisible;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return false;
	}
	
	int _popoverArrowDirection(int tag)
	{
        UIPopoverController *popcontroller = [MHTools getActualObject:tag];
        if(popcontroller)
        {
            return (int)popcontroller.popoverArrowDirection;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return (int)UIPopoverArrowDirectionAny;
	}
    
	void _presentPopoverFromRect(int tag, const char * rect, int inView, int permittedArrowDirections, bool animated)
	{
        UIPopoverController *popcontroller = [MHTools getActualObject:tag];
        if(popcontroller)
        {
            [popcontroller presentPopoverFromRect:[MHTools convertRectToCGRect:rect] inView:[MHTools getActualView:inView] permittedArrowDirections:(UIPopoverArrowDirection)permittedArrowDirections animated:animated];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _presentPopoverFromBarButtonItem(int tag, int item, int permittedArrowDirections, bool animated)
	{
        UIPopoverController *popcontroller = [MHTools getActualObject:tag];
        if(popcontroller)
        {
            [popcontroller presentPopoverFromBarButtonItem:[MHTools getActualObject:item] permittedArrowDirections:(UIPopoverArrowDirection)permittedArrowDirections animated:animated];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _dismissPopoverAnimated(int tag, bool animated)
	{
        UIPopoverController *popcontroller = [MHTools getActualObject:tag];
        if(popcontroller)
        {
            [popcontroller dismissPopoverAnimated:animated];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	const char * _popoverLayoutMargins(int tag)
	{
        UIPopoverController *popcontroller = [MHTools getActualObject:tag];
        if(popcontroller)
        {
            return [MHTools convertUIEdgeInsetToVector4:popcontroller.popoverLayoutMargins];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	void _popoverLayoutMargins_set(int tag, const char * edgeInsets)
	{
        UIPopoverController *popcontroller = [MHTools getActualObject:tag];
        if(popcontroller)
        {
            popcontroller.popoverLayoutMargins = [MHTools convertVector4ToUIEdgeInset:edgeInsets];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
}
