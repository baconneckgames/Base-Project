using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class GroupBus {
	public string busName;
	public float volume = 1.0f;
	public bool isSoloed = false;
	public bool isMuted = false;
	public int voiceLimit = -1;
}
