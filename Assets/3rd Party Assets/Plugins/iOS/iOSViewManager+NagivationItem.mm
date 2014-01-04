//
//  iOSViewManager+NagivationItem.m
//  Unity-iPhone
//
//  Created by Patrick Antoine on 9/19/13.
//
//

#import "iOSViewManager+NagivationItem.h"
#define CANNOT_FIND_ERROR @"ERROR: Cannot find UINavigationItem"

@implementation iOSViewManager (NagivationItem)

@end

extern "C"
{
    int _init_navigationitem(int tag)
	{
        if(![MHTools objectExists:tag])
        {
            ALBNavigationItem *navitem = [[[ALBNavigationItem alloc] init] autorelease];
            
            [MHTools addreplaceObject:navitem key:tag actualUI:navitem];
        }
        else
        {
            NSLog(@"ERROR: This tag is already taken, cannot initiate this object (NAVIGATIONITEM)");
        }
        return tag;
	}
	
	int _initWithTitle(int tag, const char * newTitle)
	{
        if(![MHTools objectExists:tag])
        {
            ALBNavigationItem *navitem = [[[ALBNavigationItem alloc] initWithTitle:GetStringParam(newTitle)] autorelease];
            
            [MHTools addreplaceObject:navitem key:tag actualUI:navitem];
        }
        else
        {
            NSLog(@"ERROR: This tag is already taken, cannot initiate this object (NAVIGATIONITEM)");
        }
        return tag;
	}
	
	const char * _prompt(int tag)
	{
        UINavigationItem *navitem = [MHTools getActualObject:tag];
        if(navitem)
        {
            return MakeStringCopy(navitem.prompt);
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	void _prompt_set(int tag, const char * newPrompt)
	{
        UINavigationItem *navitem = [MHTools getActualObject:tag];
        if(navitem)
        {
            navitem.prompt = GetStringParamOrNil(newPrompt);
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	int _backBarButtonItem(int tag)
	{
        UINavigationItem *navitem = [MHTools getActualObject:tag];
        if(navitem)
        {
            UIBarButtonItem *barbutton = navitem.backBarButtonItem;
            return [MHTools getKeyFromActual:barbutton];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0;
	}
	
	void _backBarButtonItem_set(int tag, int item)
	{
        UINavigationItem *navitem = [MHTools getActualObject:tag];
        if(navitem)
        {
            navitem.backBarButtonItem = [MHTools getActualObject:item];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	bool _hidesBackButton(int tag)
	{
        UINavigationItem *navitem = [MHTools getActualObject:tag];
        if(navitem)
        {
            return navitem.hidesBackButton;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return false;
	}
	
	void _hidesBackButton_set(int tag, bool hides)
	{
        UINavigationItem *navitem = [MHTools getActualObject:tag];
        if(navitem)
        {
            navitem.hidesBackButton = hides;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _setHidesBackButton(int tag, bool hidesBackButton, bool animated)
	{
        UINavigationItem *navitem = [MHTools getActualObject:tag];
        if(navitem)
        {
            [navitem setHidesBackButton:hidesBackButton animated:animated];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	bool _leftItemsSupplementBackButton(int tag)
	{
        UINavigationItem *navitem = [MHTools getActualObject:tag];
        if(navitem)
        {
            return navitem.leftItemsSupplementBackButton;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return false;
	}
	
	void _leftItemsSupplementBackButton_set(int tag, bool button)
	{
        UINavigationItem *navitem = [MHTools getActualObject:tag];
        if(navitem)
        {
            navitem.leftItemsSupplementBackButton = button;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	int _titleView(int tag)
	{
        UINavigationItem *navitem = [MHTools getActualObject:tag];
        if(navitem)
        {
            UIView *title = navitem.titleView;
            return [MHTools getKeyFromActual:title];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0;
	}
	
	void _titleView_set(int tag, int view)
	{
        UINavigationItem *navitem = [MHTools getActualObject:tag];
        if(navitem)
        {
            navitem.titleView = [MHTools getActualObject:view];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	const char * _leftBarButtonItems(int tag)
	{
        UINavigationItem *navitem = [MHTools getActualObject:tag];
        if(navitem)
        {
            NSArray *leftitems = navitem.leftBarButtonItems;
            return [MHTools convertNSArrayToMultipleTags:leftitems];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return nil;
	}
	
	void _leftBarButtonItems_set(int tag, int * items)
	{
        UINavigationItem *navitem = [MHTools getActualObject:tag];
        if(navitem)
        {
            navitem.leftBarButtonItems = [MHTools convertMultipleTagsToNSArray:items];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	int _leftBarButtonItem(int tag)
	{
        UINavigationItem *navitem = [MHTools getActualObject:tag];
        if(navitem)
        {
            UIBarButtonItem *leftitem = navitem.leftBarButtonItem;
            return [MHTools getKeyFromActual:leftitem];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0;
	}
	
	void _leftBarButtonItem_set(int tag, int item)
	{
        UINavigationItem *navitem = [MHTools getActualObject:tag];
        if(navitem)
        {
            navitem.leftBarButtonItem = [MHTools getActualObject:item];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	const char * _rightBarButtonItems(int tag)
	{
        UINavigationItem *navitem = [MHTools getActualObject:tag];
        if(navitem)
        {
            NSArray *rightitems = navitem.rightBarButtonItems;
            return [MHTools convertNSArrayToMultipleTags:rightitems];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return nil;
	}
	
	void _rightBarButtonItems_set(int tag, int * items)
	{
        UINavigationItem *navitem = [MHTools getActualObject:tag];
        if(navitem)
        {
            navitem.rightBarButtonItems = [MHTools convertMultipleTagsToNSArray:items];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	int _rightBarButtonItem(int tag)
	{
        UINavigationItem *navitem = [MHTools getActualObject:tag];
        if(navitem)
        {
            UIBarButtonItem *rightitem = navitem.rightBarButtonItem;
            return [MHTools getKeyFromActual:rightitem];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0;
	}
	
	void _rightBarButtonItem_set(int tag, int item)
	{
        UINavigationItem *navitem = [MHTools getActualObject:tag];
        if(navitem)
        {
            navitem.rightBarButtonItem = [MHTools getActualObject:item];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _setLeftBarButtonItems(int tag, int * items, bool animated)
	{
        UINavigationItem *navitem = [MHTools getActualObject:tag];
        if(navitem)
        {
            [navitem setLeftBarButtonItems:[MHTools convertMultipleTagsToNSArray:items] animated:animated];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _setLeftBarButtonItem(int tag, int item, bool animated)
	{
        UINavigationItem *navitem = [MHTools getActualObject:tag];
        if(navitem)
        {
            [navitem setLeftBarButtonItem:[MHTools getActualObject:item] animated:animated];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _setRightBarButtonItems(int tag, int * items, bool animated)
	{
        UINavigationItem *navitem = [MHTools getActualObject:tag];
        if(navitem)
        {
            [navitem setRightBarButtonItems:[MHTools convertMultipleTagsToNSArray:items] animated:animated];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _setRightBarButtonItem(int tag, int item, bool animated)
	{
        UINavigationItem *navitem = [MHTools getActualObject:tag];
        if(navitem)
        {
            [navitem setRightBarButtonItem:[MHTools getActualObject:item] animated:animated];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
}
