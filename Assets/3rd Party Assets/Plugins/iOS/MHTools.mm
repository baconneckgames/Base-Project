//
//  MHTools.m
//  Unity-iPhone
//
//  Created by Patrick Antoine on 8/22/13.
//
//

#import "MHTools.h"

UIViewController *UnityGetGLViewController();

@implementation MHTools

// Rects
+ (CGRect) convertRectToCGRect:(const char *)rect
{
    NSDictionary *dict = nil;
    NSString *rectjson = GetStringParamOrNil(rect);
    
    if( rectjson )
        dict = [rectjson objectFromJSONString];
    
    NSString *objType = [dict objectForKey:@"ObjectType"];
    if([objType isEqualToString:Rect_Object_Type])
    {
        CGRect newRect = CGRectMake([[dict objectForKey:@"X"] floatValue], [[dict objectForKey:@"Y"] floatValue], [[dict objectForKey:@"Width"] floatValue], [[dict objectForKey:@"Height"] floatValue]);
        return newRect;
    }
    return CGRectNull;
}

+ (const char *) convertCGRectToRect:(CGRect)cgrect
{
    if(CGRectIsNull(cgrect))
        return MakeStringCopy(@"");
    
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          Rect_Object_Type, @"ObjectType",
                          [NSNumber numberWithFloat:cgrect.origin.x], @"X",
                          [NSNumber numberWithFloat:cgrect.origin.y], @"Y",
                          [NSNumber numberWithFloat:cgrect.size.width], @"Width",
                          [NSNumber numberWithFloat:cgrect.size.height], @"Height",
                          nil];
    return MakeStringCopy([dict JSONString]);
}


// Images
+ (UIImage *) convertImageToUIImage:(const char *)image
{
    NSDictionary *dict = nil;
    NSString *imagejson = GetStringParamOrNil(image);
    
    if( imagejson )
        dict = [imagejson objectFromJSONString];
    
    NSString *objType = [dict objectForKey:@"ObjectType"];
    if([objType isEqualToString:Image_Object_Type])
    {
        
        NSString *fullImagePath = [dict objectForKey:@"Path"];
        
        // early out if the file doesnt exist
        if( ![[NSFileManager defaultManager] fileExistsAtPath:fullImagePath] )
            return nil;
        
        UIImage *image = [UIImage imageWithContentsOfFile:fullImagePath];
        
        // early out if we dont have in image
        if( !image )
            return nil;
        return image;
    }
    return nil;
}

+ (const char *) convertUIImageToImage:(UIImage *)image
{
    NSString *fileName = [self createUniqueFileName:@"image"];
    
    NSArray *paths = NSSearchPathForDirectoriesInDomains(NSCachesDirectory, NSUserDomainMask, YES);
    NSString *filePath = [[paths objectAtIndex:0] stringByAppendingPathComponent:[NSString stringWithFormat:@"%@/%@", @"MHiOS", fileName]];
    
    NSData *data = [NSData dataWithData:UIImagePNGRepresentation(image)];
    NSError *writeError = nil;
    
    [data writeToFile:filePath options:NSDataWritingAtomic error:&writeError];
    
    if (writeError) {
        NSLog(@"Error writing file: %@", writeError);
    }
    
    return MakeStringCopy(filePath);
}

+(NSString *) createUniqueFileName:(NSString *) prefix
{
    NSString *guid = [[NSProcessInfo processInfo] globallyUniqueString] ;
    NSString *uniqueFileName = [NSString stringWithFormat:@"%@_%@", prefix, guid];
    
    return uniqueFileName;
}

// Points and Sizes
+ (CGPoint) convertVector2ToCGPoint:(const char *)vector
{
    NSDictionary *dict = nil;
    NSString *vectorjson = GetStringParamOrNil(vector);
    
    if( vectorjson )
        dict = [vectorjson objectFromJSONString];
    
    NSString *objType = [dict objectForKey:@"ObjectType"];
    if([objType isEqualToString:Vector2_Object_Type])
    {
        CGPoint newPoint = CGPointMake([[dict objectForKey:@"X"] floatValue], [[dict objectForKey:@"Y"] floatValue]);
        return newPoint;
    }
    return CGPointZero;
}

+ (const char *) convertCGPointToVector2:(CGPoint)cgpoint
{
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          Vector2_Object_Type, @"ObjectType",
                          [NSNumber numberWithFloat:cgpoint.x], @"X",
                          [NSNumber numberWithFloat:cgpoint.y], @"Y",
                          nil];
    return MakeStringCopy([dict JSONString]);

}

