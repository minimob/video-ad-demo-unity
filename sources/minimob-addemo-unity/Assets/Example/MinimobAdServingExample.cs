using UnityEngine;
using UnityEngine.UI;

public class MinimobAdServingExample : MonoBehaviour
{
    private MinimobAdServing _videoPlayer;

    //public GameObject LoadingVideoPanel;
    public GameObject LoadNShowVideoButton;
    public GameObject LoadVideoButton;
    public GameObject ShowVideoButton;

    [HideInInspector]
    public string customTrackingData = "";
    [HideInInspector]
    public string adTagString = "<script> \n" +
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
                    " dataUrl:'http://172.30.3.166:3000/adserver/servep/', \n" +
                    " templateUrl:'http://172.30.3.166:3000/', \n" +
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
                    " <script id=\"sdk-loader\" onerror=\"if(typeof(mmji)!='undefined'){mmji.noAds()}\" type=\"text/javascript\" src='http://172.30.3.166:3000/assets/video-fullscreen-mmji.js'></script>";


    void Start () 
    {
        //LoadNShowVideoButton.GetComponent<Button>().onClick.AddListener(OnLoadNShowVideoButtonClicked);
        //LoadVideoButton.GetComponent<Button>().onClick.AddListener(OnLoadVideoButtonClicked);
        //ShowVideoButton.GetComponent<Button>().onClick.AddListener(OnShowVideoButtonClicked);

        var videoPlayer = MinimobAdServing.GetInstance();

        // declare the delegates
        videoPlayer.OnAdsAvailableAction = () =>
        {
            //LoadingVideoPanel.SetActive(false);
            LoadNShowVideoButton.SetActive(true);
            LoadVideoButton.SetActive(true);
            ShowVideoButton.SetActive(false);
            Debug.Log("MinimobAdServingExample:OnAdsAvailableAction()");
        };
        videoPlayer.OnAdsNotAvailableAction = () =>
        {
            //LoadingVideoPanel.SetActive(false);
            LoadNShowVideoButton.SetActive(true);
            LoadVideoButton.SetActive(true);
            ShowVideoButton.SetActive(false);
            Debug.Log("MinimobAdServingExample:OnAdsNotAvailableAction()");
        };
        // pre-loaded only
        videoPlayer.OnVideoLoadingAction = () =>
        {
            //LoadingVideoPanel.SetActive(true);
            LoadNShowVideoButton.SetActive(false);
            LoadVideoButton.SetActive(false);
            ShowVideoButton.SetActive(false);
            Debug.Log("MinimobAdServingExample:OnVideoLoadingAction()");
        };
        // pre-loaded only
        videoPlayer.OnVideoLoadedAction = () =>
        {
            //LoadingVideoPanel.SetActive(false);
            LoadNShowVideoButton.SetActive(true);
            LoadVideoButton.SetActive(false);
            ShowVideoButton.SetActive(true);
            Debug.Log("MinimobAdServingExample:OnVideoLoadedAction()");
        };
        videoPlayer.OnVideoPlayingAction = () =>
        {
            //LoadingVideoPanel.SetActive(false);
            LoadNShowVideoButton.SetActive(false);
            LoadVideoButton.SetActive(false);
            ShowVideoButton.SetActive(false);
            Debug.Log("MinimobAdServingExample:OnVideoPlayingAction()");
        };
        videoPlayer.OnVideoFinishedAction = () =>
        {
            //LoadingVideoPanel.SetActive(false);
            LoadNShowVideoButton.SetActive(true);
            LoadVideoButton.SetActive(true);
            ShowVideoButton.SetActive(false);
            Debug.Log("MinimobAdServingExample:OnVideoFinishedAction()");
        };
        videoPlayer.OnVideoClosedAction = () =>
        {
            //LoadingVideoPanel.SetActive(false);
            LoadNShowVideoButton.SetActive(true);
            LoadVideoButton.SetActive(true);
            ShowVideoButton.SetActive(false);
            Debug.Log("MinimobAdServingExample:OnVideoClosedAction()");
        };
    }

    public void OnLoadNShowVideoButtonClicked()
    {
        Debug.Log("MinimobAdServingExample:OnLoadNPlayVideoButtonClicked()");
        var videoPlayer = MinimobAdServing.GetInstance();

        videoPlayer.CreateAdZone(adTagString, customTrackingData, ()=>
        {
            Debug.Log("MinimobAdServing:adTagString:" + adTagString);
            Debug.Log("MinimobAdServingExample:onAdZoneCreatedAction() called");
            //LoadingVideoPanel.SetActive(true);
            LoadNShowVideoButton.SetActive(false);
            LoadVideoButton.SetActive(false);
            ShowVideoButton.SetActive(false);
            videoPlayer.ShowVideo();
        }
        ,false);
    }

    public void OnLoadVideoButtonClicked()
    {
        Debug.Log("MinimobAdServingExample:OnLoadVideoButtonClicked()");
        var videoPlayer = MinimobAdServing.GetInstance();
        videoPlayer.CreateAdZone(adTagString, customTrackingData, ()=>
        {
            Debug.Log("MinimobAdServingExample:onAdZoneCreatedAction() called");
            //LoadingVideoPanel.SetActive(true);
            LoadNShowVideoButton.SetActive(false);
            LoadVideoButton.SetActive(false);
            ShowVideoButton.SetActive(false);
            _videoPlayer = videoPlayer;
            videoPlayer.LoadVideo();
        }
        ,true);
    }

    public void OnShowVideoButtonClicked()
    {
        Debug.Log("MinimobAdServingExample:OnShowVideoButtonClicked()");
        //LoadingVideoPanel.SetActive(true);
        LoadNShowVideoButton.SetActive(false);
        LoadVideoButton.SetActive(false);
        ShowVideoButton.SetActive(false);
        if (_videoPlayer != null)
        {
            _videoPlayer.ShowVideo();
            Debug.Log("MinimobAdServingExample:Showing video...");
        }
    }
}
