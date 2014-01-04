using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(PlaylistController))]
public class PlaylistControllerInspector : Editor {
	public override void OnInspectorGUI() {
		EditorGUIUtility.LookLikeControls();
		EditorGUI.indentLevel = 0;
		
		PlaylistController controller = (PlaylistController)target;
		
		var ma = MasterAudio.Instance;
		if (ma != null) {
			GUIHelper.ShowHeaderTexture(ma.logoTexture);
		}
		
		var newVol = EditorGUILayout.Slider("Playlist Volume", controller.playlistVolume, 0f, 1f);
		if (newVol != controller.playlistVolume) {
			UndoHelper.RecordObjectPropertyForUndo(controller, "change Playlist Volume");
			controller.playlistVolume = newVol;
			controller.UpdateMasterVolume();
		}
		
		ma = MasterAudio.Instance;
		if (ma != null) {
			var plNames = MasterAudio.Instance.PlaylistNames;
			
			var existingIndex = plNames.IndexOf(controller.startPlaylistName);
			
			int? groupIndex = null;
			
			if (existingIndex >= 1) {
				groupIndex = EditorGUILayout.Popup("Initial Playlist", existingIndex, plNames.ToArray());
				if (existingIndex == 1) {
					GUIHelper.ShowColorWarning("*Initial Playlist not specified. No music will play.");
				}
			} else if (existingIndex == -1 && controller.startPlaylistName == MasterAudio.NO_GROUP_NAME) {
				groupIndex = EditorGUILayout.Popup("Initial Playlist", existingIndex, plNames.ToArray());
			} else { // non-match
				GUIHelper.ShowColorWarning("Initial Playlist found no match. Type in or choose one from 'All Playlists'.");
				var newPlaylist = EditorGUILayout.TextField("Initial Playlist", controller.startPlaylistName);
				if (newPlaylist != controller.startPlaylistName) {
					UndoHelper.RecordObjectPropertyForUndo(controller, "change Initial Playlist");
					controller.startPlaylistName = newPlaylist;
				}
				
				var newIndex = EditorGUILayout.Popup("All Playlists", -1, plNames.ToArray());
				if (newIndex >= 0) {
					groupIndex = newIndex;
				}
			}
			
			if (groupIndex.HasValue) {
				if (existingIndex != groupIndex.Value) {
					UndoHelper.RecordObjectPropertyForUndo(controller, "change Initial Playlist");
				}
				if (groupIndex.Value == -1) {
					controller.startPlaylistName = MasterAudio.NO_GROUP_NAME;
				} else {
					controller.startPlaylistName = plNames[groupIndex.Value];
				}
			}
		}
		
		EditorGUI.indentLevel = 0;
		var newAwake = EditorGUILayout.Toggle("Start Playlist on Awake?", controller.startPlaylistOnAwake);
		if (newAwake != controller.startPlaylistOnAwake) {
			UndoHelper.RecordObjectPropertyForUndo(controller, "toggle Start Playlist on Awake");
			controller.startPlaylistOnAwake = newAwake;
		}
		
		var newShuffle = EditorGUILayout.Toggle("Shuffle Mode", controller.isShuffle);
		if (newShuffle != controller.isShuffle) {
			UndoHelper.RecordObjectPropertyForUndo(controller, "toggle Shuffle Mode");
			controller.isShuffle = newShuffle;
		}
		
		var newAuto = EditorGUILayout.Toggle("Auto advance clips", controller.isAutoAdvance);
		if (newAuto != controller.isAutoAdvance) {
			UndoHelper.RecordObjectPropertyForUndo(controller, "toggle Auto advance clips");
			controller.isAutoAdvance = newAuto;
		}
		
		GUIHelper.ShowColorWarning("*Note: auto advance will not advance past a looped track.");

		GUIHelper.RepaintIfUndoOrRedo(this);
		
		if (GUI.changed) {
			EditorUtility.SetDirty(target);
		}
		
		//DrawDefaultInspector();
	}
}