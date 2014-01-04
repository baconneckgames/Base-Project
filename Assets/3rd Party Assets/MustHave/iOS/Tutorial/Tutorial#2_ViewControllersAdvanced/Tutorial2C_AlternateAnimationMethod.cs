using UnityEngine;
using System.Collections;

public class Tutorial2C_AlternateAnimationMethod : Tutorial1D_Labels
{
	public MHView view2C;
	public MHButton button2C;
	
	public Tutorial2C_AlternateAnimationMethod(){}
	
	public override string GetButtonText()
	{
		return "Alternate Animation Methods\n(Advanced)";
	}
	
	public override string GetTutorialDescription()
	{
		return "Tutorial2C:\nThis section shows you how to make some custom animations. For advanced users.";
	}
	
	public override void StartTutorial()
	{
		// Do the same stuff from Tutorial1
		base.StartTutorial();
		
		// Change the custom button to now go to our second View
		button1C.setTitle("Alternate Transition", MHControlState.MHControlStateNormal);
		button1C.removeActionForControlEvents(Dismiss_1C, MHControlEvents.MHControlEventTouchUpInside);
		button1C.addActionForControlEvents(TransitionToSecondView, MHControlEvents.MHControlEventTouchUpInside);
		
		// For consitency sake, we'll change the title though it's not visible yet (for later tutorials)
		viewController1A.title = "Tutorial2C";
	}
	
	public void TransitionToSecondView()
	{
		// Turn off user interaction
		viewController1A.view.userInteractionEnabled = false;
		
		// Create the second view
		view2C = new MHView(screenSize);
		view2C.backgroundColor = Color.white;
		view1A.addSubview(view2C);
		
		// Set the frame really small so that it can expand out in the animation
		view2C.frame = new Rect((screenSize.width/2f)-50f, (screenSize.height/2f)-50f, 100f, 100f);
		
		// Set up the custom animation
		view2C.animationDidStart += AnimationStarts;
		view2C.animationDidStop += AnimationStops;
		view2C.beginAnimations("Expand", null);
		view2C.setAnimationCurve(MHViewAnimationCurve.MHViewAnimationCurveEaseInOut);
		view2C.setAnimationDuration(.5f);
		
		view2C.frame = screenSize;
		
		view2C.commitAnimations();
	}
	
	public void AnimationStarts(string animationName)
	{
		// Run when the animation starts
		view2C.animationDidStart -= AnimationStarts;
	}
	
	public void AnimationStops(string animationName, MHObject context)
	{
		// Run when teh animation stops
		view2C.animationDidStop -= AnimationStops;
		
		// For the expand animation
		if(animationName == "Expand")
		{
			// When the animation stops, create Button on the second view
			// that will transition back to the first view
			button2C = new MHButton(MHButtonType.MHButtonTypeCustom);
			button2C.setBackgroundImage(texture1C, MHControlState.MHControlStateNormal);
			button2C.setTitle("Go Back!", MHControlState.MHControlStateNormal);
			button2C.frame = new Rect((screenSize.width/2f)-80f, (screenSize.height/2f)-100f, 160f, 40f);
			button2C.addActionForControlEvents(TransitionToFirstView, MHControlEvents.MHControlEventTouchUpInside);
			view2C.addSubview(button2C);
				
			// On animation end, set the viewcontroller's view to the second view and hide the first view
			viewController1A.view = view2C;
			view1A.hidden = true;
		}
		
		// Enable user interaction again
		viewController1A.view.userInteractionEnabled = true;
	}
	
	public void TransitionToFirstView()
	{	
		// When transitioning back to the first view, make sure the first view is not hidden
		// Then transition to the first view by dissolving
		view1A.hidden = false;
		view1A.transitionFromView(view2C, view1A, .5f, MHViewAnimationOptions.MHViewAnimationOptionTransitionCrossDissolve, DissolveFinished);
	}
	
	public void DissolveFinished(bool finished)
	{
		// When the dissolve is finished, set the view and enable user interaction
		viewController1A.view = view1A;
		viewController1A.view.userInteractionEnabled = true;
		
		// And release the elements from the second view
		button2C.release();
		view2C.release();
	}

	public override void EndTutorial()
	{
		button2C.release();
		view2C.release();
		base.EndTutorial();
	}
}
