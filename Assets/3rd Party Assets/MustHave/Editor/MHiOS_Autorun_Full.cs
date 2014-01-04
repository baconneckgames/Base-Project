using UnityEngine;
using UnityEditor;

[InitializeOnLoad()]
public class MHiOS_Autorun_Full
{
	private static string preReviewID = "MHiOS_";
	private static string postReviewID = "Full_";
	private static string assetName = "MHiOS Full Combo Pack";
	private static int assetURL_ID = 9209;
	private static bool deleteOthers = true; //switch to true
	
	private static int reviewPromptIntervalFull = 50;
	private static int reviewPromptInitialFull = 100;
	
	public static GameObject versionChecker;
	
	static MHiOS_Autorun_Full()
	{
		EditorApplication.update += Reviews;
	}
	
	static void Reviews()
	{
		if(deleteOthers)
			DeleteOtherFiles();
		LookForUpdates();
		EvaluateTimesOpened();
		CheckPrompts();
		EditorApplication.update -= Reviews;
	}
	
	static void DeleteOtherFiles()
	{
		bool hasToDelete = false;
		
		if(AssetDatabase.LoadMainAssetAtPath("Assets/MustHave/Editor/MHiOS_Autorun_AlertAction.cs") != null)
			hasToDelete = true;
		else if(AssetDatabase.LoadMainAssetAtPath("Assets/MustHave/Editor/MHiOS_Autorun_PopNav.cs") != null)
			hasToDelete = true;
		else if(AssetDatabase.LoadMainAssetAtPath("Assets/MustHave/Editor/MHiOS_Autorun_Basic.cs") != null)
			hasToDelete = true;
		else if(AssetDatabase.LoadMainAssetAtPath("Assets/MustHave/EditorHelper/MHiOS_EditorUpdateCheck_AlertAction.cs") != null)
			hasToDelete = true;
		else if(AssetDatabase.LoadMainAssetAtPath("Assets/MustHave/EditorHelper/MHiOS_EditorUpdateCheck_PopNav.cs") != null)
			hasToDelete = true;
		else if(AssetDatabase.LoadMainAssetAtPath("Assets/MustHave/EditorHelper/MHiOS_EditorUpdateCheck_Basic.cs") != null)
			hasToDelete = true;
		else if(AssetDatabase.LoadMainAssetAtPath("Assets/MustHave/Editor/MHiOS_MenuItems_AlertAction") != null)
			hasToDelete = true;
		else if(AssetDatabase.LoadMainAssetAtPath("Assets/MustHave/Editor/MHiOS_MenuItems_PopNav") != null)
			hasToDelete = true;
		else if(AssetDatabase.LoadMainAssetAtPath("Assets/MustHave/Editor/MHiOS_MenuItems_Basic") != null)
			hasToDelete = true;
		
		if(hasToDelete)
			hasToDelete = EditorUtility.DisplayDialog("Must Have iOS Full Combo - Remove old files", "You should remove files from previously purchased packs now that you have the full combo pack. We can remove them for you if you want.", "Remove Files", "I'll do it");
		
		if(hasToDelete)
		{
			if(AssetDatabase.DeleteAsset("Assets/MustHave/Editor/MHiOS_Autorun_AlertAction.cs"))
				Debug.LogWarning("Removed old editor file used before full combo pack purchase: MHiOS_Autorun_AlertAction.cs");
			if(AssetDatabase.DeleteAsset("Assets/MustHave/Editor/MHiOS_Autorun_PopNav.cs"))
				Debug.LogWarning("Removed old editor file used before full combo pack purchase: MHiOS_Autorun_PopNav.cs");
			if(AssetDatabase.DeleteAsset("Assets/MustHave/Editor/MHiOS_Autorun_Basic.cs"))
				Debug.LogWarning("Removed old editor file used before full combo pack purchase: MHiOS_Autorun_Basic.cs");
			if(AssetDatabase.DeleteAsset("Assets/MustHave/EditorHelper/MHiOS_EditorUpdateCheck_AlertAction.cs"))
				Debug.LogWarning("Removed old editor file used before full combo pack purchase: MHiOS_EditorUpdateCheck_AlertAction.cs");
			if(AssetDatabase.DeleteAsset("Assets/MustHave/EditorHelper/MHiOS_EditorUpdateCheck_PopNav.cs"))
				Debug.LogWarning("Removed old editor file used before full combo pack purchase: MHiOS_EditorUpdateCheck_PopNav.cs");
			if(AssetDatabase.DeleteAsset("Assets/MustHave/EditorHelper/MHiOS_EditorUpdateCheck_Basic.cs"))
				Debug.LogWarning("Removed old editor file used before full combo pack purchase: MHiOS_EditorUpdateCheck_Basic.cs");
			if(AssetDatabase.DeleteAsset("Assets/MustHave/Editor/MHiOS_MenuItems_AlertAction"))
				Debug.LogWarning("Removed old editor file used before full combo pack purchase: MHiOS_MenuItems_AlertAction.cs");
			if(AssetDatabase.DeleteAsset("Assets/MustHave/Editor/MHiOS_MenuItems_PopNav"))
				Debug.LogWarning("Removed old editor file used before full combo pack purchase: MHiOS_MenuItems_PopNav.cs");
			if(AssetDatabase.DeleteAsset("Assets/MustHave/Editor/MHiOS_MenuItems_Basic"))
				Debug.LogWarning("Removed old editor file used before full combo pack purchase: MHiOS_MenuItems_Basic.cs");
		}
	}
	
