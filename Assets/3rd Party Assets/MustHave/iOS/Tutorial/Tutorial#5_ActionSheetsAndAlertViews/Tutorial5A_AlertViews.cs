using UnityEngine;
using System.Collections;

public class Tutorial5A_AlertViews : ALBBaseTutorial
{
	public MHAlertView alertview5A;
	
	public Tutorial5A_AlertViews(){}
	
	public override string GetButtonText()
	{
		return "Show A Basic\nAlertView";
	}
	
	public override string GetTutorialDescription()
	{
		return "Tutorial5A:\nThis section shows you how to present a simple AlertView.";
	}
	
	public override void StartTutorial()
	{
		base.StartTutorial();
		
		// Create the alertview with the title, message, and cancel button
		alertview5A = new MHAlertView("Simple AlertView", "Simply Done", "OK");
		
		// Assign delegate methods
		alertview5A.alertViewClickedButtonAtIndex += ClickedAlertViewWithButtonIndex;
		alertview5A.alertViewDidDismissWithButtonIndex += DimissedAlertViewWithButtonIndex;
		
		// Show it! That's it!
		alertview5A.show();
	}
	
	public void ClickedAlertViewWithButtonIndex(MHAlertView alert, int buttonIndex)
	{
		alertview5A.alertViewDidDismissWithButtonIndex -= ClickedAlertViewWithButtonIndex;
	}
	
	public void DimissedAlertViewWithButtonIndex(MHAlertView alert, int buttonIndex)
	{
		alertview5A.alertViewDidDismissWithButtonIndex -= DimissedAlertViewWithButtonIndex;
		
		EndTutorial();
	}

	public override void EndTutorial()
	{
		// Remember to release when you're done, 
		// set them to null as well if you want
		alertview5A.release();
		base.EndTutorial();
	}
}
