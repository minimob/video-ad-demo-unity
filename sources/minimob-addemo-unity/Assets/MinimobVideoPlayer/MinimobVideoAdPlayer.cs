using UnityEngine;
using UnityEngine.Events;
using Debug = UnityEngine.Debug;

public class MinimobVideoAdPlayer : MonoBehaviour
{
    public UnityAction OnAdsAvailableAction;
    public UnityAction OnAdsNotAvailableAction;
    public UnityAction OnVideoLoadingAction; // pre-loaded only
    public UnityAction OnVideoLoadedAction; // pre-loaded only
    public UnityAction OnVideoPlayingAction;
    public UnityAction OnVideoFinishedAction;
    public UnityAction OnVideoClosedAction;

    // internal action
    private UnityAction _onVideoCreatedAction;

    private bool _videoCreated = false;
    private bool _preloadedVideo = false;

    private static MinimobVideoAdPlayer _instance = null;

    public static MinimobVideoAdPlayer GetInstance()
    {
        if (_instance == null)
        {
            var go = new GameObject();
            _instance = go.AddComponent<MinimobVideoAdPlayer>();
            go.name = "MinimobVideoAdPlayer";
            DontDestroyOnLoad(go);
        }
        return _instance;
    }

    public void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
        gameObject.name = "MinimobVideoAdPlayer";
    }

    public void CreateVideo(string adTagString, string customTrackingData, UnityAction onVideoCreatedAction, bool preloadedVideo)
    {
        if (_videoCreated && _preloadedVideo == preloadedVideo)
        {
            if (onVideoCreatedAction != null)
                onVideoCreatedAction();
            return;
        }
        _preloadedVideo = preloadedVideo;
        _onVideoCreatedAction = onVideoCreatedAction;
        _videoCreated = false;

#if UNITY_ANDROID && !UNITY_EDITOR
        using (var adPlayerJavaClass = new AndroidJavaClass("com.minimob.adserving.unityplugin.MinimobVideoAdPlayer"))
        {
            using (var adPlayerObject = adPlayerJavaClass.CallStatic<AndroidJavaObject>("GetInstance"))
            {
                adPlayerObject.Call("CreateVideo", adTagString, customTrackingData, preloadedVideo);
            }
        };
#endif
    }

    /// <summary>
    /// Only call this to preload videos 
    /// </summary>
    public void LoadVideo()
    {
        if (!_videoCreated)
        {
            return;
        }

#if UNITY_ANDROID && !UNITY_EDITOR
        using (var adPlayerJavaClass = new AndroidJavaClass("com.minimob.adserving.unityplugin.MinimobVideoAdPlayer"))
        {
            using (var adPlayerObject = adPlayerJavaClass.CallStatic<AndroidJavaObject>("GetInstance"))
            {
                adPlayerObject.Call("LoadVideo");
            }
        };
#endif
    }

    /// <summary>
    /// This shows a preloaded video/creates and shows a video
    /// </summary>
    public void ShowVideo()
    {
        if (!_videoCreated)
        {
            return;
        }

#if UNITY_ANDROID && !UNITY_EDITOR
        using (var adPlayerJavaClass = new AndroidJavaClass("com.minimob.adserving.unityplugin.MinimobVideoAdPlayer"))
        {
            using (var adPlayerObject = adPlayerJavaClass.CallStatic<AndroidJavaObject>("GetInstance"))
            {
                adPlayerObject.Call("ShowVideo");
            }
        };
#endif
    }

    public void OnAdsAvailable()
    {
        if (OnAdsAvailableAction != null)
            OnAdsAvailableAction();
    }

    public void OnAdsNotAvailable()
    {
        if (OnAdsNotAvailableAction != null)
            OnAdsNotAvailableAction();
    }

    // pre-loaded only
    public void OnVideoLoading()
    {
        if (OnVideoLoadingAction != null)
            OnVideoLoadingAction();
    }

    // pre-loaded only
    public void OnVideoLoaded()
    {
        if (OnVideoLoadedAction != null)
            OnVideoLoadedAction();
    }

    public void OnVideoPlaying()
    {
        if (OnVideoPlayingAction != null)
            OnVideoPlayingAction();
    }

    public void OnVideoFinished()
    {
        if (OnVideoFinishedAction != null)
            OnVideoFinishedAction();
    }

    public void OnVideoClosed()
    {
        if (OnVideoClosedAction != null)
            OnVideoClosedAction();
    }

    public void OnVideoCreated()
    {
        _videoCreated = true;
        if (_onVideoCreatedAction != null)
        {
            _onVideoCreatedAction();
            _onVideoCreatedAction = null;
        }
    }

    public void OnApplicationFocus(bool focus)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        using (var adPlayerJavaClass = new AndroidJavaClass("com.minimob.adserving.unityplugin.MinimobVideoAdPlayer"))
        {
            using (var adPlayerObject = adPlayerJavaClass.CallStatic<AndroidJavaObject>("GetInstance"))
            {
                adPlayerObject.Call("OnApplicationFocus", focus);
            }
        };
#endif
    }
}
