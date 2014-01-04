using UnityEngine;
using System.Collections;

public partial class MHiOSTutorialGUIManager : Singleton<MHiOSTutorialGUIManager> {

	void NavStart()
	{
		tutorials.Add(new Tutorial4A_NavigationController());
		tutorials.Add(new Tutorial4B_PopoverController());
	}
}
