//
//  iOSActionManager.h
//  Unity-iPhone
//
//  Created by Patrick Antoine on 9/27/13.
//
//

#import <Foundation/Foundation.h>
#import "MHTools.h"

@interface iOSActionManager : NSObject
{
    NSMutableDictionary *_allActions;
}

@property (nonatomic, assign) NSMutableDictionary *_allActions;

+ (iOSActionManager*)sharedManager;

- (id)init;
- (BOOL) actionExists:(id)sender actionID:(int)action;
- (BOOL) eventExists:(id)sender event:(UIControlEvents)evt;
- (void) addreplaceAction:(id)sender event:(UIControlEvents)evt key:(int)action;
- (void) removeAction:(id)sender actionID:(int)action;
- (int) getAction:(id)sender event:(UIControlEvents)evt;
- (NSArray *) getAllActions:(id)sender event:(UIControlEvents)evt;
- (UIControlEvents) getEvent:(id)sender actionID:(int)action;
- (void) setActionID:(id)sender action:(int)actionID event:(UIControlEvents)evt;
- (void) removeActionID:(id)sender action:(int)actionID event:(UIControlEvents)evt;
- (IBAction)sendActionToUnity_TouchDown:(id)sender;
- (IBAction)sendActionToUnity_TouchDownRepeat:(id)sender;
- (IBAction)sendActionToUnity_TouchDragInside:(id)sender;
- (IBAction)sendActionToUnity_TouchDragOutside:(id)sender;
- (IBAction)sendActionToUnity_TouchDragEnter:(id)sender;
- (IBAction)sendActionToUnity_TouchDragExit:(id)sender;
- (IBAction)sendActionToUnity_TouchUpInside:(id)sender;
- (IBAction)sendActionToUnity_TouchUpOutside:(id)sender;
- (IBAction)sendActionToUnity_TouchCancel:(id)sender;
- (IBAction)sendActionToUnity_ValueChanged:(id)sender;
- (IBAction)sendActionToUnity_EditingDidBegin:(id)sender;
- (IBAction)sendActionToUnity_EditingChanged:(id)sender;
- (IBAction)sendActionToUnity_EditingDidEnd:(id)sender;
- (IBAction)sendActionToUnity_EditingDidEndOnExit:(id)sender;
- (IBAction)sendActionToUnity_AllTouchEvents:(id)sender;
- (IBAction)sendActionToUnity_AllEditingEvents:(id)sender;
- (IBAction)sendActionToUnity_ApplicationReserved:(id)sender;
- (void) performActionsForControlState:(id)sender event:(UIControlEvents)evt;
- (void) performActionByID:(int)actionID;
- (void) performActionByID:(int)actionID param1:(BOOL)param1;

@end
