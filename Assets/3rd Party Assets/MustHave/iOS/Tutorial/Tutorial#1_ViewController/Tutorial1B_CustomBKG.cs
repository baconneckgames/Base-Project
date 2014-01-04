using UnityEngine;
using System.Collections;

public class Tutorial1B_CustomBKG : Tutorial1A_ViewController
{
	public MHImageView imageview1B;
	public Texture2D texture1B;
	
	public Tutorial1B_CustomBKG(){}
	
	public override string GetButtonText()
	{
		return "Show ViewController\nWith Custom BKG";
	}
	
	public override string GetTutorialDescription()
	{
		return "Tutorial1B:\nThis section shows you how add a custom background to your View using an ImageView";
	}
	
	public override void StartTutorial()
	{
		// Do the same stuff from Tutorial1A
		base.StartTutorial();
		
		// Lets get the texture, get it however you want
		// If you don't load from Resources, it will be faster
		// NOTE: The first time you load a texture it will be slower than usual.
		// After that, it will be faster because it will not overwrite the file.
		// To force OverWrite, set MHTools.OverWriteNextSaveTexture = true
		if(texture1B == null)
			texture1B = Resources.Load(GetBackgroundImageForDevice("bkg1")) as Texture2D;
		
		// Create an ImageView using the Texture2D
		// Add the ImageView to our View made in 1A, and 
		// send it to the back so it doesn't overlap anything else.
		// Set the frame also to full screen.
		imageview1B = new MHImageView(texture1B);
		view1A.addSubview(imageview1B);
		view1A.sendSubviewToBack(imageview1B);
		imageview1B.frame = screenSize;
	}
	
	public string GetBackgroundImageForDevice(string imgName)
	{
		// Could also use MHTools.IsIpad and MHTools.IsRetina
		switch(iPhone.generation)
		{
		case iPhoneGeneration.iPad1Gen:
		case iPhoneGeneration.iPad2Gen:
		case iPhoneGeneration.iPadMini1Gen:
			return imgName+"_ipad";
		case iPhoneGeneration.iPhone:
		case iPhoneGeneration.iPhone3G:
		case iPhoneGeneration.iPhone3GS:
		case iPhoneGeneration.iPodTouch1Gen:
		case iPhoneGeneration.iPodTouch2Gen:
		case iPhoneGeneration.iPodTouch3Gen:
			return imgName+"_iphone";
		case iPhoneGeneration.iPad3Gen:
		case iPhoneGeneration.iPad4Gen:
			return imgName+"_ipadretina";
		case iPhoneGeneration.iPhone4:
		case iPhoneGeneration.iPhone4S:
		case iPhoneGeneration.iPhone5:
		case iPhoneGeneration.iPodTouch4Gen:
		case iPhoneGeneration.iPodTouch5Gen:
			return imgName+"_iphoneretina";
		case iPhoneGeneration.iPadUnknown:
			return imgName+"_ipadretina";	
		case iPhoneGeneration.iPhoneUnknown:
		case iPhoneGeneration.iPodTouchUnknown:
			return imgName+"_iphoneretina";
		case iPhoneGeneration.Unknown:
		default:
			return imgName+"_iphone";
		}
	}

	public override void EndTutorial()
	{
		imageview1B.release();
		base.EndTutorial();
	}
}
