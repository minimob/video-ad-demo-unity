<h3>Installation</h3>
<p>Steps to import the code to Unity</p>
<ol>
  <li>Donwload the minimobvideounity.unitypackage file</li>
  <li>Open your unity project and  go to Assets->Import Package->Custom Package</li>
  <li>Select the minimobvideo.unitypackage file</li>
  <li>Click ok</li>
</ol>
<p>all the files that are needed will be automatically imported.</p>

<p>Please note that if you already have an AndroidManifest.xml in your project you might want to uncheck the AndroidManifest.xml file before you import it.</p>
<p>The only changes that you will need on your manifest are:</p>
<ol>
  <li>Add the android:hardwareAccelerated="true" attribute in your application tag. (This is required for the video player to work properly.)</li>
  <li>Add the following permissions if you don't already have them:
      <uses-permission android:name="android.permission.ACCESS_FINE_LOCATION" />
      <uses-permission android:name="android.permission.ACCESS_COARSE_LOCATION" />
      <uses-permission android:name="android.permission.INTERNET" />
      <uses-permission android:name="android.permission.READ_PHONE_STATE" />
  </li>
</ol>
<h3>How To Use</h3>
<p>Create an empty game object in your main scene and add the MinimobVideoAdPlayer script to it. Note that this script is a singleton and will remain in memory after you load another scene.</p>

<h4>There are two ways you can play your Video Ad:</h4>

  <h5>Normal video ad</h5>

<p>To play a video ad you must first create the video:</p>

<pre class="prettyprint linenums">
<code>
    MinimobVideoAdPlayer.GetInstance().CreateVideo(AdTagString, CustomTrackingData,()=>
    {
        Debug.Log("MinimobVideoExample:Video created succesfully.Showing video...");
        videoPlayer.ShowVideo();
    }
    ,false);
</code>
</pre>

<p>
the 1st parameter is the AdTagString that your get from your Minimob Dashboard for your AdZone. 
the 2nd parameter is a string of custom tracking data that you want to pass.
the 3rd parameter is a callback that will be called when the adzone is created.
the 4th parameter specifies whether the video will be preloaded or not.
</p>

<p>When the creation callback is called you can call ShowVideo() directly to play the video.</p>

<h5>Preloaded video Ad</h5>

<p>Your ad is preloaded and once the preloading is finished your are notified with a callback. To preload a video you must call the CreateVideo function like before:</p>

<pre class="prettyprint linenums">
<code>
    MinimobVideoAdPlayer.GetInstance().CreateVideo(AdTagString, CustomTrackingData,()=>
    {
        Debug.Log("MinimobVideoExample:Video created succesfully.Showing video...");
        videoPlayer.PreloadVideo();
    }
    ,true);
</code>
</pre>

<p>But this time once the video creation callback is called you start the preloading process instead. Once the video has finished preloading the OnVideoPreloadedAction callback will be invoked. You can then play the video from this callback by calling ShowVideo() or wait to call it when the user performs an action.</p>

<pre class="prettyprint linenums">
<code>
    MinimobVideoAdPlayer.GetInstance().OnVideoPreloadedAction = ()=>
    {
        PreloadingVideoGameObject.SetActive(false);
        Debug.Log("MinimobVideoExample:Video preloaded succesfully. Showing video...");
        videoPlayer.ShowVideo();
    };
</code>
</pre>

<h3>Callback actions</h3>
<p>There are several events that you can listen to if you like.</p>

<ol>

<li>OnAdsNotAvailableAction
	<p>This is called when there are no ads available and it will be invoked after a call to ShowVideo() or PreloadVideo().</p>
</li>
<li>OnAdsAvailableAction
	<p>This is called when there are ads available and will be invoked after a call to ShowVideo() or PreloadVideo().</p>
</li>
<li>OnVideoPlayingAction 
  <p>This is called when the video starts playing.</p>
</li>
<li>OnVideoFinishedAction
    <p>This is called when the video finished playing. This means that the user watched the whole video and it is a good place to give a reward if you want to incentivize the action.</p>
</li>
	
<li>OnVideoClosedAction
  <p>This is called if the user closes the Video Ad before it finishes.</p>
</li>
<li>OnVideoPreloadedAction
	<p>This is relevant only for preloaded video ads and is called when the preloaded video is ready to play.</p>
</li>

<p>To listen to an event simply set a handler for it. For example:
<pre class="prettyprint linenums">
<code>
    MinimobVideoAdPlayer.GetInstance().OnAdsNotAvailableAction = ()=>
    {
        Debug.Log("MinimobVideoExample:no videos are currently available...");
    };
</code>
</pre>
</p>

<p>You can also check the example code located in 
Assets\MinimobVideoPlayer\Example\
</p>
