using UnityEngine;
using System.Collections;

public class Tutorial1A_ViewController : ALBBaseTutorial
{
	public MHView view1A;
	public MHViewController viewController1A;
	public MHButton button1A;
	public Rect screenSize;
	
	public Tutorial1A_ViewController(){}
	
	public override string GetButtonText()
	{
		return "Show ViewController";
	}
	
	public override string GetTutorialDescription()
	{
		return "Tutorial1A:\nThis section shows you how to present a simple view controller with a button to dismiss it.";
	}
	
	public override void StartTutorial()
	{
		base.StartTutorial();
		
		// Get the screen size in iOS coordinates from the Unity View
		screenSize = MHView.unityView.frame;
		
		// Create a FullScreen View, Set Background Color if you want
		view1A = new MHView(screenSize);
		view1A.backgroundColor = Color.white;
		
		// Create the ViewController that houses that View
		// Set the view of the ViewController to the previously made view
		viewController1A = new MHViewController(true);
		viewController1A.view = view1A;
		viewController1A.title = "Tutorial1A";
		
		// Create Button that will dismiss the ViewController
		button1A = new MHButton(MHButtonType.MHButtonTypeRoundedRect);
		button1A.setTitle("Dismiss Me!", MHControlState.MHControlStateNormal);
		button1A.frame = new Rect((screenSize.width/2f)-80f, (screenSize.height/2f)-20f, 160f, 40f);
		
		// Add the action for the button, when the press is lifted (other events available)
		button1A.addActionForControlEvents(Dismiss_1A, MHControlEvents.MHControlEventTouchUpInside);
		
		// Add the button as a subview so that it appears
		view1A.addSubview(button1A);
		
		// Have the Unity ViewController present the ViewController we just made! (by animating it in)
		// Completion is null so we don't run anything when it's done animating in
		MHViewController.unityViewController.presentViewController(viewController1A, true, null);
	}
	
	public void Dismiss_1A()
	{
		// Dismiss the ViewController by animated it out. 
		// When the animation is done, call EndTutorial
		// When using actions, make sure each one is different
		// Since we'll be using this multiple times, I'm going to have them all call the same function
		Dismiss();
	}
	
	public void Dismiss()
	{
		viewController1A.dismissViewControllerAnimated(true, EndTutorial);
	}
	
	public override void EndTutorial()
	{
		// Remember to release when you're done, 
		// set them to null as well if you want
		button1A.release();
		view1A.release();
		viewController1A.release();
		base.EndTutorial();
	}
}
