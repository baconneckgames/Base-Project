using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

public class MHiOS_MenuItems_Full : MonoBehaviour {
	[MenuItem ("Window/AntiLunchBox/MustHave iOS/Check For Updates", false, 100)]
	static void CheckForUpdates()
	{
		if(Application.internetReachability != NetworkReachability.NotReachable)
		{
			if(MHiOS_Autorun_Full.versionChecker && MHiOS_Autorun_Full.versionChecker.GetComponent<MHiOS_EditorUpdateCheck_Full>().querying)
			{
				if(MHiOS_Autorun_Full.versionChecker.GetComponent<MHiOS_EditorUpdateCheck_Full>().respondInAlerts)
					Debug.Log("Stopping previous queries for updates...");
				MHiOS_Autorun_Full.versionChecker.GetComponent<MHiOS_EditorUpdateCheck_Full>().StopChecking();
			}
			
			Debug.Log("Checking servers for updates...keep working in the meantime.");
			
			MHiOS_Autorun_Full.versionChecker = new GameObject("versionChecker", typeof(MHiOS_EditorUpdateCheck_Full));
			MHiOS_Autorun_Full.versionChecker.hideFlags = HideFlags.HideAndDontSave;
			
			MHiOS_Autorun_Full.versionChecker.GetComponent<MHiOS_EditorUpdateCheck_Full>().QueryUpdates(true);
		}
		else
		{
			EditorUtility.DisplayDialog("No Internet Connection...", "You must be connected to the internet to check for updates.", "OK");
		}
	}
	/*
	[MenuItem ("Window/AntiLunchBox/MustHave iOS/Documentation", false, 300)]
	static void Documentation()
	{
		Application.OpenURL("http://antilunchbox.com/musthave-ios/musthave-ios-documentation/");
	}
	[MenuItem ("Window/AntiLunchBox/MustHave iOS/Forum", false, 301)]
	static void Forum()
	{
		Application.OpenURL("http://forum.unity3d.com/threads/186067-RELEASED-SoundManagerPro-Easy-Game-Music-Plugin");
	}*/
	[MenuItem ("Window/AntiLunchBox/MustHave iOS/About", false, 302)]
	static void About()
	{
		Application.OpenURL("http://antilunchbox.com/musthave-ios/");
	}
}
