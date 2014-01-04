//
//  iOSViewManager+Common.m
//  Unity-iPhone
//
//  Created by Matthew Scuderi on 9/11/13.
//
//

#import "iOSViewManager+Common.h"
#define CANNOT_FIND_ERROR @"ERROR: Cannot find NSObject"
#define INCOMPATIBLE_OBJECT @"ERROR: This object is tagged to an incompatible type"

@implementation iOSViewManager (Common)

@end

extern "C"
{
#pragma mark - navigationitem_viewcontroller_alertview_
	const char*  _title(int tag)
	{
        NSObject *obj = [MHTools getActualObject:tag];
        
        if(obj)
        {
            if([obj isKindOfClass:[UINavigationItem class]])
            {
                return MakeStringCopy([(UINavigationItem *)obj title]);
            }
            else if([obj isKindOfClass:[UIViewController class]])
            {
                return MakeStringCopy([(UIViewController *)obj title]);
            }
            else if([obj isKindOfClass:[UIAlertView class]])
            {
                return MakeStringCopy([(UIAlertView *)obj title]);
            }
            else
            {
                NSLog(INCOMPATIBLE_OBJECT);
                return "";
            }
        }
    
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	void _title_set(int tag, const char*  newTitle)
	{
		NSObject *obj = [MHTools getActualObject:tag];
        NSString *title = GetStringParamOrNil(newTitle);
        
        if(obj)
        {
            if([obj isKindOfClass:[UINavigationItem class]])
            {
                ((UINavigationItem *)obj).title = title;
            }
            else if([obj isKindOfClass:[UIViewController class]])
            {
                ((UIViewController *)obj).title = title;
            }
            else if([obj isKindOfClass:[UIAlertView class]])
            {
                ((UIAlertView *)obj).title = title;
            }
            else
            {
                NSLog(INCOMPATIBLE_OBJECT);
            }
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
#pragma mark - barbuttonitem_toolbar_navbar_button_switch
	const char*  _tintColor(int tag)
	{
		NSObject *obj = [MHTools getActualObject:tag];
        
        if(obj)
        {
            if([obj isKindOfClass:[UIBarButtonItem class]])
            {
                return [MHTools convertUIColorToColor:[(UIBarButtonItem *)obj tintColor]];
            }
            else if([obj isKindOfClass:[UIToolbar class]])
            {
                return [MHTools convertUIColorToColor:[(UIToolbar *)obj tintColor]];
            }
            else if([obj isKindOfClass:[UINavigationBar class]])
            {
                return [MHTools convertUIColorToColor:[(UINavigationBar *)obj tintColor]];
            }
            else if([obj isKindOfClass:[UIButton class]])
            {
                return [MHTools convertUIColorToColor:[(UIButton *)obj tintColor]];
            }
            else if([obj isKindOfClass:[UISwitch class]])
            {
                return [MHTools convertUIColorToColor:[(UISwitch *)obj tintColor]];
            }
            else
            {
                NSLog(INCOMPATIBLE_OBJECT);
                return "";
            }
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	void _tintColor_set(int tag, const char*  color)
	{
		NSObject *obj = [MHTools getActualObject:tag];
        UIColor *uicolor = [MHTools convertColorToUIColor:color];
        
        if(obj)
        {
            if([obj isKindOfClass:[UIBarButtonItem class]])
            {
                ((UIBarButtonItem *)obj).tintColor = uicolor;
            }
            else if([obj isKindOfClass:[UIToolbar class]])
            {
                ((UIToolbar *)obj).tintColor = uicolor;
            }
            else if([obj isKindOfClass:[UINavigationBar class]])
            {
                ((UINavigationBar *)obj).tintColor = uicolor;
            }
            else if([obj isKindOfClass:[UIButton class]])
            {
                ((UIButton *)obj).tintColor = uicolor;
            }
            else if([obj isKindOfClass:[UISwitch class]])
            {
                ((UISwitch *)obj).tintColor = uicolor;
            }
            else
            {
                NSLog(INCOMPATIBLE_OBJECT);
            }
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
#pragma mark - navbar_toolbar_
	int _barStyle(int tag)
	{
		NSObject *obj = [MHTools getActualObject:tag];
        
        if(obj)
        {
            if([obj isKindOfClass:[UINavigationBar class]])
            {
                return (int)[(UINavigationBar *)obj barStyle];
            }
            else if([obj isKindOfClass:[UIToolbar class]])
            {
                return (int)[(UIToolbar *)obj barStyle];
            }
            else
            {
                NSLog(INCOMPATIBLE_OBJECT);
                return (int)UIBarStyleDefault;
            }
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return (int)UIBarStyleDefault;
	}
	
	void _barStyle_set(int tag, int style)
	{
		NSObject *obj = [MHTools getActualObject:tag];
        UIBarStyle barstyle = (UIBarStyle)style;
        
        if(obj)
        {
            if([obj isKindOfClass:[UINavigationBar class]])
            {
                ((UINavigationBar *)obj).barStyle = barstyle;
            }
            else if([obj isKindOfClass:[UIToolbar class]])
            {
                ((UIToolbar *)obj).barStyle = barstyle;
            }
            else
            {
                NSLog(INCOMPATIBLE_OBJECT);
            }
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	bool _translucent(int tag)
	{
        NSObject *obj = [MHTools getActualObject:tag];
        
        if(obj)
        {
            if([obj isKindOfClass:[UINavigationBar class]])
            {
                return ((UINavigationBar *)obj).translucent;
            }
            else if([obj isKindOfClass:[UIToolbar class]])
            {
                return ((UIToolbar *)obj).translucent;
            }
            else
            {
                NSLog(INCOMPATIBLE_OBJECT);
                return false;
            }
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return false;
	}
	
	void _translucent_set(int tag, bool trans)
	{
		NSObject *obj = [MHTools getActualObject:tag];
        
        if(obj)
        {
            if([obj isKindOfClass:[UINavigationBar class]])
            {
                ((UINavigationBar *)obj).translucent = trans;
            }
            else if([obj isKindOfClass:[UIToolbar class]])
            {
                ((UIToolbar *)obj).translucent = trans;
            }
            else
            {
                NSLog(INCOMPATIBLE_OBJECT);
            }
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
#pragma mark - control_imageview_label_
    bool _highlighted(int tag)
	{
        NSObject *obj = [MHTools getActualObject:tag];
        
        if(obj)
        {
            if([obj isKindOfClass:[UIControl class]])
            {
                return ((UIControl *)obj).highlighted;
            }
            else if([obj isKindOfClass:[UIImageView class]])
            {
                return ((UIImageView *)obj).highlighted;
            }
            else if([obj isKindOfClass:[UILabel class]])
            {
                return ((UILabel *)obj).highlighted;
            }
            else
            {
                NSLog(INCOMPATIBLE_OBJECT);
                return false;
            }
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return false;
	}
	
	void _highlighted_set(int tag, bool highlight)
	{
		NSObject *obj = [MHTools getActualObject:tag];
        
        if(obj)
        {
            if([obj isKindOfClass:[UIControl class]])
            {
                ((UIControl *)obj).highlighted = highlight;
            }
            else if([obj isKindOfClass:[UIImageView class]])
            {
                ((UIImageView *)obj).highlighted = highlight;
            }
            else if([obj isKindOfClass:[UILabel class]])
            {
                ((UILabel *)obj).highlighted = highlight;
            }
            else
            {
                NSLog(INCOMPATIBLE_OBJECT);
            }
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
#pragma mark - control_label_
	bool _enabled(int tag)
	{
        NSObject *obj = [MHTools getActualObject:tag];
        
        if(obj)
        {
            if([obj isKindOfClass:[UIControl class]])
            {
                return ((UIControl *)obj).enabled;
            }
            else if([obj isKindOfClass:[UILabel class]])
            {
                return ((UILabel *)obj).enabled;
            }
            else
            {
                NSLog(INCOMPATIBLE_OBJECT);
                return false;
            }
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return false;
	}
	
	void _enabled_set(int tag, bool enable)
	{
		NSObject *obj = [MHTools getActualObject:tag];
        
        if(obj)
        {
            if([obj isKindOfClass:[UIControl class]])
            {
                ((UIControl *)obj).enabled = enable;
            }
            else if([obj isKindOfClass:[UILabel class]])
            {
                ((UILabel *)obj).enabled = enable;
            }
            else
            {
                NSLog(INCOMPATIBLE_OBJECT);
            }
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
#pragma mark - label_textfield_textview_
	const char* _text(int tag)
	{
        NSObject *obj = [MHTools getActualObject:tag];
        
        if(obj)
        {
            if([obj isKindOfClass:[UILabel class]])
            {
                return MakeStringCopy(((UILabel *)obj).text);
            }
            else if([obj isKindOfClass:[UITextField class]])
            {
                return MakeStringCopy(((UITextField *)obj).text);
            }
            else if([obj isKindOfClass:[UITextView class]])
            {
                return MakeStringCopy(((UITextView *)obj).text);
            }
            else
            {
                NSLog(INCOMPATIBLE_OBJECT);
                return "";
            }
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	void _text_set(int tag, const char*  txt)
	{
		NSObject *obj = [MHTools getActualObject:tag];
        NSString *text = GetStringParamOrNil(txt);
        
        if(obj)
        {
            if([obj isKindOfClass:[UILabel class]])
            {
                ((UILabel *)obj).text = text;
            }
            else if([obj isKindOfClass:[UITextField class]])
            {
                ((UITextField *)obj).text = text;
            }
            else if([obj isKindOfClass:[UITextView class]])
            {
                ((UITextView *)obj).text = text;
            }
            else
            {
                NSLog(INCOMPATIBLE_OBJECT);
            }
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	const char*  _textColor(int tag)
	{
		NSObject *obj = [MHTools getActualObject:tag];
        
        if(obj)
        {
            if([obj isKindOfClass:[UILabel class]])
            {
                return MakeStringCopy(((UILabel *)obj).text);
            }
            else if([obj isKindOfClass:[UITextField class]])
            {
                return MakeStringCopy(((UITextField *)obj).text);
            }
            else if([obj isKindOfClass:[UITextView class]])
            {
                return MakeStringCopy(((UITextView *)obj).text);
            }
            else
            {
                NSLog(INCOMPATIBLE_OBJECT);
                return "";
            }
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	void _textColor_set(int tag, const char*  color)
	{
		NSObject *obj = [MHTools getActualObject:tag];
        UIColor *uicolor = [MHTools convertColorToUIColor:color];
        
        if(obj)
        {
            if([obj isKindOfClass:[UILabel class]])
            {
                ((UILabel *)obj).textColor = uicolor;
            }
            else if([obj isKindOfClass:[UITextField class]])
            {
                ((UITextField *)obj).textColor = uicolor;
            }
            else if([obj isKindOfClass:[UITextView class]])
            {
                ((UITextView *)obj).textColor = uicolor;
            }
            else
            {
                NSLog(INCOMPATIBLE_OBJECT);
            }
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
    int _textAlignment(int tag)
	{
		NSObject *obj = [MHTools getActualObject:tag];
        
        if(obj)
        {
            if([obj isKindOfClass:[UILabel class]])
            {
                return (int)((UILabel *)obj).textAlignment;
            }
            else if([obj isKindOfClass:[UITextField class]])
            {
                return (int)((UITextField *)obj).textAlignment;
            }
            else if([obj isKindOfClass:[UITextView class]])
            {
                return (int)((UITextView *)obj).textAlignment;
            }
            else
            {
                NSLog(INCOMPATIBLE_OBJECT);
                return (int)NSTextAlignmentLeft;
            }
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return (int)NSTextAlignmentLeft;
	}
	
	void _textAlignment_set(int tag, int alignment)
	{
		NSObject *obj = [MHTools getActualObject:tag];
        NSTextAlignment align = (NSTextAlignment)alignment;
        
        if(obj)
        {
            if([obj isKindOfClass:[UILabel class]])
            {
                ((UILabel *)obj).textAlignment = align;
            }
            else if([obj isKindOfClass:[UITextField class]])
            {
                ((UITextField *)obj).textAlignment = align;
            }
            else if([obj isKindOfClass:[UITextView class]])
            {
                ((UITextView *)obj).textAlignment = align;
            }
            else
            {
                NSLog(INCOMPATIBLE_OBJECT);
            }
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
#pragma mark - label_textfield_
	bool _adjustsFontSizeToFitWidth(int tag)
	{
		NSObject *obj = [MHTools getActualObject:tag];
        
        if(obj)
        {
            if([obj isKindOfClass:[UILabel class]])
            {
                return ((UILabel *)obj).adjustsFontSizeToFitWidth;
            }
            else if([obj isKindOfClass:[UITextField class]])
            {
                return ((UITextField *)obj).adjustsFontSizeToFitWidth;
            }
            else
            {
                NSLog(INCOMPATIBLE_OBJECT);
                return false;
            }
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return false;
	}
	
	void _adjustsFontSizeToFitWidth_set(int tag, bool adjusts)
	{
		NSObject *obj = [MHTools getActualObject:tag];
        
        if(obj)
        {
            if([obj isKindOfClass:[UILabel class]])
            {
                ((UILabel *)obj).adjustsFontSizeToFitWidth = adjusts;
            }
            else if([obj isKindOfClass:[UITextField class]])
            {
                ((UITextField *)obj).adjustsFontSizeToFitWidth = adjusts;
            }
            else
            {
                NSLog(INCOMPATIBLE_OBJECT);
            }
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
#pragma mark - viewcontroller_textfield_
	bool _editing(int tag)
	{
		NSObject *obj = [MHTools getActualObject:tag];
        
        if(obj)
        {
            if([obj isKindOfClass:[UIViewController class]])
            {
                return ((UIViewController *)obj).editing;
            }
            else if([obj isKindOfClass:[UITextField class]])
            {
                return ((UITextField *)obj).editing;
            }
            else
            {
                NSLog(INCOMPATIBLE_OBJECT);
                return false;
            }
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return false;
	}
	
#pragma mark - textfield_textview_
	int _inputView(int tag)
	{
		NSObject *obj = [MHTools getActualObject:tag];
        
        if(obj)
        {
            if([obj isKindOfClass:[UITextField class]])
            {
                return [MHTools getKeyFromActual:((UITextField *)obj).inputView];
            }
            else if([obj isKindOfClass:[UITextView class]])
            {
                return [MHTools getKeyFromActual:((UITextView *)obj).inputView];
            }
            else
            {
                NSLog(INCOMPATIBLE_OBJECT);
                return 0;
            }
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0;
	}
	
	void _inputView_set(int tag, int view)
	{
		NSObject *obj = [MHTools getActualObject:tag];
        UIView * vw = [MHTools getActualObject:view];
        
        if(obj)
        {
            if([obj isKindOfClass:[UITextField class]])
            {
                ((UITextField *)obj).inputView = vw;
            }
            else if([obj isKindOfClass:[UITextView class]])
            {
                ((UITextView *)obj).inputView = vw;
            }
            else
            {
                NSLog(INCOMPATIBLE_OBJECT);
            }
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
    
	int _inputAccessoryView(int tag)
	{
		NSObject *obj = [MHTools getActualObject:tag];
        
        if(obj)
        {
            if([obj isKindOfClass:[UITextField class]])
            {
                return [MHTools getKeyFromActual:((UITextField *)obj).inputAccessoryView];
            }
            else if([obj isKindOfClass:[UITextView class]])
            {
                return [MHTools getKeyFromActual:((UITextView *)obj).inputAccessoryView];
            }
            else
            {
                NSLog(INCOMPATIBLE_OBJECT);
                return 0;
            }
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0;
	}
	
	void _inputAccessoryView_set(int tag, int view)
	{
		NSObject *obj = [MHTools getActualObject:tag];
        UIView * vw = [MHTools getActualObject:view];
        
        if(obj)
        {
            if([obj isKindOfClass:[UITextField class]])
            {
                ((UITextField *)obj).inputAccessoryView = vw;
            }
            else if([obj isKindOfClass:[UITextView class]])
            {
                ((UITextView *)obj).inputAccessoryView = vw;
            }
            else
            {
                NSLog(INCOMPATIBLE_OBJECT);
            }
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
#pragma mark - alertview_actionsheet_
	bool _visible(int tag)
	{
		NSObject *obj = [MHTools getActualObject:tag];
        
        if(obj)
        {
            if([obj isKindOfClass:[UIAlertView class]])
            {
                return ((UIAlertView *)obj).visible;
            }
            else if([obj isKindOfClass:[UIActionSheet class]])
            {
                return ((UIActionSheet *)obj).visible;
            }
            else
            {
                NSLog(INCOMPATIBLE_OBJECT);
                return false;
            }
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return false;
	}
	
	int _numberOfButtons(int tag)
	{
        NSObject *obj = [MHTools getActualObject:tag];
        
        if(obj)
        {
            if([obj isKindOfClass:[UIAlertView class]])
            {
                return ((UIAlertView *)obj).numberOfButtons;
            }
            else if([obj isKindOfClass:[UIActionSheet class]])
            {
                return ((UIActionSheet *)obj).numberOfButtons;
            }
            else
            {
                NSLog(INCOMPATIBLE_OBJECT);
                return 0;
            }
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0;
	}
	
	int _cancelButtonIndex(int tag)
	{
        NSObject *obj = [MHTools getActualObject:tag];
        
        if(obj)
        {
            if([obj isKindOfClass:[UIAlertView class]])
            {
                return ((UIAlertView *)obj).cancelButtonIndex;
            }
            else if([obj isKindOfClass:[UIActionSheet class]])
            {
                return ((UIActionSheet *)obj).cancelButtonIndex;
            }
            else
            {
                NSLog(INCOMPATIBLE_OBJECT);
                return 0;
            }
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0;
	}
	
	void _cancelButtonIndex_set(int tag, int index)
	{
		NSObject *obj = [MHTools getActualObject:tag];
        
        if(obj)
        {
            if([obj isKindOfClass:[UIAlertView class]])
            {
                ((UIAlertView *)obj).cancelButtonIndex = index;
            }
            else if([obj isKindOfClass:[UIActionSheet class]])
            {
                ((UIActionSheet *)obj).cancelButtonIndex = index;
            }
            else
            {
                NSLog(INCOMPATIBLE_OBJECT);
            }
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	int _firstOtherButtonIndex(int tag)
	{
        NSObject *obj = [MHTools getActualObject:tag];
        
        if(obj)
        {
            if([obj isKindOfClass:[UIAlertView class]])
            {
                return ((UIAlertView *)obj).firstOtherButtonIndex;
            }
            else if([obj isKindOfClass:[UIActionSheet class]])
            {
                return ((UIActionSheet *)obj).firstOtherButtonIndex;
            }
            else
            {
                NSLog(INCOMPATIBLE_OBJECT);
                return 0;
            }
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0;
	}
#pragma mark switch_slider_
    const char * _thumbTintColor(int tag)
	{
        NSObject *obj = [MHTools getActualObject:tag];
        
        if(obj)
        {
            if([obj isKindOfClass:[UISwitch class]])
            {
                return [MHTools convertUIColorToColor:((UISwitch *)obj).thumbTintColor];
            }
            else if([obj isKindOfClass:[UISlider class]])
            {
                return [MHTools convertUIColorToColor:((UISlider *)obj).thumbTintColor];
            }
            else
            {
                NSLog(INCOMPATIBLE_OBJECT);
                return "";
            }
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	void _thumbTintColor_set(int tag, const char * color)
	{
		NSObject *obj = [MHTools getActualObject:tag];
        
        if(obj)
        {
            if([obj isKindOfClass:[UISwitch class]])
            {
                ((UISwitch *)obj).thumbTintColor = [MHTools convertColorToUIColor:color];
            }
            else if([obj isKindOfClass:[UISlider class]])
            {
                ((UISlider *)obj).thumbTintColor = [MHTools convertColorToUIColor:color];
            }
            else
            {
                NSLog(INCOMPATIBLE_OBJECT);
            }
            return ;
        }
        NSLog(CANNOT_FIND_ERROR);
	}
#pragma mark imageview_activityindicator_
	void _startAnimating(int tag)
	{
        UIImageView *imageview = [MHTools getActualObject:tag];
        if(imageview)
        {
            [imageview startAnimating];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _stopAnimating(int tag)
	{
        UIImageView *imageview = [MHTools getActualObject:tag];
        if(imageview)
        {
            [imageview stopAnimating];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	bool _isAnimating(int tag)
	{
        UIImageView *imageview = [MHTools getActualObject:tag];
        if(imageview)
        {
            return imageview.isAnimating;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return false;
	}
}