	static void LookForUpdates()
	{
		if(Application.internetReachability != NetworkReachability.NotReachable)
		{
			if(!(versionChecker && versionChecker.GetComponent<MHiOS_EditorUpdateCheck_Full>().querying))
			{
				versionChecker = new GameObject("versionChecker", typeof(MHiOS_EditorUpdateCheck_Full));
				versionChecker.hideFlags = HideFlags.HideAndDontSave;
				
				versionChecker.GetComponent<MHiOS_EditorUpdateCheck_Full>().QueryUpdates(false);
			}
		}
	}
	
	static void EvaluateTimesOpened()
	{
		EditorPrefs.SetInt(preReviewID+postReviewID+"Opened", EditorPrefs.GetInt(preReviewID+postReviewID+"Opened", 0) + 1);

		if(EditorPrefs.GetInt(preReviewID+postReviewID+"Opened", 0) > 5000)
			EditorPrefs.SetInt(preReviewID+postReviewID+"Opened", EditorPrefs.GetInt(preReviewID+postReviewID+"Opened", 0) -5000 + reviewPromptInitialFull);

		string todaysDate = System.DateTime.Now.ToString("M/d/yyyy");
		if(EditorPrefs.GetString("WasLastPrompted", todaysDate) != todaysDate)
		{
			if(EditorPrefs.GetInt(preReviewID+postReviewID+"Opened", 0) >= reviewPromptInitialFull)
			{
				if(!EditorPrefs.GetBool(preReviewID+postReviewID+"PassedInitialPrompt", false))
				{
					EditorPrefs.SetBool(preReviewID+postReviewID+"PassedInitialPrompt", true);
					EditorPrefs.SetBool(preReviewID+postReviewID+"PromptReview", true);
				}
				else if(EditorPrefs.GetBool(preReviewID+postReviewID+"ReviewPromptActivated", true) && EditorPrefs.GetInt(preReviewID+postReviewID+"Opened", 0) % reviewPromptIntervalFull == 0)
					EditorPrefs.SetBool(preReviewID+postReviewID+"PromptReview", true);
			}
		}
	}

	static void CheckPrompts()
	{
		if(Application.isPlaying)
			return;
		bool wasPrompted = false;

		if(EditorPrefs.GetBool(preReviewID+postReviewID+"PromptReview", false))
		{
			wasPrompted = true;
			EditorPrefs.SetBool(preReviewID+postReviewID+"PromptReview", false);
			int option = EditorUtility.DisplayDialogComplex ("Found "+assetName+" Useful?", "Please rate us in the Unity Asset Store (hopefully a good rating!).\n\nIt won't take more than a minute.  We'll make it easy and take you directly there!\nThanks for your support!\n\nP.S. - Keep in mind that reviews aren't sent to the developer so keep bug reports to the forums please! We'll be able to respond in a timely manner there.", "Rate Now!", "Remind Me Later", "No Thanks");
			switch(option)
			{
				case 0: //YES
				Application.OpenURL("com.unity3d.kharma:content/"+assetURL_ID.ToString());
				EditorPrefs.SetBool(preReviewID+postReviewID+"ReviewPromptActivated", false);
				break;
				case 1: //Later
				EditorPrefs.SetBool(preReviewID+postReviewID+"ReviewPromptActivated", true);
				break;
				case 2: //NO
				EditorPrefs.SetBool(preReviewID+postReviewID+"ReviewPromptActivated", false);
				break;
				default: //unrecognized option
				EditorPrefs.SetBool(preReviewID+postReviewID+"ReviewPromptActivated", false);
				break;
			}
		}

		if(wasPrompted)
			EditorPrefs.SetString("WasLastPrompted", System.DateTime.Now.ToString("M/d/yyyy"));
	}
}
