using UnityEngine;
using System.Collections;

public class Tutorial1D_Labels : Tutorial1C_CustomButton
{
	public MHLabel label1D;
	
	public Tutorial1D_Labels(){}
	
	public override string GetButtonText()
	{
		return "Add a Label";
	}
	
	public override string GetTutorialDescription()
	{
		return "Tutorial1D:\nThis section shows you how to make a label";
	}
	
	public override void StartTutorial()
	{
		// Do the same stuff from Tutorial1A and 1B and 1C
		base.StartTutorial();
		
		// Create the button, add the dismiss action from 1A
		// Set the image to the texture2D
		// Set the title to lay on top of the image
		// Set the position below the 1A button
		label1D = new MHLabel(new Rect((screenSize.width/2f)-80f, (screenSize.height/2f)-40f, 160f, 40f));
		label1D.text = "Press Either Button";
		label1D.textAlignment = MHTextAlignment.MHTextAlignmentCenter;
		label1D.textColor = Color.white;
		view1A.addSubview(label1D);
	}

	public override void EndTutorial()
	{
		label1D.release();
		base.EndTutorial();
	}
}
