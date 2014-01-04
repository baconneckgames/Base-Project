using UnityEngine;
using System.Collections;

public class Tutorial2B_MultipleViewControllers2 : Tutorial2A_MultipleViewControllers
{
	public MHView view2B;
	public MHButton button2B;
	public MHButton button2B_2;
	
	public Tutorial2B_MultipleViewControllers2(){}
	
	public override string GetButtonText()
	{
		return "Add a Third View\nWith Different Transitions";
	}
	
	public override string GetTutorialDescription()
	{
		return "Tutorial2B:\nThis section shows you how to make a third View with a different transition";
	}
	
	public override void StartTutorial()
	{
		// Do the same stuff from Tutorial1 and 2A
		base.StartTutorial();
		
		// Create a FullScreen View
		view2B = new MHView(screenSize);
		view2B.backgroundColor = Color.white;
		
		// For consitency sake, we'll change the title though it's not visible yet (for later tutorials)
		viewController1A.title = "Tutorial2A";
		
		// Create an extra Button on the second view that will transition back to the third view
		button2B = new MHButton(MHButtonType.MHButtonTypeCustom);
		button2B.setBackgroundImage(texture1C, MHControlState.MHControlStateNormal);
		button2B.setTitle("Check Under Me!", MHControlState.MHControlStateNormal);
		button2B.frame = new Rect((screenSize.width/2f)-80f, (screenSize.height/2f)+100f, 160f, 40f);
		button2B.addActionForControlEvents(TransitionToThirdView, MHControlEvents.MHControlEventTouchUpInside);
		view2A.addSubview(button2B);
		
		// Create a button on the third view that will skip back to the first view
		// by playing through all the animations in succession
		button2B_2 = new MHButton(MHButtonType.MHButtonTypeCustom);
		button2B_2.setBackgroundImage(texture1C, MHControlState.MHControlStateNormal);
		button2B_2.setTitle("Skip To First!", MHControlState.MHControlStateNormal);
		button2B_2.frame = new Rect((screenSize.width/2f)-80f, (screenSize.height/2f)-100f, 160f, 40f);
		button2B_2.addActionForControlEvents(TransitionSkipToFirstView, MHControlEvents.MHControlEventTouchUpInside);
		view2B.addSubview(button2B_2);
	}
	
	public void TransitionToThirdView()
	{
		// Transition to the third view
		view2A.transitionFromView(view2A, view2B, .5f, MHViewAnimationOptions.MHViewAnimationOptionTransitionCurlUp, null);
	}
	
	public void TransitionSkipToFirstView()
	{
		// First Transition to the second view, then call AnimationConnection on completion
		view2A.transitionFromView(view2B, view2A, .5f, MHViewAnimationOptions.MHViewAnimationOptionTransitionCurlDown, AnimationConnection);
	}
	
	public void AnimationConnection(bool finished)
	{
		// Now call Transition to first view
		TransitionToFirstView();
	}

	public override void EndTutorial()
	{
		button2B.release();
		button2B_2.release();
		view2B.release();
		base.EndTutorial();
	}
}
