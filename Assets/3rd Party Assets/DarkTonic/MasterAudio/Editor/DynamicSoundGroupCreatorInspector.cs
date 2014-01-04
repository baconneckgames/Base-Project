using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(DynamicSoundGroupCreator))]
public class DynamicSoundGroupCreatorInspector : Editor {
	DynamicSoundGroupCreator _creator;

	public override void OnInspectorGUI() {
        EditorGUIUtility.LookLikeControls();
		
		EditorGUI.indentLevel = 1;
		var isDirty = false;
		
		_creator = (DynamicSoundGroupCreator)target;
		
		if (_creator.logoTexture != null) {
			GUIHelper.ShowHeaderTexture(_creator.logoTexture);
		}
	
        EditorGUI.indentLevel = 0;  // Space will handle this for the header

		var newAwake = EditorGUILayout.Toggle("Auto-create Groups", _creator.createOnAwake);
		if (newAwake != _creator.createOnAwake) {
			UndoHelper.RecordObjectPropertyForUndo(_creator, "toggle Auto-create Groups");
			_creator.createOnAwake = newAwake;
		}
		if (_creator.createOnAwake) {
			GUIHelper.ShowColorWarning("*Groups will be created as soon as this object is in the Scene.");
		} else {
			GUIHelper.ShowColorWarning("*You will need to call this object's CreateGroups method.");
		}

		var newRemove = EditorGUILayout.Toggle("Auto-remove Groups", _creator.removeGroupsOnSceneChange);
		if (newRemove != _creator.removeGroupsOnSceneChange) {
			UndoHelper.RecordObjectPropertyForUndo(_creator, "toggle Auto-remove Groups");
			_creator.removeGroupsOnSceneChange = newRemove;
		}

		if (_creator.removeGroupsOnSceneChange) {
			GUIHelper.ShowColorWarning("*Groups will be deleted when the Scene changes.");
		} else {
			GUIHelper.ShowColorWarning("*Groups will persist across Scenes if MasterAudio does.");
		}
		
		EditorGUILayout.Separator();

		var newDragMode = (MasterAudio.DragGroupMode) EditorGUILayout.EnumPopup("Bulk Creation Mode", _creator.curDragGroupMode);
		if (newDragMode != _creator.curDragGroupMode) {
			UndoHelper.RecordObjectPropertyForUndo(_creator, "change Bulk Creation Mode");
			_creator.curDragGroupMode = newDragMode;
		}

		var bulkMode = (SoundGroupVariation.AudioLocation) EditorGUILayout.EnumPopup("Variation Mode", _creator.bulkVariationMode);
		if (bulkMode != _creator.bulkVariationMode) {
			UndoHelper.RecordObjectPropertyForUndo(_creator, "change Variation Mode");
			_creator.bulkVariationMode = bulkMode;
		}
		
		// create groups start
		EditorGUILayout.BeginVertical();
		var aEvent = Event.current;

		GUI.color = Color.yellow;
		
		var dragAreaGroup = GUILayoutUtility.GetRect(0f,35f,GUILayout.ExpandWidth(true));
		GUI.Box (dragAreaGroup, "Drag Audio clips here to create groups!");

		GUI.color = Color.white;
		
		switch (aEvent.type) {
			case EventType.DragUpdated:
			case EventType.DragPerform:
				if(!dragAreaGroup.Contains(aEvent.mousePosition)) {
					break;
				}
				
				DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
				
				if(aEvent.type == EventType.DragPerform) {
					DragAndDrop.AcceptDrag();
					
					DynamicSoundGroupInfo groupInfo = null;
				
					var clips = new List<AudioClip>();
				
					foreach (var dragged in DragAndDrop.objectReferences) {
						var aClip = dragged as AudioClip;
						if(aClip == null) {
							continue;
						}
						
						clips.Add(aClip);
					}
				
					clips.Sort(delegate(AudioClip x, AudioClip y) {
						return x.name.CompareTo(y.name);
					});
				
					for (var i = 0; i < clips.Count; i++) {
						var aClip = clips[i];
						if (_creator.curDragGroupMode == MasterAudio.DragGroupMode.OneGroupPerClip) {
							CreateGroup(aClip);
						} else {
							if (groupInfo == null) { // one group with variations
								groupInfo = CreateGroup(aClip);
							} else {
								CreateVariation(groupInfo, aClip);
							}
						}
					
						isDirty = true;
					}
				}
				Event.current.Use();
				break;
		}
		EditorGUILayout.EndVertical();
		// create groups end
		
		EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
		var newGroupEx = GUIHelper.Foldout(_creator.soundGroupsAreExpanded, "Dynamic Sound Groups");
		if (newGroupEx != _creator.soundGroupsAreExpanded) {
			UndoHelper.RecordObjectPropertyForUndo(_creator, "toggle Dynamic Sound Groups");
			_creator.soundGroupsAreExpanded = newGroupEx;
		}

		EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(100));
		GUIContent content;
        var collapseIcon = '\u2261'.ToString();
        content = new GUIContent(collapseIcon, "Click to collapse all");
        var masterCollapse = GUILayout.Button(content, EditorStyles.toolbarButton);

