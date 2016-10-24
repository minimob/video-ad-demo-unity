using UnityEngine;
using UnityEngine.Events;
using Debug = UnityEngine.Debug;

public class MinimobAdServing : MonoBehaviour
{
    private UnityAction _onAdZoneCreatedAction;
    public UnityAction onAdsAvailableAction;
    public UnityAction onAdsNotAvailableAction;
    public UnityAction onVideoLoadingAction; // pre-loaded only
    public UnityAction onVideoLoadedAction; // pre-loaded only
    public UnityAction onVideoPlayingAction;
    public UnityAction onVideoFinishedAction;
    public UnityAction onVideoClosedAction;

    private bool _adZoneCreated = false;
    private bool _preloadVideo = false;

    private static MinimobAdServing _instance = null;

    public static MinimobAdServing GetInstance()
    {
        //Debug.Log("MinimobAdServing:GetInstance()");
        if (_instance == null)
        {
            var go = new GameObject();
            _instance = go.AddComponent<MinimobAdServing>();
            go.name = "MinimobAdServing";
            DontDestroyOnLoad(go);
        }
        return _instance;
    }

    public void Awake()
    {
        //Debug.Log("MinimobAdServing:Awake()");
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
        gameObject.name = "MinimobAdServing";
    }

    public void CreateAdZone(string adTagString, string customTrackingData, UnityAction onAdZoneCreatedAction, bool preloadVideo)
    {
        Debug.Log("MinimobAdServing:CreateAdZone()");
        /*
        //Debug.Log("MinimobAdServing:adTagString:" + adTagString);
        //Debug.Log("MinimobAdServing:preloadVideo:"+ preloadVideo);
        // If the AdZone was already created and the mode is the same (immediate OR preloaded video), then execute the onAdZoneCreatedAction
        if (_adZoneCreated && _preloadVideo == preloadVideo)
        {
            if (onAdZoneCreatedAction != null)
            {
                Debug.Log("MinimobAdServing:Calling onAdZoneCreatedAction()");
                onAdZoneCreatedAction();
            }
            return;
        }
        // set the mode (immediate OR preloaded video)
        _preloadVideo = preloadVideo;
        */
        // set the action for when the viceo is created
        _onAdZoneCreatedAction = onAdZoneCreatedAction;
        /*
        // reset the adZone to not created YET
        _adZoneCreated = false;
        */

#if UNITY_ANDROID && !UNITY_EDITOR
        using (var adPlayerJavaClass = new AndroidJavaClass("com.minimob.adserving.unityplugin.MinimobAdServingUnityPlugin"))
        {
            using (var adPlayerObject = adPlayerJavaClass.CallStatic<AndroidJavaObject>("GetInstance"))
            {
                adPlayerObject.Call("CreateAdZone", adTagString, customTrackingData, preloadVideo);
            }
        };
#endif
    }

    /// <summary>
    /// Only call this to preload videos 
    /// </summary>
    public void LoadVideo()
    {
        Debug.Log("MinimobAdServing:LoadVideo()");
        if (!_adZoneCreated)
        {
            Debug.LogError("MinimobAdServing:Load Video called before Video was created");
            return;
        }

#if UNITY_ANDROID && !UNITY_EDITOR
        using (var adPlayerJavaClass = new AndroidJavaClass("com.minimob.adserving.unityplugin.MinimobAdServingUnityPlugin"))
        {
            using (var adPlayerObject = adPlayerJavaClass.CallStatic<AndroidJavaObject>("GetInstance"))
            {
                adPlayerObject.Call("LoadVideo");
                Debug.Log("MinimobAdServing:AndroidJavaClass called LoadVideo");
            }
        };
#endif
    }

    /// <summary>
    /// This shows a preloaded video/creates and shows a video
    /// </summary>
    public void ShowVideo()
    {
        Debug.Log("MinimobAdServing:ShowVideo()");
        if (!_adZoneCreated)
        {
            Debug.LogError("MinimobAdServing:Showvideo called before Video was created");
            return;
        }

#if UNITY_ANDROID && !UNITY_EDITOR
        using (var adPlayerJavaClass = new AndroidJavaClass("com.minimob.adserving.unityplugin.MinimobAdServingUnityPlugin"))
        {
            using (var adPlayerObject = adPlayerJavaClass.CallStatic<AndroidJavaObject>("GetInstance"))
            {
                adPlayerObject.Call("ShowVideo");
                Debug.Log("MinimobAdServing:AndroidJavaClass called ShowVideo");
            }
        };
#endif
    }

    public void OnAdZoneCreated()
    {
        Debug.Log("MinimobAdServing:OnAdZoneCreated()");
        _adZoneCreated = true;
        if (_onAdZoneCreatedAction != null)
        {
            _onAdZoneCreatedAction();
            _onAdZoneCreatedAction = null;
        }
    }

    public void OnAdsAvailable()
    {
        Debug.Log("MinimobAdServing:OnAdsAvailable()");
        if (onAdsAvailableAction != null)
            onAdsAvailableAction();
    }

    public void OnAdsNotAvailable()
    {
        Debug.Log("MinimobAdServing:OnAdsNotAvailable()");
        if (onAdsNotAvailableAction != null)
            onAdsNotAvailableAction();
    }

    // pre-loaded only
    public void OnVideoLoading()
    {
        Debug.Log("MinimobAdServing:OnVideoLoading()");
        if (onVideoLoadingAction != null)
            onVideoLoadingAction();
    }

    // pre-loaded only
    public void OnVideoLoaded()
    {
        Debug.Log("MinimobAdServing:OnVideoLoaded()");
        if (onVideoLoadedAction != null)
            onVideoLoadedAction();
    }

    public void OnVideoPlaying()
    {
        Debug.Log("MinimobAdServing:OnVideoPlaying()");
        if (onVideoPlayingAction != null)
            onVideoPlayingAction();
    }

    public void OnVideoFinished()
    {
        Debug.Log("MinimobAdServing:OnVideoFinished()");
        if (onVideoFinishedAction != null)
            onVideoFinishedAction();
    }

    public void OnVideoClosed()
    {
        Debug.Log("MinimobAdServing:OnVideoClosed()");
        if (onVideoClosedAction != null)
            onVideoClosedAction();
    }

    public void OnApplicationFocus(bool focus)
    {
        //Debug.Log("MinimobAdServing:OnApplicationFocus()");
#if UNITY_ANDROID && !UNITY_EDITOR
        using (var adPlayerJavaClass = new AndroidJavaClass("com.minimob.adserving.unityplugin.MinimobAdServingUnityPlugin"))
        {
            using (var adPlayerObject = adPlayerJavaClass.CallStatic<AndroidJavaObject>("GetInstance"))
            {
                adPlayerObject.Call("OnApplicationFocus", focus);
            }
        };
#endif
    }
}
