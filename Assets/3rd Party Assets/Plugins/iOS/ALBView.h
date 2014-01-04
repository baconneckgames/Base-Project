//
//  ALBView.h
//  Unity-iPhone
//
//  Created by Patrick Antoine on 9/19/13.
//
//

#import <Foundation/Foundation.h>
#import "MHTools.h"
#import "iOSActionManager.h"

@interface ALBView : UIView

+ (void)animationDidStart:(int)tag animationID:(NSString *)animationID context:(void *)context;
+ (void)animationDidStop:(int)tag animationID:(NSString *)animationID finished:(NSNumber *)finished context:(void *)context;
+ (void)didAddSubview:(int)tag subView:(UIView *)subview;
+ (void)willRemoveSubview:(int)tag subView:(UIView *)subview;
+ (void)willMoveToSuperview:(int)tag superView:(UIView *)newSuperview;
+ (void)didMoveToSuperview:(int)tag;

- (void)animateWithDuration:(NSTimeInterval)duration delay:(NSTimeInterval)delay options:(UIViewAnimationOptions)options completion:(int)completionID;
- (void)animateWithDuration:(NSTimeInterval)duration completion:(int)completionID;
- (void)animateWithDuration:(NSTimeInterval)duration;

- (void)transitionFromView:(UIView *)fromView toView:(UIView *)toView duration:(NSTimeInterval)duration options:(UIViewAnimationOptions)options completion:(int)completionID;
- (void)transitionWithView:(UIView *)view duration:(NSTimeInterval)duration options:(UIViewAnimationOptions)options completion:(int)completionID;

- (BOOL)areAnimationsEnabled;
- (void)beginAnimations:(NSString *)animationID context:(void *)context;
- (void)commitAnimations;
- (void)setAnimationBeginsFromCurrentState:(BOOL)fromCurrentState;
- (void)setAnimationCurve:(UIViewAnimationCurve)curve;
- (void)setAnimationDelay:(NSTimeInterval)delay;
- (void)setAnimationDuration:(NSTimeInterval)duration;
- (void)setAnimationRepeatAutoreverses:(BOOL)repeatAutoreverses;
- (void)setAnimationRepeatCount:(float)repeatCount;
- (void)setAnimationsEnabled:(BOOL)enabled;
- (void)setAnimationStartDate:(NSDate *)startTime;
- (void)setAnimationTransition:(UIViewAnimationTransition)transition forView:(UIView *)view cache:(BOOL)cache;

@end
