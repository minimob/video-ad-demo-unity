using UnityEngine;
using UnityEngine.Events;
using Debug = UnityEngine.Debug;

public class MinimobVideoAdPlayer : MonoBehaviour
{
    public UnityAction OnAdsNotAvailableAction;
    public UnityAction OnAdsAvailableAction;
    public UnityAction OnVideoPlayingAction;
    public UnityAction OnVideoFinishedAction;
    public UnityAction OnVideoClosedAction;
    public UnityAction OnVideoPreloadedAction;

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
            // don't destroy the video game object when loading a new scene
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

    public void CreateVideo(string adTagString, string customTrackingData, UnityAction onVideoCreatedAction , bool preloadedVideo)
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

        using (var adPlayerJavaClass = new AndroidJavaClass("com.minimob.unityplugin.MinimobVideoAdPlayer"))
        {
            using (var adPlayerObject = adPlayerJavaClass.CallStatic<AndroidJavaObject>("GetInstance"))
            {
                adPlayerObject.Call("CreateVideo", adTagString, customTrackingData,preloadedVideo);
            }
        };
    }

    // preload video can only be called for preloaded videos 
    public void PreloadVideo()
    {
        if (!_videoCreated)
        {
            return;
        }
        
        using (var adPlayerJavaClass = new AndroidJavaClass("com.minimob.unityplugin.MinimobVideoAdPlayer"))
        {
            using (var adPlayerObject = adPlayerJavaClass.CallStatic<AndroidJavaObject>("GetInstance"))
            {
                adPlayerObject.Call("PreloadVideo");
            }
        };
    }

    public void ShowVideo()
    {
        if (!_videoCreated)
        {
            return;
        }
        
        using (var adPlayerJavaClass = new AndroidJavaClass("com.minimob.unityplugin.MinimobVideoAdPlayer"))
        {
            using (var adPlayerObject = adPlayerJavaClass.CallStatic<AndroidJavaObject>("GetInstance"))
            {
                adPlayerObject.Call("ShowVideo");
            }
        };
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

    public void OnAdsNotAvailable()
    {
        if (OnAdsNotAvailableAction != null)
            OnAdsNotAvailableAction();
    }

    public void OnAdsAvailable()
    {
        if (OnAdsAvailableAction != null)
            OnAdsAvailableAction();
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

    public void OnVideoLoaded()
    {
        if (OnVideoPreloadedAction != null)
            OnVideoPreloadedAction();
    }

    public void OnApplicationFocus(bool focus)
    {
        using (var adPlayerJavaClass = new AndroidJavaClass("com.minimob.unityplugin.MinimobVideoAdPlayer"))
        {
            using (var adPlayerObject = adPlayerJavaClass.CallStatic<AndroidJavaObject>("GetInstance"))
            {
                adPlayerObject.Call("OnApplicationFocus", focus);
            }
        };
    }
}
