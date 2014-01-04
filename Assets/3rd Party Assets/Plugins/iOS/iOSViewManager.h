//
//  iOSViewManager.h
//  Unity-iPhone
//
//  Created by Patrick Antoine on 8/6/13.
//
//

#import <Foundation/Foundation.h>
#import "MHTools.h"
#import "ALBLabel.h"
#import "ALBImageView.h"

@interface iOSViewManager: NSObject 
{
    NSMutableDictionary *_allObjects;
    NSMutableDictionary *_allActualUIElements;
    const char *_result;
    void *_instantMessageMethod;
}

@property (nonatomic, assign) NSMutableDictionary *_allObjects;
@property (nonatomic, assign) NSMutableDictionary *_allActualUIElements;
@property (nonatomic, assign) const char *_result;
@property (nonatomic, assign) void *_instantMessageMethod;

+ (iOSViewManager*)sharedManager;
+ (UIViewController*)unityViewController;
+ (void)UnitySendInstantMessage:(const char *)methodName JSONPARAM:(const char*)jsonresult;
- (void)observeValueForKeyPath:(NSString *)keyPath ofObject:(id)object change:(NSDictionary *)change context:(void *)context;
@end
