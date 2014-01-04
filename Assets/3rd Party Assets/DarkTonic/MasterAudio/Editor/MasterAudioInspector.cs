using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(MasterAudio))] 
public class MasterAudioInspector : Editor {
	private const string NO_MUTE_SOLO_ALLOWED = "You cannot mute or solo this Group because the bus it uses is soloed or muted. Please unmute or unsolo the bus instead.";
	private const string NEW_BUS_NAME = "[NEW BUS]";
	private const string RENAME_ME_BUS_NAME = "[BUS NAME]";
	private bool isValid = true;
	private MasterAudio sounds;
	
	public List<MasterAudioGroup> groups = new List<MasterAudioGroup>();
	private	List<string> playlistNames = new List<string>();
	
	public override void OnInspectorGUI() {
		EditorGUIUtility.LookLikeControls();
		EditorGUI.indentLevel = 0;
		
		sounds = (MasterAudio)target;
		
		if (sounds.logoTexture != null) {
			GUIHelper.ShowHeaderTexture(sounds.logoTexture);
		}
		
		this.ScanGroups();	
		
		if (!isValid) {
			return;
		}
		
		playlistNames = new List<string>();
		MasterAudio.Playlist pList = null;
		
		var maxPlaylistNameChars = 11;
		for (var i = 0; i < sounds.musicPlaylists.Count; i++) {
			pList = sounds.musicPlaylists[i];
			
			playlistNames.Add(pList.playlistName);
			if (pList.playlistName.Length > maxPlaylistNameChars) {
				maxPlaylistNameChars = pList.playlistName.Length;
			}
		}
		
		var groupNameList = GroupNameList;
		
		var busFilterList = new List<string>();
		busFilterList.Add(MasterAudio.ALL_BUSES_NAME);
		busFilterList.Add(MasterAudioGroup.NO_BUS);
		
		var maxChars = 9;
		var busList = new List<string>();
		busList.Add(MasterAudioGroup.NO_BUS);
		busList.Add(NEW_BUS_NAME);
		
		var busVoiceLimitList = new List<string>();
		busVoiceLimitList.Add(MasterAudio.NO_VOICE_LIMIT_NAME);

		for (var i = 1; i <= 32; i++) {
			busVoiceLimitList.Add(i.ToString());
		}
		
		GroupBus bus = null;
		for (var i = 0; i < sounds.groupBuses.Count; i++) {
			bus = sounds.groupBuses[i];
			busList.Add(bus.busName);
			busFilterList.Add(bus.busName);
			
			if (bus.busName.Length > maxChars) {
				maxChars = bus.busName.Length;
			}
		}
		var busListWidth = 9 * maxChars;
		var playlistListWidth = 9 * maxPlaylistNameChars;
		
		PlaylistController.Instances = null;
		var pcs = PlaylistController.Instances;
		var plControllerInScene = pcs.Count > 0;
		
		// mixer master volume!
		EditorGUILayout.BeginHorizontal();
		var volumeBefore = sounds.masterAudioVolume;
		GUILayout.Label("Master Mixer Volume");
		GUILayout.Space(20);
		
		var newMasterVol = GUILayout.HorizontalSlider(sounds.masterAudioVolume, 0f, 1f, GUILayout.Width(130));
		if (newMasterVol != sounds.masterAudioVolume) {
			UndoHelper.RecordObjectPropertyForUndo(sounds, "change Master Mixer Volume");
			if (Application.isPlaying) {
				MasterAudio.MasterVolumeLevel = newMasterVol;
			} else {
				sounds.masterAudioVolume = newMasterVol;
			}
		}
		GUILayout.Label(sounds.masterAudioVolume.ToString("N2"));
		GUILayout.Space(10);
		
		var mixerMuteButtonPressed = GUIHelper.AddMixerMuteButton("Mixer", sounds);
		
		GUILayout.FlexibleSpace();
		
		if (mixerMuteButtonPressed == GUIHelper.DTFunctionButtons.Mute) {
			UndoHelper.RecordObjectPropertyForUndo(sounds, "toggle Mixer Mute");
			
			sounds.mixerMuted = !sounds.mixerMuted;
			if (Application.isPlaying) {
				MasterAudio.MixerMuted = sounds.mixerMuted;
			} else {
				for (var i = 0; i < groups.Count; i++) {
					var aGroup = groups[i];
					aGroup.isMuted = sounds.mixerMuted;
					if (aGroup.isMuted) {
						aGroup.isSoloed = false;
					}
				}
			}
		}
		
		EditorGUILayout.EndHorizontal();
		
		if (volumeBefore != sounds.masterAudioVolume) {
			// fix it for realtime adjustments!
			MasterAudio.MasterVolumeLevel = sounds.masterAudioVolume;
		}
		
		// playlist master volume!
		if (plControllerInScene) {
			EditorGUILayout.BeginHorizontal();
			GUILayout.Label("Master Playlist Volume");
			GUILayout.Space(11);
			var newPlaylistVol = GUILayout.HorizontalSlider(sounds.masterPlaylistVolume, 0f, 1f, GUILayout.Width(130));
			if (newPlaylistVol != sounds.masterPlaylistVolume) {
				UndoHelper.RecordObjectPropertyForUndo(sounds, "change Master Playlist Volume");
				if (Application.isPlaying) {
					MasterAudio.PlaylistMasterVolume = newPlaylistVol;
				} else {
					sounds.masterPlaylistVolume = newPlaylistVol;
				}
			}
			GUILayout.Label(sounds.masterPlaylistVolume.ToString("N2"));
			
			GUILayout.Space(10);
			var playlistMuteButtonPressed = GUIHelper.AddPlaylistMuteButton("All Playlists", sounds);
			if (playlistMuteButtonPressed == GUIHelper.DTFunctionButtons.Mute) {
				if (Application.isPlaying) {
					MasterAudio.PlaylistsMuted = !MasterAudio.PlaylistsMuted;
				} else {
					sounds.playlistsMuted = !sounds.playlistsMuted;
					
					for (var i = 0; i < pcs.Count; i++) {
						pcs[i].IsMuted = sounds.playlistsMuted;
					}
				}
			}
			
			GUILayout.FlexibleSpace();
			EditorGUILayout.EndHorizontal();
			
			EditorGUILayout.BeginHorizontal();
			GUILayout.Label("Cross-fade Time (sec)");
			GUILayout.Space(11);
			var newCrossTime = GUILayout.HorizontalSlider(sounds.crossFadeTime, 0f, 10f, GUILayout.Width(130));
			GUILayout.Label(sounds.crossFadeTime.ToString("N2"));
			if (newCrossTime != sounds.crossFadeTime) {
				UndoHelper.RecordObjectPropertyForUndo(sounds, "change Cross-fade Time");
				sounds.crossFadeTime = newCrossTime;
			}
			GUILayout.FlexibleSpace();
			EditorGUILayout.EndHorizontal();
			
			// jukebox controls
			if (Application.isPlaying) { 
				DisplayJukebox();
			}
		}
		
		if (!Application.isPlaying) {
			var newPersist = EditorGUILayout.Toggle("Persist Across Scenes", sounds.persistBetweenScenes);
			if (newPersist != sounds.persistBetweenScenes) {
				UndoHelper.RecordObjectPropertyForUndo(sounds, "toggle Persist Across Scenes");
				sounds.persistBetweenScenes = newPersist;
			}
		}
		
		var newResourcePause = EditorGUILayout.Toggle("Keep Paused Resources", sounds.resourceClipsPauseDoNotUnload);
		if (newResourcePause != sounds.resourceClipsPauseDoNotUnload) {
			UndoHelper.RecordObjectPropertyForUndo(sounds, "toggle Keep Paused Resources");
			sounds.resourceClipsPauseDoNotUnload = newResourcePause;
		}
		
		if (sounds.persistBetweenScenes && plControllerInScene) {
			GUIHelper.ShowColorWarning("*Playlist Controller will also persist between scenes!");
		}
		
		var newLog = EditorGUILayout.Toggle("Log All Sounds", sounds.LogSounds);
		if (newLog != sounds.LogSounds) {
			if (Application.isPlaying) {
				MasterAudio.LogSoundsEnabled = sounds.LogSounds;
			}
			sounds.LogSounds = newLog;
		}
		
		// Music Ducking Start
		EditorGUI.indentLevel = 0;
		EditorGUILayout.BeginHorizontal(EditorStyles.objectFieldThumb);
		
		var newShowDuck = EditorGUILayout.Toggle("Show Music Ducking", sounds.showMusicDucking);		
		if (newShowDuck != sounds.showMusicDucking) {
			UndoHelper.RecordObjectPropertyForUndo(sounds, "toggle Show Music Ducking");
			sounds.showMusicDucking = newShowDuck;
		}
		EditorGUILayout.EndHorizontal();
		
		if (sounds.showMusicDucking) {
			var newEnableDuck = EditorGUILayout.BeginToggleGroup("Enable Ducking", sounds.EnableMusicDucking);
			if (newEnableDuck != sounds.EnableMusicDucking) {
				UndoHelper.RecordObjectPropertyForUndo(sounds, "toggle Enable Ducking");
				sounds.EnableMusicDucking = newEnableDuck;
			}
			
			EditorGUILayout.Separator();
			var newMult = EditorGUILayout.Slider("Ducked Vol Multiplier", sounds.duckedVolumeMultiplier, 0f, 1f);
			if (newMult != sounds.duckedVolumeMultiplier) {
				UndoHelper.RecordObjectPropertyForUndo(sounds, "change Ducked Vol Multiplier");
				sounds.DuckedVolumeMultiplier = newMult;
			}
			
			GUI.contentColor = Color.green;
			EditorGUILayout.BeginHorizontal();
			GUILayout.Space(10);
			
			if (GUILayout.Button(new GUIContent("Add Duck Group"), EditorStyles.toolbarButton, GUILayout.Width(100))) {
				UndoHelper.RecordObjectPropertyForUndo(sounds, "Add Duck Group");
				sounds.musicDuckingSounds.Add(new DuckGroupInfo() {
					soundType = MasterAudio.NO_GROUP_NAME
				});	
			}
			
			EditorGUILayout.EndHorizontal();
			GUI.contentColor = Color.white;
			EditorGUILayout.Separator();
			
			if (sounds.musicDuckingSounds.Count == 0) {
				GUIHelper.ShowColorWarning("No ducking sounds set up yet.");
			} else {
				int? duckSoundToRemove = null;
				
				for (var i = 0; i < sounds.musicDuckingSounds.Count; i++) {
					var duckSound = sounds.musicDuckingSounds[i];
					var index = groupNameList.IndexOf(duckSound.soundType);
					if (index == -1) {
						index = 0;
					}
					
					EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
					var newIndex = EditorGUILayout.Popup(index, groupNameList.ToArray(), GUILayout.MaxWidth(200));
					if (newIndex >= 0) {
						if (index != newIndex) {
							UndoHelper.RecordObjectPropertyForUndo(sounds, "change Duck Group");							
						}
						duckSound.soundType = groupNameList[newIndex];
					}
					
					GUI.contentColor = Color.green;
					GUILayout.TextField("Begin Unduck " + duckSound.riseVolStart.ToString("N2"), 20, EditorStyles.miniLabel);
					
					var newUnduck = GUILayout.HorizontalSlider(duckSound.riseVolStart, 0f, 1f, GUILayout.Width(60));
					if (newUnduck != duckSound.riseVolStart) {
						UndoHelper.RecordObjectPropertyForUndo(sounds, "change Begin Unduck");
						duckSound.riseVolStart = newUnduck;
					}
					GUI.contentColor = Color.white;
					
					GUILayout.FlexibleSpace();
					GUILayout.Space(10);
					if (GUIHelper.AddDeleteIcon(sounds)) {
						duckSoundToRemove = i;	
					}
					
					EditorGUILayout.EndHorizontal();
				}
				
				if (duckSoundToRemove.HasValue) {
					UndoHelper.RecordObjectPropertyForUndo(sounds, "delete Duck Group");
					sounds.musicDuckingSounds.RemoveAt(duckSoundToRemove.Value);
				}	
				
			}
			
			EditorGUILayout.EndToggleGroup();
		}
		// Music Ducking End
		
		// Sound Groups Start		
		EditorGUILayout.BeginHorizontal();
		EditorGUI.indentLevel = 0;  // Space will handle this for the header
		
		EditorGUILayout.BeginHorizontal(EditorStyles.objectFieldThumb);
		var newGroupEx = EditorGUILayout.Toggle("Show Group Mixer", sounds.areGroupsExpanded);
		if (newGroupEx != sounds.areGroupsExpanded) {
			UndoHelper.RecordObjectPropertyForUndo(sounds, "toggle Show Group Mixer");
			sounds.areGroupsExpanded = newGroupEx;
		}
		
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.EndHorizontal();
		
		GameObject groupToDelete = null;
		
		if (sounds.areGroupsExpanded) {
			EditorGUI.indentLevel = 0;
			
			var newGroupMode = (MasterAudio.DragGroupMode) EditorGUILayout.EnumPopup("Bulk Creation Mode", sounds.curDragGroupMode);
			if (newGroupMode != sounds.curDragGroupMode) {
				UndoHelper.RecordObjectPropertyForUndo(sounds, "change Bulk Creation Mode");
				sounds.curDragGroupMode = newGroupMode;
			}
			
			var newBulkMode = (SoundGroupVariation.AudioLocation) EditorGUILayout.EnumPopup("Bulk Variation Mode",  sounds.bulkLocationMode);
			if (newBulkMode != sounds.bulkLocationMode) {
				UndoHelper.RecordObjectPropertyForUndo(sounds, "change Bulk Variation Mode");
				sounds.bulkLocationMode = newBulkMode;
			}
			
			if (sounds.bulkLocationMode == SoundGroupVariation.AudioLocation.ResourceFile) {
				GUIHelper.ShowColorWarning("*Resource mode: make sure to drag from Resource folders only.");
			}
			
			// create groups start
			EditorGUILayout.BeginVertical();
			var anEvent = Event.current;
			
			GUI.color = Color.yellow;
			
			var dragArea = GUILayoutUtility.GetRect(0f,35f,GUILayout.ExpandWidth(true));
			GUI.Box (dragArea, "Drag Audio clips here to create groups!");
			
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
						
						Transform groupTrans = null;
						
						foreach (var dragged in DragAndDrop.objectReferences) {
							var aClip = dragged as AudioClip;
							if(aClip == null) {
								continue;
							}
							
							if (sounds.curDragGroupMode == MasterAudio.DragGroupMode.OneGroupPerClip) {
								CreateSoundGroup(aClip);
							} else {
								if (groupTrans == null) { // one group with variations
									groupTrans = CreateSoundGroup(aClip);
								} else {
									CreateVariation(groupTrans, aClip);
									// create the variations
								}
							}
						}
					}
					Event.current.Use();
					break;
			}
			EditorGUILayout.EndVertical();
			// create groups end
			
