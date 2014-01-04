//
//  iOSViewManager+ActionSheet.m
//  Unity-iPhone
//
//  Created by Matthew Scuderi on 9/11/13.
//
//

#import "iOSViewManager+ActionSheet.h"
#define CANNOT_FIND_ERROR @"ERROR: Cannot find UIActionSheet"

@implementation iOSViewManager (ActionSheet)

@end

extern "C"
{
#pragma mark - actionsheet_xcode_output
    int _init_actionsheet(int tag)
    {
        if(![MHTools objectExists:tag])
        {
            ALBActionSheet *actionsheet = [[[ALBActionSheet alloc] init] autorelease];
            actionsheet.delegate = actionsheet;
            
            [MHTools addreplaceObject:actionsheet key:tag actualUI:actionsheet];
        }
        else
        {
            NSLog(@"ERROR: This tag is already taken, cannot initiate this object (ACTIONSHEET)");
        }
        
        return tag;
    }
    
    int _initWithFrame_actionsheet(int tag, const char* aRect)
    {
        if(![MHTools objectExists:tag])
        {
            CGRect cgRect = [MHTools convertRectToCGRect:aRect];
            
            ALBActionSheet *actionsheet = [[[ALBActionSheet alloc] initWithFrame:cgRect] autorelease];
            actionsheet.delegate = actionsheet;
            
            [MHTools addreplaceObject:actionsheet key:tag actualUI:actionsheet];
        }
        else
        {
            NSLog(@"ERROR: This tag is already taken, cannot initiate this object (ACTIONSHEET)");
        }
        
        return tag;
    }
    
	int _initWithTitle_actionsheet(int tag, const char*  title, const char*  cancelButtonTitle, const char*  destructiveButtonTitle, const char* otherButtonTitles)
	{
        NSArray *buttonarray=[MHTools convertArrayToNSArray_string:otherButtonTitles];
        
        if(![MHTools objectExists:tag])
        {
            ALBActionSheet *actionsheet = [[[ALBActionSheet alloc] initWithTitle:GetStringParamOrNil(title) delegate:nil cancelButtonTitle:GetStringParamOrNil(cancelButtonTitle) destructiveButtonTitle:GetStringParamOrNil(destructiveButtonTitle) otherButtonTitles:nil] autorelease];
            for (id buttonTitle in buttonarray) {
                [actionsheet addButtonWithTitle:buttonTitle];
            }
            actionsheet.delegate = actionsheet;
            
            [MHTools addreplaceObject:actionsheet key:tag actualUI:actionsheet];
        }
        else
        {
            NSLog(@"ERROR: This tag is already taken, cannot initiate this object (ACTIONSHEET)");
        }
        
        return tag;
	}
	
	int _actionSheetStyle(int tag)
	{
		UIActionSheet *as = [MHTools getActualObject:tag];
        
        if(as)
        {
            return (int)as.actionSheetStyle;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return (int)UIActionSheetStyleDefault;
    }
	
	void _actionSheetStyle_set(int tag, int style)
	{
		UIActionSheet *as = [MHTools getActualObject:tag];
        
        if(as)
        {
            as.actionSheetStyle = (UIActionSheetStyle)style;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	int _addButtonWithTitle_actionsheet(int tag, const char*  title)
	{
        UIActionSheet *as = [MHTools getActualObject:tag];
        if(as)
        {
            NSString *stringTitle = GetStringParamOrNil(title);
            return [as addButtonWithTitle:stringTitle];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0;
	}
	
	const char* _buttonTitleAtIndex_actionsheet(int tag, int buttonIndex)
	{
        UIActionSheet *as = [MHTools getActualObject:tag];
        
        if(as)
        {
            return MakeStringCopy([as buttonTitleAtIndex:buttonIndex]);
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	int _destructiveButtonIndex(int tag)
	{
        UIActionSheet *as = [MHTools getActualObject:tag];
        
        if(as)
        {
            return as.destructiveButtonIndex;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0;
	}
	
	void _destructiveButtonIndex_set(int tag, int index)
	{
		UIActionSheet *as = [MHTools getActualObject:tag];
        
        if(as)
        {
            as.destructiveButtonIndex = index;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _showFromToolbar(int tag, int view)
	{
		UIActionSheet *as = [MHTools getActualObject:tag];
        
        if(as)
        {
            UIToolbar *toolBar = [MHTools getActualObject:view];
            if(!toolBar)
            {
                NSLog(@"ERROR: Toolbar not found.");
                return;
            }
            
            [as showFromToolbar:toolBar];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _showInView(int tag, int view)
	{
		UIActionSheet *as = [MHTools getActualObject:tag];
        
        if(as)
        {
            UIView *fromView = [MHTools getActualView:view];
            if(!fromView)
            {
                NSLog(@"ERROR: View not found.");
                return;
            }
            
            [as showInView:fromView];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _showFromBarButtonItem(int tag, int item, bool animated)
	{
		UIActionSheet *as = [MHTools getActualObject:tag];
        
        if(as)
        {
            UIBarButtonItem *barButtonItem = [MHTools getActualObject:item];
            if(!barButtonItem)
            {
                NSLog(@"ERROR: BarButtonItem notfound");
                return;
            }
            
            [as showFromBarButtonItem:barButtonItem animated:animated];
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _showFromRect(int tag, const char*  rect, int view, bool animated)
	{
		UIActionSheet *as = [MHTools getActualObject:tag];
        
        if(as)
        {
            CGRect cgRect = [MHTools convertRectToCGRect:rect];
            UIView *fromView = [MHTools getActualView:view];
            [as showFromRect:cgRect inView:fromView animated:animated];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _dismissWithClickedButtonIndex_actionsheet(int tag, int buttonIndex, bool animated)
	{
		UIActionSheet *as = [MHTools getActualObject:tag];
        
        if(as)
        {
            [as dismissWithClickedButtonIndex:buttonIndex animated:animated];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
}

