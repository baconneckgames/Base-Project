//
//  ALBScrollView.m
//  Unity-iPhone
//
//  Created by Patrick Antoine on 9/19/13.
//
//

#import "ALBScrollView.h"

@implementation ALBScrollView

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

#pragma mark UIScrollViewDelegate
- (void)scrollViewDidScroll:(UIScrollView *)scrollView
{
    int tag = [MHTools getKeyFromActual:self];
    int sView = [MHTools getKeyFromActual:scrollView];
    
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          [NSNumber numberWithInt:tag], @"tag",
                          [NSNumber numberWithInt:sView], @"scrollView",
                          nil];
    const char *jsonResult = MakeStringCopy([dict JSONString]);
    
    [iOSViewManager UnitySendInstantMessage:"scrollViewDidScroll" JSONPARAM:jsonResult];
}

- (void)scrollViewWillBeginDragging:(UIScrollView *)scrollView
{
    int tag = [MHTools getKeyFromActual:self];
    int sView = [MHTools getKeyFromActual:scrollView];
    
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          [NSNumber numberWithInt:tag], @"tag",
                          [NSNumber numberWithInt:sView], @"scrollView",
                          nil];
    const char *jsonResult = MakeStringCopy([dict JSONString]);
    
    [iOSViewManager UnitySendInstantMessage:"scrollViewWillBeginDragging" JSONPARAM:jsonResult];
}

- (void)scrollViewWillEndDragging:(UIScrollView *)scrollView withVelocity:(CGPoint)velocity targetContentOffset:(inout CGPoint *)targetContentOffset
{
    int tag = [MHTools getKeyFromActual:self];
    int sView = [MHTools getKeyFromActual:scrollView];
    NSString *vel = GetStringParamOrNil([MHTools convertCGPointToVector2:velocity]);
    NSString *target = GetStringParamOrNil([MHTools convertCGPointToVector2:*targetContentOffset]);
    
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          [NSNumber numberWithInt:tag], @"tag",
                          [NSNumber numberWithInt:sView], @"scrollView",
                          vel, @"velocity",
                          target, @"targetContentOffset",
                          nil];
    const char *jsonResult = MakeStringCopy([dict JSONString]);
    
    [iOSViewManager UnitySendInstantMessage:"scrollViewWillEndDragging" JSONPARAM:jsonResult];
}

- (void)scrollViewDidEndDragging:(UIScrollView *)scrollView willDecelerate:(BOOL)decelerate
{
    int tag = [MHTools getKeyFromActual:self];
    int sView = [MHTools getKeyFromActual:scrollView];
    bool dec = decelerate;
    
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          [NSNumber numberWithInt:tag], @"tag",
                          [NSNumber numberWithInt:sView], @"scrollView",
                          [NSNumber numberWithBool:dec], @"decelerate",
                          nil];
    const char *jsonResult = MakeStringCopy([dict JSONString]);
    
    [iOSViewManager UnitySendInstantMessage:"scrollViewDidEndDragging" JSONPARAM:jsonResult];
}

- (BOOL)scrollViewShouldScrollToTop:(UIScrollView *)scrollView
{
    int tag = [MHTools getKeyFromActual:self];
    int sView = [MHTools getKeyFromActual:scrollView];
    
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          [NSNumber numberWithInt:tag], @"tag",
                          [NSNumber numberWithInt:sView], @"scrollView",
                          nil];
    const char *jsonResult = MakeStringCopy([dict JSONString]);
    
    [iOSViewManager UnitySendInstantMessage:"scrollViewShouldScrollToTop" JSONPARAM:jsonResult];
    
    NSString *result =  GetStringParamOrNil([[iOSViewManager sharedManager] _result]);
    [iOSViewManager sharedManager]._result = "NULL";
    
    if([result isEqualToString:@"true"])
        return true;
    return false;
}

- (void)scrollViewDidScrollToTop:(UIScrollView *)scrollView
{
    int tag = [MHTools getKeyFromActual:self];
    int sView = [MHTools getKeyFromActual:scrollView];
    
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          [NSNumber numberWithInt:tag], @"tag",
                          [NSNumber numberWithInt:sView], @"scrollView",
                          nil];
    const char *jsonResult = MakeStringCopy([dict JSONString]);
    
    [iOSViewManager UnitySendInstantMessage:"scrollViewDidScrollToTop" JSONPARAM:jsonResult];
}

