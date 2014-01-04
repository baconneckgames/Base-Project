using UnityEngine;
using System.Collections;

public class Tutorial4A_NavigationController : ALBBaseTutorial
{
	public MHView view4A;
	public MHViewController viewController4A;
	
	public MHView view4A_2;
	public MHViewController viewController4A_2;
	
	public MHNavigationController navigationController4A;
	
	public MHBarButtonItem barButton4A_Left_1st;
	public MHBarButtonItem barButton4A_Right_1st;
	public MHBarButtonItem barButton4A_Right_2nd;
	
	public Rect screenSize;
	
	public Tutorial4A_NavigationController(){}
	
	public override string GetButtonText()
	{
		return "How to use a\nNavigation Controller";
	}
	
	public override string GetTutorialDescription()
	{
		return "Tutorial4A:\nThis section shows you how to use a navigation controller to manage different viewcontrollers";
	}
	
	public override void StartTutorial()
	{
		base.StartTutorial();
		
		// Get the screen size in iOS coordinates from the Unity View
		screenSize = MHView.unityView.frame;
		
		// Create the first view
		view4A = new MHView(screenSize);
		view4A.backgroundColor = Color.cyan;
		
		// Create the second view
		view4A_2 = new MHView(screenSize);
		view4A_2.backgroundColor = Color.grey;
		
		// Create the 2 view controllers to house the views
		viewController4A = new MHViewController(true);
		viewController4A_2 = new MHViewController(true);
		
		// Assign the views to the view controllers
		viewController4A.view = view4A;
		viewController4A_2.view = view4A_2;
		
		// Set the titles of the view controllers that will show up on the navigation bar
		viewController4A.title = "Page1";
		viewController4A_2.title = "Page2";
		
		// Create the navigation controller, with setting the root view controller
		// (the view controller on the bottom of the stack and appears first)
		navigationController4A = new MHNavigationController(viewController4A);
		
		// Assign the events so that we can have the navigation bar buttons animate in
		navigationController4A.didShowViewController += DidShowViewController;
		navigationController4A.viewDidAppear += ViewDidAppear;
		
		// Present the View Controller
		PushViewController_4A();
	}
	
	public virtual void PushViewController_4A()
	{
		// Making this overridable for future tutorials
		PushViewController();
	}
	
	public void PushViewController()
	{
		// Present the navigation controller that is housing the other view controllers
		MHViewController.unityViewController.presentViewController(navigationController4A, true, null);
	}
	
	public void DidShowViewController(MHNavigationController nav, MHViewController vc, bool animated)
	{
		// Just showing a useful callback
		nav.didShowViewController -= DidShowViewController;
	}
	
	public void ViewDidAppear(bool animated)
	{
		navigationController4A.viewDidAppear -= ViewDidAppear;
		
		// When the navigation controller is presented, let's add the bar buttons:
		// This is the specialized Done button, check out the other system items available
		barButton4A_Left_1st = new MHBarButtonItem(MHBarButtonSystemItem.MHBarButtonSystemItemDone, DonePressed);
		navigationController4A.visibleViewController.navigationItem.setLeftBarButtonItem(barButton4A_Left_1st, true);
		
		// This is another bar button item that will transition to the second view controller
		barButton4A_Right_1st = new MHBarButtonItem("Next Page", MHBarButtonItemStyle.MHBarButtonItemStyleBordered, NextPressed);
		viewController4A.navigationItem.setRightBarButtonItem(barButton4A_Right_1st, true);
		
		// The navigation controller will automatically create a leftbarbutton item that pops this view from the stack
		// However, for demonstation purposes, we'll also make a rightbarbutton item that does the same thing.
		barButton4A_Right_2nd = new MHBarButtonItem("Prev Page", MHBarButtonItemStyle.MHBarButtonItemStyleBordered, PrevPressed);
		viewController4A_2.navigationItem.setRightBarButtonItem(barButton4A_Right_2nd, true);
	}
	
	public void NextPressed()
	{
		// Push the second view controller on the pack
		navigationController4A.pushViewController(viewController4A_2, true);
	}
	
	public void PrevPressed()
	{
		// Pop the current view controller off the stack
		// (in this case, bringing us back to the first view controller)
		navigationController4A.popViewControllerAnimated(true);
	}
			
	public virtual void DonePressed()
	{
		// Take us back to Unity
		navigationController4A.dismissViewControllerAnimated(true, EndTutorial);
	}
	
	public override void EndTutorial()
	{
		view4A.release();
		view4A_2.release();
		viewController4A.release();
		viewController4A_2.release();
		barButton4A_Left_1st.release();
		barButton4A_Right_1st.release();
		barButton4A_Right_2nd.release();
		navigationController4A.release();
		base.EndTutorial();
	}
}
