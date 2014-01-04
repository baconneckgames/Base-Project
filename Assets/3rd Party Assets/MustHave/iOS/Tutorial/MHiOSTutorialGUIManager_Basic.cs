using UnityEngine;
using System.Collections;

public partial class MHiOSTutorialGUIManager : Singleton<MHiOSTutorialGUIManager> {

	void BasicStart()
	{
		tutorials.Add(new Tutorial1A_ViewController());
		tutorials.Add(new Tutorial1B_CustomBKG());
		tutorials.Add(new Tutorial1C_CustomButton());
		tutorials.Add(new Tutorial1D_Labels());
		tutorials.Add(new Tutorial2A_MultipleViewControllers());
		tutorials.Add(new Tutorial2B_MultipleViewControllers2());
		tutorials.Add(new Tutorial2C_AlternateAnimationMethod());
		tutorials.Add(new Tutorial3A_PickerViews());
		tutorials.Add(new Tutorial3B_TextViews());
	}
	
	void BasicStart2()
	{
		tutorials.Add(new Tutorial6A_Switch());
		tutorials.Add(new Tutorial6B_Slider());
	}
}
