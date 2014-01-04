using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(MasterAudioGroup))]
public class MasterAudioGroupInspector : Editor {
	private bool isValid = true;
	
	public override void OnInspectorGUI() {
		EditorGUIUtility.LookLikeControls();
		
		EditorGUI.indentLevel = 0;
		var isDirty = false;
		
		MasterAudio ma = MasterAudio.Instance;
		if (ma == null) {
			isValid = false;
		} 
		
		MasterAudioGroup _group = (MasterAudioGroup)target;
		_group = RescanChildren(_group);
		
		if (!isValid) {
			return;
		}
		
		if (_group.logoTexture != null) {
			GUIHelper.ShowHeaderTexture(_group.logoTexture);
		}
		
		var newVol = EditorGUILayout.Slider("Group Master Volume", _group.groupMasterVolume, 0f, 1f);
		if (newVol != _group.groupMasterVolume) {
			UndoHelper.RecordObjectPropertyForUndo(_group, "change Group Master Volume");
			_group.groupMasterVolume = newVol;
		}
		
		var newRetrigger = EditorGUILayout.IntSlider("Retrigger Percentage", _group.retriggerPercentage, 0, 100);
		if (newRetrigger != _group.retriggerPercentage) {
			UndoHelper.RecordObjectPropertyForUndo(_group, "change Retrigger Percentage");
			_group.retriggerPercentage = newRetrigger;
		}
		
		var newVarSequence = (MasterAudioGroup.VariationSequence) EditorGUILayout.EnumPopup("Variation Sequence", _group.curVariationSequence);
		if (newVarSequence != _group.curVariationSequence) {
			UndoHelper.RecordObjectPropertyForUndo(_group, "change Variation Sequence");
			_group.curVariationSequence = newVarSequence;
		}

		if (_group.curVariationSequence == MasterAudioGroup.VariationSequence.TopToBottom) {
			var newUseInactive = EditorGUILayout.BeginToggleGroup("Refill Variation Pool After Inactive Time", _group.useInactivePeriodPoolRefill);
			if (newUseInactive != _group.useInactivePeriodPoolRefill) {
				UndoHelper.RecordObjectPropertyForUndo(_group, "toggle Inactive Refill");
				_group.useInactivePeriodPoolRefill = newUseInactive;
			}

			EditorGUI.indentLevel = 1;
			var newInactivePeriod = EditorGUILayout.Slider("Inactive Time (sec)", _group.inactivePeriodSeconds, .2f, 30f);
			if (newInactivePeriod != _group.inactivePeriodSeconds) {
				UndoHelper.RecordObjectPropertyForUndo(_group, "change Inactive Time");
				_group.inactivePeriodSeconds = newInactivePeriod;
			}

			EditorGUILayout.EndToggleGroup();
		}

		EditorGUI.indentLevel = 0;
		var newVarMode = (MasterAudioGroup.VariationMode) EditorGUILayout.EnumPopup("Variation Mode", _group.curVariationMode);
		if (newVarMode != _group.curVariationMode) {
			UndoHelper.RecordObjectPropertyForUndo(_group, "change Variation Mode");
			_group.curVariationMode = newVarMode;
		}
		
		if (_group.curVariationMode == MasterAudioGroup.VariationMode.Normal) {
			var newLimitPoly = EditorGUILayout.Toggle("Limit Polyphony", _group.limitPolyphony);
			if (newLimitPoly != _group.limitPolyphony) {
				UndoHelper.RecordObjectPropertyForUndo(_group, "toggle Limit Polyphony");
				_group.limitPolyphony = newLimitPoly;
			}
			if (_group.limitPolyphony) {
				int maxVoices = 0;
				for (var i = 0; i < _group.groupVariations.Count; i++) {
					var variation = _group.groupVariations[i];
					maxVoices += variation.weight;
				}
				
				var newVoiceLimit = EditorGUILayout.IntSlider("Polyphony Voice Limit", _group.voiceLimitCount, 1, maxVoices);
				if (newVoiceLimit != _group.voiceLimitCount) {
					UndoHelper.RecordObjectPropertyForUndo(_group, "change Polyphony Voice Limit");
					_group.voiceLimitCount = newVoiceLimit;
				}
			}
			
			var newLimitMode = (MasterAudioGroup.LimitMode) EditorGUILayout.EnumPopup("Replay Limit Mode", _group.limitMode);
			if (newLimitMode != _group.limitMode) {
				UndoHelper.RecordObjectPropertyForUndo(_group, "change Replay Limit Mode");
				_group.limitMode = newLimitMode;
			}
			
			switch (_group.limitMode) {
			case MasterAudioGroup.LimitMode.FrameBased:
				var newFrameLimit = EditorGUILayout.IntSlider("Min Frames Between", _group.limitPerXFrames, 1, 120);
				if (newFrameLimit != _group.limitPerXFrames) {
					UndoHelper.RecordObjectPropertyForUndo(_group, "change Min Frames Between");
					_group.limitPerXFrames = newFrameLimit;
				}
				break;
			case MasterAudioGroup.LimitMode.TimeBased:
				var newMinTime = EditorGUILayout.Slider("Min Seconds Between", _group.minimumTimeBetween, 0.1f, 10f);
				if (newMinTime != _group.minimumTimeBetween) {
					UndoHelper.RecordObjectPropertyForUndo(_group, "change Min Seconds Between");
					_group.minimumTimeBetween = newMinTime;
				}
				break;
			}
		} else {
			GUIHelper.ShowColorWarning("*In this mode, only one Variation can be played at a time.");
			GUIHelper.ShowColorWarning(" They will chain together randomly until you stop the Group.");
		}
		
		var newBulkMode = (SoundGroupVariation.AudioLocation) EditorGUILayout.EnumPopup("Bulk Variation Mode", _group.bulkVariationMode);
		if (newBulkMode != _group.bulkVariationMode) {
			UndoHelper.RecordObjectPropertyForUndo(_group, "change Bulk Variation Mode");
			_group.bulkVariationMode = newBulkMode;
		}
		if (_group.bulkVariationMode == SoundGroupVariation.AudioLocation.ResourceFile) {
			GUIHelper.ShowColorWarning("*Resource mode: make sure to drag from Resource folders only.");
		}
		
		var newLog = EditorGUILayout.Toggle("Log Sounds", _group.logSound);
		if (newLog != _group.logSound) {
			UndoHelper.RecordObjectPropertyForUndo(_group, "toggle Log Sounds");
			_group.logSound = newLog;
		}
		
		int? deadChildIndex = null;
		
		if (!Application.isPlaying) {
			EditorGUILayout.BeginHorizontal();
			GUILayout.Space(4);
			GUILayout.Label("Actions", EditorStyles.wordWrappedLabel, GUILayout.Width(50f));
			GUILayout.Space(96);
			GUI.contentColor = Color.green;
			if (GUILayout.Button(new GUIContent("Equalize Weights", "Reset Weights to zero"), EditorStyles.toolbarButton, GUILayout.Width(120))) {
				isDirty = true;
				EqualizeWeights(_group);
			}	
			
			GUILayout.Space(10);
			if (GUILayout.Button(new GUIContent("Equalize Variation Volumes"), EditorStyles.toolbarButton, GUILayout.Width(150))) {
				EqualizeVariationVolumes(_group.groupVariations);
			}
			
			GUI.contentColor = Color.white;
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.Separator();
		}
		
		if (!Application.isPlaying) {
			// new variation settings
			EditorGUILayout.BeginVertical();
			var anEvent = Event.current;
			
			GUI.color = Color.yellow;
			
			var dragArea = GUILayoutUtility.GetRect(0f,35f,GUILayout.ExpandWidth(true));
			GUI.Box (dragArea, "Drag Audio clips here to create Variations!");
			
			GUI.color = Color.white;
			
			switch (anEvent.type) {
			case EventType.DragUpdated:
			case EventType.DragPerform:
				if(!dragArea.Contains(anEvent.mousePosition)) {
					break;
				}
				
				DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
				
				if(anEvent.type == EventType.DragPerform) {
					DragAndDrop.AcceptDrag();
					
					foreach (var dragged in DragAndDrop.objectReferences) {
						var aClip = dragged as AudioClip;
						if(aClip == null) {
							continue;
						}
						
						CreateVariation(_group, ma, aClip);
					}
				}
				Event.current.Use();
				break;
			}
			EditorGUILayout.EndVertical();
			// end new variation settings
		}
		
		if (_group.groupVariations.Count == 0) {
			GUIHelper.ShowColorWarning("You currently have no Variations.");
		} else {
			_group.groupVariations.Sort(delegate(SoundGroupVariation x, SoundGroupVariation y) {
				return x.name.CompareTo(y.name);	
			});
			
			for (var i = 0; i < _group.groupVariations.Count; i++) {
				var variation = _group.groupVariations[i];
				EditorGUILayout.BeginHorizontal(EditorStyles.objectFieldThumb);
				EditorGUILayout.LabelField(variation.name, EditorStyles.boldLabel);
				
				GUILayout.FlexibleSpace();

				if (GUILayout.Button(new GUIContent(_group.settingsTexture, "Click to goto Variation"), EditorStyles.toolbarButton, GUILayout.Width(40))) {
					Selection.activeObject = variation; 
				}
				
				if (!Application.isPlaying) {
					if (GUILayout.Button(new GUIContent(_group.deleteTexture, "Click to delete this Variation"), EditorStyles.toolbarButton, GUILayout.Width(40))) {
						deadChildIndex = i;
						isDirty = true;
					}
				}
				
				var buttonPressed = GUIHelper.AddVariationButtons(ma);
				switch (buttonPressed) {
				case GUIHelper.DTFunctionButtons.Play:
					if (Application.isPlaying) {
						MasterAudio.PlaySound(_group.name, 1f, null, 0f, variation.name);
					} else {
						if (variation.audLocation == SoundGroupVariation.AudioLocation.ResourceFile) {
							MasterAudio.PreviewerInstance.Stop();
							MasterAudio.PreviewerInstance.PlayOneShot(Resources.Load(variation.resourceFileName) as AudioClip);
						} else {
							variation.audio.Stop();
							variation.audio.Play();
						}
					}
					break;
				case GUIHelper.DTFunctionButtons.Stop:
					if (variation.audLocation == SoundGroupVariation.AudioLocation.ResourceFile) {
						MasterAudio.PreviewerInstance.Stop();
					} else {
						variation.audio.Stop();
					}
					break;
				}
				
				EditorGUILayout.EndHorizontal();
				
				if (!Application.isPlaying) {
					GUIHelper.ShowColorWarning("*Fading & random settings are ignored by preview in edit mode.");
				}
				if (variation.audio == null) {
					GUIHelper.ShowColorWarning(string.Format("The Variation: '{0}' has no Audio Source.", variation.name));
					break;
				}
				
				var oldLocation = variation.audLocation;
				var newLocation = (SoundGroupVariation.AudioLocation) EditorGUILayout.EnumPopup("Audio Origin", variation.audLocation);
				if (newLocation != variation.audLocation) {
					UndoHelper.RecordObjectPropertyForUndo(variation, "change Audio Origin");
					variation.audLocation = newLocation;
				}
				
				switch (variation.audLocation) {
				case SoundGroupVariation.AudioLocation.Clip:
					var newClip = (AudioClip) EditorGUILayout.ObjectField("Audio Clip", variation.audio.clip, typeof(AudioClip), false);
					if (newClip != variation.audio.clip) {
						UndoHelper.RecordObjectPropertyForUndo(variation.audio, "change Audio Clip");
						variation.audio.clip = newClip;
					}
					break;
				case SoundGroupVariation.AudioLocation.ResourceFile:
					if (oldLocation != variation.audLocation) {
						if (variation.audio.clip != null) {
							Debug.Log("Audio clip removed to prevent unnecessary memory usage on Resource file Variation.");
						}
						variation.audio.clip = null;
					}
					
					EditorGUILayout.BeginVertical();
					var anEvent = Event.current;
					
					GUI.color = Color.yellow;
					var dragArea = GUILayoutUtility.GetRect(0f, 20f,GUILayout.ExpandWidth(true));
					GUI.Box (dragArea, "Drag Resource Audio clip here to use its name!");
					GUI.color = Color.white;
					
					switch (anEvent.type) {
					case EventType.DragUpdated:
					case EventType.DragPerform:
						if(!dragArea.Contains(anEvent.mousePosition)) {
							break;
						}
						
						DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
						
						if(anEvent.type == EventType.DragPerform) {
							DragAndDrop.AcceptDrag();
							
							foreach (var dragged in DragAndDrop.objectReferences) {
								var aClip = dragged as AudioClip;
								if(aClip == null) {
									continue;
								}
								
								UndoHelper.RecordObjectPropertyForUndo(variation, "change Resource Filename");
								variation.resourceFileName = aClip.name;
							}
						}
						Event.current.Use();
						break;
					}
					EditorGUILayout.EndVertical();
					
					var newFilename = EditorGUILayout.TextField("Resource Filename", variation.resourceFileName);		
					if (newFilename != variation.resourceFileName) {
						UndoHelper.RecordObjectPropertyForUndo(variation, "change Resource Filename");
						variation.resourceFileName = newFilename;
					}
					break;
				}
				
				var newVolume = EditorGUILayout.Slider("Volume", variation.audio.volume, 0f, 1f);
				if (newVolume != variation.audio.volume) {
					UndoHelper.RecordObjectPropertyForUndo(variation.audio, "change Volume");
					variation.audio.volume = newVolume;
				}
				
				var newPitch = EditorGUILayout.Slider("Pitch", variation.audio.pitch, -3f, 3f);
				if (newPitch != variation.audio.pitch) {
					UndoHelper.RecordObjectPropertyForUndo(variation.audio, "change Pitch");
					variation.audio.pitch = newPitch;
				}
				
				var newLoop = EditorGUILayout.Toggle("Loop Clip", variation.audio.loop);
				if (newLoop != variation.audio.loop) {
					UndoHelper.RecordObjectPropertyForUndo(variation.audio, "toggle Loop Clip");
					variation.audio.loop = newLoop;
				}
				
				EditorUtility.SetDirty(variation.audio);
				
				var newRandomPitch = EditorGUILayout.Slider("Random Pitch", variation.randomPitch, 0f, 3f);
				if (newRandomPitch != variation.randomPitch) {
					UndoHelper.RecordObjectPropertyForUndo(variation, "change Random Pitch");
					variation.randomPitch = newRandomPitch;
				}
				
				var newRandomVol = EditorGUILayout.Slider("Random Volume", variation.randomVolume, 0f, 1f);
				if (newRandomVol != variation.randomVolume) {
					UndoHelper.RecordObjectPropertyForUndo(variation, "change Random Volume");
					variation.randomVolume = newRandomVol;
				}
				
				var newWeight = EditorGUILayout.IntSlider("Weight (Instances)", variation.weight, 0, 100);
				if (newWeight != variation.weight) {
					UndoHelper.RecordObjectPropertyForUndo(variation, "change Weight");
					variation.weight = newWeight;
				}
				
				if (variation.HasActiveFXFilter) {
					var newFxTailTime = EditorGUILayout.Slider("FX Tail Time", variation.fxTailTime, 0f, 10f);
					if (newFxTailTime != variation.fxTailTime) {
						UndoHelper.RecordObjectPropertyForUndo(variation, "change FX Tail Time");
						variation.fxTailTime = newFxTailTime;
					}
				}
				
				var newFades = EditorGUILayout.BeginToggleGroup("Use Custom Fading", variation.useFades);
				if (newFades != variation.useFades) {
					UndoHelper.RecordObjectPropertyForUndo(variation, "toggle Use Custom Fading");
					variation.useFades = newFades;
				}
				
				var newFadeIn = EditorGUILayout.Slider("Fade In Time (sec)", variation.fadeInTime, 0f, 10f);
				if (newFadeIn != variation.fadeInTime) {
					UndoHelper.RecordObjectPropertyForUndo(variation, "change Fade In Time");
					variation.fadeInTime = newFadeIn;
				}
				
				var newFadeOut = EditorGUILayout.Slider("Fade Out time (sec)", variation.fadeOutTime, 0f, 10f);
				if (newFadeOut != variation.fadeOutTime) {
					UndoHelper.RecordObjectPropertyForUndo(variation, "change Fade Out Time");
					variation.fadeOutTime = newFadeOut;
				}
				EditorGUILayout.EndToggleGroup();
				
				EditorGUILayout.Separator();
			}
		}
		
		if (deadChildIndex.HasValue) {
			var deadVar = _group.groupVariations[deadChildIndex.Value];
			
			if (deadVar != null) {
				// delete variation from Hierarchy
				GameObject.DestroyImmediate(deadVar.gameObject);
			}
			
			// delete group.
			_group.groupVariations.RemoveAt(deadChildIndex.Value);
		}
		
		if (GUI.changed || isDirty) {
			EditorUtility.SetDirty(target);
		}
		
		GUIHelper.RepaintIfUndoOrRedo(this);
		
		//DrawDefaultInspector();
	}
	
