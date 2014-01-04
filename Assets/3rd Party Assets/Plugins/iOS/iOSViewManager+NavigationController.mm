//
//  iOSViewManager+NavigationController.m
//  Unity-iPhone
//
//  Created by Patrick Antoine on 9/19/13.
//
//

#import "iOSViewManager+NavigationController.h"
#define CANNOT_FIND_ERROR @"ERROR: Cannot find UINavigationController"

@implementation iOSViewManager (NavigationController)

@end

extern "C"
{
    int _init_navigationcontroller(int tag)
	{
        if(![MHTools objectExists:tag])
        {
            ALBNavigationController *navcontroller = [[[ALBNavigationController alloc] init] autorelease];
            navcontroller.delegate = navcontroller;
            
            [MHTools addreplaceObject:navcontroller key:tag actualUI:navcontroller];
        }
        else
        {
            NSLog(@"ERROR: This tag is already taken, cannot initiate this object (NAVIGATIONCONTROLLER)");
        }
        return tag;
	}
	
	int _initWithRootViewController(int tag, int rootViewController)
	{
        if(![MHTools objectExists:tag])
        {
            UIViewController *rootVC = [MHTools getActualViewController:rootViewController];
            ALBNavigationController *navcontroller = [[[ALBNavigationController alloc] initWithRootViewController:rootVC] autorelease];
            navcontroller.delegate = navcontroller;
            
            [MHTools addreplaceObject:navcontroller key:tag actualUI:navcontroller];
        }
        else
        {
            NSLog(@"ERROR: This tag is already taken, cannot initiate this object (NAVIGATIONCONTROLLER)");
        }
        return tag;
	}
	
	int _topViewController(int tag)
	{
        UINavigationController *navcontroller = [MHTools getActualObject:tag];
        if(navcontroller)
        {
            UIViewController *vc = navcontroller.topViewController;
            return [MHTools getKeyFromActual:vc];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0;
	}
	
	int _visibleViewController(int tag)
	{
        UINavigationController *navcontroller = [MHTools getActualObject:tag];
        if(navcontroller)
        {
            UIViewController *vc = navcontroller.visibleViewController;
            return [MHTools getKeyFromActual:vc];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0;
	}
	
	const char * _viewControllers(int tag)
	{
        UINavigationController *navcontroller = [MHTools getActualObject:tag];
        if(navcontroller)
        {
            NSArray *vc = navcontroller.viewControllers;
            return [MHTools convertNSArrayToMultipleTags:vc];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return nil;
	}
	
	void _viewControllers_set(int tag, int * viewControllers)
	{
        UINavigationController *navcontroller = [MHTools getActualObject:tag];
        if(navcontroller)
        {
            NSArray *vc = [MHTools convertMultipleTagsToNSArray:viewControllers];
            navcontroller.viewControllers = vc;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _setViewControllers(int tag, int * viewControllers, bool animated)
	{
        UINavigationController *navcontroller = [MHTools getActualObject:tag];
        if(navcontroller)
        {
            NSArray *vc = [MHTools convertMultipleTagsToNSArray:viewControllers];
            [navcontroller setViewControllers:vc animated:animated];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _pushViewController(int tag, int viewController, bool animated)
	{
        UINavigationController *navcontroller = [MHTools getActualObject:tag];
        if(navcontroller)
        {
            UIViewController *vc = [MHTools getActualViewController:viewController];
            [navcontroller pushViewController:vc animated:animated];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	int _popViewControllerAnimated(int tag, bool animated)
	{
        UINavigationController *navcontroller = [MHTools getActualObject:tag];
        if(navcontroller)
        {
            return [MHTools getKeyFromActual:[navcontroller popViewControllerAnimated:animated]];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0;
	}
	
	const char * _popToRootViewControllerAnimated(int tag, bool animated)
	{
        UINavigationController *navcontroller = [MHTools getActualObject:tag];
        if(navcontroller)
        {
            return [MHTools convertNSArrayToMultipleTags:[navcontroller popToRootViewControllerAnimated:animated]];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return nil;
	}
	
	const char * _popToViewController(int tag, int viewController, bool animated)
	{
        UINavigationController *navcontroller = [MHTools getActualObject:tag];
        if(navcontroller)
        {
            return [MHTools convertNSArrayToMultipleTags:[navcontroller popToViewController:[MHTools getActualViewController:viewController] animated:animated]];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return nil;
	}
	
	int _navigationBar(int tag)
	{
        UINavigationController *navcontroller = [MHTools getActualObject:tag];
        if(navcontroller)
        {
            return [MHTools getKeyFromActual:navcontroller.navigationBar];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0;
	}
	
	bool _navigationBarHidden(int tag)
	{
        UINavigationController *navcontroller = [MHTools getActualObject:tag];
        if(navcontroller)
        {
            return navcontroller.navigationBarHidden;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return false;
	}
	
	void _navigationBarHidden_set(int tag, bool hidden)
	{
        UINavigationController *navcontroller = [MHTools getActualObject:tag];
        if(navcontroller)
        {
            navcontroller.navigationBarHidden = hidden;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _setNavigationBarHidden(int tag, bool hidden, bool animated)
	{
        UINavigationController *navcontroller = [MHTools getActualObject:tag];
        if(navcontroller)
        {
            [navcontroller setNavigationBarHidden:hidden animated:animated];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	int _toolbar(int tag)
	{
        UINavigationController *navcontroller = [MHTools getActualObject:tag];
        if(navcontroller)
        {
            return [MHTools getKeyFromActual:navcontroller.toolbar];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0;
	}
	
	void _setToolbarHidden(int tag, bool hidden, bool animated)
	{
        UINavigationController *navcontroller = [MHTools getActualObject:tag];
        if(navcontroller)
        {
            [navcontroller setToolbarHidden:hidden animated:animated];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	bool _toolbarHidden(int tag)
	{
        UINavigationController *navcontroller = [MHTools getActualObject:tag];
        if(navcontroller)
        {
            return navcontroller.toolbarHidden;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return false;
	}
	
	void _toolbarHidden_set(int tag, bool hidden)
	{
        UINavigationController *navcontroller = [MHTools getActualObject:tag];
        if(navcontroller)
        {
            navcontroller.toolbarHidden = hidden;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
}
