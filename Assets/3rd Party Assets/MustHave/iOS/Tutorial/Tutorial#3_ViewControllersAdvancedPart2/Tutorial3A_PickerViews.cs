using UnityEngine;
using System.Collections;

public class Tutorial3A_PickerViews : Tutorial1A_ViewController
{
	public MHPickerView pickerView3A;
	public string[] components = new string[]{"Color", "Shape", "Type"};
	public string[] colorRows = new string[]{"Blue", "Green", "Yellow"};
	public string[] shapeRows = new string[]{"Circle", "Square", "Triangle", "Cylinder", "Donut"};
	public string[] typeRows = new string[]{"String", "Int", "Float", "Bool", "Vector2"};
	
	public Tutorial3A_PickerViews(){}
	
	public override string GetButtonText()
	{
		return "Add a PickerView";
	}
	
	public override string GetTutorialDescription()
	{
		return "Tutorial3A:\nThis section shows you how to make a picker view";
	}
	
	public override void StartTutorial()
	{
		// Do the same stuff from Tutorial1
		base.StartTutorial();
		
		// Create a pickerview on the bottom 4th of the screen
		pickerView3A = new MHPickerView(new Rect(0f, 3f*screenSize.height/4f, screenSize.width, screenSize.height/4f));
		pickerView3A.showsSelectionIndicator = true;
		
		// Connect the necessary actions for pickerviews
		pickerView3A.pickerViewRowHeightForComponent += PickerViewRowHeight;
		pickerView3A.pickerViewDidSelectRow += PickerViewDidSelectRow;
		pickerView3A.numberOfComponentsInPickerView += NumberOfComponentsInPickerView; 
		pickerView3A.pickerViewNumberOfRowsInComponent += PickerViewNumberOfRowsInComponent;
		pickerView3A.pickerViewViewForRow += PickerViewViewForRow;
		pickerView3A.pickerViewWidthForComponent += PickerViewWidthForComponent;
		
		// Add the pickerview
		view1A.addSubview(pickerView3A);
	}
	
	public float PickerViewRowHeight(MHPickerView pView, int component)
	{
		// Return the standard size for a pickerview row
		return 44f;
	}
	
	public virtual void PickerViewDidSelectRow(MHPickerView pView, int row, int component)
	{
		// When a row is selected, debug which item was selected
		switch(component)
		{
		case 0:
			Debug.Log("Selected: " + colorRows[row]);
			break;
		case 1:
			Debug.Log("Selected: " + shapeRows[row]);
			break;
		case 2:
			Debug.Log("Selected: " + typeRows[row]);
			break;
		default:
			Debug.Log("Selected Default");
			break;
		}
	}
	
	public int PickerViewNumberOfRowsInComponent(MHPickerView pView, int component)
	{
		// Tell the pickerview how many rows are in each component
		switch(component)
		{
		case 0:
			return colorRows.Length;
		case 1:
			return shapeRows.Length;
		case 2:
			return typeRows.Length;
		default:
			return 0;
		}
	}
	
	public int NumberOfComponentsInPickerView(MHPickerView pView)
	{
		// Tell the pickerview how many components there are in the pickerview
		return components.Length;
	}
	
	public MHView PickerViewViewForRow(MHPickerView pView, int row, int component, MHView reusingView)
	{
		// Give the pickerview a view to use for each row in the pickerview
		MHLabel label = (MHLabel)reusingView;
		
		// If the reusingView is null, make a new one -- in this case a label
		// Otherwise, just use the reusing view
		if(reusingView == null)
		{
			label = new MHLabel(MHTools.RectZero);
		}
		
		// Decide what text to use for the label
		string resultLabel = "";
		switch(component)
		{
		case 0:
			resultLabel = colorRows[row];
			break;
		case 1:
			resultLabel = shapeRows[row];
			break;
		case 2:
			resultLabel = typeRows[row];
			break;
		default:
			resultLabel = "default";
			break;
		}
		
		// Set the label text, color, and alignment
		label.text = resultLabel;
		label.textAlignment = MHTextAlignment.MHTextAlignmentCenter;
		label.textColor = Color.black;
		
		// Return the label
		return label;
	}
	
	public float PickerViewWidthForComponent(MHPickerView pView, int component)
	{
		// Return the width of each component (using number of components)
		return screenSize.width / (components.Length * 1f);
	}

	public override void EndTutorial()
	{
		pickerView3A.release();
		base.EndTutorial();
	}
}
