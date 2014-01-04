using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public partial class MHiOSManager : Singleton<MHiOSManager> {
	public static MHiOSManager Instance {
		get {
			return ((MHiOSManager)mInstance);
		} set {
			mInstance = value;
		}
	}
	Dictionary<int, MHObject> objectsByTag = new Dictionary<int, MHObject>();
	Hashtable actionsByID = new Hashtable();
	int currentTag = int.MinValue;
	int currentID = int.MinValue;
	
	void Awake()
	{
		Instance = this;
		DontDestroyOnLoad(gameObject);
	}
	
	#region tag_handlers
	public static int CreateUniqueTagForObject(MHObject obj)
	{
		if(Instance.objectsByTag.ContainsValue(obj))
			foreach(KeyValuePair<int, MHObject> pair in Instance.objectsByTag)
				if(pair.Value == obj)
					return pair.Key;
		
		int iterations = 0;
		int maxIterations = 2;
		while(Instance.objectsByTag.ContainsKey(Instance.currentTag))
		{
			if(Instance.currentTag + 1 > int.MaxValue || Instance.currentTag + 1 == int.MinValue)
			{//if the current tag is at max int value or automatically wrapped
				iterations++;
				if(iterations >= maxIterations)
				{
					Debug.LogError("YOU HAVE WAY TOO MANY MHObjects! Release some before adding more! " +
						"Your app would've broken down by now anyway");
					return 0;
				}
				Instance.currentTag = int.MinValue;
			}
			else
			{
				Instance.currentTag++;
				if(Instance.currentTag == 0) //0 is reserved for unity view controller
					Instance.currentTag++;
			}
		}
		Instance.objectsByTag.Add(Instance.currentTag, obj);
		return Instance.currentTag;
	}
	
	[DllImport("__Internal")]
    private static extern void _release(int tag);
	public void release(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
			_release(tag);
	}
	
	[DllImport("__Internal")]
    private static extern void _pauseUnity(bool pause);
	public void pauseUnity(bool pause)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
			_pauseUnity(pause);
	}
	
	public static void RemoveUniqueTagForObject(int tag)
	{
		Instance.objectsByTag.Remove(tag);
	}
	
	public static MHObject GetObjectByUniqueTag(int tag)
	{
		if(tag != 0)
			return Instance.objectsByTag[tag];
		return MHViewController.unityViewController;
	}
	
	public static MHObject[] GetObjectsByUniqueTags(int[] tags)
	{
		MHObject[] objs = new MHObject[tags.Length];
		for(int i = 0; i < tags.Length; i++)
		{
			objs[i] = GetObjectByUniqueTag(tags[i]);
		}
		return objs;
	}
	#endregion
	
	#region action_handlers
	void RunActionByUniqueID(string jsonString)
	{
		int id = 0;
		
		Hashtable actionDecoded = jsonString.hashtableFromJson();
		if(actionDecoded != null)
		{
			if(Convert.ToString(actionDecoded["ObjectType"]) == "Action")
			{
				id = Convert.ToInt32(actionDecoded["ID"]);
				if(id == 0)
					return;
				
				var action = actionsByID[id];
				
				if(action != null)
				{
					if(action is Action<bool>)
					{
						bool param1 = Convert.ToBoolean(actionDecoded["Param1"]);
						(action as Action<bool>).Invoke(param1);
					}
					else if(action is Action<string>)
					{
						string param1 = Convert.ToString(actionDecoded["Param1"]);
						(action as Action<string>).Invoke(param1);
					}
					else if(action is Action<int>)
					{
						int param1 = Convert.ToInt32(actionDecoded["Param1"]);
						(action as Action<int>).Invoke(param1);
					}
					else if(action is Action<float>)
					{
						float param1 = Convert.ToSingle(actionDecoded["Param1"]);
						(action as Action<float>).Invoke(param1);
					}
					else if(action is Action)
					{
						(action as Action).Invoke();
					}
					//RemoveActionOfUniqueID(id);
				}
			}
		}
	}
	
	int SaveActionToUniqueID(object action)
	{
		if(action == null)
			return 0;
		
		if(actionsByID.ContainsValue(action))
			foreach(DictionaryEntry entry in actionsByID)
				if(entry.Value.Equals(action))
					return (int)entry.Key;
		
		int iterations = 0;
		int maxIterations = 2;
		while(actionsByID.ContainsKey(currentID))
		{
			if(currentID + 1 > int.MaxValue || currentID + 1 == int.MinValue)
			{//if the current id is at max int value or automatically wrapped
				iterations++;
				if(iterations >= maxIterations)
				{
					Debug.LogError("YOU HAVE WAY TOO MANY stored Actions! Release some before adding more! " +
						"Your app would've broken down by now anyway");
					return 0;
				}
				currentID = int.MinValue;
			}
			else
			{
				currentID++;
				if(currentID == 0) //0 is reserved for null actions
					currentID++;
			}
		}
		actionsByID.Add(currentID, action);
		return currentID;
	}
	
	int GetIDForAction(object action)
	{
		if(action == null)
			return 0;
		
		if(actionsByID.ContainsValue(action))
			foreach(DictionaryEntry entry in actionsByID)
				if(entry.Value.Equals(action))
					return (int)entry.Key;
		
		return 0;
	}
	
	void RemoveActionOfUniqueID(int id)
	{
		if(id == 0)
			return;
		actionsByID.Remove(id);
	}
	
	object GetActionForUniqueID(int id)
	{
		if(id == 0)
			return null;
		return actionsByID[id];
	}
	
	object[] GetActionsForUniqueID(int[] ids)
	{
		object[] actions = new object[ids.Length];
		for(int i = 0; i < ids.Length; i++)
			actions[i] = GetActionForUniqueID(ids[i]);
		return actions;
	}
	#endregion
	
	#region misc
	[DllImport("__Internal")]
    private static extern int _init(int tag);
	public MHObject init(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int newTag = _init(tag);
			
			return GetObjectByUniqueTag(newTag) as MHObject;
        }
		else
			return new MHObject();
	}
	
	//labels and imageviews are automatically created, so this just syncs the tag
	[DllImport("__Internal")]
    private static extern void _syncLabel(int tag, int button);
	public void syncLabel(int tag, MHButton button)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int buttonTag = button.tag;
			
			_syncLabel(tag, buttonTag);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _syncImageView(int tag, int button);
	public void syncImageView(int tag, MHButton button)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int buttonTag = button.tag;
			
			_syncImageView(tag, buttonTag);
		}
	}
	#endregion
	
	#region Obj-C Messaging
	static void ReceiveInstantMessage(string methodName, string jsonParam)
	{
		Instance.SendMessage(methodName, jsonParam);
	}
	#endregion
}
