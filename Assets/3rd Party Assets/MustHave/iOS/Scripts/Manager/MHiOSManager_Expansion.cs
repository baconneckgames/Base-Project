using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public partial class MHiOSManager : Singleton<MHiOSManager> {
	#region misc_expansion
	//toolbars and navbars are automatically created, so this just syncs the tag
	[DllImport("__Internal")]
    private static extern void _syncToolbar(int tag, int navController);
	public void syncToolbar(int tag, MHNavigationController navController)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int navTag = navController.tag;
			
			_syncToolbar(tag, navTag);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _syncNavbar(int tag, int navController);
	public void syncNavbar(int tag, MHNavigationController navController)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int navTag = navController.tag;
			
			_syncNavbar(tag, navTag);
		}
	}
	#endregion
}
