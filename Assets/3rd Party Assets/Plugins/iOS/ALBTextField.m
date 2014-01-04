//
//  ALBTextField.m
//  Unity-iPhone
//
//  Created by Patrick Antoine on 9/19/13.
//
//

#import "ALBTextField.h"

@implementation ALBTextField

#pragma mark BaseCallbacks
- (void)animationDidStart:(NSString *)animationID context:(void *)context
{
    [ALBView animationDidStart:[MHTools getKeyFromActual:self] animationID:animationID context:context];
}

- (void)animationDidStop:(NSString *)animationID finished:(NSNumber *)finished context:(void *)context
{
    [ALBView animationDidStop:[MHTools getKeyFromActual:self] animationID:animationID finished:finished context:context];
}

- (void)didAddSubview:(UIView *)subview
{
    if(![MHTools actualObjectExistsByObject:subview])
        return;
    [ALBView didAddSubview:[MHTools getKeyFromActual:self] subView:subview];
}

- (void)willRemoveSubview:(UIView *)subview
{
    if(![MHTools actualObjectExistsByObject:subview])
        return;
    [ALBView willRemoveSubview:[MHTools getKeyFromActual:self] subView:subview];
}

- (void)willMoveToSuperview:(UIView *)newSuperview
{
    if(![MHTools actualObjectExistsByObject:newSuperview])
        return;
    [ALBView willMoveToSuperview:[MHTools getKeyFromActual:self] superView:newSuperview];
}

- (void)didMoveToSuperview
{
    [ALBView didMoveToSuperview:[MHTools getKeyFromActual:self]];
    
}

#pragma mark UITextFieldDelegate
- (BOOL)textFieldShouldBeginEditing:(UITextField *)textField
{
    int tag = [MHTools getKeyFromActual:self];
    int tField = [MHTools getKeyFromActual:textField];
    
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          [NSNumber numberWithInt:tag], @"tag",
                          [NSNumber numberWithInt:tField], @"textField",
                          nil];
    const char *jsonResult = MakeStringCopy([dict JSONString]);
    
    [iOSViewManager UnitySendInstantMessage:"textFieldShouldBeginEditing" JSONPARAM:jsonResult];
    
    NSString *result =  GetStringParamOrNil([[iOSViewManager sharedManager] _result]);
    [iOSViewManager sharedManager]._result = "NULL";
    
    if([result isEqualToString:@"true"])
        return true;
    return false;
}

- (void)textFieldDidBeginEditing:(UITextField *)textField
{
    int tag = [MHTools getKeyFromActual:self];
    int tField = [MHTools getKeyFromActual:textField];
    
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          [NSNumber numberWithInt:tag], @"tag",
                          [NSNumber numberWithInt:tField], @"textField",
                          nil];
    const char *jsonResult = MakeStringCopy([dict JSONString]);
    
    [iOSViewManager UnitySendInstantMessage:"textFieldDidBeginEditing" JSONPARAM:jsonResult];
}

- (BOOL)textFieldShouldEndEditing:(UITextField *)textField
{
    int tag = [MHTools getKeyFromActual:self];
    int tField = [MHTools getKeyFromActual:textField];
    
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          [NSNumber numberWithInt:tag], @"tag",
                          [NSNumber numberWithInt:tField], @"textField",
                          nil];
    const char *jsonResult = MakeStringCopy([dict JSONString]);
    
    [iOSViewManager UnitySendInstantMessage:"textFieldShouldEndEditing" JSONPARAM:jsonResult];
    
    NSString *result =  GetStringParamOrNil([[iOSViewManager sharedManager] _result]);
    [iOSViewManager sharedManager]._result = "NULL";
    
    if([result isEqualToString:@"true"])
        return true;
    return false;
}

- (void)textFieldDidEndEditing:(UITextField *)textField
{
    int tag = [MHTools getKeyFromActual:self];
    int tField = [MHTools getKeyFromActual:textField];
    
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          [NSNumber numberWithInt:tag], @"tag",
                          [NSNumber numberWithInt:tField], @"textField",
                          nil];
    const char *jsonResult = MakeStringCopy([dict JSONString]);
    
    [iOSViewManager UnitySendInstantMessage:"textFieldDidEndEditing" JSONPARAM:jsonResult];
}

- (BOOL)textFieldShouldClear:(UITextField *)textField
{
    int tag = [MHTools getKeyFromActual:self];
    int tField = [MHTools getKeyFromActual:textField];
    
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          [NSNumber numberWithInt:tag], @"tag",
                          [NSNumber numberWithInt:tField], @"textField",
                          nil];
    const char *jsonResult = MakeStringCopy([dict JSONString]);
    
    [iOSViewManager UnitySendInstantMessage:"textFieldShouldClear" JSONPARAM:jsonResult];
    
    NSString *result =  GetStringParamOrNil([[iOSViewManager sharedManager] _result]);
    [iOSViewManager sharedManager]._result = "NULL";
    
    if([result isEqualToString:@"true"])
        return true;
    return false;
}

- (BOOL)textFieldShouldReturn:(UITextField *)textField
{
    int tag = [MHTools getKeyFromActual:self];
    int tField = [MHTools getKeyFromActual:textField];
    
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          [NSNumber numberWithInt:tag], @"tag",
                          [NSNumber numberWithInt:tField], @"textField",
                          nil];
    const char *jsonResult = MakeStringCopy([dict JSONString]);
    
    [iOSViewManager UnitySendInstantMessage:"textFieldShouldReturn" JSONPARAM:jsonResult];
    
    NSString *result =  GetStringParamOrNil([[iOSViewManager sharedManager] _result]);
    [iOSViewManager sharedManager]._result = "NULL";
    
    if([result isEqualToString:@"true"])
        return true;
    return false;
}
@end
