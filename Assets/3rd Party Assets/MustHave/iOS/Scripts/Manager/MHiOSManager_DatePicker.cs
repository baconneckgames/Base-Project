using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public partial class MHiOSManager : Singleton<MHiOSManager> {
	#region datepicker
	[DllImport("__Internal")]
    private static extern int _init_datepicker(int tag);
	public MHDatePicker init_datepicker(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return GetObjectByUniqueTag(_init_datepicker(tag)) as MHDatePicker;
		}
		else
			return new MHDatePicker();
	}
	
	[DllImport("__Internal")]
    private static extern int _initWithFrame_datepicker(int tag, string aRect);
	public MHDatePicker initWithFrame_datepicker(int tag, Rect aRect)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string rectJsonString = MHTools.ConvertRectToJSON(aRect);
			
			return GetObjectByUniqueTag(_initWithFrame_datepicker(tag, rectJsonString)) as MHDatePicker;
		}
		else
			return new MHDatePicker();
	}
	
	[DllImport("__Internal")]
    private static extern string _date(int tag);
	public DateTime date(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string dateJsonString = _date(tag);
			
			return MHTools.ConvertJSONToDateTime(dateJsonString);
		}
		else
			return DateTime.Now;
	}
	
	[DllImport("__Internal")]
    private static extern void _date_set(int tag, string dt);
	public void date(int tag, DateTime dt)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string dateJsonString = MHTools.ConvertDateTimeToJSON(dt);
			
			_date_set(tag, dateJsonString);
		}
	}
	
	[DllImport("__Internal")]
    private static extern void _setDate(int tag, string date, bool animated);
	public void setDate(int tag, DateTime date, bool animated)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string dateJsonString = MHTools.ConvertDateTimeToJSON(date);
			
			_setDate(tag, dateJsonString, animated);
		}
	}
	
	[DllImport("__Internal")]
    private static extern string _timeZone(int tag);
	public TimeZoneInfo timeZone(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string zoneJsonString = _timeZone(tag);
			
			return MHTools.ConvertJSONToTimeZone(zoneJsonString);
		}
		else
			return TimeZoneInfo.Local;
	}
	
	[DllImport("__Internal")]
    private static extern void _timeZone_set(int tag, string timeZone);
	public void timeZone(int tag, TimeZoneInfo timeZone)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string zoneJsonString = MHTools.ConvertTimeZoneToJSON(timeZone);
			
			_timeZone_set(tag, zoneJsonString);
		}
	}
	
	[DllImport("__Internal")]
    private static extern int _datePickerMode(int tag);
	public MHDatePickerMode datePickerMode(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return (MHDatePickerMode)_datePickerMode(tag);
		}
		else
			return MHDatePickerMode.MHDatePickerModeDateAndTime;
	}
	
	[DllImport("__Internal")]
    private static extern void _datePickerMode_set(int tag, int mode);
	public void datePickerMode(int tag, MHDatePickerMode mode)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_datePickerMode_set(tag, (int)mode);
		}
	}
	
	[DllImport("__Internal")]
    private static extern string _maximumDate(int tag);
	public DateTime maximumDate(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string dateJsonString = _maximumDate(tag);
			
			return MHTools.ConvertJSONToDateTime(dateJsonString);
		}
		else
			return DateTime.Now;
	}
	
	[DllImport("__Internal")]
    private static extern void _maximumDate_set(int tag, string date);
	public void maximumDate(int tag, DateTime date)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string dateJsonString = MHTools.ConvertDateTimeToJSON(date);
			
			_maximumDate_set(tag, dateJsonString);
		}
	}
	
	[DllImport("__Internal")]
    private static extern string _minimumDate(int tag);
	public DateTime minimumDate(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string dateJsonString = _minimumDate(tag);
			
			return MHTools.ConvertJSONToDateTime(dateJsonString);
		}
		else
			return DateTime.Now;
	}
	
	[DllImport("__Internal")]
    private static extern void _minimumDate_set(int tag, string date);
	public void minimumDate(int tag, DateTime date)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			string dateJsonString = MHTools.ConvertDateTimeToJSON(date);
			
			_minimumDate_set(tag, dateJsonString);
		}
	}
	
	[DllImport("__Internal")]
    private static extern int _minuteInterval(int tag);
	public int minuteInterval(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _minuteInterval(tag);
		}
		else
			return 0;
	}
	
	[DllImport("__Internal")]
    private static extern void _minuteInterval_set(int tag, int interval);
	public void minuteInterval(int tag, int interval)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_minuteInterval_set(tag, interval);
		}
	}
	
	[DllImport("__Internal")]
    private static extern float _countDownDuration(int tag);
	public float countDownDuration(int tag)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			return _countDownDuration(tag);
		}
		else
			return 0f;
	}
	
	[DllImport("__Internal")]
    private static extern void _countDownDuration_set(int tag, float duration);
	public void countDownDuration(int tag, float duration)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
        {
			_countDownDuration_set(tag, duration);
		}
	}
	#endregion
}