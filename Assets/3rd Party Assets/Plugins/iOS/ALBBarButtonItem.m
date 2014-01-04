//
//  ALBBarButtonItem.m
//  Unity-iPhone
//
//  Created by Sammy on 9/20/13.
//
//

#import "ALBBarButtonItem.h"

@implementation ALBBarButtonItem

@synthesize actionID = _actionID;

#pragma mark actions
- (void) setActionID:(int)actionID
{
    _actionID = actionID;
    if(_actionID != 0)
        self.action = @selector(sendActionToUnity:);
    else
        self.action = nil;
}

-(IBAction)sendActionToUnity:(id)sender
{
    const char *jsonResult = [MHTools convertIDtoAction:[NSNumber numberWithInt:_actionID],nil];
    [iOSViewManager UnitySendInstantMessage:"RunActionByUniqueID" JSONPARAM:jsonResult];
}

@end
