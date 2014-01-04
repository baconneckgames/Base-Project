using UnityEngine;
using System.Collections;

public partial class MHiOSTutorialGUIManager : Singleton<MHiOSTutorialGUIManager> {

	void ActionStart()
	{
		tutorials.Add(new Tutorial5A_AlertViews());
		tutorials.Add(new Tutorial5B_ActionSheets());
		tutorials.Add(new Tutorial5C_AlertViews_Advanced());
		tutorials.Add(new Tutorial5D_ActionSheets_Advanced());
	}
}
