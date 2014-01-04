# Easy Analytics Unity 3D plugin for Android, iOS and Web
Easy Analytics is a ready to use plugin which wraps the 2 mobile Android and iPhone Google Analytics SDK v2 (Beta) and the Web (analytics.js) into a unified common C# API.

# Usage

## Installation

* Import the module in Unity or copy the 'Assets' folder content to your Unity project (Asset > Import Package > Custom Package...).
* Drag and drop the EasyAnalyticsExample.cs on a GameObject or camera
* On Android only : 
	- Make sure the bundle identifier in the player settings is the one declared in Assets/Plugins/Android/AndroidManifest.xml in the player settings (com.c4mprod.easyga).
	- Add the 2 required permissions in your own Manifest.xml file for an existing project.
* Build and Run


## WebPlayer

To user EasyAnalytics with the webplayer, you need to 
* unzip the Assets/Plugins/WebPlayer/gawebwrapper.js.zip and put the uncompressed js file next to the generated HTML file 
* Add <script type="text/javascript" src="gawebwrapper.js"></script> in the header section of the generated HTML file.


##Compilation

### iOS

* Build the plugin in Unity3D (switch to iOS and 'Build')
* In the generated XCode project in 'Build Phases' > 'Link Binary With Libraries'
    - Add CoreData.framework
    - Add libsqlite3.0.dylib 
    - set Architecture to 'Standard armv7'
* Build and Run with XCode 