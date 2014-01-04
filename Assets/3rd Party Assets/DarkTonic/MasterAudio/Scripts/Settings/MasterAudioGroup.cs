using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MasterAudioGroup : MonoBehaviour {
	public const string NO_BUS = "[NO BUS]";
	
	public Texture logoTexture;
	public Texture settingsTexture;
	public Texture deleteTexture;
	public int busIndex = -1;
	
	public bool isExpanded = true;
	public float groupMasterVolume = 1f;
	public int retriggerPercentage = 50;
	public VariationMode curVariationMode = VariationMode.Normal;
	public VariationSequence curVariationSequence = VariationSequence.Randomized;
	public bool useInactivePeriodPoolRefill = false;
	public float inactivePeriodSeconds = 5f;
	public List<SoundGroupVariation> groupVariations = new List<SoundGroupVariation>();
	public SoundGroupVariation.AudioLocation bulkVariationMode = SoundGroupVariation.AudioLocation.Clip;
	public bool logSound = false;
	
	public LimitMode limitMode = LimitMode.None;
	public int limitPerXFrames = 1;
	public float minimumTimeBetween = 0.1f;
	
	public bool limitPolyphony = false;
	public int voiceLimitCount = 1;
	
	public bool isSoloed = false;
	public bool isMuted = false;
	
	private List<int> activeAudioSourcesIds = new List<int>();

	public enum VariationSequence {
		Randomized,
		TopToBottom
	}

	public enum VariationMode {
		Normal,
		LoopedChain				
	}
	
	public enum LimitMode {
		None,
		FrameBased,
		TimeBased
	}
	
	public bool HasVoicesRemaining {
		get {
			if (busIndex < MasterAudio.HARD_CODED_BUS_OPTIONS || !Application.isPlaying) {
				return true; // no bus, so no voice limit
			}
			
			var bus = MasterAudio.GroupBuses[busIndex - MasterAudio.HARD_CODED_BUS_OPTIONS];
			
			if (bus.voiceLimit <= 0) { 
				return true; // no voice limit set
			}
			
			return bus.voiceLimit > activeAudioSourcesIds.Count;
		}
	}
	
	public void AddActiveAudioSourceId(SoundGroupVariation variation) {
		var id = variation.GetInstanceID();
		
		if (activeAudioSourcesIds.Contains(id)) {
			return;
		}
		
		activeAudioSourcesIds.Add(id);
	}
	
	public void RemoveActiveAudioSourceId(SoundGroupVariation variation) {
		var id = variation.GetInstanceID();
		activeAudioSourcesIds.Remove(id);
	}
}
