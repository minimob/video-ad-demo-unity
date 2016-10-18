<div id="D-Entire-document">
<div id="D-adtag-integration">
<h2>Integrating VAST Video Ads using JavaScript Ad Tags</h2>
   <p>The JavaScript Ad Tag is a static script for requesting and displaying an ad within an HTML-based page. For implementing this, it is required to insert the JavaScript Ad Tag into the html code of a mobile web application or mobile in-app web controller.</p>
   <p>Note that this method is intended for real time ad serving and presentation, between the Minimob ad servers and mobile devices.  It cannot be used for server-to-server batch retrieval of ads. For each ad impression, a separate request must be made to the ad servers, supplying all the required information plus any additional data, if available. By specifying values at the parameters included at the ad tag, an application can forward extra information towards the ad servers, which can be useful for delivering better targeted ads.</p>
   <p>In case of preloading ads to an app, the lifetime of ads delivered should not exceed 5 minutes. If an application is open for periods longer than this duration, the app should refresh the presented ads at regular intervals well under that limit.</p>
<h3>Prerequisites</h3>
   <p>Before you proceed, make sure that you already have:</p>
<ol>
    <li>Registered to Minimob </li>
    <li>Created an app under <strong>Monetize &gt; Video Ads</strong> </li>
    <li>Created an ad zone under an app </li>
</ol>
   <p>Then, follow the instructions given in this guide for enabling your app to request and display video ads from Minimob.</p>
<h3>Workflow overview</h3>
   <p>When developing your app, you need to carry out the following tasks:</p>
<ol>
    <li>Update the app’s manifest file</li>
    <li>Import the Minimob ad-serving module to your project</li>
    <li>Use the imported module for requesting, loading and displaying video ads from Minimob </li>
</ol>
<h3>Updating the app’s android manifest file</h3>
   <p>At the app’s android manifest file, add the following line in the <code>&lt;application&gt;</code> node (it enables hardware-accelerated rendering for all activities and views in the application):</p>
<pre class="prettyprint linenums">
<code>android:hardwareAccelerated="true"
</code>
</pre>
   <p>Furthermore, make sure that the following required permissions are added:</p>
<pre class="prettyprint linenums">
<code>&lt;uses-permission android:name="android.permission.ACCESS_FINE_LOCATION" /&gt;
&lt;uses-permission android:name="android.permission.ACCESS_COARSE_LOCATION" /&gt;
&lt;uses-permission android:name="android.permission.INTERNET" /&gt;
&lt;uses-permission android:name="android.permission.READ_PHONE_STATE" /&gt;
</code>
</pre>
<h3>Importing the Minimob ad-serving module to your project</h3>
   <p>To import the code to Unity:</p>
<ol>
    <li>Download the <strong>minimobvideounity.unitypackage</strong> file</li>
    <li>Open your unity project and go to <strong>Assets &gt; Import Package &gt; Custom Package</strong></li>
    <li>Select the <strong>minimobvideo.unitypackage</strong> file and click <strong>OK</strong>.</li>
</ol>
   <p>All the files that are needed will be automatically imported.</p>
<h3>Requesting, loading and displaying video ads from Minimob</h3>
   <p>To play video ads from Minimob, you need to create an empty game object in your main scene and add the <strong>MinimobVideoAdPlayer</strong> script to it. Note that this script is a singleton and will remain in memory after you load another scene.</p>
   <p>Two distinct video playing modes are supported:</p>
<ul>
    <li><strong>Immediate Video ad</strong>: the video ad is played immediately after being downloaded (a single call is used for loading and showing a video ad) </li>
    <li><strong>Preloaded video ad</strong>: the video ad is preloaded and it is played upon a separate request (two separate calls are used, one for loading a video ad and another for showing the video ad) </li>
</ul>
<h5>Immediate Video ad</h5>
   <p>To play a video ad, you must use the <strong>CreateVideo</strong> method of <strong>MinimobVideoAdPlayer</strong>. The method takes the following parameters:</p>
<ul>
    <li>The 1st parameter is the <strong>AdTagString</strong> that your get from your <strong>Minimob Dashboard</strong> for your <strong>AdZone</strong>.</li>
    <li>The 2nd parameter is a string of custom tracking data that you want to pass.</li>
    <li>The 3rd parameter is a callback that will be called when the <strong>AdZone</strong> is created.</li>
    <li>The 4th parameter specifies whether the video will be preloaded or not.</li>