	private MasterAudioGroup RescanChildren(MasterAudioGroup group) {
		var newChildren = new List<SoundGroupVariation>();
		
		var childNames = new List<string>();
		
		SoundGroupVariation variation = null;
		
		for (var i = 0; i < group.transform.childCount; i++) {
			var child = group.transform.GetChild(i);
			
			if (!Application.isPlaying) {
				if (childNames.Contains(child.name)) {
					GUIHelper.ShowColorWarning("You have more than one Variation named: " + child.name + ".");
					GUIHelper.ShowColorWarning("Please ensure each Variation of this Group has a unique name.");
					isValid = false;
					return null;
				}
			}
			
			childNames.Add(child.name);
			
			variation = child.GetComponent<SoundGroupVariation>();
			
			newChildren.Add(variation);
		}
		
		group.groupVariations = newChildren;
		return group;
	}
	
	public void EqualizeWeights(MasterAudioGroup _group) {
		var variations = new SoundGroupVariation[_group.groupVariations.Count];
		
		SoundGroupVariation variation = null;
		for (var i = 0; i < _group.groupVariations.Count; i++) {
			variation = _group.groupVariations[i];
			variations[i] = variation;
		}
		
		UndoHelper.RecordObjectsForUndo(variations, "Equalize Weights");
		
		foreach (var vari in variations) {
			vari.weight = 1;
		}
	}
	
