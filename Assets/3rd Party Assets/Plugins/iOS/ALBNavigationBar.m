//
//  ALBNavigationBar.m
//  Unity-iPhone
//
//  Created by Patrick Antoine on 9/19/13.
//
//

#import "ALBNavigationBar.h"

@implementation ALBNavigationBar

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

#pragma mark UINavigationBarDelegate
- (BOOL)navigationBar:(UINavigationBar *)navigationBar shouldPushItem:(UINavigationItem *)item
{
    if(![MHTools actualObjectExistsByObject:item])
        return true;
    int tag = [MHTools getKeyFromActual:self];
    int nBar = [MHTools getKeyFromActual:navigationBar];
    int nItem = [MHTools getKeyFromActual:item];
    
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          [NSNumber numberWithInt:tag], @"tag",
                          [NSNumber numberWithInt:nBar], @"navigationBar",
                          [NSNumber numberWithInt:nItem], @"item",
                          nil];
    const char *jsonResult = MakeStringCopy([dict JSONString]);
    
    [iOSViewManager UnitySendInstantMessage:"shouldPushItem" JSONPARAM:jsonResult];
    
    NSString *result =  GetStringParamOrNil([[iOSViewManager sharedManager] _result]);
    [iOSViewManager sharedManager]._result = "NULL";
    
    if([result isEqualToString:@"true"])
        return true;
    return false;
}

- (void)navigationBar:(UINavigationBar *)navigationBar didPushItem:(UINavigationItem *)item
{
    if(![MHTools actualObjectExistsByObject:item])
        return;
    int tag = [MHTools getKeyFromActual:self];
    int nBar = [MHTools getKeyFromActual:navigationBar];
    int nItem = [MHTools getKeyFromActual:item];
    
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          [NSNumber numberWithInt:tag], @"tag",
                          [NSNumber numberWithInt:nBar], @"navigationBar",
                          [NSNumber numberWithInt:nItem], @"item",
                          nil];
    const char *jsonResult = MakeStringCopy([dict JSONString]);
    
    [iOSViewManager UnitySendInstantMessage:"didPushItem" JSONPARAM:jsonResult];
}

- (BOOL)navigationBar:(UINavigationBar *)navigationBar shouldPopItem:(UINavigationItem *)item
{
    if(![MHTools actualObjectExistsByObject:item])
        return true;
    int tag = [MHTools getKeyFromActual:self];
    int nBar = [MHTools getKeyFromActual:navigationBar];
    int nItem = [MHTools getKeyFromActual:item];
    
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          [NSNumber numberWithInt:tag], @"tag",
                          [NSNumber numberWithInt:nBar], @"navigationBar",
                          [NSNumber numberWithInt:nItem], @"item",
                          nil];
    const char *jsonResult = MakeStringCopy([dict JSONString]);
    
    [iOSViewManager UnitySendInstantMessage:"shouldPopItem" JSONPARAM:jsonResult];
    
    NSString *result =  GetStringParamOrNil([[iOSViewManager sharedManager] _result]);
    [iOSViewManager sharedManager]._result = "NULL";
    
    if([result isEqualToString:@"true"])
        return true;
    return false;
}

- (void)navigationBar:(UINavigationBar *)navigationBar didPopItem:(UINavigationItem *)item
{
    if(![MHTools actualObjectExistsByObject:item])
        return;
    int tag = [MHTools getKeyFromActual:self];
    int nBar = [MHTools getKeyFromActual:navigationBar];
    int nItem = [MHTools getKeyFromActual:item];
    
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          [NSNumber numberWithInt:tag], @"tag",
                          [NSNumber numberWithInt:nBar], @"navigationBar",
                          [NSNumber numberWithInt:nItem], @"item",
                          nil];
    const char *jsonResult = MakeStringCopy([dict JSONString]);
    
    [iOSViewManager UnitySendInstantMessage:"didPopItem" JSONPARAM:jsonResult];
}
@end
