using UnityEngine;

// Simple GUI just to demonstrate the modal dialog system
public class MainSceneGUI : MonoBehaviour, IModalDialogUser {
	public Camera CameraRef;
	
	const int margin = 5;
	const int buttonWidth = 300;
	const int buttonHeight = 30;
	const int numRows = 10;
	const int numColumns = 2;
	const int guiHeight = (buttonHeight * numRows) + (margin * (numRows + 1));
	const int guiWidth = (buttonWidth * numColumns) + (margin * (numColumns + 1));

	const string text1 = "This is a simple modal dialog.\n\nIf you'd rather have centered text, you can change the Alignment of the Modal Dialog's Text Style to Upper Center instead of Upper Left.";
	const string text2 = "This is another simple modal dialog, but when clicking OK it'll call the ModalSelectedButton1 method on this class.";
	const string text3 = "This one is like the one above, but also lets you choose whether to show it in the future.";
	const string text4 = "This one provides yes/no/cancel button options. These call back the ModalSelectedButton1, ModalSelectedButton2 or ModalSelectedButton3 methods when clicked.";
	const string text5 = "This one is like the one above, but also lets you choose whether to show it in the future.";
	
	const string text6 = "One custom button without callback.";
	const string text7 = "One custom button with callback.";
	const string text8 = "One custom button plus a don't show again option.";
	const string text9 = "Two custom buttons.";
	const string text10 = "Two custom buttons plus a don't show again option.";
	const string text11 = "Three custom buttons.";
	const string text12 = "Three custom buttons plus a don't show again option.";

	const string toggleText1 = "Show OK dialog.";
	const string toggleText2 = "Show Yes/No/Cancel dialog.";
	const string toggleText3 = "Show one button dialog.";
	const string toggleText4 = "Show two button dialog.";
	const string toggleText5 = "Show three button dialog.";

	string displayText = "";
	public GUIStyle DisplayTextStyle;

	ModalDialog.ModalBool showOKDialog;
	ModalDialog.ModalBool showYesNoCancelDialog;
	ModalDialog.ModalBool showOneButtonDialog;
	ModalDialog.ModalBool showTwoButtonDialog;
	ModalDialog.ModalBool showThreeButtonDialog;

	void Awake() {
		showOKDialog = new ModalDialog.ModalBool(true);
		showYesNoCancelDialog = new ModalDialog.ModalBool(true);
		showOneButtonDialog = new ModalDialog.ModalBool(true);
		showTwoButtonDialog = new ModalDialog.ModalBool(true);
		showThreeButtonDialog = new ModalDialog.ModalBool(true);
	}

