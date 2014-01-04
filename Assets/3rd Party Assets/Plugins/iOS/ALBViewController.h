//
//  ALBViewController.h
//  Unity-iPhone
//
//  Created by Patrick Antoine on 9/19/13.
//
//

#import <Foundation/Foundation.h>
#import "MHTools.h"

@interface ALBViewController : UIViewController

+ (void)viewDidLoad:(int)tag;
+ (void)viewWillAppear:(int)tag animated:(BOOL)animated;
+ (void)viewDidAppear:(int)tag animated:(BOOL)animated;
+ (void)viewWillDisappear:(int)tag animated:(BOOL)animated;
+ (void)viewDidDisappear:(int)tag animated:(BOOL)animated;
+ (void)viewWillLayoutSubviews:(int)tag;
+ (void)viewDidLayoutSubviews:(int)tag;
+ (void)didReceiveMemoryWarning:(int)tag;
+ (void)willRotateToInterfaceOrientation:(int)tag orientation:(UIInterfaceOrientation)toInterfaceOrientation duration:(NSTimeInterval)duration;
+ (void)willAnimateRotationToInterfaceOrientation:(int)tag orientation:(UIInterfaceOrientation)interfaceOrientation duration:(NSTimeInterval)duration;
+ (void)didRotateFromInterfaceOrientation:(int)tag orientation:(UIInterfaceOrientation)fromInterfaceOrientation;
+ (void)willMoveToParentViewController:(int)tag parentViewController:(UIViewController *)parent;
+ (void)didMoveToParentViewController:(int)tag parentViewController:(UIViewController *)parent;

@end


