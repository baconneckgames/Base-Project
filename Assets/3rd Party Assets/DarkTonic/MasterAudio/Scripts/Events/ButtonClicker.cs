using UnityEngine;
using System.Collections;

[AddComponentMenu("Dark Tonic/Master Audio/ButtonClicker")]
public class ButtonClicker : MonoBehaviour {
	public bool resizeOnClick = true;
	public string mouseDownSound = string.Empty;
	public string mouseUpSound = string.Empty;
	
	private Vector3 originalSize;
	private Vector3 smallerSize;
	private Transform trans;

	// This script can be triggered from NGUI clickable elements only. You can change the OnPress method to another if you have another use for this.
	void Awake() {
		this.trans = this.transform;
		this.originalSize = trans.localScale;
		this.smallerSize = this.originalSize * 0.90f;
	}
	
	void OnPress(bool isDown) {
		if (isDown) {
			MasterAudio.PlaySound(mouseDownSound);
			
			if (resizeOnClick) {			
				trans.localScale = this.smallerSize;
			}
		} else {
			MasterAudio.PlaySound(mouseUpSound);

			if (resizeOnClick) {			
				trans.localScale = this.originalSize;
			}
		}
	}
}