</ul>
   <p>When the creation callback is called you can call <strong>ShowVideo()</strong> directly to play the video.</p>
   <p>At the point in your code where you want to show the video ad, include the following lines.</p>
<pre class="prettyprint linenums">
<code>// Copy the JavaScript Ad Tag that is given at the corresponding Ad Zone under Monetize &gt; Video Ads of the Minimob dashboard, and paste it at the adTagString
    public string AdTagString = "&lt;script&gt; \n" +
                        " var mmAdTagSettings = { \n" +
                        " imei: \"[imei]\", \n" +
                        " android_id: \"[android_id]\", \n" +
                        " gaid: \"[gaid]\", \n" +
                        " idfa: \"[idfa]\", \n" +
                        " idfv: \"[idfv]\", \n" +
                        " category: \"[category]\", \n" +
                        " age: \"[age]\", \n" +
                        " gender: \"[gender]\", \n" +
                        " keywords: \"[keywords]\", \n" +
                        " lat: \"[lat]\", \n" +
                        " lon: \"[lon]\", \n" +
                        " device_width: \"[device_width]\", \n" +
                        " device_height: \"[device_height]\", \n" +
                        " mnc: \"[mnc]\", \n" +
                        " mcc: \"[mcc]\", \n" +
                        " wifi: \"[wifi]\", \n" +
                        " ios_version: \"[ios_version]\", \n" +
                        " android_version: \"[android_version]\", \n" +
                        " placement_width: \"[placement_width]\", \n" +
                        " placement_height: \"[placement_height]\", \n" +
                        " preload: \"[preload]\", \n" +
                        " custom_tracking_data: \"[custom_tracking_data]\"}; \n" +
                        " \n" +
                        " var mmAdTagSettings_auto = { \n" +
                        " adzoneId:\"571793a200000a\", \n" +
                        " templateId: \"video-fullscreen2.html\", \n" +
                        " mobile_web: false, \n" +
                        " video_supported: true, \n" +
                        " appId: \"57174ada000002\", \n" +
                        " bundleId: \"com.minimob.addemos\", \n" +
                        " placement: \"video fullscreen interstitial\"}; \n" +
                        " &lt;/script&gt; \n" +
                        " &lt;script id=\"sdk-loader\" onerror=\"if(typeof(mmji)!='undefined'){mmji.noAds()}\" type=\"text/javascript\" src=\"http://s.rtad.bid/assets/video-fullscreen-mmji.js\"&gt;&lt;/script&gt;";

    void Start ()
    {
        var videoPlayer = MinimobVideoAdPlayer.GetInstance();

        // Optionally, set the delegates for the supported events:
        // ads available, ads NOT available, video playing, video finished, video closed
        // This enables you to customize the user experience according to the needs of your app
        // OnAdsAvailableAction is called when there are ads available and will be invoked after a call to ShowVideo() or PreloadVideo()
        videoPlayer.OnAdsAvailableAction = () =&gt;
        {
        };
        // OnAdsNotAvailableAction is called when there are no ads available and it will be invoked after a call to ShowVideo() or PreloadVideo()
        videoPlayer.OnAdsNotAvailableAction = () =&gt;
        {
        };
        // OnVideoPlayingAction is called when the video starts playing
        videoPlayer.OnVideoPlayingAction = () =&gt;
        {
        };
        // OnVideoFinishedAction is called when the video has finished playing.
        // This means that the user watched the whole video and it is a good place to give a reward if you want to incentivize the action.
        videoPlayer.OnVideoFinishedAction = () =&gt;
        {
        };
        // OnVideoClosedAction is called when the user closes the Video Ad before it has finished
        videoPlayer.OnVideoClosedAction = () =&gt;
        {
        };
    }

    // Create video
    videoPlayer.CreateVideo(AdTagString, CustomTrackingData,()=&gt;
    {
        // Show the video
        videoPlayer.ShowVideo();
    }
    ,false);
</code>
</pre>
<h5>Preloaded video ad</h5>
   <p>To preload and play a video ad, you must use the <strong>CreateVideo</strong> method of <strong>MinimobVideoAdPlayer</strong>. The method takes the following parameters:</p>
<ul>
    <li>The 1st parameter is the <strong>AdTagString</strong> that your get from your <strong>Minimob Dashboard</strong> for your <strong>AdZone</strong>.</li>
    <li>The 2nd parameter is a string of custom tracking data that you want to pass.</li>
    <li>The 3rd parameter is a callback that will be called when the <strong>AdZone</strong> is created.</li>
    <li>The 4th parameter specifies whether the video will be preloaded or not.</li>
