//
//  iOSViewManager+TextField.m
//  Unity-iPhone
//
//  Created by Matthew Scuderi on 9/12/13.
//
//

#import "iOSViewManager+TextField.h"
#define CANNOT_FIND_ERROR @"ERROR: Cannot find UITextField"

@implementation iOSViewManager (TextField)

@end

extern "C"
{
#pragma mark - textfield_xcode_output
	int _init_textfield(int tag)
	{
		if(![MHTools objectExists:tag])
        {
            ALBTextField *textfield = [[[ALBTextField alloc] init] autorelease];
            textfield.delegate = textfield;
            
            [MHTools addreplaceObject:textfield key:tag actualUI:textfield];
        }
        else
        {
            NSLog(@"ERROR: This tag is already taken, cannot initiate this object (TEXTFIELD)");
        }
        return tag;
	}
	
	int _initWithFrame_textfield(int tag, const char*  aRect)
	{
		if(![MHTools objectExists:tag])
        {
            CGRect frame = [MHTools convertRectToCGRect:aRect];
            ALBTextField *textfield = [[[ALBTextField alloc] initWithFrame:frame] autorelease];
            textfield.delegate = textfield;
            
            [MHTools addreplaceObject:textfield key:tag actualUI:textfield];
        }
        else
        {
            NSLog(@"ERROR: This tag is already taken, cannot initiate this object (TEXTFIELD)");
        }
        return tag;
	}
	
	const char*  _placeholder(int tag)
	{
		UITextField *textfield = [MHTools getActualObject:tag];
        if(textfield)
        {
            return MakeStringCopy(textfield.placeholder);
        }
        
        NSLog(CANNOT_FIND_ERROR);
		return "";
	}
	
	void _placeholder_set(int tag, const char* holder)
	{
		UITextField *textfield = [MHTools getActualObject:tag];
        if(textfield)
        {
            textfield.placeholder = GetStringParamOrNil(holder);
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	float _minimumFontSize(int tag)
	{
        UITextField *textfield = [MHTools getActualObject:tag];
        if(textfield)
        {
            return textfield.minimumFontSize;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0.0f;
	}
	
	void _minimumFontSize_set(int tag, float size)
	{
		UITextField *textfield = [MHTools getActualObject:tag];
        if(textfield)
        {
            textfield.minimumFontSize = size;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	bool _clearsOnBeginEditing(int tag)
	{
        UITextField *textfield = [MHTools getActualObject:tag];
        if(textfield)
        {
            return textfield.clearsOnBeginEditing;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return false;
	}
	
	void _clearsOnBeginEditing_set(int tag, bool clears)
	{
		UITextField *textfield = [MHTools getActualObject:tag];
        if(textfield)
        {
            textfield.clearsOnBeginEditing = clears;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	bool _clearsOnInsertion(int tag)
	{
		UITextField *textfield = [MHTools getActualObject:tag];
        if(textfield)
        {
            return textfield.clearsOnBeginEditing;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return false;
	}
	
	void _clearsOnInsertion_set(int tag, bool clears)
	{
		UITextField *textfield = [MHTools getActualObject:tag];
        if(textfield)
        {
            textfield.clearsOnInsertion = clears;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	int _borderStyle(int tag)
	{
		UITextField *textfield = [MHTools getActualObject:tag];
        if(textfield)
        {
            return (int)textfield.borderStyle;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return (int)UITextBorderStyleNone;
	}
	
	void _borderStyle_set(int tag, int style)
	{
		UITextField *textfield = [MHTools getActualObject:tag];
        if(textfield)
        {
            textfield.borderStyle = (UITextBorderStyle)style;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	const char*  _background(int tag)
	{
		UITextField *textfield = [MHTools getActualObject:tag];
        if(textfield)
        {
            return [MHTools convertUIImageToImage:textfield.background];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	void _background_set(int tag, const char* image)
	{
		UITextField *textfield = [MHTools getActualObject:tag];
        if(textfield)
        {
            textfield.background = [MHTools convertImageToUIImage:image];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	const char*  _disabledBackground(int tag)
	{
		UITextField *textfield = [MHTools getActualObject:tag];
        if(textfield)
        {
            return [MHTools convertUIImageToImage:textfield.disabledBackground];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	void _disabledBackground_set(int tag, const char*  image)
	{
		UITextField *textfield = [MHTools getActualObject:tag];
        if(textfield)
        {
            textfield.disabledBackground = [MHTools convertImageToUIImage:image];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	int _clearButtonMode(int tag)
	{
        UITextField *textfield = [MHTools getActualObject:tag];
        if(textfield)
        {
            return (int)textfield.clearButtonMode;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return (int)UITextFieldViewModeNever;
	}
	
	void _clearButtonMode_set(int tag, int mode)
	{
		UITextField *textfield = [MHTools getActualObject:tag];
        if(textfield)
        {
            textfield.clearButtonMode = (UITextFieldViewMode)mode;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	int _leftView(int tag)
	{
		UITextField *textfield = [MHTools getActualObject:tag];
        if(textfield)
        {
            return [MHTools getKeyFromActual:textfield.leftView];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0;
	}
	
	void _leftView_set(int tag, int view)
	{
		UITextField *textfield = [MHTools getActualObject:tag];
        if(textfield)
        {
            textfield.leftView = [MHTools getActualView:view];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	int _leftViewMode(int tag)
	{
        UITextField *textfield = [MHTools getActualObject:tag];
        if(textfield)
        {
            return (int)textfield.leftViewMode;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return (int)UITextFieldViewModeNever;
	}
	
	void _leftViewMode_set(int tag, int mode)
	{
		UITextField *textfield = [MHTools getActualObject:tag];
        if(textfield)
        {
            textfield.leftViewMode = (UITextFieldViewMode)mode;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	int _rightView(int tag)
	{
		UITextField *textfield = [MHTools getActualObject:tag];
        if(textfield)
        {
            return [MHTools getKeyFromActual:textfield.rightView];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0;
	}
	
	void _rightView_set(int tag, int view)
	{
		UITextField *textfield = [MHTools getActualObject:tag];
        if(textfield)
        {
            textfield.rightView = [MHTools getActualView:view];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	int _rightViewMode(int tag)
	{
		UITextField *textfield = [MHTools getActualObject:tag];
        if(textfield)
        {
            return (int)textfield.rightViewMode;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return (int)UITextFieldViewModeNever;
	}
	
	void _rightViewMode_set(int tag, int mode)
	{
		UITextField *textfield = [MHTools getActualObject:tag];
        if(textfield)
        {
            textfield.rightViewMode = (UITextFieldViewMode)mode;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	const char* _textRectForBounds_textfield(int tag, const char* bounds)
	{
		UITextField *textfield = [MHTools getActualObject:tag];
        if(textfield)
        {
            CGRect bound = [MHTools convertRectToCGRect:bounds];
            return [MHTools convertCGRectToRect:[textfield textRectForBounds:bound]];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	void _drawTextInRect_textfield(int tag, const char* rect)
	{
		UITextField *textfield = [MHTools getActualObject:tag];
        if(textfield)
        {
            CGRect rct = [MHTools convertRectToCGRect:rect];
            [textfield drawTextInRect:rct];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	const char* _placeholderRectForBounds(int tag, const char* bounds)
	{
		UITextField *textfield = [MHTools getActualObject:tag];
        if(textfield)
        {
            CGRect bound = [MHTools convertRectToCGRect:bounds];
            return [MHTools convertCGRectToRect:[textfield placeholderRectForBounds:bound]];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	void _drawPlaceholderInRect(int tag, const char* rect)
	{
		UITextField *textfield = [MHTools getActualObject:tag];
        if(textfield)
        {
            CGRect rct = [MHTools convertRectToCGRect:rect];
            [textfield drawPlaceholderInRect:rct];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	const char* _borderRectForBounds(int tag, const char* bounds)
	{
		UITextField *textfield = [MHTools getActualObject:tag];
        if(textfield)
        {
            CGRect bound = [MHTools convertRectToCGRect:bounds];
            return [MHTools convertCGRectToRect:[textfield borderRectForBounds:bound]];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	const char* _editingRectForBounds(int tag, const char* bounds)
	{
		UITextField *textfield = [MHTools getActualObject:tag];
        if(textfield)
        {
            CGRect bound = [MHTools convertRectToCGRect:bounds];
            return [MHTools convertCGRectToRect:[textfield editingRectForBounds:bound]];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	const char* _clearButtonRectForBounds(int tag, const char* bounds)
	{
		UITextField *textfield = [MHTools getActualObject:tag];
        if(textfield)
        {
            CGRect bound = [MHTools convertRectToCGRect:bounds];
            return [MHTools convertCGRectToRect:[textfield clearButtonRectForBounds:bound]];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	const char* _leftViewRectForBounds(int tag, const char* bounds)
	{
		UITextField *textfield = [MHTools getActualObject:tag];
        if(textfield)
        {
            CGRect bound = [MHTools convertRectToCGRect:bounds];
            return [MHTools convertCGRectToRect:[textfield leftViewRectForBounds:bound]];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	const char* _rightViewRectForBounds(int tag, const char* bounds)
	{
		UITextField *textfield = [MHTools getActualObject:tag];
        if(textfield)
        {
            CGRect bound = [MHTools convertRectToCGRect:bounds];
            return [MHTools convertCGRectToRect:[textfield rightViewRectForBounds:bound]];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
}
