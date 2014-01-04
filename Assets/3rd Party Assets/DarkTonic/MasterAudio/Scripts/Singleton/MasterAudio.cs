using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MasterAudio : MonoBehaviour {
	public const string GIZMO_FILE_NAME = "MasterAudio Icon.png";
	public const int HARD_CODED_BUS_OPTIONS = 2;
	public const string ALL_BUSES_NAME = "[All]";
	public const string NO_GROUP_NAME = "[None]";
	public const string DYNAMIC_GROUP_NAME = "[Type In]";
	public const string NO_PLAYLIST_NAME = "[No Playlist]";
	public const string NO_VOICE_LIMIT_NAME = "[NO LMT]";
	public const string ONLY_PLAYLIST_CONTROLLER_NAME = "~only~";
	public const float INNER_LOOP_CHECK_INTERVAL = .1f;
	
	#region public variables
	public SoundGroupVariation.AudioLocation bulkLocationMode = SoundGroupVariation.AudioLocation.Clip;
	public DragGroupMode curDragGroupMode = DragGroupMode.OneGroupPerClip;
	
	public Texture[] ledTextures;
	public Texture stopTrackTexture;
	public Texture nextTrackTexture;
	public Texture playTrackTexture;
	public Texture pauseTrackTexure;
	public Texture randomTrackTexure; 
	
	public Texture logoTexture;
	public Texture deleteTexture;
	public Texture settingsTexture;
	public Texture soloOnTexture;
	public Texture soloOffTexture;
	public Texture muteOnTexture;
	public Texture muteOffTexture;
	public Texture playTexture; 
	
	public bool mixerMuted = false;
	public bool playlistsMuted = false;
	
	public string busFilter = string.Empty;
	public bool resourceClipsPauseDoNotUnload = false;
	public Transform playlistControllerPrefab;
	public bool persistBetweenScenes = false;
	public bool areGroupsExpanded = true;
	public Transform soundGroupTemplate;
	public Transform soundGroupVariationTemplate;
	public List<GroupBus> groupBuses = new List<GroupBus>();
	public bool groupByBus = true;
	
	public bool playListExpanded = true;
	public bool playlistEditorExpanded = true;
	
	public List<Playlist> musicPlaylists = new List<Playlist>() {
		new Playlist()
	}; 
	
	public float masterAudioVolume = 1.0f;
	public bool LogSounds = false;
	public bool showMusicDucking = false;
	public bool enableMusicDucking = true;
	public List<DuckGroupInfo> musicDuckingSounds = new List<DuckGroupInfo>(); 
	public float duckedVolumeMultiplier = .5f;
	public float crossFadeTime = 1f;
	public float masterPlaylistVolume = 1f;
	
	private Transform trans;
	private bool soundsLoaded;
	#endregion
	 
	#region private variables

	#if UNITY_WP8
		private static SortedDictionary<string, AudioGroupInfo> audioSourcesBySoundType = new SortedDictionary<string, AudioGroupInfo>();
	#else
		private static SortedList<string, AudioGroupInfo> audioSourcesBySoundType = new SortedList<string, AudioGroupInfo>();
	#endif
	
	private static Dictionary<string, List<int>> randomizer = new Dictionary<string, List<int>>();
	private static List<MasterAudioGroup> soloedGroups = new List<MasterAudioGroup>();
	private static List<BusFadeInfo> busFades = new List<BusFadeInfo>();
	private static List<GroupFadeInfo> groupFades = new List<GroupFadeInfo>();
	
	private static Dictionary<string, PlaylistController> playlistControllersByName = new Dictionary<string, PlaylistController>();
	private static Dictionary<string, SoundGroupRefillInfo> lastTimeSoundGroupPlayed = new Dictionary<string, SoundGroupRefillInfo>();
	private static MasterAudio _instance;
	private static AudioSource _previewerInstance;
	private static bool appIsShuttingDown;
	#endregion
	
	#region Master Audio enums
	public enum BusCommand {
		None,
		FadeToVolume,
		Mute,
		Pause,
		Solo,
		Unmute,
		Unpause,
		Unsolo,
		Stop
	}
	
	public enum SoundGroupCommand {
		None,
		FadeToVolume,
		FadeOutAllOfSound,
		Mute, 
		Pause,
		Solo,
		StopAllOfSound,
		Unmute,
		Unpause,
		Unsolo
	}
	
	public enum PlaylistCommand {
		None,
		ChangePlaylist, // by name
		FadeToVolume,
		PlayClip, // by name
		PlayRandomSong,
		PlayNextSong,
		Pause,
		Resume,
		Stop
	}
	
	public enum EventSoundFunctionType {
		PlaySound,
		GroupControl,
		BusControl,
		PlaylistControl
	}
	
	public enum DragGroupMode {
		OneGroupPerClip,
		OneGroupWithVariations
	}
	
	public enum SoundSpawnLocationMode {
		MasterAudioLocation,
		CallerLocation,
		AttachToCaller
	}
	
	public enum SongFadeInPosition {
		NewClipFromBeginning = 1,
		NewClipFromLastKnownPosition = 3,
		SynchronizeClips = 5,
	}
	#endregion
	
	#region inner classes
	[Serializable]
	public class SoundGroupRefillInfo {
		public float _lastTimePlayed;
		public float _inactivePeriodSeconds;

		public SoundGroupRefillInfo(float lastTimePlayed, float inactivePeriodSeconds) {
			_lastTimePlayed = lastTimePlayed;
			_inactivePeriodSeconds = inactivePeriodSeconds;
		}
	}

	[Serializable]
	public class AudioGroupInfo {
		public List<AudioInfo> _sources;
		public int _lastFramePlayed;
		public float _lastTimePlayed;
		public MasterAudioGroup _group;
		
		public AudioGroupInfo(List<AudioInfo> sources, MasterAudioGroup groupScript) {
			this._sources = sources;
			this._lastFramePlayed = -50;
			this._lastTimePlayed = -50; 
			this._group = groupScript;
		}
	}
	
	[Serializable]
	public class Playlist {
		public bool isExpanded = true;
		public string playlistName = "new playlist";
		public SongFadeInPosition songTransitionType = SongFadeInPosition.NewClipFromBeginning;
		public List<MusicSetting> MusicSettings;
		
		public Playlist() {
			MusicSettings = new List<MusicSetting>() {
				new MusicSetting() {
					pitch = 1f,
					volume = 1
				}
			};
		}
	}
	
	[Serializable]
	public class AudioInfo {
		public AudioSource source;
		public float originalVolume;
		public float lastPercentageVolume;
		public float lastRandomVolume;
		public SoundGroupVariation variation;
		
		public AudioInfo(SoundGroupVariation _variation, AudioSource _source, float _origVol) {
			this.variation = _variation;
			this.source = _source;
			this.originalVolume = _origVol;
			this.lastPercentageVolume = 1f;
			this.lastRandomVolume = 0f;
		}
	}
	#endregion
	
	#region MonoDevelop events and Helpers
	void Awake() {
		this.useGUILayout = false;
		trans = this.transform;
		soundsLoaded = false;
		
		var aud = this.audio;
		if (aud != null) {
			// delete the previewer
			GameObject.Destroy(aud);
		}
		
		audioSourcesBySoundType.Clear();		
		playlistControllersByName.Clear();
		lastTimeSoundGroupPlayed.Clear();

		var plNames = new List<string>();
		
		PlaylistController.Instances = null; // clear the cache
		var playlists = PlaylistController.Instances;
		for (var i = 0; i < playlists.Count; i++) {
			var aList = playlists[i];
			
			if (plNames.Contains(aList.name)) {
				Debug.LogError("You have more than 1 Playlist Controller with the name '" + aList.name + "'. You must name them all uniquely. Ducking and other functions will not function properly until you fix this.");
				continue;
			} else {
				plNames.Add(aList.name);
			}
			
			playlistControllersByName.Add(aList.name, aList);
			if (persistBetweenScenes) {
				DontDestroyOnLoad(aList);
			}
		}
		
		// start up Objects!
		if (persistBetweenScenes) {
			DontDestroyOnLoad(this.gameObject);
		}
		
		Transform parentGroup;
		List<AudioInfo> sources;
		
		var playedStatuses = new List<int>();
		AudioSource source;
		AudioGroupInfo _group;
		MasterAudioGroup groupScript;
		randomizer = new Dictionary<string, List<int>>();
		string soundType = string.Empty;
		
		for (var k = 0; k < trans.childCount; k++) {
			parentGroup = trans.GetChild(k);
			
			sources = new List<AudioInfo>();
			
			groupScript = parentGroup.GetComponent<MasterAudioGroup>();
			
			if (groupScript == null) {
				Debug.LogError("MasterAudio could not find 'MasterAudioGroup' script for group '" + parentGroup.name + "'. Skipping this group.");
				continue;
			}

			soundType = parentGroup.name;
			var newWeightedChildren = new List<Transform>();
			
			SoundGroupVariation variation = null;
			SoundGroupVariation childVariation = null;
			Transform child = null;
			
			AudioLowPassFilter lp = null;
			AudioHighPassFilter hp = null;
			AudioEchoFilter echo = null;
			AudioDistortionFilter dist = null;
			AudioReverbFilter reverb = null;
			AudioChorusFilter chorus = null;
			
			for (int i = 0; i < parentGroup.childCount; i++) {
				child = parentGroup.GetChild(i);
				variation = child.GetComponent<SoundGroupVariation>();
				source = child.audio;
				
				var weight = variation.weight;
				
				for (var j = 0; j < weight; j++) {
					if (j > 0) {
						var extraChild = (GameObject) GameObject.Instantiate(soundGroupVariationTemplate.gameObject, parentGroup.transform.position, Quaternion.identity);
						childVariation = extraChild.GetComponent<SoundGroupVariation>();
						
						childVariation.audLocation = variation.audLocation;
						childVariation.resourceFileName = variation.resourceFileName;
						extraChild.name = child.name;
						extraChild.audio.clip = source.audio.clip;
						extraChild.audio.pitch = source.audio.pitch;
						extraChild.audio.volume = source.volume;
						extraChild.audio.loop = source.audio.loop;
						
						extraChild.audio.mute = source.audio.mute;
						extraChild.audio.bypassEffects = source.audio.bypassEffects;
						
						#if UNITY_3_0 || UNITY_3_1 || UNITY_3_2 || UNITY_3_3 || UNITY_3_4 || UNITY_3_5
						// unsupported
						#else
						extraChild.audio.bypassListenerEffects = source.audio.bypassListenerEffects;
						extraChild.audio.bypassReverbZones = source.audio.bypassReverbZones;
						#endif
						
						extraChild.audio.playOnAwake = source.audio.playOnAwake;
						extraChild.audio.priority = source.audio.priority;
						
						extraChild.audio.dopplerLevel = source.audio.dopplerLevel;
						extraChild.audio.rolloffMode = source.audio.rolloffMode;
						extraChild.audio.minDistance = source.audio.minDistance;
						extraChild.audio.panLevel = source.audio.panLevel;
						extraChild.audio.pan = source.audio.pan;
						extraChild.audio.spread = source.audio.spread;
						extraChild.audio.maxDistance = source.audio.maxDistance;
						
						childVariation.randomPitch = variation.randomPitch;
						childVariation.randomVolume = variation.randomVolume;
						childVariation.useFades = variation.useFades;
						childVariation.fadeInTime = variation.fadeInTime;
						childVariation.fadeOutTime = variation.fadeOutTime;
						childVariation.fxTailTime = variation.fxTailTime;
						
						// Clone fx
						if (lp != null && lp.enabled) {
							var newLP = extraChild.GetComponent<AudioLowPassFilter>();
							if (newLP == null) {
								extraChild.AddComponent<AudioLowPassFilter>();
								newLP = extraChild.GetComponent<AudioLowPassFilter>();
							}
							newLP.enabled = true;
							newLP.lowpassResonaceQ = lp.lowpassResonaceQ;
							newLP.cutoffFrequency = lp.cutoffFrequency;
						}
						
						if (hp != null && hp.enabled) {
							var newHP = extraChild.GetComponent<AudioHighPassFilter>();
							if (newHP == null) {
								extraChild.AddComponent<AudioHighPassFilter>();
								newHP = extraChild.GetComponent<AudioHighPassFilter>();
							}
							newHP.enabled = true;
							newHP.highpassResonaceQ = hp.highpassResonaceQ;
							newHP.cutoffFrequency = hp.cutoffFrequency;
						}
						
						if (echo != null && echo.enabled) {
							var newEcho = extraChild.GetComponent<AudioEchoFilter>();
							if (newEcho == null) {
								extraChild.AddComponent<AudioEchoFilter>();
								newEcho = extraChild.GetComponent<AudioEchoFilter>();
							}
							newEcho.enabled = true;
							newEcho.delay = echo.delay;
							newEcho.decayRatio = echo.decayRatio;
							newEcho.wetMix = echo.wetMix;
							newEcho.dryMix = echo.dryMix;
						}
						
						if (dist != null && dist.enabled) {
							var newDist = extraChild.GetComponent<AudioDistortionFilter>();
							if (newDist == null) {
								extraChild.AddComponent<AudioDistortionFilter>();
								newDist = extraChild.GetComponent<AudioDistortionFilter>();
							}
							newDist.enabled = true;
							newDist.distortionLevel = dist.distortionLevel;
						}
						
						if (reverb != null && reverb.enabled) {
							var newVerb = extraChild.GetComponent<AudioReverbFilter>();
							if (newVerb == null) {
								extraChild.AddComponent<AudioReverbFilter>();
								newVerb = extraChild.GetComponent<AudioReverbFilter>();
							}
							newVerb.enabled = true;
							newVerb.reverbPreset = reverb.reverbPreset;
							newVerb.dryLevel = reverb.dryLevel;
							newVerb.room = reverb.room;
							newVerb.roomHF = reverb.roomHF;
							newVerb.roomLF = reverb.roomLF;
							newVerb.decayTime = reverb.decayTime;
							newVerb.decayHFRatio = reverb.decayHFRatio;
							newVerb.reflectionsLevel = reverb.reflectionsLevel;
							newVerb.reflectionsDelay = reverb.reflectionsDelay;
							newVerb.reverbLevel = reverb.reverbLevel;
							newVerb.reverbDelay = reverb.reverbDelay;
							newVerb.hfReference = reverb.hfReference;
							newVerb.lFReference = reverb.lFReference;
							newVerb.diffusion = reverb.diffusion;
							newVerb.density = reverb.density;
						}
						
						if (chorus != null && chorus.enabled) {
							var newChorus = extraChild.GetComponent<AudioChorusFilter>();
							if (newChorus == null) {
								extraChild.AddComponent<AudioChorusFilter>();
								newChorus = extraChild.GetComponent<AudioChorusFilter>();
							}
							newChorus.enabled = true;
							newChorus.dryMix = chorus.dryMix;
							newChorus.wetMix1 = chorus.wetMix1;
							newChorus.wetMix2 = chorus.wetMix2;
							newChorus.wetMix3 = chorus.wetMix3;
							newChorus.delay = chorus.delay;
							newChorus.rate = chorus.rate;
							newChorus.depth = chorus.depth;
						}
						
						newWeightedChildren.Add(extraChild.transform);
						source = extraChild.audio;
						
						sources.Add(new AudioInfo(childVariation, source, source.volume)); 
						
						if (childVariation.audLocation == SoundGroupVariation.AudioLocation.ResourceFile) {
							AudioResourceOptimizer.AddTargetForClip(childVariation.resourceFileName, source);	
						}
					} else {
						sources.Add(new AudioInfo(variation, source, source.volume));
						
						if (variation.audLocation == SoundGroupVariation.AudioLocation.ResourceFile) {
							AudioResourceOptimizer.AddTargetForClip(variation.resourceFileName, source);	
						}
						
						lp = child.GetComponent<AudioLowPassFilter>();
						hp = child.GetComponent<AudioHighPassFilter>();
						echo = child.GetComponent<AudioEchoFilter>();
						dist = child.GetComponent<AudioDistortionFilter>();
						reverb = child.GetComponent<AudioReverbFilter>();
						chorus = child.GetComponent<AudioChorusFilter>();
					}
				}
			}
			
			// attach extra children from weight property.
			for (var i = 0; i < newWeightedChildren.Count; i++) {
				newWeightedChildren[i].parent = parentGroup;
			}
			
			_group = new AudioGroupInfo(sources, groupScript);
			if (groupScript.isSoloed) {
				soloedGroups.Add(groupScript);
			}
			
			if (audioSourcesBySoundType.ContainsKey(soundType)) {
				Debug.LogError("You have more than one SoundGroup named '" + soundType + "'. Ignoring the 2nd one. Please rename it.");
				continue;
			}
			
			_group._sources.Sort(delegate(AudioInfo x, AudioInfo y) {
				return x.variation.name.CompareTo(y.variation.name);	
			});
			
			audioSourcesBySoundType.Add(soundType, _group);
			
			if (sources.Count > 1) {
				for (var i = 0; i < sources.Count; i++) {
					playedStatuses.Add(i);
				}
				
				randomizer.Add(soundType, playedStatuses);
			}
			
			playedStatuses = new List<int>();
		}
		
		busFades.Clear();
		groupFades.Clear();
		
		Playlist pl = null;
		float firstSongLength = 0f;
		
		// check Syncrhonized Playlists for problems!
		for (var i = 0; i < musicPlaylists.Count; i++) {
			pl = musicPlaylists[i];
			if (pl.songTransitionType != SongFadeInPosition.SynchronizeClips) {
				continue;
			}
			
			if (pl.MusicSettings.Count < 2) {
				continue;
			}
			
			var songOne = pl.MusicSettings[0].clip;
			if (songOne == null) {
				continue;
			}
			
			firstSongLength = songOne.length;
			for (var s = 1; s < pl.MusicSettings.Count; s++) {
				var clip = pl.MusicSettings[s].clip;
				if (clip == null) {
					continue;
				}
				
				if (clip.length != firstSongLength) {
					Debug.LogError("Playlist '" + pl.playlistName + "' is marked as Synchronized but the clip lengths are not all the same within the Playlist. Clips may not synchronize as intended.");
					break;
				}
			}
		}
		
		soundsLoaded = true;
	}
	
	void Start () {
		// wait for Playlist Controller to initialize!
		if (musicPlaylists.Count > 0 
			&& musicPlaylists[0].MusicSettings != null 
			&& musicPlaylists[0].MusicSettings.Count > 0
			&&musicPlaylists[0].MusicSettings[0].clip != null
			&&  playlistControllersByName.Count == 0) {
			
			Debug.Log("No Playlist Controllers exist in the Scene. Music will not play.");
		}
		
		StartCoroutine(this.CoStart());
	}
	
	IEnumerator CoStart() {
		while (true) {
			yield return StartCoroutine(this.CoUpdate());
		}
	}
	
	IEnumerator CoUpdate() {
		yield return new WaitForSeconds(INNER_LOOP_CHECK_INTERVAL);
		
		// adjust for Inspector realtime slider.
		PerformBusFades();
		PerformGroupFades();
		RefillInactiveGroupPools();
	}

	private static void UpdateRefillTime(string sType, float inactivePeriodSeconds) {
		if (!lastTimeSoundGroupPlayed.ContainsKey(sType)) {
			lastTimeSoundGroupPlayed.Add(sType, new SoundGroupRefillInfo(Time.time, inactivePeriodSeconds));
		} else {
			lastTimeSoundGroupPlayed[sType]._lastTimePlayed = Time.time;
		}
	}

	private void RefillInactiveGroupPools() {
		var groups = lastTimeSoundGroupPlayed.GetEnumerator();

		var groupsToRemove = new List<string>();

		while (groups.MoveNext()) {
			var grp = groups.Current;
			if (grp.Value._lastTimePlayed + grp.Value._inactivePeriodSeconds < Time.time) {
				RefillSoundGroupPool(grp.Key);
				groupsToRemove.Add(grp.Key);
			}
		}

		for (var i = 0; i < groupsToRemove.Count; i++) {
			lastTimeSoundGroupPlayed.Remove(groupsToRemove[i]);
		}
	}

	private void PerformBusFades() {
		BusFadeInfo aFader = null;
		GroupBus aBus = null;
		for (var i = 0; i < busFades.Count; i++) {
			aFader = busFades[i];
			if (!aFader.IsActive) {
				continue;
			}
			
			aBus = GrabBusByName(aFader.BusName);
			if (aBus == null) {
				Debug.Log("Could not find bus named '" + aFader.BusName + "' to fade it one step.");
				aFader.IsActive = false;
				continue;
			}
			
			var newVolume = aBus.volume + aFader.VolumeStep;
			
			if (aFader.VolumeStep > 0f) {
				newVolume = Math.Min(newVolume, aFader.TargetVolume);	
			} else {
				newVolume = Math.Max(newVolume, aFader.TargetVolume);	
			}
			
			SetBusVolumeByName(aBus.busName, newVolume);
			
			if (newVolume == aFader.TargetVolume) {
				aFader.IsActive = false;
			}
		}
		
		busFades.RemoveAll(delegate(BusFadeInfo obj) {
			return obj.IsActive == false;
		});
	}
	
	private void PerformGroupFades() {
		GroupFadeInfo aFader = null;
		MasterAudioGroup aGroup = null;
		for (var i = 0; i < groupFades.Count; i++) {
			aFader = groupFades[i];
			if (!aFader.IsActive) {
				continue;
			}
			
			aGroup = GrabGroup(aFader.GroupName);
			if (aGroup == null) {
				Debug.Log("Could not find Sound Group named '" + aFader.GroupName + "' to fade it one step.");
				aFader.IsActive = false;
				continue;
			}
			
			var newVolume = aGroup.groupMasterVolume + aFader.VolumeStep;
			
			if (aFader.VolumeStep > 0f) {
				newVolume = Math.Min(newVolume, aFader.TargetVolume);	
			} else {
				newVolume = Math.Max(newVolume, aFader.TargetVolume);	
			}
			
			SetGroupVolume(aGroup.name, newVolume);
			
			if (newVolume == aFader.TargetVolume) {
				aFader.IsActive = false;
			}
		}
		
		groupFades.RemoveAll(delegate(GroupFadeInfo obj) {
			return obj.IsActive == false;
		});
	}
	
	void OnApplicationQuit() {
		AppIsShuttingDown = true; // very important!! Dont' take this out, false debug info may show up when you stop the Player
	}
	
	#endregion
	
	#region Sound Playing / Stopping Methods
	/// <summary>
	/// This method allows you to play a sound in a Sound Group in a "2D sound" manner. 
	/// </summary>
	/// <param name="sType">The name of the Sound Group to trigger a sound from.</param>
	/// <param name="volumePercentage"><b>Optional</b> - used if you want to play the sound at a reduced volume (between 0 and 1).</param>
	/// <param name="pitch"><b>Optional</b> - used if you want to play the sound at a specific pitch. If you do, it will override the pich and random pitch in the variation.</param>
	/// <param name="delaySoundTime"><b>Optional</b> - used if you want to play the sound X seconds from now instead of immediately.</param>
	/// <param name="variationName"><b>Optional</b> - used if you want to play a specific variation by name. Otherwise a random variation is played.</param>
	/// <returns>PlaySoundResult - this object can be used to be notified when the clip is done playing and whether the sound successfully started playing or not.</returns>
	public static PlaySoundResult PlaySound(string sType, float volumePercentage = 1f, float? pitch = null, float delaySoundTime = 0f, string variationName = null) {	
		if (!SceneHasMasterAudio) {
			return new PlaySoundResult() {
				ActingVariation = null,
				SoundPlayed = false,
				SoundScheduled = false
			};
		}
		
		return PlaySound3D(sType, null, false, volumePercentage, pitch, delaySoundTime, variationName);
	}

	/// <summary>
	/// This method allows you to play a sound in a Sound Group from a specific position.
	/// </summary>
	/// <param name="sType">The name of the Sound Group to trigger a sound from.</param>
	/// <param name="sourcePosition">The position you want the sound to eminate from. Required.</param>
	/// <param name="volumePercentage"><b>Optional</b> - used if you want to play the sound at a reduced volume (between 0 and 1).</param>
	/// <param name="pitch"><b>Optional</b> - used if you want to play the sound at a specific pitch. If you do, it will override the pich and random pitch in the variation.</param>
	/// <param name="delaySoundTime"><b>Optional</b> - used if you want to play the sound X seconds from now instead of immediately.</param>
	/// <param name="variationName"><b>Optional</b> - used if you want to play a specific variation by name. Otherwise a random variation is played.</param>
	/// <returns>PlaySoundResult - this object can be used to be notified when the clip is done playing and whether the sound successfully started playing or not.</returns>
	public static PlaySoundResult PlaySound3D(string sType, Vector3 sourcePosition, float volumePercentage = 1f, float? pitch = null, float delaySoundTime = 0f, string variationName = null)
	{
		if (!SceneHasMasterAudio) {
			return new PlaySoundResult() {
				ActingVariation = null,
				SoundPlayed = false,
				SoundScheduled = false
			};
		}
		
		if (!SoundsReady) {
			Debug.LogError ("MasterAudio not finished initializing sounds. Cannot play: " + sType);
			return new PlaySoundResult();
		}
		
		return PlaySoundAtVolume(sType, volumePercentage, sourcePosition, pitch, null, variationName, false, delaySoundTime, true);
	}
	
	/// <summary>
	/// This method allows you to play a sound in a Sound Group from a specific position.
	/// </summary>
	/// <param name="sType">The name of the Sound Group to trigger a sound from.</param>
	/// <param name="sourceTrans">The Transform whose position you want the sound to eminate from. Pass null if you want to play the sound 2D.</param>
	/// <param name="attachToSource"><b>Optional</b> - defaults to False. If you specify true, and also passed a non-null value for sourceTrans, the Sound Variation will be attached to the sourceTrans object so that the sound can follow it.</param>
	/// <param name="volumePercentage"><b>Optional</b> - used if you want to play the sound at a reduced volume (between 0 and 1).</param>
	/// <param name="pitch"><b>Optional</b> - used if you want to play the sound at a specific pitch. If you do, it will override the pich and random pitch in the variation.</param>
	/// <param name="delaySoundTime"><b>Optional</b> - used if you want to play the sound X seconds from now instead of immediately.</param>
	/// <param name="variationName"><b>Optional</b> - used if you want to play a specific variation by name. Otherwise a random variation is played.</param>
	/// <returns>PlaySoundResult - this object can be used to be notified when the clip is done playing and whether the sound successfully started playing or not.</returns>
	public static PlaySoundResult PlaySound3D(string sType, Transform sourceTrans = null, bool attachToSource = false, float volumePercentage = 1f, float? pitch = null, float delaySoundTime = 0f, string variationName = null)
	{
		if (!SceneHasMasterAudio) {
			return new PlaySoundResult() {
				ActingVariation = null,
				SoundPlayed = false,
				SoundScheduled = false
			};
		}
		
		if (!SoundsReady) {
			Debug.LogError ("MasterAudio not finished initializing sounds. Cannot play: " + sType);
			return new PlaySoundResult();
		}
		
		return PlaySoundAtVolume(sType, volumePercentage, Vector3.zero, pitch, sourceTrans, variationName, attachToSource, delaySoundTime, false);
	}
	
	#region PlaySound methods by index (int) - only advised if you have an enum defined and can convert it to an int and keep track of the numbers.
	/// <summary>
	/// This method allows you to play a sound in a Sound Group in a "2D sound" manner. 
	/// </summary>
	/// <param name="soundGroupIndex">The index of the Sound Group to trigger a sound from (they are alpha sorted by name).</param>
	/// <param name="volumePercentage"><b>Optional</b> - used if you want to play the sound at a reduced volume (between 0 and 1).</param>
	/// <param name="pitch"><b>Optional</b> - used if you want to play the sound at a specific pitch. If you do, it will override the pich and random pitch in the variation.</param>
	/// <param name="delaySoundTime"><b>Optional</b> - used if you want to play the sound X seconds from now instead of immediately.</param>
	/// <param name="variationName"><b>Optional</b> - used if you want to play a specific variation by name. Otherwise a random variation is played.</param>
	/// <returns>PlaySoundResult - this object can be used to be notified when the clip is done playing and whether the sound successfully started playing or not.</returns>
	public static PlaySoundResult PlaySound(int soundGroupIndex, float volumePercentage = 1f, float? pitch = null, float delaySoundTime = 0f, string variationName = null) {	
		if (!SceneHasMasterAudio) {
			return new PlaySoundResult() {
				ActingVariation = null,
				SoundPlayed = false,
				SoundScheduled = false
			};
		}
		
		return PlaySound3D(soundGroupIndex, null, false, volumePercentage, pitch, delaySoundTime, variationName);
	}
	
	/// <summary>
	/// This method allows you to play a sound in a Sound Group from a specific position.
	/// </summary>
	/// <param name="soundGroupIndex">The index of the Sound Group to trigger a sound from (they are alpha sorted by name).</param>
	/// <param name="sourcePosition">The position you want the sound to eminate from. Required.</param>
	/// <param name="volumePercentage"><b>Optional</b> - used if you want to play the sound at a reduced volume (between 0 and 1).</param>
	/// <param name="pitch"><b>Optional</b> - used if you want to play the sound at a specific pitch. If you do, it will override the pich and random pitch in the variation.</param>
	/// <param name="delaySoundTime"><b>Optional</b> - used if you want to play the sound X seconds from now instead of immediately.</param>
	/// <param name="variationName"><b>Optional</b> - used if you want to play a specific variation by name. Otherwise a random variation is played.</param>
	/// <returns>PlaySoundResult - this object can be used to be notified when the clip is done playing and whether the sound successfully started playing or not.</returns>
	public static PlaySoundResult PlaySound3D(int soundGroupIndex, Vector3 sourcePosition, float volumePercentage = 1f, float? pitch = null, float delaySoundTime = 0f, string variationName = null)
	{
		if (!SceneHasMasterAudio) {
			return new PlaySoundResult() {
				ActingVariation = null,
				SoundPlayed = false,
				SoundScheduled = false
			};
		}
		
		if (!SoundsReady) {
			Debug.LogError ("MasterAudio not finished initializing sounds. Cannot play soundGroupIndex: " + soundGroupIndex);
			return new PlaySoundResult();
		}
		
		if (soundGroupIndex > audioSourcesBySoundType.Keys.Count - 1) { 
			Debug.LogError("Master Audio only has " + audioSourcesBySoundType.Keys.Count + " Sound Groups. Can't play index: " + soundGroupIndex);
			return new PlaySoundResult();
		}

		#if UNITY_WP8
			var i = 0;
	        var sType = "";
	
	        foreach (var k in audioSourcesBySoundType.Keys) {
	           if (i == soundGroupIndex) {
	               sType = k;
	               break;
	           }
	           i++;
	        }
		#else
			var sType = audioSourcesBySoundType.Keys[soundGroupIndex];
		#endif
		
		return PlaySoundAtVolume(sType, volumePercentage, sourcePosition, pitch, null, variationName, false, delaySoundTime, true);
	}
	
	/// <summary>
	/// This method allows you to play a sound in a Sound Group from a specific position.
	/// </summary>
	/// <param name="soundGroupIndex">The index of the Sound Group to trigger a sound from (they are alpha sorted by name).</param>
	/// <param name="sourceTrans">The Transform whose position you want the sound to eminate from. Pass null if you want to play the sound 2D.</param>
	/// <param name="attachToSource"><b>Optional</b> - defaults to False. If you specify true, and also passed a non-null value for sourceTrans, the Sound Variation will be attached to the sourceTrans object so that the sound can follow it.</param>
	/// <param name="volumePercentage"><b>Optional</b> - used if you want to play the sound at a reduced volume (between 0 and 1).</param>
	/// <param name="pitch"><b>Optional</b> - used if you want to play the sound at a specific pitch. If you do, it will override the pich and random pitch in the variation.</param>
	/// <param name="delaySoundTime"><b>Optional</b> - used if you want to play the sound X seconds from now instead of immediately.</param>
	/// <param name="variationName"><b>Optional</b> - used if you want to play a specific variation by name. Otherwise a random variation is played.</param>
	/// <returns>PlaySoundResult - this object can be used to be notified when the clip is done playing and whether the sound successfully started playing or not.</returns>
	public static PlaySoundResult PlaySound3D(int soundGroupIndex, Transform sourceTrans = null, bool attachToSource = false, float volumePercentage = 1f, float? pitch = null, float delaySoundTime = 0f, string variationName = null)
	{
		if (!SceneHasMasterAudio) {
			return new PlaySoundResult() {
				ActingVariation = null,
				SoundPlayed = false,
				SoundScheduled = false
			};
		}
		
		if (!SoundsReady) {
			Debug.LogError ("MasterAudio not finished initializing sounds. Cannot play soundGroupIndex: " + soundGroupIndex);
			return new PlaySoundResult();
		}
		
		if (soundGroupIndex > audioSourcesBySoundType.Keys.Count - 1) { 
			Debug.LogError("Master Audio only has " + audioSourcesBySoundType.Keys.Count + " Sound Groups. Can't play index: " + soundGroupIndex);
			return new PlaySoundResult();
		}

		#if UNITY_WP8
			var i = 0;
	        var sType = "";
	
	        foreach (var k in audioSourcesBySoundType.Keys) {
	           if (i == soundGroupIndex) {
	               sType = k;
	               break;
	           }
	           i++;
	        }
		#else
			var sType = audioSourcesBySoundType.Keys[soundGroupIndex];
		#endif
			
		return PlaySoundAtVolume(sType, volumePercentage, Vector3.zero, pitch, sourceTrans, variationName, attachToSource, delaySoundTime, false);
	}
	
	#endregion
	
	private static PlaySoundResult PlaySoundAtVolume(string sType, float volumePercentage, Vector3 sourcePosition, float? pitch = null, Transform sourceTrans = null, string variationName = null, 
		bool attachToSource = false, float delaySoundTime = 0f, bool useVector3 = false) {
		
		var result = new PlaySoundResult();
		
		if (!SceneHasMasterAudio) { // No MA
			return result;
		}
		
		var ma = MasterAudio.Instance;
		
		if (!SoundsReady || sType == string.Empty || sType == NO_GROUP_NAME) {
			return result; // not awake yet
		}
		
		if (!audioSourcesBySoundType.ContainsKey(sType)) {
			var msg = "MasterAudio could not find sound: " + sType + ". If your Scene just changed, this could happen when an OnDisable or OnInvisible event sound happened to a per-scene sound, which is expected.";
			if (sourceTrans != null) {
				msg += " Triggered by prefab: " + (sourceTrans == null ? "Unknown" : sourceTrans.name);
			}
			
			LogMissingSoundFile(msg);
			return result;
		}
		
		AudioGroupInfo _group = audioSourcesBySoundType[sType];
		MasterAudioGroup maGroup = _group._group;
		
		if (MasterAudio.Instance.mixerMuted) {
			LogIfLoggingEnabled("MasterAudio skipped playing sound: " + sType + " because the Mixer is muted.", sType);
			return result;
		}
		if (maGroup.isMuted) {
			LogIfLoggingEnabled("MasterAudio skipped playing sound: " + sType + " because the Group is muted.", sType);
			return result;
		}
		if (soloedGroups.Count > 0 && !soloedGroups.Contains(maGroup)) {
			LogIfLoggingEnabled("MasterAudio skipped playing sound: " + sType + " because there are one or more Groups soloed. This one is not.", sType);
			return result;
		}
		
		switch (maGroup.limitMode) {
		case MasterAudioGroup.LimitMode.TimeBased:
			if (maGroup.minimumTimeBetween > 0) {
				if (Time.time < (_group._lastTimePlayed + maGroup.minimumTimeBetween)) {
					LogIfLoggingEnabled("MasterAudio skipped playing sound: " + sType + " due to Group's Min Seconds setting.", sType);
					return result;
				}
				
				_group._lastTimePlayed = Time.time;
			}
			break;
		case MasterAudioGroup.LimitMode.FrameBased:
			if (Time.frameCount - _group._lastFramePlayed < maGroup.limitPerXFrames) {
				LogIfLoggingEnabled("Master Audio skipped playing sound: " + sType + " due to Group's Per Frame Limit.", sType);
				return result;
			}
			
			_group._lastFramePlayed = Time.frameCount;
			
			break;
		case MasterAudioGroup.LimitMode.None:
			_group._lastTimePlayed = Time.time;
			_group._lastFramePlayed = Time.frameCount;
			break;
		}
		
		var sources = _group._sources;
		var isNonSpecific = string.IsNullOrEmpty(variationName);
		
		if (sources.Count == 0) {
			Debug.Log("Sound Group {" + sType + "} has no active variations."); 
			return result;
		}
		
		if (_group._group.limitPolyphony) {
			var maxVoices = _group._group.voiceLimitCount;
			var busyVoices = 0;
			for (var i = 0; i < _group._sources.Count; i++) {
				if (_group._sources[i].source == null) {
					continue;
				}
				if (!_group._sources[i].source.isPlaying) {
					continue;
				}
				
				busyVoices++;
				if (busyVoices >= maxVoices) {
					LogIfLoggingEnabled("Polyphony limit of group: " + _group._group.name + " exceeded. Will not play this sound for this instance.", sType);
					return result;
				}
			}
		}
		
		if (!_group._group.HasVoicesRemaining) {
			LogIfLoggingEnabled("Bus voice limit has been reached. Cannot play the sound: " + _group._group.name + " until one voice has stopped playing.", sType);
			return result;
		}

		AudioInfo randomSource = null; 

		if (sources.Count == 1) { 
			LogIfLoggingEnabled("Cueing only child of " + sType, sType);
			randomSource = sources[0];
		} 
		
		List<int> choices = null;
		int? randomIndex = null;
		var otherChoices = new List<int>();
		
		if (randomSource == null) { // we must get a non-busy random source!
			if (!randomizer.ContainsKey(sType)) {
				Debug.Log("Sound Group {" + sType + "} has no active variations."); 
				return result;
			}
			
			if (isNonSpecific) {
				choices = randomizer[sType];

				if (_group._group.curVariationSequence == MasterAudioGroup.VariationSequence.Randomized) {
					randomIndex = UnityEngine.Random.Range(0, choices.Count);
				} else {
					// top to bottom! 
					randomIndex = 0;
				}
				var pickedChoice = choices[randomIndex.Value];
				randomSource = sources[pickedChoice];
				
				// fill list with other random sources not used yet in case the first is busy.
				otherChoices.AddRange(choices.ToArray());
				otherChoices.Remove(pickedChoice);
				
				LogIfLoggingEnabled("Cueing child {" + choices[randomIndex.Value] + "} of " + sType, sType);
			} else {
				// find source by name
				var isFound = false;
				var matchesFound = 0;
				for (var i = 0; i < sources.Count; i++) {
					var aSource = sources[i];
					if (aSource.source.name != variationName) {
						continue;
					}
					
					matchesFound++;
					if (aSource.source.isPlaying) {
						continue;
					}
					
					randomSource = aSource;
					isFound = true;
					break;
				}
				
				if (!isFound) {
					if (matchesFound == 0) {
						LogMissingSoundFile("Can't find variation {" + variationName + "} of " + sType);
					}
					
					return result;
				}
			}
		}
		
		PlaySoundResult playedState = null;
		bool playedSound = false;
		
		do {
			playedState = PlaySoundIfAvailable(randomSource, sourcePosition, volumePercentage, pitch, _group, sourceTrans, attachToSource, delaySoundTime, useVector3);
			
			if (playedState.SoundPlayed || playedState.SoundScheduled) {
				playedSound = true;
	
				if (isNonSpecific && randomIndex.HasValue) { // only if successfully played!
					choices.RemoveAt(randomIndex.Value);			
					
					if (choices.Count == 0) {
						LogIfLoggingEnabled("Reloading sound: " + sType, sType);
						
						RefillSoundGroupPool(sType);
					}
				} 

				if (_group._group.curVariationSequence == MasterAudioGroup.VariationSequence.TopToBottom && _group._group.useInactivePeriodPoolRefill) {
					UpdateRefillTime(sType, _group._group.inactivePeriodSeconds);
				}
			} else if (isNonSpecific) {
				// try the other ones
				if (otherChoices.Count > 0) {
					randomSource = sources[otherChoices[0]];
					LogIfLoggingEnabled("Child was busy. Cueing child {" + sources[otherChoices[0]] + "} of " + sType, sType);
					otherChoices.RemoveAt(0);
				}
			}
		}
		while (!playedSound && otherChoices.Count > 0); // repeat until you've either played the sound or exhausted all possibilities.
		
		
		if (playedState.SoundPlayed) {
			// sound play worked! Duck music if a ducking sound.
			var matchingDuck = ma.musicDuckingSounds.Find(delegate(DuckGroupInfo obj) {
				return obj.soundType == sType;
			});
			
			if (ma.EnableMusicDucking && matchingDuck != null) {
				// duck music
				var duckLength = randomSource.source.audio.clip.length;
				var duckPitch = randomSource.source.pitch;
				
				var pcs = PlaylistController.Instances;
				for (var i = 0; i < pcs.Count; i++) {
					pcs[i].DuckMusicForTime(duckLength, duckPitch, matchingDuck.riseVolStart);
				}
				
				if (pcs.Count == 0) {
					Debug.LogWarning("Playlist Controller is not in the Scene. Cannot duck music.");
				}
			}
			
			return playedState;
		}
		
		if (!playedState.SoundScheduled) {
			LogIfLoggingEnabled("All children of " + sType + " were busy. Will not play this sound for this instance.", sType);
		}
		
		return playedState;
	}
	
	private static PlaySoundResult PlaySoundIfAvailable(AudioInfo info, Vector3 sourcePosition, float volumePercentage, float? pitch = null, AudioGroupInfo audioGroup = null, Transform sourceTrans = null, 
		bool attachToSource = false, float delaySoundTime = 0f, bool useVector3 = false) {
		
		var result = new PlaySoundResult();
		
		if (info.source == null) {
			// this avoids false errors when stopping the game (from became invisible event callers)
			return result;	
		}
		
		MasterAudioGroup maGroup = audioGroup._group;
		
		if (info.source.audio.isPlaying) {
			var playedPercentage = AudioUtil.GetAudioPlayedPercentage(info.source);
			var retriggerPercent = maGroup.retriggerPercentage;
			
			if (playedPercentage < retriggerPercent) {
				return result; // wait for this to stop playing or play further.
			}
		}
		
		info.variation.Stop(); 
		info.variation.ObjectToFollow = null;
		
		if (useVector3) {
			info.source.transform.position = sourcePosition;
		} else if (sourceTrans != null) {
			if (attachToSource) {
				info.variation.ObjectToFollow = sourceTrans;
			} else {
				info.source.transform.position = sourceTrans.position;
			}
		} else {
			info.source.transform.localPosition = Vector3.zero; // put it back in MA prefab position after being detached.
		}
		
		var groupVolume = maGroup.groupMasterVolume;
		var busVolume = GetBusVolume(maGroup);
		
		float calcVolume = info.originalVolume * groupVolume * busVolume * MasterAudio.Instance.masterAudioVolume;
		
		// set volume to percentage.
		float volume = calcVolume * volumePercentage;
		
		// random volume
		var randomVol = 0f;
		if (info.variation.randomVolume != 0f) {
			randomVol = UnityEngine.Random.Range(-info.variation.randomVolume, info.variation.randomVolume);
		}
		
		var targetVolume = volume + randomVol;
		
		info.source.audio.volume = targetVolume;
		
		// save these for on the fly adjustments afterward
		info.lastPercentageVolume = volumePercentage;
		info.lastRandomVolume = randomVol;
		
		bool isActive = false;
		
		#if UNITY_3_0 || UNITY_3_1 || UNITY_3_2 || UNITY_3_3 || UNITY_3_4 || UNITY_3_5
			isActive = info.source.audio.gameObject.active;
		#else
			isActive = info.source.audio.gameObject.activeInHierarchy;
		#endif
		
		if (!isActive) {
			return result;
		}
		
		result.ActingVariation = info.variation;
		
		if (delaySoundTime > 0f) {
			result.SoundScheduled = true;
		} else {
			result.SoundPlayed = true;
		}
		
		var playSoundParams = new SoundGroupVariation.PlaySoundParams(maGroup.name, 
              volumePercentage, 
              calcVolume,
              pitch, 
              sourceTrans, 
              attachToSource, 
              delaySoundTime, 
              maGroup.curVariationMode == MasterAudioGroup.VariationMode.LoopedChain);
		
		if (playSoundParams.isChainLoop) {
			// make sure there isn't 2 going, ever
			MasterAudio.StopAllOfSound(playSoundParams.soundType);
		}
		
		info.variation.Play(pitch, targetVolume, playSoundParams);
		
		return result;
	}
	
	/// <summary>
	/// This method allows you to abruptly stop all sounds in a specified Sound Group.
	/// </summary>
	/// <param name="sType">The name of the Sound Group.</param>
	public static void StopAllOfSound(string sType)
	{
		if (!SceneHasMasterAudio) { // No MA
			return;
		}
		
		if (!audioSourcesBySoundType.ContainsKey(sType)) {
			Debug.LogWarning("Could not locate group '" + sType + "'.");
			return;
		}
		
		var sources = audioSourcesBySoundType[sType]._sources;
		
		var _grp = GrabGroup(sType);
		
		var stopEndDetector = _grp != null && _grp.curVariationMode == MasterAudioGroup.VariationMode.LoopedChain;
		
		foreach (AudioInfo audio in sources) {
			audio.variation.Stop(stopEndDetector);
		}
	}
	
	/// <summary>
	/// This method allows you to fade out all sounds in a specified Sound Group for X seconds.
	/// </summary>
	/// <param name="sType">The name of the Sound Group.</param>
	/// <param name="fadeTime">The amount of seconds the fading will take.</param>
	public static void FadeOutAllOfSound(string sType, float fadeTime) {
		if (!SceneHasMasterAudio) { // No MA
			return;
		}
		
		if (!audioSourcesBySoundType.ContainsKey(sType)) {
			Debug.LogWarning("Could not locate group '" + sType + "'.");
			return;
		}
		
		var sources = audioSourcesBySoundType[sType]._sources;
		
		foreach (AudioInfo audio in sources) {
			audio.variation.FadeOutNow(fadeTime);
		}
	}	
	#endregion
	
	#region Particle methods
	/// <summary>
	/// This method will trigger particles from a Transform you specify.
	/// </summary>
	/// <param name="trans">The Transform whose ParticleSystem you will be using.</param>
	/// <param name="particleCount">The number of particles to spawn.</param>
	public static void TriggerParticleEmission(Transform trans, int particleCount) {
		ParticleSystem part = trans.GetComponent<ParticleSystem>();
		if (part == null) {
			return;
		}
		
		part.Emit(particleCount);
	}
	#endregion
	
	#region Variation methods
	/// <summary>
	/// This method will change the pitch of a variation or all variations in a Sound Group.
	/// </summary>
	/// <param name="sType">The name of the Sound Group.</param>
	/// <param name="changeAllVariations">Whether to change all variations in the Sound Group or just one.</param>
	/// <param name="variationName">Use this to specify a certain variation's name. Only that variation will be changes if you haven't passed changeAllVariations as true.</param>
	/// <param name="pitch">The new pitch of the variation.</param>
	public static void ChangeVariationPitch(string sType, bool changeAllVariations, string variationName, float pitch) {
		if (!SoundsReady) {
			Debug.LogError("MasterAudio not finished initializing sounds. Cannot create change variation clip yet.");
			return;
		}
		
		if (!audioSourcesBySoundType.ContainsKey(sType)) {
			Debug.LogWarning("Could not locate group '" + sType + "'.");
			return;
		}
		
		var grp = audioSourcesBySoundType[sType];
		
		var iChanged = 0;
		
		for (var i = 0; i < grp._sources.Count; i++) {
			var aVar = grp._sources[i];
			if (changeAllVariations || aVar.source.transform.name == variationName) {
				aVar.variation.original_Pitch = pitch;
				iChanged++;
			}
		}
		
		if (iChanged == 0 && !changeAllVariations) {
			Debug.Log("Could not find any matching variations of Sound Group '" + sType + "' to change the pitch of.");
		}
	}
	
	/// <summary>
	/// This method will change the Audio Clip used by a variation into one named from a Resource file.
	/// </summary>
	/// <param name="sType">The name of the Sound Group.</param>
	/// <param name="changeAllVariations">Whether to change all variations in the Sound Group or just one.</param>
	/// <param name="variationName">Use this to specify a certain variation's name. Only that variation will be changes if you haven't passed changeAllVariations as true.</param>
	/// <param name="resourceFileName">The name of the file in the Resource.</param>
	public static void ChangeVariationClipFromResources(string sType, bool changeAllVariations, string variationName, string resourceFileName) {
		if (!SoundsReady) {
			Debug.LogError("MasterAudio not finished initializing sounds. Cannot create change variation clip yet.");
			return;
		}
		
		var aClip = Resources.Load(resourceFileName) as AudioClip;
		
		if (aClip == null) {
			LogMissingSoundFile("Resource file '" + resourceFileName + "' could not be located.");
			return;
		}
		
		ChangeVariationClip(sType, changeAllVariations, variationName, aClip);
	}
	
	/// <summary>
	/// This method will change the Audio Clip used by a variation into one you specify.
	/// </summary>
	/// <param name="sType">The name of the Sound Group.</param>
	/// <param name="changeAllVariations">Whether to change all variations in the Sound Group or just one.</param>
	/// <param name="variationName">Use this to specify a certain variation's name. Only that variation will be changes if you haven't passed changeAllVariations as true.</param>
	/// <param name="clip">The Audio Clip to replace the old one with.</param>
	public static void ChangeVariationClip(string sType, bool changeAllVariations, string variationName, AudioClip clip) {
		if (!SoundsReady) {
			Debug.LogError("MasterAudio not finished initializing sounds. Cannot create change variation clip yet.");
			return;
		}
		
		if (!audioSourcesBySoundType.ContainsKey(sType)) {
			Debug.LogWarning("Could not locate group '" + sType + "'.");
			return;
		}
		
		var grp = audioSourcesBySoundType[sType];
		
		for (var i = 0; i < grp._sources.Count; i++) {
			var aVar = grp._sources[i];
			if (changeAllVariations || aVar.source.transform.name == variationName) {
				aVar.source.clip = clip;
			}
		}
	}
	
	#endregion	
	
	#region Sound Group methods
	/// <summary>
	/// This method allow you to refill the pool of the Variation sounds for a Sound Group. That way you don't have to wait for all remaining random (or top to bottom) sounds to be played before it refills.
	/// </summary>
	/// <param name="sType">The name of the Sound Group to refill the pool of.</param>
	public static void RefillSoundGroupPool(string sType) {
		var grp = GrabGroup(sType, false);
		if (grp == null) {
			return;
		}
		
		AudioGroupInfo _group = audioSourcesBySoundType[sType];
		var choices = randomizer[sType];
		
		choices.Clear();
		
		for (var i = 0; i < _group._sources.Count; i++) {
			choices.Add(i);
		}
	}
	
	/// <summary>
	/// This method allow you to check if a Sound Group exists.
	/// </summary>
	/// <param name="sType">The name of the Sound Group to check.</param>
	/// <returns>Whether or not the Sound Group exists.</returns>
	public static bool SoundGroupExists(string sType)
	{
		var aGroup = GrabGroup(sType, false);
		return aGroup != null;
	}

	/// <summary>
	/// This method will give you the index of a Sound Group. Returns -1 if it doesn't exist
	/// </summary>
	/// <param name="sType">The name of the Sound Group to check.</param>
	/// <returns>Returns the index of the SoundGroup if it exists, otherwise -1</returns>
	public static int GetSoundGroupIndex(string sType)
	{
		#if UNITY_WP8
			var i = -1;

			var counter = 0;

			foreach (var k in audioSourcesBySoundType.Keys) {
				if (k == sType) {
					i = counter;
					break;
				}
				counter++;
			}

			return i;
		#else
			return audioSourcesBySoundType.IndexOfKey(sType);
		#endif
	}
	
	/// <summary>
	/// This method allow you to pause all Audio Sources in a Sound Group.
	/// </summary>
	/// <param name="sType">The name of the Sound Group to pause.</param>
	public static void PauseSoundGroup(string sType) {
		var aGroup = GrabGroup(sType);
		
		if (aGroup == null) {
			return;
		}
		
		SoundGroupVariation aVar = null;
		
		var sources = audioSourcesBySoundType[sType]._sources;
		
		for (var i = 0; i < sources.Count; i++) {
			aVar = sources[i].variation;
			
			aVar.Pause();
		}
	}
	
	/// <summary>
	/// This method allow you to unpause all Audio Sources in a Sound Group.
	/// </summary>
	/// <param name="sType">The name of the Sound Group to unpause.</param>
	public static void UnpauseSoundGroup(string sType) {
		var aGroup = GrabGroup(sType);
		
		if (aGroup == null) {
			return;
		}
		
		SoundGroupVariation aVar = null;
		
		var sources = audioSourcesBySoundType[sType]._sources;
		
		for (var i = 0; i < sources.Count; i++) {
			aVar = sources[i].variation;
			
			if (aVar.audio.time <= 0f) {
				continue;
			}
			
			aVar.audio.Play();
		}
	}
	
	/// <summary>
	/// This method allow you to fade the volume of a Sound Group over X seconds.
	/// </summary>
	/// <param name="sType">The name of the Sound Group to fade.</param>
	/// <param name="newVolume">The target volume of the Sound Group.</param>
	/// <param name="fadeTime">The amount of time the fade will take.</param>
	public static void FadeSoundGroupToVolume(string sType, float newVolume, float fadeTime) {
		if (fadeTime <= INNER_LOOP_CHECK_INTERVAL) {
			SetGroupVolume(sType, newVolume); // time really short, just do it at once.
			return;
		}
		
		var aGroup = GrabGroup(sType);
		
		if (aGroup == null) {
			return;
		}
		
		if (newVolume < 0f || newVolume > 1f) {
			Debug.Log("Cannot fade Sound Group '" + sType + "'. Invalid volume specified. Volume should be between 0 and 1.");
			return;
		}
		
		// make sure no other group fades for this group are happenning.
		var matchingFade = groupFades.Find(delegate(GroupFadeInfo obj) {
			return obj.GroupName == sType;
		});
		
		if (matchingFade != null) {
			matchingFade.IsActive = false; // start with a new one, delete old.
		}
		
		var volStep = (newVolume - aGroup.groupMasterVolume) / (fadeTime / INNER_LOOP_CHECK_INTERVAL);
		
		var groupFade = new GroupFadeInfo() {
			GroupName = sType,
			VolumeStep = volStep,
			TargetVolume = newVolume
		};
		
		groupFades.Add(groupFade);
	}
	
	/// <summary>
	/// This method will delete a Sound Group, and all variations from the current Scene's Master Audio object.
	/// </summary>
	/// <param name="sType">The name of the Sound Group.</param>
	public static void RemoveSoundGroup(Transform groupTrans) {
		string sType = groupTrans.name;
		
		var ma = MasterAudio.Instance;
		
		var deadDuck = ma.musicDuckingSounds.Find(delegate(DuckGroupInfo obj) {
			return obj.soundType == sType;
		});
		
		if (deadDuck != null) {
			ma.musicDuckingSounds.Remove(deadDuck);
		}
		randomizer.Remove(sType);
		audioSourcesBySoundType.Remove(sType);
		lastTimeSoundGroupPlayed.Remove(sType);

		AudioSource aSource = null;
		SoundGroupVariation aVar = null;
		Transform aChild = null;
		
		// delete resource file pointers to Audio Sources being deleted
		for (var i = 0; i < groupTrans.childCount; i++) {
			aChild = groupTrans.GetChild(i);
			aSource = aChild.audio;
			aVar = aChild.GetComponent<SoundGroupVariation>();
			
			if (aVar.audLocation != SoundGroupVariation.AudioLocation.ResourceFile) {
				continue;
			}
			
			AudioResourceOptimizer.DeleteAudioSourceFromList(aVar.resourceFileName, aSource);
		}
		
		groupTrans.parent = null;
		GameObject.Destroy(groupTrans.gameObject);
	}
	
	/// <summary>
	/// This method will create a new Sound Group from the Audio Clips you pass in.
	/// </summary>
	/// <param name="gInfo">The object containing all variations and group info.</param>
	/// <param name="creatorObjectName">The name of the object creating this group (for debug).</param>
	/// <returns>Whether or not the Sound Group was created.</returns>
	public static Transform CreateNewSoundGroup(DynamicSoundGroupInfo gInfo, string creatorObjectName)
	{
		if (!SceneHasMasterAudio) {
			return null;
		}
		
		if (!SoundsReady) {
			Debug.LogError("MasterAudio not finished initializing sounds. Cannot create new group yet.");
			return null;
		}
		
		var groupName = gInfo.groupName;
		
		var ma = MasterAudio.Instance;
		
		if (MasterAudio.Instance.trans.FindChild(groupName) != null) {
			Debug.LogError("Cannot add a new Sound Group named '" + groupName + "' because there is already a Sound Group of that name.");
			return null;
		}
		
		GameObject newGroup = (GameObject) GameObject.Instantiate(
			ma.soundGroupTemplate.gameObject, ma.trans.position, Quaternion.identity);		
		
		var groupTrans = newGroup.transform;
		groupTrans.name = UtilStrings.TrimSpace(groupName);
		groupTrans.parent = MasterAudio.Instance.trans;
		
		SoundGroupVariation variation = null;
		DynamicSoundGroupVariation aVariation = null;
		AudioClip clip = null;
		
		for (var i = 0; i < gInfo.variations.Count; i++) {
			aVariation = gInfo.variations[i];
			
			for (var j = 0; j < aVariation.weight; j++) {
				GameObject newVariation = (GameObject) GameObject.Instantiate(ma.soundGroupVariationTemplate.gameObject, groupTrans.position, Quaternion.identity);		
				
				variation = newVariation.GetComponent<SoundGroupVariation>();
				
				var clipName = UtilStrings.TrimSpace(aVariation.clipName);
				
				switch (aVariation.audLocation) {
				case SoundGroupVariation.AudioLocation.Clip:
					clip = aVariation.clip;
					if (clip == null) {
						Debug.LogWarning("No clip specified in DynamicSoundGroupCreator '" + creatorObjectName + "' - skipping this item.");
						continue;
					}
					variation.audio.clip = clip;
					break;
				case SoundGroupVariation.AudioLocation.ResourceFile:
					AudioResourceOptimizer.AddTargetForClip(aVariation.resourceFileName, variation.audio);
					variation.audLocation = SoundGroupVariation.AudioLocation.ResourceFile;
					variation.resourceFileName = aVariation.resourceFileName;
					break;
				}
				
				variation.audio.volume = aVariation.volume;
				variation.audio.pitch = aVariation.pitch;
				variation.audio.loop = aVariation.loopClip;
				variation.original_Pitch = aVariation.pitch;
				
				// 3D settings
				if (aVariation.showAudio3DSettings) {
					variation.audio.dopplerLevel = aVariation.audDopplerLevel;
					variation.audio.rolloffMode = aVariation.audRollOffMode;
					variation.audio.minDistance = aVariation.audMinDistance;
					variation.audio.spread = aVariation.audSpread;
					variation.audio.maxDistance = aVariation.audMaxDistance;
				}
				
				variation.transform.name = clipName;
				variation.transform.parent = groupTrans;
				variation.randomPitch = aVariation.randomPitch;
				variation.randomVolume = aVariation.randomVolume;
				variation.useFades = aVariation.useFades;
				variation.fadeInTime = aVariation.fadeInTime;
				variation.fadeOutTime = aVariation.fadeOutTime;
			}
		}
		// added to Hierarchy!
		
		// populate sounds for playing!
		var groupScript = newGroup.GetComponent<MasterAudioGroup>();
		// populate other properties.
		groupScript.retriggerPercentage = gInfo.retriggerPercentage;
		groupScript.groupMasterVolume = gInfo.groupMasterVolume;
		groupScript.limitMode = gInfo.limitMode;
		groupScript.limitPerXFrames = gInfo.limitPerXFrames;
		groupScript.minimumTimeBetween = gInfo.minimumTimeBetween;
		groupScript.limitPolyphony = gInfo.limitPolyphony;
		groupScript.voiceLimitCount = gInfo.voiceLimitCount;
		
		var sources = new List<AudioInfo>();
		Transform aChild;
		AudioSource aSource;
		var playedStatuses = new List<int>();
		
		for (var i = 0; i < newGroup.transform.childCount; i++) {
			playedStatuses.Add(i);
			aChild = newGroup.transform.GetChild(i);
			aSource = aChild.GetComponent<AudioSource>();
			variation = aChild.GetComponent<SoundGroupVariation>();
			sources.Add(new AudioInfo(variation, aSource, aSource.volume));
		}
		
		audioSourcesBySoundType.Add(groupName, new AudioGroupInfo(sources, groupScript));
		
		// fill up randomizer
		randomizer.Add(groupName, playedStatuses);
		
		if (gInfo.duckSound) {
			AddSoundGroupToDuckList(gInfo.groupName);
		}
		
		switch (gInfo.busMode) {
		case DynamicSoundGroupInfo.BusMode.CreateNew:
			var index = GetBusIndex(gInfo.busName, false); // check if another Dynamic created this already.
			if (index < 0) {
				CreateBus(gInfo.busName);
				index = GetBusIndex(gInfo.busName, false);
			} 
			groupScript.busIndex = index;
			break;
		case DynamicSoundGroupInfo.BusMode.UseExisting:
			var busIndex = GetBusIndex(gInfo.busName, true);
			if (busIndex < 0) {
				Debug.LogWarning("Could not find bus named '" + gInfo.busName + "' to assign to new Sound Group '" + gInfo.groupName + "'.");
			}
			groupScript.busIndex = busIndex;
			break;
		}
		
		return groupTrans;
	}
	
	/// <summary>
	/// This will return the volume of a Sound Group.
	/// </summary>
	/// <param name="sType">The name of the Sound Group</param>
	/// <returns>The volume of the Sound Group</returns>
	public static float GetGroupVolume(string sType) {
		var aGroup = GrabGroup(sType);
		if (aGroup == null) {
			return 0f;
		}
		
		return aGroup.groupMasterVolume;
	}
	
	/// <summary>
	/// This method will set the volume of a Sound Group.
	/// </summary>
	/// <param name="sType">The name of the Sound Group</param>
	/// <param name="volumeLevel">The new volume level.</param>
	public static void SetGroupVolume(string sType, float volumeLevel) {
		var aGroup = GrabGroup(sType, Application.isPlaying);
		if (aGroup == null || AppIsShuttingDown) {
			return;
		}
		
		aGroup.groupMasterVolume = volumeLevel;
		
		AudioInfo aInfo;
		AudioSource aSource = null;
		
		var theGroup = audioSourcesBySoundType[sType];
		
		var busVolume = GetBusVolume(aGroup);
		
		for (var i = 0; i < theGroup._sources.Count; i++) {
			aInfo = theGroup._sources[i];
			aSource = aInfo.source;						
			
			if (aSource == null || !aSource.isPlaying) {
				continue;
			}
			
			var newVol = (aInfo.originalVolume * aInfo.lastPercentageVolume * aGroup.groupMasterVolume * busVolume * MasterAudio.Instance.masterAudioVolume) + aInfo.lastRandomVolume;
			aSource.volume = newVol;
		}
	}
	
	/// <summary>
	/// This method will mute all variations in a Sound Group.
	/// </summary>
	/// <param name="sType">The name of the Sound Group</param>
	public static void MuteGroup(string sType)
	{
		var aGroup = GrabGroup(sType);
		if (aGroup == null) {
			return;
		}
		
		soloedGroups.Remove(aGroup);
		aGroup.isSoloed = false;
		
		SetGroupMuteStatus(aGroup, sType, true);
	}
	
	/// <summary>
	/// This method will unmute all variations in a Sound Group
	/// </summary>
	/// <param name="sType">The name of the Sound Group</param>
	public static void UnmuteGroup(string sType)
	{
		var aGroup = GrabGroup(sType);
		if (aGroup == null) {
			return;
		}
		
		SetGroupMuteStatus(aGroup, sType, false);
	}
	
	private static void SetGroupMuteStatus(MasterAudioGroup aGroup, string sType, bool isMute) {
		aGroup.isMuted = isMute;
		
		var theGroup = audioSourcesBySoundType[sType];
		AudioInfo aInfo;
		AudioSource aSource;
		
		for (var i = 0; i < theGroup._sources.Count; i++) {
			aInfo = theGroup._sources[i];
			aSource = aInfo.source;						
			
			aSource.mute = isMute;
		}
	}
	
	/// <summary>
	/// This method will solo a Sound Group. If anything is soloed, only soloed Sound Groups will be heard.
	/// </summary>
	/// <param name="sType">The name of the Sound Group</param>
	public static void SoloGroup(string sType)
	{
		var aGroup = GrabGroup(sType);
		if (aGroup == null) {
			return;
		}
		
		aGroup.isMuted = false;
		aGroup.isSoloed = true;
		
		soloedGroups.Add(aGroup);
		
		SetGroupMuteStatus(aGroup, sType, false);
	}
	
	/// <summary>
	/// This method will unsolo a Sound Group. 
	/// </summary>
	/// <param name="sType">The name of the Sound Group</param>
	public static void UnsoloGroup(string sType)
	{
		var aGroup = GrabGroup(sType);
		if (aGroup == null) {
			return;
		}
		
		aGroup.isSoloed = false;
		
		soloedGroups.Remove(aGroup);
	}
	
	/// <summary>
	/// This method will return the Sound Group settings for examination purposes.
	/// </summary>
	/// <param name="sType">The name of the Sound Group</param>
	/// <returns>A MasterAudioGroup object</returns>
	public static MasterAudioGroup GrabGroup(string sType, bool logIfMissing = true) {
		if (!audioSourcesBySoundType.ContainsKey(sType)) {
			if (logIfMissing)
			{
				Debug.LogError("Could not grab Sound Group '" + sType + "' because it does not exist in this scene.");
			}
			return null;
		}
		
		AudioGroupInfo _group = audioSourcesBySoundType[sType];
		MasterAudioGroup maGroup = _group._group;
		return maGroup;
	}
	
	/// <summary>
	/// This method will return the Audio Group Info settings for examination purposes. Use on during play in editor, not during edit.
	/// </summary>
	/// <param name="sType">The name of the Sound Group</param>
	/// <returns>an Audio Group Info object</returns>
	public static AudioGroupInfo GetGroupInfo(string sType) {
		if (!audioSourcesBySoundType.ContainsKey(sType)) {
			return null;
		}
		
		AudioGroupInfo _group = audioSourcesBySoundType[sType];
		return _group;
	}
	#endregion
	
	#region Bus methods
	private static int GetBusIndex(string busName, bool alertMissing) {
		for (var i = 0; i < GroupBuses.Count; i++) {
			if (GroupBuses[i].busName == busName) {
				return i + HARD_CODED_BUS_OPTIONS;
			}
		}
		
		if (alertMissing) {
			LogMissingSoundFile("Could not find bus '" + busName + "'.");
		}
		
		return -1;
	}
	
	private static GroupBus GetBusByIndex(int busIndex) {
		if (busIndex < HARD_CODED_BUS_OPTIONS) {
			return null;
		}		
		
		return GroupBuses[busIndex - HARD_CODED_BUS_OPTIONS];
	}
	
	/// <summary>
	/// This method allow you to mute all Groups in a bus.
	/// </summary>
	/// <param name="busName">The name of the bus to mute.</param>
	public static void MuteBus(string busName) {
		var busIndex = GetBusIndex(busName, true);
		
		if (busIndex < 0) {
			return;
		}
		
		var bus = GrabBusByName(busName);
		bus.isMuted = true;
		
		if (bus.isSoloed) {
			UnsoloBus(busName);
		}
		
		var sources = audioSourcesBySoundType.GetEnumerator();
		
		MasterAudioGroup aGroup = null;
		AudioGroupInfo aInfo = null;
		
		while(sources.MoveNext()) {
			aInfo = sources.Current.Value;
			aGroup = aInfo._group;
			if (aGroup.busIndex  != busIndex) {
				continue;
			}
			
			MuteGroup(aGroup.name);
		}
	}
	
	/// <summary>
	/// This method allow you to unmute all Groups in a bus.
	/// </summary>
	/// <param name="busName">The name of the bus to unmute.</param>
	public static void UnmuteBus(string busName) {
		var busIndex = GetBusIndex(busName, true);
		
		if (busIndex < 0) {
			return;
		}
		
		var bus = GrabBusByName(busName);
		bus.isMuted = false;
		
		var sources = audioSourcesBySoundType.GetEnumerator();
		
		MasterAudioGroup aGroup = null;
		AudioGroupInfo aInfo = null;
		
		while(sources.MoveNext()) {
			aInfo = sources.Current.Value;
			aGroup = aInfo._group;
			if (aGroup.busIndex  != busIndex) {
				continue;
			}
			
			UnmuteGroup(aGroup.name);
		}
	}
	
	/// <summary>
	/// This method allow you to solo all Groups in a bus.
	/// </summary>
	/// <param name="busName">The name of the bus to solo.</param>
	public static void SoloBus(string busName) {
		var busIndex = GetBusIndex(busName, true);
		
		if (busIndex < 0) {
			return;
		}
		
		var bus = GrabBusByName(busName);
		bus.isSoloed = true;
		
		if (bus.isMuted) {
			UnmuteBus(busName);
		}
		
		var sources = audioSourcesBySoundType.GetEnumerator();
		
		MasterAudioGroup aGroup = null;
		AudioGroupInfo aInfo = null;
		
		while(sources.MoveNext()) {
			aInfo = sources.Current.Value;
			aGroup = aInfo._group;
			if (aGroup.busIndex  != busIndex) {
				continue;
			}
			
			SoloGroup(aGroup.name);
		}
	}
	
	/// <summary>
	/// This method allow you to unsolo all Groups in a bus.
	/// </summary>
	/// <param name="busName">The name of the bus to unsolo.</param>
	public static void UnsoloBus(string busName) {
		var busIndex = GetBusIndex(busName, true);
		
		if (busIndex < 0) {
			return;
		}
		
		var bus = GrabBusByName(busName);
		bus.isSoloed = false;
		
		var sources = audioSourcesBySoundType.GetEnumerator();
		
		MasterAudioGroup aGroup = null;
		AudioGroupInfo aInfo = null;
		
		while(sources.MoveNext()) {
			aInfo = sources.Current.Value;
			aGroup = aInfo._group;
			if (aGroup.busIndex  != busIndex) {
				continue;
			}
			
			UnsoloGroup(aGroup.name);
		}
	}
	
	/// <summary>
	/// This method allow you to pause all Audio Sources in a bus.
	/// </summary>
	/// <param name="busName">The name of the bus to pause.</param>
	public static void PauseBus(string busName) {
		var busIndex = GetBusIndex(busName, true);
		
		if (busIndex < 0) {
			return;
		}
		
		var sources = audioSourcesBySoundType.GetEnumerator();
		
		MasterAudioGroup aGroup = null;
		AudioGroupInfo aInfo = null;
		
		while(sources.MoveNext()) {
			aInfo = sources.Current.Value;
			aGroup = aInfo._group;
			if (aGroup.busIndex  != busIndex) {
				continue;
			}
			
			PauseSoundGroup(aGroup.name);
		}
	}

	/// <summary>
	/// This method allow you to stop all Audio Sources in a bus.
	/// </summary>
	/// <param name="busName">The name of the bus to stop.</param>
	public static void StopBus(string busName) {
		var busIndex = GetBusIndex(busName, true);
		
		if (busIndex < 0) {
			return;
		}
		
		var sources = audioSourcesBySoundType.GetEnumerator();
		
		MasterAudioGroup aGroup = null;
		AudioGroupInfo aInfo = null;
		
		while(sources.MoveNext()) {
			aInfo = sources.Current.Value;
			aGroup = aInfo._group;
			if (aGroup.busIndex  != busIndex) {
				continue;
			}
			
			StopAllOfSound(aGroup.name);
		}
	}
	
	/// <summary>
	/// This method allow you to unpause all paused Audio Sources in a bus.
	/// </summary>
	/// <param name="busName">The name of the bus to unpause.</param>
	public static void UnpauseBus(string busName) {
		var busIndex = GetBusIndex(busName, true);
		
		if (busIndex < 0) {
			return;
		}
		
		var sources = audioSourcesBySoundType.GetEnumerator();
		
		MasterAudioGroup aGroup = null;
		AudioGroupInfo aInfo = null;
		
		while(sources.MoveNext()) {
			aInfo = sources.Current.Value;
			aGroup = aInfo._group;
			if (aGroup.busIndex  != busIndex) {
				continue;
			}
			
			UnpauseSoundGroup(aGroup.name);
		}
	}
	
	/// <summary>
	/// This method will create a new bus with the name you specify.
	/// </summary>
	/// <param name="busName">The name of the new bus.</param>
	public static void CreateBus(string busName) {
		var newBus = new GroupBus();
		newBus.busName = busName;
		
		GroupBuses.Add(newBus);
	}
	
	/// <summary>
	/// This method will delete a bus by name.
	/// </summary>
	/// <param name="busName">The name of the bus to delete.</param>
	public static void DeleteBusByName(string busName) {
		var index = GetBusIndex(busName, false);
		if (index > 0) {
			DeleteBus(index);
		}
	}
	
	public static void DeleteBus(int busIndex) {
		GroupBuses.RemoveAt(busIndex - HARD_CODED_BUS_OPTIONS);
		
		MasterAudioGroup aGroup = null;
		
		var sources = audioSourcesBySoundType.GetEnumerator();
		
		while(sources.MoveNext()) {
			aGroup = sources.Current.Value._group;
			if (aGroup.busIndex == -1) {
				continue;
			}
			if (aGroup.busIndex == busIndex + MasterAudio.HARD_CODED_BUS_OPTIONS) {					
				aGroup.busIndex = -1;
			} else if (aGroup.busIndex > busIndex + MasterAudio.HARD_CODED_BUS_OPTIONS) {
				aGroup.busIndex--;
			}
		}
	}
	
	/// <summary>
	/// This method will return the bus volume of a specified Sound Group, if any. If the Group is not in a bus, this will return 1.
	/// </summary>
	/// <param name="maGroup">The Sound Group object.</param>
	/// <returns>The volume of the bus.</returns>
	public static float GetBusVolume(MasterAudioGroup maGroup) {
		var busVolume = 1f;
		if (maGroup.busIndex >= HARD_CODED_BUS_OPTIONS) {
			busVolume = GroupBuses[maGroup.busIndex - HARD_CODED_BUS_OPTIONS].volume;
		}
		
		return busVolume;
	}
	
	/// <summary>
	/// This method allow you to fade the volume of a bus over X seconds.
	/// </summary>
	/// <param name="busName">The name of the bus to fade.</param>
	/// <param name="newVolume">The target volume of the bus.</param>
	/// <param name="fadeTime">The amount of time the fade will take.</param>
	public static void FadeBusToVolume(string busName, float newVolume, float fadeTime) {
		if (fadeTime <= INNER_LOOP_CHECK_INTERVAL) {
			SetBusVolumeByName(busName, newVolume); // time really short, just do it at once.
			return;
		}
		
		var bus = GrabBusByName(busName);
		
		if (bus == null) {
			Debug.Log("Could not find bus '" + busName + "' to fade it.");
			return;
		}
		
		if (newVolume < 0f || newVolume > 1f) {
			Debug.Log("Cannot fade bus '" + busName + "'. Invalid volume specified. Volume should be between 0 and 1.");
			return;
		}
		
		// make sure no other bus fades for this bus are happenning.
		var matchingFade = busFades.Find(delegate(BusFadeInfo obj) {
			return obj.BusName == busName;
		});
		
		if (matchingFade != null) {
			matchingFade.IsActive = false; // start with a new one, delete old.
		}
		
		var volStep = (newVolume - bus.volume) / (fadeTime / INNER_LOOP_CHECK_INTERVAL);
		
		var busFade = new BusFadeInfo() {
			BusName = busName,
			VolumeStep = volStep,
			TargetVolume = newVolume
		};
		
		busFades.Add(busFade);
	}
	
	/// <summary>
	/// This method will set the volume of a bus.
	/// </summary>
	/// <param name="newVolume">The volume to set the bus to.</param>
	/// <param name="busName">The bus name.</param>
	public static void SetBusVolumeByName(string busName, float newVolume) {
		var bus = GrabBusByName(busName);
		if (bus == null) {
			Debug.LogError("bus '" + busName + "' not found!");
			return;
		}
		
		SetBusVolume(bus, newVolume);
	}
	
	private static void SetBusVolume(GroupBus bus, float newVolume) {
		bus.volume = newVolume;
		
		AudioInfo aInfo;
		AudioSource aSource = null;
		AudioGroupInfo aGroup = null;
		
		foreach (var key in audioSourcesBySoundType.Keys) {
			aGroup = audioSourcesBySoundType[key];
			var groupBus = GetBusByIndex(aGroup._group.busIndex);
			
			if (groupBus == null || groupBus.busName != bus.busName) {
				continue;
			}
			
			for (var i = 0; i < aGroup._sources.Count; i++) {
				aInfo = aGroup._sources[i];
				aSource = aInfo.source;						
				
				if (!aSource.isPlaying) {
					continue;
				}
				
				var newVol = (aInfo.originalVolume * aInfo.lastPercentageVolume * aGroup._group.groupMasterVolume * bus.volume * MasterAudio.Instance.masterAudioVolume) + aInfo.lastRandomVolume;
				aSource.volume = newVol;
			}
		}
	}
	
	/// <summary>
	/// This method will return the settings of a bus.
	/// </summary>
	/// <param name="busName">The bus name.</param>
	/// <returns>GroupBus object</returns>
	public static GroupBus GrabBusByName(string busName) {
		for (var i = 0; i < GroupBuses.Count; i++) {
			var aBus = GroupBuses[i];
			if (aBus.busName == busName) {
				return aBus;
			}
		}
		
		return null;
	}
	
	#endregion
	
	#region Ducking methods
	/// <summary>
	/// This method will allow you to add a Sound Group to the list of sounds that cause music in the Playlist to duck.
	/// </summary>
	/// <param name="sType">The name of the Sound Group.</param>
	public static void AddSoundGroupToDuckList(string sType) {
		var ma = MasterAudio.Instance;
		
		var matchingDuck = ma.musicDuckingSounds.Find(delegate(DuckGroupInfo obj) {
			return obj.soundType == sType;
		});
		
		if (matchingDuck != null) {
			return;
		}
		
		ma.musicDuckingSounds.Add(new DuckGroupInfo() {
			soundType = sType
		});
	}
	
	/// <summary>
	/// This method will allow you to remove a Sound Group from the list of sounds that cause music in the Playlist to duck.
	/// </summary>
	/// <param name="sType">The name of the Sound Group.</param>
	public static void RemoveSoundGroupFromDuckList(string sType) {
		var ma = MasterAudio.Instance;
		
		var matchingDuck = ma.musicDuckingSounds.Find(delegate(DuckGroupInfo obj) {
			return obj.soundType == sType;
		});
		
		if (matchingDuck != null) {
			ma.musicDuckingSounds.Remove(matchingDuck);
		}
	}
	#endregion
	
	#region Playlist methods
	/// <summary>
	/// This method will find a Playlist by name and return it to you.
	/// </summary>
	public static Playlist GrabPlaylist(string playlistName) {
		if (playlistName == MasterAudio.NO_GROUP_NAME) {
			return null;
		}
		
		for (var i = 0; i < MusicPlaylists.Count; i++) {
			var aPlaylist = MusicPlaylists[i];
			if (aPlaylist.playlistName == playlistName) {
				return aPlaylist;
			}
		}
		
		Debug.LogError("Could not find Playlist '" + playlistName + "'.");
		
		return null;
	}
	
	#region Pause Playlist
	/// <summary>
	/// This method will allow you to pause your Playlist Controller. You should not use this if you have more than one Playlist Controller. Use the overloaded method instead, it takes a playlistControllerName parameter.
	/// </summary>
	public static void PausePlaylist() {
		PausePlaylist(ONLY_PLAYLIST_CONTROLLER_NAME);
	}
	
	/// <summary>
	/// This method will allow you to pause a Playlist Controller by name.
	/// </summary>
	/// <param name="playlistControllerName">The name of the Playlist Controller.</param>
	public static void PausePlaylist(string playlistControllerName) {
		var pcs = PlaylistController.Instances;
		
		var controllers = new List<PlaylistController>();
		
		if (playlistControllerName == ONLY_PLAYLIST_CONTROLLER_NAME) {
			if (!IsOkToCallOnlyPlaylistMethod(pcs, "PausePlaylist")) {
				return;
			}
			
			controllers.Add(pcs[0]);
		} else {
			// multiple playlist controllers
			var pl = PlaylistController.InstanceByName(playlistControllerName);
			if (pl != null) {
				controllers.Add(pl);
			}
		}
		
		PausePlaylists(controllers);
	}
	
	/// <summary>
	/// This method will allow you to pause all Playlist Controllers.
	/// </summary>
	public static void PauseAllPlaylists() {
		PausePlaylists(PlaylistController.Instances);
	}
	
	private static void PausePlaylists(List<PlaylistController> playlists) {
		PlaylistController aList = null;
		
		for (var i = 0; i < playlists.Count; i++) {
			aList = playlists[i];
			aList.PausePlaylist();
		}
	}
	#endregion
	
	#region Resume Playlist
	/// <summary>
	/// This method will allow you to resume a paused Playlist Controller. You should not use this if you have more than one Playlist Controller. Use the overloaded method instead, it takes a playlistControllerName parameter.
	/// </summary>
	public static void ResumePlaylist() {
		ResumePlaylist(ONLY_PLAYLIST_CONTROLLER_NAME);
	}
	
	/// <summary>
	/// This method will allow you to resume a paused Playlist Controller by name.
	/// </summary>
	/// <param name="playlistControllerName">The name of the Playlist Controller.</param>
	public static void ResumePlaylist(string playlistControllerName) {
		var pcs = PlaylistController.Instances;
		
		var controllers = new List<PlaylistController>();
		
		if (playlistControllerName == ONLY_PLAYLIST_CONTROLLER_NAME) {
			if (!IsOkToCallOnlyPlaylistMethod(pcs, "ResumePlaylist")) {
				return;
			}
			
			controllers.Add(pcs[0]);
		} else {
			// multiple playlist controllers
			var pl = PlaylistController.InstanceByName(playlistControllerName);
			if (pl != null) {
				controllers.Add(pl);
			}
		}
		
		ResumePlaylists(controllers);
	}
	
	/// <summary>
	/// This method will allow you to resume all paused Playlist Controllers.
	/// </summary>
	public static void ResumeAllPlaylists() {
		ResumePlaylists(PlaylistController.Instances);
	}
	
	private static void ResumePlaylists(List<PlaylistController> controllers) {
		PlaylistController aList = null;
		
		for (var i = 0; i < controllers.Count; i++) {
			aList = controllers[i];
			aList.ResumePlaylist();
		}
	}
	#endregion
	
	#region Stop Playlist
	/// <summary>
	/// This method will stop a Playlist Controller. You should not use this if you have more than one Playlist Controller. Use the overloaded method instead, it takes a playlistControllerName parameter.
	/// </summary>
	public static void StopPlaylist() {
		StopPlaylist(ONLY_PLAYLIST_CONTROLLER_NAME);
	}
	
	/// <summary>
	/// This method will stop a Playlist Controller by name.
	/// </summary>
	/// <param name="playlistControllerName">The name of the Playlist Controller.</param>
	public static void StopPlaylist(string playlistControllerName) {
		var pcs = PlaylistController.Instances;
		
		var controllers = new List<PlaylistController>();
		
		if (playlistControllerName == ONLY_PLAYLIST_CONTROLLER_NAME) {
			if (!IsOkToCallOnlyPlaylistMethod(pcs, "StopPlaylist")) {
				return;
			}
			
			controllers.Add(pcs[0]);
		} else {
			// multiple playlist controllers
			var pl = PlaylistController.InstanceByName(playlistControllerName);
			if (pl != null) {
				controllers.Add(pl);
			}
		}
		
		StopPlaylists(controllers);
	}
	
	/// <summary>
	/// This method will allow you to stop all Playlist Controllers.
	/// </summary>
	public static void StopAllPlaylists() {
		StopPlaylists(PlaylistController.Instances);
	}
	
	private static void StopPlaylists(List<PlaylistController> playlists) {
		PlaylistController aList = null;
		
		for (var i = 0; i < playlists.Count; i++) {
			aList = playlists[i];
			aList.StopPlaylist();
		}
	}
	#endregion
	
	#region Next Playlist Clip
	/// <summary>
	/// This method will advance the Playlist to the next clip in your Playlist Controller. You should not use this if you have more than one Playlist Controller. Use the overloaded method instead, it takes a playlistControllerName parameter.
	/// </summary>
	public static void TriggerNextPlaylistClip() {
		TriggerNextPlaylistClip(ONLY_PLAYLIST_CONTROLLER_NAME);
	}
	
	/// <summary>
	/// This method will advance the Playlist to the next clip in the Playlist Controller you name.
	/// </summary>
	/// <param name="playlistControllerName">The name of the Playlist Controller.</param>
	public static void TriggerNextPlaylistClip(string playlistControllerName) {
		var pcs = PlaylistController.Instances;
		
		var controllers = new List<PlaylistController>();
		
		if (playlistControllerName == ONLY_PLAYLIST_CONTROLLER_NAME) {
			if (!IsOkToCallOnlyPlaylistMethod(pcs, "TriggerNextPlaylistClip")) {
				return;
			}
			
			controllers.Add(pcs[0]);
		} else {
			// multiple playlist controllers
			var pl = PlaylistController.InstanceByName(playlistControllerName);
			if (pl != null) {
				controllers.Add(pl);
			}
		}
		
		NextPlaylistClips(controllers);
	}
	
	/// <summary>
	/// This method will allow you to advance Playlists in all Playlist Controllers to the next clip in their Playlist.
	/// </summary>
	public static void TriggerNextClipAllPlaylists() {
		NextPlaylistClips(PlaylistController.Instances);
	}
	
	private static void NextPlaylistClips(List<PlaylistController> playlists) {
		PlaylistController aList = null;
		
		for (var i = 0; i < playlists.Count; i++) {
			aList = playlists[i];
			aList.PlayNextSong();
		}
	}
	#endregion
	
	#region Random Playlist Clip
	/// <summary>
	/// This method will play a random clip in the current Playlist for your Playlist Controller. You should not use this if you have more than one Playlist Controller. Use the overloaded method instead, it takes a playlistControllerName parameter.
	/// </summary>
	public static void TriggerRandomPlaylistClip() {
		TriggerRandomPlaylistClip(ONLY_PLAYLIST_CONTROLLER_NAME);
	}
	
	/// <summary>
	/// This method will play a random clip in the current Playlist for the Playlist Controller you name.
	/// </summary>
	/// <param name="playlistControllerName">The name of the Playlist Controller.</param>
	public static void TriggerRandomPlaylistClip(string playlistControllerName) {
		var pcs = PlaylistController.Instances;
		
		var controllers = new List<PlaylistController>();
		
		if (playlistControllerName == ONLY_PLAYLIST_CONTROLLER_NAME) {
			if (!IsOkToCallOnlyPlaylistMethod(pcs, "TriggerRandomPlaylistClip")) {
				return;
			}
			
			controllers.Add(pcs[0]);
		} else {
			// multiple playlist controllers
			var pl = PlaylistController.InstanceByName(playlistControllerName);
			if (pl != null) {
				controllers.Add(pl);
			}
		}
		
		RandomPlaylistClips(controllers);
	}
	
	/// <summary>
	/// This method will allow you to play a random clip in all Playlist Controllers using their currenct Playlist
	/// </summary>
	public static void TriggerRandomClipAllPlaylists() {
		RandomPlaylistClips(PlaylistController.Instances);
	}
	
	private static void RandomPlaylistClips(List<PlaylistController> playlists) {
		PlaylistController aList = null;
		
		for (var i = 0; i < playlists.Count; i++) {
			aList = playlists[i];
			aList.PlayRandomSong();
		}
	}
	
	#endregion
	
	#region Trigger Playlist Clip 
	/// <summary>
	/// This method will play an Audio Clip by name that's in the current Playlist of your Playlist Controller. You should not use this if you have more than one Playlist Controller. Use the overloaded method instead, it takes a playlistControllerName parameter.
	/// </summary>
	/// <param name="clipName">The name of the clip.</param>
	public static void TriggerPlaylistClip(string clipName) {
		TriggerPlaylistClip(ONLY_PLAYLIST_CONTROLLER_NAME, clipName);
	}
	
	/// <summary>
	/// This method will play an Audio Clip by name that's in the current Playlist of the Playlist Controller you name.
	/// </summary>
	/// <param name="clipName">The name of the clip.</param>
	/// <param name="playlistControllerName">The name of the Playlist Controller.</param>
	public static void TriggerPlaylistClip(string playlistControllerName, string clipName) {
		var pcs = PlaylistController.Instances;
		
		PlaylistController controller = null;
		
		if (playlistControllerName == ONLY_PLAYLIST_CONTROLLER_NAME) {
			if (!IsOkToCallOnlyPlaylistMethod(pcs, "TriggerPlaylistClip")) {
				return;
			}
			
			controller = pcs[0];
		} else {
			// multiple playlist controllers
			var pl = PlaylistController.InstanceByName(playlistControllerName);
			if (pl == null) {
				return;
			}
			
			controller = pl;
		}
		
		if (controller != null) {
			controller.TriggerPlaylistClip(clipName);
		}
	}
	
	#endregion
	
	#region ChangePlaylistByName
	/// <summary>
	/// This method will change the current Playlist in the Playlist Controller to a Playlist whose name you specify. You should not use this if you have more than one Playlist Controller. Use the overloaded method instead, it takes a playlistControllerName parameter.
	/// </summary>
	/// <param name="playlistName">The name of the new Playlist.</param>
	/// <param name="playFirstClip"><b>Optional</b> - defaults to True. If you specify false, the first clip in the Playlist will not automatically play.</param>
	public static void ChangePlaylistByName(string playlistName, bool playFirstClip = true) {
		ChangePlaylistByName(ONLY_PLAYLIST_CONTROLLER_NAME, playlistName, playFirstClip);
	}
	
	/// <summary>
	/// This method will play an Audio Clip by name that's in the current Playlist of the Playlist Controller you name.
	/// </summary>
	/// <param name="playlistName">The name of the new Playlist.</param>
	/// <param name="playFirstClip"><b>Optional</b> - defaults to True. If you specify false, the first clip in the Playlist will not automatically play.</param>
	public static void ChangePlaylistByName(string playlistControllerName, string playlistName, bool playFirstClip = true) {
		var pcs = PlaylistController.Instances;
		
		PlaylistController controller = null;
		
		if (playlistControllerName == ONLY_PLAYLIST_CONTROLLER_NAME) {
			if (!IsOkToCallOnlyPlaylistMethod(pcs, "ChangePlaylistByName")) {
				return;
			}
			
			controller = pcs[0];
		} else {
			// multiple playlist controllers
			var pl = PlaylistController.InstanceByName(playlistControllerName);
			if (pl == null) {
				return;
			}
			
			controller = pl;
		}
		
		if (controller != null) {
			controller.ChangePlaylist(playlistName, playFirstClip);
		}
	}
	
	#endregion
	
	#region Playlist Fade To Volume
	/// <summary>
	/// This method will fade the volume of the Playlist Controller over X seconds. You should not use this if you have more than one Playlist Controller. Use the overloaded method instead, it takes a playlistControllerName parameter.
	/// </summary>
	/// <param name="targetVolume">The target volume of the Playlist.</param>
	/// <param name="fadeTime">The time to fade completely to the target volume.</param>
	public static void FadePlaylistToVolume(float targetVolume, float fadeTime) {
		FadePlaylistToVolume(ONLY_PLAYLIST_CONTROLLER_NAME, targetVolume, fadeTime);
	}
	
	/// <summary>
	/// This method will fade the volume of the Playlist Controller whose name you specify over X seconds. 
	/// </summary>
	/// <param name="playlistControllerName">The name of the Playlist Controller.</param>
	/// <param name="targetVolume">The target volume of the Playlist.</param>
	/// <param name="fadeTime">The time to fade completely to the target volume.</param>
	public static void FadePlaylistToVolume(string playlistControllerName, float targetVolume, float fadeTime) {
		var pcs = PlaylistController.Instances;
		
		var controllers = new List<PlaylistController>();
		
		if (playlistControllerName == ONLY_PLAYLIST_CONTROLLER_NAME) {
			if (!IsOkToCallOnlyPlaylistMethod(pcs, "FadePlaylistToVolume")) {
				return;
			}
			
			controllers.Add(pcs[0]);
		} else {
			// multiple playlist controllers
			var pl = PlaylistController.InstanceByName(playlistControllerName);
			if (pl != null) {
				controllers.Add(pl);
			}
		}
		
		FadePlaylists(controllers, targetVolume, fadeTime);
	}
	
	/// <summary>
	/// This method will allow you to fade all current Playlists used by Playlist Controllers to a target volume over X seconds.
	/// </summary>
	public static void FadeAllPlaylistsToVolume(float targetVolume, float fadeTime) {
		FadePlaylists(PlaylistController.Instances, targetVolume, fadeTime);
	}
	
	private static void FadePlaylists(List<PlaylistController> playlists, float targetVolume, float fadeTime) {
		PlaylistController aList = null;
		
		for (var i = 0; i < playlists.Count; i++) {
			aList = playlists[i];
			aList.FadeToVolume(targetVolume, fadeTime);
		}
	}
	
	#endregion
	
	/// <summary>
	/// This method will allow you to add a song to a Playlist by code.
	/// </summary>
	/// <param name="playlistName">The name of the Playlist to add the song to.</param>
	/// <param name="song">The Audio clip of the song.</param>
	/// <param name="loopSong">Optional - whether or not to loop the song.</param>
	/// <param name="songPitch">Optional - the pitch of the song.</param>
	/// <param name="songVolume">Optional - The volume of the song.</param>
	public static void AddSongToPlaylist(string playlistName, AudioClip song, bool loopSong = false, float songPitch = 1f, float songVolume = 1f) {
		var pl = GrabPlaylist(playlistName);
		
		if (pl == null) {
			return;
		}
		
		var newSong = new MusicSetting() {
			clip = song,
			isExpanded = true,
			isLoop = loopSong,
			pitch = songPitch,
			volume = songVolume
		};
		
		pl.MusicSettings.Add(newSong);
	}
	
	/// <summary>
	/// This Property can read and set the Playlist Master Volume. 
	/// </summary>
	public static float PlaylistMasterVolume {
		get {
			return MasterAudio.Instance.masterPlaylistVolume;
		}
		set {
			MasterAudio.Instance.masterPlaylistVolume = value;
			
			var pcs = PlaylistController.Instances;
			for (var i = 0; i < pcs.Count; i++) {
				pcs[i].UpdateMasterVolume();
			}
		}
	}
	
	#endregion
	
	#region Properties
	/// <summary>
	/// This property returns a reference to the Singleton instance of MasterAudio.
	/// </summary>
	public static MasterAudio Instance {
		get {
			if (_instance == null) {
				_instance = (MasterAudio) GameObject.FindObjectOfType(typeof(MasterAudio));
				if (_instance == null && Application.isPlaying) {
					Debug.LogError("There is no Master Audio prefab in this Scene. Subsequent method calls will fail.");
				}
			}
			
			return _instance;
		}
	}
	
	public static AudioSource PreviewerInstance {
		get {
			if (_previewerInstance == null) {
				_previewerInstance = MasterAudio.Instance.GetComponent<AudioSource>();
				if (_previewerInstance == null) {
					MasterAudio.Instance.gameObject.AddComponent<AudioSource>();
					_previewerInstance = MasterAudio.Instance.GetComponent<AudioSource>();
				}
				
				_previewerInstance.playOnAwake = false;
			}
			
			return _previewerInstance;
		}
	}
	
	/// <summary>
	/// This returns true if MasterAudio is initialized and ready to use, false otherwise.
	/// </summary>
	public static bool SoundsReady {
		get {
			return MasterAudio.Instance != null && MasterAudio.Instance.soundsLoaded;
		}
	}
	
	/// <summary>
	/// This property is used to prevent bogus Unity errors while the editor is stopping play. You should never need to read or set this.
	/// </summary>
	public static bool AppIsShuttingDown {
		get {
			return appIsShuttingDown;
		}
		set {
			appIsShuttingDown = value;
		}
	}
	
	/// <summary>
	/// This will return a list of all the Sound Group names.
	/// </summary>
	public List<string> GroupNames {
		get {
			var groupNames = new List<string>();
			groupNames.Add(DYNAMIC_GROUP_NAME);
			groupNames.Add(NO_GROUP_NAME);
			
			for (var i = 0; i < this.transform.childCount; i++) {
				groupNames.Add(this.transform.GetChild(i).name);
			}
			
			return groupNames;
		}
	}
	
	/// <summary>
	/// This will return a list of all the Bus names, including the selectors for "type in" and "no bus".
	/// </summary>
	public List<string> BusNames {
		get {
			var busNames = new List<string>();
			
			busNames.Add(DYNAMIC_GROUP_NAME);
			busNames.Add(NO_GROUP_NAME);
			
			for (var i = 0; i < groupBuses.Count; i++) {
				busNames.Add(groupBuses[i].busName);
			}
			
			return busNames;
		}
	}
	
	/// <summary>
	/// This will return a list of all the Playlists, including the selectors for "type in" and "no bus".
	/// </summary>
	public List<string> PlaylistNames {
		get {
			var playlistNames = new List<string>();
			
			playlistNames.Add(DYNAMIC_GROUP_NAME);
			playlistNames.Add(NO_GROUP_NAME);
			
			for (var i = 0; i < musicPlaylists.Count; i++) {
				playlistNames.Add(musicPlaylists[i].playlistName);
			}
			
			return playlistNames;
		}
	}
	
	/// <summary>
	/// This is the overall master volume level which can change the relative volume of all buses and Sound Groups - not Playlist Controller songs though, they have their own master volume.
	/// </summary>
	public static float MasterVolumeLevel {
		get {
			return MasterAudio.Instance.masterAudioVolume;
		}
		set {
			MasterAudio.Instance.masterAudioVolume = value;
			
			if (!Application.isPlaying) {
				return;
			}
			
			// change all currently playing sound volumes!
			var sources = audioSourcesBySoundType.GetEnumerator();
			MasterAudioGroup _group = null;
			while(sources.MoveNext()) {
				_group = sources.Current.Value._group;
				SetGroupVolume(_group.name, _group.groupMasterVolume); // set to same volume, but it recalcs based on master volume level.
			}
		}
	}
	
	private static bool SceneHasMasterAudio {
		get {
			return MasterAudio.Instance != null;
		}
	}
	
	#endregion
	
	#region Logging (only when turned on via Inspector)
	private static void LogIfLoggingEnabled(string message, string soundGroupName) {
		MasterAudioGroup groupToPlay = GrabGroup(soundGroupName, false);
		
		if (MasterAudio.Instance.LogSounds || (groupToPlay != null && groupToPlay.logSound)) {
			Debug.LogError("T: " + Time.time + " - MasterAudio " + message);
		}
	}
	
	/// <summary>
	/// This gets or sets whether Logging is enabled in Master Audio
	/// </summary>
	public static bool LogSoundsEnabled {
		get {
			return MasterAudio.Instance.LogSounds;
		}
		set {
			MasterAudio.Instance.LogSounds = value;
		}
	}
	
	/// <summary>
	/// This gets or sets whether the entire Mixer is muted or not.
	/// </summary>
	public static bool MixerMuted {
		get {
			return MasterAudio.Instance.mixerMuted;
		}
		set {
			MasterAudio.Instance.mixerMuted = value;
			
			if (value) {
				foreach (var key in audioSourcesBySoundType.Keys) {
					MuteGroup(audioSourcesBySoundType[key]._group.name);
				}
			} else {
				foreach (var key in audioSourcesBySoundType.Keys) {
					UnmuteGroup(audioSourcesBySoundType[key]._group.name);
				}
			}
		}
	}

	/// <summary>
	/// This gets or sets whether the all Playlists are muted or not.
	/// </summary>
	public static bool PlaylistsMuted {
		get {
			return MasterAudio.Instance.playlistsMuted;
		}
		set {
			MasterAudio.Instance.playlistsMuted = value;
			
			var pcs = PlaylistController.Instances;
			
			for (var i = 0; i < pcs.Count; i++) {
				pcs[i].IsMuted = value;
			}
		}
	}
	
	/// <summary>
	/// This gets or sets whether music ducking is enabled.
	/// </summary>
	public bool EnableMusicDucking {
		get {
			return enableMusicDucking;
		}
		set {
			enableMusicDucking = value;
		}
	}
	
	/// <summary>
	/// This gets or sets the ducked volume multiplier
	/// </summary>
	public float DuckedVolumeMultiplier {
		get {
			return duckedVolumeMultiplier;
		}
		set {
			duckedVolumeMultiplier = value;
			
			var pcs = PlaylistController.Instances;
			
			for (var i = 0; i < pcs.Count; i++) {
				pcs[i].UpdateDuckedVolumeMultiplier();
			}
		}
	}
	
	/// <summary>
	/// This gets the cross-fade time for Playlists
	/// </summary>
	public float CrossFadeTime {
		get {
			return crossFadeTime;
		}
	}
	
	public static List<Playlist> MusicPlaylists {
		get {
			return MasterAudio.Instance.musicPlaylists;
		}
	}
	
	/// <summary>
	/// This returns of list of all Buses.
	/// </summary>
	public static List<GroupBus> GroupBuses {
		get {
			return MasterAudio.Instance.groupBuses;
		}
	}
	
	/// <summary>
	/// This will get you the list of all Sound Group Names at runtime only.
	/// </summary>
	public static List<string> RuntimeSoundGroupNames {
		get {
			if (!Application.isPlaying) {
				return new List<string>();
			}
			return new List<string>(audioSourcesBySoundType.Keys);
		}
	}
	
	/// <summary>
	/// This will get you the list of all Bus Names at runtime only.
	/// </summary>
	public static List<string> RuntimeBusNames {
		get {
			if (!Application.isPlaying) {
				return new List<string>();
			}
			
			var busNames = new List<string>();
			
			for (var i = 0; i < MasterAudio.Instance.groupBuses.Count; i++) {
				busNames.Add(MasterAudio.Instance.groupBuses[i].busName);
			}
			
			return busNames;
		}
	}
	
	public static void LogMissingSoundFile(string msg) {
		Debug.LogWarning(msg);
	}
	
	public static void LogNoPlaylist(string playlistControllerName, string methodName) {
		Debug.LogWarning("There is currently no Playlist assigned to Playlist Controller '" + playlistControllerName + "'. Cannot call '" + methodName + "' method.");
	}
	
	private static bool IsOkToCallOnlyPlaylistMethod(List<PlaylistController> pcs, string methodName) {
		if (pcs.Count == 0) {
			Debug.LogError(string.Format("You have no Playlist Controllers in the Scene. You cannot '{0}'.", methodName));
			return false;
		} else if (pcs.Count > 1) {
			Debug.LogError(string.Format("You cannot call '{0}' without specifying a playlist name when you have more than one Playlist Controller.", methodName));
			return false;
		} 
		
		return true;
	}
	
	#endregion
}