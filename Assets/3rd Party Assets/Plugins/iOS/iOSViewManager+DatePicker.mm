//
//  iOSViewManager+DatePicker.m
//  Unity-iPhone
//
//  Created by Matthew Scuderi on 9/11/13.
//
//

#import "iOSViewManager+DatePicker.h"
#define CANNOT_FIND_ERROR @"ERROR: Cannot find UIDatePicker"

@implementation iOSViewManager (DatePicker)

@end

extern "C"
{
#pragma mark - datepicker
    int _init_datepicker(int tag)
	{
		if(![MHTools objectExists:tag])
        {
            ALBDatePicker *datepicker = [[[ALBDatePicker alloc] init] autorelease];
            
            [MHTools addreplaceObject:datepicker key:tag actualUI:datepicker];
        }
        else
        {
            NSLog(@"ERROR: This tag is already taken, cannot initiate this object (DATEPICKER)");
        }
        return tag;
	}
	
	int _initWithFrame_datepicker(int tag, const char *aRect)
    {
        if(![MHTools objectExists:tag])
        {
            CGRect frame = [MHTools convertRectToCGRect:aRect];
            ALBDatePicker *datepicker = [[[ALBDatePicker alloc] initWithFrame:frame] autorelease];
            
            [MHTools addreplaceObject:datepicker key:tag actualUI:datepicker];
        }
        else
        {
            NSLog(@"ERROR: This tag is already taken, cannot initiate this object (DATEPICKER)");
        }
        return tag;
    }
    
	const char*  _date(int tag)
	{
		UIDatePicker *datepicker = [MHTools getActualObject:tag];
        if(datepicker)
        {
            NSDate *date = datepicker.date;
            return [MHTools convertNSDateToDateTime:date];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	void _date_set(int tag, const char*  dt)
	{
		UIDatePicker *datepicker = [MHTools getActualObject:tag];
        if(datepicker)
        {
            NSDate *date = [MHTools convertDateTimeToNSDate:dt];
            datepicker.date = date;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	void _setDate(int tag, const char* date, bool animated)
	{
		UIDatePicker *datepicker = [MHTools getActualObject:tag];
        if(datepicker)
        {
            NSDate *nsdate = [MHTools convertDateTimeToNSDate:date];
            [datepicker setDate:nsdate animated:animated];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	const char*  _timeZone(int tag)
	{
		UIDatePicker *datepicker = [MHTools getActualObject:tag];
        if(datepicker)
        {
            NSTimeZone *timezone = datepicker.timeZone;
            return [MHTools convertNSTimeZoneToTimeZoneInfo:timezone];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	void _timeZone_set(int tag, const char*  timeZone)
	{
		UIDatePicker *datepicker = [MHTools getActualObject:tag];
        if(datepicker)
        {
            NSTimeZone *timezone = [MHTools convertTimeZoneInfoToNSTimeZone:timeZone];
            datepicker.timeZone = timezone;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	int _datePickerMode(int tag)
	{
		UIDatePicker *datepicker = [MHTools getActualObject:tag];
        if(datepicker)
        {
            UIDatePickerMode pickermode = datepicker.datePickerMode;
            return (int)pickermode;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return UIDatePickerModeDateAndTime;
	}
	
	void _datePickerMode_set(int tag, int mode)
	{
		UIDatePicker *datepicker = [MHTools getActualObject:tag];
        if(datepicker)
        {
            UIDatePickerMode pickermode = (UIDatePickerMode)mode;
            datepicker.datePickerMode = pickermode;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	const char* _maximumDate(int tag)
	{
		UIDatePicker *datepicker = [MHTools getActualObject:tag];
        if(datepicker)
        {
            NSDate *mdate = datepicker.maximumDate;
            return [MHTools convertNSDateToDateTime:mdate];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	void _maximumDate_set(int tag, const char*  date)
	{
		UIDatePicker *datepicker = [MHTools getActualObject:tag];
        if(datepicker)
        {
            NSDate *mdate = [MHTools convertDateTimeToNSDate:date];
            datepicker.maximumDate = mdate;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	const char*  _minimumDate(int tag)
	{
		UIDatePicker *datepicker = [MHTools getActualObject:tag];
        if(datepicker)
        {
            NSDate *mdate = datepicker.minimumDate;
            return [MHTools convertNSDateToDateTime:mdate];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	void _minimumDate_set(int tag, const char*  date)
	{
		UIDatePicker *datepicker = [MHTools getActualObject:tag];
        if(datepicker)
        {
            NSDate *mdate = [MHTools convertDateTimeToNSDate:date];
            datepicker.minimumDate = mdate;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	int _minuteInterval(int tag)
	{
		UIDatePicker *datepicker = [MHTools getActualObject:tag];
        if(datepicker)
        {
            return datepicker.minuteInterval;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0;
	}
	
	void _minuteInterval_set(int tag, int interval)
	{
		UIDatePicker *datepicker = [MHTools getActualObject:tag];
        if(datepicker)
        {
            datepicker.minuteInterval = interval;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	float _countDownDuration(int tag)
	{
        UIDatePicker *datepicker = [MHTools getActualObject:tag];
        if(datepicker)
        {
            return (float)datepicker.countDownDuration;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0.0f;
	}
	
	void _countDownDuration_set(int tag, float duration)
	{
		UIDatePicker *datepicker = [MHTools getActualObject:tag];
        if(datepicker)
        {
            datepicker.countDownDuration = (double)duration;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
}