        var expandIcon = '\u25A1'.ToString();
        content = new GUIContent(expandIcon, "Click to expand all");
        var masterExpand = GUILayout.Button(content, EditorStyles.toolbarButton);
		if (masterExpand) {
			ExpandCollapseAllSoundGroups(true);
		} 
		if (masterCollapse) {
			ExpandCollapseAllSoundGroups(false);
		}
        EditorGUILayout.EndHorizontal();
		EditorGUILayout.EndHorizontal();
		
		DynamicSoundGroupVariation aVar;

		if (_creator.soundGroupsAreExpanded) {
			int? addGroupIndex = null;
			int? removeGroupIndex = null;
			int? addVariationIndex = null;
			int? removeVariationIndex = null;
			int? variationIndextoShiftUp = null;
			int? variationIndextoShiftDown = null;
			
			for (var i = 0; i < _creator.soundGroupsToCreate.Count; i++) {
				EditorGUILayout.Separator();
				var aGroup = _creator.soundGroupsToCreate[i];
				
		        EditorGUI.indentLevel = 1;  // Space will handle this for the header
				EditorGUILayout.BeginHorizontal(EditorStyles.objectFieldThumb);
		        
				var newGroupExpanded = GUIHelper.Foldout(aGroup.isExpanded, "GROUP: " + aGroup.groupName);
				if (newGroupExpanded != aGroup.isExpanded) {
					UndoHelper.RecordObjectPropertyForUndo(_creator, "toggle Group");
					aGroup.isExpanded = newGroupExpanded;
				}
			
				EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(100));

				var groupButtonPressed = GUIHelper.AddFoldOutListItemButtons(i, _creator.soundGroupsToCreate.Count, "Sound Group", false, false);
				EditorGUILayout.EndHorizontal();
				EditorGUILayout.EndHorizontal();
				
				if (aGroup.isExpanded) {
			        EditorGUI.indentLevel = 0;  // Space will handle this for the header
					var newGroupSettingsEx = EditorGUILayout.Toggle("Show Group Settings", aGroup.groupSettingsExpanded);
					if (newGroupSettingsEx != aGroup.groupSettingsExpanded) {
						UndoHelper.RecordObjectPropertyForUndo(_creator, "toggle Show Group Settings");
						aGroup.groupSettingsExpanded = newGroupSettingsEx;
					}
					if (aGroup.groupSettingsExpanded) {
						var newGroupName = EditorGUILayout.TextField("Group Name", aGroup.groupName);
						if (newGroupName != aGroup.groupName) {
							UndoHelper.RecordObjectPropertyForUndo(_creator, "change Group Name");
							aGroup.groupName = newGroupName;
						}

						var newGroupVol = EditorGUILayout.Slider("Group Master Volume", aGroup.groupMasterVolume, 0f, 1f);
						if (newGroupVol != aGroup.groupMasterVolume) {
							UndoHelper.RecordObjectPropertyForUndo(_creator, "change Group Master Volume");
							aGroup.groupMasterVolume = newGroupVol;
						}

						var newTrigger = EditorGUILayout.IntSlider("Never Interrupt Clips", aGroup.retriggerPercentage, 0, 100);
						if (newTrigger != aGroup.retriggerPercentage) {
							UndoHelper.RecordObjectPropertyForUndo(_creator, "change Never Interrupt Clips");
							aGroup.retriggerPercentage = newTrigger;
						}

						var newDuck = EditorGUILayout.Toggle("Duck Music?", aGroup.duckSound);
						if (newDuck != aGroup.duckSound) {
							UndoHelper.RecordObjectPropertyForUndo(_creator, "toggle Duck Music");
							aGroup.duckSound = newDuck;
						}
	
						var newLimitPoly = EditorGUILayout.Toggle("Limit Polyphony", aGroup.limitPolyphony);
						if (newLimitPoly != aGroup.limitPolyphony) {
							UndoHelper.RecordObjectPropertyForUndo(_creator, "toggle Limit Polyphany");
							aGroup.limitPolyphony = newLimitPoly;
						}
						if (aGroup.limitPolyphony) {
							int maxVoices = 0;
							for (var j = 0; j < aGroup.variations.Count; j++) {
								var variation = aGroup.variations[j];
								maxVoices += variation.weight;
							}

							var newVoiceLimit = EditorGUILayout.IntSlider("Polyphony Voice Limit", aGroup.voiceLimitCount, 1, maxVoices);
							if (newVoiceLimit != aGroup.voiceLimitCount) {
								UndoHelper.RecordObjectPropertyForUndo(_creator, "change Polyphony Voice Limit");
								aGroup.voiceLimitCount = newVoiceLimit;
							}
						}

						var newLimitMode = (MasterAudioGroup.LimitMode) EditorGUILayout.EnumPopup("Replay Limit Mode", aGroup.limitMode);
						if (newLimitMode != aGroup.limitMode) {
							UndoHelper.RecordObjectPropertyForUndo(_creator, "change Replay Limit Mode");
							aGroup.limitMode = newLimitMode;
						}
						switch (aGroup.limitMode) {
							case MasterAudioGroup.LimitMode.FrameBased:
								var newFrameLimit = EditorGUILayout.IntSlider("Min Frames Between", aGroup.limitPerXFrames, 1, 120);
								if (newFrameLimit != aGroup.limitPerXFrames) {
									UndoHelper.RecordObjectPropertyForUndo(_creator, "change Min Frames Between");
									aGroup.limitPerXFrames = newFrameLimit;
								}
								break;
							case MasterAudioGroup.LimitMode.TimeBased:
								var newTimeLimit = EditorGUILayout.Slider("Min Seconds Between", aGroup.minimumTimeBetween, 0.1f, 10f);
								if (newTimeLimit != aGroup.minimumTimeBetween) {
									UndoHelper.RecordObjectPropertyForUndo(_creator, "change Min Seconds Between");
									aGroup.minimumTimeBetween = newTimeLimit;
								}
								break;
						}

						var newBusMode = (DynamicSoundGroupInfo.BusMode) EditorGUILayout.EnumPopup("Bus Mode", aGroup.busMode);
						if (newBusMode != aGroup.busMode) {
							UndoHelper.RecordObjectPropertyForUndo(_creator, "change Bus Mode");
							aGroup.busMode = newBusMode;
						}
						if (aGroup.busMode != DynamicSoundGroupInfo.BusMode.NoBus) {
							var newBusName = EditorGUILayout.TextField("Bus Name", aGroup.busName);
							if (newBusName != aGroup.busName) {
								UndoHelper.RecordObjectPropertyForUndo(_creator, "change Bus Name");
								aGroup.busName = newBusName;
							}
							GUIHelper.ShowColorWarning("*Bus will be created if it does not exist.");
						}
						 
						EditorGUILayout.BeginHorizontal();
						GUILayout.Label("Actions", EditorStyles.wordWrappedLabel, GUILayout.Width(50f));
						GUILayout.Space(87);
						GUI.contentColor = Color.green;
						if (GUILayout.Button(new GUIContent("Equalize Weights", "Reset Weights to zero"), EditorStyles.toolbarButton, GUILayout.Width(120))) {
							isDirty = true;
							EqualizeWeights(aGroup);
						}	
						
						GUILayout.Space(8);
						GUI.contentColor = Color.green;
						if (GUILayout.Button(new GUIContent("Alpha Sort Variations", "Sort Variations by name"), EditorStyles.toolbarButton, GUILayout.Width(120))) {
							isDirty = true;
							AlphaSortVariations(aGroup);
						}	
						
						GUI.contentColor = Color.white;
						EditorGUILayout.EndHorizontal();
						EditorGUILayout.Separator();
					}
					
					// create variations start
					EditorGUILayout.BeginVertical();
					var anEvent = Event.current;
		
					GUI.color = Color.yellow;
					
					var dragArea = GUILayoutUtility.GetRect(0f,35f,GUILayout.ExpandWidth(true));
					GUI.Box (dragArea, "Drag Audio clips here to create variations!");
		
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

								var clips = new List<AudioClip>();	
							
								foreach (var dragged in DragAndDrop.objectReferences) {
									var aClip = dragged as AudioClip;
									if(aClip == null) {
										continue;
									}
								
									clips.Add(aClip);									
								}
							
								clips.Sort(delegate(AudioClip x, AudioClip y) {
									return x.name.CompareTo(y.name);
								});
							
								for (var j = 0; j < clips.Count; j++) {
									var aClip = clips[j];
			
									CreateVariation(aGroup, aClip);
									isDirty = true;
											
								}
							}
							Event.current.Use();
							break;
					}
					EditorGUILayout.EndVertical();
					// create variations end
					
