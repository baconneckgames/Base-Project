using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(ButtonClicker))]
[CanEditMultipleObjects]
public class ButtonClickerInspector : Editor
{
	private List<string> groupNames = null;
	private bool maInScene;
	
	public override void OnInspectorGUI()
	{
		EditorGUIUtility.LookLikeControls();
		EditorGUI.indentLevel = 0;
		
		var ma = MasterAudio.Instance;
		if (ma != null) {
			GUIHelper.ShowHeaderTexture(ma.logoTexture);
		}
		
		ButtonClicker sounds = (ButtonClicker)target;
		
		maInScene = ma != null;		
		if (maInScene) {
			groupNames = ma.GroupNames;
		}
		
		var resizeOnClick = EditorGUILayout.Toggle("Resize On Click", sounds.resizeOnClick);
		 
		if (resizeOnClick != sounds.resizeOnClick) {
			UndoHelper.RecordObjectPropertyForUndo(sounds, "change Resize On Click");
			sounds.resizeOnClick = resizeOnClick;
		}
		
		if (maInScene) {
			var existingIndex = groupNames.IndexOf(sounds.mouseDownSound);

			int? groupIndex = null;
			
			if (existingIndex >= 1) {
				groupIndex = EditorGUILayout.Popup("Mouse Down Sound", existingIndex, groupNames.ToArray());
			} else if (existingIndex == -1 && sounds.mouseDownSound == MasterAudio.NO_GROUP_NAME) {
				groupIndex = EditorGUILayout.Popup("Mouse Down Sound", existingIndex, groupNames.ToArray());
			} else { // non-match
				GUIHelper.ShowColorWarning("Sound Type found no match. Type in or choose one.");

				var newMouseDown = EditorGUILayout.TextField("Mouse Down Sound", sounds.mouseDownSound);
				if (newMouseDown != sounds.mouseDownSound) {
					UndoHelper.RecordObjectPropertyForUndo(sounds, "change Sound Group");
					sounds.mouseDownSound = newMouseDown;
				}
				var newIndex = EditorGUILayout.Popup("All Sound Types", -1, groupNames.ToArray());
				if (newIndex >= 0) {
					groupIndex = newIndex;
				}
			}
			
			if (groupIndex.HasValue) {
				if (existingIndex != groupIndex.Value) {
					UndoHelper.RecordObjectPropertyForUndo(sounds, "change Sound Group");
				}

				if (groupIndex.Value == -1) {
					sounds.mouseDownSound = MasterAudio.NO_GROUP_NAME;
				} else {
					sounds.mouseDownSound = groupNames[groupIndex.Value];
				}
			}
		} else {
			var newDown = EditorGUILayout.TextField("Mouse Down Sound", sounds.mouseDownSound);
			if (newDown != sounds.mouseDownSound) {
				UndoHelper.RecordObjectPropertyForUndo(sounds, "change Sound Group");
				sounds.mouseDownSound = newDown;
			}
		}
		
		if (maInScene) {
			var existingIndex = groupNames.IndexOf(sounds.mouseUpSound);

			int? groupIndex = null;
			
			if (existingIndex >= 0) {
				groupIndex = EditorGUILayout.Popup("Mouse Up Sound", existingIndex, groupNames.ToArray());
			} else if (existingIndex == -1 && sounds.mouseUpSound == MasterAudio.NO_GROUP_NAME) {
				groupIndex = EditorGUILayout.Popup("Mouse Up Sound", existingIndex, groupNames.ToArray());
			} else { // non-match
				GUIHelper.ShowColorWarning("Sound Type found no match. Choose one from 'All Sound Types'.");

				var newMouseUp = EditorGUILayout.TextField("Mouse Up Sound", sounds.mouseUpSound);
				if (newMouseUp != sounds.mouseUpSound) {
					UndoHelper.RecordObjectPropertyForUndo(sounds, "change Sound Group");
					sounds.mouseUpSound = newMouseUp;
				}
				var newIndex = EditorGUILayout.Popup("All Sound Types", -1, groupNames.ToArray());
				if (newIndex >= 0) {
					groupIndex = newIndex;
				}
			}
			
			if (groupIndex.HasValue) {
				if (existingIndex != groupIndex.Value) {
					UndoHelper.RecordObjectPropertyForUndo(sounds, "change Sound Group");
				}
				if (groupIndex.Value == -1) {
					sounds.mouseUpSound = MasterAudio.NO_GROUP_NAME;
				} else {
					sounds.mouseUpSound = groupNames[groupIndex.Value];
				}
			}
		} else {
			var newUp = EditorGUILayout.TextField("Mouse Up Sound", sounds.mouseUpSound);
			if (newUp != sounds.mouseUpSound) {
				sounds.mouseUpSound = newUp;
				UndoHelper.RecordObjectPropertyForUndo(sounds, "change Sound Group");
			}
		}
		
		if (GUI.changed) {
			EditorUtility.SetDirty(target);
		}

		GUIHelper.RepaintIfUndoOrRedo(this);

		//DrawDefaultInspector();
	}
}
