//
//  iOSViewManager+NavigationBar.m
//  Unity-iPhone
//
//  Created by Patrick Antoine on 9/19/13.
//
//

#import "iOSViewManager+NavigationBar.h"
#define CANNOT_FIND_ERROR @"ERROR: Cannot find UINavigationBar"

@implementation iOSViewManager (NavigationBar)

@end

extern "C"
{
    int _init_navigationbar(int tag)
	{
        if(![MHTools objectExists:tag])
        {
            ALBNavigationBar *navbar = [[[ALBNavigationBar alloc] init] autorelease];
            navbar.delegate = navbar;
            
            [MHTools addreplaceObject:navbar key:tag actualUI:navbar];
        }
        else
        {
            NSLog(@"ERROR: This tag is already taken, cannot initiate this object (NAVIGATIONBAR)");
        }
        return tag;
	}
	
	int _initWithFrame_navigationbar(int tag, const char * aRect)
	{
        if(![MHTools objectExists:tag])
        {
            CGRect frame = [MHTools convertRectToCGRect:aRect];
            ALBNavigationBar *navbar = [[[ALBNavigationBar alloc] initWithFrame:frame] autorelease];
            navbar.delegate = navbar;
            
            [MHTools addreplaceObject:navbar key:tag actualUI:navbar];
        }
        else
        {
            NSLog(@"ERROR: This tag is already taken, cannot initiate this object (NAVIGATIONBAR)");
        }
        return tag;
	}
	
	const char * _shadowImage(int tag)
	{
        UINavigationBar *navbar = [MHTools getActualObject:tag];
        if(navbar)
        {
            return [MHTools convertUIImageToImage:navbar.shadowImage];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	void _shadowImage_set(int tag, const char * image)
	{
        UINavigationBar *navbar = [MHTools getActualObject:tag];
        if(navbar)
        {
            navbar.shadowImage = [MHTools convertImageToUIImage:image];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _pushNavigationItem(int tag, int item, bool animated)
	{
        UINavigationBar *navbar = [MHTools getActualObject:tag];
        if(navbar)
        {
            [navbar pushNavigationItem:[MHTools getActualObject:item] animated:animated];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	int _popNavigationItem(int tag, bool animated)
	{
        UINavigationBar *navbar = [MHTools getActualObject:tag];
        if(navbar)
        {
            return [MHTools getKeyFromActual:[navbar popNavigationItemAnimated:animated]];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0;
	}
	
	void _setItems_navbar(int tag, int * items, bool animated)
	{
        UINavigationBar *navbar = [MHTools getActualObject:tag];
        if(navbar)
        {
            [navbar setItems:[MHTools convertMultipleTagsToNSArray:items] animated:animated];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	const char * _items_navbar(int tag)
	{
        UINavigationBar *navbar = [MHTools getActualObject:tag];
        if(navbar)
        {
            return [MHTools convertNSArrayToMultipleTags:navbar.items];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return nil;
	}
	
	void _items_navbar_set(int tag, int * items)
	{
        UINavigationBar *navbar = [MHTools getActualObject:tag];
        if(navbar)
        {
            navbar.items = [MHTools convertMultipleTagsToNSArray:items];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	int _topItem(int tag)
	{
        UINavigationBar *navbar = [MHTools getActualObject:tag];
        if(navbar)
        {
            return [MHTools getKeyFromActual:navbar.topItem];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0;
	}
	
	int _backItem(int tag)
	{
        UINavigationBar *navbar = [MHTools getActualObject:tag];
        if(navbar)
        {
            return [MHTools getKeyFromActual:navbar.backItem];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0;
	}
	
	const char * _backgroundImageForBarMetrics(int tag, int barMetrics)
	{
        UINavigationBar *navbar = [MHTools getActualObject:tag];
        if(navbar)
        {
            return [MHTools convertUIImageToImage:[navbar backgroundImageForBarMetrics:(UIBarMetrics)barMetrics]];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	void _setBackgroundImage_metrics(int tag, const char * backgroundImage, int barMetrics)
	{
        UINavigationBar *navbar = [MHTools getActualObject:tag];
        if(navbar)
        {
            [navbar setBackgroundImage:[MHTools convertImageToUIImage:backgroundImage] forBarMetrics:(UIBarMetrics)barMetrics];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	float _titleVerticalPositionAdjustmentForBarMetrics(int tag, int barMetrics)
	{
        UINavigationBar *navbar = [MHTools getActualObject:tag];
        if(navbar)
        {
            return [navbar titleVerticalPositionAdjustmentForBarMetrics:(UIBarMetrics)barMetrics];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0.0f;
	}
	
	void _setTitleVerticalPositionAdjustment(int tag, float adjustment, int barMetrics)
	{
        UINavigationBar *navbar = [MHTools getActualObject:tag];
        if(navbar)
        {
            [navbar setTitleVerticalPositionAdjustment:adjustment forBarMetrics:(UIBarMetrics)barMetrics];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	const char * _titleTextAttributes(int tag)
	{
        UINavigationBar *navbar = [MHTools getActualObject:tag];
        if(navbar)
        {
            return MakeStringCopy([navbar.titleTextAttributes JSONString]);
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	void _titleTextAttributes_set(int tag, const char * attributes)
	{
        UINavigationBar *navbar = [MHTools getActualObject:tag];
        if(navbar)
        {
            navbar.titleTextAttributes = [GetStringParamOrNil(attributes) objectFromJSONString];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
}
