using UnityEngine;
using System.Collections;

public class Tutorial4B_PopoverController : Tutorial4A_NavigationController
{
	public MHPopoverController popoverController4B;
	
	public Tutorial4B_PopoverController(){}
	
	public override string GetButtonText()
	{
		return "How to use a\nPopoverController\n(iPad Only)";
	}
	
	public override string GetTutorialDescription()
	{
		return "Tutorial4B:\nThis section shows you how to use a popover controller. This feature is only available on iPads.";
	}
	
	public override void StartTutorial()
	{
		// Do the same stuff from Tutorial4A
		base.StartTutorial();
		
		// Make sure this is an iPad first, otherwise just do 1A
		if(MHTools.IsIpad)
		{
			// Make the popover controller using the navigation controller as the contentviewcontroller
			popoverController4B = new MHPopoverController(navigationController4A);
			// Set the contentsizeinpopover for the navigation controller
			popoverController4B.contentViewController.contentSizeForViewInPopover = new Vector2(320f, 480f);
			
			popoverController4B.popoverControllerDidDismissPopover += PopoverDismissed;
			
			// Present the popover from the main tutorial button rect
			// Use presentFromBarButtonItem to present from a bar button
			popoverController4B.presentPopoverFromRect(new Rect(screenSize.width/4f, 3f*screenSize.height/18f, screenSize.width / 2f, 3f*screenSize.height/9f), MHView.unityView,MHPopoverArrowDirection.MHPopoverArrowDirectionAny,true);
		}
		else
		{
			Debug.LogError("YOU ARE NOT ON AN IPAD, WE'RE NOT USING A POPOVER");
			PushViewController_4A();
		}
	}
	
	public override void DonePressed()
	{
		if(MHTools.IsIpad)
		{
			// Override the old done pressed and make it dismiss the popover instead
			popoverController4B.dismissPopoverAnimated(true);
		}
		else
		{
			base.DonePressed();
		}
	}
	
	public override void PushViewController_4A()
	{
		if(MHTools.IsIpad)
		{
			// Make sure the navigation isn't pushed from 4A
			return;
		}
		else
		{
			base.PushViewController_4A();
		}
	}
	
	public void PopoverDismissed(MHPopoverController pop)
	{
		popoverController4B.popoverControllerDidDismissPopover -= PopoverDismissed;
		
		EndTutorial();
	}
	
	public override void EndTutorial()
	{
		base.EndTutorial();
		
		popoverController4B.release();
	}
}
