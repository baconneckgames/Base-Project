using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class MHViewController : MHObject {
	#region functions_expansion
	public MHNavigationController navigationController {
		get {
			return MHiOSManager.Instance.navigationController(tag);
		}
	}
	
	public MHNavigationItem navigationItem {
		get {
			return MHiOSManager.Instance.navigationItem(tag);
		}
	}
	
	public Vector2 contentSizeForViewInPopover {
		get {
			return MHiOSManager.Instance.contentSizeForViewInPopover(tag);
		} set {
			MHiOSManager.Instance.contentSizeForViewInPopover(tag, value);
		}
	}
	
	public bool modalInPopover {
		get {
			return MHiOSManager.Instance.modalInPopover(tag);
		} set {
			MHiOSManager.Instance.modalInPopover(tag, value);
		}
	}
	#endregion
}