+ (CGSize) convertVector2ToCGSize:(const char *)vector
{
    NSDictionary *dict = nil;
    NSString *vectorjson = GetStringParamOrNil(vector);
    
    if( vectorjson )
        dict = [vectorjson objectFromJSONString];
    
    NSString *objType = [dict objectForKey:@"ObjectType"];
    if([objType isEqualToString:Vector2_Object_Type])
    {
        CGSize newSize = CGSizeMake([[dict objectForKey:@"X"] floatValue], [[dict objectForKey:@"Y"] floatValue]);
        return newSize;
    }
    return CGSizeZero;
}

+ (const char *) convertCGSizeToVector2:(CGSize)cgsize
{
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          Vector2_Object_Type, @"ObjectType",
                          [NSNumber numberWithFloat:cgsize.width], @"X",
                          [NSNumber numberWithFloat:cgsize.height], @"Y",
                          nil];
    return MakeStringCopy([dict JSONString]);
}

+ (UIOffset) convertVector2ToUIOffset:(const char *)vector
{
    NSDictionary *dict = nil;
    NSString *vectorjson = GetStringParamOrNil(vector);
    
    if( vectorjson )
        dict = [vectorjson objectFromJSONString];
    
    NSString *objType = [dict objectForKey:@"ObjectType"];
    if([objType isEqualToString:Vector2_Object_Type])
    {
        UIOffset newOffset = UIOffsetMake([[dict objectForKey:@"X"] floatValue], [[dict objectForKey:@"Y"] floatValue]);
        return newOffset;
    }
    return UIOffsetZero;
}

+ (const char *) convertUIOffsetToVector2:(UIOffset)offset
{
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          Vector2_Object_Type, @"ObjectType",
                          [NSNumber numberWithFloat:offset.horizontal], @"X",
                          [NSNumber numberWithFloat:offset.vertical], @"Y",
                          nil];
    return MakeStringCopy([dict JSONString]);
    
}

+ (UIEdgeInsets) convertVector4ToUIEdgeInset:(const char *)vector
{
    NSDictionary *dict = nil;
    NSString *vectorjson = GetStringParamOrNil(vector);
    
    if( vectorjson )
        dict = [vectorjson objectFromJSONString];
    
    NSString *objType = [dict objectForKey:@"ObjectType"];
    if([objType isEqualToString:Vector4_Object_Type])
    {
        UIEdgeInsets inset = UIEdgeInsetsMake([[dict objectForKey:@"X"] floatValue], [[dict objectForKey:@"Y"] floatValue], [[dict objectForKey:@"Z"] floatValue], [[dict objectForKey:@"W"] floatValue]);
        return inset;
    }
    return UIEdgeInsetsZero;
}

+ (const char *) convertUIEdgeInsetToVector4:(UIEdgeInsets)edgeinset
{
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          Vector4_Object_Type, @"ObjectType",
                          [NSNumber numberWithFloat:edgeinset.top], @"X",
                          [NSNumber numberWithFloat:edgeinset.left], @"Y",
                          [NSNumber numberWithFloat:edgeinset.bottom], @"Z",
                          [NSNumber numberWithFloat:edgeinset.right], @"W",
                          nil];
    return MakeStringCopy([dict JSONString]);
}

// Colors
+ (UIColor *) convertColorToUIColor:(const char *)color
{
    NSDictionary *dict = nil;
    NSString *colorjson = GetStringParamOrNil(color);
    
    if( colorjson )
        dict = [colorjson objectFromJSONString];
    
    NSString *objType = [dict objectForKey:@"ObjectType"];
    if([objType isEqualToString:Color_Object_Type])
    {
        UIColor *newColor = [UIColor colorWithRed:[[dict objectForKey:@"R"] floatValue] green:[[dict objectForKey:@"G"] floatValue] blue:[[dict objectForKey:@"B"] floatValue] alpha:[[dict objectForKey:@"A"] floatValue]];
        return newColor;
    }
    return nil;
}

+ (const char *) convertUIColorToColor:(UIColor *)uicolor
{
    if(!uicolor)
        return MakeStringCopy(@"");
    CGFloat r = 0, g = 0, b = 0, a = 0;
    
    CGColorRef colorRef = [uicolor CGColor];
    
    int _countComponents = CGColorGetNumberOfComponents(colorRef);
    
    if (_countComponents == 4) {
        const CGFloat *_components = CGColorGetComponents(colorRef);
        r = _components[0];
        g = _components[1];
        b = _components[2];
        a = _components[3];
    }
    else
        return MakeStringCopy(@"");
    
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          Color_Object_Type, @"ObjectType",
                          [NSNumber numberWithFloat:r], @"R",
                          [NSNumber numberWithFloat:g], @"G",
                          [NSNumber numberWithFloat:b], @"B",
                          [NSNumber numberWithFloat:a], @"A",
                          nil];
    return MakeStringCopy([dict JSONString]);
}

