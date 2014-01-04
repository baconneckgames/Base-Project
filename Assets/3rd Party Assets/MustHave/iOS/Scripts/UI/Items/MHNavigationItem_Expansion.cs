using UnityEngine;
using System.Collections;

public partial class MHNavigationItem : MHObject {
	#region functions
	public MHNavigationItem(MHViewController viewController)
	{
		if(viewController != null)
			MHiOSManager.Instance.syncNavitem(tag, viewController);
	}
	#endregion
}
