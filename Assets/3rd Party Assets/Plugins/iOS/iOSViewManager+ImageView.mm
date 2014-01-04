//
//  iOSViewManager+ImageView.m
//  Unity-iPhone
//
//  Created by Patrick Antoine on 9/19/13.
//
//

#import "iOSViewManager+ImageView.h"
#define CANNOT_FIND_ERROR @"ERROR: Cannot find UIImageView"

@implementation iOSViewManager (ImageView)

@end

extern "C"
{
    int _init_imageview(int tag)
    {
        if(![MHTools objectExists:tag])
        {
            ALBImageView *imageview = [[[ALBImageView alloc] init] autorelease];
            
            [MHTools addreplaceObject:imageview key:tag actualUI:imageview];
        }
        else
        {
            NSLog(@"ERROR: This tag is already taken, cannot initiate this object (IMAGEVIEW)");
        }
        return tag;
	}
	
	int _initWithFrame_imageview(int tag, const char * aRect)
	{
        if(![MHTools objectExists:tag])
        {
            CGRect frame = [MHTools convertRectToCGRect:aRect];
            ALBImageView *imageview = [[[ALBImageView alloc] initWithFrame:frame] autorelease];
            
            [MHTools addreplaceObject:imageview key:tag actualUI:imageview];
        }
        else
        {
            NSLog(@"ERROR: This tag is already taken, cannot initiate this object (IMAGEVIEW)");
        }
        return tag;
	}
	
	int _initWithImage_imageview(int tag, const char * image)
	{
        if(![MHTools objectExists:tag])
        {
            UIImage *img = [MHTools convertImageToUIImage:image];
            ALBImageView *imageview = [[[ALBImageView alloc] initWithImage:img] autorelease];
            
            [MHTools addreplaceObject:imageview key:tag actualUI:imageview];
        }
        else
        {
            NSLog(@"ERROR: This tag is already taken, cannot initiate this object (IMAGEVIEW)");
        }
        return tag;
	}
	
	int _initWithImageHighlightedImage(int tag, const char * image, const char * highlightedImage)
	{
        if(![MHTools objectExists:tag])
        {
            UIImage *img = [MHTools convertImageToUIImage:image];
            UIImage *highlightedimg = [MHTools convertImageToUIImage:highlightedImage];
            ALBImageView *imageview = [[[ALBImageView alloc] initWithImage:img highlightedImage:highlightedimg] autorelease];
            
            [MHTools addreplaceObject:imageview key:tag actualUI:imageview];
        }
        else
        {
            NSLog(@"ERROR: This tag is already taken, cannot initiate this object (IMAGEVIEW)");
        }
        return tag;
	}
	
	const char * _image(int tag)
	{
        UIImageView *imageview = [MHTools getActualObject:tag];
        if(imageview)
        {
            return [MHTools convertUIImageToImage:imageview.image];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	void _image_set(int tag, const char * img)
	{
        UIImageView *imageview = [MHTools getActualObject:tag];
        if(imageview)
        {
            imageview.image = [MHTools convertImageToUIImage:img];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	const char * _highlightedImage(int tag)
	{
        UIImageView *imageview = [MHTools getActualObject:tag];
        if(imageview)
        {
            return [MHTools convertUIImageToImage:imageview.highlightedImage];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return "";
	}
	
	void _highlightedImage_set(int tag, const char * img)
	{
        UIImageView *imageview = [MHTools getActualObject:tag];
        if(imageview)
        {
            imageview.highlightedImage = [MHTools convertImageToUIImage:img];
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	const char * _animationImages(int tag)
	{
        UIImageView *imageview = [MHTools getActualObject:tag];
        if(imageview)
        {
            NSArray *images = imageview.animationImages;
            NSMutableArray *paths = [[NSMutableArray alloc] initWithCapacity:images.count];
            for(int i = 0; i < images.count; i++)
                [paths addObject:GetStringParam([MHTools convertUIImageToImage:images[i]])];
            
            return [MHTools convertNSArrayToArray_string:paths];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return nil;
	}
	
	void _animationImages_set(int tag, const char * animation)
	{
        UIImageView *imageview = [MHTools getActualObject:tag];
        if(imageview)
        {
            NSArray *paths = [MHTools convertArrayToNSArray_string:animation];
            NSMutableArray *images = [[NSMutableArray alloc] initWithCapacity:paths.count];
            for(int i = 0; i < paths.count; i++)
                [images addObject:[MHTools convertImageToUIImage:MakeStringCopy(paths[i])]];
            
            imageview.animationImages = images;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	const char * _highlightedAnimationImages(int tag)
	{
        UIImageView *imageview = [MHTools getActualObject:tag];
        if(imageview)
        {
            NSArray *images = imageview.highlightedAnimationImages;
            NSMutableArray *paths = [[NSMutableArray alloc] initWithCapacity:images.count];
            for(int i = 0; i < images.count; i++)
                [paths addObject:GetStringParam([MHTools convertUIImageToImage:images[i]])];
            
            return [MHTools convertNSArrayToArray_string:paths];
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return nil;
	}
	
	void _highlightedAnimationImages_set(int tag, const char * animation)
	{
        UIImageView *imageview = [MHTools getActualObject:tag];
        if(imageview)
        {
            NSArray *paths = [MHTools convertArrayToNSArray_string:animation];
            NSMutableArray *images = [[NSMutableArray alloc] initWithCapacity:paths.count];
            for(int i = 0; i < paths.count; i++)
                [images addObject:[MHTools convertImageToUIImage:MakeStringCopy(paths[i])]];
            
            imageview.highlightedAnimationImages = images;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	float _animationDuration(int tag)
	{
        UIImageView *imageview = [MHTools getActualObject:tag];
        if(imageview)
        {
            return (float)imageview.animationDuration;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0.0f;
	}
	
	void _animationDuration_set(int tag, float duration)
	{
        UIImageView *imageview = [MHTools getActualObject:tag];
        if(imageview)
        {
            imageview.animationDuration = (double)duration;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
	
	int _animationRepeatCount(int tag)
	{
        UIImageView *imageview = [MHTools getActualObject:tag];
        if(imageview)
        {
            return imageview.animationRepeatCount;
        }
        
        NSLog(CANNOT_FIND_ERROR);
        return 0;
	}
	
	void _animationRepeatCount_set(int tag, int count)
	{
        UIImageView *imageview = [MHTools getActualObject:tag];
        if(imageview)
        {
            imageview.animationRepeatCount = count;
            return;
        }
        
        NSLog(CANNOT_FIND_ERROR);
	}
}
