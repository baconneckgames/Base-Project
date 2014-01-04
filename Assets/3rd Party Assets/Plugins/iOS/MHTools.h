//
//  MHTools.h
//  Unity-iPhone
//
//  Created by Patrick Antoine on 8/22/13.
//
//
#define Rect_Object_Type @"Rect"
#define Image_Object_Type @"Image"
#define Color_Object_Type @"Color"
#define Vector2_Object_Type @"Vector2"
#define Vector4_Object_Type @"Vector4"
#define Date_Object_Type @"Date"
#define TimeZone_Object_Type @"TimeZone"
#define Action_Object_Type @"Action"

// Converts NSString to C style string by way of copy (Mono will free it)
#define MakeStringCopy( _x_ ) ( _x_ != NULL && [_x_ isKindOfClass:[NSString class]] ) ? strdup( [_x_ UTF8String] ) : NULL

// Converts C style string to NSString
#define GetStringParam( _x_ ) ( _x_ != NULL ) ? [NSString stringWithUTF8String:_x_] : [NSString stringWithUTF8String:""]

// Converts C style string to NSString as long as it isnt empty
#define GetStringParamOrNil( _x_ ) ( _x_ != NULL && strlen( _x_ ) ) ? [NSString stringWithUTF8String:_x_] : nil

#import "JSONKit.h"
#import "iOSViewManager.h"

@interface MHTools : NSObject

+ (CGRect) convertRectToCGRect:(const char *)rect;
+ (const char *) convertCGRectToRect:(CGRect)cgrect;

+ (UIImage *) convertImageToUIImage:(const char *)image;
+ (const char *) convertUIImageToImage:(UIImage *)image;

+ (CGPoint) convertVector2ToCGPoint:(const char *)vector;
+ (const char *) convertCGPointToVector2:(CGPoint)cgpoint;

+ (CGSize) convertVector2ToCGSize:(const char *)vector;
+ (const char *) convertCGSizeToVector2:(CGSize)cgsize;

+ (UIOffset) convertVector2ToUIOffset:(const char *)vector;
+ (const char *) convertUIOffsetToVector2:(UIOffset)offset;

+ (UIEdgeInsets) convertVector4ToUIEdgeInset:(const char *)vector;
+ (const char *) convertUIEdgeInsetToVector4:(UIEdgeInsets)edgeinset;

+ (UIColor *) convertColorToUIColor:(const char *)color;
+ (const char *) convertUIColorToColor:(UIColor *)uicolor;

+ (NSDate *) convertDateTimeToNSDate:(const char *)datetime;
+ (const char *) convertNSDateToDateTime:(NSDate *)date;

+ (NSTimeZone *) convertTimeZoneInfoToNSTimeZone:(const char *)timezoneinfo;
+ (const char *) convertNSTimeZoneToTimeZoneInfo:(NSTimeZone *)timezone;

+ (const char *) convertIDtoAction:(id)actionID,...;

+ (const char *) convertNSArrayToArray_string:(NSArray *)array;
+ (NSArray *) convertArrayToNSArray_string:(const char *)array;

+ (const char *) convertNSArrayToArray_int:(NSArray *)array;
+ (NSArray *) convertArrayToNSArray_int:(int *)array;

+ (const char *) convertNSArrayToMultipleTags:(NSArray *)array;
+ (NSArray *) convertMultipleTagsToNSArray:(int *)tags;

+ (BOOL) objectExists:(int)tag;
+ (BOOL) objectExistsByObject:(id)obj;
+ (BOOL) actualObjectExists:(int)tag;
+ (BOOL) actualObjectExistsByObject:(id)obj;
+ (void) addreplaceObject:(id)obj key:(int)tag actualUI:(id)actualObj;
+ (void) removeObject:(int)tag;
+ (id) getObject:(int)tag;
+ (id) getActualObject:(int)tag;
+ (int) getKey:(id)obj;
+ (int) getKeyFromActual:(id)obj;

+ (id) getView:(int)tag;
+ (id) getActualView:(int)tag;

+ (id) getViewController:(int)tag;
+ (id) getActualViewController:(int)tag;


@end
