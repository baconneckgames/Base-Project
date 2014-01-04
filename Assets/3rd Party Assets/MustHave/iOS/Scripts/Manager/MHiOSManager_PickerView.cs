using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public partial class MHiOSManager : Singleton<MHiOSManager> {
	#region pickerview_xcode_output
	[DllImport("__Internal")]
    private static extern int _init_pickerview(int tag);
	public MHPickerView init_pickerview(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return GetObjectByUniqueTag(_init_pickerview(tag)) as MHPickerView;
		}
		else
			return new MHPickerView();
	}
	
	[DllImport("__Internal")]
    private static extern int _initWithFrame_pickerview(int tag, string aRect);
	public MHPickerView initWithFrame_pickerview(int tag, Rect aRect)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string rectJsonString = MHTools.ConvertRectToJSON(aRect);
			
			return GetObjectByUniqueTag(_initWithFrame_pickerview(tag, rectJsonString)) as MHPickerView;
		}
		else
			return new MHPickerView();
	}
	
	[DllImport("__Internal")]
    private static extern int _numberOfComponents(int tag);
	public int numberOfComponents(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _numberOfComponents(tag);
		}
		else
			return 0;
	}
	
	[DllImport("__Internal")]
    private static extern int _numberOfRowsInComponent(int tag, int component);
	public int numberOfRowsInComponent(int tag, int component)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _numberOfRowsInComponent(tag, component);
		}
		else
			return 0;
	}
	
	[DllImport("__Internal")]
    private static extern string _rowSizeForComponent(int tag, int component);
	public Vector2 rowSizeForComponent(int tag, int component)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string vectorJsonString = _rowSizeForComponent(tag, component);
			
			return MHTools.ConvertJSONToVector2(vectorJsonString);
		}
		else
			return Vector2.zero;
	}
	
	[DllImport("__Internal")]
    private static extern void _reloadAllComponents(int tag);
	public void reloadAllComponents(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_reloadAllComponents(tag);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _reloadComponent(int tag, int component);
	public void reloadComponent(int tag, int component)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_reloadComponent(tag, component);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _selectRow(int tag, int row, int component, bool animated);
	public void selectRow(int tag, int row, int component, bool animated)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_selectRow(tag, row, component, animated);
		}
	}
	
	[DllImport("__Internal")]
    private static extern int _selectedRow(int tag, int component);
	public int selectedRow(int tag, int component)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _selectedRow(tag, component);
		}
		else
			return 0;
	}
	
	[DllImport("__Internal")]
    private static extern int _viewForRow(int tag, int row, int component);
	public MHView viewForRow(int tag, int row, int component)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			int viewTag = _viewForRow(tag, row, component);
			
			return GetObjectByUniqueTag(viewTag) as MHView;
		}
		else
			return new MHView();
	}
	
	[DllImport("__Internal")]
    private static extern bool _showsSelectionIndicator(int tag);
	public bool showsSelectionIndicator(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _showsSelectionIndicator(tag);
		}
		else
			return false;
	}
	
	[DllImport("__Internal")]
    private static extern void _showsSelectionIndicator_set(int tag, bool shows);
	public void showsSelectionIndicator(int tag, bool shows)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_showsSelectionIndicator_set(tag, shows);
		}
	}
	#endregion
	
	#region pickerview_xcode_input
	void pickerViewRowHeightForComponent(string jsonString)
	{
		int tag;
		MHPickerView pickerView;
		int rowHeightForComponent;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			pickerView = GetObjectByUniqueTag(Convert.ToInt32(result["pickerView"])) as MHPickerView;
			rowHeightForComponent = Convert.ToInt32(result["rowHeightForComponent"]);
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHPickerView)
				(obj as MHPickerView)._pickerViewRowHeightForComponent(pickerView, rowHeightForComponent);
		}
	}
	
	void pickerViewWidthForComponent(string jsonString)
	{
		int tag;
		MHPickerView pickerView;
		int widthForComponent;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			pickerView = GetObjectByUniqueTag(Convert.ToInt32(result["pickerView"])) as MHPickerView;
			widthForComponent = Convert.ToInt32(result["widthForComponent"]);
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHPickerView)
				(obj as MHPickerView)._pickerViewWidthForComponent(pickerView, widthForComponent);
		}
	}
	
	void pickerViewViewForRow(string jsonString)
	{
		int tag;
		MHPickerView pickerView;
		int row;
		int forComponent;
		MHView reusingView;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			pickerView = GetObjectByUniqueTag(Convert.ToInt32(result["pickerView"])) as MHPickerView;
			row = Convert.ToInt32(result["row"]);
			forComponent = Convert.ToInt32(result["forComponent"]);
			reusingView = GetObjectByUniqueTag(Convert.ToInt32(result["reusingView"])) as MHView;
			if(reusingView == MHView.unityView)
				reusingView = null;
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHPickerView)
				(obj as MHPickerView)._pickerViewViewForRow(pickerView, row, forComponent, reusingView);
		}
	}
	
	void pickerViewDidSelectRow(string jsonString)
	{
		int tag;
		MHPickerView pickerView;
		int row;
		int component;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			pickerView = GetObjectByUniqueTag(Convert.ToInt32(result["pickerView"])) as MHPickerView;
			row = Convert.ToInt32(result["row"]);
			component = Convert.ToInt32(result["component"]);
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHPickerView)
				(obj as MHPickerView)._pickerViewDidSelectRow(pickerView, row, component);
		}
	}
	#endregion
	
	#region pickerview_xcode_datasource
	void numberOfComponentsInPickerView(string jsonString)
	{
		int tag;
		MHPickerView pickerView;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			pickerView = GetObjectByUniqueTag(Convert.ToInt32(result["pickerView"])) as MHPickerView;
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHPickerView)
				(obj as MHPickerView)._numberOfComponentsInPickerView(pickerView);
		}
	}
	
	void pickerViewNumberOfRowsInComponent(string jsonString)
	{
		int tag;
		MHPickerView pickerView;
		int component;
		
		Hashtable result = jsonString.hashtableFromJson();
		if(result != null)
		{
			tag = Convert.ToInt32(result["tag"]);
			pickerView = GetObjectByUniqueTag(Convert.ToInt32(result["pickerView"])) as MHPickerView;
			component = Convert.ToInt32(result["component"]);
			
			MHObject obj = GetObjectByUniqueTag(tag);
			if(obj is MHPickerView)
				(obj as MHPickerView)._pickerViewNumberOfRowsInComponent(pickerView, component);
		}
	}
	#endregion
}