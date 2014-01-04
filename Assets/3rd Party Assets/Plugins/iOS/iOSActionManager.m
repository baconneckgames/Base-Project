//
//  iOSActionManager.m
//  Unity-iPhone
//
//  Created by Patrick Antoine on 9/27/13.
//
//

#import "iOSActionManager.h"

@implementation iOSActionManager

@synthesize _allActions;

+ (iOSActionManager*)sharedManager
{
    static iOSActionManager *sharedManager = nil;
    
    if( !sharedManager )
        sharedManager = [[iOSActionManager alloc] init];
    
    return sharedManager;
}

- (id)init
{
    if((self = [super init]))
    {
        _allActions = [[NSMutableDictionary alloc] init];
    }
    return self;
}

#pragma mark actions
- (BOOL) actionExists:(id)sender actionID:(int)action
{
    NSMutableDictionary *sendersActions = [_allActions objectForKey:[NSValue valueWithNonretainedObject:sender]];
    if(!sendersActions)
        return false;
    
    if([sendersActions objectForKey:[NSNumber numberWithInt:action]])
        return true;
    
    return false;
}
- (BOOL) eventExists:(id)sender event:(UIControlEvents)evt
{
    NSMutableDictionary *sendersActions = [_allActions objectForKey:[NSValue valueWithNonretainedObject:sender]];
    if(!sendersActions)
        return false;
    
    if([sendersActions allKeysForObject:[NSNumber numberWithInt:(int)evt]])
        return true;
    
    return false;
}
- (void) addreplaceAction:(id)sender event:(UIControlEvents)evt key:(int)action
{
    if(![_allActions objectForKey:[NSValue valueWithNonretainedObject:sender]])
    {
        NSMutableDictionary *newDictionary = [[[NSMutableDictionary alloc] init] autorelease];
        [_allActions setObject:newDictionary forKey:[NSValue valueWithNonretainedObject:sender]];
    }
    NSMutableDictionary *sendersActions = [_allActions objectForKey:[NSValue valueWithNonretainedObject:sender]];
    
    [sendersActions setObject:[NSNumber numberWithInt:(int)evt] forKey:[NSNumber numberWithInt:action]];
}
- (void) removeAction:(id)sender actionID:(int)action
{
    NSMutableDictionary *sendersActions = [_allActions objectForKey:[NSValue valueWithNonretainedObject:sender]];
    if(!sendersActions)
        return;
    
    [sendersActions removeObjectForKey:[NSNumber numberWithInt:action]];
}
- (int) getAction:(id)sender event:(UIControlEvents)evt
{
    NSMutableDictionary *sendersActions = [_allActions objectForKey:[NSValue valueWithNonretainedObject:sender]];
    if(!sendersActions)
        return nil;
    
    return [[[sendersActions allKeysForObject:[NSNumber numberWithInt:(int)evt]] objectAtIndex:0] integerValue];
}
- (NSArray *) getAllActions:(id)sender event:(UIControlEvents)evt
{
    NSMutableDictionary *sendersActions = [_allActions objectForKey:[NSValue valueWithNonretainedObject:sender]];
    if(!sendersActions)
        return nil;
    
    return [sendersActions allKeysForObject:[NSNumber numberWithInt:(int)evt]];
}
- (UIControlEvents) getEvent:(id)sender actionID:(int)action
{
    NSMutableDictionary *sendersActions = [_allActions objectForKey:[NSValue valueWithNonretainedObject:sender]];
    if(!sendersActions)
        return nil;
    
    return (UIControlEvents)[[sendersActions objectForKey:[NSNumber numberWithInt:action]] intValue];
}

#pragma mark sendactions
- (void) setActionID:(id)sender action:(int)actionID event:(UIControlEvents)evt
{
    [self addreplaceAction:sender event:evt key:actionID];
    [(UIControl *)sender addTarget:self action:[self getCorrectSelector:evt] forControlEvents:evt];
}

- (void) removeActionID:(id)sender action:(int)actionID event:(UIControlEvents)evt
{
    UIControlEvents events = [self getEvent:sender actionID:actionID] ^ evt;
    
    if((int)events == 0)
        [self removeAction:sender actionID:actionID];
    else
        [self addreplaceAction:sender event:evt key:actionID];
    
    [(UIControl *)sender removeTarget:self action:[self getCorrectSelector:evt] forControlEvents:evt];
}

