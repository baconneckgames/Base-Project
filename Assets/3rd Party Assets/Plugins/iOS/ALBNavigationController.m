//
//  ALBNavigationController.m
//  Unity-iPhone
//
//  Created by Patrick Antoine on 9/19/13.
//
//

#import "ALBNavigationController.h"

@implementation ALBNavigationController

#pragma mark UINavigationControllerDelegate
- (void)navigationController:(UINavigationController *)navigationController willShowViewController:(UIViewController *)viewController animated:(BOOL)animated
{
    if(![MHTools actualObjectExistsByObject:viewController])
        return;
    int tag = [MHTools getKeyFromActual:self];
    int nController = [MHTools getKeyFromActual:navigationController];
    int vController = [MHTools getKeyFromActual:viewController];
    bool anim = animated;
    
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          [NSNumber numberWithInt:tag], @"tag",
                          [NSNumber numberWithInt:nController], @"navigationController",
                          [NSNumber numberWithInt:vController], @"viewController",
                          [NSNumber numberWithBool:anim], @"animated",
                          nil];
    const char *jsonResult = MakeStringCopy([dict JSONString]);
    
    [iOSViewManager UnitySendInstantMessage:"willShowViewController" JSONPARAM:jsonResult];
}
- (void)navigationController:(UINavigationController *)navigationController didShowViewController:(UIViewController *)viewController animated:(BOOL)animated
{
    if(![MHTools actualObjectExistsByObject:viewController])
        return;
    int tag = [MHTools getKeyFromActual:self];
    int nController = [MHTools getKeyFromActual:navigationController];
    int vController = [MHTools getKeyFromActual:viewController];
    bool anim = animated;
    
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          [NSNumber numberWithInt:tag], @"tag",
                          [NSNumber numberWithInt:nController], @"navigationController",
                          [NSNumber numberWithInt:vController], @"viewController",
                          [NSNumber numberWithBool:anim], @"animated",
                          nil];
    const char *jsonResult = MakeStringCopy([dict JSONString]);
    
    [iOSViewManager UnitySendInstantMessage:"didShowViewController" JSONPARAM:jsonResult];
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
