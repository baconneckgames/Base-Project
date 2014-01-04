//
//  ALBTextView.m
//  Unity-iPhone
//
//  Created by Matthew Scuderi on 9/13/13.
//
//

#import "ALBTextView.h"

@implementation ALBTextView

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

#pragma mark UITextViewDelegate
- (void)textViewDidBeginEditing:(UITextView *)textView
{
    int tag = [MHTools getKeyFromActual:self];
    int tView = [MHTools getKeyFromActual:textView];
    
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          [NSNumber numberWithInt:tag], @"tag",
                          [NSNumber numberWithInt:tView], @"textView",
                          nil];
    const char *jsonResult = MakeStringCopy([dict JSONString]);
    
    [iOSViewManager UnitySendInstantMessage:"textViewDidBeginEditing" JSONPARAM:jsonResult];
}

- (void)textViewDidEndEditing:(UITextView *)textView
{
    int tag = [MHTools getKeyFromActual:self];
    int tView = [MHTools getKeyFromActual:textView];
    
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          [NSNumber numberWithInt:tag], @"tag",
                          [NSNumber numberWithInt:tView], @"textView",
                          nil];
    const char *jsonResult = MakeStringCopy([dict JSONString]);
    
    [iOSViewManager UnitySendInstantMessage:"textViewDidEndEditing" JSONPARAM:jsonResult];
}

- (BOOL)textViewShouldBeginEditing:(UITextView *)textView
{
    int tag = [MHTools getKeyFromActual:self];
    int tView = [MHTools getKeyFromActual:textView];
    
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          [NSNumber numberWithInt:tag], @"tag",
                          [NSNumber numberWithInt:tView], @"textView",
                          nil];
    const char *jsonResult = MakeStringCopy([dict JSONString]);
    
    [iOSViewManager UnitySendInstantMessage:"textViewShouldBeginEditing" JSONPARAM:jsonResult];
    
    NSString *result =  GetStringParamOrNil([[iOSViewManager sharedManager] _result]);
    [iOSViewManager sharedManager]._result = "NULL";
    
    if([result isEqualToString:@"true"])
        return true;
    return false;
}

- (BOOL)textViewShouldEndEditing:(UITextView *)textView
{
    int tag = [MHTools getKeyFromActual:self];
    int tView = [MHTools getKeyFromActual:textView];
    
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          [NSNumber numberWithInt:tag], @"tag",
                          [NSNumber numberWithInt:tView], @"textView",
                          nil];
    const char *jsonResult = MakeStringCopy([dict JSONString]);
    
    [iOSViewManager UnitySendInstantMessage:"textViewShouldEndEditing" JSONPARAM:jsonResult];
    
    NSString *result =  GetStringParamOrNil([[iOSViewManager sharedManager] _result]);
    [iOSViewManager sharedManager]._result = "NULL";
    
    if([result isEqualToString:@"true"])
        return true;
    return false;
}


@end
