using UnityEngine;
using System.Collections;

public partial class MHToolbar : MHView {
	#region functions
	public MHToolbar(MHNavigationController navController)
	{
		if(navController != null)
			MHiOSManager.Instance.syncToolbar(tag, navController);
	}
	#endregion
}
