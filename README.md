
# Chromecast Unity Project Restoration
In 2015, Google released the Unity Chromecast Plugin, which was quickly soon after deprecated, and then abandoned. Most resources discussing this plugin are missing or had to be reconstructed. Here are some important links:
[Asset Store Package](https://assetstore.unity.com/packages/tools/integration/google-cast-remote-display-plugin-beta-50168)

[Cast Unity Docs](https://developers.google.com/cast/docs/reference/unity/)

[Google Developer Cast Docs](https://developers.google.com/cast/docs/developers)

# Work Done/Changes
Originally, the goal was to restore functionality to the plugin by supplying the missing packages. My first thought was that perhaps the plugin’s packages didn’t exist anymore because of the amount of time that had passed. Upon initially building in Unity I received a build.gradle error.

Upon exporting to the project folder and running in gradle, I noticed that it was missing a few classes. Adding this to the build.gradle file allows it to compile:
```
implementation 'com.google.android.gms:play-services-base:16.1.0'
implementation 'com.google.android.gms:play-services-cast-framework:16.1.2'
implementation 'com.android.volley:volley:1.1.1'
```

but doesn't do much else.

The `play-services-base` and `play-services-cast-framework` seemed to be the most important. Further testing and research showed that Google split the `play-services` package at some point around [this release](https://developers.google.com/android/guides/releases#may_2016_-_version_90), which confuses things further. The package may depend on `com.google.android.gms:play-services-auth:8.3.0`, as that's one of the errors I received, but I think it's unlikely considering I both tested the package and it doesn't make sense considering this was released after API version 19 (KitKat). 

I restored an old archive I found of the codelab project they used and tested the sample project, but it doesn't function correctly anymore, and attempts to restore that project failed as well. Console output to Android Studio or Eclipse is possible by using the included edited .xml manifest to allow it to compile.

At this point the project should build and run in Unity with no errors and the Chromecast should accept its connection, but then nothing. I can't get the callback to fire after its connected to the device, which starts the process of sending the remote texture over, which is very bizarre. The problem seems to be somewhere in the UnityBridge.jar file, and I have included the `classes.jar` file with the zip as well.

## Things To Try

This stuff is stuff I think is important, but I ran out of time or it seemed too farfetched.

- Try different versions of `mediarouter-v7` (I tried v7-18 - v7-24), didn't matter
- Try different versions of `appcompat-v7` (same as above)
- One error I was getting was resolved by adding `multidex-1.0.1`, but someone else suggested `1.0.3`, it's unlikely this makes any difference at all, but I haven't tested it extensively. 
- The new versions of Android require more lines in the .xml manifests. If you download the samples or the packages, add this to the `AndroidManifest.xml` right before the closing `</application` tag:

```
<uses-library android:name="org.apache.http.legacy"  android:required="false"  />
```

Add this to the "manifest" tag:

```
xmlns:android="http://schemas.android.com/apk/res/android"
```
And add this before the closing `</manifest>` tag:
```
<uses-permission  android:name="android.permission.FOREGROUND_SERVICE"  />
```

#
`play-services-4.4.52` was the version I guessed the project used because the dates corrosponded, but I also tested 12.0 (latest), the earliest version, and a few in between. None seemed to change anything and I never received the string

```
Google Play Services out of date.
```
which is in the values.xml file of the GoogleCastRemoteDisplay.aar package, and the library's standard version checking and logging seemed functional, so there's no reason to doubt that any version of `play-services` before 6.5x when they were split into different packages.
