#import "GAITrackerWrapper.h"
#import <QuartzCore/QuartzCore.h>


@implementation GAITrackerWrapper

// Google Analytics API v3.02
void trackerWithTrackingIdImpl(const char* trackingId) {
        
    // Get a new tracker.
	id newTracker = [[GAI sharedInstance]trackerWithTrackingId:[NSString stringWithCString:trackingId encoding:NSUTF8StringEncoding]];
	//[[GAI sharedInstance].logger setLogLevel:kGAILogLevelVerbose];

	// Set the new tracker as the default tracker, globally.
	[GAI sharedInstance].defaultTracker = newTracker;
}

const char* getClientIdImpl() {
	id tracker = [GAI sharedInstance].defaultTracker;
	NSString* identifier = [tracker get:kGAIClientId];
	return [identifier UTF8String];
	
}

void setCustomDimensionImpl(int index, const char* dimension) {

	id tracker = [GAI sharedInstance].defaultTracker;  // Get the tracker object.
	[tracker set:[GAIFields customDimensionForIndex:index] value:[NSString stringWithCString:dimension encoding:NSUTF8StringEncoding]];
}

void setCustomMetricImpl(int index, int64_t metric) {

	id tracker = [GAI sharedInstance].defaultTracker;  // Get the tracker object.
	[tracker set:[GAIFields customMetricForIndex:index] value:[NSString stringWithFormat:@"%lld",[[NSNumber numberWithLongLong:metric] longLongValue]]];
}

void sendViewImpl(const char* page) {

	id tracker = [GAI sharedInstance].defaultTracker;
	[tracker send:[[[GAIDictionaryBuilder createAppView] set:[NSString stringWithCString:page encoding:NSUTF8StringEncoding] forKey:kGAIScreenName] build]];
}

void sendEventWithImpl(const char* category, const char* action, const char* buttonName, int64_t value) {

	id tracker = [GAI sharedInstance].defaultTracker;
	[tracker send:[[GAIDictionaryBuilder createEventWithCategory:[NSString stringWithCString:category encoding:NSUTF8StringEncoding]    // Event category (required)
                                                      action:[NSString stringWithCString:action encoding:NSUTF8StringEncoding]  		// Event action (required)
                                                       label:[NSString stringWithCString:buttonName encoding:NSUTF8StringEncoding]      // Event label
                                                       value:[NSNumber numberWithInt:value]] build]];    								// Event value
                      
}

void setStartSessionImpl(bool startSession) {
	id<GAITracker> tracker = [GAI sharedInstance].defaultTracker;
	if (tracker) {
		if (startSession) {
			[tracker set:kGAISessionControl value:@"start"];
		} else {
			[tracker set:kGAISessionControl value:@"end"];
		}
    }
}

void sendExceptionImpl(const char* description, bool fatal) {
	id tracker = [GAI sharedInstance].defaultTracker;
	[tracker send:[[GAIDictionaryBuilder createExceptionWithDescription:[NSString stringWithCString:description encoding:NSUTF8StringEncoding] withFatal:[NSNumber numberWithBool:fatal]] build]];
}

void sendSocialImpl(const char* network, const char* action, const char* target) {
	id tracker = [GAI sharedInstance].defaultTracker;
    [tracker send:[[GAIDictionaryBuilder createSocialWithNetwork:[NSString stringWithCString:network encoding:NSUTF8StringEncoding]     // Social network (required)
                  						action:[NSString stringWithCString:action encoding:NSUTF8StringEncoding]            			// Social action (required)
                  						target:[NSString stringWithCString:target encoding:NSUTF8StringEncoding]] build]];  			// Social target
}

