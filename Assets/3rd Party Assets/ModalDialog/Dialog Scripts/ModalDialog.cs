using UnityEngine;

// Custom modal dialog class, so they're easy to make. Must be added to the scene somewhere
// Copy-paste the component once you've made it once so you don't have to re-do all the GUI stuff!
// Could make it a static class, but it's way easier to create all the GUI styles in the inspector
//
// Uses the GUI ModalWindow function
// Raises messages on the caller object, which must implement IModalDialogUser
//
// Call show() to show the dialog
// Call ModalDialogGui() at the end of your OnGUI method

// Bill Borman 2013

// Interface that classes need to implement to get callback when using a Modal Dialog
public interface IModalDialogUser {
	void ModalSelectedButton1(string id);
	void ModalSelectedButton2(string id);
	void ModalSelectedButton3(string id);
}

// Main class
public class ModalDialog : MonoSingleton<ModalDialog> {
	// Reference type wrapper for "Don't show" settings
	public class ModalBool {
		public ModalBool(bool val) {
			Value = val;
		}
		
		public bool Value;
	}	
	
	public int Width = 350;
	int curHeight; // Height is dynamic depending on the amount of text and whether the buttons and/or "don't ask me" are shown
	public int Margin = 20;
	public int Spacing = 10;	
	public int ButtonHeight = 35;
	AudioSource audioSource;
	int toggleHeight; // Calculated based on the fixed height value of the toggle style
	
	public bool Showing { get; private set; }

	public Texture TintTexture;
	public GUIStyle BackgroundStyle;
	public GUIStyle TextStyle;
	public GUIStyle ButtonStyle;
	public GUIStyle ToggleButtonStyle;

	string curText;
	string curID;
	// The "don't show me again" option will be dialog-specific,
	// so it's passed in as a reference whenever the dialog is drawn
	ModalBool curShowThis;
	bool showDontShowMe;
	bool showButtons;
	int screenWidth;
	int screenHeight;
	string button1Text;
	public string YesDefaultText = "Yes"; // Default only - change in the Inspector
	string button2Text;
	public string NoDefaultText = "No"; // Default only - change in the Inspector
	string button3Text;
	public string CancelDefaultText = "Cancel"; // Default only - change in the Inspector
	public string OKDefaultText = "OK"; // Default only - change in the Inspector
	public string DontShowAgainText = "Don't show this again.";

	// For keyboard selection
	int numSelections;
	int selectedIndex = -1;
	readonly string[] buttonOptions = new string[4];

	IModalDialogUser callBackObj;

	// #### UNITY INTERNAL METHODS ####

	// Called automatically from Awake in the MonoSingleton base class
	public override void Init() {
		buttonOptions[0] = "Don't Ask Me";
		buttonOptions[1] = "Button1";
		buttonOptions[2] = "Button2";
		buttonOptions[3] = "Button3";

		// Check that the style has Word Wrap on
		TextStyle.wordWrap = true;

		toggleHeight = Mathf.CeilToInt(ToggleButtonStyle.fixedHeight);
	}