- (void)scrollViewWillBeginDecelerating:(UIScrollView *)scrollView
{
    int tag = [MHTools getKeyFromActual:self];
    int sView = [MHTools getKeyFromActual:scrollView];
    
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          [NSNumber numberWithInt:tag], @"tag",
                          [NSNumber numberWithInt:sView], @"scrollView",
                          nil];
    const char *jsonResult = MakeStringCopy([dict JSONString]);
    
    [iOSViewManager UnitySendInstantMessage:"scrollViewWillBeginDecelerating" JSONPARAM:jsonResult];
}

- (void)scrollViewDidEndDecelerating:(UIScrollView *)scrollView
{
    int tag = [MHTools getKeyFromActual:self];
    int sView = [MHTools getKeyFromActual:scrollView];
    
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          [NSNumber numberWithInt:tag], @"tag",
                          [NSNumber numberWithInt:sView], @"scrollView",
                          nil];
    const char *jsonResult = MakeStringCopy([dict JSONString]);
    
    [iOSViewManager UnitySendInstantMessage:"scrollViewDidEndDecelerating" JSONPARAM:jsonResult];
}

- (void)scrollViewWillBeginZooming:(UIScrollView *)scrollView withView:(UIView *)view
{
    if(![MHTools actualObjectExistsByObject:view])
        return;
    int tag = [MHTools getKeyFromActual:self];
    int sView = [MHTools getKeyFromActual:scrollView];
    int vw = [MHTools getKeyFromActual:view];
    
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          [NSNumber numberWithInt:tag], @"tag",
                          [NSNumber numberWithInt:sView], @"scrollView",
                          [NSNumber numberWithInt:vw], @"view",
                          nil];
    const char *jsonResult = MakeStringCopy([dict JSONString]);
    
    [iOSViewManager UnitySendInstantMessage:"scrollViewWillBeginZooming" JSONPARAM:jsonResult];
}

- (void)scrollViewDidEndZooming:(UIScrollView *)scrollView withView:(UIView *)view atScale:(float)scale
{
    if(![MHTools actualObjectExistsByObject:view])
        return;
    int tag = [MHTools getKeyFromActual:self];
    int sView = [MHTools getKeyFromActual:scrollView];
    int vw = [MHTools getKeyFromActual:view];
    float scl = scale;
    
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          [NSNumber numberWithInt:tag], @"tag",
                          [NSNumber numberWithInt:sView], @"scrollView",
                          [NSNumber numberWithInt:vw], @"view",
                          [NSNumber numberWithFloat:scl], @"scale",
                          nil];
    const char *jsonResult = MakeStringCopy([dict JSONString]);
    
    [iOSViewManager UnitySendInstantMessage:"scrollViewDidEndZooming" JSONPARAM:jsonResult];
}

- (void)scrollViewDidZoom:(UIScrollView *)scrollView
{
    int tag = [MHTools getKeyFromActual:self];
    int sView = [MHTools getKeyFromActual:scrollView];
    
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          [NSNumber numberWithInt:tag], @"tag",
                          [NSNumber numberWithInt:sView], @"scrollView",
                          nil];
    const char *jsonResult = MakeStringCopy([dict JSONString]);
    
    [iOSViewManager UnitySendInstantMessage:"scrollViewDidZoom" JSONPARAM:jsonResult];
}

- (void)scrollViewDidEndScrollingAnimation:(UIScrollView *)scrollView
{
    int tag = [MHTools getKeyFromActual:self];
    int sView = [MHTools getKeyFromActual:scrollView];
    
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          [NSNumber numberWithInt:tag], @"tag",
                          [NSNumber numberWithInt:sView], @"scrollView",
                          nil];
    const char *jsonResult = MakeStringCopy([dict JSONString]);
    
    [iOSViewManager UnitySendInstantMessage:"scrollViewDidEndScrollingAnimation" JSONPARAM:jsonResult];
}

- (UIView *)viewForZoomingInScrollView:(UIScrollView *)scrollView
{
    int tag = [MHTools getKeyFromActual:self];
    int sView = [MHTools getKeyFromActual:scrollView];
    
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          [NSNumber numberWithInt:tag], @"tag",
                          [NSNumber numberWithInt:sView], @"scrollView",
                          nil];
    const char *jsonResult = MakeStringCopy([dict JSONString]);
    
    [iOSViewManager UnitySendInstantMessage:"viewForZoomingInScrollView" JSONPARAM:jsonResult];
    
    NSString *result =  GetStringParamOrNil([[iOSViewManager sharedManager] _result]);
    [iOSViewManager sharedManager]._result = "NULL";
    
    int viewTag = [result intValue];
    
    return [MHTools getActualView:viewTag];
}
@end