</ul>
   <p>When the creation callback is called you can call <strong>LoadVideo()</strong> to preload the video.</p>
   <p>At the point in your code where you want to preload the video ad, include the following lines.</p>
<pre class="prettyprint linenums">
<code>// Copy the JavaScript Ad Tag that is given at the corresponding Ad Zone under Monetize &gt; Video Ads of the Minimob dashboard, and paste it at the adTagString
    public string AdTagString = "&lt;script&gt; \n" +
                        " var mmAdTagSettings = { \n" +
                        " imei: \"[imei]\", \n" +
                        " android_id: \"[android_id]\", \n" +
                        " gaid: \"[gaid]\", \n" +
                        " idfa: \"[idfa]\", \n" +
                        " idfv: \"[idfv]\", \n" +
                        " category: \"[category]\", \n" +
                        " age: \"[age]\", \n" +
                        " gender: \"[gender]\", \n" +
                        " keywords: \"[keywords]\", \n" +
                        " lat: \"[lat]\", \n" +
                        " lon: \"[lon]\", \n" +
                        " device_width: \"[device_width]\", \n" +
                        " device_height: \"[device_height]\", \n" +
                        " mnc: \"[mnc]\", \n" +
                        " mcc: \"[mcc]\", \n" +
                        " wifi: \"[wifi]\", \n" +
                        " ios_version: \"[ios_version]\", \n" +
                        " android_version: \"[android_version]\", \n" +
                        " placement_width: \"[placement_width]\", \n" +
                        " placement_height: \"[placement_height]\", \n" +
                        " preload: \"[preload]\", \n" +
                        " custom_tracking_data: \"[custom_tracking_data]\"}; \n" +
                        " \n" +
                        " var mmAdTagSettings_auto = { \n" +
                        " adzoneId:\"571793a200000a\", \n" +
                        " templateId: \"video-fullscreen2.html\", \n" +
                        " mobile_web: false, \n" +
                        " video_supported: true, \n" +
                        " appId: \"57174ada000002\", \n" +
                        " bundleId: \"com.minimob.addemos\", \n" +
                        " placement: \"video fullscreen interstitial\"}; \n" +
                        " &lt;/script&gt; \n" +
                        " &lt;script id=\"sdk-loader\" onerror=\"if(typeof(mmji)!='undefined'){mmji.noAds()}\" type=\"text/javascript\" src=\"http://s.rtad.bid/assets/video-fullscreen-mmji.js\"&gt;&lt;/script&gt;";

    void Start ()
    {
        var videoPlayer = MinimobVideoAdPlayer.GetInstance();

        // Optionally, set the delegates for the supported events:
        // ads available, ads NOT available, video loading, video loaded, video playing, video finished, video closed
        // This enables you to customize the user experience according to the needs of your app
        // OnAdsAvailableAction is called when there are ads available and will be invoked after a call to ShowVideo() or LoadVideo()
        videoPlayer.OnAdsAvailableAction = () =&gt;
        {
        };
        // OnAdsNotAvailableAction is called when there are no ads available and it will be invoked after a call to ShowVideo() or LoadVideo()
        videoPlayer.OnAdsNotAvailableAction = () =&gt;
        {
        };
        // OnVideoLoadingAction is called when the video is loading
        videoPlayer.OnVideoLoadingAction = () =&gt;
        {
        };
        // OnVideoLoadedAction is called when the video has finished loading
        videoPlayer.OnVideoLoadedAction = () =&gt;
        {
        };
        // OnVideoPlayingAction is called when the video starts playing
        videoPlayer.OnVideoPlayingAction = () =&gt;
        {
        };
        // OnVideoFinishedAction is called when the video has finished playing.
        // This means that the user watched the whole video and it is a good place to give a reward if you want to incentivize the action.
        videoPlayer.OnVideoFinishedAction = () =&gt;
        {
        };
        // OnVideoClosedAction is called when the user closes the Video Ad before it has finished
        videoPlayer.OnVideoClosedAction = () =&gt;
        {
        };
    }

    // Create video
    videoPlayer.CreateVideo(AdTagString, CustomTrackingData, ()=&gt;
    {
        // Load the video
        videoPlayer.LoadVideo();
    }
    ,true);