	public void CreateVariation(MasterAudioGroup group, MasterAudio ma, AudioClip clip) {
		var resourceFileName = string.Empty;
		if (group.bulkVariationMode == SoundGroupVariation.AudioLocation.ResourceFile) {
			resourceFileName = GUIHelper.GetResourcePath(clip);
			if (string.IsNullOrEmpty(resourceFileName)) {
				resourceFileName = clip.name;
			}
		}
		
		var clipName = clip.name;
		
		if (group.transform.FindChild(clipName) != null) {
			GUIHelper.ShowAlert("You already have a Variation for this Group named '" + clipName + "'. \n\nPlease rename these Variations when finished to be unique, or you may not be able to play them by name if you have a need to.");
		}
		
		var newVar = (GameObject) GameObject.Instantiate(ma.soundGroupVariationTemplate.gameObject, group.transform.position, Quaternion.identity);
		UndoHelper.CreateObjectForUndo(newVar, "create Variation");
		
		newVar.transform.name = clipName;
		newVar.transform.parent = group.transform;
		var variation = newVar.GetComponent<SoundGroupVariation>();
		
		if (group.bulkVariationMode == SoundGroupVariation.AudioLocation.ResourceFile) {
			variation.audLocation = SoundGroupVariation.AudioLocation.ResourceFile;
			variation.resourceFileName = resourceFileName;
		} else {
			newVar.audio.clip = clip;
		}
	}
	
