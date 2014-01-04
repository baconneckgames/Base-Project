//
//  iOSViewManager+Button.m
//  Unity-iPhone
//
//  Created by Matthew Scuderi on 9/11/13.
//
//

#import "iOSViewManager+Button.h"
#define CANNOT_FIND_ERROR @"ERROR: Cannot find UIButton"

@implementation iOSViewManager (Button)

@end

extern "C"
{
#pragma mark - button
    int _init_button(int tag)
	{
        if(![MHTools objectExists:tag])
        {
            ALBButton *button = [[[ALBButton alloc] init] autorelease];
            
            [MHTools addreplaceObject:button key:tag actualUI:button];
        }
        else
        {
            NSLog(@"ERROR: This tag is already taken, cannot initiate this object (BUTTON)");
        }
        return tag;
	}
	
	int _initWithFrame_button(int tag, const char* aRect)
	{
		if(![MHTools objectExists:tag])
        {
            CGRect frame = [MHTools convertRectToCGRect:aRect];
            ALBButton *button = [[[ALBButton alloc] initWithFrame:frame] autorelease];
            
            [MHTools addreplaceObject:button key:tag actualUI:button];
        }
        else
        {
            NSLog(@"ERROR: This tag is already taken, cannot initiate this object (BUTTON)");
        }
        return tag;
	}
    
    int _buttonWithType(int tag, int type)
    {
        if(![MHTools objectExists:tag])
        {
            ALBButton *button = [ALBButton buttonWithType:(UIButtonType)type];
            
            [MHTools addreplaceObject:button key:tag actualUI:button];
        }
        else
        {
            NSLog(@"ERROR: This tag is already taken, cannot initiate this object. (BUTTON)");
        }
        return tag;
    }
    
    int _titleLabel(int tag)
    {
        UIButton *button = [MHTools getActualObject:tag];
        if(button)
        {
            return [MHTools getKeyFromActual:button.titleLabel];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0;
    }
    
    bool _reversesTitleShadowWhenHighlighted(int tag)
    {
        UIButton *button = [MHTools getActualObject:tag];
        if(button)
        {
            return button.reversesTitleShadowWhenHighlighted;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return false;
    }
    
    void _reversesTitleShadowWhenHighlighted_set(int tag, bool reverse)
    {
        UIButton *button = [MHTools getActualObject:tag];
        if(button)
        {
            button.reversesTitleShadowWhenHighlighted = reverse;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
    }
    
    void _setTitle(int tag, const char* title, int state)
    {
        UIButton *button = [MHTools getActualObject:tag];
        if(button)
        {
            NSString *strTitle = GetStringParamOrNil(title);
            [button setTitle:strTitle forState:(UIControlState)state];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
    }
    
    void _setTitleColor(int tag, const char*  color, int state)
    {
        UIButton *button = [MHTools getActualObject:tag];
        if(button)
        {
            UIColor *clr = [MHTools convertColorToUIColor:color];
            [button setTitleColor:clr forState:(UIControlState)state];
            return;
        }
       
        NSLog(CANNOT_FIND_ERROR);
    }
    
    void _setTitleShadowColor(int tag, const char* color, int state)
    {
        UIButton *button = [MHTools getActualObject:tag];
        if(button)
        {
            UIColor *clr = [MHTools convertColorToUIColor:color];
            [button setTitleShadowColor:clr forState:(UIControlState)state];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
    }
    
    const char*  _titleColorForState(int tag, int state)
    {
        UIButton *button = [MHTools getActualObject:tag];
        if(button)
        {
            UIColor *titleColor = [button titleColorForState:(UIControlState)state];
            return [MHTools convertUIColorToColor:titleColor];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
    }
    
    const char*  _titleForState(int tag, int state)
    {
        UIButton *button = [MHTools getActualObject:tag];
        if(button)
        {
            return MakeStringCopy([button titleForState:(UIControlState)state]);
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
    }
    
    const char*  _titleShadowColorForState(int tag, int state)
    {
        UIButton *button = [MHTools getActualObject:tag];
        if(button)
        {
            UIColor *shadowColor = [button titleShadowColorForState:(UIControlState)state];
            return [MHTools convertUIColorToColor:shadowColor];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
    }
    
    bool _adjustsImageWhenHighlighted(int tag)
    {
        UIButton *button = [MHTools getActualObject:tag];
        if(button)
        {
            return button.adjustsImageWhenHighlighted;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return false;
    }
    
    void _adjustsImageWhenHighlighted_set(int tag, bool adjust)
    {
        UIButton *button = [MHTools getActualObject:tag];
        if(button)
        {
            button.adjustsImageWhenHighlighted = adjust;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
    }
    
    bool _adjustsImageWhenDisabled(int tag)
    {
        UIButton *button = [MHTools getActualObject:tag];
        if(button)
        {
            return button.adjustsImageWhenDisabled;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return false;
    }
    
    void _adjustsImageWhenDisabled_set(int tag, bool adjust)
    {
        UIButton *button = [MHTools getActualObject:tag];
        if(button)
        {
            button.adjustsImageWhenDisabled = adjust;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
    }
    
    bool _showsTouchWhenHighlighted(int tag)
    {
        UIButton *button = [MHTools getActualObject:tag];
        if(button)
        {
            return button.showsTouchWhenHighlighted;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return false;
    }
    
    void _showsTouchWhenHighlighted_set(int tag, bool show)
    {
        UIButton *button = [MHTools getActualObject:tag];
        if(button)
        {
            button.showsTouchWhenHighlighted = show;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
    }
    
    const char*  _backgroundImageForState_button(int tag, int state)
    {
        UIButton *button = [MHTools getActualObject:tag];
        if(button)
        {
            UIImage *bgImage = [button backgroundImageForState:(UIControlState)state];
            return [MHTools convertUIImageToImage:bgImage];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return nil;
    }
    
    const char*  _imageForState(int tag, int state)
    {
        UIButton *button = [MHTools getActualObject:tag];
        if(button)
        {
            UIImage *image = [button imageForState:(UIControlState)state];
            return [MHTools convertUIImageToImage:image];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return nil;
    }
    
    void _setBackgroundImage_button(int tag, const char*  image, int state)
    {
        UIButton *button = [MHTools getActualObject:tag];
        if(button)
        {
            UIImage *bgImage = [MHTools convertImageToUIImage:image];
            [button setBackgroundImage:bgImage forState:(UIControlState)state];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
    }
    
    void _setImage(int tag, const char*  image, int state)
    {
        UIButton *button = [MHTools getActualObject:tag];
        if(button)
        {
            UIImage *img = [MHTools convertImageToUIImage:image];
            [button setImage:img forState:(UIControlState)state];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
    }
    
    const char*  _contentEdgeInsets(int tag)
    {
        UIButton *button = [MHTools getActualObject:tag];
        if(button)
        {
            UIEdgeInsets edgeinsets = button.contentEdgeInsets;
            return [MHTools convertUIEdgeInsetToVector4:edgeinsets];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
    }
    
    void _contentEdgeInsets_set(int tag, const char*  edgeInsets)
    {
        UIButton *button = [MHTools getActualObject:tag];
        if(button)
        {
            UIEdgeInsets edgeinsets = [MHTools convertVector4ToUIEdgeInset:edgeInsets];
            button.contentEdgeInsets = edgeinsets;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
    }
    
    const char*  _titleEdgeInsets(int tag)
    {
        UIButton *button = [MHTools getActualObject:tag];
        if(button)
        {
            UIEdgeInsets edgeinsets = button.titleEdgeInsets;
            return [MHTools convertUIEdgeInsetToVector4:edgeinsets];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
    }
    
    void _titleEdgeInsets_set(int tag, const char*  edgeInsets)
    {
        UIButton *button = [MHTools getActualObject:tag];
        if(button)
        {
            UIEdgeInsets edgeinsets = [MHTools convertVector4ToUIEdgeInset:edgeInsets];
            button.titleEdgeInsets = edgeinsets;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
    }
    
    const char* _imageEdgeInsets(int tag)
    {
        UIButton *button = [MHTools getActualObject:tag];
        if(button)
        {
            UIEdgeInsets edgeinsets = button.imageEdgeInsets;
            return [MHTools convertUIEdgeInsetToVector4:edgeinsets];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
    }
    
    void _imageEdgeInsets_set(int tag, const char*  edgeInsets)
    {
        UIButton *button = [MHTools getActualObject:tag];
        if(button)
        {
            UIEdgeInsets edgeinsets = [MHTools convertVector4ToUIEdgeInset:edgeInsets];
            button.imageEdgeInsets = edgeinsets;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
    }
    
    int _buttonType(int tag)
    {
        UIButton *button = [MHTools getActualObject:tag];
        if(button)
        {
            return (int)button.buttonType;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return (int)UIButtonTypeCustom;
    }
    
    const char*  _currentTitle(int tag)
    {
        UIButton *button = [MHTools getActualObject:tag];
        if(button)
        {
            return MakeStringCopy([button currentTitle]);
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
    }
    
    const char*  _currentTitleColor(int tag)
    {
        UIButton *button = [MHTools getActualObject:tag];
        if(button)
        {
            return [MHTools convertUIColorToColor:[button currentTitleColor]];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
    }
    
    const char*  _currentTitleShadowColor(int tag)
    {
        UIButton *button = [MHTools getActualObject:tag];
        if(button)
        {
            return [MHTools convertUIColorToColor:[button currentTitleShadowColor]];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
    }
    
    const char*  _currentImage(int tag)
    {
        UIButton *button = [MHTools getActualObject:tag];
        if(button)
        {
            UIImage *image = button.currentImage;
            return [MHTools convertUIImageToImage:image];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return nil;
    }
    
    const char*  _currentBackgroundImage(int tag)
    {
        UIButton *button = [MHTools getActualObject:tag];
        if(button)
        {
            UIImage *image = button.currentImage;
            return [MHTools convertUIImageToImage:image];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return nil;
    }
    
    int _imageView(int tag)
    {
        UIButton *button = [MHTools getActualObject:tag];
        if(button)
        {
            return [MHTools getKeyFromActual:button.imageView];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0;
    }
    
    const char* _backgroundRectForBounds(int tag, const char*  bounds)
    {
        UIButton *button = [MHTools getActualObject:tag];
        if(button)
        {
            CGRect rectBounds = [MHTools convertRectToCGRect:bounds];
            CGRect bgBounds = [button backgroundRectForBounds:rectBounds];
            return [MHTools convertCGRectToRect:bgBounds];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
    }
    
    const char* _contentRectForBounds(int tag, const char*  bounds)
    {
        UIButton *button = [MHTools getActualObject:tag];
        if(button)
        {
            CGRect rectBounds = [MHTools convertRectToCGRect:bounds];
            CGRect bgBounds = [button contentRectForBounds:rectBounds];
            return [MHTools convertCGRectToRect:bgBounds];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
    }
    
    const char*  _titleRectForContentRect(int tag, const char*  contentRect)
    {
        UIButton *button = [MHTools getActualObject:tag];
        if(button)
        {
            CGRect contRect = [MHTools convertRectToCGRect:contentRect];
            CGRect titleRect = [button titleRectForContentRect:contRect];
            return [MHTools convertCGRectToRect:titleRect];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
    }
    
    const char*  _imageRectForContentRect(int tag, const char*  contentRect)
    {
        UIButton *button = [MHTools getActualObject:tag];
        if(button)
        {
            CGRect contRect = [MHTools convertRectToCGRect:contentRect];
            CGRect imageRect = [button imageRectForContentRect:contRect];
            return [MHTools convertCGRectToRect:imageRect];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
    }
}