</code>
</pre>
   <p>Once the video has finished loading, the <strong>OnVideoLoadedAction</strong> callback will be invoked.</p>
   <p>At the point in your code where you want to play the video, you need to call the <strong>ShowVideo()</strong> method.</p>
<pre class="prettyprint linenums">
<code>// Show the video
videoPlayer.ShowVideo();
</code>
</pre>
<h3>Ad Tag parameters</h3>
   <p>The JavaScript Ad Tag of an ad zone holds the following variables:</p>
<ul>
    <li><strong>mmAdTagSettings</strong>: this variable contains parameters that are mainly used for passing device or user information; such information can then be utilized by Minimob in order to return more suitable and relevant ads.</li>
    <li><strong>mmAdTagSettings_auto</strong>: this variable contains parameters that are required by the platform; they are automatically generated by Minimob.</li>
</ul>
<blockquote><strong>Warning</strong>: When copying the Ad Tag script of an Ad Zone and pasting it into your code, <strong>you should NOT delete or modify any of the parameters</strong> of the script. If you need to specify age, gender, category or any custom information, use the respective methods as described in the previous sections (<em>see</em> the inline comments at the code that you need to include).</blockquote>
   <p>The parameters of each variable are described below.</p>
<h5>mmAdTagSettings Parameters</h5>
<table>
  <tr>
    <th>Parameter</th>
    <th>Type</th>
    <th>Description</th>
    <th>Example</th>
  </tr>
  <tr>
    <td>imei</td>
    <td>String</td>
    <td>The International Mobile Station Equipment Identity number that uniquely identifies mobile phones<br />
Hashed MD5 or SHA1</td>
    <td>"d41d8cd98f00b204e9800998ecf8427e"</td>
  </tr>
  <tr>
    <td>android_id</td>
    <td>String</td>
    <td>[android only]<br />
The Android ID of the device<br />
Hashed MD5 or SHA1</td>
    <td>"d3b4f06fc2bd14b417f39f7d7e72f47f"</td>
  </tr>
  <tr>
    <td>gaid</td>
    <td>String </td>
    <td>[android only]<br />
Raw (clear text) Google Advertising ID</td>
    <td>"3D016490-C470-4B04-99AD-B4FFF3330D46"</td>
  </tr>
  <tr>
    <td>idfa</td>
    <td>String </td>
    <td>[iOS only]<br />
Raw (clear text) Apple’s Identifier For Advertisers (IFA/IDFA)</td>
    <td>"236A005B-700F-4889-B9CE-999EAB2B605D"</td>
  </tr>
  <tr>
    <td>idfv</td>
    <td>String </td>
    <td>[iOS only]<br />
Raw (clear text) Apple’s Identifier For Vendor (IFA/IDFA)</td>
    <td>"599F9C00-92DC-4B5C-9464-7971F01F8370"</td>
  </tr>
  <tr>
    <td>category</td>
    <td>Integer<br />
comma separated multiple values are allowed</td>
    <td>The category ID of advertised apps, as defined by <a href="http://www.iab.com/wp-content/uploads/2016/03/OpenRTB-API-Specification-Version-2-4-FINAL.pdf" target="_blank">IAB</a>.<br />
Category is used for filtering the ads. Several comma separated values can be specified to filter for multiple categories. Only ads from apps that belong to the specified categories will be served. </td>
    <td>"IAB1-1,IAB2"</td>
  </tr>
  <tr>
    <td>age</td>
    <td>Integer</td>
    <td>The age of the device user<br />
If omitted (i.e. null), it implies "unknown".</td>
    <td>"37"</td>
  </tr>
  <tr>
    <td>gender</td>
    <td>String<br />
M = male, F = female, O = known to be other</td>
    <td>The gender of the device user <br />
If omitted (i.e. null), it implies "unknown". </td>
    <td>"F"</td>
  </tr>
  <tr>
    <td>keywords</td>
    <td>String<br />
comma separated multiple values are allowed</td>
    <td>List of keywords to filter ads by (e.g. list of interests of the device user)</td>
    <td>"fashion,sailing"</td>
  </tr>
  <tr>
    <td>lat</td>
    <td>Float <br />
from -90.0 to +90.0, where negative is south</td>
    <td>Latitude of the mobile device<br />
Should be used together with the <strong>lon</strong> parameter.</td>
    <td>"+10.2"</td>
  </tr>
  <tr>
    <td>lon</td>
    <td>Float <br />
from -180.0 to +180.0, where negative is west</td>
    <td>Longitude of the mobile device <br />
