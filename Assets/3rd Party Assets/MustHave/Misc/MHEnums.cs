using UnityEngine;
using System.Collections;

public class MHEnums {

}

#region iOS
public enum MHDeviceOrientation
{
	MHDeviceOrientationUnknown = 0,
    MHDeviceOrientationPortrait,
    MHDeviceOrientationPortraitUpsideDown,
    MHDeviceOrientationLandscapeLeft,
    MHDeviceOrientationLandscapeRight,
    MHDeviceOrientationFaceUp,
    MHDeviceOrientationFaceDown
}

public enum MHInterfaceOrientation
{
	MHInterfaceOrientationPortrait = MHDeviceOrientation.MHDeviceOrientationPortrait,
   	MHInterfaceOrientationPortraitUpsideDown = MHDeviceOrientation.MHDeviceOrientationPortraitUpsideDown,
   	MHInterfaceOrientationLandscapeLeft = MHDeviceOrientation.MHDeviceOrientationLandscapeRight,
   	MHInterfaceOrientationLandscapeRight = MHDeviceOrientation.MHDeviceOrientationLandscapeLeft
}

public enum MHInterfaceOrientationMask
{
	MHInterfaceOrientationMaskPortrait = (1 << MHInterfaceOrientation.MHInterfaceOrientationPortrait),
    MHInterfaceOrientationMaskLandscapeLeft = (1 << MHInterfaceOrientation.MHInterfaceOrientationLandscapeLeft),
    MHInterfaceOrientationMaskLandscapeRight = (1 << MHInterfaceOrientation.MHInterfaceOrientationLandscapeRight),
    MHInterfaceOrientationMaskPortraitUpsideDown = (1 << MHInterfaceOrientation.MHInterfaceOrientationPortraitUpsideDown),
    MHInterfaceOrientationMaskLandscape = (MHInterfaceOrientationMaskLandscapeLeft | MHInterfaceOrientationMaskLandscapeRight),
    MHInterfaceOrientationMaskAll = (MHInterfaceOrientationMaskPortrait | MHInterfaceOrientationMaskLandscapeLeft | MHInterfaceOrientationMaskLandscapeRight | MHInterfaceOrientationMaskPortraitUpsideDown),
    MHInterfaceOrientationMaskAllButUpsideDown = (MHInterfaceOrientationMaskPortrait | MHInterfaceOrientationMaskLandscapeLeft | MHInterfaceOrientationMaskLandscapeRight)
}

public enum MHModalTransitionStyle
{
	MHModalTransitionStyleCoverVertical = 0,
	MHModalTransitionStyleFlipHorizontal,
	MHModalTransitionStyleCrossDissolve,
	MHModalTransitionStylePartialCurl
}

public enum MHModalPresentationStyle
{
	MHModalPresentationFullScreen = 0,
	MHModalPresentationPageSheet,
	MHModalPresentationFormSheet,
	MHModalPresentationCurrentContext
}

public enum MHViewAnimationOptions
{
	MHViewAnimationOptionLayoutSubviews            = 1 <<  0,
   	MHViewAnimationOptionAllowUserInteraction      = 1 <<  1,
   	MHViewAnimationOptionBeginFromCurrentState     = 1 <<  2,
   	MHViewAnimationOptionRepeat                    = 1 <<  3,
   	MHViewAnimationOptionAutoreverse               = 1 <<  4,
   	MHViewAnimationOptionOverrideInheritedDuration = 1 <<  5,
   	MHViewAnimationOptionOverrideInheritedCurve    = 1 <<  6,
   	MHViewAnimationOptionAllowAnimatedContent      = 1 <<  7,
   	MHViewAnimationOptionShowHideTransitionViews   = 1 <<  8,
   
   	MHViewAnimationOptionCurveEaseInOut            = 0 << 16,
   	MHViewAnimationOptionCurveEaseIn               = 1 << 16,
   	MHViewAnimationOptionCurveEaseOut              = 2 << 16,
   	MHViewAnimationOptionCurveLinear               = 3 << 16,
   
   	MHViewAnimationOptionTransitionNone            = 0 << 20,
   	MHViewAnimationOptionTransitionFlipFromLeft    = 1 << 20,
   	MHViewAnimationOptionTransitionFlipFromRight   = 2 << 20,
   	MHViewAnimationOptionTransitionCurlUp          = 3 << 20,
   	MHViewAnimationOptionTransitionCurlDown        = 4 << 20,
   	MHViewAnimationOptionTransitionCrossDissolve   = 5 << 20,
   	MHViewAnimationOptionTransitionFlipFromTop     = 6 << 20,
   	MHViewAnimationOptionTransitionFlipFromBottom  = 7 << 20
}

