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

#if UNITY_ANDROID && !UNITY_EDITOR
        using(var adPlayerJavaClass = new AndroidJavaClass("com.minimob.unityplugin.MinimobVideoAdPlayer"))
        {
            using (var adPlayerObject = adPlayerJavaClass.CallStatic<AndroidJavaObject>("GetInstance"))
            {
                adPlayerObject.Call("CreateVideo", adTagString, customTrackingData,preloadedVideo);
            }
        };
#endif
    }

    // preload video can only be called for preloaded videos 
    public void PreloadVideo()
    {
        if (!_videoCreated)
        {
            Debug.LogError("Preload Video called before Video was created");
            return;
        }

#if UNITY_ANDROID && !UNITY_EDITOR
        using (var adPlayerJavaClass = new AndroidJavaClass("com.minimob.unityplugin.MinimobVideoAdPlayer"))
        {
            using (var adPlayerObject = adPlayerJavaClass.CallStatic<AndroidJavaObject>("GetInstance"))
            {
                adPlayerObject.Call("PreloadVideo");
            }
        };
#endif
    }

    public void ShowVideo()
    {
        if (!_videoCreated)
        {
            Debug.LogError("Showvideo called before Video was created");
            return;
        }

#if UNITY_ANDROID && !UNITY_EDITOR
        using (var adPlayerJavaClass = new AndroidJavaClass("com.minimob.unityplugin.MinimobVideoAdPlayer"))
        {
            using (var adPlayerObject = adPlayerJavaClass.CallStatic<AndroidJavaObject>("GetInstance"))
            {
                adPlayerObject.Call("ShowVideo");
            }
        };
#endif
    }

    public void OnVideoCreated()
    {
        if (Debug.isDebugBuild)
            Debug.Log("MinimobVideo:unity video created");
        _videoCreated = true;
        if (_onVideoCreatedAction != null)
        {
            _onVideoCreatedAction();
            _onVideoCreatedAction = null;
        }
    }

    public void OnAdsNotAvailable()
    {
        if (Debug.isDebugBuild)
            Debug.Log("MinimobVideo:unity ads not available");
        if (OnAdsNotAvailableAction != null)
            OnAdsNotAvailableAction();
    }

    public void OnAdsAvailable()
    {
        if (Debug.isDebugBuild)
            Debug.Log("MinimobVideo:unity ads available");
        if (OnAdsAvailableAction != null)
            OnAdsAvailableAction();
    }

    public void OnVideoPlaying()
    {
        if (Debug.isDebugBuild)
            Debug.Log("MinimobVideo:unity video playing");
        if (OnVideoPlayingAction != null)
            OnVideoPlayingAction();
    }

    public void OnVideoFinished()
    {
        if (Debug.isDebugBuild)
            Debug.Log("MinimobVideo:unity video finished");
        if (OnVideoFinishedAction != null)
            OnVideoFinishedAction();
    }

    public void OnVideoClosed()
    {
        if (Debug.isDebugBuild)
            Debug.Log("MinimobVideo:unity video closed");
        if (OnVideoClosedAction != null)
            OnVideoClosedAction();
    }

    public void OnVideoLoaded()
    {
        if (Debug.isDebugBuild)
            Debug.Log("MinimobVideo:unity video preloaded");

        if (OnVideoPreloadedAction != null)
            OnVideoPreloadedAction();
    }

    public void OnApplicationFocus(bool focus)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        using (var adPlayerJavaClass = new AndroidJavaClass("com.minimob.unityplugin.MinimobVideoAdPlayer"))
        {
            using (var adPlayerObject = adPlayerJavaClass.CallStatic<AndroidJavaObject>("GetInstance"))
            {
                adPlayerObject.Call("OnApplicationFocus", focus);
            }
        };
#endif
    }

}
