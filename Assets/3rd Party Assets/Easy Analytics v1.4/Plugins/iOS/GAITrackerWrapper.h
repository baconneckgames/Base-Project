#import <UIKit/UIKit.h>
#import "GAI.h"
#import "GAITracker.h"
#import "GAIFields.h"
#import "GAIDictionaryBuilder.h"

@interface GAITrackerWrapper : NSObject
{
    id myDefault;
}

#ifdef __cplusplus
extern "C" {
#endif

    void trackerWithTrackingIdImpl(const char* trackingId);
    const char* getClientIdImpl();
    void setCustomDimensionImpl(int index, const char* dimension);
    void setCustomMetricImpl(int index, int64_t metric);
    void sendEventWithImpl(const char* category, const char* action, const char* buttonName, int64_t value);
    void sendViewImpl(const char* page);
    void setStartSessionImpl(bool startSession);
	void sendExceptionImpl(const char* description, bool fatal);
	void sendSocialImpl(const char* network, const char* action, const char* target);
	void sendTimingImpl(const char* category, const char* nameStr, const char* label, int64_t intervalInMilliseconds);
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
			const char* productCategory);
	void setCampaignUrlImpl(const char* campaignUrl);
	void setReferrerImpl(const char* url);
	void setDispatchPeriodImpl(int seconds);
	void dispatchImpl();
    
#ifdef __cplusplus
}
#endif


@end
