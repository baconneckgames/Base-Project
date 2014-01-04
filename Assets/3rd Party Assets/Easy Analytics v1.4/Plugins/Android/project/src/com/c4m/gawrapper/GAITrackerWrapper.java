package com.c4m.gawrapper;

import android.app.Activity;

import com.google.analytics.tracking.android.Fields;
import com.google.analytics.tracking.android.GAServiceManager;
import com.google.analytics.tracking.android.GoogleAnalytics;
import com.google.analytics.tracking.android.MapBuilder;
import com.google.analytics.tracking.android.Tracker;

/**
 * Google Analytics API V3.01
 * 
 * @author damienpraca
 * 
 */
public class GAITrackerWrapper {

    private static GoogleAnalytics mGaInstance;
    private static Tracker         mGaTracker;

    public static void trackerWithTrackingId(Activity root, String trackingId) {
        mGaInstance = GoogleAnalytics.getInstance(root);

        // GoogleAnalytics.getInstance(root).getLogger().setLogLevel(LogLevel.VERBOSE);
        mGaTracker = mGaInstance.getTracker(trackingId);

    }

    // --------------------------------------
    // Traker
    // --------------------------------------
    public static String getClientId() {
        if (mGaTracker != null) {
            return mGaTracker.get(Fields.CLIENT_ID);
        }
        return null;
    }

    // --------------------------------------
    // Custom Dimensions & Metrics
    // --------------------------------------
    public static void setCustomDimension(int index, String dimension) {
        if (mGaTracker != null) {
            mGaTracker.set(Fields.customDimension(index), dimension);
        }
    }

    public static void setCustomMetric(int index, long metric) {
        if (mGaTracker != null) {
            mGaTracker.set(Fields.customMetric(index), "" + metric);
        }
    }

    // --------------------------------------
    // Event Tracking
    // --------------------------------------
    public static void sendEventWith(String category, String action, String buttonName, long value) {
        if (mGaTracker != null) {
            mGaTracker.send(MapBuilder.createEvent(category, // Event category (required)
                                                   action, // Event action (required)
                                                   buttonName, // Event label
                                                   value) // Event value
                                      .build());
        }
    }

    // --------------------------------------
    // Screen Tracking
    // --------------------------------------
    public static void sendView(String page) {
        if (mGaTracker != null) {
            mGaTracker.send(MapBuilder.createAppView().set(Fields.SCREEN_NAME, page).build());
        }
    }

    // --------------------------------------
    // Sessions
    // --------------------------------------
    public static void setStartSession(boolean startSession) {
        if (mGaTracker != null) {
            if (startSession) {
                mGaTracker.set(Fields.SESSION_CONTROL, "start");
            } else {
                mGaTracker.set(Fields.SESSION_CONTROL, "end");
            }
        }
    }

    // --------------------------------------
    // Crashes & Exceptions
    // --------------------------------------
    public static void sendException(String description, boolean fatal) {
        if (mGaTracker != null) {
            mGaTracker.send(MapBuilder.createException(description, fatal).build());
        }
    }

    // --------------------------------------
    // Social Interactions
    // --------------------------------------
    public static void sendSocial(String network, String action, String target) {
        if (mGaTracker != null) {
            mGaTracker.send(MapBuilder.createSocial(network, // Social network (required)
                                                    action, // Social action (required)
                                                    target) // Social target
                                      .build());
        }
    }

    // --------------------------------------
    // User Timings
    // --------------------------------------
    public static void sendTiming(String category, long intervalInMilliseconds, String name, String label) {
        if (mGaTracker != null) {
            mGaTracker.send(MapBuilder.createTiming(category, // Timing category (required)
                                                    intervalInMilliseconds, // Timing interval in milliseconds (required)
                                                    name, // Timing name
                                                    label) // Timing label
                                      .build());
        }
    }

    // --------------------------------------
    // Ecommerce Tracking
    // --------------------------------------
    public static void sendTransaction(String transactionId,
                                       long orderTotal,
                                       String affiliation,
                                       long totalTax,
                                       long totalShipping,
                                       String currencyCode,
                                       String productSKU,
                                       String productName,
                                       long productPrice,
                                       int quantity,
                                       String ProductCategory) {
        if (mGaTracker != null) {

            mGaTracker.send(MapBuilder.createTransaction(transactionId, // (String) Transaction ID
                                                         affiliation, // (String) Affiliation
                                                         orderTotal * 0.000001d, // (Double) Order revenue
                                                         totalTax * 0.000001d, // (Double) Tax
                                                         totalShipping * 0.000001d, // (Double) Shipping
                                                         (currencyCode != null ? currencyCode : "USD")) // (String) Currency code
                                      .build());

            mGaTracker.send(MapBuilder.createItem(transactionId, // (String) Transaction ID
                                                  productName, // (String) Product name
                                                  productSKU, // (String) Product SKU
                                                  "Game expansions", // (String) Product category
                                                  productPrice * 0.000001d, // (Double) Product price
                                                  (long) quantity, // (Long) Product quantity
                                                  (currencyCode != null ? currencyCode : "USD")) // (String) Currency code
                                      .build());

        }
    }

    // --------------------------------------
    // Campaigns
    // --------------------------------------
    public static void setCampaignUrl(String campaignUrl) {
        MapBuilder paramMap = new MapBuilder();
        paramMap.setCampaignParamsFromUrl(campaignUrl);
        MapBuilder.createAppView().setAll(paramMap.build());
    }

    // --------------------------------------
    // Referrer
    // --------------------------------------
    public static void setReferrer(String url) {
        MapBuilder paramMap = new MapBuilder();
        paramMap.set(Fields.CAMPAIGN_MEDIUM, "referral");
        paramMap.set(Fields.CAMPAIGN_SOURCE, url);
        MapBuilder.createAppView().setAll(paramMap.build());
    }

    // --------------------------------------
    // Dispatch period
    // --------------------------------------
    public static void setDispatchPeriod(int seconds) {
        GAServiceManager.getInstance().setLocalDispatchPeriod(seconds);
    }

    // --------------------------------------
    // Manual dispatch
    // --------------------------------------
    public static void dispatch() {
        GAServiceManager.getInstance().dispatchLocalHits();
    }

}
