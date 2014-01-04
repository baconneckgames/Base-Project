using UnityEngine;
using System.Collections;

public class Tutorial5B_ActionSheets : ALBBaseTutorial
{
	public MHActionSheet actionsheet5B;
	
	public Tutorial5B_ActionSheets(){}
	
	public override string GetButtonText()
	{
		return "Show A Basic\nActionSheet";
	}
	
	public override string GetTutorialDescription()
	{
		return "Tutorial5B:\nThis section shows you how to present a simple ActionSheet.";
	}
	
	public override void StartTutorial()
	{
		base.StartTutorial();
		
		// Create the actionsheet with a title, and a cancel button or OK button (no destructive button or other buttons)
		// NOTE: CANCELBUTTONS ARE NOT ENCOURAGED ON IPADS
		if(MHTools.IsIpad)
			actionsheet5B = new MHActionSheet("Simple ActionSheet", null, null, "OK");
		else
			actionsheet5B = new MHActionSheet("Simple ActionSheet", "OK", null);
		
		// Assign delegate methods
		actionsheet5B.actionSheetClickedButtonAtIndex += ClickedActionSheetWithButtonIndex;
		actionsheet5B.actionSheetDidDismissWithButtonIndex += DimissedActionSheetWithButtonIndex;
		
		// Show it! That's it!
		actionsheet5B.showInView(MHView.unityView);
	}
	
	public void ClickedActionSheetWithButtonIndex(MHActionSheet actionsheeet, int buttonIndex)
	{
		actionsheet5B.actionSheetClickedButtonAtIndex -= ClickedActionSheetWithButtonIndex;
	}
	
	public void DimissedActionSheetWithButtonIndex(MHActionSheet actionsheeet, int buttonIndex)
	{
		actionsheet5B.actionSheetDidDismissWithButtonIndex -= DimissedActionSheetWithButtonIndex;
		
		EndTutorial();
	}
	public override void EndTutorial()
	{
		// Remember to release when you're done, 
		// set them to null as well if you want
		actionsheet5B.release();
		base.EndTutorial();
	}
}