Should be used together with the <strong>lat</strong> parameter.</td>
    <td>"-102.8"</td>
  </tr>
  <tr>
    <td>device_width</td>
    <td>Integer</td>
    <td>The width of the mobile device, in physical pixels</td>
    <td>"1440"</td>
  </tr>
  <tr>
    <td>device_height</td>
    <td>Integer</td>
    <td>The height of the mobile device, in physical pixels</td>
    <td>"2560"</td>
  </tr>
  <tr>
    <td>mnc</td>
    <td>Integer</td>
    <td>The mobile network code of the mobile network operator of the mobile device<br />
It is used in combination with the mobile country code (MCC) to uniquely identify the mobile phone operator/carrier.</td>
    <td>"260"</td>
  </tr>
  <tr>
    <td>mcc</td>
    <td>Integer</td>
    <td>The mobile country code of the mobile network operator of the mobile device<br />
It is used in combination with the mobile network code (MNC) to uniquely identify the mobile phone operator/carrier.</td>
    <td>" 310"</td>
  </tr>
  <tr>
    <td>wifi</td>
    <td>Boolean</td>
    <td>The network connection type of the mobile device <br />
true: indicates that the device is currently connected to the internet via WiFi<br />
false: indicates that the current network connection of the device is NOT via WiFi </td>
    <td>"true"</td>
  </tr>
  <tr>
    <td>ios_version</td>
    <td>String</td>
    <td>[iOS only]<br />
The iOS version of the operating system of the device </td>
    <td>"7.1.2"</td>
  </tr>
  <tr>
    <td>android_version</td>
    <td>String</td>
    <td>[android only]<br />
The android version of the operating system of the device</td>
    <td>"4.4.2"</td>
  </tr>
  <tr>
    <td>placement_width</td>
    <td>Integer</td>
    <td>The width of the advertising area within the app, where the ads are placed, in pixels</td>
    <td>"720"</td>
  </tr>
  <tr>
    <td>placement_height</td>
    <td>Integer</td>
    <td>The height of the advertising area within the app, where the ads are placed, in pixels</td>
    <td>"1280"</td>
  </tr>
  <tr>
    <td>preload</td>
    <td>Boolean</td>
    <td>true: indicates that the requested video ad will be preloaded<br />
false: indicates that the requested video ad will NOT be preloaded</td>
    <td>"true"</td>
  </tr>
  <tr>
    <td>custom_tracking_data</td>
    <td>String</td>
    <td>Custom information, defined by the app developer </td>
    <td>"00000000-5a19-dc1b-ffff-ffffef05ac4a"</td>
  </tr>
</table>
<h5>mmAdTagSettings_auto Parameters</h5>
<table>
  <tr>
    <th>Parameter</th>
    <th>Type</th>
    <th>Description</th>
    <th>Example</th>
  </tr>
  <tr>
    <td>adzoneId</td>
    <td>string</td>
    <td>The unique id of the particular ad zone</td>
    <td>"56f1727aFAKEc3"</td>
  </tr>
  <tr>
    <td>templateId</td>
    <td>string</td>
    <td>The id of the template of the ad zone, i.e. the html code that will be used for rendering the ads.</td>
    <td> "video-fullscreen2.html"</td>
  </tr>
  <tr>
    <td>mobile_web</td>
    <td>string</td>
    <td>true: for web app supported ads <br />
false: [default] for in-app supported ads </td>
    <td> false</td>
  </tr>
  <tr>
    <td>video_supported</td>
    <td>string</td>
    <td>true: [default] for allowing video media supported ads <br />
false: for blocking video media supported ads</td>
    <td> true</td>
  </tr>
  <tr>
    <td>appId</td>
    <td>string</td>
    <td>The unique id of the app to which the particular ad zone belongs</td>
    <td> "56d57201FAKE2e"</td>
  </tr>
  <tr>
    <td>bundleId</td>
    <td>string</td>
    <td>The bundle id of the app, as it is registered to the app store</td>
    <td> "com.minimob.addemos "</td>
  </tr>
  <tr>
    <td>placement</td>
    <td>string</td>
    <td>The placement type of the ad</td>
    <td> "video fullscreen interstitial" </td>
  </tr>
</table>
<h3>Examples</h3>
   <p>Here is an indicative example of an Ad Tag.</p>
