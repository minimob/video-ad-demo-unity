using UnityEngine;

public class MinimobVideoExampleUi : MonoBehaviour
{
    public GameObject PreloadingVideoGameObject;
    private MinimobVideoAdPlayer _videoPlayer;

    [HideInInspector]
    public string CustomTrackingData = "";
    [HideInInspector]
    public string AdTagString = "<script> \n" +
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
                        " </script> \n" +
                        " <script id=\"sdk-loader\" onerror=\"if(typeof(mmji)!='undefined'){mmji.noAds()}\" type=\"text/javascript\" src=\"http://s.rtad.bid/assets/video-fullscreen-mmji.js\"></script>";    
    
    void Start () 
    {
        var videoPlayer = MinimobVideoAdPlayer.GetInstance();

        // declare the delegates
        videoPlayer.OnAdsAvailableAction = () =>
        {
            Debug.Log("MinimobVideoExample:Video ads are currently available...");
        };
        videoPlayer.OnAdsNotAvailableAction = () =>
        {
            Debug.Log("MinimobVideoExample:No videos are currently available...");
            PreloadingVideoGameObject.SetActive(false);
        };
        // pre-loaded only
        videoPlayer.OnVideoLoadingAction = () =>
        {
            Debug.Log("MinimobVideoExample:Video loading...");
        };
        // pre-loaded only
        videoPlayer.OnVideoLoadedAction = () =>
        {
            PreloadingVideoGameObject.SetActive(false);
            Debug.Log("MinimobVideoExample:Video loaded succesfully...");
            videoPlayer.ShowVideo();
        };
        videoPlayer.OnVideoPlayingAction = () =>
        {
            Debug.Log("MinimobVideoExample:Video playing action...");
        };
        videoPlayer.OnVideoFinishedAction = () =>
        {
            Debug.Log("MinimobVideoExample:Video finished playing...");
        };
        videoPlayer.OnVideoClosedAction = () =>
        {
            Debug.Log("MinimobVideoExample:User closed video...");
        };
    }

    public void OnLoadNPlayVideoButtonClicked()
    {
        var videoPlayer = MinimobVideoAdPlayer.GetInstance();
        videoPlayer.CreateVideo(AdTagString, CustomTrackingData,()=>
        {
            Debug.Log("MinimobVideoExample:Loading and playing video...");
            videoPlayer.ShowVideo();
        }
        ,false);
    }

    public void OnLoadVideoButtonClicked()
    {
        var videoPlayer = MinimobVideoAdPlayer.GetInstance();
        videoPlayer.CreateVideo(AdTagString, CustomTrackingData, ()=>
        {
            Debug.Log("MinimobVideoExample:Preloading video...");
            PreloadingVideoGameObject.SetActive(true);
            _videoPlayer = videoPlayer;
            videoPlayer.LoadVideo();
        }
        ,true);
    }

    public void OnPlayVideoButtonClicked()
    {
        Debug.Log("MinimobVideoExample:Playing video...");
        if (_videoPlayer != null)
        {
            _videoPlayer.LoadVideo();
        }
    }
}
