using UnityEngine;
using System;
using System.Collections;
using System.IO;

public static class MHTools {
	public static bool OverWriteNextSaveTexture = false;
	
	public static string SaveTexture2DToDeviceForUIImage(Texture2D texture)
	{
		return SaveTexture2DToDeviceForUIImage(texture, "");
	}
	
	public static string SaveTexture2DToDeviceForUIImage(Texture2D texture, string subfolderName)
	{
#if !UNITY_WEBPLAYER
		if(!string.IsNullOrEmpty(subfolderName))
			subfolderName += "/";
		string filePath = Application.persistentDataPath + "/MHiOS/" + subfolderName;
		if(!Directory.Exists(filePath))
			Directory.CreateDirectory(filePath);
		string fullFilePath = filePath + texture.name + ".png";
		
		if(OverWriteNextSaveTexture || !File.Exists(fullFilePath))
		{
			byte[] bytes = texture.EncodeToPNG();
	        File.WriteAllBytes(fullFilePath, bytes);
		}
		OverWriteNextSaveTexture = false;
        return fullFilePath;
#else
		return "";
#endif
	}
	
	public static Texture2D LoadTextureFromDevice(string path)
	{
#if !UNITY_WEBPLAYER		
		// "Empty" texture. Will be replaced by LoadImage
        Texture2D texture = new Texture2D(4, 4);
        FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
        byte[] imageData = new byte[fs.Length];
        fs.Read(imageData, 0, (int)fs.Length);
        texture.LoadImage(imageData);

        return texture;
#else
		return null;
#endif
	}
	
	public static string ConvertTextureToUIImage(Texture2D texture)
	{
		Hashtable imageEncoded = new Hashtable();
		imageEncoded.Add("ObjectType", "Image");
		imageEncoded.Add("Path", SaveTexture2DToDeviceForUIImage(texture));
		
		return imageEncoded.toJson();
	}
	
	public static Texture2D ConvertUIImageToTexture(string jsonString)
	{
		Hashtable imageDecoded = jsonString.hashtableFromJson();
		if(imageDecoded != null)
		{
			if((string)imageDecoded["ObjectType"] == "Image")
				return LoadTextureFromDevice((string)imageDecoded["Path"]);
		}
		return null;
	}
	
	public static string ConvertColorToUIColor(Color color)
	{
		Hashtable colorEncoded = new Hashtable();
		colorEncoded.Add("ObjectType", "Color");
		colorEncoded.Add("R", color.r);
		colorEncoded.Add("G", color.g);
		colorEncoded.Add("B", color.b);
		colorEncoded.Add("A", color.a);
		
		return colorEncoded.toJson();
	}
	
	public static Color ConvertUIColorToColor(string jsonString)
	{
		Hashtable colorDecoded = jsonString.hashtableFromJson();
		if(colorDecoded != null)
		{
			if((string)colorDecoded["ObjectType"] == "Color")
				return new Color((float)((double)colorDecoded["R"]), (float)((double)colorDecoded["G"]), (float)((double)colorDecoded["B"]), (float)((double)colorDecoded["A"]));
		}
		return Color.black;
	}
	
	public static string ConvertVector2ToJSON(Vector2 vector)
	{
		Hashtable vectorEncoded = new Hashtable();
		vectorEncoded.Add("ObjectType", "Vector2");
		vectorEncoded.Add("X", vector.x);
		vectorEncoded.Add("Y", vector.y);
		
		return vectorEncoded.toJson();
	}
	
	public static Vector2 ConvertJSONToVector2(string jsonString)
	{
		Hashtable vectorDecoded = jsonString.hashtableFromJson();
		if(vectorDecoded != null)
		{
			if((string)vectorDecoded["ObjectType"] == "Vector2")
				return new Vector2((float)((double)vectorDecoded["X"]), (float)((double)vectorDecoded["Y"]));
		}
		return Vector2.zero;
	}
	
	public static string ConvertRectToJSON(Rect rect)
	{
		Hashtable rectEncoded = new Hashtable();
		rectEncoded.Add("ObjectType", "Rect");
		rectEncoded.Add("X", rect.x);
		rectEncoded.Add("Y", rect.y);
		rectEncoded.Add("Width", rect.width);
		rectEncoded.Add("Height", rect.height);
		
		return rectEncoded.toJson();
	}
	
	public static Rect ConvertJSONToRect(string jsonString)
	{
		Hashtable rectDecoded = jsonString.hashtableFromJson();
		if(rectDecoded != null)
		{
			if((string)rectDecoded["ObjectType"] == "Rect")
				return new Rect((float)((double)rectDecoded["X"]), (float)((double)rectDecoded["Y"]), (float)((double)rectDecoded["Width"]), (float)((double)rectDecoded["Height"]));
		}
		return RectZero;
	}
	
	public static string ConvertVector4ToJSON(Vector4 vector)
	{
		Hashtable vectorEncoded = new Hashtable();
		vectorEncoded.Add("ObjectType", "Vector4");
		vectorEncoded.Add("X", vector.x);
		vectorEncoded.Add("Y", vector.y);
		vectorEncoded.Add("Z", vector.z);
		vectorEncoded.Add("W", vector.w);
		
		return vectorEncoded.toJson();
	}
	