public enum MHPopoverArrowDirection
{
	MHPopoverArrowDirectionUp = 1 << 0,
    MHPopoverArrowDirectionDown = 1 << 1,
    MHPopoverArrowDirectionLeft = 1 << 2,
    MHPopoverArrowDirectionRight = 1 << 3,
    MHPopoverArrowDirectionAny = MHPopoverArrowDirectionUp | MHPopoverArrowDirectionDown | MHPopoverArrowDirectionLeft | MHPopoverArrowDirectionRight,
    MHPopoverArrowDirectionUnknown = int.MaxValue
}

public enum MHViewAutoresizing
{
	MHViewAutoresizingNone                 = 0,
	MHViewAutoresizingFlexibleLeftMargin   = 1 << 0,
	MHViewAutoresizingFlexibleWidth        = 1 << 1,
	MHViewAutoresizingFlexibleRightMargin  = 1 << 2,
	MHViewAutoresizingFlexibleTopMargin    = 1 << 3,
	MHViewAutoresizingFlexibleHeight       = 1 << 4,
	MHViewAutoresizingFlexibleBottomMargin = 1 << 5
}

public enum MHViewContentMode
{
	MHViewContentModeScaleToFill,
	MHViewContentModeScaleAspectFit,
	MHViewContentModeScaleAspectFill,
	MHViewContentModeRedraw,
	MHViewContentModeCenter,
	MHViewContentModeTop,
	MHViewContentModeBottom,
	MHViewContentModeLeft,
	MHViewContentModeRight,
	MHViewContentModeTopLeft,
	MHViewContentModeTopRight,
	MHViewContentModeBottomLeft,
	MHViewContentModeBottomRight
}

public enum MHBarStyle
{
	MHBarStyleDefault          = 0,
	MHBarStyleBlack            = 1,
	
	MHBarStyleBlackOpaque      = 1, // Deprecated
	MHBarStyleBlackTranslucent = 2, // Deprecated
}

public enum MHToolbarPosition
{
	MHToolbarPositionAny = 0,
    MHToolbarPositionBottom = 1,
    MHToolbarPositionTop = 2
}

public enum MHBarMetrics
{
	MHBarMetricsDefault,
	MHBarMetricsLandscapePhone
}

public enum MHBarButtonSystemItem
{
	MHBarButtonSystemItemDone,
	MHBarButtonSystemItemCancel,
	MHBarButtonSystemItemEdit,
	MHBarButtonSystemItemSave,
	MHBarButtonSystemItemAdd,
	MHBarButtonSystemItemFlexibleSpace,
	MHBarButtonSystemItemFixedSpace,
	MHBarButtonSystemItemCompose,
	MHBarButtonSystemItemReply,
	MHBarButtonSystemItemAction,
	MHBarButtonSystemItemOrganize,
	MHBarButtonSystemItemBookmarks,
	MHBarButtonSystemItemSearch,
	MHBarButtonSystemItemRefresh,
	MHBarButtonSystemItemStop,
	MHBarButtonSystemItemCamera,
	MHBarButtonSystemItemTrash,
	MHBarButtonSystemItemPlay,
	MHBarButtonSystemItemPause,
	MHBarButtonSystemItemRewind,
	MHBarButtonSystemItemFastForward,
	MHBarButtonSystemItemUndo,
	MHBarButtonSystemItemRedo,
	MHBarButtonSystemItemPageCurl
}

public enum MHBarButtonItemStyle
{
	MHBarButtonItemStylePlain,
	MHBarButtonItemStyleBordered,
	MHBarButtonItemStyleDone
}

public enum MHControlState
{
	MHControlStateNormal               = 0,
	MHControlStateHighlighted          = 1 << 0,
	MHControlStateDisabled             = 1 << 1,
	MHControlStateSelected             = 1 << 2,
	MHControlStateApplication          = 0x00FF0000
}

public enum MHControlEvents
{
	MHControlEventTouchDown           = 1 <<  0,
	MHControlEventTouchDownRepeat     = 1 <<  1,
	MHControlEventTouchDragInside     = 1 <<  2,
	MHControlEventTouchDragOutside    = 1 <<  3,
	MHControlEventTouchDragEnter      = 1 <<  4,
	MHControlEventTouchDragExit       = 1 <<  5,
	MHControlEventTouchUpInside       = 1 <<  6,
	MHControlEventTouchUpOutside      = 1 <<  7,
	MHControlEventTouchCancel         = 1 <<  8,
	
	MHControlEventValueChanged        = 1 << 12,
	
	MHControlEventEditingDidBegin     = 1 << 16,
	MHControlEventEditingChanged      = 1 << 17,
	MHControlEventEditingDidEnd       = 1 << 18,
	MHControlEventEditingDidEndOnExit = 1 << 19,
	
