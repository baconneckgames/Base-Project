using UnityEngine;
using System.Collections;

public class ALBBaseTutorial {
	
	public bool tutorialStarted;

	public ALBBaseTutorial()
	{
		tutorialStarted = false;
	}
	
	public virtual string GetButtonText()
	{
		return "Click Me!";
	}
	
	public virtual string GetTutorialDescription()
	{
		return "Tutorial Section Description";
	}
	
	public virtual void StartTutorial()
	{
		if(tutorialStarted)
			return;
		tutorialStarted = true;
	}
	
	public virtual void EndTutorial()
	{
		tutorialStarted = false;
	}
}