	// Checks for keyboard controls. Only runs while showing
	void Update() {
		if (!Showing) return; 

		// TODO: Gamepad/joystick support here
		if (Input.GetKeyDown(KeyCode.Escape)) {
			// Escape chooses the rightmost button, or "button 1" if there are no buttons showing
			if (showButtons) {
				if (button3Text != "") ChoseButton3();
				else if (button2Text != "") ChoseButton2();
				else ChoseButton1(); // Even if blank
			}
			else ChoseButton1();
		}

		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			SelectPrevButton();
		}		
		if (Input.GetKeyDown(KeyCode.RightArrow)) {
			SelectNextButton();
		}
		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			SelectDontShow();
		}
		if (Input.GetKeyDown(KeyCode.DownArrow)) {
			SelectNextButton();
		}

		if (Input.GetKeyDown(KeyCode.Return)) {
			ActivateButton();
		}
	}

	// #### PUBLIC METHODS ####

	// Only runs while showing. Must call this from an external OnGUI method!
	// Call this last so it's on top - Gui.Depth does weird things with GUI.Window so we're not using it
	public void ModalDialogGui() {
		if (!Showing) return;

		// Tint the whole background
		GUI.DrawTexture(new Rect(0, 0, screenWidth, screenHeight), TintTexture);

		// Work out the height needed
		GUIContent tempText = new GUIContent(curText);
		float textHeight = TextStyle.CalcHeight(tempText, Width - (2 * Margin));
		curHeight = Mathf.CeilToInt(textHeight) + (2 * Margin);
		if (showDontShowMe) curHeight += (35 + Spacing);
		if (showButtons) curHeight += (ButtonHeight + Margin);

		// GUI.Window ignores depth - but we want it in front!
		GUI.ModalWindow(0, new Rect((screenWidth - Width) / 2.0f, (screenHeight - curHeight) / 2.0f,
			Width, curHeight), DrawDialogContent, "", BackgroundStyle);
	}

	// For simple messages which have no choice, and no callback
	// Button is always a single OK button, since no real choice can be acted upon
	public void ShowOK(string text) {
		ModalBool blankRef = null;
		Show(text, ref blankRef, OKDefaultText, "", "", null, "");
	}
	// As above, but with a callback so we can do something when they click OK
	public void ShowOK(string text, IModalDialogUser called, string id) {
		ModalBool blankRef = null;
		Show(text, ref blankRef, OKDefaultText, "", "", called, id);
	}
	// As above, but with a "don't show me again" option
	public void ShowOK(string text, ref ModalBool curShow, IModalDialogUser called, string id) {
		Show(text, ref curShow, OKDefaultText, "", "", called, id);
	}

	// Show a dialog with the standard yes/no/cancel button options
	public void ShowYesNoCancel(string text, IModalDialogUser called, string id) {
		ModalBool blankRef = null;
		Show(text, ref blankRef, YesDefaultText, NoDefaultText, CancelDefaultText, called, id);
	}
	// As above, but with a "don't show me again" option
	public void ShowYesNoCancel(string text, ref ModalBool curShow, IModalDialogUser called, string id) {
		Show(text, ref curShow, YesDefaultText, NoDefaultText, CancelDefaultText, called, id);
	}

	// General overload, one button only, no callback
	public void Show(string text, string buttonText) {
		ModalBool blankRef = null;
		Show(text, ref blankRef, buttonText, "", "", null, "");
	}
	// General overload, one button only with callback
	public void Show(string text, string buttonText, IModalDialogUser called, string id) {
		ModalBool blankRef = null;
		Show(text, ref blankRef, buttonText, "", "", called, id);
	}
	// As above, with "don't show again" option
	public void Show(string text, ref ModalBool curShow, string buttonText, IModalDialogUser called, string id) {
		Show(text, ref curShow, buttonText, "", "", called, id);
	}

	// General overload, two buttons only
	public void Show(string text, string but1Text, string but2Text, IModalDialogUser called, string id) {
		ModalBool blankRef = null;
		Show(text, ref blankRef, but1Text, but2Text, "", called, id);
	}
	// As above, with "don't show again" option
	public void Show(string text, ref ModalBool curShow, string but1Text, string but2Text, IModalDialogUser called, string id) {
		Show(text, ref curShow, but1Text, but2Text, "", called, id);
	}

	// General overload, three buttons, no "Don't show" checkbox
	public void Show(string text, string but1Text, string but2Text, string but3Text, IModalDialogUser called, string id) {
		ModalBool blankRef = null;
		Show(text, ref blankRef, but1Text, but2Text, but3Text, called, id);
	}
	// As above, with "don't show again" option. This is the master show method, specifying everything
	// text: Text to show in the dialog
	// curShow: True or False ModalBool on whether this dialog should be shown at all (optional - can pass a null ModalRef)
	// but1Text: Custom text for the first button
	// but2Text: Custom text for the second button. Pass in a blank string to have the button not show
	// but3Text: Custom text for the third button. Pass in a blank string to have the button not show
	// called: Calling stript, so it can be called back on button press (optional - can pass null)
	// id: ID of this dialog, so the callback sender can be identified (optional - can pass a blank string)
	public void Show(string text, ref ModalBool curShow, string but1Text, string but2Text, string but3Text, IModalDialogUser called, string id) {
		if (curShow != null) {
			// Automatic early exit if the "show this" bool is false (from the user ticking "don't show again')
			// This makes it so the user doesn't have to check it outside of the dialog code
			if (curShow.Value == false) return;
		}
		
		Showing = true;

		curText = text;
		curID = id;
		showDontShowMe = (curShow != null);
		if (showDontShowMe) curShowThis = curShow; // Will always start true, or the dialog wouldn't have shown
		showButtons = but1Text != "";

		if (!string.IsNullOrEmpty(but3Text)) numSelections = 3;
		else if (!string.IsNullOrEmpty(but2Text)) numSelections = 2;
		else if (!string.IsNullOrEmpty(but1Text)) numSelections = 1;
		else numSelections = 0;
		if (showDontShowMe) numSelections++;

		callBackObj = called;
		screenWidth = Screen.width;
		screenHeight = Screen.height;
		button1Text = but1Text;
		button2Text = but2Text;
		button3Text = but3Text;

		SetSelection();
	}

	// Called automatically when closing the dialog
	public void Hide() {
		Showing = false;
	}

	// #### PRIVATE METHODS ####

	// Called from ModalDialogGUI to draw the main dialog content in the window
	void DrawDialogContent(int windowID) {
		int contentWidth = Width - (2 * Margin);

		if (showDontShowMe) {
			GUI.SetNextControlName ("Don't Ask Me");
			int dontShowY = curHeight - ButtonHeight - toggleHeight - Margin - Spacing - 5;
			bool newShowThis = GUI.Toggle(new Rect(Margin, dontShowY, contentWidth, toggleHeight), !curShowThis.Value, DontShowAgainText, ToggleButtonStyle);
			if (newShowThis == curShowThis.Value) {
				PressedDontAskMe(!newShowThis); // We set the opposite because the toggle on = DON'T show
			}
		}

		int buttonY = curHeight - Margin - ButtonHeight;

		if (showButtons) {
			if (button2Text == "" && button3Text == "") { // Only one button
				int buttonWidth = (Width - (2 * Margin) - Spacing) / 2;
				GUI.SetNextControlName("Button1");
				if (GUI.Button(new Rect((Width - buttonWidth) / 2.0f, buttonY, buttonWidth, ButtonHeight), button1Text, ButtonStyle)) {
					PlayClickSound();
					ChoseButton1();
				}
			}
			else if (button3Text == "") { // Two buttons
				int buttonWidth = (Width - (2 * Margin) - Spacing) / 2;
				GUI.SetNextControlName("Button1");
				if (GUI.Button(new Rect(Margin, buttonY, buttonWidth, ButtonHeight), button1Text, ButtonStyle)) {
					PlayClickSound();
					ChoseButton1();
				}
				GUI.SetNextControlName("Button2");
				if (GUI.Button(new Rect(Width - Margin - buttonWidth, buttonY, buttonWidth, ButtonHeight), button2Text, ButtonStyle)) {
					PlayClickSound();
					ChoseButton2();
				}
			}
			else { // Three buttons
				int buttonWidth = (Width - (2 * Margin) - (2 * Spacing)) / 3;
				GUI.SetNextControlName("Button1");
				if (GUI.Button(new Rect(Margin, buttonY, buttonWidth, ButtonHeight), button1Text, ButtonStyle)) {
					PlayClickSound();
					ChoseButton1();
				}
				GUI.SetNextControlName("Button2");
				if (GUI.Button(new Rect(Margin + Spacing + buttonWidth, buttonY, buttonWidth, ButtonHeight), button2Text, ButtonStyle)) {
					PlayClickSound();
					ChoseButton2();
				}
				GUI.SetNextControlName("Button3");
				if (GUI.Button(new Rect(Width - Margin - buttonWidth, buttonY, buttonWidth, ButtonHeight), button3Text, ButtonStyle)) {
					PlayClickSound();
					ChoseButton3();
				}
			}
		}

		GUI.Label(new Rect(Margin, Margin, contentWidth, 63), curText, TextStyle);
		if (selectedIndex != -1) {
			GUI.FocusControl(buttonOptions[selectedIndex]);
		}
	}

	void PressedDontAskMe(bool newVal) {
		curShowThis.Value = newVal;
	}

	void ChoseButton1() {
		Hide(); // Hide ourself
		if (callBackObj != null) callBackObj.ModalSelectedButton1(curID);
	}

	void ChoseButton2() {
		Hide(); // Hide ourself
		if (callBackObj != null) callBackObj.ModalSelectedButton2(curID);
	}

	void ChoseButton3() {
		Hide(); // Hide ourself
		if (callBackObj != null) callBackObj.ModalSelectedButton3(curID);
	}

	// For keyboard controls
	void SelectNextButton() {
		SelectButton(true);
	}

	// For keyboard controls
	void SelectPrevButton() {
		SelectButton(false);
	}

	// For keyboard controls
	void SelectDontShow() {
		if (showDontShowMe) selectedIndex = 0;
		else SelectPrevButton();
	}

	// For keyboard controls
	void ActivateButton() {
		switch (buttonOptions[selectedIndex]) {
			case "Don't Ask Me":
				PressedDontAskMe(!curShowThis.Value); // Switch the bool
				break;
			case "Button1":
				ChoseButton1();
				break;
			case "Button2":
				ChoseButton2();
				break;
			case "Button3":
				ChoseButton3();
				break;
		}
	}

	// Called when the dialog box is shown, to select the right item
	void SetSelection() {
		if (showButtons) selectedIndex = 1;
		else {
			if (showDontShowMe) selectedIndex = 0;
			else selectedIndex = -1;
		}		
	}

	// 0 = Don't Ask Me, 1-3 = buttons
	void SelectButton(bool forward) {
		if (showButtons) {
			if (showDontShowMe) {
				if (forward && selectedIndex < numSelections - 1) selectedIndex++;
				else if (!forward && selectedIndex > 0) selectedIndex--;
			}
			else {
				if (forward && selectedIndex < numSelections) selectedIndex++;
				else if (!forward && selectedIndex > 1) selectedIndex--;
			}
		}
		else {
			if (showDontShowMe) selectedIndex = 0; // Only one to select - can't change
			else selectedIndex = -1; // Nothing to select at all
		}
	}

	// Play a sound when clicking the buttons, if there's one available on the GameObject
	void PlayClickSound() {
		if (GetAudioSource()) {
			audioSource.Play();
		}
	}

	// Can an audio source if there is one on this GameObject
	AudioSource GetAudioSource() {
		if (!audioSource) {
			audioSource = GetComponent<AudioSource>();
		}
		return audioSource;
	}
}
