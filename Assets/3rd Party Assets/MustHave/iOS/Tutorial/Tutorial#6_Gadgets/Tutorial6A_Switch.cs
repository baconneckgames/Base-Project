using UnityEngine;
using System.Collections;

public class Tutorial6A_Switch : Tutorial1A_ViewController {
	
	public MHSwitch switch6A;
	public MHLabel label6A;
	
	public Tutorial6A_Switch(){}
	
	public override string GetButtonText()
	{
		return "Show A Switch";
	}
	
	public override string GetTutorialDescription()
	{
		return "Tutorial6A:\nThis section shows you how add a switch and use the result.";
	}
	
	public override void StartTutorial()
	{
		// Do the same stuff from Tutorial1A
		base.StartTutorial();
		
		// Create the Switch in a rect. Just initialize with a zero rect since UISwitches determine their own size.
		switch6A = new MHSwitch(MHTools.RectZero);
		// Use the frame to get the correct position. Only use the first two for positioning for the reason above.
		float switchWidth = switch6A.frame.width;
		switch6A.frame = new Rect((screenSize.width/2f)-(switchWidth/2f), (screenSize.height/2f)+20f, 0f, 0f);
		
		// Now we have to add actions for the switch. Switches, according to Apple documentation, respond to
		// the valuechanged control event.  So let's make it call the FlipLights function.
		switch6A.addActionForControlEvents(FlipLights, MHControlEvents.MHControlEventValueChanged);
		
		// Finally, we have to add the switch to the view so you can see it! Always remember this!
		view1A.addSubview(switch6A);
		
		// Now for some additional label set up to show you result management. Also setting the beginning light state
		// to the status of the switch.
		label6A = new MHLabel(new Rect((screenSize.width/2f)-80f, (screenSize.height/2f)+60f, 160f, 40f));
		label6A.textAlignment = MHTextAlignment.MHTextAlignmentCenter;
		view1A.addSubview(label6A);
		SetLights(switch6A.on);
	}
	
	public void FlipLights()
	{
		// This just sets the light to the new value.
		SetLights(switch6A.on);
	}
	
	public void SetLights(bool on)
	{
		// Based on the entry, the light is set.
		if(on)
		{
			label6A.text = "ON";
			label6A.textColor = Color.black;
			view1A.backgroundColor = Color.white;
		}
		else
		{
			label6A.text = "OFF";
			label6A.textColor = Color.white;
			view1A.backgroundColor = Color.black;
		}
	}

	public override void EndTutorial()
	{
		switch6A.release();
		label6A.release();
		base.EndTutorial();
	}
}
