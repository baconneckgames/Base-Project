//
//  iOSViewManager+PickerView.m
//  Unity-iPhone
//
//  Created by Matthew Scuderi on 9/12/13.
//
//

#import "iOSViewManager+PickerView.h"
#define CANNOT_FIND_ERROR @"ERROR: Cannot find UIPickerView"

@implementation iOSViewManager (PickerView)

@end

extern "C"
{
#pragma mark - pickerview_xcode_output
	int _init_pickerview(int tag)
	{
        if(![MHTools objectExists:tag])
        {
            ALBPickerView *pickerview = [[[ALBPickerView alloc] init] autorelease];
            pickerview.delegate = pickerview;
            pickerview.dataSource = pickerview;
            
            [MHTools addreplaceObject:pickerview key:tag actualUI:pickerview];
        }
        else
        {
            NSLog(@"ERROR: This tag is already taken, cannot initiate this object (PICKERVIEW)");
        }
        return tag;
	}
	
	int _initWithFrame_pickerview(int tag, const char*  aRect)
	{
        if(![MHTools objectExists:tag])
        {
            CGRect frame = [MHTools convertRectToCGRect:aRect];
            ALBPickerView *pickerview = [[[ALBPickerView alloc] initWithFrame:frame] autorelease];
            pickerview.delegate = pickerview;
            pickerview.dataSource = pickerview;
            
            [MHTools addreplaceObject:pickerview key:tag actualUI:pickerview];
        }
        else
        {
            NSLog(@"ERROR: This tag is already taken, cannot initiate this object (PICKERVIEW)");
        }
        return tag;
	}
	
	int _numberOfComponents(int tag)
	{
		UIPickerView *pickerview = [MHTools getActualObject:tag];
        if(pickerview)
        {
            return pickerview.numberOfComponents;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0;
	}
	
	int _numberOfRowsInComponent(int tag, int component)
	{
        UIPickerView *pickerview = [MHTools getActualObject:tag];
        if(pickerview)
        {
            return [pickerview numberOfRowsInComponent:component];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0;
    }
	
	const char* _rowSizeForComponent(int tag, int component)
	{
		UIPickerView *pickerview = [MHTools getActualObject:tag];
        if(pickerview)
        {
            return [MHTools convertCGSizeToVector2:[pickerview rowSizeForComponent:component]];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	void _reloadAllComponents(int tag)
	{
		UIPickerView *pickerview = [MHTools getActualObject:tag];
        if(pickerview)
        {
            [pickerview reloadAllComponents];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _reloadComponent(int tag, int component)
	{
		UIPickerView *pickerview = [MHTools getActualObject:tag];
        if(pickerview)
        {
            [pickerview reloadComponent:component];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _selectRow(int tag, int row, int component, bool animated)
	{
		UIPickerView *pickerview = [MHTools getActualObject:tag];
        if(pickerview)
        {
            [pickerview selectRow:row inComponent:component animated:animated];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	int _selectedRow(int tag, int component)
	{
		UIPickerView *pickerview = [MHTools getActualObject:tag];
        if(pickerview)
        {
            return [pickerview selectedRowInComponent:component];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0;
	}
	
	int _viewForRow(int tag, int row, int component)
	{
		UIPickerView *pickerview = [MHTools getActualObject:tag];
        if(pickerview)
        {
            UIView *vw = [pickerview viewForRow:row forComponent:component];
            return [MHTools getKeyFromActual:vw];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0;
	}
	
	bool _showsSelectionIndicator(int tag)
	{
		UIPickerView *pickerview = [MHTools getActualObject:tag];
        if(pickerview)
        {
            return pickerview.showsSelectionIndicator;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return false;
	}
	
	void _showsSelectionIndicator_set(int tag, bool shows)
	{
		UIPickerView *pickerview = [MHTools getActualObject:tag];
        if(pickerview)
        {
            pickerview.showsSelectionIndicator = shows;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
}
