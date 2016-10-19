using UnityEngine;
using UnityEngine.UI;

public class MinimobVideoExampleUi : MonoBehaviour
{
    private MinimobAdServing _videoPlayer;

    //public GameObject LoadingVideoPanel;
    //public GameObject LoadNPlayVideoButton;
    //public GameObject LoadVideoButton;
    //public GameObject PlayVideoButton;

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

                    " var dev_settings = { \n" +
                    " dataUrl:\"http://172.30.3.166:3000/adserver/servep/\", \n" +
                    " templateUrl:\"http://s.rtad.bid/public/\", \n" +
                    " x_debug_ip:\"66.87.121.197\" \n" +
                    "}; \n" +

                    " var mmAdTagSettings_auto = { \n" +
                    " adzoneId:\"58077927000062\", \n" +
                    " templateId: \"video-fullscreen2.html\", \n" +
                    " mobile_web: false, \n" +
                    " video_supported: true, \n" +
                    " appId: \"58077827000077\", \n" +
                    " bundleId: \"com.minimob.addemos.unity\", \n" +
                    " placement: \"video fullscreen interstitial\"}; \n" +
                    " </script> \n" +
                    " <script id=\"sdk-loader\" onerror=\"if(typeof(mmji)!='undefined'){mmji.noAds()}\" type=\"text/javascript\" src=\"http://s.rtad.bid/assets/video-fullscreen-mmji.js\"></script>";


    void Start () 
    {
        //LoadNPlayVideoButton.GetComponent<Button>().onClick.AddListener(OnLoadNPlayVideoButtonClicked);
        //LoadVideoButton.GetComponent<Button>().onClick.AddListener(OnLoadVideoButtonClicked);
        //PlayVideoButton.GetComponent<Button>().onClick.AddListener(OnPlayVideoButtonClicked);

        var videoPlayer = MinimobAdServing.GetInstance();

        // declare the delegates
        videoPlayer.OnAdsAvailableAction = () =>
        {
            //LoadingVideoPanel.SetActive(false);
            //LoadNPlayVideoButton.SetActive(true);
            //LoadVideoButton.SetActive(true);
            //PlayVideoButton.SetActive(false);
            Debug.Log("MinimobVideoExample:Video ads are currently available...");
        };
        videoPlayer.OnAdsNotAvailableAction = () =>
        {
            //LoadingVideoPanel.SetActive(false);
            //LoadNPlayVideoButton.SetActive(true);
            //LoadVideoButton.SetActive(true);
            //PlayVideoButton.SetActive(false);
            Debug.Log("MinimobVideoExample:No videos are currently available...");
        };
        // pre-loaded only
        videoPlayer.OnVideoLoadingAction = () =>
        {
            //LoadingVideoPanel.SetActive(true);
            //LoadNPlayVideoButton.SetActive(false);
            //LoadVideoButton.SetActive(false);
            //PlayVideoButton.SetActive(false);
            Debug.Log("MinimobVideoExample:Video loading...");
        };
        // pre-loaded only
        videoPlayer.OnVideoLoadedAction = () =>
        {
            //LoadingVideoPanel.SetActive(false);
            //LoadNPlayVideoButton.SetActive(true);
            //LoadVideoButton.SetActive(false);
            //PlayVideoButton.SetActive(true);
            Debug.Log("MinimobVideoExample:Video loaded succesfully...");
            videoPlayer.ShowVideo();
        };
        videoPlayer.OnVideoPlayingAction = () =>
        {
            //LoadingVideoPanel.SetActive(false);
            //LoadNPlayVideoButton.SetActive(false);
            //LoadVideoButton.SetActive(false);
            //PlayVideoButton.SetActive(false);
            Debug.Log("MinimobVideoExample:Video playing action...");
        };
        videoPlayer.OnVideoFinishedAction = () =>
        {
            //LoadingVideoPanel.SetActive(false);
            //LoadNPlayVideoButton.SetActive(true);
            //LoadVideoButton.SetActive(true);
            //PlayVideoButton.SetActive(false);
            Debug.Log("MinimobVideoExample:Video finished playing...");
        };
        videoPlayer.OnVideoClosedAction = () =>
        {
            //LoadingVideoPanel.SetActive(false);
            //LoadNPlayVideoButton.SetActive(true);
            //LoadVideoButton.SetActive(true);
            //PlayVideoButton.SetActive(false);
            Debug.Log("MinimobVideoExample:User closed video...");
        };
    }

    public void OnLoadNPlayVideoButtonClicked()
    {
        var videoPlayer = MinimobAdServing.GetInstance();
        videoPlayer.CreateVideo(AdTagString, CustomTrackingData,()=>
        {
            //LoadingVideoPanel.SetActive(true);
            //LoadNPlayVideoButton.SetActive(false);
            //LoadVideoButton.SetActive(false);
            //PlayVideoButton.SetActive(false);
            Debug.Log("MinimobVideoExample:Loading and playing video...");
            videoPlayer.ShowVideo();
        }
        ,false);
    }

    public void OnLoadVideoButtonClicked()
    {
        var videoPlayer = MinimobAdServing.GetInstance();
        videoPlayer.CreateVideo(AdTagString, CustomTrackingData, ()=>
        {
            //LoadingVideoPanel.SetActive(true);
            //LoadNPlayVideoButton.SetActive(false);
            //LoadVideoButton.SetActive(false);
            //PlayVideoButton.SetActive(false);
            _videoPlayer = videoPlayer;
            videoPlayer.LoadVideo();
            Debug.Log("MinimobVideoExample:Preloading video...");
        }
        ,true);
    }

    public void OnPlayVideoButtonClicked()
    {
        //LoadingVideoPanel.SetActive(true);
        //LoadNPlayVideoButton.SetActive(false);
        //LoadVideoButton.SetActive(false);
        //PlayVideoButton.SetActive(false);
        if (_videoPlayer != null)
        {
            _videoPlayer.ShowVideo();
            Debug.Log("MinimobVideoExample:Playing video...");
        }
    }
}
