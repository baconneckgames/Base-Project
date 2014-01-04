using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DynamicSoundGroupCreator : MonoBehaviour {
	public MasterAudio.DragGroupMode curDragGroupMode = MasterAudio.DragGroupMode.OneGroupPerClip;
	public Texture logoTexture;
	public bool createOnAwake = true;
	public List<DynamicSoundGroupInfo> soundGroupsToCreate;
	public bool soundGroupsAreExpanded = true;
	public bool removeGroupsOnSceneChange = true;
	public SoundGroupVariation.AudioLocation bulkVariationMode = SoundGroupVariation.AudioLocation.Clip;
	
	private bool hasCreated = false;
	private List<Transform> groupsToRemove = new List<Transform>();
	private Transform trans;
	
	public DynamicSoundGroupCreator() {
		soundGroupsToCreate = new List<DynamicSoundGroupInfo>() {
			new DynamicSoundGroupInfo() {
				variations = new List<DynamicSoundGroupVariation>() {
					new DynamicSoundGroupVariation()
				}
			}
		};
	}
	
	void Awake() {
		this.trans = this.transform;
		hasCreated = false;
	}
	
	void Start() {
		if (createOnAwake) {
			CreateGroups();
		}
	}
	
	void OnDisable() {
		if (MasterAudio.AppIsShuttingDown) {
			return;
		}
		
		// scene changing
		if (!removeGroupsOnSceneChange) {
			// nothing to do.
			return;
		}

		// delete any buses we created too
		for (var i = 0; i < soundGroupsToCreate.Count; i++) {
			var aGroup = soundGroupsToCreate[i];
			if (aGroup.busMode != DynamicSoundGroupInfo.BusMode.CreateNew) {
				continue;
			}
			
			MasterAudio.DeleteBusByName(aGroup.busName);
		}		
		
		for (var i = 0; i < groupsToRemove.Count; i++) {
			MasterAudio.RemoveSoundGroup(groupsToRemove[i]);
		}
	}

    /// <summary>
    /// This method will create the Sound Groups, Variations, buses and ducking triggers specified in the Dynamic Sound Group Creator's Inspector. It is called automatically if you check the "auto-create" checkbox, otherwise you will need to call this method manually.
    /// </summary>
	public void CreateGroups() {
		if (hasCreated) {
			Debug.LogWarning("DynamicSoundGroupCreator '" + this.transform.name + "' has already created its groups. Cannot create again.");
			return;
		}
		
		for (var i = 0; i < soundGroupsToCreate.Count; i++) {
			var aGroup = soundGroupsToCreate[i];
			
			var groupTrans = MasterAudio.CreateNewSoundGroup(aGroup, this.trans.name);
			if (groupTrans == null) {
				continue;
			}
			
			groupsToRemove.Add(groupTrans);
		}
		
		hasCreated = true;
	}
}
