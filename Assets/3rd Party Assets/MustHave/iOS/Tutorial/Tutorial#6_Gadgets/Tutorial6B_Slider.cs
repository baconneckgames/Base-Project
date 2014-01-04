using UnityEngine;
using System.Collections;

public class Tutorial6B_Slider : Tutorial1A_ViewController {
	
	public MHSlider slider6B;
	public MHLabel label6B;
	
	public Tutorial6B_Slider(){}
	
	public override string GetButtonText()
	{
		return "Show A Slider";
	}
	
	public override string GetTutorialDescription()
	{
		return "Tutorial6B:\nThis section shows you how add a slider and use the result.";
	}
	
	public override void StartTutorial()
	{
		// Do the same stuff from Tutorial1A
		base.StartTutorial();
		
		// Create the Slider in a rect.
		slider6B = new MHSlider(new Rect((screenSize.width/2f)-80f, (screenSize.height/2f)+20f, 160f, 40f));
		
		// Now we have to add actions for the slider. Sliders, according to Apple documentation, respond to
		// the valuechanged control event.  So let's make it call the UpdateLights function.
		slider6B.addActionForControlEvents(UpdateLights, MHControlEvents.MHControlEventValueChanged);
		// Set a clear background color so it's see through
		slider6B.backgroundColor = Color.clear;
		// Set value range
		slider6B.minimumValue = 0f;
		slider6B.maximumValue = 10f;
		// Set it so that UpdateLights will continuously be called when dragging.  Otherwise, it
		// will only call when the user lifts their finger.
		slider6B.continuous = true;
		// Set initial slider value to the middle.
		slider6B.val = 5f;
		
		// Finally, we have to add the slider to the view so you can see it! Always remember this!
		view1A.addSubview(slider6B);
		
		// Now for some additional label set up to show you result management. Also setting the beginning light state
		// to the status of the switch.
		label6B = new MHLabel(new Rect((screenSize.width/2f)-80f, (screenSize.height/2f)+60f, 160f, 40f));
		label6B.textAlignment = MHTextAlignment.MHTextAlignmentCenter;
		label6B.textColor = Color.red;
		view1A.addSubview(label6B);
		UpdateLights();
	}
	
	public void UpdateLights()
	{
		float percent = slider6B.val / slider6B.maximumValue;
		view1A.backgroundColor = new Color(percent, percent, percent, 1f);
		label6B.text = slider6B.val.ToString();
	}

	public override void EndTutorial()
	{
		slider6B.release();
		label6B.release();
		base.EndTutorial();
	}
}
