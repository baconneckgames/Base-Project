using UnityEngine;
using System.Collections;

public class Tutorial3B_TextViews : Tutorial3A_PickerViews
{
	public string color3B = "Blue";
	public string shape3B = "Circle";
	public string type3B = "String";
	public MHTextView textview3B;
	
	public Tutorial3B_TextViews(){}
	
	public override string GetButtonText()
	{
		return "Add a TextView";
	}
	
	public override string GetTutorialDescription()
	{
		return "Tutorial3B:\nThis section shows you how to make a textview. It will show the results of the pickerview from 3A";
	}
	
	public override void StartTutorial()
	{
		// Do the same stuff from Tutorial1
		base.StartTutorial();
		
		// Create the text view and add it to the view
		textview3B = new MHTextView(new Rect(0f, 5f*screenSize.height/8f, screenSize.width, screenSize.height/8f));
		textview3B.text = GetTextViewText();
		textview3B.backgroundColor = Color.grey;
		view1A.addSubview(textview3B);
	}
	
	public string GetTextViewText()
	{
		string result = "";
		
		result += "Color: ";
		result += color3B + "\n";
		result += "Shape: ";
		result += shape3B + "\n";
		result += "Type: ";
		result += type3B;
		
		return result;
	}
	
	public override void PickerViewDidSelectRow(MHPickerView pView, int row, int component)
	{
		// When a row is selected, debug which item was selected
		switch(component)
		{
		case 0:
			color3B = colorRows[row];
			break;
		case 1:
			shape3B = shapeRows[row];
			break;
		case 2:
			type3B = typeRows[row];
			break;
		default:
			Debug.Log("Nothing Selected");
			break;
		}
		
		textview3B.text = GetTextViewText();
	}

	public override void EndTutorial()
	{
		base.EndTutorial();
	}
}