	// Unity ShowGUI method
	public void OnGUI() {
		int widthOffset = (Screen.width - guiWidth) / 2;
		int heightOffset = (Screen.height - guiHeight) / 2;
		int xPos = margin;
		int yPos = margin;
		
		GUI.Box(new Rect(widthOffset, heightOffset, guiWidth, guiHeight), "");

		GUI.BeginGroup(new Rect(widthOffset, heightOffset, guiWidth, guiHeight));

		// Column 1

		if (GUI.Button(new Rect(xPos, yPos, buttonWidth, buttonHeight), "Simple OK dialog (no callback)")) {
			ModalDialog.Instance.ShowOK(text1);
		}
		yPos += buttonHeight + margin;
		if (GUI.Button(new Rect(xPos, yPos, buttonWidth, buttonHeight), "OK dialog with callback")) {
			ModalDialog.Instance.ShowOK(text2, this, "id_ok");
		}
		yPos += buttonHeight + margin;
		if (GUI.Button(new Rect(xPos, yPos, buttonWidth, buttonHeight), "OK dialog with callback and 'Don't show'")) {
			ModalDialog.Instance.ShowOK(text3, ref showOKDialog, this, "id_ok2");
		}
		yPos += buttonHeight + margin;
		showOKDialog.Value = GUI.Toggle(new Rect(xPos, yPos, buttonWidth, buttonHeight), showOKDialog.Value, toggleText1);
		yPos += buttonHeight + margin;
		if (GUI.Button(new Rect(xPos, yPos, buttonWidth, buttonHeight), "Yes/No/Cancel dialog")) {
			ModalDialog.Instance.ShowYesNoCancel(text4, this, "id_yesnocancel");
		}
		yPos += buttonHeight + margin;
		if (GUI.Button(new Rect(xPos, yPos, buttonWidth, buttonHeight), "Yes/No/Cancel with 'Don't show'")) {
			ModalDialog.Instance.ShowYesNoCancel(text5, ref showYesNoCancelDialog, this, "id_yesnocancel2");
		}
		yPos += buttonHeight + margin;
		showYesNoCancelDialog.Value = GUI.Toggle(new Rect(xPos, yPos, buttonWidth, buttonHeight), showYesNoCancelDialog.Value, toggleText2);
		
		// Display area
		GUI.Label(new Rect(margin, guiHeight - 20 - margin, buttonWidth, 20), displayText, DisplayTextStyle);


		// Column 2
		xPos = guiWidth / 2 + (margin / 2);
		yPos = margin;

		if (GUI.Button(new Rect(xPos, yPos, buttonWidth, buttonHeight), "One custom button (no callback)")) {
			ModalDialog.Instance.Show(text6, "Button");
		}
		yPos += buttonHeight + margin;
		if (GUI.Button(new Rect(xPos, yPos, buttonWidth, buttonHeight), "One custom button with callback")) {
			ModalDialog.Instance.Show(text7, "Red", this, "id_onebutton");
		}
		yPos += buttonHeight + margin;
		if (GUI.Button(new Rect(xPos, yPos, buttonWidth, buttonHeight), "One custom button with 'Don't show'")) {
			ModalDialog.Instance.Show(text8, ref showOneButtonDialog, "Red", this, "id_onebutton2");
		}
		yPos += buttonHeight + margin;
		showOneButtonDialog.Value = GUI.Toggle(new Rect(xPos, yPos, buttonWidth, buttonHeight), showOneButtonDialog.Value, toggleText3);
		yPos += buttonHeight + margin;
		if (GUI.Button(new Rect(xPos, yPos, buttonWidth, buttonHeight), "Two custom buttons")) {
			ModalDialog.Instance.Show(text9, "Green", "Blue", this, "id_twobuttons");
		}
		yPos += buttonHeight + margin;
		if (GUI.Button(new Rect(xPos, yPos, buttonWidth, buttonHeight), "Two custom buttons with 'Don't show'")) {
			ModalDialog.Instance.Show(text10, ref showTwoButtonDialog, "Green", "Blue", this, "id_twobuttons2");
		}
		yPos += buttonHeight + margin;
		showTwoButtonDialog.Value = GUI.Toggle(new Rect(xPos, yPos, buttonWidth, buttonHeight), showTwoButtonDialog.Value, toggleText4);		
		yPos += buttonHeight + margin;
		if (GUI.Button(new Rect(xPos, yPos, buttonWidth, buttonHeight), "Three custom buttons")) {
			ModalDialog.Instance.Show(text11, "Cyan", "Yellow", "White", this, "id_threebuttons");
		}
		yPos += buttonHeight + margin;
		if (GUI.Button(new Rect(xPos, yPos, buttonWidth, buttonHeight), "Three custom buttons with 'Don't show'")) {
			ModalDialog.Instance.Show(text12, ref showThreeButtonDialog, "Cyan", "Yellow", "White", this, "id_threebuttons2");
		}
		yPos += buttonHeight + margin;
		showThreeButtonDialog.Value = GUI.Toggle(new Rect(xPos, yPos, buttonWidth, buttonHeight), showThreeButtonDialog.Value, toggleText5);

		GUI.EndGroup();

		// Needed for modal dialogs to show
		ModalDialog.Instance.ModalDialogGui();
	}


	// #### MODAL DIALOG CALLBACK METHODS ####

	// Called if the first button is pressed
	public void ModalSelectedButton1(string id) {
		displayText = "";
		
		switch (id) {
			case "id_ok": case "id_ok2":
				displayText = "Pressed OK!";
				break;
			case "id_yesnocancel": case "id_yesnocancel2":
				displayText = "Pressed YES!";
				break;
			case "id_onebutton": case "id_onebutton2":
				CameraRef.backgroundColor = Color.red;
				break;
			case "id_twobuttons": case "id_twobuttons2":
				CameraRef.backgroundColor = Color.green;
				break;
			case "id_threebuttons": case "id_threebuttons2":
				CameraRef.backgroundColor = Color.cyan;
				break;
		}
	}

	// Called if the second button is pressed
	public void ModalSelectedButton2(string id) {
		displayText = "";
		
		switch (id) {
			case "id_yesnocancel": case "id_yesnocancel2":
				displayText = "Pressed NO!";
				break;
			case "id_twobuttons": case "id_twobuttons2":
				CameraRef.backgroundColor = Color.blue;
				break;
			case "id_threebuttons": case "id_threebuttons2":
				CameraRef.backgroundColor = Color.yellow;
				break;
		}
	}

	// Called if the third button is pressed
	public void ModalSelectedButton3(string id) {
		displayText = "";
		
		if (id == "id_yesnocancel" || id == "id_yesnocancel2") {
			displayText = "Pressed CANCEL!";
		}
		else if (id == "id_threebuttons" || id == "id_threebuttons2") {
			CameraRef.backgroundColor = Color.white;
		}
	}
}