			EditorGUILayout.LabelField("Group Control", EditorStyles.miniBoldLabel);
			
			if (sounds.groupBuses.Count > 0) {
				var newGroupByBus = GUILayout.Toggle(sounds.groupByBus, "Group by Bus");
				if (newGroupByBus != sounds.groupByBus) {
					UndoHelper.RecordObjectPropertyForUndo(sounds, "toggle Group by Bus");
					sounds.groupByBus = newGroupByBus;
				}
			}
			
			var newBusFilterIndex = -1;
			var busFilterActive = false;
			
			if (sounds.groupBuses.Count > 0) {
				busFilterActive = true;
				var oldBusFilter = busFilterList.IndexOf(sounds.busFilter);
				if (oldBusFilter == -1) {
					 oldBusFilter = 0;
				}
				
				newBusFilterIndex = EditorGUILayout.Popup("Bus Filter", oldBusFilter, busFilterList.ToArray());
				
				var newBusFilter = busFilterList[newBusFilterIndex];
				
				if (sounds.busFilter != newBusFilter) {
					UndoHelper.RecordObjectPropertyForUndo(sounds, "change Bus Filter");
					sounds.busFilter = newBusFilter;
				}
			}
			
			GUIHelper.DTFunctionButtons groupButtonPressed = GUIHelper.DTFunctionButtons.None;
			
