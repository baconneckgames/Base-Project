//
//  iOSViewManager+ViewController.m
//  Unity-iPhone
//
//  Created by Patrick Antoine on 8/22/13.
//
//

#import "iOSViewManager+ViewController.h"
#define CANNOT_FIND_ERROR @"ERROR: Cannot find UIViewController"

@implementation iOSViewManager (ViewController)

@end

extern "C"
{
    int _init_viewcontroller(int tag)
    {
        if(![MHTools objectExists:tag])
        {
            ALBViewController *vc = [[[ALBViewController alloc] init] autorelease];
            
            [MHTools addreplaceObject:vc key:tag actualUI:vc];
        }
        else
        {
            NSLog(@"ERROR: This tag is already taken, cannot initiate this object (VIEWCONTROLLER)");
        }
        return tag;
    }
    
    int _view(int tag)
	{
        UIViewController *viewcontroller = [MHTools getActualViewController:tag];
        if(viewcontroller)
        {
            return [MHTools getKeyFromActual:viewcontroller.view];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0;
	}
	
	void _view_set(int tag, int viewTag)
	{
        UIViewController *viewcontroller = [MHTools getActualViewController:tag];
        if(viewcontroller)
        {
            viewcontroller.view = [MHTools getActualView:viewTag];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	bool _isViewLoaded(int tag)
	{
        UIViewController *viewcontroller = [MHTools getActualViewController:tag];
        if(viewcontroller)
        {
            return viewcontroller.isViewLoaded;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return false;
	}
	
	bool _isMovingFromParentViewController(int tag)
	{
        UIViewController *viewcontroller = [MHTools getActualViewController:tag];
        if(viewcontroller)
        {
            return viewcontroller.isMovingFromParentViewController;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return false;
	}
	
	bool _isMovingToParentViewController(int tag)
	{
        UIViewController *viewcontroller = [MHTools getActualViewController:tag];
        if(viewcontroller)
        {
            return viewcontroller.isMovingToParentViewController;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return false;
	}
	
	bool _isBeingPresented(int tag)
	{
        UIViewController *viewcontroller = [MHTools getActualViewController:tag];
        if(viewcontroller)
        {
            return viewcontroller.isBeingPresented;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return false;
	}
	
	bool _isBeingDismissed(int tag)
	{
        UIViewController *viewcontroller = [MHTools getActualViewController:tag];
        if(viewcontroller)
        {
            return viewcontroller.isBeingDismissed;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return false;
	}
	
	bool _wantsFullScreenLayout(int tag)
	{
        UIViewController *viewcontroller = [MHTools getActualViewController:tag];
        if(viewcontroller)
        {
            return viewcontroller.wantsFullScreenLayout;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return false;
	}
	
	void _wantsFullScreenLayout_set(int tag, bool fullscreen)
	{
        UIViewController *viewcontroller = [MHTools getActualViewController:tag];
        if(viewcontroller)
        {
            viewcontroller.wantsFullScreenLayout = fullscreen;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	bool _shouldAutorotate(int tag)
	{
        UIViewController *viewcontroller = [MHTools getActualViewController:tag];
        if(viewcontroller)
        {
            return [viewcontroller shouldAutorotate];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return false;
	}
	
	int _supportedInterfaceOrientations(int tag)
	{
        UIViewController *viewcontroller = [MHTools getActualViewController:tag];
        if(viewcontroller)
        {
            return (int)viewcontroller.supportedInterfaceOrientations;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return (int)UIInterfaceOrientationPortrait;
	}
	
	int _preferredInterfaceOrientationForPresentation(int tag)
	{
        UIViewController *viewcontroller = [MHTools getActualViewController:tag];
        if(viewcontroller)
        {
            return (int)viewcontroller.preferredInterfaceOrientationForPresentation;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return (int)UIInterfaceOrientationPortrait;
	}
	
	int _interfaceOrientation(int tag)
	{
        UIViewController *viewcontroller = [MHTools getActualViewController:tag];
        if(viewcontroller)
        {
            return (int)viewcontroller.interfaceOrientation;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return (int)UIInterfaceOrientationPortrait;
	}
	
	void _attemptRotationToDeviceOrientation(int tag)
	{
        UIViewController *viewcontroller = [MHTools getActualViewController:tag];
        if(viewcontroller)
        {
            [UIViewController attemptRotationToDeviceOrientation];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	int _rotatingHeaderView(int tag)
	{
        UIViewController *viewcontroller = [MHTools getActualViewController:tag];
        if(viewcontroller)
        {
            return [MHTools getKeyFromActual:viewcontroller.rotatingHeaderView];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0;
	}
	
	int _rotatingFooterView(int tag)
	{
        UIViewController *viewcontroller = [MHTools getActualViewController:tag];
        if(viewcontroller)
        {
            return [MHTools getKeyFromActual:viewcontroller.rotatingFooterView];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0;
	}
	
	void _editing_set(int tag, bool edit)
	{
        UIViewController *viewcontroller = [MHTools getActualViewController:tag];
        if(viewcontroller)
        {
            viewcontroller.editing = edit;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _setEditing(int tag, bool editing, bool animated)
	{
        UIViewController *viewcontroller = [MHTools getActualViewController:tag];
        if(viewcontroller)
        {
            [viewcontroller setEditing:editing animated:animated];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _presentViewController(int tag, int viewControllerToPresent, bool animated, int completion)
	{
        UIViewController *viewcontroller = [MHTools getActualViewController:tag];
        if(viewcontroller)
        {
            [viewcontroller presentViewController:[MHTools getActualViewController:viewControllerToPresent]
                                         animated:animated
                                       completion:^{
                                           [[iOSActionManager sharedManager] performActionByID:completion];
                                       }];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _dismissViewControllerAnimated(int tag, bool animated, int completion)
	{
        UIViewController *viewcontroller = [MHTools getActualViewController:tag];
        if(viewcontroller)
        {
            [viewcontroller dismissViewControllerAnimated:animated
                                               completion:^{
                                                   [[iOSActionManager sharedManager] performActionByID:completion];
                                               }];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	int _modalTransitionStyle(int tag)
	{
        UIViewController *viewcontroller = [MHTools getActualViewController:tag];
        if(viewcontroller)
        {
            return (int)viewcontroller.modalTransitionStyle;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return (int)UIModalTransitionStyleCrossDissolve;
	}
	
	void _modalTransitionStyle_set(int tag, int style)
	{
        UIViewController *viewcontroller = [MHTools getActualViewController:tag];
        if(viewcontroller)
        {
            viewcontroller.modalTransitionStyle = (UIModalTransitionStyle)style;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	int _modalPresentationStyle(int tag)
	{
        UIViewController *viewcontroller = [MHTools getActualViewController:tag];
        if(viewcontroller)
        {
            return (int)viewcontroller.modalPresentationStyle;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return (int)UIModalPresentationCurrentContext;
	}
	
	void _modalPresentationStyle_set(int tag, int style)
	{
        UIViewController *viewcontroller = [MHTools getActualViewController:tag];
        if(viewcontroller)
        {
            viewcontroller.modalPresentationStyle = (UIModalPresentationStyle)style;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	bool _definesPresentationContext(int tag)
	{
        UIViewController *viewcontroller = [MHTools getActualViewController:tag];
        if(viewcontroller)
        {
            return viewcontroller.definesPresentationContext;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return false;
	}
	
	void _definesPresentationContext_set(int tag, bool context)
	{
        UIViewController *viewcontroller = [MHTools getActualViewController:tag];
        if(viewcontroller)
        {
            viewcontroller.definesPresentationContext = context;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	bool _providesPresentationContextTransitionStyle(int tag)
	{
        UIViewController *viewcontroller = [MHTools getActualViewController:tag];
        if(viewcontroller)
        {
            return viewcontroller.providesPresentationContextTransitionStyle;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return false;
	}
	
	void _providesPresentationContextTransitionStyle_set(int tag, bool context)
	{
        UIViewController *viewcontroller = [MHTools getActualViewController:tag];
        if(viewcontroller)
        {
            viewcontroller.providesPresentationContextTransitionStyle = context;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	bool _disablesAutomaticKeyboardDismissal(int tag)
	{
        UIViewController *viewcontroller = [MHTools getActualViewController:tag];
        if(viewcontroller)
        {
            return viewcontroller.disablesAutomaticKeyboardDismissal;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return false;
	}
	
	int _presentingViewController(int tag)
	{
        UIViewController *viewcontroller = [MHTools getActualViewController:tag];
        if(viewcontroller)
        {
            return [MHTools getKeyFromActual:viewcontroller.presentingViewController];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0;
	}
	
	int _presentedViewController(int tag)
	{
        UIViewController *viewcontroller = [MHTools getActualViewController:tag];
        if(viewcontroller)
        {
            return [MHTools getKeyFromActual:viewcontroller.presentedViewController];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0;
	}
	
	int _parentViewController(int tag)
	{
        UIViewController *viewcontroller = [MHTools getActualViewController:tag];
        if(viewcontroller)
        {
            return [MHTools getKeyFromActual:viewcontroller.parentViewController];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0;
	}
	
	const char * _childViewControllers(int tag)
	{
        UIViewController *viewcontroller = [MHTools getActualViewController:tag];
        if(viewcontroller)
        {
            return [MHTools convertNSArrayToMultipleTags:viewcontroller.childViewControllers];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return nil;
	}
	
	void _addChildViewController(int tag, int childController)
	{
        UIViewController *viewcontroller = [MHTools getActualViewController:tag];
        if(viewcontroller)
        {
            [viewcontroller addChildViewController:[MHTools getActualViewController:childController]];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _removeFromParentViewController(int tag)
	{
        UIViewController *viewcontroller = [MHTools getActualViewController:tag];
        if(viewcontroller)
        {
            [viewcontroller removeFromParentViewController];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	bool _shouldAutomaticallyForwardRotationMethods(int tag)
	{
        UIViewController *viewcontroller = [MHTools getActualViewController:tag];
        if(viewcontroller)
        {
            return viewcontroller.shouldAutomaticallyForwardRotationMethods;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return false;
	}
	
	bool _shouldAutomaticallyForwardAppearanceMethods(int tag)
	{
        UIViewController *viewcontroller = [MHTools getActualViewController:tag];
        if(viewcontroller)
        {
            return viewcontroller.shouldAutomaticallyForwardAppearanceMethods;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return false;
	}
	
	//animations is always empty block
	void _transitionFromViewController(int tag, int fromViewController, int toViewController, float duration, int options, int completion)
	{
        UIViewController *viewcontroller = [MHTools getActualViewController:tag];
        if(viewcontroller)
        {
            [viewcontroller transitionFromViewController:[MHTools getActualViewController:fromViewController]
                                        toViewController:[MHTools getActualViewController:toViewController]
                                                duration:(double)duration
                                                 options:(UIViewAnimationOptions)options
                                              animations:^{}
                                              completion:^(BOOL finished){
                                                  [[iOSActionManager sharedManager] performActionByID:completion param1:finished];
                                              }];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _beginAppearanceTransition(int tag, bool isAppearing, bool animated)
	{
        UIViewController *viewcontroller = [MHTools getActualViewController:tag];
        if(viewcontroller)
        {
            [viewcontroller beginAppearanceTransition:isAppearing animated:animated];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _endAppearanceTransition(int tag)
	{
        UIViewController *viewcontroller = [MHTools getActualViewController:tag];
        if(viewcontroller)
        {
            [viewcontroller endAppearanceTransition];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	int _editButtonItem(int tag)
	{
        UIViewController *viewcontroller = [MHTools getActualViewController:tag];
        if(viewcontroller)
        {
            return [MHTools getKeyFromActual:viewcontroller.editButtonItem];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0;
	}
	
	void _setToolbarItems(int tag, int * toolbarItems, bool animated)
	{
        UIViewController *viewcontroller = [MHTools getActualViewController:tag];
        if(viewcontroller)
        {
            [viewcontroller setToolbarItems:[MHTools convertMultipleTagsToNSArray:toolbarItems] animated:animated];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	const char * _toolbarItems(int tag)
	{
        UIViewController *viewcontroller = [MHTools getActualViewController:tag];
        if(viewcontroller)
        {
            return [MHTools convertNSArrayToMultipleTags:viewcontroller.toolbarItems];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return nil;
	}
	
	void _toolbarItems_set(int tag, int * items)
	{
        UIViewController *viewcontroller = [MHTools getActualViewController:tag];
        if(viewcontroller)
        {
            viewcontroller.toolbarItems = [MHTools convertMultipleTagsToNSArray:items];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
}
