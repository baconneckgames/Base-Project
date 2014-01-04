//
//  ALBViewController.m
//  Unity-iPhone
//
//  Created by Patrick Antoine on 9/19/13.
//
//

#import "ALBViewController.h"

@implementation ALBViewController
#pragma mark UIViewControllerCallbacks
+ (void)viewDidLoad:(int)tag 
{
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          [NSNumber numberWithInt:tag], @"tag",
                          nil];
    const char *jsonResult = MakeStringCopy([dict JSONString]);
    
    [iOSViewManager UnitySendInstantMessage:"viewDidLoad" JSONPARAM:jsonResult];
}

+ (void)viewWillAppear:(int)tag animated:(BOOL)animated
{
    bool anim = animated;
    
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          [NSNumber numberWithInt:tag], @"tag",
                          [NSNumber numberWithBool:anim], @"animated",
                          nil];
    const char *jsonResult = MakeStringCopy([dict JSONString]);
    
    [iOSViewManager UnitySendInstantMessage:"viewWillAppear" JSONPARAM:jsonResult];
}

+ (void)viewDidAppear:(int)tag animated:(BOOL)animated
{
    bool anim = animated;
    
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          [NSNumber numberWithInt:tag], @"tag",
                          [NSNumber numberWithBool:anim], @"animated",
                          nil];
    const char *jsonResult = MakeStringCopy([dict JSONString]);
    
    [iOSViewManager UnitySendInstantMessage:"viewDidAppear" JSONPARAM:jsonResult];
}

+ (void)viewWillDisappear:(int)tag animated:(BOOL)animated
{
    bool anim = animated;
    
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          [NSNumber numberWithInt:tag], @"tag",
                          [NSNumber numberWithBool:anim], @"animated",
                          nil];
    const char *jsonResult = MakeStringCopy([dict JSONString]);
    
    [iOSViewManager UnitySendInstantMessage:"viewWillDisappear" JSONPARAM:jsonResult];
}

+ (void)viewDidDisappear:(int)tag animated:(BOOL)animated
{
    bool anim = animated;
    
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          [NSNumber numberWithInt:tag], @"tag",
                          [NSNumber numberWithBool:anim], @"animated",
                          nil];
    const char *jsonResult = MakeStringCopy([dict JSONString]);
    
    [iOSViewManager UnitySendInstantMessage:"viewDidDisappear" JSONPARAM:jsonResult];
}

+ (void)viewWillLayoutSubviews:(int)tag
{
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          [NSNumber numberWithInt:tag], @"tag",
                          nil];
    const char *jsonResult = MakeStringCopy([dict JSONString]);
    
    [iOSViewManager UnitySendInstantMessage:"viewWillLayoutSubviews" JSONPARAM:jsonResult];
}

+ (void)viewDidLayoutSubviews:(int)tag
{
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          [NSNumber numberWithInt:tag], @"tag",
                          nil];
    const char *jsonResult = MakeStringCopy([dict JSONString]);
    
    [iOSViewManager UnitySendInstantMessage:"viewDidLayoutSubviews" JSONPARAM:jsonResult];
}

+ (void)didReceiveMemoryWarning:(int)tag
{
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          [NSNumber numberWithInt:tag], @"tag",
                          nil];
    const char *jsonResult = MakeStringCopy([dict JSONString]);
    
    [iOSViewManager UnitySendInstantMessage:"didReceiveMemoryWarning" JSONPARAM:jsonResult];
}

+ (void)willRotateToInterfaceOrientation:(int)tag orientation:(UIInterfaceOrientation)toInterfaceOrientation duration:(NSTimeInterval)duration
{
    int orientation = (int)toInterfaceOrientation;
    float dur = (float)duration;
    
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          [NSNumber numberWithInt:tag], @"tag",
                          [NSNumber numberWithInt:orientation], @"orientation",
                          [NSNumber numberWithFloat:dur], @"duration",
                          nil];
    const char *jsonResult = MakeStringCopy([dict JSONString]);
    
    [iOSViewManager UnitySendInstantMessage:"willRotateToInterfaceOrientation" JSONPARAM:jsonResult];
}

+ (void)willAnimateRotationToInterfaceOrientation:(int)tag orientation:(UIInterfaceOrientation)interfaceOrientation duration:(NSTimeInterval)duration
{
    int orientation = (int)interfaceOrientation;
    float dur = (float)duration;
    
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          [NSNumber numberWithInt:tag], @"tag",
                          [NSNumber numberWithInt:orientation], @"orientation",
                          [NSNumber numberWithFloat:dur], @"duration",
                          nil];
    const char *jsonResult = MakeStringCopy([dict JSONString]);
    
    [iOSViewManager UnitySendInstantMessage:"willAnimateRotationToInterfaceOrientation" JSONPARAM:jsonResult];
}

+ (void)didRotateFromInterfaceOrientation:(int)tag orientation:(UIInterfaceOrientation)fromInterfaceOrientation
{
    int orientation = (int)fromInterfaceOrientation;
    
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          [NSNumber numberWithInt:tag], @"tag",
                          [NSNumber numberWithInt:orientation], @"orientation",
                          nil];
    const char *jsonResult = MakeStringCopy([dict JSONString]);
    
    [iOSViewManager UnitySendInstantMessage:"didRotateFromInterfaceOrientation" JSONPARAM:jsonResult];
}

+ (void)willMoveToParentViewController:(int)tag parentViewController:(UIViewController *)parent
{
    if(![MHTools actualObjectExistsByObject:parent])
        return;
    int rent = [MHTools getKeyFromActual:parent];
    
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          [NSNumber numberWithInt:tag], @"tag",
                          [NSNumber numberWithInt:rent], @"parent",
                          nil];
    const char *jsonResult = MakeStringCopy([dict JSONString]);
    
    [iOSViewManager UnitySendInstantMessage:"willMoveToParentViewController" JSONPARAM:jsonResult];
}

+ (void)didMoveToParentViewController:(int)tag parentViewController:(UIViewController *)parent
{
    if(![MHTools actualObjectExistsByObject:parent])
        return;
    int rent = [MHTools getKeyFromActual:parent];
    
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          [NSNumber numberWithInt:tag], @"tag",
                          [NSNumber numberWithInt:rent], @"parent",
                          nil];
    const char *jsonResult = MakeStringCopy([dict JSONString]);
    
    [iOSViewManager UnitySendInstantMessage:"didMoveToParentViewController" JSONPARAM:jsonResult];
}

#pragma mark LocalCalls
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
