using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class MHViewController : MHObject {
	#region functions
	public MHViewController(){}
	
	public MHViewController(bool autoInitialize)
	{
		if(Application.isPlaying && autoInitialize)
			init();
	}
	
	new public MHViewController init()
	{
		MHViewController vc = MHiOSManager.Instance.init_viewcontroller(tag);
		new MHNavigationItem(vc);
		return vc;
	}
	
	public MHView view {
		get {
			if(tag == 0)
				return MHView.unityView;
			return MHiOSManager.Instance.view(tag);
		} set {
			MHiOSManager.Instance.view(tag, value);
		}
	}
	
	public bool isViewLoaded()
	{
		return MHiOSManager.Instance.isViewLoaded(tag);
	}
	
	public string title {
		get {
			return MHiOSManager.Instance.title(tag);
		} set {
			MHiOSManager.Instance.title(tag, value);
		}
	}
	
	public bool isMovingFromParentViewController()
	{
		return MHiOSManager.Instance.isMovingFromParentViewController(tag);
	}
	public bool isMovingToParentViewController()
	{
		return MHiOSManager.Instance.isMovingToParentViewController(tag);
	}
	
	public bool isBeingPresented()
	{
		return MHiOSManager.Instance.isBeingPresented(tag);
	}
	
	public bool isBeingDismissed()
	{
		return MHiOSManager.Instance.isBeingDismissed(tag);
	}
	
	public bool wantsFullScreenLayout {
		get {
			return MHiOSManager.Instance.wantsFullScreenLayout(tag);
		} set {
			MHiOSManager.Instance.wantsFullScreenLayout(tag, value);
		}
	}
	
	public bool shouldAutorotate()
	{
		return MHiOSManager.Instance.shouldAutorotate(tag);
	}

	public MHInterfaceOrientationMask supportedInterfaceOrientations()
	{
		return MHiOSManager.Instance.supportedInterfaceOrientations(tag);
	}
	
	public MHInterfaceOrientation preferredInterfaceOrientationForPresentation()
	{
		return MHiOSManager.Instance.preferredInterfaceOrientationForPresentation(tag);
	}
	
    public MHInterfaceOrientation interfaceOrientation {
		get {
			return MHiOSManager.Instance.interfaceOrientation(tag);
		}
	}
	
	public void attemptRotationToDeviceOrientation()
	{
		MHiOSManager.Instance.attemptRotationToDeviceOrientation(tag);
	}
	
	public MHView rotatingHeaderView()
	{
		return MHiOSManager.Instance.rotatingHeaderView(tag);
	}
	
	public MHView rotatingFooterView()
	{
		return MHiOSManager.Instance.rotatingFooterView(tag);
	}
	
	public bool editing {
		get {
			return MHiOSManager.Instance.editing(tag);
		} set {
			MHiOSManager.Instance.editing(tag, value);
		}
	}
	
	public void setEditing(bool editing, bool animated)
	{
		MHiOSManager.Instance.setEditing(tag, editing, animated);
	}
	
	public void presentViewController(MHViewController viewControllerToPresent, bool animated, Action completion)
	{
		MHiOSManager.Instance.presentViewController(tag, viewControllerToPresent, animated, completion);
	}
	
	public void dismissViewControllerAnimated(bool animated, Action completion)
	{
		MHiOSManager.Instance.dismissViewControllerAnimated(tag, animated, completion);
	}
	
	public MHModalTransitionStyle modalTransitionStyle {
		get {
			return MHiOSManager.Instance.modalTransitionStyle(tag);
		} set {
			MHiOSManager.Instance.modalTransitionStyle(tag, value);
		}
	}

	public MHModalPresentationStyle modalPresentationStyle {
		get {
			return MHiOSManager.Instance.modalPresentationStyle(tag);
		} set {
			MHiOSManager.Instance.modalPresentationStyle(tag, value);
		}
	}
	
	public bool definesPresentationContext {
		get {
			return MHiOSManager.Instance.definesPresentationContext(tag);
		} set {
			MHiOSManager.Instance.definesPresentationContext(tag, value);
		}
	}
	
	public bool providesPresentationContextTransitionStyle {
		get {
			return MHiOSManager.Instance.providesPresentationContextTransitionStyle(tag);
		} set {
			MHiOSManager.Instance.providesPresentationContextTransitionStyle(tag, value);
		}
	}
	
	public bool disablesAutomaticKeyboardDismissal()
	{
		return MHiOSManager.Instance.disablesAutomaticKeyboardDismissal(tag);
	}
	
	public MHViewController presentingViewController {
		get {
			return MHiOSManager.Instance.presentingViewController(tag);
		}
	}
	
	public MHViewController presentedViewController {
		get {
			return MHiOSManager.Instance.presentedViewController(tag);
		}
	}
	
	public MHViewController parentViewController {
		get {
			return MHiOSManager.Instance.parentViewController(tag);
		}
	}
	
	public MHViewController[] childViewControllers {
		get {
			return MHiOSManager.Instance.childViewControllers(tag);
		}
	}
	
	public void addChildViewController(MHViewController childController)
	{
		MHiOSManager.Instance.addChildViewController(tag, childController);
	}
	
	public void removeFromParentViewController()
	{
		MHiOSManager.Instance.removeFromParentViewController(tag);
	}
	
	public bool shouldAutomaticallyForwardRotationMethods()
	{
		return MHiOSManager.Instance.shouldAutomaticallyForwardRotationMethods(tag);
	}
	
	public bool shouldAutomaticallyForwardAppearanceMethods()
	{
		return MHiOSManager.Instance.shouldAutomaticallyForwardAppearanceMethods(tag);
	}
	
	public void transitionFromViewController(MHViewController fromViewController, MHViewController toViewController, float duration, MHViewAnimationOptions options, Action<bool> completion)
	{
		MHiOSManager.Instance.transitionFromViewController(tag, fromViewController, toViewController, duration, options, completion);
	}
	
	public void beginAppearanceTransition(bool isAppearing, bool animated)
	{
		MHiOSManager.Instance.beginAppearanceTransition(tag, isAppearing, animated);
	}
	
	public void endAppearanceTransition()
	{
		MHiOSManager.Instance.endAppearanceTransition(tag);
	}
	
	public MHBarButtonItem editButtonItem()
	{
		return MHiOSManager.Instance.editButtonItem(tag);
	}
	
	public void setToolbarItems(MHBarButtonItem[] toolbarItems, bool animated)
	{
		MHiOSManager.Instance.setToolbarItems(tag, toolbarItems, animated);
	}
	
	public MHBarButtonItem[] toolbarItems {
		get {
			return MHiOSManager.Instance.toolbarItems(tag);
		} set {
			MHiOSManager.Instance.toolbarItems(tag, value);
		}
	}
	#endregion
	
	/////////////////////////////////////////////////////////////////////////////////////////////////////
	
	#region callbacks
	public event Action viewDidLoad;
	public void _viewDidLoad()
	{
		if(viewDidLoad != null)
			viewDidLoad();
	}
	
	public event Action<bool> viewWillAppear;
	public void _viewWillAppear(bool animated)
	{
		if(viewWillAppear != null)
			viewWillAppear(animated);
	}
	
	public event Action<bool> viewDidAppear;
	public void _viewDidAppear(bool animated)
	{
		if(viewDidAppear != null)
			viewDidAppear(animated);
	}
	
	public event Action<bool> viewWillDisappear;
	public void _viewWillDisappear(bool animated)
	{
		if(viewWillDisappear != null)
			viewWillDisappear(animated);
	}
	
	public event Action<bool> viewDidDisappear;
	public void _viewDidDisappear(bool animated)
	{
		if(viewDidDisappear != null)
			viewDidDisappear(animated);
	}
	
	public event Action viewWillLayoutSubviews;
	public void _viewWillLayoutSubviews()
	{
		if(viewWillLayoutSubviews != null)
			viewWillLayoutSubviews();
	}
	
	public event Action viewDidLayoutSubviews;
	public void _viewDidLayoutSubviews()
	{
		if(viewDidLayoutSubviews != null)
			viewDidLayoutSubviews();
	}
	
	public event Action didReceiveMemoryWarning;
	public void _didReceiveMemoryWarning()
	{
		if(didReceiveMemoryWarning != null)
			didReceiveMemoryWarning();
	}
	
	public event Action<MHInterfaceOrientation, float> willRotateToInterfaceOrientation;
	public void _willRotateToInterfaceOrientation(MHInterfaceOrientation orientation, float duration)
	{
		if(willRotateToInterfaceOrientation != null)
			willRotateToInterfaceOrientation(orientation, duration);
	}
	
	public event Action<MHInterfaceOrientation, float> willAnimateRotationToInterfaceOrientation;
	public void _willAnimateRotationToInterfaceOrientation(MHInterfaceOrientation orientation, float duration)
	{
		if(willAnimateRotationToInterfaceOrientation != null)
			willAnimateRotationToInterfaceOrientation(orientation, duration);
	}
	
	public event Action<MHInterfaceOrientation> didRotateFromInterfaceOrientation;
	public void _didRotateFromInterfaceOrientation(MHInterfaceOrientation fromInterfaceOrientation)
	{
		if(didRotateFromInterfaceOrientation != null)
			didRotateFromInterfaceOrientation(fromInterfaceOrientation);
	}
	
	public event Action<MHViewController> willMoveToParentViewController;
	public void _willMoveToParentViewController(MHViewController parent)
	{
		if(willMoveToParentViewController != null)
			willMoveToParentViewController(parent);
	}
	
	public event Action<MHViewController> didMoveToParentViewController;
	public void _didMoveToParentViewController(MHViewController parent)
	{
		if(didMoveToParentViewController != null)
			didMoveToParentViewController(parent);
	}
	#endregion
}
