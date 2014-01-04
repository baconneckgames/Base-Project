using UnityEngine;
using System;
using System.Collections;

public class MHPopoverController : MHObject {
	#region functions
	public MHPopoverController(){}
	
	public MHPopoverController(bool autoInitialize)
	{
		if(Application.isPlaying && autoInitialize)
			init();
	}
	
	public MHPopoverController(MHViewController viewController)
	{
		if(Application.isPlaying)
			if(viewController != null)
				initWithContentViewController(viewController);
	}
	
	new public MHPopoverController init()
	{
		return MHiOSManager.Instance.init_popovercontroller(tag);
	}
	
	public MHPopoverController initWithContentViewController(MHViewController viewController)
	{
		return MHiOSManager.Instance.initWithContentViewController(tag, viewController);
	}
	
	public MHViewController contentViewController {
		get {
			return MHiOSManager.Instance.contentViewController(tag);
		} set {
			MHiOSManager.Instance.contentViewController(tag, value);
		}
	}
	
	public void setContentViewController(MHViewController viewController, bool animated)
	{
		MHiOSManager.Instance.setContentViewController(tag, viewController, animated);
	}
	
	public Vector2 popoverContentSize {
		get {
			return MHiOSManager.Instance.popoverContentSize(tag);
		} set {
			MHiOSManager.Instance.popoverContentSize(tag, value);
		}
	}
	
	public void setPopoverContentSize(Vector2 size, bool animated)
	{
		MHiOSManager.Instance.setPopoverContentSize(tag, size, animated);
	}
	
	public MHView[] passthroughViews {
		get {
			return MHiOSManager.Instance.passthroughViews(tag);
		} set {
			MHiOSManager.Instance.passthroughViews(tag, value);
		}
	}

   	public bool popoverVisible {
		get {
			return MHiOSManager.Instance.popoverVisible(tag);
		}
	}
	
	public MHPopoverArrowDirection popoverArrowDirection {
		get {
			return MHiOSManager.Instance.popoverArrowDirection(tag);
		}
	}

	public void presentPopoverFromRect(Rect rect, MHView inView, MHPopoverArrowDirection permittedArrowDirections, bool animated)
	{
		MHiOSManager.Instance.presentPopoverFromRect(tag, rect, inView, permittedArrowDirections, animated);
	}
	
	public void presentPopoverFromBarButtonItem(MHBarButtonItem item, MHPopoverArrowDirection permittedArrowDirections, bool animated)
	{
		MHiOSManager.Instance.presentPopoverFromBarButtonItem(tag, item, permittedArrowDirections, animated);
	}

	public void dismissPopoverAnimated(bool animated)
	{
		MHiOSManager.Instance.dismissPopoverAnimated(tag, animated);
	}
	
	public Vector4 popoverLayoutMargins {
		get {
			return MHiOSManager.Instance.popoverLayoutMargins(tag);
		} set {
			MHiOSManager.Instance.popoverLayoutMargins(tag, value);
		}
	}
	#endregion
	
	#region delegate
	public event Func<MHPopoverController, bool> popoverControllerShouldDismissPopover;
	public void _popoverControllerShouldDismissPopover(MHPopoverController popoverController)
	{
		if(popoverControllerShouldDismissPopover != null)
			MHTools.ReturnResultToXCode((popoverControllerShouldDismissPopover(popoverController)).ToString());
		else
			MHTools.ReturnResultToXCode("true");
	}
	
	public event Action<MHPopoverController> popoverControllerDidDismissPopover;
	public void _popoverControllerDidDismissPopover(MHPopoverController popoverController)
	{
		if(popoverControllerDidDismissPopover != null)
			popoverControllerDidDismissPopover(popoverController);
	}
	#endregion
}
