//
//  ALBView.m
//  Unity-iPhone
//
//  Created by Patrick Antoine on 9/19/13.
//
//

#import "ALBView.h"

@implementation ALBView

- (void)animateWithDuration:(NSTimeInterval)duration delay:(NSTimeInterval)delay options:(UIViewAnimationOptions)options completion:(int)completionID
{
    [UIView animateWithDuration:duration
                          delay:delay
                        options:options
                     animations:^{}
                     completion:^(BOOL finished) {
                         [[iOSActionManager sharedManager] performActionByID:completionID param1:finished];
                     }];
}

- (void)animateWithDuration:(NSTimeInterval)duration completion:(int)completionID
{
    [UIView animateWithDuration:duration
                     animations:^{}
                     completion:^(BOOL finished) {
                         [[iOSActionManager sharedManager] performActionByID:completionID param1:finished];
                     }];
}

- (void)animateWithDuration:(NSTimeInterval)duration
{
    [UIView animateWithDuration:duration
                     animations:^{}];
}

- (void)transitionFromView:(UIView *)fromView toView:(UIView *)toView duration:(NSTimeInterval)duration options:(UIViewAnimationOptions)options completion:(int)completionID
{
    [UIView transitionFromView:fromView
                        toView:toView
                      duration:duration
                       options:options
                    completion:^(BOOL finished) {
                        [[iOSActionManager sharedManager] performActionByID:completionID param1:finished];
                    }];
}
- (void)transitionWithView:(UIView *)view duration:(NSTimeInterval)duration options:(UIViewAnimationOptions)options completion:(int)completionID
{
    [UIView transitionWithView:view
                      duration:duration
                       options:options
                    animations:^{}
                    completion:^(BOOL finished) {
                        [[iOSActionManager sharedManager] performActionByID:completionID param1:finished];
                    }];
}

- (BOOL)areAnimationsEnabled
{
    return [UIView areAnimationsEnabled];
}

- (void)beginAnimations:(NSString *)animationID context:(void *)context
{
    [UIView beginAnimations:animationID context:context];
    [UIView setAnimationDelegate:self];
    [UIView setAnimationWillStartSelector:@selector(animationDidStart:context:)];
    [UIView setAnimationDidStopSelector:@selector(animationDidStop:finished:context:)];
}

- (void)commitAnimations
{
    [UIView commitAnimations];
}

- (void)setAnimationBeginsFromCurrentState:(BOOL)fromCurrentState
{
    [UIView setAnimationBeginsFromCurrentState:fromCurrentState];
}

- (void)setAnimationCurve:(UIViewAnimationCurve)curve
{
    [UIView setAnimationCurve:curve];
}

- (void)setAnimationDelay:(NSTimeInterval)delay
{
    [UIView setAnimationDelay:delay];
}

- (void)setAnimationDuration:(NSTimeInterval)duration
{
    [UIView setAnimationDuration:duration];
}

- (void)setAnimationRepeatAutoreverses:(BOOL)repeatAutoreverses
{
    [UIView setAnimationRepeatAutoreverses:repeatAutoreverses];
}

- (void)setAnimationRepeatCount:(float)repeatCount
{
    [UIView setAnimationRepeatCount:repeatCount];
}

- (void)setAnimationsEnabled:(BOOL)enabled
{
    [UIView setAnimationsEnabled:enabled];
}

- (void)setAnimationStartDate:(NSDate *)startTime
{
    [UIView setAnimationStartDate:startTime];
}

- (void)setAnimationTransition:(UIViewAnimationTransition)transition forView:(UIView *)view cache:(BOOL)cache
{
    [UIView setAnimationTransition:transition forView:view cache:cache];
}

#pragma mark UIViewCallbacks
+ (void)animationDidStart:(int)tag animationID:(NSString *)animationID context:(void *)context
{
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          [NSNumber numberWithInt:tag], @"tag",
                          animationID, @"animationID",
                          nil];
    const char *jsonResult = MakeStringCopy([dict JSONString]);
    
    [iOSViewManager UnitySendInstantMessage:"animationDidStart" JSONPARAM:jsonResult];
}

+ (void)animationDidStop:(int)tag animationID:(NSString *)animationID finished:(NSNumber *)finished context:(void *)context
{
    int contextTag = 0;
    if(context)
        contextTag = [MHTools getKeyFromActual:context];
    
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          [NSNumber numberWithInt:tag], @"tag",
                          animationID, @"animationID",
                          [NSNumber numberWithInt:contextTag], @"context",
                          nil];
    const char *jsonResult = MakeStringCopy([dict JSONString]);
    
    [iOSViewManager UnitySendInstantMessage:"animationDidStop" JSONPARAM:jsonResult];
}

+ (void)didAddSubview:(int)tag subView:(UIView *)subview
{
    if(![MHTools actualObjectExistsByObject:subview])
        return;
    int sView = [MHTools getKeyFromActual:subview];
    
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          [NSNumber numberWithInt:tag], @"tag",
                          [NSNumber numberWithInt:sView], @"subview",
                          nil];
    const char *jsonResult = MakeStringCopy([dict JSONString]);
    
    [iOSViewManager UnitySendInstantMessage:"didAddSubview" JSONPARAM:jsonResult];
}

+ (void)willRemoveSubview:(int)tag subView:(UIView *)subview
{
    if(![MHTools actualObjectExistsByObject:subview])
        return;
    int sView = [MHTools getKeyFromActual:subview];
    
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          [NSNumber numberWithInt:tag], @"tag",
                          [NSNumber numberWithInt:sView], @"subview",
                          nil];
    const char *jsonResult = MakeStringCopy([dict JSONString]);
    
    [iOSViewManager UnitySendInstantMessage:"willRemoveSubview" JSONPARAM:jsonResult];
}

+ (void)willMoveToSuperview:(int)tag superView:(UIView *)newSuperview
{
    if(![MHTools actualObjectExistsByObject:newSuperview])
        return;
    int sView = [MHTools getKeyFromActual:newSuperview];
    
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          [NSNumber numberWithInt:tag], @"tag",
                          [NSNumber numberWithInt:sView], @"newSuperview",
                          nil];
    const char *jsonResult = MakeStringCopy([dict JSONString]);
    
    [iOSViewManager UnitySendInstantMessage:"willMoveToSuperview" JSONPARAM:jsonResult];
}

+ (void)didMoveToSuperview:(int)tag
{
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          [NSNumber numberWithInt:tag], @"tag",
                          nil];
    const char *jsonResult = MakeStringCopy([dict JSONString]);
    
    [iOSViewManager UnitySendInstantMessage:"didMoveToSuperview" JSONPARAM:jsonResult];
    
}

#pragma mark LocalCallbacks
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
