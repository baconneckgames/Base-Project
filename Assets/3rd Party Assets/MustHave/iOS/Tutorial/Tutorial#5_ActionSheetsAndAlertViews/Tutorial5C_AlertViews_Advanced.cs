using UnityEngine;
using System.Collections;

public class Tutorial5C_AlertViews_Advanced : ALBBaseTutorial
{
	public MHAlertView alertview5C;
	
	public Tutorial5C_AlertViews_Advanced(){}
	
	public override string GetButtonText()
	{
		return "Show An Advanced\nAlertView";
	}
	
	public override string GetTutorialDescription()
	{
		return "Tutorial5C:\nThis section shows you how to present an advanced AlertView. It will display options to share socially.";
	}
	
	public override void StartTutorial()
	{
		base.StartTutorial();
		
		// Create the alertview with the title, message, and cancel button
		// Feel free to change the index of the buttons by setting the button index
		alertview5C = new MHAlertView("Share?", "Pick a Method", "No Thanks", "Facebook", "Twitter");
		
		// Assign delegate methods
		alertview5C.alertViewClickedButtonAtIndex += ClickedAlertViewWithButtonIndex;
		alertview5C.alertViewDidDismissWithButtonIndex += DimissedAlertViewWithButtonIndex;
		
		// Show it! That's it!
		alertview5C.show();
	}
	
	public void ClickedAlertViewWithButtonIndex(MHAlertView alert, int buttonIndex)
	{
		alertview5C.alertViewDidDismissWithButtonIndex -= ClickedAlertViewWithButtonIndex;
		
		// Handle what to do when each button is pressed
		
		// Get the button title
		string buttonTitle = alert.buttonTitleAtIndex(buttonIndex);
		
		// Determine what to do with each button:
		switch(buttonTitle)
		{
		case "Facebook":
			Application.OpenURL("https://www.facebook.com/antilunchbox");
			break;
		case "Twitter":
			Application.OpenURL("https://twitter.com/antilunchbox");
			break;
		case "No Thanks":
		default:
			return;
		}
	}
	
	public void DimissedAlertViewWithButtonIndex(MHAlertView alert, int buttonIndex)
	{
		alertview5C.alertViewDidDismissWithButtonIndex -= DimissedAlertViewWithButtonIndex;
		
		EndTutorial();
	}

	public override void EndTutorial()
	{
		// Remember to release when you're done, 
		// set them to null as well if you want
		alertview5C.release();
		base.EndTutorial();
	}
}