	private void EqualizeVariationVolumes(List<SoundGroupVariation> variations) {
		var clips = new Dictionary<SoundGroupVariation, float>();
		
		if (variations.Count < 2) {
			GUIHelper.ShowAlert("You must have at least 2 Variations to use this function.");
			return;
		}
		
		float lowestVolume = 1f;
		
		for (var i = 0; i < variations.Count; i++) {
			var setting = variations[i];
			
			AudioClip ac = null;
			
			switch (setting.audLocation) {
			case SoundGroupVariation.AudioLocation.Clip:
				if (setting.audio.clip == null) {
					continue;
				}
				ac = setting.audio.clip;
				break;
			case SoundGroupVariation.AudioLocation.ResourceFile:
				if (string.IsNullOrEmpty(setting.resourceFileName)) {
					continue;
				}
				
				ac = Resources.Load(setting.resourceFileName) as AudioClip;	
				
				if (ac == null) {
					continue; // bad resource path
				}
				break;
			}
			
			float average = 0f;
			var buffer = new float[ac.samples];
			
			Debug.Log("Measuring amplitude of '" + ac.name + "'.");
			
			try {
				ac.GetData(buffer, 0);
			}
			catch {    
				Debug.Log("Could not read data from compressed sample. Skipping '" + setting.name + "'.");
				continue;
			}
			
			for(int c = 0; c < ac.samples; c++){
				average += Mathf.Pow(buffer[c], 2);
			}
			
			average = Mathf.Sqrt(1f/(float)ac.samples*average);
			
			if (average < lowestVolume) {
				lowestVolume = average;
			}
			
			clips.Add (setting, average);
		}
		
		if (clips.Count < 2) {
			GUIHelper.ShowAlert("You must have at least 2 Variations to use this function.");
			return;
		}
		
		foreach (var kv in clips) { 
			float adjustedVol = lowestVolume / kv.Value;
			//set your volume for each Variation in your Sound Group.
			kv.Key.audio.volume = adjustedVol;
		}
	}
}
