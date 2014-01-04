using UnityEngine;
using System.Collections;
using System;

public class MHDatePicker : MHControl {
	#region functions
	public MHDatePicker(){}
	
	public MHDatePicker(bool autoInitialize)
	{
		if(Application.isPlaying && autoInitialize)
			init();
	}
	
	public MHDatePicker(Rect aRect)
	{
		if(Application.isPlaying)
			initWithFrame(aRect);
	}
	
	new public MHDatePicker init()
	{
		return MHiOSManager.Instance.init_datepicker(tag);
	}
	
	new public MHDatePicker initWithFrame(Rect aRect)
	{
		return MHiOSManager.Instance.initWithFrame_datepicker(tag, aRect);
	}
	
	public DateTime date {
		get {
			return MHiOSManager.Instance.date(tag);
		} set {
			MHiOSManager.Instance.date(tag, value);
		}
	}
	
	public void setDate(DateTime date, bool animated)
	{
		MHiOSManager.Instance.setDate(tag, date, animated);
	}
	
	public TimeZoneInfo timeZone {
		get {
			return MHiOSManager.Instance.timeZone(tag);
		} set {
			MHiOSManager.Instance.timeZone(tag, value);
		}
	}
	
	public MHDatePickerMode datePickerMode {
		get {
			return MHiOSManager.Instance.datePickerMode(tag);
		} set {
			MHiOSManager.Instance.datePickerMode(tag, value);
		}
	}
	
	public DateTime maximumDate {
		get {
			return MHiOSManager.Instance.maximumDate(tag);
		} set {
			MHiOSManager.Instance.maximumDate(tag, value);
		}
	}
	
	public DateTime minimumDate {
		get {
			return MHiOSManager.Instance.minimumDate(tag);
		} set {
			MHiOSManager.Instance.minimumDate(tag, value);
		}
	}
	
	public int minuteInterval {
		get {
			return MHiOSManager.Instance.minuteInterval(tag);
		} set {
			MHiOSManager.Instance.minuteInterval(tag, value);
		}
	}
	
	public float countDownDuration {
		get {
			return MHiOSManager.Instance.countDownDuration(tag);
		} set {
			MHiOSManager.Instance.countDownDuration(tag, value);
		}
	}
	#endregion
}
