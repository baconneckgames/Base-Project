//
//  iOSViewManager+AlertView.m
//  Unity-iPhone
//
//  Created by Matthew Scuderi on 9/11/13.
//
//

#import "iOSViewManager+AlertView.h"
#define CANNOT_FIND_ERROR @"ERROR: Cannot find UIAlertView"

@implementation iOSViewManager (AlertView)

@end

extern "C"
{
#pragma mark - alertview_xcode_output
    int _init_alertview(int tag)
    {
        if(![MHTools objectExists:tag])
        {
            ALBAlertView *alertview = [[[ALBAlertView alloc] init] autorelease];
            alertview.delegate = alertview;
            
            [MHTools addreplaceObject:alertview key:tag actualUI:alertview];
        }
        else
        {
            NSLog(@"ERROR: This tag is already taken, cannot initiate this object (ALERTVIEW)");
        }
        
        return tag;
    }
    
    int _initWithFrame_alertview(int tag, const char* aRect)
    {
        if(![MHTools objectExists:tag])
        {
            CGRect cgRect = [MHTools convertRectToCGRect:aRect];
            
            ALBAlertView *alertview = [[[ALBAlertView alloc] initWithFrame:cgRect] autorelease];
            alertview.delegate = alertview;
            
            [MHTools addreplaceObject:alertview key:tag actualUI:alertview];
        }
        else
        {
            NSLog(@"ERROR: This tag is already taken, cannot initiate this object (ALERTVIEW)");
        }
        
        return tag;
    }
    
	int _initWithTitle_alertview(int tag, const char*  title, const char*  message, const char*  cancelButtonTitle, const char* otherButtonTitles)
	{
		NSArray *buttonarray=[MHTools convertArrayToNSArray_string:otherButtonTitles];
        
        if(![MHTools objectExists:tag])
        {
            ALBAlertView *alertview = [[[ALBAlertView alloc] initWithTitle:GetStringParamOrNil(title) message:GetStringParamOrNil(message) delegate:nil cancelButtonTitle:GetStringParamOrNil(cancelButtonTitle) otherButtonTitles: nil] autorelease];
            for (id buttonTitle in buttonarray) {
                [alertview addButtonWithTitle:buttonTitle];
            }
            alertview.delegate = alertview;
            
            [MHTools addreplaceObject:alertview key:tag actualUI:alertview];
        }
        else
        {
            NSLog(@"ERROR: This tag is already taken, cannot initiate this object");
        }
        return tag;
	}
	
	int _alertViewStyle(int tag)
	{
		UIAlertView *aw = [MHTools getActualObject:tag];
        
        if(aw)
        {
            return (int)aw.alertViewStyle;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return (int)UIAlertViewStyleDefault;
	}
	
	void _alertViewStyle_set(int tag, int style)
	{
		UIAlertView *aw = [MHTools getActualObject:tag];
        
        if(aw)
        {
            aw.alertViewStyle = (UIAlertViewStyle)style;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	const char* _message(int tag)
	{
        UIAlertView * aw = [MHTools getActualObject:tag];
        
        if(aw)
        {
            return MakeStringCopy(aw.message);
        }
        
        NSLog(CANNOT_FIND_ERROR);
		return NULL;
	}
	
	void _message_set(int tag, const char*  msg)
	{
		UIAlertView *aw = [MHTools getActualObject:tag];
        
        if(aw)
        {
            aw.message = GetStringParamOrNil(msg);
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	int _addButtonWithTitle_alertview(int tag, const char*  title)
	{
        UIAlertView *aw = [MHTools getActualObject:tag];
        
        if(aw)
        {
            return [aw addButtonWithTitle:GetStringParamOrNil(title)];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0;
	}
	
	const char*  _buttonTitleAtIndex_alertview(int tag, int index)
	{
        UIAlertView *aw = [MHTools getActualObject:tag];
        
        if(aw)
        {
            return MakeStringCopy([aw buttonTitleAtIndex:index]);
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	int _textFieldAtIndex(int tag, int textFieldIndex)
	{
        UIAlertView *aw = [MHTools getActualObject:tag];
        
        if(aw)
        {
            return [aw textFieldAtIndex:textFieldIndex].tag;
        }
        
        NSLog(CANNOT_FIND_ERROR);
		return 0;
	}
	
	void _show(int tag)
	{
		UIAlertView *aw = [MHTools getActualObject:tag];
        
        if(aw)
        {
            [aw show];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _dismissWithClickedButtonIndex_alertview(int tag, int buttonIndex, bool animated)
	{
		UIAlertView *aw = [MHTools getActualObject:tag];
        
        if(aw)
        {
            [aw dismissWithClickedButtonIndex:buttonIndex animated:animated];
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
}