			MasterAudioGroup aGroup = null;
			var filteredGroups = new List<MasterAudioGroup>();
			
			filteredGroups.AddRange(this.groups);

			if (busFilterActive && !string.IsNullOrEmpty(sounds.busFilter)) {
				if (newBusFilterIndex == 0) {
					// no filter
				} else if (newBusFilterIndex == 1) {
					filteredGroups.RemoveAll(delegate(MasterAudioGroup obj) {
						return obj.busIndex != 0;
					});
				} else {
					filteredGroups.RemoveAll(delegate(MasterAudioGroup obj) {
						return obj.busIndex != newBusFilterIndex;
					});
				}
			}
			
			if (filteredGroups.Count == 0) {
				if (busFilterActive) {
					GUIHelper.ShowColorWarning("*You currently have zero Sound Groups matching your filter.");
				} else {
					GUIHelper.ShowColorWarning("*You currently have zero Sound Groups.");
				}
			} else {
				int? busToCreate = null;
				
				for (var l = 0; l < filteredGroups.Count; l++) {
					EditorGUI.indentLevel = 0;
					aGroup = filteredGroups[l];
					
					string sType = string.Empty;
					if (Application.isPlaying) {
						sType = aGroup.name;
					}
					
					EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
					var groupName = aGroup.name;
					
					EditorGUILayout.LabelField(groupName, EditorStyles.label, GUILayout.MinWidth(50));    
					//GUILayout.Space(90);
					
					EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(50));
					
					// find bus.
					var selectedBusIndex = aGroup.busIndex == -1 ? 0 : aGroup.busIndex;
					
					GUI.contentColor = Color.white;
					GUI.color = Color.cyan;
					
					var busIndex = EditorGUILayout.Popup("", selectedBusIndex, busList.ToArray(), GUILayout.Width(busListWidth));
					if (busIndex == -1) {
						busIndex = 0; 
					}
					
					if (aGroup.busIndex != busIndex && busIndex != 1) {
						UndoHelper.RecordObjectPropertyForUndo(aGroup, "change Group Bus");
					}
					
					if (busIndex != 1) { // don't change the index, so undo will work.
						aGroup.busIndex = busIndex;
					}
					
					GUI.color = Color.white;
					
					if (selectedBusIndex != busIndex) {
						if (busIndex == 1) {
							busToCreate = l;
						} else if (busIndex >= MasterAudio.HARD_CODED_BUS_OPTIONS) {
							GroupBus newBus = sounds.groupBuses[busIndex - MasterAudio.HARD_CODED_BUS_OPTIONS];
							if (Application.isPlaying) {
								var statGroup = MasterAudio.GrabGroup(sType);
								statGroup.busIndex = busIndex;
								
								if (newBus.isMuted) {
									MasterAudio.MuteGroup(aGroup.name);
								} else if (newBus.isSoloed) {
									MasterAudio.SoloGroup(aGroup.name);
								}
							} else {
								// check if bus soloed or muted.
								if (newBus.isMuted) {
									aGroup.isMuted = true;
									aGroup.isSoloed = false;
								} else if (newBus.isSoloed) {
									aGroup.isMuted = false;
									aGroup.isSoloed = true;
								}
							}
						}
					}
					
					GUI.contentColor = Color.green;
					GUILayout.TextField("V " + aGroup.groupMasterVolume.ToString("N2"), 6, EditorStyles.miniLabel);
					
					var newVol = GUILayout.HorizontalSlider(aGroup.groupMasterVolume, 0f, 1f, GUILayout.Width(60));
					if (newVol != aGroup.groupMasterVolume) {
						UndoHelper.RecordObjectPropertyForUndo(aGroup, "change Group Volume");
						aGroup.groupMasterVolume = newVol;
						if (Application.isPlaying) {
							MasterAudio.SetGroupVolume(aGroup.name, aGroup.groupMasterVolume);
						}
					}
					
					GUI.contentColor = Color.white;
					GUIHelper.AddLedSignalLight(sounds, groupName);
					
					groupButtonPressed = GUIHelper.AddMixerButtons(aGroup, "Group", sounds);
					
					EditorGUILayout.EndHorizontal();
					EditorGUILayout.EndHorizontal();
					
					GroupBus groupBus = null;
					var groupBusIndex = aGroup.busIndex - MasterAudio.HARD_CODED_BUS_OPTIONS;
					if (groupBusIndex >= 0 && groupBusIndex < sounds.groupBuses.Count) {
						groupBus = sounds.groupBuses[groupBusIndex];
					}
					
					switch (groupButtonPressed) {
						case GUIHelper.DTFunctionButtons.Play:
							if (Application.isPlaying) {
								MasterAudio.PlaySound(aGroup.name);
							} else {
								var rndIndex = UnityEngine.Random.Range(0, aGroup.groupVariations.Count);
								var rndVar = aGroup.groupVariations[rndIndex];
	
								if (rndVar.audLocation == SoundGroupVariation.AudioLocation.ResourceFile) {
									MasterAudio.PreviewerInstance.Stop();
									MasterAudio.PreviewerInstance.PlayOneShot(Resources.Load(rndVar.resourceFileName) as AudioClip, rndVar.audio.volume);
								} else {
									rndVar.audio.Play();
								}
							}
							break;
						case GUIHelper.DTFunctionButtons.Stop:
							if (Application.isPlaying) {
								MasterAudio.StopAllOfSound(aGroup.name);
							} else {
								var hasResourceFile = false;
								for (var i = 0; i < aGroup.groupVariations.Count; i++) {
									aGroup.groupVariations[i].audio.Stop();
									if (aGroup.groupVariations[i].audLocation == SoundGroupVariation.AudioLocation.ResourceFile) {
										hasResourceFile = true;
									} 
								}
							
								if (hasResourceFile) {
									MasterAudio.PreviewerInstance.Stop();
								}
							}
							break;
						case GUIHelper.DTFunctionButtons.Mute:
							if (groupBus != null && (groupBus.isMuted || groupBus.isSoloed)) {
								if (Application.isPlaying) {
									Debug.LogWarning(NO_MUTE_SOLO_ALLOWED);
								} else {
									GUIHelper.ShowAlert(NO_MUTE_SOLO_ALLOWED);
								}
							} else {
								UndoHelper.RecordObjectPropertyForUndo(aGroup, "toggle Group mute");	
								
								if (Application.isPlaying) {
									if (aGroup.isMuted) {
										MasterAudio.UnmuteGroup(sType);
									} else {
										MasterAudio.MuteGroup(sType);
									}
								} else {
									aGroup.isMuted = !aGroup.isMuted;
									if (aGroup.isMuted) {
										aGroup.isSoloed = false;
									}
								}
							}
							break;
						case GUIHelper.DTFunctionButtons.Solo:
							if (groupBus != null && (groupBus.isMuted || groupBus.isSoloed)) {
								if (Application.isPlaying) {
									Debug.LogWarning(NO_MUTE_SOLO_ALLOWED);
								} else {
									GUIHelper.ShowAlert(NO_MUTE_SOLO_ALLOWED);
								}
							} else {
								UndoHelper.RecordObjectPropertyForUndo(aGroup, "toggle Group solo");	
								
								if (Application.isPlaying) {
									if (aGroup.isSoloed) {							
										MasterAudio.UnsoloGroup(sType);
									} else {
										MasterAudio.SoloGroup(sType);
									}
								} else {
									aGroup.isSoloed = !aGroup.isSoloed;
									if (aGroup.isSoloed) {
										aGroup.isMuted = false;
									} 
								}
							}
							break;
						case GUIHelper.DTFunctionButtons.Go:
							Selection.activeObject = aGroup.transform;				
							break;
						case GUIHelper.DTFunctionButtons.Remove:
							groupToDelete = aGroup.transform.gameObject;
							break;
					}
					
					EditorUtility.SetDirty(aGroup);
				}
				
