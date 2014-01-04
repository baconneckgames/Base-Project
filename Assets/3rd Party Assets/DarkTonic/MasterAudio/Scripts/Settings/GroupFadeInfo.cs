using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class GroupFadeInfo  {
	public string GroupName;
	public float TargetVolume;
	public float VolumeStep;
	public bool IsActive = true;
}