	MHControlEventAllTouchEvents      = 0x00000FFF,
	MHControlEventAllEditingEvents    = 0x000F0000,
	MHControlEventApplicationReserved = 0x0F000000
}

public enum MHControlContentVerticalAlignment
{
	MHControlContentVerticalAlignmentCenter  = 0,
	MHControlContentVerticalAlignmentTop     = 1,
	MHControlContentVerticalAlignmentBottom  = 2,
	MHControlContentVerticalAlignmentFill    = 3
}

public enum MHControlContentHorizontalAlignment
{
	MHControlContentHorizontalAlignmentCenter = 0,
	MHControlContentHorizontalAlignmentLeft    = 1,
	MHControlContentHorizontalAlignmentRight = 2,
	MHControlContentHorizontalAlignmentFill   = 3
}

public enum MHButtonType
{
	MHButtonTypeCustom = 0,
	MHButtonTypeRoundedRect,
	MHButtonTypeDetailDisclosure,
	MHButtonTypeInfoLight,
	MHButtonTypeInfoDark,
	MHButtonTypeContactAdd
}

public enum MHTextAlignment
{
	MHTextAlignmentLeft      = 0,
	MHTextAlignmentCenter    = 1,
	MHTextAlignmentRight     = 2,
	MHTextAlignmentJustified = 3,
	MHTextAlignmentNatural   = 4
}

public enum MHLineBreakMode
{
	MHLineBreakByWordWrapping = 0,
	MHLineBreakByCharWrapping,
	MHLineBreakByClipping,
	MHLineBreakByTruncatingHead,
	MHLineBreakByTruncatingTail,
	MHLineBreakByTruncatingMiddle
}

public enum MHBaselineAdjustment
{
	MHBaselineAdjustmentAlignBaselines,
	MHBaselineAdjustmentAlignCenters,
	MHBaselineAdjustmentNone
}

public enum MHScrollViewIndicatorStyle
{
	MHScrollViewIndicatorStyleDefault,
	MHScrollViewIndicatorStyleBlack,
	MHScrollViewIndicatorStyleWhite
}

public enum MHTextBorderStyle
{
	MHTextBorderStyleNone,
	MHTextBorderStyleLine,
	MHTextBorderStyleBezel,
	MHTextBorderStyleRoundedRect
}

public enum MHTextFieldViewMode
{
	MHTextFieldViewModeNever,
	MHTextFieldViewModeWhileEditing,
	MHTextFieldViewModeUnlessEditing,
	MHTextFieldViewModeAlways
}

public enum MHDataDetectorType
{
	MHDataDetectorTypePhoneNumber   = 1 << 0,
	MHDataDetectorTypeLink          = 1 << 1,
	MHDataDetectorTypeAddress       = 1 << 2,
	MHDataDetectorTypeCalendarEvent = 1 << 3,
	MHDataDetectorTypeNone          = 0,
	MHDataDetectorTypeAll           = MHDataDetectorTypePhoneNumber | MHDataDetectorTypeLink | MHDataDetectorTypeAddress | MHDataDetectorTypeCalendarEvent
}

public enum MHDatePickerMode
{
	MHDatePickerModeTime,
	MHDatePickerModeDate,
	MHDatePickerModeDateAndTime,
	MHDatePickerModeCountDownTimer
}

public enum MHActionSheetStyle
{
	MHActionSheetStyleAutomatic        = -1,
	MHActionSheetStyleDefault          = MHBarStyle.MHBarStyleDefault,
	MHActionSheetStyleBlackTranslucent = MHBarStyle.MHBarStyleBlackTranslucent,
	MHActionSheetStyleBlackOpaque      = MHBarStyle.MHBarStyleBlackOpaque
}

public enum MHAlertViewStyle
{
	MHAlertViewStyleDefault = 0,
	MHAlertViewStyleSecureTextInput,
	MHAlertViewStylePlainTextInput,
	MHAlertViewStyleLoginAndPasswordInput
}

public enum MHViewAnimationCurve
{
	MHViewAnimationCurveEaseInOut,
	MHViewAnimationCurveEaseIn,
	MHViewAnimationCurveEaseOut,
	MHViewAnimationCurveLinear
}

public enum MHViewAnimationTransition
{
	MHViewAnimationTransitionNone,
	MHViewAnimationTransitionFlipFromLeft,
	MHViewAnimationTransitionFlipFromRight,
	MHViewAnimationTransitionCurlUp,
	MHViewAnimationTransitionCurlDown
}

public enum MHActivityIndicatorStyle
{
	MHActivityIndicatorStyleWhiteLarge,
	MHActivityIndicatorStyleWhite,
	MHActivityIndicatorStyleGray
}

public enum MHProgressViewStyle
{
	MHProgressViewStyleDefault,
	MHProgressViewStyleBar
}
#endregion