				if (busToCreate.HasValue) {
					CreateBus(busToCreate.Value);
				}
				
				if (groupToDelete != null) {
					UndoHelper.DestroyForUndo(groupToDelete);
				}
				
				EditorGUILayout.Separator();
				EditorGUILayout.BeginHorizontal();
				GUILayout.Space(10);
				GUI.contentColor = Color.green;
				if (GUILayout.Button(new GUIContent("Mute/Solo Reset", "Turn off all group mute and solo switches"), EditorStyles.toolbarButton, GUILayout.Width(120))) { 
					UndoHelper.RecordObjectsForUndo(this.groups.ToArray(), "Mute/Solo Reset");

					for (var l = 0; l < this.groups.Count; l++) {
						aGroup = this.groups[l];
						aGroup.isSoloed = false;
						aGroup.isMuted = false;
					}
					
				}
				
				GUILayout.Space(6);
				
				if (GUILayout.Button(new GUIContent("Max Group Volumes", "Reset all group volumes to full"), EditorStyles.toolbarButton, GUILayout.Width(120))) { 
					UndoHelper.RecordObjectsForUndo(this.groups.ToArray(), "Max Group Volumes");
					
					for (var l = 0; l < this.groups.Count; l++) {
						aGroup = this.groups[l];
						aGroup.groupMasterVolume = 1f;
					}
				}
				
				GUI.contentColor = Color.white;
				
