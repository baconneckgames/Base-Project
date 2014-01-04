//
//  ALBActionSheet.m
//  Unity-iPhone
//
//  Created by Patrick Antoine on 9/12/13.
//
//

#import "ALBActionSheet.h"

@implementation ALBActionSheet

#pragma mark UIActionSheetDelegate
- (void)actionSheet:(UIActionSheet *)actionSheet clickedButtonAtIndex:(NSInteger)buttonIndex
{
    int tag = [MHTools getKeyFromActual:self];
    int aSheet = [MHTools getKeyFromActual:actionSheet];
    int bIndex = buttonIndex;
    
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          [NSNumber numberWithInt:tag], @"tag",
                          [NSNumber numberWithInt:aSheet], @"actionSheet",
                          [NSNumber numberWithInt:bIndex], @"buttonIndex",
                          nil];
    const char *jsonResult = MakeStringCopy([dict JSONString]);
    
    [iOSViewManager UnitySendInstantMessage:"actionSheetClickedButtonAtIndex" JSONPARAM:jsonResult];
}

- (void)actionSheet:(UIActionSheet *)actionSheet didDismissWithButtonIndex:(NSInteger)buttonIndex
{
    int tag = [MHTools getKeyFromActual:self];
    int aSheet = [MHTools getKeyFromActual:actionSheet];
    int bIndex = buttonIndex;
    
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          [NSNumber numberWithInt:tag], @"tag",
                          [NSNumber numberWithInt:aSheet], @"actionSheet",
                          [NSNumber numberWithInt:bIndex], @"buttonIndex",
                          nil];
    const char *jsonResult = MakeStringCopy([dict JSONString]);
    
    [iOSViewManager UnitySendInstantMessage:"actionSheetDidDismissWithButtonIndex" JSONPARAM:jsonResult];
}

- (void)actionSheet:(UIActionSheet *)actionSheet willDismissWithButtonIndex:(NSInteger)buttonIndex
{
    int tag = [MHTools getKeyFromActual:self];
    int aSheet = [MHTools getKeyFromActual:actionSheet];
    int bIndex = buttonIndex;
    
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          [NSNumber numberWithInt:tag], @"tag",
                          [NSNumber numberWithInt:aSheet], @"actionSheet",
                          [NSNumber numberWithInt:bIndex], @"buttonIndex",
                          nil];
    const char *jsonResult = MakeStringCopy([dict JSONString]);
    
    [iOSViewManager UnitySendInstantMessage:"actionSheetWillDismissWithButtonIndex" JSONPARAM:jsonResult];
}

- (void)actionSheetCancel:(UIActionSheet *)actionSheet
{
    int tag = [MHTools getKeyFromActual:self];
    int aSheet = [MHTools getKeyFromActual:actionSheet];
    
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          [NSNumber numberWithInt:tag], @"tag",
                          [NSNumber numberWithInt:aSheet], @"actionSheet",
                          nil];
    const char *jsonResult = MakeStringCopy([dict JSONString]);
    
    [iOSViewManager UnitySendInstantMessage:"actionSheetCancel" JSONPARAM:jsonResult];

}

- (void)didPresentActionSheet:(UIActionSheet *)actionSheet
{
    int tag = [MHTools getKeyFromActual:self];
    int aSheet = [MHTools getKeyFromActual:actionSheet];
    
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          [NSNumber numberWithInt:tag], @"tag",
                          [NSNumber numberWithInt:aSheet], @"actionSheet",
                          nil];
    const char *jsonResult = MakeStringCopy([dict JSONString]);
    
    [iOSViewManager UnitySendInstantMessage:"didPresentActionSheet" JSONPARAM:jsonResult];
}

- (void)willPresentActionSheet:(UIActionSheet *)actionSheet
{
    int tag = [MHTools getKeyFromActual:self];
    int aSheet = [MHTools getKeyFromActual:actionSheet];
    
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          [NSNumber numberWithInt:tag], @"tag",
                          [NSNumber numberWithInt:aSheet], @"actionSheet",
                          nil];
    const char *jsonResult = MakeStringCopy([dict JSONString]);
    
    [iOSViewManager UnitySendInstantMessage:"willPresentActionSheet" JSONPARAM:jsonResult];
}

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
@end