-(SEL)getCorrectSelector:(UIControlEvents) evt
{
    for ( NSInteger i=0; i<sizeof(UIControlEvents)*8; i++ )
    {
        UIControlEvents event = 1 << i;
        if ( !(evt & event) ) continue;
        
        switch (event)
        {
            case UIControlEventAllEditingEvents:
                return @selector(sendActionToUnity_AllEditingEvents:);
            case UIControlEventAllTouchEvents:
                return @selector(sendActionToUnity_AllTouchEvents:);
            case UIControlEventApplicationReserved:
                return @selector(sendActionToUnity_ApplicationReserved:);
            case UIControlEventEditingChanged:
                return @selector(sendActionToUnity_EditingChanged:);
            case UIControlEventEditingDidBegin:
                return @selector(sendActionToUnity_EditingDidBegin:);
            case UIControlEventEditingDidEnd:
                return @selector(sendActionToUnity_EditingDidEnd:);
            case UIControlEventEditingDidEndOnExit:
                return @selector(sendActionToUnity_EditingDidEndOnExit:);
            case UIControlEventTouchCancel:
                return @selector(sendActionToUnity_TouchCancel:);
            case UIControlEventTouchDown:
                return @selector(sendActionToUnity_TouchDown:);
            case UIControlEventTouchDownRepeat:
                return @selector(sendActionToUnity_TouchDownRepeat:);
            case UIControlEventTouchDragEnter:
                return @selector(sendActionToUnity_TouchDragEnter:);
            case UIControlEventTouchDragExit:
                return @selector(sendActionToUnity_TouchDragExit:);
            case UIControlEventTouchDragInside:
                return @selector(sendActionToUnity_TouchDragInside:);
            case UIControlEventTouchDragOutside:
                return @selector(sendActionToUnity_TouchDragOutside:);
            case UIControlEventTouchUpInside:
                return @selector(sendActionToUnity_TouchUpInside:);
            case UIControlEventTouchUpOutside:
                return @selector(sendActionToUnity_TouchUpOutside:);
            case UIControlEventValueChanged:
                return @selector(sendActionToUnity_ValueChanged:);
            default:
                break;
        }
    }
    return nil;
}

- (IBAction)sendActionToUnity_TouchDown:(id)sender
{
    [self performActionsForControlState:sender event:UIControlEventTouchDown];
}
- (IBAction)sendActionToUnity_TouchDownRepeat:(id)sender
{
    [self performActionsForControlState:sender event:UIControlEventTouchDownRepeat];
}
- (IBAction)sendActionToUnity_TouchDragInside:(id)sender
{
    [self performActionsForControlState:sender event:UIControlEventTouchDragInside];
}
- (IBAction)sendActionToUnity_TouchDragOutside:(id)sender
{
    [self performActionsForControlState:sender event:UIControlEventTouchDragOutside];
}
- (IBAction)sendActionToUnity_TouchDragEnter:(id)sender
{
    [self performActionsForControlState:sender event:UIControlEventTouchDragEnter];
}
- (IBAction)sendActionToUnity_TouchDragExit:(id)sender
{
    [self performActionsForControlState:sender event:UIControlEventTouchDragExit];
}
- (IBAction)sendActionToUnity_TouchUpInside:(id)sender
{
    [self performActionsForControlState:sender event:UIControlEventTouchUpInside];
}
- (IBAction)sendActionToUnity_TouchUpOutside:(id)sender
{
    [self performActionsForControlState:sender event:UIControlEventTouchUpOutside];
}
- (IBAction)sendActionToUnity_TouchCancel:(id)sender
{
    [self performActionsForControlState:sender event:UIControlEventTouchCancel];
}
- (IBAction)sendActionToUnity_ValueChanged:(id)sender
{
    [self performActionsForControlState:sender event:UIControlEventValueChanged];
}
- (IBAction)sendActionToUnity_EditingDidBegin:(id)sender
{
    [self performActionsForControlState:sender event:UIControlEventEditingDidBegin];
}
- (IBAction)sendActionToUnity_EditingChanged:(id)sender
{
    [self performActionsForControlState:sender event:UIControlEventEditingChanged];
}
- (IBAction)sendActionToUnity_EditingDidEnd:(id)sender
{
    [self performActionsForControlState:sender event:UIControlEventEditingDidEnd];
}
- (IBAction)sendActionToUnity_EditingDidEndOnExit:(id)sender
{
    [self performActionsForControlState:sender event:UIControlEventEditingDidEndOnExit];
}
- (IBAction)sendActionToUnity_AllTouchEvents:(id)sender
{
    [self performActionsForControlState:sender event:UIControlEventAllTouchEvents];
}
- (IBAction)sendActionToUnity_AllEditingEvents:(id)sender
{
    [self performActionsForControlState:sender event:UIControlEventAllEditingEvents];
}
- (IBAction)sendActionToUnity_ApplicationReserved:(id)sender
{
    [self performActionsForControlState:sender event:UIControlEventApplicationReserved];
}

- (void) performActionsForControlState:(id)sender event:(UIControlEvents)evt
{
    NSArray *actions = [self getAllActions:sender event:evt];
    for (id object in actions) {
        const char *jsonResult = [MHTools convertIDtoAction:object,nil];
        [iOSViewManager UnitySendInstantMessage:"RunActionByUniqueID" JSONPARAM:jsonResult];
    }
}

- (void) performActionByID:(int)actionID
{
    const char *jsonResult = [MHTools convertIDtoAction:[NSNumber numberWithInt:actionID],nil];
    [iOSViewManager UnitySendInstantMessage:"RunActionByUniqueID" JSONPARAM:jsonResult];
}

- (void) performActionByID:(int)actionID param1:(BOOL)param1
{
    const char *jsonResult = [MHTools convertIDtoAction:[NSNumber numberWithInt:actionID],[NSNumber numberWithBool:param1],nil];
    [iOSViewManager UnitySendInstantMessage:"RunActionByUniqueID" JSONPARAM:jsonResult];
}
@end
