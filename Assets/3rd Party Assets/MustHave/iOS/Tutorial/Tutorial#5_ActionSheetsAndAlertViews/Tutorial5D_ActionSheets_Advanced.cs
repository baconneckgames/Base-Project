using UnityEngine;
using System.Collections;

public class Tutorial5D_ActionSheets_Advanced : ALBBaseTutorial
{
	public MHActionSheet actionsheet5D;
	
	public Tutorial5D_ActionSheets_Advanced(){}
	
	public override string GetButtonText()
	{
		return "Show An Advanced\nActionSheet";
	}
	
	public override string GetTutorialDescription()
	{
		return "Tutorial5D:\nThis section shows you how to present an advanced ActionSheet. It will display options to share socially.";
	}
	
	public override void StartTutorial()
	{
		base.StartTutorial();
		
		// Create the actionsheet with a cancel button, destructive button, and other buttons
		// NOTE: CANCELBUTTONS ARE NOT ENCOURAGED ON IPAD
		// Feel free to change the index of the buttons by setting the button index
		if(MHTools.IsIpad)
			actionsheet5D = new MHActionSheet("Share?", null, "Stop Asking", "Facebook", "Twitter");
		else
			actionsheet5D = new MHActionSheet("Share?", "No Thanks", "Stop Asking", "Facebook", "Twitter");
		
		// Assign delegate methods
		actionsheet5D.actionSheetClickedButtonAtIndex += ClickedActionSheetWithButtonIndex;
		actionsheet5D.actionSheetDidDismissWithButtonIndex += DimissedActionSheetWithButtonIndex;
		
		// Show it! That's it!
		actionsheet5D.showInView(MHView.unityView);
	}
	
	public void ClickedActionSheetWithButtonIndex(MHActionSheet actionsheet, int buttonIndex)
	{
		actionsheet5D.actionSheetClickedButtonAtIndex -= ClickedActionSheetWithButtonIndex;
		
		// Handle what to do when each button is pressed
		
		// Get the button title
		if(buttonIndex < 0)
			return; // is dismissed with no cancel button
		string buttonTitle = actionsheet.buttonTitleAtIndex(buttonIndex);
		
		// Determine what to do with each button:
		switch(buttonTitle)
		{
		case "Facebook":
			Application.OpenURL("https://www.facebook.com/antilunchbox");
			break;
		case "Twitter":
			Application.OpenURL("https://twitter.com/antilunchbox");
			break;
		case "Stop Asking":
			// Do something to stop bringing the actionsheet up
			break;
		case "No Thanks":
		default:
			return;
		}
	}
	
	public void DimissedActionSheetWithButtonIndex(MHActionSheet actionsheeet, int buttonIndex)
	{
		actionsheet5D.actionSheetDidDismissWithButtonIndex -= DimissedActionSheetWithButtonIndex;
		
		EndTutorial();
	}
	public override void EndTutorial()
	{
		// Remember to release when you're done, 
		// set them to null as well if you want
		actionsheet5D.release();
		base.EndTutorial();
	}
}