<blockquote><strong>Warning</strong>: Dummy Ids have been used in this example, so do not use it as is. Please navigate to Minimob’s Monetization dashboard for Video Ads and use the ad tag that is displayed at the details of the corresponding Ad Zone.</blockquote>
<pre class="prettyprint linenums">
<code>&lt;script&gt;
    var mmAdTagSettings = {
        imei: "[imei]",
        android_id: "[android_id]",
        gaid: "[gaid]",
        idfa: "[idfa]",
        idfv: "[idfv]",
        category: "[category]",
        age: "[age]",
        gender: "[gender]",
        keywords: "[keywords]",
        lat: "[lat]",
        lon: "[lon]",
        device_width: "[device_width]",
        device_height: "[device_height]",
        mnc: "[mnc]",
        mcc: "[mcc]",
        wifi: "[wifi]",
        ios_version: "[ios_version]",
        android_version: "[android_version]",
        placement_width: "[placement_width]",
        placement_height: "[placement_height]",
        custom_tracking_data: "[custom_tracking_data]"
    };

    var mmAdTagSettings_auto = {
        adzoneId: "This-is-a-FAKE-adzoneId",
        templateId: "video-fullscreen2.html",
        mobile_web: false,
        video_supported: true,
        appId: "This-is-a-FAKE-appId",
        bundleId: "com.minimob.addemos",
        placement: "video fullscreen interstitial"
    };
&lt;/script&gt;
&lt;script id="sdk-loader" onerror="if(typeof(mmji)!='undefined'){mmji.noAds()}" type="text/javascript" src="http://s.rtad.bid/assets/video-fullscreen-mmji.js"&gt;&lt;/script&gt;
</code>
</pre>
   <p>Here is an indicative example of generated html code.</p>
<blockquote><strong>Warning</strong>: Dummy Ids have been used in this example.</blockquote>
<pre class="prettyprint linenums">
<code>&lt;html&gt;
    &lt;head&gt;
        &lt;meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1"&gt;
    &lt;/head&gt;
    &lt;body style="background-color:#000000"&gt;
        &lt;script&gt;
            var mmAdTagSettings = {
                imei: "d41d8cd98f00b204e9800998ecf8427e",
                android_id: "d3b4f06fc2bd14b417f39f7d7e72f47f",
                gaid: "af13cb72-fafa-4328-b86c-d48875a4561a",
                idfa: "[idfa]",
                idfv: "[idfv]",
                category: "[category]",
                age: "[age]",
                gender: "[gender]",
                keywords: "[keywords]",
                lat: "0.0",
                lon: "0.0",
                device_width: "1080",
                device_height: "1794",
                mnc: "260",
                mcc: "310",
                wifi: "false",
                ios_version: "[ios_version]",
                android_version: "23",
                placement_width: "1080",
                placement_height: "1794",
                preload: "false",
                custom_tracking_data: "[custom_tracking_data]"};

            var mmAdTagSettings_auto = {
                adzoneId:"This-is-a-FAKE-adzoneId",
                templateId: "video-fullscreen2.html",
                mobile_web: false,
                video_supported: true,
                appId: "This-is-a-FAKE-appId",
                bundleId: "com.minimob.addemos",
                placement: "video fullscreen interstitial"};
        &lt;/script&gt;
        &lt;script id="sdk-loader" onerror="if(typeof(mmji)!='undefined'){mmji.noAds()}" type="text/javascript" src="http://s.rtad.bid/assets/video-fullscreen-mmji.js"&gt;&lt;/script&gt;
    &lt;/body&gt;
&lt;/html&gt;
</code>
</pre>
</div>
<div id="D-adtag-license">
<h2>License</h2>
   <p>This is free and unencumbered software released into the public domain.</p>
   <p>Anyone is free to copy, modify, publish, use, compile, sell, or<br />
distribute this software, either in source code form or as a compiled<br />
binary, for any purpose, commercial or non-commercial, and by any<br />
means.</p>
   <p>In jurisdictions that recognize copyright laws, the author or authors<br />
of this software dedicate any and all copyright interest in the<br />
software to the public domain. We make this dedication for the benefit<br />
of the public at large and to the detriment of our heirs and<br />
successors. We intend this dedication to be an overt act of<br />
relinquishment in perpetuity of all present and future rights to this<br />
software under copyright law.</p>
   <p>THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,<br />
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF<br />
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.<br />
IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR<br />
OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,<br />
ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR<br />
OTHER DEALINGS IN THE SOFTWARE.</p>
   <p>For more information, please refer to <a href="http://unlicense.org/" target="_blank">http://unlicense.org</a></p>
</div>
</div>
