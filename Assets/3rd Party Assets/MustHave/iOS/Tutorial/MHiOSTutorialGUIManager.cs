using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public partial class MHiOSTutorialGUIManager : Singleton<MHiOSTutorialGUIManager> {

	public static MHiOSTutorialGUIManager Instance {
		get {
			return ((MHiOSTutorialGUIManager)mInstance);
		} set {
			mInstance = value;
		}
	}
	
	public string description = "Tutorial Section Description";
	public string buttonText = "Click Me!";
	public List<ALBBaseTutorial> tutorials;
	int tutorialIndex = 0;
	
	float mainButtonX = 1f / 4f;
	float mainButtonY = 3f / 18f;
	float mainButtonW = 1f / 2f;
	float mainButtonH = 3f / 9f;
	
	float descButtonX = 1f / 4f;
	float descButtonY = 10f / 18f;
	float descButtonW = 1f / 2f;
	float descButtonH = 2f / 9f;
	
	float nextButtonX = 3f / 8f;
	float nextButtonY = 5f / 6f;
	float nextButtonW = 1f / 4f;
	float nextButtonH = 1f / 18f;
	
	float bounce = 0f;
	float maxBounce = 1f / 18f;
	float nextBounceTime;
	
	int bounceStep = 1;
	
	GUIStyle textStyle;
	
	void Start ()
	{
		tutorials = new List<ALBBaseTutorial>();
		SendMessage("BasicStart", SendMessageOptions.DontRequireReceiver);
		SendMessage("NavStart", SendMessageOptions.DontRequireReceiver);
		SendMessage("ActionStart", SendMessageOptions.DontRequireReceiver);
		SendMessage("BasicStart2", SendMessageOptions.DontRequireReceiver);
		//SendMessage("FullStart", SendMessageOptions.DontRequireReceiver);
		SetTutorialText();
	}
	
	void SetTutorialText()
	{
		if(tutorialIndex < tutorials.Count)
		{
			description = tutorials[tutorialIndex].GetTutorialDescription();
			buttonText = tutorials[tutorialIndex].GetButtonText();
		}
	}	

	void OnGUI ()
	{
		GUI.enabled = true;
		if(textStyle == null)
		{
			textStyle = GUI.skin.label;
			textStyle.wordWrap = true;
		}
		
		textStyle.fontSize = 32;
		textStyle.alignment = TextAnchor.MiddleCenter;
		
		if(GUI.Button(new Rect(Screen.width * mainButtonX, Screen.height * mainButtonY, Screen.width * mainButtonW, Screen.height * mainButtonH), ""))
		{
			if(tutorials.Count > 0 && tutorialIndex < tutorials.Count)
				tutorials[tutorialIndex].StartTutorial();
		}
		
		CalculateBounce();
		GUI.Label(new Rect(Screen.width * mainButtonX, Screen.height * (mainButtonY - bounce), Screen.width * mainButtonW, Screen.height * mainButtonH), buttonText, textStyle);
			
		textStyle.fontSize = 18;
		textStyle.alignment = TextAnchor.UpperCenter;
		
		GUI.Label(new Rect(Screen.width * descButtonX, Screen.height * descButtonY, Screen.width * descButtonW, Screen.height * descButtonH), description, textStyle);
		
		if(tutorialIndex + 1 >= tutorials.Count)
			GUI.enabled = false;
		if(GUI.Button(new Rect(Screen.width * nextButtonX, Screen.height * nextButtonY, Screen.width * nextButtonW, Screen.height * nextButtonH), ""))
		{
			tutorialIndex++;
			SetTutorialText();
		}
		
		textStyle.alignment = TextAnchor.MiddleCenter;
		GUI.Label(new Rect(Screen.width * nextButtonX, Screen.height * nextButtonY, Screen.width * nextButtonW, Screen.height * nextButtonH), "NEXT", textStyle);
	}
	
	void CalculateBounce()
	{
		if(bounceStep == 1)
		{
			bounce = Mathf.MoveTowards(bounce, maxBounce, Time.deltaTime*.25f);
			if(Mathf.Approximately(bounce,maxBounce))
				bounceStep = 2;
		} 
		else if (bounceStep == 2)
		{
			bounce = Mathf.MoveTowards(bounce, 0, Time.deltaTime*.25f);
			if(Mathf.Approximately(bounce, 0))
				bounceStep = 3;
		}
		else if (bounceStep == 3)
		{
			bounce = Mathf.MoveTowards(bounce, maxBounce/2f, Time.deltaTime*.25f);
			if(Mathf.Approximately(bounce, maxBounce/2f))
				bounceStep = 4;
		}
		else if (bounceStep == 4)
		{
			bounce = Mathf.MoveTowards(bounce, 0, Time.deltaTime*.25f);
			if(Mathf.Approximately(bounce, 0))
			{
				bounceStep = 5;
				nextBounceTime = Time.time + 5f;
			}
		}
		else if (bounceStep == 5)
		{
			if(Time.time > nextBounceTime)
				bounceStep = 1;
		}
	}
}
