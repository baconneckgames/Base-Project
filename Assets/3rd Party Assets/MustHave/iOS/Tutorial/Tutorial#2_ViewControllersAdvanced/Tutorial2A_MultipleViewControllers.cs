using UnityEngine;
using System.Collections;

public class Tutorial2A_MultipleViewControllers : Tutorial1D_Labels
{
	public MHView view2A;
	public MHImageView imageview2A;
	public Texture2D texture2A;
	public MHButton button2A;
	
	public Tutorial2A_MultipleViewControllers(){}
	
	public override string GetButtonText()
	{
		return "Add a Second View\nWith Transitions";
	}
	
	public override string GetTutorialDescription()
	{
		return "Tutorial2A:\nThis section shows you how to make a second View and transition between the two";
	}
	
	public override void StartTutorial()
	{
		// Do the same stuff from Tutorial1
		base.StartTutorial();
		
		// Change the custom button to now go to our second View
		button1C.setTitle("Transition", MHControlState.MHControlStateNormal);
		button1C.removeActionForControlEvents(Dismiss_1C, MHControlEvents.MHControlEventTouchUpInside);
		button1C.addActionForControlEvents(TransitionToSecondView, MHControlEvents.MHControlEventTouchUpInside);
		
		// Create a FullScreen View
		view2A = new MHView(screenSize);
		view2A.backgroundColor = Color.white;
		
		// For consitency sake, we'll change the title though it's not visible yet (for later tutorials)
		viewController1A.title = "Tutorial2A";
		
		// Create Button on the second view that will transition back to the first view
		button2A = new MHButton(MHButtonType.MHButtonTypeCustom);
		button2A.setBackgroundImage(texture1C, MHControlState.MHControlStateNormal);
		button2A.setTitle("Go Back!", MHControlState.MHControlStateNormal);
		button2A.frame = new Rect((screenSize.width/2f)-80f, (screenSize.height/2f)-100f, 160f, 40f);
		button2A.addActionForControlEvents(TransitionToFirstView, MHControlEvents.MHControlEventTouchUpInside);
		view2A.addSubview(button2A);
		
		// Lets get the background texture for the second view
		if(texture2A == null)
			texture2A = Resources.Load(GetBackgroundImageForDevice("bkg2")) as Texture2D;
		
		// Create an ImageView and set it
		imageview2A = new MHImageView(texture2A);
		view2A.addSubview(imageview2A);
		view2A.sendSubviewToBack(imageview2A);
		imageview2A.frame = screenSize;
	}
	
	public void TransitionToSecondView()
	{
		view2A.transitionFromView(view1A, view2A, .5f, MHViewAnimationOptions.MHViewAnimationOptionTransitionFlipFromLeft, null);
	}
	
	public void TransitionToFirstView()
	{	
		view2A.transitionFromView(view2A, view1A, .5f, MHViewAnimationOptions.MHViewAnimationOptionTransitionFlipFromRight, null);
	}

	public override void EndTutorial()
	{
		button2A.release();
		imageview2A.release();
		view2A.release();
		base.EndTutorial();
	}
}