					if (aGroup.variations.Count == 0) {
						GUIHelper.ShowColorWarning("*This Group has zero variations.");
					} else {
						for (var v = 0; v < aGroup.variations.Count; v++) {
					        EditorGUI.indentLevel = 2;  // Space will handle this for the header
							EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
					        
							aVar = aGroup.variations[v];
							var newExpanded = GUIHelper.Foldout(aVar.isExpanded, aVar.clipName);
							if (newExpanded != aVar.isExpanded) {
								UndoHelper.RecordObjectPropertyForUndo(_creator, "toggle Variation");
								aVar.isExpanded = newExpanded;
							}
						
							EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(100));
			
							var variationButtonPressed = GUIHelper.AddFoldOutListItemButtons(v, aGroup.variations.Count, "variation", false, true, true);
							EditorGUILayout.EndHorizontal();
							EditorGUILayout.EndHorizontal();
							if (aVar.isExpanded) {
						        EditorGUI.indentLevel = 0;  // Space will handle this for the header
								
								var oldLocation = aVar.audLocation;
								var newLocation = (SoundGroupVariation.AudioLocation) EditorGUILayout.EnumPopup("Audio Origin", aVar.audLocation);
								if (newLocation != aVar.audLocation) {
									UndoHelper.RecordObjectPropertyForUndo(_creator, "change Audio Origin");
									aVar.audLocation = newLocation;
								}
								
								switch (aVar.audLocation) {
									case SoundGroupVariation.AudioLocation.Clip:
										var newClip = (AudioClip) EditorGUILayout.ObjectField("Audio Clip", aVar.clip, typeof(AudioClip), false);
										if (newClip != aVar.clip) {
											UndoHelper.RecordObjectPropertyForUndo(_creator, "change Audio Clip");
											aVar.clip = newClip;
										}
										break;
									case SoundGroupVariation.AudioLocation.ResourceFile:
										if (oldLocation != aVar.audLocation) {
											if (aVar.clip != null) {
												Debug.Log("Audio clip removed to prevent unnecessary memory usage on Resource file variation.");
											}
											aVar.clip = null;
										}

										EditorGUILayout.BeginVertical();
										var myEvent = Event.current;
									
										GUI.color = Color.yellow;
										var myDragArea = GUILayoutUtility.GetRect(0f, 20f,GUILayout.ExpandWidth(true));
										GUI.Box (myDragArea, "Drag Resource Audio clip here to use its name!");
										GUI.color = Color.white;
										
										switch (myEvent.type) {
											case EventType.DragUpdated:
											case EventType.DragPerform:
												if(!myDragArea.Contains(myEvent.mousePosition)) {
													break;
												}
												
												DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
												
												if(myEvent.type == EventType.DragPerform) {
													DragAndDrop.AcceptDrag();
													
													foreach (var dragged in DragAndDrop.objectReferences) {
														var aClip = dragged as AudioClip;
														if(aClip == null) {
															continue;
														}
														
														UndoHelper.RecordObjectPropertyForUndo(_creator, "change Resource Filename");
														aVar.resourceFileName = GUIHelper.GetResourcePath(aClip);
													}
												}
												Event.current.Use();
												break;
										}
										EditorGUILayout.EndVertical();
									
										aVar.resourceFileName = EditorGUILayout.TextField("Resource Filename", aVar.resourceFileName);
										break;
								}

								var newVol = EditorGUILayout.Slider("Volume", aVar.volume, 0f, 1f);
								if (newVol != aVar.volume) {
									UndoHelper.RecordObjectPropertyForUndo(_creator, "change Volume");
									aVar.volume = newVol;
								}

								var newPitch = EditorGUILayout.Slider("Pitch", aVar.pitch, -3f, 3f);
								if (newPitch != aVar.pitch) {
									UndoHelper.RecordObjectPropertyForUndo(_creator, "change Pitch");
									aVar.pitch = newPitch;
								}

								var newClipName = EditorGUILayout.TextField("Variation Name", aVar.clipName);
								if (newClipName != aVar.clipName) {
									UndoHelper.RecordObjectPropertyForUndo(_creator, "change Variation Name");
									aVar.clipName = newClipName;
								}

								var newLoop = EditorGUILayout.Toggle("Loop Clip", aVar.loopClip);
								if (newLoop != aVar.loopClip) {
									UndoHelper.RecordObjectPropertyForUndo(_creator, "toggle Loop Clip");
									aVar.loopClip = newLoop;
								}

								var newRandPitch = EditorGUILayout.Slider("Random Pitch", aVar.randomPitch, 0f, 3f);
								if (newRandPitch != aVar.randomPitch) {
									UndoHelper.RecordObjectPropertyForUndo(_creator, "change Random Pitch");
									aVar.randomPitch = newRandPitch;
								}

								var newRandVol = EditorGUILayout.Slider("Random Volume", aVar.randomVolume, 0f, 1f);
								if (newRandVol != aVar.randomVolume) {
									UndoHelper.RecordObjectPropertyForUndo(_creator, "change Random Volume");
									aVar.randomVolume = newRandVol;
								}

								var newWeight = EditorGUILayout.IntSlider("Weight (Instances)", aVar.weight, 0, 100);
								if (newWeight != aVar.weight) {
									UndoHelper.RecordObjectPropertyForUndo(_creator, "change Weight");
									aVar.weight = newWeight;
								}

								var newUseFade = EditorGUILayout.Toggle("Use Custom Fading?", aVar.useFades);
								if (newUseFade != aVar.useFades) {
									UndoHelper.RecordObjectPropertyForUndo(_creator, "toggle Use Custom Fading");
									aVar.useFades = newUseFade;
								}
								if (aVar.useFades) {
							        EditorGUI.indentLevel = 1;  // Space will handle this for the header
									var newFadeIn = EditorGUILayout.Slider("Fade In Time (sec)", aVar.fadeInTime, 0f, 10f);
									if (newFadeIn != aVar.fadeInTime) {
										UndoHelper.RecordObjectPropertyForUndo(_creator, "change Fade In Time");
										aVar.fadeInTime = newFadeIn;
									}

									var newFadeOut = EditorGUILayout.Slider("Fade Out Time (sec)", aVar.fadeOutTime, 0f, 10f);
									if (newFadeOut != aVar.fadeOutTime) {
										UndoHelper.RecordObjectPropertyForUndo(_creator, "change Fade Out Time");
										aVar.fadeOutTime = newFadeOut;
									}
								}

								EditorGUI.indentLevel = 0;  // Space will handle this for the header

								var newShow3D = EditorGUILayout.Toggle("Use 3D Settings?", aVar.showAudio3DSettings);
								if (newShow3D != aVar.showAudio3DSettings) {
									UndoHelper.RecordObjectPropertyForUndo(_creator, "toggle Use 3D Settings");
									aVar.showAudio3DSettings = newShow3D;
								}
								if (aVar.showAudio3DSettings) {
							        EditorGUI.indentLevel = 1;  // Space will handle this for the header

									var newDoppler = EditorGUILayout.Slider("Doppler Level", aVar.audDopplerLevel, 0f, 5f);
									if (newDoppler != aVar.audDopplerLevel) {
										UndoHelper.RecordObjectPropertyForUndo(_creator, "change Doppler Level");
										aVar.audDopplerLevel = newDoppler;
									}

									var newRolloffMode = (AudioRolloffMode) EditorGUILayout.EnumPopup("Volume Rolloff", aVar.audRollOffMode);
									if (newRolloffMode != aVar.audRollOffMode) {
										UndoHelper.RecordObjectPropertyForUndo(_creator, "change Volume Rolloff");
										aVar.audRollOffMode = newRolloffMode;
									}

									var newMinDist = EditorGUILayout.FloatField("Min Distance", aVar.audMinDistance);
									if (newMinDist != aVar.audMinDistance) {
										UndoHelper.RecordObjectPropertyForUndo(_creator, "change Min Distance");
										aVar.audMinDistance = newMinDist;
									}

									var newSpread = EditorGUILayout.Slider("Spread", aVar.audSpread, 0f, 360f);
									if (newSpread != aVar.audSpread) {
										UndoHelper.RecordObjectPropertyForUndo(_creator, "change Spread");
										aVar.audSpread = newSpread;
									}

									var newMaxDist = EditorGUILayout.FloatField("Max Distance", aVar.audMaxDistance);
									if (newMaxDist != aVar.audMaxDistance) {
										UndoHelper.RecordObjectPropertyForUndo(_creator, "change Max Distance");
										aVar.audMaxDistance = newMaxDist;
									}

									if (_creator.audio == null) {
										GUIHelper.ShowColorWarning("*You have no Audio Source in this prefab. Please delete prefab.");
										GUIHelper.ShowColorWarning(" Then add a new Dyanmic SGC from the Audio Manager window.");
									} else {
										EditorGUILayout.BeginHorizontal();
										//GUILayout.Label("Actions", EditorStyles.wordWrappedLabel, GUILayout.Width(50f));
										GUILayout.Space(15);
										GUI.contentColor = Color.green;
										if (GUILayout.Button(new GUIContent("Copy 3D to Audio Source", "Copy the 3D settings to the Audio Source in this prefab for using the Unity widgets."), EditorStyles.toolbarButton, GUILayout.Width(160))) {
											isDirty = true;
											Copy3DSettingsToAudioSource(_creator.audio, aVar);
										}	
	
										GUILayout.Space(10);
										
										if (GUILayout.Button(new GUIContent("Copy 3D from Audio Source", "Copy the 3D settings from the Audio Source in this prefab back into this section."), EditorStyles.toolbarButton, GUILayout.Width(160))) {
											isDirty = true;
											Copy3DSettingsFromAudioSource(_creator.audio, aVar);
										}
										GUI.contentColor = Color.white;
										EditorGUILayout.EndHorizontal();
									}
								}
							}
							
							switch (variationButtonPressed) {
								case GUIHelper.DTFunctionButtons.Add:
									addVariationIndex = v;
									break;
								case GUIHelper.DTFunctionButtons.Remove:
									removeVariationIndex = v;
									break;
								case GUIHelper.DTFunctionButtons.ShiftUp:
									variationIndextoShiftUp = v;	
									break;
								case GUIHelper.DTFunctionButtons.ShiftDown:
									variationIndextoShiftDown = v;	
									break;
								case GUIHelper.DTFunctionButtons.Play:
									_creator.audio.Stop();

									if (aVar.audLocation == SoundGroupVariation.AudioLocation.ResourceFile) {
										_creator.audio.PlayOneShot(Resources.Load(aVar.resourceFileName) as AudioClip, aVar.volume);
									} else {
										_creator.audio.PlayOneShot(aVar.clip, aVar.volume);
									}
									break;
								case GUIHelper.DTFunctionButtons.Stop:
									_creator.audio.Stop();
									break;
							}
						}
					}
				}
				
				if (addVariationIndex.HasValue) {
					var newVar = new DynamicSoundGroupVariation();
					UndoHelper.RecordObjectPropertyForUndo(_creator, "add Variation");
					aGroup.variations.Insert(addVariationIndex.Value + 1, newVar);
				} else if (removeVariationIndex.HasValue) {
					UndoHelper.RecordObjectPropertyForUndo(_creator, "remove Variation");
					aGroup.variations.RemoveAt(removeVariationIndex.Value); 
				} else if (variationIndextoShiftUp.HasValue) {
					UndoHelper.RecordObjectPropertyForUndo(_creator, "shift Variation Up");
					var item = aGroup.variations[variationIndextoShiftUp.Value];
					aGroup.variations.Insert(variationIndextoShiftUp.Value - 1, item);
					aGroup.variations.RemoveAt(variationIndextoShiftUp.Value + 1);
				} else if (variationIndextoShiftDown.HasValue) {
					UndoHelper.RecordObjectPropertyForUndo(_creator, "shift Variation Down");

					var index = variationIndextoShiftDown.Value + 1;
	
					var item = aGroup.variations[index];
					aGroup.variations.Insert(index - 1, item);
					aGroup.variations.RemoveAt(index + 1);
				}
				
				switch (groupButtonPressed) {
					case GUIHelper.DTFunctionButtons.Add:
						addGroupIndex = i;
						break;
					case GUIHelper.DTFunctionButtons.Remove:
						removeGroupIndex = i;
						break;
				}

				EditorGUILayout.Separator();
			}
			
			if (addGroupIndex.HasValue) {
				var newGroup = new DynamicSoundGroupInfo();
				UndoHelper.RecordObjectPropertyForUndo(_creator, "add Group");
				_creator.soundGroupsToCreate.Insert(addGroupIndex.Value + 1, newGroup);
			} else if (removeGroupIndex.HasValue) {
				if (_creator.soundGroupsToCreate.Count <= 1) {
					GUIHelper.ShowAlert("You cannot delete the last new Sound Group. You can delete the prefab if you don't need any.");
				} else {
					UndoHelper.RecordObjectPropertyForUndo(_creator, "remove Group");
					_creator.soundGroupsToCreate.RemoveAt(removeGroupIndex.Value); 
				}
			}	
		}
		
		if (GUI.changed || isDirty) {
			EditorUtility.SetDirty(target);
		}

		GUIHelper.RepaintIfUndoOrRedo(this);

		//DrawDefaultInspector();
    }
	
	private void CreateVariation(DynamicSoundGroupInfo aGroup, AudioClip aClip) {
		var resourceFileName = string.Empty;
		if (_creator.bulkVariationMode == SoundGroupVariation.AudioLocation.ResourceFile) {
			resourceFileName = GUIHelper.GetResourcePath(aClip);
			if (string.IsNullOrEmpty(resourceFileName)) {
				resourceFileName = aClip.name;
			}
		}

		var clipName = UtilStrings.TrimSpace(aClip.name);
		
		var matchingVar = aGroup.variations.Find(delegate(DynamicSoundGroupVariation obj) {
			return obj.clipName == aClip.name;
		});
		
		if (matchingVar != null) {
			GUIHelper.ShowAlert("You already have a variation for this Group named '" + clipName + "'. \n\nPlease rename these variations when finished to be unique, or you may not be able to play them by name if you have a need to.");
		}

		var newVar = new DynamicSoundGroupVariation() {
			clipName = clipName
		};
		
		if (_creator.bulkVariationMode == SoundGroupVariation.AudioLocation.ResourceFile) {
			newVar.audLocation = SoundGroupVariation.AudioLocation.ResourceFile;
			newVar.resourceFileName = resourceFileName;
		} else {
			newVar.clip = aClip;
		}

		UndoHelper.RecordObjectPropertyForUndo(_creator, "add Group / Variation");
		aGroup.variations.Add(newVar);
		
		if (aGroup.groupName == DynamicSoundGroupInfo.NEW_GROUP_START_NAME) {
			aGroup.groupName = clipName;
		}
	}

	private DynamicSoundGroupInfo CreateGroup(AudioClip aClip) {
		var groupName = UtilStrings.TrimSpace(aClip.name);
		
		var matchingGroup = _creator.soundGroupsToCreate.Find(delegate(DynamicSoundGroupInfo obj) {
			return obj.groupName == groupName;
		});
		
		if (matchingGroup != null) {
			GUIHelper.ShowAlert("You already have a Group named '" + groupName + "'. \n\nPlease rename this Group when finished to be unique.");
		}

		var newGroup = new DynamicSoundGroupInfo() {
			groupName = groupName
		};

		UndoHelper.RecordObjectPropertyForUndo(_creator, "add Group");
		_creator.soundGroupsToCreate.Add(newGroup);
		CreateVariation(newGroup, aClip);
		
		return newGroup;
	}
	
	private void ExpandCollapseAllSoundGroups(bool shouldExpand) {
		DynamicSoundGroupInfo aGroup = null;
	
		UndoHelper.RecordObjectPropertyForUndo(_creator, shouldExpand ? "expand" : "collapse" + " all Sound Groups");

		for (var i = 0; i < _creator.soundGroupsToCreate.Count; i++) {
			aGroup = _creator.soundGroupsToCreate[i];
			aGroup.isExpanded = shouldExpand;
			
			for (var v = 0; v < aGroup.variations.Count; v++) {
				aGroup.variations[v].isExpanded = shouldExpand;
			}
		}
	}
	
	public void EqualizeWeights(DynamicSoundGroupInfo _group) {
		UndoHelper.RecordObjectPropertyForUndo(_creator, "Equalize Weights");

		foreach (var variation in _group.variations) {
			variation.weight = 1;
		}
	}
	
	public void AlphaSortVariations(DynamicSoundGroupInfo _group) {
		UndoHelper.RecordObjectPropertyForUndo(_creator, "Alpha Sort Variations");

		_group.variations.Sort(delegate(DynamicSoundGroupVariation x, DynamicSoundGroupVariation y) {
			return x.clipName.CompareTo(y.clipName);
		});
	}
	
	private void Copy3DSettingsToAudioSource(AudioSource aSource, DynamicSoundGroupVariation aVar) {
		UndoHelper.RecordObjectPropertyForUndo(_creator.audio, "Copy 3D to Audio Source");

		aSource.dopplerLevel = aVar.audDopplerLevel;
		aSource.rolloffMode = aVar.audRollOffMode;
		aSource.minDistance = aVar.audMinDistance;
		aSource.spread = aVar.audSpread;
		aSource.maxDistance = aVar.audMaxDistance;
		Debug.Log("3D Settings copied from variation '" + aVar.clipName + "' to Audio Source.");
	}
	
	private void Copy3DSettingsFromAudioSource(AudioSource aSource, DynamicSoundGroupVariation aVar) {
		UndoHelper.RecordObjectPropertyForUndo(_creator, "Copy 3D from Audio Source");

		aVar.audDopplerLevel = aSource.dopplerLevel;
		aVar.audRollOffMode = aSource.rolloffMode;
		aVar.audMinDistance = aSource.minDistance;
		aVar.audSpread = aSource.spread;
		aVar.audMaxDistance = aSource.maxDistance;
		Debug.Log("3D Settings copied from Audio Source to variation '" + aVar.clipName + "'.");
	}
}
