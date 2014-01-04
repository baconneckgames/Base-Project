//
//  ALBBarButtonItem.h
//  Unity-iPhone
//
//  Created by Sammy on 9/20/13.
//
//

#import <Foundation/Foundation.h>
#import "MHTools.h"
#import "ALBView.h"

@interface ALBBarButtonItem : UIBarButtonItem
{
    int _actionID;
}

@property (nonatomic) int actionID;

@end
