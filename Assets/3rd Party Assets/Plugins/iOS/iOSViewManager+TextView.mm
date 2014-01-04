//
//  iOSViewManager+TextView.m
//  Unity-iPhone
//
//  Created by Matthew Scuderi on 9/12/13.
//
//

#import "iOSViewManager+TextView.h"
#define CANNOT_FIND_ERROR @"ERROR: Cannot find UITextView"

@implementation iOSViewManager (TextView)

@end

extern "C"
{
#pragma mark - textview_xcode_output
	int _init_textview(int tag)
	{
        if(![MHTools objectExists:tag])
        {
            ALBTextView *tView = [[[ALBTextView alloc] init] autorelease];
            tView.delegate = tView;
            
            [MHTools addreplaceObject:tView key:tag actualUI:tView];
        }
        else
        {
            NSLog(@"ERROR: This tag is already taken, cannot initiate this object (TEXTVIEW)");
        }
        return tag;
	}
	
	int _initWithFrame_textview(int tag, const char* aRect)
	{
        if(!([MHTools objectExists:tag]))
        {
            CGRect frame = [MHTools convertRectToCGRect:aRect];
            ALBTextView *tView = [[[ALBTextView alloc] initWithFrame:frame] autorelease];
            tView.delegate = tView;
            
            [MHTools addreplaceObject:tView key:tag actualUI:tView];
        }
        else
        {
            NSLog(@"ERROR: This tag is already taken, cannot initiate this object (TEXTVIEW)");
        }
		return tag;
	}
	
	bool _editable(int tag)
	{
        UITextView *view = [MHTools getActualObject:tag];
        
        if(view)
        {
            return view.editable;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return false;
	}
	
	void _editable_set(int tag, bool edit)
	{
		UITextView *view = [MHTools getActualObject:tag];
        
        if(view)
        {
            view.editable = edit;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	int _dataDetectorTypes(int tag)
	{
        UITextView *tView = [MHTools getActualObject:tag];
        
        if(tView)
        {
            return (int)tView.dataDetectorTypes;
        }
        
        NSLog(CANNOT_FIND_ERROR);
		return (int)UIDataDetectorTypeAll;
	}
	
	void _dataDetectorTypes_set(int tag, int dataDetector)
	{
		UITextView *tView = [MHTools getActualObject:tag];
        
        if(tView)
        {
            [tView setDataDetectorTypes:(UIDataDetectorTypes)dataDetector];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	bool _hasText(int tag)
	{
        UITextView *tView = [MHTools getActualObject:tag];
        
        if(tView)
        {
            return tView.hasText;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return false;
	}
}