// Dates

+ (NSDate *) convertDateTimeToNSDate:(const char *)datetime
{
    NSDictionary *dict = nil;
    NSString *datejson = GetStringParamOrNil(datetime);
    
    if( datejson )
        dict = [datejson objectFromJSONString];
    
    NSString *objType = [dict objectForKey:@"ObjectType"];
    if([objType isEqualToString:Date_Object_Type])
    {
        NSDateComponents *comps = [[[NSDateComponents alloc] init] autorelease];
        [comps setDay:[[dict objectForKey:@"Day"] intValue]];
        [comps setMonth:[[dict objectForKey:@"Month"] intValue]];
        [comps setYear:[[dict objectForKey:@"Year"] intValue]];
        [comps setHour:[[dict objectForKey:@"Hour"] intValue]];
        [comps setMinute:[[dict objectForKey:@"Minute"] intValue]];
        [comps setSecond:[[dict objectForKey:@"Second"] intValue]];
        NSDate *newDate = [[NSCalendar currentCalendar] dateFromComponents:comps];
        return newDate;
    }
    return [NSDate date];
}

+ (const char *) convertNSDateToDateTime:(NSDate *)date
{
    NSDateComponents *components = [[NSCalendar currentCalendar] components:NSDayCalendarUnit | NSMonthCalendarUnit | NSYearCalendarUnit | NSHourCalendarUnit | NSMinuteCalendarUnit | NSSecondCalendarUnit fromDate:date];
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          Date_Object_Type, @"ObjectType",
                          [NSNumber numberWithInt:[components day]], @"Day",
                          [NSNumber numberWithInt:[components month]], @"Month",
                          [NSNumber numberWithInt:[components year]], @"Year",
                          [NSNumber numberWithInt:[components hour]], @"Hour",
                          [NSNumber numberWithInt:[components minute]], @"Minute",
                          [NSNumber numberWithInt:[components second]], @"Second",
                          nil];
    return MakeStringCopy([dict JSONString]);
}

+ (NSTimeZone *) convertTimeZoneInfoToNSTimeZone:(const char *)timezoneinfo
{
    NSDictionary *dict = nil;
    NSString *timezonejson = GetStringParamOrNil(timezoneinfo);
    
    if( timezonejson )
        dict = [timezonejson objectFromJSONString];
    
    NSString *objType = [dict objectForKey:@"ObjectType"];
    if([objType isEqualToString:TimeZone_Object_Type])
    {
        NSTimeZone *timezone = [NSTimeZone timeZoneWithAbbreviation:[[dict objectForKey:@"Name"] stringValue]];
        return timezone;
    }
    return [NSTimeZone localTimeZone];
}

+ (const char *) convertNSTimeZoneToTimeZoneInfo:(NSTimeZone *)timezone
{
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:
                          TimeZone_Object_Type, @"ObjectType",
                          [timezone abbreviation], @"Name",
                          nil];
    return MakeStringCopy([dict JSONString]);
}
// Actions
+ (const char *) convertIDtoAction:(id)actionID,...
{
    NSMutableDictionary *dict = [[NSMutableDictionary alloc] initWithObjectsAndKeys:
                          Action_Object_Type, @"ObjectType",
                          actionID, @"ID",
                          nil];
    
    int count = 1;
    id arg;
    va_list args;
    if(actionID)
    {
        va_start(args, actionID);
        while((arg = va_arg(args, id)))
        {
            [dict setObject:arg forKey:[NSString stringWithFormat:@"Param%d",count]];
            count++;
        }
        va_end(args);
    }
    return MakeStringCopy([dict JSONString]);
}

// Misc
+ (const char *) convertNSArrayToArray_string:(NSArray *)array
{
    return MakeStringCopy([array JSONString]);
}

+ (NSArray *) convertArrayToNSArray_string:(const char *)array
{
    NSMutableArray *newArray2 = [GetStringParamOrNil(array) objectFromJSONString];

    return newArray2;
}

+ (const char *) convertNSArrayToArray_int:(NSArray *)array
{
    return MakeStringCopy([array JSONString]);
}

+ (NSArray *) convertArrayToNSArray_int:(int *)array
{
    int arrayCount = sizeof(array) / sizeof(array[0]);
    
    NSMutableArray *newArray=[[NSMutableArray alloc] initWithCapacity:arrayCount];
    for(int i = 0; i < arrayCount; i++)
    {
        NSNumber *arrayItem = [NSNumber numberWithInt:array[i]];
        if(arrayItem)
            [newArray addObject: arrayItem];
    }
    
    return newArray;
}

