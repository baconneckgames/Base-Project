#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections;

[ExecuteInEditMode()]
public class MHiOS_EditorUpdateCheck_Full : MonoBehaviour {
	const string prelink = "http://www.antilunchbox.com/checkupdates-musthaveios";
	const string postlink = "-full.php";
	const string assetName = "MHiOS Full Combo Pack";
	const int assetURL_ID = 12067;
	int currentVersion = 120;
	
	
	public bool respondInAlerts = false;
	public bool readyForAction = false;
	public bool readyToDestroy = false;
	public string falseMessage = "";
	public bool querying = false;
	double startTime;
	double timeout = 30;
	public WWW www;
	
	void Awake()
	{
		gameObject.hideFlags = HideFlags.HideAndDontSave;
		querying = true;
		startTime = EditorApplication.timeSinceStartup;
	}
	
	void Update()
	{
		if(readyForAction)
		{
			readyForAction = false;
			if(!string.IsNullOrEmpty(falseMessage) && EditorUtility.DisplayDialog("Update Available!", falseMessage, "Update Now!", "No Thanks"))
				Application.OpenURL("com.unity3d.kharma:content/"+assetURL_ID.ToString());
			querying = false;
		}
		if(!querying)
		{
			StopChecking();
		}
		else
		{
			if(EditorApplication.timeSinceStartup > startTime + timeout)
			{
				if(respondInAlerts)
					Debug.Log(assetName + " update request timed out. Try again.");
				StopChecking();
			}
		}
	}
	
	public void QueryUpdates(bool alerts)
	{
		WWWForm form = new WWWForm();
		form.AddField("version", currentVersion);
		
		respondInAlerts = alerts;
		StartCoroutine("CheckUpdate", form);
	}
	
	void OnDestroy()
	{
		StopAllCoroutines();
		if (www != null)
		{
			www.Dispose();
			www = null;
		}
	}
	
	IEnumerator CheckUpdate(WWWForm form)
	{
		www = new WWW(prelink+postlink, form);
		yield return www;
		
		if(www.error != null) 
		{
			if(respondInAlerts) 
				Debug.LogWarning("Error checking "+assetName+" for updates:\n" + www.error);
			querying = false;
			yield break;
		}  
		else 
		{
			string[] results = www.text.Split('~');
			int count = results.Length;
			
			if(results[0] == "true")
			{
				if(respondInAlerts) 
					Debug.Log(assetName+" is up to date.");
				if(count > 1)
					for(int i = 1; i < count; i++)
						Debug.Log(results[i]);
				querying = false;
			}
			else if(results[0] == "false")
			{
				bool firstMessage = false;
				for(int i = 1; i < count; i++)
				{
					if(!respondInAlerts)
					{
						Debug.Log(results[i]);
						querying = false;
					}
					else if (!firstMessage)
					{
						firstMessage = true;
						falseMessage = results[i];
						readyForAction = true;
						yield break;
					}
				}
			}
		}
	}
	
	public void StopChecking()
	{
		StopAllCoroutines();
		if (www != null)
		{
			www.Dispose();
			www = null;
		}
		DestroyImmediate(gameObject);
	}
}
#endif