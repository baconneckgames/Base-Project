//
//  iOSViewManager+ViewController_Expansion.m
//  Unity-iPhone
//
//  Created by Patrick Antoine on 9/19/13.
//
//

#import "iOSViewManager+ViewController_Expansion.h"
#define CANNOT_FIND_ERROR @"ERROR: Cannot find UIViewController"

@implementation iOSViewManager (ViewController_Expansion)

@end

extern "C"
{
    int _navigationController(int tag)
	{
        UIViewController *viewcontroller = [MHTools getActualViewController:tag];
        if(viewcontroller)
        {
            return [MHTools getKeyFromActual:viewcontroller.navigationController];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0;
	}
	
	int _navigationItem(int tag)
	{
        UIViewController *viewcontroller = [MHTools getActualViewController:tag];
        if(viewcontroller)
        {
            return [MHTools getKeyFromActual:viewcontroller.navigationItem];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0;
	}
	
	const char * _contentSizeForViewInPopover(int tag)
	{
        UIViewController *viewcontroller = [MHTools getActualViewController:tag];
        if(viewcontroller)
        {
            return [MHTools convertCGSizeToVector2:viewcontroller.contentSizeForViewInPopover];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	void _contentSizeForViewInPopover_set(int tag, const char * size)
	{
        UIViewController *viewcontroller = [MHTools getActualViewController:tag];
        if(viewcontroller)
        {
            viewcontroller.contentSizeForViewInPopover = [MHTools convertVector2ToCGSize:size];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	bool _modalInPopover(int tag)
	{
        UIViewController *viewcontroller = [MHTools getActualViewController:tag];
        if(viewcontroller)
        {
            return viewcontroller.modalInPopover;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return false;
	}
	
	void _modalInPopover_set(int tag, bool isModal)
	{
        UIViewController *viewcontroller = [MHTools getActualViewController:tag];
        if(viewcontroller)
        {
            viewcontroller.modalInPopover = isModal;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
}