+ (const char *) convertNSArrayToMultipleTags:(NSArray *)array
{
    NSMutableArray *newArray = [[NSMutableArray alloc] initWithCapacity:array.count];
    for(int i = 0; i < array.count; i++)
        [newArray setObject:[NSNumber numberWithInt:[MHTools getKeyFromActual:[array objectAtIndex:i]]] atIndexedSubscript:i];
    
    return MakeStringCopy([newArray JSONString]);
}

+ (NSArray *) convertMultipleTagsToNSArray:(int *)tags
{
    int tagCount = sizeof(tags) / sizeof(tags[0]);
    
    NSMutableArray *newArray=[[NSMutableArray alloc] initWithCapacity:tagCount];
    for(int i = 0; i < tagCount; i++)
    {
        [newArray addObject: [MHTools getActualObject:tags[i]]];
    }
    
    return newArray;
}

// Object Handling
+ (BOOL) objectExists:(int)tag
{
    if(tag == 0)
        return true;
    if([[[iOSViewManager sharedManager] _allObjects] objectForKey:[NSNumber numberWithInt:tag]])
        return true;
    return false;
}
+ (BOOL) objectExistsByObject:(id)obj
{
    if(obj == UnityGetGLViewController() || obj == [UnityGetGLViewController() view])
        return true;
    if(![[[[iOSViewManager sharedManager] _allObjects] allKeysForObject:obj] count])
        return false;
    return true;
}
+ (BOOL) actualObjectExists:(int)tag
{
    if(tag == 0)
        return true;
    if([[[iOSViewManager sharedManager] _allActualUIElements] objectForKey:[NSNumber numberWithInt:tag]])
        return true;
    return false;
}
+ (BOOL) actualObjectExistsByObject:(id)obj
{
    if(obj == UnityGetGLViewController() || obj == [UnityGetGLViewController() view])
        return true;
    if(![[[[iOSViewManager sharedManager] _allActualUIElements] allKeysForObject:obj] count])
        return false;
    return true;
}
+ (void) addreplaceObject:(id)obj key:(int)tag actualUI:(id)actualObj
{
    if(tag == 0)
        return;
    [[[iOSViewManager sharedManager] _allObjects] setObject:obj forKey:[NSNumber numberWithInt:tag]];
    [[[iOSViewManager sharedManager] _allActualUIElements] setObject:actualObj forKey:[NSNumber numberWithInt:tag]];
}
+ (void) removeObject:(int)tag
{
    if(tag == 0)
        return;
    [[[iOSViewManager sharedManager] _allObjects] removeObjectForKey:[NSNumber numberWithInt:tag]];
    [[[iOSViewManager sharedManager] _allActualUIElements] removeObjectForKey:[NSNumber numberWithInt:tag]];
}
+ (id) getObject:(int)tag
{
    if(tag == 0)
        return UnityGetGLViewController();
    return [[[iOSViewManager sharedManager] _allObjects] objectForKey:[NSNumber numberWithInt:tag]];
}
+ (id) getActualObject:(int)tag
{
    if(tag == 0)
        return UnityGetGLViewController();
    return [[[iOSViewManager sharedManager] _allActualUIElements] objectForKey:[NSNumber numberWithInt:tag]];
}
+ (int) getKey:(id)obj
{
    if(obj == UnityGetGLViewController() || obj == [UnityGetGLViewController() view])
        return 0;
    return [[[[[iOSViewManager sharedManager] _allObjects] allKeysForObject:obj] objectAtIndex:0] integerValue];
}
+ (int) getKeyFromActual:(id)obj
{
    if(obj == UnityGetGLViewController() || obj == [UnityGetGLViewController() view])
        return 0;
    return [[[[[iOSViewManager sharedManager] _allActualUIElements] allKeysForObject:obj] objectAtIndex:0] integerValue];
}

+ (id) getView:(int)tag
{
    if(tag == 0)
        return [UnityGetGLViewController() view];
    else
        return (UIView *)[self getObject:tag];
}
+ (id) getActualView:(int)tag
{
    if(tag == 0)
        return [UnityGetGLViewController() view];
    else
        return (UIView *)[self getActualObject:tag];
}

+ (id) getViewController:(int)tag
{
    if(tag == 0)
        return UnityGetGLViewController();
    else
        return (UIViewController *)[self getObject:tag];
}
+ (id) getActualViewController:(int)tag
{
    if(tag == 0)
        return UnityGetGLViewController();
    else
        return (UIViewController *)[self getActualObject:tag];
}

@end
