//
//  iOSViewManager+BarButtonItem.m
//  Unity-iPhone
//
//  Created by Matthew Scuderi on 9/11/13.
//
//  SPECIAL CASE, ALB saved under tag

#import "iOSViewManager+BarButtonItem.h"
#define CANNOT_FIND_ERROR @"ERROR: Cannot find UIBarButtonItem"

@implementation iOSViewManager (BarButtonItem)

@end

extern "C"
{
#pragma mark - bar_button_item
    int _init_barbuttonitem(int tag)
    {
        if(![MHTools objectExists:tag])
        {
            ALBBarButtonItem *barbuttonitem = [[[ALBBarButtonItem alloc] init] autorelease];
            
            [MHTools addreplaceObject:barbuttonitem key:tag actualUI:barbuttonitem];
        }
        else
        {
            NSLog(@"ERROR: This tag is already taken, cannot initiate this object (BARBUTTONITEM)");
        }
        
        return tag;
    }
    
	int _initWithBarButtonSystemItem(int tag, int systemItem, int action)
	{
		if(![MHTools objectExists:tag])
        {
            
            ALBBarButtonItem *barbuttonitem = [[[ALBBarButtonItem alloc] initWithBarButtonSystemItem:(UIBarButtonSystemItem)systemItem target:nil action:@selector(sendActionToUnity:)] autorelease];
            barbuttonitem.target = barbuttonitem;
            barbuttonitem.actionID = action;
            
            [MHTools addreplaceObject:barbuttonitem key:tag actualUI:barbuttonitem];
        }
        else
        {
            NSLog(@"ERROR: This tag is already taken, cannot initiate this object (BARBUTTONITEM)");
        }
        
        return tag;
	}
	
	int _initWithCustomView(int tag, int customView)
	{
		if(![MHTools objectExists:tag])
        {
            UIView *vw = [MHTools getActualView:customView];
            
            ALBBarButtonItem *barbuttonitem = [[[ALBBarButtonItem alloc] initWithCustomView:vw] autorelease];
            
            [MHTools addreplaceObject:barbuttonitem key:tag actualUI:barbuttonitem];
        }
        else
        {
            NSLog(@"ERROR: This tag is already taken, cannot initiate this object (BARBUTTONITEM)");
        }
        
        return tag;
	}
	
    int _initWithImage_barbutton(int tag, const char*  image, int style, int action)
	{
		if(![MHTools objectExists:tag])
        {
            UIImage *img = [MHTools convertImageToUIImage:image];
            
            ALBBarButtonItem *barbuttonitem = [[[ALBBarButtonItem alloc] initWithImage:img style:(UIBarButtonItemStyle)style target:nil action:@selector(sendActionToUnity:)] autorelease];
            barbuttonitem.target = barbuttonitem;
            barbuttonitem.actionID = action;
            
            [MHTools addreplaceObject:barbuttonitem key:tag actualUI:barbuttonitem];
        }
        else
        {
            NSLog(@"ERROR: This tag is already taken, cannot initiate this object (BARBUTTONITEM)");
        }
        
        return tag;
	}
	
	int _initWithTitle_style(int tag, const char*  newTitle, int style, int action)
	{
		if(![MHTools objectExists:tag])
        {
            NSString *title = GetStringParamOrNil(newTitle);
            
            ALBBarButtonItem *barbuttonitem = [[[ALBBarButtonItem alloc] initWithTitle:title style:(UIBarButtonItemStyle)style target:nil action:@selector(sendActionToUnity:)] autorelease];
            barbuttonitem.target = barbuttonitem;
            barbuttonitem.actionID = action;
            
            [MHTools addreplaceObject:barbuttonitem key:tag actualUI:barbuttonitem];
        }
        else
        {
            NSLog(@"ERROR: This tag is already taken, cannot initiate this object (BARBUTTONITEM)");
        }
        
        return tag;
	}
	
	int _initWithImage_landscape(int tag, const char*  image, const char*  landscapeImagePhone, int style, int action)
	{
		if(![MHTools objectExists:tag])
        {
            UIImage *img = [MHTools convertImageToUIImage:image];
            UIImage *landimg = [MHTools convertImageToUIImage:landscapeImagePhone];
            
            ALBBarButtonItem *barbuttonitem = [[[ALBBarButtonItem alloc] initWithImage:img landscapeImagePhone:landimg style:(UIBarButtonItemStyle)style target:nil action:@selector(sendActionToUnity:)] autorelease];
            barbuttonitem.target = barbuttonitem;
            barbuttonitem.actionID = action;
            
            [MHTools addreplaceObject:barbuttonitem key:tag actualUI:barbuttonitem];
        }
        else
        {
            NSLog(@"ERROR: This tag is already taken, cannot initiate this object (BARBUTTONITEM)");
        }
        
        return tag;
	}
	
	int _action(int tag)
	{
		ALBBarButtonItem *button = [MHTools getObject:tag];
        
        if(button)
        {
            return button.actionID;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0;
	}
	
	void _action_set(int tag, int action)
	{
		ALBBarButtonItem *button = [MHTools getObject:tag];
        
        if(button)
        {
            [button setActionID:action];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	int _style(int tag)
	{
		UIBarButtonItem *button = [MHTools getActualObject:tag];
        
        if(button)
        {
            return (int)button.style;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return (int)UIBarButtonItemStylePlain;
	}
	
	void _style_set(int tag, int style)
	{
		UIBarButtonItem *button = [MHTools getActualObject:tag];
        
        if(button)
        {
            button.style = (UIBarButtonItemStyle)style;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	const char * _possibleTitles(int tag) //need to find way of doing string[]
	{
		UIBarButtonItem *button = [MHTools getActualObject:tag];
        
        if(button)
        {
            NSArray *set = button.possibleTitles.allObjects;
            
            return [MHTools convertNSArrayToArray_string:set];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return NULL;
	}
	
    void _possibleTitles_set(int tag, const char* titles) //need to find way of doing string[]
	{
		UIBarButtonItem *button = [MHTools getActualObject:tag];
        
        if(button)
        {
            NSArray *set = [MHTools convertArrayToNSArray_string:titles];
            NSSet *newSet = [[NSSet alloc] init];
            [newSet setByAddingObjectsFromArray:set];
            
            button.possibleTitles = newSet;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	float _width(int tag)
	{
        UIBarButtonItem *button = [MHTools getActualObject:tag];
        
        if(button)
        {
            return (int)button.width;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0.0f;
	}
	
	void _width_set(int tag, float width)
	{
		UIBarButtonItem *button = [MHTools getActualObject:tag];
        
        if(button)
        {
            button.width = width;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	int _customView(int tag)
	{
		UIBarButtonItem *button = [MHTools getActualObject:tag];
        
        if(button)
        {
            return [MHTools getKeyFromActual:button.customView];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0;
	}
	
	void _customView_set(int tag, int view)
	{
		UIBarButtonItem *button = [MHTools getActualObject:tag];
        
        if(button)
        {
            UIView *vw = [MHTools getActualView:view];
            if(vw)
            {
                button.customView = vw;
                return;
            }
            NSLog(@"Cannot find custom view");
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	const char*  _backButtonBackgroundImageForState(int tag, int state, int barMetrics)
	{
		UIBarButtonItem *button = [MHTools getActualObject:tag];
        
        if(button)
        {
            return [MHTools convertUIImageToImage:[button backButtonBackgroundImageForState:(UIControlState)state barMetrics:(UIBarMetrics)barMetrics]];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	void _setBackButtonBackgroundImage(int tag, const char*  backgroundImage, int state, int barMetrics)
	{
		UIBarButtonItem *button = [MHTools getActualObject:tag];
        
        if(button)
        {
            UIImage *bkg = [MHTools convertImageToUIImage:backgroundImage];
            if(bkg)
            {
                [button setBackButtonBackgroundImage:bkg forState:(UIControlState)state barMetrics:(UIBarMetrics)barMetrics];
                return;
            }
            NSLog(@"Cannot find image");
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	const char*  _backButtonTitlePositionAdjustmentForBarMetrics(int tag, int barMetrics)
	{
		UIBarButtonItem *button = [MHTools getActualObject:tag];
        
        if(button)
        {
            return [MHTools convertUIOffsetToVector2:[button backButtonTitlePositionAdjustmentForBarMetrics:(UIBarMetrics)barMetrics]];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	void _setBackButtonTitlePositionAdjustment(int tag, const char*  adjustment, int barMetrics)
	{
		UIBarButtonItem *button = [MHTools getActualObject:tag];
        
        if(button)
        {
            UIOffset offset = [MHTools convertVector2ToUIOffset:adjustment];
            [button setBackButtonTitlePositionAdjustment:offset forBarMetrics:(UIBarMetrics)barMetrics];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	float _backButtonBackgroundVerticalPositionAdjustmentForBarMetrics(int tag, int barMetrics)
	{
        UIBarButtonItem *button = [MHTools getActualObject:tag];
        
        if(button)
        {
            return [button backButtonBackgroundVerticalPositionAdjustmentForBarMetrics:(UIBarMetrics)barMetrics];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0.0f;
	}
	
	void _setBackButtonBackgroundVerticalPositionAdjustment(int tag, float adjustment, int barMetrics)
	{
		UIBarButtonItem *button = [MHTools getActualObject:tag];
        
        if(button)
        {
            [button setBackButtonBackgroundVerticalPositionAdjustment:adjustment forBarMetrics:(UIBarMetrics)barMetrics];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	float _backgroundVerticalPositionAdjustmentForBarMetrics(int tag, int barMetrics)
	{
        UIBarButtonItem *button = [MHTools getActualObject:tag];
        
        if(button)
        {
            return [button backgroundVerticalPositionAdjustmentForBarMetrics:(UIBarMetrics)barMetrics];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0.0f;
	}
	
	void _setBackgroundVerticalPositionAdjustment(int tag, float adjustment, int barMetrics)
	{
		UIBarButtonItem *button = [MHTools getActualObject:tag];
        
        if(button)
        {
            [button setBackgroundVerticalPositionAdjustment:adjustment forBarMetrics:(UIBarMetrics)barMetrics];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	const char*  _backgroundImageForState_barbutton(int tag, int state, int barMetrics)
	{
		UIBarButtonItem *button = [MHTools getActualObject:tag];
        
        if(button)
        {
            return [MHTools convertUIImageToImage:[button backgroundImageForState:(UIControlState)state barMetrics:(UIBarMetrics)barMetrics]];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	void _setBackgroundImage_control(int tag, const char*  image, int state, int barMetrics)
	{
		UIBarButtonItem *button = [MHTools getActualObject:tag];
        
        if(button)
        {
            UIImage *bkg = [MHTools convertImageToUIImage:image];
            if(bkg)
            {
                [button setBackgroundImage:bkg forState:(UIControlState)state barMetrics:(UIBarMetrics)barMetrics];
                return;
            }
            NSLog(@"Cannot find image");
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	const char*  _backgroundImageForState_style(int tag, int state, int style, int barMetrics)
	{
		UIBarButtonItem *button = [MHTools getActualObject:tag];
        
        if(button)
        {
            return [MHTools convertUIImageToImage:[button backgroundImageForState:(UIControlState)state style:(UIBarButtonItemStyle)style barMetrics:(UIBarMetrics)barMetrics]];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	void _setBackgroundImage_style(int tag, const char*  image, int state, int style, int barMetrics)
	{
		UIBarButtonItem *button = [MHTools getActualObject:tag];
        
        if(button)
        {
            UIImage *bkg = [MHTools convertImageToUIImage:image];
            if(bkg)
            {
                [button setBackgroundImage:bkg forState:(UIControlState)state style:(UIBarButtonItemStyle)style barMetrics:(UIBarMetrics)barMetrics];
                return;
            }
            NSLog(@"Cannot find image");
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	const char*  _titlePositionAdjustmentForBarMetrics(int tag, int barMetrics)
	{
		UIBarButtonItem *button = [MHTools getActualObject:tag];
        
        if(button)
        {
            return [MHTools convertUIOffsetToVector2:[button titlePositionAdjustmentForBarMetrics:(UIBarMetrics)barMetrics]];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	void _setTitlePositionAdjustment(int tag, const char*  adjustment, int barMetrics)
	{
		UIBarButtonItem *button = [MHTools getActualObject:tag];
        
        if(button)
        {
            UIOffset offset = [MHTools convertVector2ToUIOffset:adjustment];
            [button setTitlePositionAdjustment:offset forBarMetrics:(UIBarMetrics)barMetrics];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
}
