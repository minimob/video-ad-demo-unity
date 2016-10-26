using UnityEngine;
using UnityEngine.UI;

public class MinimobAdServingExample : MonoBehaviour
{
    private MinimobAdServing videoPlayer;

    public GameObject LoadingVideoPanel;
    public GameObject LoadNShowVideoButton;
    public GameObject LoadVideoButton;
    public GameObject ShowVideoButton;

    [HideInInspector]
    public string customTrackingData = "";
    [HideInInspector]
    private string adTagString = "<script> \n" +
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
                    " adzoneId:\"58077927000062\", \n" +
                    " templateId: \"video-fullscreen2.html\", \n" +
                    " mobile_web: false, \n" +
                    " video_supported: true, \n" +
                    " appId: \"58077827000077\", \n" +
                    " bundleId: \"com.minimob.addemos.unity\", \n" +
                    " placement: \"video fullscreen interstitial\"}; \n" +
                    " </script> \n" +
                    " <script id=\"sdk-loader\" onerror=\"if(typeof(mmji)!='undefined'){mmji.noAds()}\" type=\"text/javascript\" src='http://s.rtad.bid/assets/video-fullscreen-mmji.js'></script>";


    void Start () 
    {
        
    }

    public void OnLoadNShowVideoButtonClicked()
    {
        Debug.Log("MinimobAdServingExample:OnLoadNPlayVideoButtonClicked()");
        _setupAdZone();
    }

    public void OnLoadVideoButtonClicked()
    {
        Debug.Log("MinimobAdServingExample:OnLoadVideoButtonClicked()");
        _setupAdZonePreloaded();
    }

    public void OnShowVideoButtonClicked()
    {
        Debug.Log("MinimobAdServingExample:OnShowVideoButtonClicked()");
        LoadingVideoPanel.SetActive(true);
        LoadNShowVideoButton.SetActive(false);
        LoadVideoButton.SetActive(false);
        ShowVideoButton.SetActive(false);
        if (videoPlayer != null)
        {
            videoPlayer.ShowVideo();
            Debug.Log("MinimobAdServingExample:Showing video...");
        }
    }

    private void _setupAdZone()
    {
        videoPlayer = MinimobAdServing.GetInstance();
        videoPlayer.CreateAdZone(adTagString, customTrackingData, () =>
        {
            Debug.Log("MinimobAdServingExample:onAdZoneCreatedAction() called");
            LoadingVideoPanel.SetActive(true);
            LoadNShowVideoButton.SetActive(false);
            LoadVideoButton.SetActive(false);
            ShowVideoButton.SetActive(false);
            videoPlayer.ShowVideo();
        }
        , false);

        // declare the delegates
        videoPlayer.onAdsAvailableAction = () =>
        {
            LoadingVideoPanel.SetActive(false);
            LoadNShowVideoButton.SetActive(true);
            LoadVideoButton.SetActive(true);
            ShowVideoButton.SetActive(false);
            Debug.Log("MinimobAdServingExample:OnAdsAvailableAction()");
        };
        videoPlayer.onAdsNotAvailableAction = () =>
        {
            LoadingVideoPanel.SetActive(false);
            LoadNShowVideoButton.SetActive(true);
            LoadVideoButton.SetActive(true);
            ShowVideoButton.SetActive(false);
            Debug.Log("MinimobAdServingExample:OnAdsNotAvailableAction()");
        };
        videoPlayer.onVideoPlayingAction = () =>
        {
            LoadingVideoPanel.SetActive(false);
            LoadNShowVideoButton.SetActive(false);
            LoadVideoButton.SetActive(false);
            ShowVideoButton.SetActive(false);
            Debug.Log("MinimobAdServingExample:OnVideoPlayingAction()");
        };
        videoPlayer.onVideoFinishedAction = () =>
        {
            LoadingVideoPanel.SetActive(false);
            LoadNShowVideoButton.SetActive(true);
            LoadVideoButton.SetActive(true);
            ShowVideoButton.SetActive(false);
            Debug.Log("MinimobAdServingExample:OnVideoFinishedAction()");
        };
        videoPlayer.onVideoClosedAction = () =>
        {
            LoadingVideoPanel.SetActive(false);
            LoadNShowVideoButton.SetActive(true);
            LoadVideoButton.SetActive(true);
            ShowVideoButton.SetActive(false);
            Debug.Log("MinimobAdServingExample:OnVideoClosedAction()");
        };
    }

    private void _setupAdZonePreloaded()
    {
        videoPlayer = MinimobAdServing.GetInstance();
        videoPlayer.CreateAdZone(adTagString, customTrackingData, () =>
        {
            Debug.Log("MinimobAdServingExample:onAdZoneCreatedAction() called");
            LoadingVideoPanel.SetActive(true);
            LoadNShowVideoButton.SetActive(false);
            LoadVideoButton.SetActive(false);
            ShowVideoButton.SetActive(false);
            videoPlayer.LoadVideo();
        }
        , true);

        // declare the delegates
        videoPlayer.onAdsAvailableAction = () =>
        {
            LoadingVideoPanel.SetActive(false);
            LoadNShowVideoButton.SetActive(true);
            LoadVideoButton.SetActive(true);
            ShowVideoButton.SetActive(false);
            Debug.Log("MinimobAdServingExample:OnAdsAvailableAction()");
        };
        videoPlayer.onAdsNotAvailableAction = () =>
        {
            LoadingVideoPanel.SetActive(false);
            LoadNShowVideoButton.SetActive(true);
            LoadVideoButton.SetActive(true);
            ShowVideoButton.SetActive(false);
            Debug.Log("MinimobAdServingExample:OnAdsNotAvailableAction()");
        };
        videoPlayer.onVideoLoadingAction = () =>
        {
            LoadingVideoPanel.SetActive(true);
            LoadNShowVideoButton.SetActive(false);
            LoadVideoButton.SetActive(false);
            ShowVideoButton.SetActive(false);
            Debug.Log("MinimobAdServingExample:OnVideoLoadingAction()");
        };
        videoPlayer.onVideoLoadedAction = () =>
        {
            LoadingVideoPanel.SetActive(false);
            LoadNShowVideoButton.SetActive(true);
            LoadVideoButton.SetActive(false);
            ShowVideoButton.SetActive(true);
            Debug.Log("MinimobAdServingExample:OnVideoLoadedAction()");
        };
        videoPlayer.onVideoPlayingAction = () =>
        {
            LoadingVideoPanel.SetActive(false);
            LoadNShowVideoButton.SetActive(false);
            LoadVideoButton.SetActive(false);
            ShowVideoButton.SetActive(false);
            Debug.Log("MinimobAdServingExample:OnVideoPlayingAction()");
        };
        videoPlayer.onVideoFinishedAction = () =>
        {
            LoadingVideoPanel.SetActive(false);
            LoadNShowVideoButton.SetActive(true);
            LoadVideoButton.SetActive(true);
            ShowVideoButton.SetActive(false);
            Debug.Log("MinimobAdServingExample:OnVideoFinishedAction()");
        };
        videoPlayer.onVideoClosedAction = () =>
        {
            LoadingVideoPanel.SetActive(false);
            LoadNShowVideoButton.SetActive(true);
            LoadVideoButton.SetActive(true);
            ShowVideoButton.SetActive(false);
            Debug.Log("MinimobAdServingExample:OnVideoClosedAction()");
        };
    }
}
