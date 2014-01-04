//
//  ALBPopoverController.m
//  Unity-iPhone
//
//  Created by Patrick Antoine on 9/19/13.
//
//

#import "ALBPopoverController.h"

@implementation ALBPopoverController

#pragma mark UIPopoverControllerDelegate
- (BOOL)popoverControllerShouldDismissPopover:(UIPopoverController *)popoverController
{
    int tag = [MHTools getKeyFromActual:self];
    int pController = [MHTools getKeyFromActual:popoverController];
    
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          [NSNumber numberWithInt:tag], @"tag",
                          [NSNumber numberWithInt:pController], @"popoverController",
                          nil];
    const char *jsonResult = MakeStringCopy([dict JSONString]);
    
    [iOSViewManager UnitySendInstantMessage:"popoverControllerShouldDismissPopover" JSONPARAM:jsonResult];
    
    NSString *result =  GetStringParamOrNil([[iOSViewManager sharedManager] _result]);
    [iOSViewManager sharedManager]._result = "NULL";
    
    if([result isEqualToString:@"true"])
        return true;
    return false;
}

- (void)popoverControllerDidDismissPopover:(UIPopoverController *)popoverController
{
    int tag = [MHTools getKeyFromActual:self];
    int pController = [MHTools getKeyFromActual:popoverController];
    
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          [NSNumber numberWithInt:tag], @"tag",
                          [NSNumber numberWithInt:pController], @"popoverController",
                          nil];
    const char *jsonResult = MakeStringCopy([dict JSONString]);
    
    [iOSViewManager UnitySendInstantMessage:"popoverControllerDidDismissPopover" JSONPARAM:jsonResult];
}

#pragma mark BaseCallBacks
- (void)viewDidLoad
{
    if(![MHTools actualObjectExistsByObject:self])
        return;
    [ALBViewController viewDidLoad:[MHTools getKeyFromActual:self]];
}
- (void)viewWillAppear:(BOOL)animated
{
    if(![MHTools actualObjectExistsByObject:self])
        return;
    [ALBViewController viewWillAppear:[MHTools getKeyFromActual:self] animated:animated];
}
- (void)viewDidAppear:(BOOL)animated
{
    if(![MHTools actualObjectExistsByObject:self])
        return;
    [ALBViewController viewDidAppear:[MHTools getKeyFromActual:self] animated:animated];
}
- (void)viewWillDisappear:(BOOL)animated
{
    if(![MHTools actualObjectExistsByObject:self])
        return;
    [ALBViewController viewWillDisappear:[MHTools getKeyFromActual:self] animated:animated];
}
- (void)viewDidDisappear:(BOOL)animated
{
    if(![MHTools actualObjectExistsByObject:self])
        return;
    [ALBViewController viewDidDisappear:[MHTools getKeyFromActual:self] animated:animated];
}
- (void)viewWillLayoutSubviews
{
    if(![MHTools actualObjectExistsByObject:self])
        return;
    [ALBViewController viewWillLayoutSubviews:[MHTools getKeyFromActual:self]];
}
- (void)viewDidLayoutSubviews
{
    if(![MHTools actualObjectExistsByObject:self])
        return;
    [ALBViewController viewDidLayoutSubviews:[MHTools getKeyFromActual:self]];
}
- (void)didReceiveMemoryWarning
{
    if(![MHTools actualObjectExistsByObject:self])
        return;
    [ALBViewController didReceiveMemoryWarning:[MHTools getKeyFromActual:self]];
}
- (void)willRotateToInterfaceOrientation:(UIInterfaceOrientation)toInterfaceOrientation duration:(NSTimeInterval)duration
{
    if(![MHTools actualObjectExistsByObject:self])
        return;
    [ALBViewController willRotateToInterfaceOrientation:[MHTools getKeyFromActual:self] orientation:toInterfaceOrientation duration:duration];
}
- (void)willAnimateRotationToInterfaceOrientation:(UIInterfaceOrientation)interfaceOrientation duration:(NSTimeInterval)duration
{
    if(![MHTools actualObjectExistsByObject:self])
        return;
    [ALBViewController willAnimateRotationToInterfaceOrientation:[MHTools getKeyFromActual:self] orientation:interfaceOrientation duration:duration];
}
- (void)didRotateFromInterfaceOrientation:(UIInterfaceOrientation)fromInterfaceOrientation
{
    if(![MHTools actualObjectExistsByObject:self])
        return;
    [ALBViewController didRotateFromInterfaceOrientation:[MHTools getKeyFromActual:self] orientation:fromInterfaceOrientation];
}
- (void)willMoveToParentViewController:(UIViewController *)parent
{
    if(![MHTools actualObjectExistsByObject:self])
        return;
    [ALBViewController willMoveToParentViewController:[MHTools getKeyFromActual:self] parentViewController:parent];
}
- (void)didMoveToParentViewController:(UIViewController *)parent
{
    if(![MHTools actualObjectExistsByObject:self])
        return;
    [ALBViewController willMoveToParentViewController:[MHTools getKeyFromActual:self] parentViewController:parent];
}
@end