void sendTimingImpl(const char* category, const char* nameStr, const char* label, int64_t intervalInMilliseconds) {
	id tracker = [GAI sharedInstance].defaultTracker;                              
    [tracker send:[[GAIDictionaryBuilder createTimingWithCategory:[NSString stringWithCString:category encoding:NSUTF8StringEncoding]   // Timing category (required)
                                         interval:[NSNumber numberWithInt:(intervalInMilliseconds * 0.001)]        								// Timing interval (required)
                                         name:[NSString stringWithCString:nameStr encoding:NSUTF8StringEncoding]  						// Timing name
                                         label:[NSString stringWithCString:label encoding:NSUTF8StringEncoding]] build]];				// Timing label
}

void sendTransactionImpl(const char* transactionId,
			int64_t orderTotal,
			const char* affiliation,
			int64_t totalTax,
			int64_t totalShipping,
			const char* currencyCode,
			const char* productSKU,
			const char* productName,
			int64_t productPrice,
			int quantity,
			const char* productCategory) {
	id tracker = [GAI sharedInstance].defaultTracker;
  	
   [tracker send:[[GAIDictionaryBuilder createTransactionWithId:[NSString stringWithCString:transactionId encoding:NSUTF8StringEncoding]           	// (NSString) Transaction ID
                                        affiliation:[NSString stringWithCString:affiliation encoding:NSUTF8StringEncoding]         					// (NSString) Affiliation
                                        revenue:[NSNumber numberWithDouble:(orderTotal * 0.000001)]                  								// (NSNumber) Order revenue (including tax and shipping)
                                        tax:[NSNumber numberWithDouble:(totalTax * 0.000001)]                 										// (NSNumber) Tax
                                        shipping:[NSNumber numberWithDouble:(totalShipping * 0.000001)]                      						// (NSNumber) Shipping
                                        currencyCode:(currencyCode != nil ? [NSString stringWithCString:currencyCode encoding:NSUTF8StringEncoding] : @"USD")] build]];    			// (NSString) Currency code

   [tracker send:[[GAIDictionaryBuilder createItemWithTransactionId:[NSString stringWithCString:transactionId encoding:NSUTF8StringEncoding]        // (NSString) Transaction ID
                                        name:[NSString stringWithCString:productName encoding:NSUTF8StringEncoding]  								// (NSString) Product Name
                                        sku:[NSString stringWithCString:productSKU encoding:NSUTF8StringEncoding]            						// (NSString) Product SKU
                                        category:[NSString stringWithCString:productCategory encoding:NSUTF8StringEncoding]  						// (NSString) Product category
                                        price:[NSNumber numberWithDouble:(productPrice * 0.000001)]               									// (NSNumber) Product price
                                        quantity:[NSNumber numberWithInt:quantity]                  												// (NSInteger) Product quantity
                                        currencyCode:[NSString stringWithCString:currencyCode encoding:NSUTF8StringEncoding]] build]];				// (NSString) Currency code
  	
  	
}

void setCampaignUrlImpl(const char* campaignUrl) {

	id<GAITracker> tracker = [GAI sharedInstance].defaultTracker;
	if (tracker) {
        GAIDictionaryBuilder *hitParams = [GAIDictionaryBuilder createAppView];
        [hitParams setCampaignParametersFromUrl:[NSString stringWithCString:campaignUrl encoding:NSUTF8StringEncoding]];
  		[tracker send:[hitParams build]];
	}
}

void setReferrerImpl(const char* url) {

	id<GAITracker> tracker = [GAI sharedInstance].defaultTracker;
	if (tracker) {
		GAIDictionaryBuilder *hitParams = [GAIDictionaryBuilder createAppView];
        [hitParams set:@"referrer" forKey:kGAICampaignMedium];
        [hitParams set:[NSString stringWithCString:url encoding:NSUTF8StringEncoding] forKey:kGAICampaignSource];
  		[tracker send:[hitParams build]];
	}
}

void setDispatchPeriodImpl(int seconds) {
    
    [GAI sharedInstance].dispatchInterval = seconds;
}

void dispatchImpl() {
    
    [[GAI sharedInstance] dispatch];
}

	

@end
