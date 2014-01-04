using UnityEngine;
using System;
using System.Collections;

public class MHPickerView : MHView {
	#region functions
	public MHPickerView(){}
	
	public MHPickerView(bool autoInitialize)
	{
		if(Application.isPlaying && autoInitialize)
			init();
	}
	
	public MHPickerView(Rect aRect)
	{
		if(Application.isPlaying)
			initWithFrame(aRect);
	}
	
	new public MHPickerView init()
	{
		return MHiOSManager.Instance.init_pickerview(tag);
	}
	
	new public MHPickerView initWithFrame(Rect aRect)
	{
		return MHiOSManager.Instance.initWithFrame_pickerview(tag, aRect);
	}
	
	public int numberOfComponents {
		get {
			return MHiOSManager.Instance.numberOfComponents(tag);
		}
	}
	
	public int numberOfRowsInComponent(int component)
	{
		return MHiOSManager.Instance.numberOfRowsInComponent(tag, component);
	}
	
	public Vector2 rowSizeForComponent(int component)
	{
		return MHiOSManager.Instance.rowSizeForComponent(tag, component);
	}
	
	public void reloadAllComponents()
	{
		MHiOSManager.Instance.reloadAllComponents(tag);
	}
	
	public void reloadComponent(int component)
	{
		MHiOSManager.Instance.reloadComponent(tag, component);
	}
	
	public void selectRow(int row, int component, bool animated)
	{
		MHiOSManager.Instance.selectRow(tag, row, component, animated);
	}
	
	public int selectedRow(int component)
	{
		return MHiOSManager.Instance.selectedRow(tag, component);
	}
	
	public MHView viewForRow(int row, int component)
	{
		return MHiOSManager.Instance.viewForRow(tag, row, component);
	}
	
	public bool showsSelectionIndicator {
		get {
			return MHiOSManager.Instance.showsSelectionIndicator(tag);
		} set {
			MHiOSManager.Instance.showsSelectionIndicator(tag, value);
		}
	}
	#endregion
					
	#region delegate
	public event Func<MHPickerView, int, float> pickerViewRowHeightForComponent;
	public void _pickerViewRowHeightForComponent(MHPickerView pickerView, int rowHeightForComponent)
	{
		if(pickerViewRowHeightForComponent == null)
			Debug.LogError("ERROR!! Your PickerView Delegate is not completely assigned! You must implement this function.");
		MHTools.ReturnResultToXCode((pickerViewRowHeightForComponent(pickerView, rowHeightForComponent)).ToString());
	}
	
	public event Func<MHPickerView, int, float> pickerViewWidthForComponent;
	public void _pickerViewWidthForComponent(MHPickerView pickerView, int widthForComponent)
	{
		if(pickerViewWidthForComponent == null)
			Debug.LogError("ERROR!! Your PickerView Delegate is not completely assigned! You must implement this function.");
		MHTools.ReturnResultToXCode((pickerViewWidthForComponent(pickerView, widthForComponent)).ToString());
	}
	
	public event Func<MHPickerView, int, int, MHView, MHView> pickerViewViewForRow;
	public void _pickerViewViewForRow(MHPickerView pickerView, int row, int forComponent, MHView reusingView)
	{
		if(pickerViewViewForRow == null)
			Debug.LogError("ERROR!! Your PickerView Delegate is not completely assigned! You must implement this function.");
		MHTools.ReturnResultToXCode((pickerViewViewForRow(pickerView, row, forComponent, reusingView).tag).ToString());
	}
	
	public event Action<MHPickerView, int, int> pickerViewDidSelectRow;
	public void _pickerViewDidSelectRow(MHPickerView pickerView, int row, int component)
	{
		if(pickerViewDidSelectRow == null)
			Debug.LogError("ERROR!! Your PickerView Delegate is not completely assigned! You must implement this function.");
		pickerViewDidSelectRow(pickerView, row, component);
	}
	#endregion
	
	#region datasource
	public event Func<MHPickerView, int> numberOfComponentsInPickerView;
	public void _numberOfComponentsInPickerView(MHPickerView pickerView)
	{
		if(numberOfComponentsInPickerView == null)
			Debug.LogError("ERROR!! Your PickerView DataSource is not completely assigned! You must implement this function.");
		MHTools.ReturnResultToXCode((numberOfComponentsInPickerView(pickerView)).ToString());
	}
	
	public event Func<MHPickerView, int, int> pickerViewNumberOfRowsInComponent;
	public void _pickerViewNumberOfRowsInComponent(MHPickerView pickerView, int component)
	{
		if(pickerViewNumberOfRowsInComponent == null)
			Debug.LogError("ERROR!! Your PickerView DataSource is not completely assigned! You must implement this function.");
		MHTools.ReturnResultToXCode((pickerViewNumberOfRowsInComponent(pickerView, component)).ToString());
	}
	#endregion
}
