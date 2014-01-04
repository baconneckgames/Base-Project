//
//  iOSViewManager+Label.m
//  Unity-iPhone
//
//  Created by Patrick Antoine on 9/19/13.
//
//

#import "iOSViewManager+Label.h"
#define CANNOT_FIND_ERROR @"ERROR: Cannot find UILabel"

@implementation iOSViewManager (Label)

@end

extern "C"
{
    int _init_label(int tag)
	{
        if(![MHTools objectExists:tag])
        {
            ALBLabel *label = [[[ALBLabel alloc] init] autorelease];
            
            [MHTools addreplaceObject:label key:tag actualUI:label];
        }
        else
        {
            NSLog(@"ERROR: This tag is already taken, cannot initiate this object (LABEL)");
        }
        return tag;
	}
	
	int _initWithFrame_label(int tag, const char * aRect)
	{
        if(![MHTools objectExists:tag])
        {
            CGRect frame = [MHTools convertRectToCGRect:aRect];
            ALBLabel *label = [[[ALBLabel alloc] initWithFrame:frame] autorelease];
            
            [MHTools addreplaceObject:label key:tag actualUI:label];
        }
        else
        {
            NSLog(@"ERROR: This tag is already taken, cannot initiate this object (LABEL)");
        }
        return tag;
	}
	
	int _lineBreakMode(int tag)
	{
        UILabel *label = [MHTools getActualObject:tag];
        if(label)
        {
            return (int)label.lineBreakMode;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return (int)UILineBreakModeTailTruncation;
	}
	
	void _lineBreakMode_set(int tag, int mode)
	{
        UILabel *label = [MHTools getActualObject:tag];
        if(label)
        {
            label.lineBreakMode = (UILineBreakMode)mode;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	bool _adjustsLetterSpacingToFitWidth(int tag)
	{
        UILabel *label = [MHTools getActualObject:tag];
        if(label)
        {
            return label.adjustsLetterSpacingToFitWidth;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return false;
	}
	
	void _adjustsLetterSpacingToFitWidth_set(int tag, bool adjusts)
	{
        UILabel *label = [MHTools getActualObject:tag];
        if(label)
        {
            label.adjustsLetterSpacingToFitWidth = adjusts;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	int _baselineAdjustment(int tag)
	{
        UILabel *label = [MHTools getActualObject:tag];
        if(label)
        {
            return (int)label.baselineAdjustment;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return (int)UIBaselineAdjustmentAlignBaselines;
	}
	
	void _baselineAdjustment_set(int tag, int adjustment)
	{
        UILabel *label = [MHTools getActualObject:tag];
        if(label)
        {
            label.baselineAdjustment = (UIBaselineAdjustment)adjustment;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	float _minimumScaleFactor(int tag)
	{
        UILabel *label = [MHTools getActualObject:tag];
        if(label)
        {
            return label.minimumScaleFactor;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0.0f;
	}
	
	void _minimumScaleFactor_set(int tag, float factor)
	{
        UILabel *label = [MHTools getActualObject:tag];
        if(label)
        {
            label.minimumScaleFactor = factor;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	int _numberOfLines(int tag)
	{
        UILabel *label = [MHTools getActualObject:tag];
        if(label)
        {
            return label.numberOfLines;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0;
	}
	
	void _numberOfLines_set(int tag, int lines)
	{
        UILabel *label = [MHTools getActualObject:tag];
        if(label)
        {
            label.numberOfLines = lines;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	const char * _highlightedTextColor(int tag)
	{
        UILabel *label = [MHTools getActualObject:tag];
        if(label)
        {
            return [MHTools convertUIColorToColor:label.highlightedTextColor];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	void _highlightedTextColor_set(int tag, const char * color)
	{
        UILabel *label = [MHTools getActualObject:tag];
        if(label)
        {
            label.highlightedTextColor = [MHTools convertColorToUIColor:color];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	const char * _shadowColor(int tag)
	{
        UILabel *label = [MHTools getActualObject:tag];
        if(label)
        {
            return [MHTools convertUIColorToColor:label.shadowColor];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	void _shadowColor_set(int tag, const char * color)
	{
        UILabel *label = [MHTools getActualObject:tag];
        if(label)
        {
            label.shadowColor = [MHTools convertColorToUIColor:color];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	const char * _shadowOffset(int tag)
	{
        UILabel *label = [MHTools getActualObject:tag];
        if(label)
        {
            return [MHTools convertCGSizeToVector2:label.shadowOffset];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	void _shadowOffset_set(int tag, const char * offset)
	{
        UILabel *label = [MHTools getActualObject:tag];
        if(label)
        {
            label.shadowOffset = [MHTools convertVector2ToCGSize:offset];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	const char * _textRectForBounds_label(int tag, const char * bounds, int numberOfLines)
	{
        UILabel *label = [MHTools getActualObject:tag];
        if(label)
        {
            CGRect boundsrect = [MHTools convertRectToCGRect:bounds];
            return [MHTools convertCGRectToRect:[label textRectForBounds:boundsrect limitedToNumberOfLines:numberOfLines]];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	void _drawTextInRect_label(int tag, const char * rect)
	{
        UILabel *label = [MHTools getActualObject:tag];
        if(label)
        {
            CGRect boundsrect = [MHTools convertRectToCGRect:rect];
            [label drawTextInRect:boundsrect];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	float _preferredMaxLayoutWidth(int tag)
	{
        UILabel *label = [MHTools getActualObject:tag];
        if(label)
        {
            return label.preferredMaxLayoutWidth;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0.0f;
	}
	
	void _preferredMaxLayoutWidth_set(int tag, float width)
	{
        UILabel *label = [MHTools getActualObject:tag];
        if(label)
        {
            label.preferredMaxLayoutWidth = width;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
}
