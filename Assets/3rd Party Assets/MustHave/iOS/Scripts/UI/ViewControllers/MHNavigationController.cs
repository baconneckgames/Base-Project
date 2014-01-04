using UnityEngine;
using System;
using System.Collections;

public class MHNavigationController : MHViewController {
	#region functions
	public MHNavigationController(){}
	
	public MHNavigationController(bool autoInitialize)
	{
		if(Application.isPlaying && autoInitialize)
			init();
	}
	
	public MHNavigationController(MHViewController rootViewController)
	{
		if(Application.isPlaying)
			if(rootViewController != null)
				initWithRootViewController(rootViewController);
	}
	
	new public MHNavigationController init()
	{
		MHNavigationController navController = MHiOSManager.Instance.init_navigationcontroller(tag);
		new MHToolbar(navController);
		new MHNavigationBar(navController);
		new MHNavigationItem(navController);
		return navController;
	}
	
	public MHNavigationController initWithRootViewController(MHViewController rootViewController)
	{
		MHNavigationController navController = MHiOSManager.Instance.initWithRootViewController(tag, rootViewController);
		new MHToolbar(navController);
		new MHNavigationBar(navController);
		new MHNavigationItem(navController);
		return navController;
	}
	
	public MHViewController topViewController {
		get {
			return MHiOSManager.Instance.topViewController(tag);
		}
	}
	
	public MHViewController visibleViewController {
		get {
			return MHiOSManager.Instance.visibleViewController(tag);
		}
	}
	
	public MHViewController[] viewControllers {
		get {
			return MHiOSManager.Instance.viewControllers(tag);
		} set {
			MHiOSManager.Instance.viewControllers(tag, value);
		}
	}
	
	public void setViewControllers(MHViewController[] viewControllers, bool animated)
	{
		MHiOSManager.Instance.setViewControllers(tag, viewControllers, animated);
	}
	
	public void pushViewController(MHViewController viewController, bool animated)
	{
		MHiOSManager.Instance.pushViewController(tag, viewController, animated);
	}
	
	public MHViewController popViewControllerAnimated(bool animated)
	{
		return MHiOSManager.Instance.popViewControllerAnimated(tag, animated);
	}
	
	public MHViewController[] popToRootViewControllerAnimated(bool animated)
	{
		return MHiOSManager.Instance.popToRootViewControllerAnimated(tag, animated);
	}
	
	public MHViewController[] popToViewController(MHViewController viewController, bool animated)
	{
		return MHiOSManager.Instance.popToViewController(tag, viewController, animated);
	}
	
	public MHNavigationBar navigationBar {
		get {
			return MHiOSManager.Instance.navigationBar(tag);
		}
	}
	
	public bool navigationBarHidden {
		get {
			return MHiOSManager.Instance.navigationBarHidden(tag);
		} set {
			MHiOSManager.Instance.navigationBarHidden(tag, value);
		}
	}
	
	public void setNavigationBarHidden(bool hidden, bool animated)
	{
		MHiOSManager.Instance.setNavigationBarHidden(tag, hidden, animated);
	}
	
	public MHToolbar toolbar {
		get {
			return MHiOSManager.Instance.toolbar(tag);
		}
	}
	
	public void setToolbarHidden(bool hidden, bool animated)
	{
		MHiOSManager.Instance.setToolbarHidden(tag, hidden, animated);
	}
	
	public bool toolbarHidden {
		get {
			return MHiOSManager.Instance.toolbarHidden(tag);
		} set {
			MHiOSManager.Instance.toolbarHidden(tag, value);
		}
	}
	#endregion
			
	#region delegate
	public event Action<MHNavigationController, MHViewController, bool> willShowViewController;
	public void _willShowViewController(MHNavigationController navigationController, MHViewController viewController, bool animated)
	{
		if(willShowViewController != null)
			willShowViewController(navigationController, viewController, animated);
	}
	
	public event Action<MHNavigationController, MHViewController, bool> didShowViewController;
	public void _didShowViewController(MHNavigationController navigationController, MHViewController viewController, bool animated)
	{
		if(didShowViewController != null)
			didShowViewController(navigationController, viewController, animated);
	}
	#endregion
}
