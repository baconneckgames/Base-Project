using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class MHView : MHObject {
	public static MHView unityView
	{
		get {
			MHView unityvw = new MHView();
			unityvw.tag = 0;
			return unityvw;
		}
	}
}