	public static Vector4 ConvertJSONToVector4(string jsonString)
	{
		Hashtable vectorDecoded = jsonString.hashtableFromJson();
		if(vectorDecoded != null)
		{
			if((string)vectorDecoded["ObjectType"] == "Vector4")
				return new Vector4((float)((double)vectorDecoded["X"]), (float)((double)vectorDecoded["Y"]), (float)((double)vectorDecoded["Z"]), (float)((double)vectorDecoded["W"]));
		}
		return Vector4.zero;
	}
	
	public static string ConvertDateTimeToJSON(DateTime date)
	{
		Hashtable dateEncoded = new Hashtable();
		dateEncoded.Add("ObjectType", "Date");
		dateEncoded.Add("Day", date.Day);
		dateEncoded.Add("Month", date.Month);
		dateEncoded.Add("Year", date.Year);
		dateEncoded.Add("Hour", date.Hour);
		dateEncoded.Add("Minute", date.Minute);
		dateEncoded.Add("Second", date.Second);
		
		return dateEncoded.toJson();
	}
	
	public static DateTime ConvertJSONToDateTime(string jsonString)
	{
		Hashtable dateDecoded = jsonString.hashtableFromJson();
		if(dateDecoded != null)
		{
			if((string)dateDecoded["ObjectType"] == "Date")
				return new DateTime(((int)dateDecoded["Year"]), ((int)dateDecoded["Month"]), ((int)dateDecoded["Day"]), ((int)dateDecoded["Hour"]), ((int)dateDecoded["Minute"]), ((int)dateDecoded["Second"]));
		}
		return DateTime.Today;
	}
	
	public static string ConvertTimeZoneToJSON(TimeZoneInfo timeZone)
	{
		Hashtable zoneEncoded = new Hashtable();
		zoneEncoded.Add("ObjectType", "TimeZone");
		zoneEncoded.Add("Name", timeZone.StandardName);
		
		return zoneEncoded.toJson();
	}
	
	public static TimeZoneInfo ConvertJSONToTimeZone(string jsonString)
	{
		Hashtable zoneDecoded = jsonString.hashtableFromJson();
		if(zoneDecoded != null)
		{
			if((string)zoneDecoded["ObjectType"] == "TimeZone")
			{
				string standardName = (string)zoneDecoded["Name"];
				IEnumerator enumerator = TimeZoneInfo.GetSystemTimeZones().GetEnumerator();
				while (enumerator.MoveNext())
				{
				    System.TimeZoneInfo item = enumerator.Current as System.TimeZoneInfo;
					if(item.StandardName == standardName)
						return item;
				}
			}
		}
		return TimeZoneInfo.Local;
	}
	
	public static int[] ConvertJSONToIntArray(string jsonString)
	{
		ArrayList intArrayDecoded = jsonString.arrayListFromJson();
		if(intArrayDecoded != null)
		{
			int[] resultArray = new int[intArrayDecoded.Count];
			for(int i = 0; i < intArrayDecoded.Count; i++)
				resultArray[i] = Convert.ToInt32(intArrayDecoded[i]);
			return resultArray;
		}
		return null;
	}
	
	public static string[] ConvertJsonToStringArray(string jsonString)
	{
		ArrayList stringArrayDecoded = jsonString.arrayListFromJson();
		if(stringArrayDecoded != null)
		{
			string[] resultArray = new string[stringArrayDecoded.Count];
			for(int i = 0; i < stringArrayDecoded.Count; i++)
				resultArray[i] = Convert.ToString(stringArrayDecoded[i]);
			return resultArray;
		}
		return null;
	}
	
	public static Rect RectZero {
		get {
			return new Rect(0f,0f,0f,0f);
		}
	}
	
	public static int GetObjectTag(MHObject obj)
	{
		if(obj == null)
			return 0;
		return obj.tag;
	}
	
	public static int[] GetObjectTags(MHObject[] objs)
	{
		if(objs == null)
			return null;
		int[] tags = new int[objs.Length];
		
		for (int i = 0; i < objs.Length; i++)
			tags[i] = objs[i].tag;
		
		return tags;
	}
	
	public static void ReturnResultToXCode(string result)
	{
		if(string.IsNullOrEmpty(result))
			PlayerPrefs.SetString("MHResult", "NULL");
		else
			PlayerPrefs.SetString("MHResult", result);
	}
	
	public static bool IsIpad {
		get {
			switch(iPhone.generation)
			{
			case iPhoneGeneration.iPad1Gen:
			case iPhoneGeneration.iPad2Gen:
			case iPhoneGeneration.iPadMini1Gen:
			case iPhoneGeneration.iPad3Gen:
			case iPhoneGeneration.iPad4Gen:
			case iPhoneGeneration.iPadUnknown:
				return true;
			}
			return false;
		}
	}
	
	public static bool IsRetina {
		get {
			switch(iPhone.generation)
			{
			case iPhoneGeneration.iPad3Gen:
			case iPhoneGeneration.iPad4Gen:
			case iPhoneGeneration.iPhone4:
			case iPhoneGeneration.iPhone4S:
			case iPhoneGeneration.iPhone5:
			case iPhoneGeneration.iPodTouch4Gen:
			case iPhoneGeneration.iPodTouch5Gen:
			case iPhoneGeneration.iPadUnknown:	
			case iPhoneGeneration.iPhoneUnknown:
			case iPhoneGeneration.iPodTouchUnknown:
			case iPhoneGeneration.Unknown:
				return true;
			}
			return false;
		}
	}
	
	public static void PauseUnity(bool pause)
	{
		MHiOSManager.Instance.pauseUnity(pause);
	}
}
