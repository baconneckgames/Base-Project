//
//  iOSManager+Control.m
//  Unity-iPhone
//
//  Created by Matthew Scuderi on 9/10/13.
//
//

#import "iOSViewManager+Control.h"
#define CANNOT_FIND_ERROR @"ERROR: Cannot find UIControl"

@implementation iOSViewManager (Control)

@end

extern "C"
{
#pragma mark - control_base
    int _init_control(int tag)
	{
		NSLog(@"YOU CANNOT INIT A UICONTROL, IT IS A BASE CLASS");
        return 0;
	}
	
	int _initWithFrame_control(int tag, const char* aRect)
	{
		NSLog(@"YOU CANNOT INIT A UICONTROL, IT IS A BASE CLASS");
        return 0;
	}
    
    void _sendActionsForControlEvents(int tag, int controlEvents)
	{
		UIControl *ctrl = [MHTools getActualObject:tag];
        
        if(ctrl)
        {
            [[iOSActionManager sharedManager] performActionsForControlState:ctrl event:(UIControlEvents)controlEvents];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
    void _addActionForControlEvents(int tag, int action, int controlEvents)
    {
        UIControl *ctrl = [MHTools getObject:tag];
        
        if(ctrl)
        {
            [[iOSActionManager sharedManager] setActionID:ctrl action:action event:(UIControlEvents)controlEvents];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _removeActionForControlEvents(int tag, int action, int controlEvents)
	{
		UIControl *ctrl = [MHTools getObject:tag];
        
        if(ctrl)
        {
            [[iOSActionManager sharedManager] removeActionID:ctrl action:action event:(UIControlEvents)controlEvents];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	const char * _actionsForControlEvents(int tag, int controlEvents)
	{
		UIControl *ctrl = [MHTools getObject:tag];
        
        if(ctrl)
        {
            return [MHTools convertNSArrayToArray_int:[[iOSActionManager sharedManager] getAllActions:ctrl event:(UIControlEvents)controlEvents]];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return nil;
	}
	
    int _allControlEvents(int tag)
	{
		UIControl *ctrl = [MHTools getActualObject:tag];
        
        if(ctrl)
        {
            return (int)ctrl.allControlEvents;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return nil;
	}
	
	int _state(int tag)
	{
		UIControl *ctrl = [MHTools getActualObject:tag];
        
        if(ctrl)
        {
            return (int)ctrl.state;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return (int)UIControlStateNormal;
    }
	
	bool _selected(int tag)
	{
        UIControl *ctrl = [MHTools getActualObject:tag];
        
        if(ctrl)
        {
            return ctrl.selected;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return false;
	}
	
	void _selected_set(int tag, bool sel)
	{
		UIControl *ctrl = [MHTools getActualObject:tag];
        
        if(ctrl)
        {
            ctrl.selected = sel;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	int _contentVerticalAlignment(int tag)
	{
		UIControl *ctrl = [MHTools getActualObject:tag];
        
        if(ctrl)
        {
            return (int)ctrl.contentVerticalAlignment;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return (int)UIControlContentVerticalAlignmentCenter;
	}
	
	void _contentVerticalAlignment_set(int tag, int alignment)
	{
		UIControl *ctrl = [MHTools getActualObject:tag];
        
        if(ctrl)
        {
            ctrl.contentVerticalAlignment = (UIControlContentVerticalAlignment)alignment;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
    int _contentHorizontalAlignment(int tag)
	{
		UIControl *ctrl = [MHTools getActualObject:tag];
        
        if(ctrl)
        {
            return (int)ctrl.contentHorizontalAlignment;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return (int)UIControlContentHorizontalAlignmentCenter;
	}
	
	void _contentHorizontalAlignment_set(int tag, int alignment)
	{
		UIControl *ctrl = [MHTools getActualObject:tag];
        
        if(ctrl)
        {
            ctrl.contentHorizontalAlignment = (UIControlContentHorizontalAlignment)alignment;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}

}



