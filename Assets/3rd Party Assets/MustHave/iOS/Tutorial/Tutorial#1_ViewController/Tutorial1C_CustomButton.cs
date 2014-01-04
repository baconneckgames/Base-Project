using UnityEngine;
using System.Collections;

public class Tutorial1C_CustomButton : Tutorial1B_CustomBKG
{
	public Texture2D texture1C;
	public MHButton button1C;
	
	public Tutorial1C_CustomButton(){}
	
	public override string GetButtonText()
	{
		return "Add a Custom Button";
	}
	
	public override string GetTutorialDescription()
	{
		return "Tutorial1C:\nThis section shows you how to make a custom button";
	}
	
	public override void StartTutorial()
	{
		// Do the same stuff from Tutorial1A and 1B
		base.StartTutorial();
		
		// Lets get the texture, get it however you want
		// If you don't load from Resources, it will be faster
		if(texture1C == null)
			texture1C = Resources.Load("EmptyButton") as Texture2D;
		
		// Create the button, add the dismiss action from 1A
		// Set the image to the texture2D
		// Set the title to lay on top of the image
		// Set the position below the 1A button
		button1C = new MHButton(MHButtonType.MHButtonTypeCustom);
		button1C.addActionForControlEvents(Dismiss_1C, MHControlEvents.MHControlEventTouchUpInside);
		button1C.setBackgroundImage(texture1C, MHControlState.MHControlStateNormal);
		button1C.setTitle("Dismiss v2", MHControlState.MHControlStateNormal);
		button1C.frame = new Rect((screenSize.width/2f)-80f, (screenSize.height/2f)+40f, 160f, 40f);
		view1A.addSubview(button1C);
	}
	
	public void Dismiss_1C()
	{
		// Dismiss the ViewController by animated it out. 
		// When the animation is done, call EndTutorial
		// When using actions, make sure each one is different
		// Since we'll be using this multiple times, I'm going to have them all call the same function
		Dismiss();
	}

	public override void EndTutorial()
	{
		button1C.release();
		base.EndTutorial();
	}
}
