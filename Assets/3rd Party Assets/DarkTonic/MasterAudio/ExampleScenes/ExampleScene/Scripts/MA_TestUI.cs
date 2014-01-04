using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MA_TestUI : MonoBehaviour {
	void OnGUI() {
		GUI.Label(new Rect(20,40, 450, 160), "Use left/right arrow keys and left mouse button to play. " +
			"Music ducks (gets quieter) for Screams, then ramps back up soon after. Sound FX have " +
			"variations. No code needed to be written for any of the sound triggering or ducking. See ReadMe.pdf for more information on how to set things up." +
			"Note the triple-Jukebox control that handles 3 the 3 Playlist Controllers in the scene! " +
			"It's in the Master Audio prefab's Inspector. Also, take note of the DynamicSoundGroupCreator prefab, which adds a new temporary Sound Group during the current Scene only! " + 
			"\n\nHappy gaming - DarkTonic, Inc.");
	}
}
		