				EditorGUILayout.EndHorizontal();
			}
			// Sound Groups End
			
			// Buses
			if (sounds.groupBuses.Count > 0) {
				EditorGUILayout.Separator();
				EditorGUILayout.LabelField("Bus Control", EditorStyles.miniBoldLabel);
				
				GroupBus aBus = null;
				GUIHelper.DTFunctionButtons busButtonPressed = GUIHelper.DTFunctionButtons.None;
				int? busToDelete = null;
				int? busToSolo = null;
				int? busToMute = null;
				
				for (var i = 0; i < sounds.groupBuses.Count; i++) {
					aBus = sounds.groupBuses[i];
					
					EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
					
					var newBusName = EditorGUILayout.TextField("", aBus.busName, GUILayout.MaxWidth(200));
					if (newBusName != aBus.busName) {
						UndoHelper.RecordObjectPropertyForUndo(sounds, "change Bus Name");
						aBus.busName = newBusName;
					}
					
					GUILayout.FlexibleSpace();

					GUILayout.Label("Voices");
					GUI.color = Color.cyan;
					
					var oldLimitIndex = busVoiceLimitList.IndexOf(aBus.voiceLimit.ToString());
					if (oldLimitIndex == -1) {
						oldLimitIndex = 0;
					}
					var busVoiceLimitIndex = EditorGUILayout.Popup("", oldLimitIndex, busVoiceLimitList.ToArray(), GUILayout.MaxWidth(70));
					if (busVoiceLimitIndex != oldLimitIndex) {
						UndoHelper.RecordObjectPropertyForUndo(sounds, "change Bus Voice Limit");
						aBus.voiceLimit = busVoiceLimitIndex <= 0 ? -1 : busVoiceLimitIndex;
					}
					
					GUI.color = Color.white;

					EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(50));
					GUILayout.TextField("V " + aBus.volume.ToString("N2"), 6, EditorStyles.miniLabel);
					
					var newBusVol = GUILayout.HorizontalSlider(aBus.volume, 0f, 1f, GUILayout.Width(86));
					if (newBusVol != aBus.volume) {
						UndoHelper.RecordObjectPropertyForUndo(sounds, "change Bus Volume");
						aBus.volume = newBusVol;
						if (Application.isPlaying) {
							MasterAudio.SetBusVolumeByName(aBus.busName, aBus.volume);
						}
					}
					
					GUI.contentColor = Color.white;
					
					busButtonPressed = GUIHelper.AddMixerBusButtons(aBus, sounds);
					
					switch (busButtonPressed) {
					case GUIHelper.DTFunctionButtons.Remove:
						busToDelete = i;
						break;
					case GUIHelper.DTFunctionButtons.Solo:
						busToSolo = i;
						break;
					case GUIHelper.DTFunctionButtons.Mute:
						busToMute = i;
						break;
					}
					
					EditorGUILayout.EndHorizontal();
					EditorGUILayout.EndHorizontal();
				}
				
				if (busToDelete.HasValue) {
					DeleteBus(busToDelete.Value);
				}
				if (busToMute.HasValue) {
					MuteBus(busToMute.Value);
				}
				if (busToSolo.HasValue) {
					SoloBus(busToSolo.Value);
				}
				
				EditorGUILayout.Separator();
				EditorGUILayout.BeginHorizontal();
				GUILayout.Space(10);
				GUI.contentColor = Color.green;
				
				if (GUILayout.Button(new GUIContent("Mute/Solo Reset", "Turn off all bus mute and solo switches"), EditorStyles.toolbarButton, GUILayout.Width(120))) { 
					BusMuteSoloReset();
				}
				
				GUILayout.Space(6);
				
				if (GUILayout.Button(new GUIContent("Max Bus Volumes", "Reset all bus volumes to full"), EditorStyles.toolbarButton, GUILayout.Width(120))) { 
					UndoHelper.RecordObjectPropertyForUndo(sounds, "Max Bus Volumes");
					
					for (var l = 0; l < sounds.groupBuses.Count; l++) {
						aBus = sounds.groupBuses[l];
						aBus.volume = 1f;
					}
				}
				
				GUI.contentColor = Color.white;
				
				EditorGUILayout.EndHorizontal();
			}
		}
		// Sound Buses End
		
		
		// Music playlist Start		
		EditorGUILayout.BeginHorizontal();
		EditorGUI.indentLevel = 0;  // Space will handle this for the header
		
		EditorGUILayout.BeginHorizontal(EditorStyles.objectFieldThumb);
		var isExp = EditorGUILayout.Toggle("Show Playlist Settings", sounds.playListExpanded);
		if (isExp != sounds.playListExpanded) {
			UndoHelper.RecordObjectPropertyForUndo(sounds, "toggle Show Playlist Settings");
			sounds.playListExpanded = isExp;
		}
		
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.EndHorizontal();
		
		if (sounds.playListExpanded) {
			EditorGUILayout.LabelField("Playlist Controller Setup", EditorStyles.miniBoldLabel);
			
			if (!plControllerInScene) {
				GUIHelper.ShowColorWarning("There are no Playlist Controllers in the scene. Music will not play.");
			} else {
				int? indexToDelete = null;
				
				playlistNames.Insert(0, MasterAudio.NO_PLAYLIST_NAME);
				
				for (var i = 0; i < pcs.Count; i++) {
					var control = pcs[i];
					EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
					GUILayout.Label(control.name);
					
					GUILayout.FlexibleSpace();
					
					GUI.color = Color.cyan;
					var origIndex = playlistNames.IndexOf(control.startPlaylistName);
					if (origIndex == -1) {
						origIndex = 0;
					}
					var newIndex = EditorGUILayout.Popup("", origIndex, playlistNames.ToArray(), GUILayout.Width(playlistListWidth));
					if (newIndex != origIndex) {
						UndoHelper.RecordObjectPropertyForUndo(control, "change Playlist Controller initial Playlist");
						control.startPlaylistName = playlistNames[newIndex];
					}
					GUI.color = Color.white;
					
					GUI.contentColor = Color.green;
					GUILayout.TextField("V " + control.playlistVolume.ToString("N2"), 6, EditorStyles.miniLabel);
					var newVol = GUILayout.HorizontalSlider(control.playlistVolume, 0f, 1f, GUILayout.Width(86));
					
					if (newVol != control.playlistVolume) {
						UndoHelper.RecordObjectPropertyForUndo(control, "change Playlist Controller volume");
						control.playlistVolume = newVol;
						control.UpdateMasterVolume();
					}
					
					GUI.contentColor = Color.white;
					
					var buttonPressed = GUIHelper.AddPlaylistControllerSetupButtons(control, "Playlist Controller", sounds, false);
					
					EditorGUILayout.EndHorizontal();
					
					switch (buttonPressed) {
						case GUIHelper.DTFunctionButtons.Go:
							Selection.activeObject = control.transform;
							break;
						case GUIHelper.DTFunctionButtons.Remove:
							indexToDelete = i;
							break;
						case GUIHelper.DTFunctionButtons.Mute:
							control.IsMuted = !control.IsMuted;
							break;
					}
					
					EditorUtility.SetDirty(control);
				}
				
				if (indexToDelete.HasValue) {
					UndoHelper.DestroyForUndo(pcs[indexToDelete.Value].gameObject);
				}
			}
			
			EditorGUILayout.Separator();
			GUI.contentColor = Color.green;
			EditorGUILayout.BeginHorizontal();
			GUILayout.Space(10);
			if (GUILayout.Button(new GUIContent("Create Playlist Controller"), EditorStyles.toolbarButton, GUILayout.Width(150))) {
				var go = GameObject.Instantiate(sounds.playlistControllerPrefab.gameObject);
				go.name = "PlaylistController";
				
				UndoHelper.CreateObjectForUndo(go as GameObject, "create Playlist Controller");
			}
			EditorGUILayout.EndHorizontal();
			GUI.contentColor = Color.white;
			EditorGUILayout.Separator();
			
			
			EditorGUILayout.LabelField("Playlist Setup", EditorStyles.miniBoldLabel);
			EditorGUI.indentLevel = 0;  // Space will handle this for the header
			
			EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
			var oldPlayExpanded = GUIHelper.Foldout(sounds.playlistEditorExpanded, "Playlists");
			if (oldPlayExpanded != sounds.playlistEditorExpanded) {
				UndoHelper.RecordObjectPropertyForUndo(sounds, "toggle Playlists");
				sounds.playlistEditorExpanded = oldPlayExpanded;
			}
			
			EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(100));
			
			if (sounds.musicPlaylists.Count > 0) {
				GUIContent content;
				var collapseIcon = '\u2261'.ToString();
				content = new GUIContent(collapseIcon, "Click to collapse all");
				var masterCollapse = GUILayout.Button(content, EditorStyles.toolbarButton);
				
				var expandIcon = '\u25A1'.ToString();
				content = new GUIContent(expandIcon, "Click to expand all");
				var masterExpand = GUILayout.Button(content, EditorStyles.toolbarButton);
				if (masterExpand) {
					ExpandCollapseAllPlaylists(true);
				} 
				if (masterCollapse) {
					ExpandCollapseAllPlaylists(false);
				}
				EditorGUILayout.EndHorizontal();
				EditorGUILayout.EndHorizontal();
				
				if (sounds.playlistEditorExpanded) {
					int? playlistToRemove = null;
					int? playlistToInsertAt = null;
					int? playlistToMoveUp = null;
					int? playlistToMoveDown = null;
					
					for (var i = 0; i < sounds.musicPlaylists.Count; i++) {
						EditorGUILayout.Separator();
						var aList = sounds.musicPlaylists[i];
						
						EditorGUI.indentLevel = 1;
						EditorGUILayout.BeginHorizontal(EditorStyles.objectFieldThumb);
						aList.isExpanded = GUIHelper.Foldout(aList.isExpanded, "Playlist: " + aList.playlistName);
						
						var playlistButtonPressed = GUIHelper.AddFoldOutListItemButtons(i, sounds.musicPlaylists.Count, "playlist", false, true);
						
						EditorGUILayout.EndHorizontal();
						
						if (aList.isExpanded) {
							EditorGUI.indentLevel = 0;
							var newPlaylist = EditorGUILayout.TextField("Name", aList.playlistName);
							if (newPlaylist != aList.playlistName) {
								UndoHelper.RecordObjectPropertyForUndo(sounds, "change Name");
								aList.playlistName = newPlaylist;
							}
							
							var newTransType = (MasterAudio.SongFadeInPosition) EditorGUILayout.EnumPopup("Song Transition Type", aList.songTransitionType);
							if (newTransType != aList.songTransitionType) {
								UndoHelper.RecordObjectPropertyForUndo(sounds, "change Song Transition Type");
								aList.songTransitionType = newTransType;
							}
							if (aList.songTransitionType == MasterAudio.SongFadeInPosition.SynchronizeClips) {
								GUIHelper.ShowColorWarning("*All clips must be of exactly the same length.");
							}
							
							EditorGUILayout.BeginHorizontal();
							GUILayout.Space(10);
							GUI.contentColor = Color.green;
							if (GUILayout.Button(new GUIContent("Equalize Song Volumes"), EditorStyles.toolbarButton)) {
								EqualizePlaylistVolumes(aList.MusicSettings);
							}
							GUILayout.FlexibleSpace();
							EditorGUILayout.EndHorizontal();
							GUI.contentColor = Color.white;
							EditorGUILayout.Separator();
							
							EditorGUILayout.BeginVertical();
							var anEvent = Event.current;
							
							GUI.color = Color.yellow;
							
							var dragArea = GUILayoutUtility.GetRect(0f,35f,GUILayout.ExpandWidth(true));
							GUI.Box (dragArea, "Drag Audio clips here to add to playlist!");
							
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
										
										AddSongToPlaylist(aList, aClip);
									}
								}
								Event.current.Use();
								break;
							}
							EditorGUILayout.EndVertical();
							
							EditorGUI.indentLevel = 2;
							
							int? addIndex = null;
							int? removeIndex = null;
							int? moveUpIndex = null;
							int? moveDownIndex = null;
							
							for (var j = 0; j < aList.MusicSettings.Count; j++) {
								var aSong = aList.MusicSettings[j];
								var clipName = aSong.clip == null ? "Empty" : aSong.clip.name;
								EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
								EditorGUI.indentLevel = 2;
								
								var newSongExpanded = GUIHelper.Foldout(aSong.isExpanded, clipName);
								if (newSongExpanded != aSong.isExpanded) {
									UndoHelper.RecordObjectPropertyForUndo(sounds, "toggle Song expand");
									aSong.isExpanded = newSongExpanded;
								}
								var songButtonPressed = GUIHelper.AddFoldOutListItemButtons(j, aList.MusicSettings.Count, "clip", false, true, true);
								EditorGUILayout.EndHorizontal();
								
								if (aSong.isExpanded) {
									EditorGUI.indentLevel = 0;
									
									var newLoop = EditorGUILayout.Toggle("Loop Clip", aSong.isLoop);
									if (newLoop != aSong.isLoop) {
										UndoHelper.RecordObjectPropertyForUndo(sounds, "toggle Loop Clip");
										aSong.isLoop = newLoop;
									}
									
									var newClip = (AudioClip) EditorGUILayout.ObjectField("Clip", aSong.clip, typeof(AudioClip), true);
									if (newClip != aSong.clip) {
										UndoHelper.RecordObjectPropertyForUndo(sounds, "change Clip");
										aSong.clip = newClip;
									}
									
									var newVol = EditorGUILayout.Slider("Volume", aSong.volume, 0f, 1f);
									if (newVol != aSong.volume) {
										UndoHelper.RecordObjectPropertyForUndo(sounds, "change Volume");
										aSong.volume = newVol;
									}
									
									var newPitch = EditorGUILayout.Slider("Pitch", aSong.pitch, -3f, 3f);
									if (newPitch != aSong.pitch) {
										UndoHelper.RecordObjectPropertyForUndo(sounds, "change Pitch");
										aSong.pitch = newPitch;
									}
								}
								
								switch (songButtonPressed) {
									case GUIHelper.DTFunctionButtons.Add:
										addIndex = j;
										break;
									case GUIHelper.DTFunctionButtons.Remove:
										removeIndex = j;
										break;
									case GUIHelper.DTFunctionButtons.ShiftUp:
										moveUpIndex = j;
										break;
									case GUIHelper.DTFunctionButtons.ShiftDown:
										moveDownIndex = j;
										break;
									case GUIHelper.DTFunctionButtons.Play:
										MasterAudio.PreviewerInstance.Stop();
										MasterAudio.PreviewerInstance.PlayOneShot(aSong.clip, aSong.volume);
										break;
									case GUIHelper.DTFunctionButtons.Stop:
										MasterAudio.PreviewerInstance.clip = null;
										MasterAudio.PreviewerInstance.Stop();
										break;
								}
							}
							
							if (addIndex.HasValue) {
								var mus = new MusicSetting();
								UndoHelper.RecordObjectPropertyForUndo(sounds, "add song");
								aList.MusicSettings.Insert(addIndex.Value + 1, mus);
							} else if (removeIndex.HasValue) {
								if (aList.MusicSettings.Count <= 1) {
									GUIHelper.ShowAlert("You cannot delete the last clip. You do not have to use the clips though.");
								} else {
									UndoHelper.RecordObjectPropertyForUndo(sounds, "delete song");
									aList.MusicSettings.RemoveAt(removeIndex.Value); 
								}
							} else if (moveUpIndex.HasValue) {
								var item = aList.MusicSettings[moveUpIndex.Value];
								
								UndoHelper.RecordObjectPropertyForUndo(sounds, "shift up song");
								
								aList.MusicSettings.Insert(moveUpIndex.Value - 1, item);
								aList.MusicSettings.RemoveAt(moveUpIndex.Value + 1);
							} else if (moveDownIndex.HasValue) {
								var index = moveDownIndex.Value + 1;
								var item = aList.MusicSettings[index];
								
								UndoHelper.RecordObjectPropertyForUndo(sounds, "shift down song");
								
								aList.MusicSettings.Insert(index - 1, item);
								aList.MusicSettings.RemoveAt(index + 1);
							}
						}
						
						switch (playlistButtonPressed) {
						case GUIHelper.DTFunctionButtons.Remove:
							playlistToRemove = i;
							break;
						case GUIHelper.DTFunctionButtons.Add:
							playlistToInsertAt = i;
							break;
						case GUIHelper.DTFunctionButtons.ShiftUp:
							playlistToMoveUp = i;
							break;
						case GUIHelper.DTFunctionButtons.ShiftDown:
							playlistToMoveDown = i;
							break;
						}
					}
					
					if (playlistToRemove.HasValue) {
						if (sounds.musicPlaylists.Count <= 1) {
							GUIHelper.ShowAlert("You cannot delete the last Playlist. You do not have to use it though.");
						} else {
							UndoHelper.RecordObjectPropertyForUndo(sounds, "delete Playlist");
							
							sounds.musicPlaylists.RemoveAt(playlistToRemove.Value);
						}
					}
					if (playlistToInsertAt.HasValue) {
						var pl = new MasterAudio.Playlist();
						UndoHelper.RecordObjectPropertyForUndo(sounds, "add Playlist");
						sounds.musicPlaylists.Insert(playlistToInsertAt.Value + 1, pl);
					}
					if (playlistToMoveUp.HasValue) {
						var item = sounds.musicPlaylists[playlistToMoveUp.Value];
						UndoHelper.RecordObjectPropertyForUndo(sounds, "shift up Playlist");
						sounds.musicPlaylists.Insert(playlistToMoveUp.Value - 1, item);
						sounds.musicPlaylists.RemoveAt(playlistToMoveUp.Value + 1);
					}
					if (playlistToMoveDown.HasValue) {
						var index = playlistToMoveDown.Value + 1;
						var item = sounds.musicPlaylists[index];
						
						UndoHelper.RecordObjectPropertyForUndo(sounds, "shift down Playlist");
						
						sounds.musicPlaylists.Insert(index - 1, item);
						sounds.musicPlaylists.RemoveAt(index + 1);
					}
				}
			} else {
				GUILayout.FlexibleSpace();
				EditorGUILayout.EndHorizontal();
				EditorGUILayout.EndHorizontal();
			}
		}
		// Music playlist End
		
		
		EditorUtility.SetDirty(target);
		
		if (Application.isPlaying) {
			this.Repaint(); // update the Playlist Jukebox
		}
		
		GUIHelper.RepaintIfUndoOrRedo(this);
		
		//DrawDefaultInspector();
	}
	
	
	private void AddSongToPlaylist(MasterAudio.Playlist pList, AudioClip aClip) {
		var lastClip = pList.MusicSettings[pList.MusicSettings.Count - 1];
		
		MusicSetting mus;
		
		UndoHelper.RecordObjectPropertyForUndo(sounds, "add Song");
		
		if (lastClip.clip == null) {
			mus = lastClip;
			mus.clip = aClip;
		} else {
			mus = new MusicSetting() {
				clip = aClip,
				volume = 1f,
				pitch = 1f,
				isExpanded = true
			};
			
			pList.MusicSettings.Add(mus);
		}
	}
	
	private void CreateVariation(Transform groupTrans, AudioClip aClip) {
		var resourceFileName = string.Empty;
		if (sounds.bulkLocationMode == SoundGroupVariation.AudioLocation.ResourceFile) {
			resourceFileName = GUIHelper.GetResourcePath(aClip);
			if (string.IsNullOrEmpty(resourceFileName)) {
				resourceFileName = aClip.name;
			}
		}
		
		GameObject newVariation = (GameObject) GameObject.Instantiate(sounds.soundGroupVariationTemplate.gameObject, groupTrans.position, Quaternion.identity);		
		
		var clipName = UtilStrings.TrimSpace(aClip.name);
		
		newVariation.name = clipName;
		newVariation.transform.parent = groupTrans;
		
		var variation = newVariation.GetComponent<SoundGroupVariation>();
		
		if (sounds.bulkLocationMode == SoundGroupVariation.AudioLocation.ResourceFile) {
			variation.audLocation = SoundGroupVariation.AudioLocation.ResourceFile;		
			variation.resourceFileName = resourceFileName;
		} else {
			newVariation.audio.clip = aClip;
		}
		
		newVariation.transform.name = clipName;
	}
	
	private Transform CreateSoundGroup(AudioClip aClip) {
		var groupName = aClip.name;
		
		if (sounds.soundGroupTemplate == null || sounds.soundGroupVariationTemplate == null) {
			GUIHelper.ShowAlert("Your MasterAudio prefab has been altered and cannot function properly. Please Revert it before continuing.");
			return null;
		}
		
		if (sounds.transform.FindChild(groupName) != null) {
			GUIHelper.ShowAlert("You already have a Sound Group named '" + groupName + "'. Please rename one of them when finished.");
		}
		
		var resourceFileName = string.Empty;
		if (sounds.bulkLocationMode == SoundGroupVariation.AudioLocation.ResourceFile) {
			resourceFileName = GUIHelper.GetResourcePath(aClip);
			if (string.IsNullOrEmpty(resourceFileName)) {
				resourceFileName = aClip.name;
			}
		}
		
		var newGroup = (GameObject) GameObject.Instantiate(sounds.soundGroupTemplate.gameObject, sounds.transform.position, Quaternion.identity);		
		//UndoHelper.CreateObjectForUndo(newGroup, "create Sound Group");
		
		var groupTrans = newGroup.transform;
		groupTrans.name = UtilStrings.TrimSpace(groupName);
		
		var sName = groupName;
		SoundGroupVariation variation = null;
		
		GameObject newVariation = (GameObject) GameObject.Instantiate(sounds.soundGroupVariationTemplate.gameObject, groupTrans.position, Quaternion.identity);		
		
		variation = newVariation.GetComponent<SoundGroupVariation>();
		
		if (sounds.bulkLocationMode == SoundGroupVariation.AudioLocation.ResourceFile) {
			variation.audLocation = SoundGroupVariation.AudioLocation.ResourceFile;		
			variation.resourceFileName = resourceFileName;
		} else {
			newVariation.audio.clip = aClip;
		}
		
		newVariation.transform.name = sName;
		newVariation.transform.parent = groupTrans;
		
		groupTrans.parent = sounds.transform;
		
		return groupTrans;
	}
	
	private void CreateBus(int groupIndex) {
		var sourceGroup = this.groups[groupIndex];
		
		var affectedObjects = new UnityEngine.Object[] {
			sounds,
			sourceGroup
		};
		
		UndoHelper.RecordObjectsForUndo(affectedObjects, "create Bus");
		
		var newBus = new GroupBus() {
			busName = RENAME_ME_BUS_NAME
		};
		sounds.groupBuses.Add(newBus);
		
		sourceGroup.busIndex = MasterAudio.HARD_CODED_BUS_OPTIONS + sounds.groupBuses.Count - 1;
	}
	
	private void SoloBus(int busIndex) {
		var bus = sounds.groupBuses[busIndex];
		
		var willSolo = !bus.isSoloed;
		
		MasterAudioGroup aGroup = null;
		string sType = string.Empty;
		
		var affectedGroups = new List<MasterAudioGroup>();
		
		for (var i = 0; i < this.groups.Count; i++) {
			aGroup = this.groups[i];
			
			if (aGroup.busIndex != MasterAudio.HARD_CODED_BUS_OPTIONS + busIndex) {
				continue;
			}
			
			affectedGroups.Add(aGroup);
		}
		
		var allObjects = new List<UnityEngine.Object>();
		allObjects.Add(sounds);
		
		foreach (var g in affectedGroups) {
			allObjects.Add (g as UnityEngine.Object);
		}
		
		UndoHelper.RecordObjectsForUndo(allObjects.ToArray(), "solo Bus");
		
		//change everything
		bus.isSoloed = willSolo;
		if (bus.isSoloed) {
			bus.isMuted = false;
		}
		
		foreach (var g in affectedGroups) {
			sType = g.name;
			
			if (Application.isPlaying) {
				if (willSolo) {
					MasterAudio.UnsoloGroup(sType);
				} else {
					MasterAudio.SoloGroup(sType);
				}
			} 
			
			g.isSoloed = willSolo;
			if (willSolo) {
				g.isMuted = false;
			}
		}
		
	}
	
	private void MuteBus(int busIndex) {
		var bus = sounds.groupBuses[busIndex];
		
		var willMute = !bus.isMuted; 
		
		MasterAudioGroup aGroup = null;
		
		var affectedGroups = new List<MasterAudioGroup>();
		
		for (var i = 0; i < this.groups.Count; i++) {
			aGroup = this.groups[i];
			
			if (aGroup.busIndex != MasterAudio.HARD_CODED_BUS_OPTIONS + busIndex) {
				continue;
			}
			
			affectedGroups.Add(aGroup);
		}
		
		var allObjects = new List<UnityEngine.Object>();
		allObjects.Add(sounds);
		foreach (var g in affectedGroups) {
			allObjects.Add(g as UnityEngine.Object);
		}
		
		UndoHelper.RecordObjectsForUndo(allObjects.ToArray(), "mute Bus");
		
		// change everything
		bus.isMuted = willMute;
		
		if (bus.isSoloed) {
			bus.isSoloed = false;
		}
		
		foreach (var g in affectedGroups) {
			if (Application.isPlaying) {
				if (!willMute) {				
					MasterAudio.UnmuteGroup(g.name);
				} else {
					MasterAudio.MuteGroup(g.name);
				}
			} else {
				g.isMuted = willMute;
				if (bus.isMuted) {
					g.isSoloed = false;
				}
			}
		}
	}
	
	private void DeleteBus(int busIndex) {
		MasterAudioGroup aGroup = null;
		
		var groupsWithBus = new List<MasterAudioGroup>();
		var groupsWithHigherBus = new List<MasterAudioGroup>();
		
		for (var i = 0; i < this.groups.Count; i++) {
			aGroup = this.groups[i];
			if (aGroup.busIndex == -1) {
				continue;
			}
			if (aGroup.busIndex == busIndex + MasterAudio.HARD_CODED_BUS_OPTIONS) {					
				groupsWithBus.Add(aGroup);
			} else if (aGroup.busIndex > busIndex + MasterAudio.HARD_CODED_BUS_OPTIONS) {
				groupsWithHigherBus.Add(aGroup);
			}
		}
		
		var allObjects = new List<UnityEngine.Object>();
		allObjects.Add(sounds);
		foreach (var g in groupsWithBus) {
			allObjects.Add(g as UnityEngine.Object);
		}
		
		foreach (var g in groupsWithHigherBus) {
			allObjects.Add(g as UnityEngine.Object);
		}
		
		UndoHelper.RecordObjectsForUndo(allObjects.ToArray(), "delete Bus");
		
		// change all
		sounds.groupBuses.RemoveAt(busIndex);
		
		foreach (var group in groupsWithBus) {
			group.busIndex = -1;
		}
		
		foreach (var group in groupsWithHigherBus) {
			group.busIndex--;
		}
	}
	
	private void ExpandCollapseAllPlaylists(bool expand) {
		UndoHelper.RecordObjectPropertyForUndo(sounds, "toggle Expand / Collapse Playlists");
		
		for (var i = 0; i < sounds.musicPlaylists.Count; i++) {
			var aList = sounds.musicPlaylists[i];
			aList.isExpanded = expand;
			
			for (var j = 0; j < aList.MusicSettings.Count; j++) {
				var aSong = aList.MusicSettings[j];
				aSong.isExpanded = expand;
			}
		}
	}
	
	private void ScanGroups() {
		this.groups.Clear();
		
		var names = new List<string>();
		
		for (var i = 0; i < sounds.transform.childCount; i++) {
			var aChild = sounds.transform.GetChild(i);
			if (names.Contains(aChild.name)) {
				GUIHelper.ShowColorWarning("You have more than one group named '" + aChild.name + "'.");
				GUIHelper.ShowColorWarning("Please rename one of them before continuing.");
				isValid = false;
				return;
			}
			
			names.Add(aChild.name);
			this.groups.Add(aChild.GetComponent<MasterAudioGroup>());
		}
		
		if (sounds.groupByBus) {
			this.groups.Sort(delegate(MasterAudioGroup g1, MasterAudioGroup g2)     {
				if (g1.busIndex == g2.busIndex) {
					return g1.name.CompareTo(g2.name);
				} else {
					return g1.busIndex.CompareTo(g2.busIndex);
				}
			});
		} else {
			this.groups.Sort(delegate(MasterAudioGroup g1, MasterAudioGroup g2)     {
				return g1.name.CompareTo(g2.name);
			});
		}
	}
	
	private List<string> GroupNameList {
		get {
			var groupNames = new List<string>();
			groupNames.Add(MasterAudio.NO_GROUP_NAME);
			
			for (var i = 0; i < groups.Count; i++) {
				groupNames.Add(groups[i].name);
			}
			
			return groupNames;
		}
	}
	
	private void DisplayJukebox() {
		EditorGUILayout.Separator();
		
		var pcs = PlaylistController.Instances;
		PlaylistController pl = null;
		
		var songNames = new List<string>();
		
		for (var i = 0; i < pcs.Count; i++) {
			pl = pcs[i];
			
			GUI.backgroundColor = Color.white;
			GUI.color = Color.cyan;
			
			EditorGUILayout.BeginVertical(EditorStyles.objectFieldThumb);
			EditorGUILayout.BeginHorizontal();
			
			var playlistIndex = -1;
			if (!string.IsNullOrEmpty(pl.startPlaylistName) && pl.CurrentPlaylist != null) {
				playlistIndex = playlistNames.IndexOf(pl.CurrentPlaylist.playlistName);
				
				songNames.Clear();
				for (var s = 0; s < pl.CurrentPlaylist.MusicSettings.Count; s++) {
					var aClip = pl.CurrentPlaylist.MusicSettings[s].clip;
					if (aClip == null) {
						continue;
					}
					songNames.Add(aClip.name);
				}
			}
			
			GUILayout.Label(pl.name);
			
			GUILayout.FlexibleSpace();
			
			GUI.color = Color.cyan;
			
			GUILayout.Label("V " + pl.playlistVolume.ToString("N2"));
			
			var newVol = GUILayout.HorizontalSlider(pl.playlistVolume, 0f, 1f, GUILayout.Width(100)); 
			if (newVol != pl.playlistVolume) {
				pl.playlistVolume = newVol;
				pl.UpdateMasterVolume();
			}

			GUI.color = Color.white;
			var muteButtonPressed = GUIHelper.AddPlaylistControllerSetupButtons(pl, "Playlist Controller", sounds, true);
			
			if (muteButtonPressed == GUIHelper.DTFunctionButtons.Mute) {
				pl.IsMuted = !pl.IsMuted;
			}
			
			EditorGUILayout.EndHorizontal();
			
			EditorGUILayout.EndVertical();
			
			GUI.backgroundColor = Color.cyan;
			GUI.color = Color.green;
			
			EditorGUILayout.BeginVertical(EditorStyles.objectFieldThumb);
			
			var clip = pl.CurrentPlaylistClip;
			string clipPosition = "";
			AudioSource playingSource = null;
			if (clip != null) {
				playingSource = pl == null ? null : pl.CurrentPlaylistSource;
				if (playingSource != null) {
					clipPosition = "(-" + (clip.length - playingSource.time).ToString("N2") + " secs)";
				}
			}
			
			EditorGUILayout.BeginHorizontal();
			GUILayout.Label("Playlist:");
			GUILayout.Space(30);
			var playlistIndexToStart = EditorGUILayout.Popup(playlistIndex, playlistNames.ToArray(), GUILayout.Width(150));
			
			if (playlistIndex != playlistIndexToStart) {
				pl.ChangePlaylist(playlistNames[playlistIndexToStart]);
			}
			GUILayout.Label(string.Format("[{0}]", pl.PlaylistState));
			GUILayout.FlexibleSpace();
			EditorGUILayout.EndHorizontal();
			
			EditorGUILayout.BeginHorizontal();
			GUILayout.Label("Active Clip:");
			GUILayout.Space(9);
			
			var songIndex = -1; 
			if (pl.CurrentPlaylistClip != null) {
				songIndex = songNames.IndexOf(pl.CurrentPlaylistClip.name);
			}
			var newSong = EditorGUILayout.Popup(songIndex, songNames.ToArray(), GUILayout.Width(150));
			if (newSong != songIndex) {
				pl.TriggerPlaylistClip(songNames[newSong]);
			}
			
			if (!string.IsNullOrEmpty(clipPosition)) {
				GUILayout.Label(clipPosition);
			}
			GUILayout.FlexibleSpace();
			EditorGUILayout.EndHorizontal();
			
			var fadingClip = pl == null ? null : pl.FadingPlaylistClip;
			var fadingClipName = fadingClip == null ? "[None]" : fadingClip.name;
			string fadingClipPosition = "";
			if (fadingClip != null) {
				var fadingSource = pl == null ? null : pl.FadingSource;
				if (fadingSource != null) {
					fadingClipPosition = "(-" + (fadingClip.length - fadingSource.time).ToString("N2") + " secs)";
				}
			}
			
			EditorGUILayout.BeginHorizontal();
			GUILayout.Label("Fading Clip:");
			GUILayout.Space(7);
			GUILayout.Label(fadingClipName  + "  " + fadingClipPosition);
			GUILayout.FlexibleSpace();
			
			EditorGUILayout.EndHorizontal();
			GUI.backgroundColor = Color.white;
			GUI.color = Color.cyan;
			
			
			GUIHelper.JukeboxButtons buttonPressed = GUIHelper.JukeboxButtons.None;
			
			EditorGUILayout.BeginHorizontal(EditorStyles.toolbarButton);
			buttonPressed = GUIHelper.AddJukeboxIcons(sounds);
			if (playingSource != null) {
				var oldtime = playingSource.time;
				var newTime = EditorGUILayout.Slider("", oldtime, 0f, clip.length);
				if (oldtime != newTime) {
					playingSource.time = newTime;
				}
			}
			
			EditorGUILayout.EndHorizontal();
			
			EditorGUILayout.EndVertical();
			
			GUI.color = Color.white;
			GUI.backgroundColor = Color.white;
			EditorGUILayout.Separator();
			
			switch (buttonPressed) {
			case GUIHelper.JukeboxButtons.Stop:	
				pl.StopPlaylist();
				break;
			case GUIHelper.JukeboxButtons.NextSong:	
				pl.PlayNextSong();	
				break;
			case GUIHelper.JukeboxButtons.Pause:	
				pl.PausePlaylist();
				break;
			case GUIHelper.JukeboxButtons.Play:	
				if (!pl.ResumePlaylist()) {
					if (pl.CurrentPlaylist != null) {
						pl.ChangePlaylist(pl.CurrentPlaylist.playlistName);
					}
				}
				break;
			case GUIHelper.JukeboxButtons.RandomSong:	
				pl.PlayRandomSong();
				break;
			}
		}
	}
	
	private void BusMuteSoloReset() {
		var allObjects = new List<UnityEngine.Object>();
		allObjects.Add(sounds);
		foreach (var g in groups) {
			allObjects.Add(g as UnityEngine.Object);
		}
		
		UndoHelper.RecordObjectsForUndo(allObjects.ToArray(), "Mute/Solo Reset");
		
		//reset everything
		GroupBus aBus = null;
		
		for (var l = 0; l < sounds.groupBuses.Count; l++) {
			aBus = sounds.groupBuses[l];
			
			if (Application.isPlaying) {
				MasterAudio.UnsoloBus(aBus.busName);
				MasterAudio.UnmuteBus(aBus.busName);
			} else {
				aBus.isSoloed = false;
				aBus.isMuted = false;
			}
		}
		
		foreach (var gr in groups) {
			if (Application.isPlaying) {
				MasterAudio.UnsoloGroup(gr.name);
				MasterAudio.UnmuteGroup(gr.name);
			} else {
				gr.isSoloed = false;
				gr.isMuted = false;
			}
		}
	}
	
	private void EqualizePlaylistVolumes(List<MusicSetting> playlistClips) {
		var clips = new Dictionary<MusicSetting, float>();
		
		if (playlistClips.Count < 2) {
			GUIHelper.ShowAlert("You must have at least 2 clips in a Playlist to use this function.");
			return;
		}
		
		float lowestVolume = 1f;
		
        for (var i = 0; i < playlistClips.Count; i++) {
			var setting = playlistClips[i];
			var ac = setting.clip;
			
			if (ac == null) {
				continue;
			}
			
			float average = 0f;
            var buffer = new float[ac.samples];

			Debug.Log("Measuring amplitude of '" + ac.name + "'.");
			
			try {  
				ac.GetData(buffer, 0);
			}
			catch {   
				Debug.Log("Could not read data from compressed sample. Skipping '" + setting.clip.name + "'.");
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
			GUIHelper.ShowAlert("You must have at least 2 clips in a Playlist to use this function.");
			return;
		}
		
		foreach (var kv in clips) { 
			float adjustedVol = lowestVolume / kv.Value;
			//set your volume for each song in your playlist.
			kv.Key.volume = adjustedVol;
		}
	}
}