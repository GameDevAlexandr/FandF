using System;
using UnityEngine;
using UnityEngine.Events;

public class ApplovinAD : MonoBehaviour
{
    public UnityEvent<string> completeEvent = new UnityEvent<string>();
    public bool adIsRady { get; private set; }

    [SerializeField] private string _idAdv;
    [SerializeField] private string _sdkKey;
    [SerializeField] private GameObject _notRady;

    private string _currentId;
    private int _retryAttempt;
    private void Start()
    {

        MaxSdkCallbacks.OnSdkInitializedEvent += (MaxSdkBase.SdkConfiguration sdkConfiguration) => 
        {
            InitializeRewardedAds();
        };

        MaxSdk.SetSdkKey(_sdkKey);
        MaxSdk.SetUserId("USER_ID");
        MaxSdk.InitializeSdk();

       
    }

    private void InitializeRewardedAds()
    {
        MaxSdkCallbacks.Rewarded.OnAdLoadedEvent += OnRewardedAdLoadedEvent;
        MaxSdkCallbacks.Rewarded.OnAdLoadFailedEvent += OnRewardedAdLoadFailedEvent;
        MaxSdkCallbacks.Rewarded.OnAdDisplayedEvent += OnRewardedAdDisplayedEvent;
        MaxSdkCallbacks.Rewarded.OnAdClickedEvent += OnRewardedAdClickedEvent;
        MaxSdkCallbacks.Rewarded.OnAdRevenuePaidEvent += OnRewardedAdRevenuePaidEvent;
        MaxSdkCallbacks.Rewarded.OnAdHiddenEvent += OnRewardedAdHiddenEvent;
        MaxSdkCallbacks.Rewarded.OnAdDisplayFailedEvent += OnRewardedAdFailedToDisplayEvent;
        MaxSdkCallbacks.Rewarded.OnAdReceivedRewardEvent += OnRewardedAdReceivedRewardEvent;
        MaxSdk.LoadRewardedAd(_idAdv);
    }

    public void ShowReward(string adId)
    {
        _currentId = adId;
        if (adIsRady)
        {
            MaxSdk.ShowRewardedAd(_idAdv);
            adIsRady = false;
            MaxSdk.LoadRewardedAd(_idAdv);
        }
        else
        {
            _notRady.SetActive(true);
            Invoke("CloseNotRady", 2);
            GameAnalitic.ADClick();
        }
    }
    public void LoadRewardedAd()
    {
        MaxSdk.LoadRewardedAd(_idAdv);
    }

    private void OnRewardedAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Rewarded ad is ready for you to show. MaxSdk.IsRewardedAdReady(adUnitId) now returns 'true'.
        Debug.Log("Rwward ADS Loaded");
        adIsRady = true;
        // Reset retry attempt
        _retryAttempt = 0;
    }

    private void OnRewardedAdLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
    {
        // Rewarded ad failed to load 
        // AppLovin recommends that you retry with exponentially higher delays, up to a maximum delay (in this case 64 seconds).
        Debug.Log("Reward ADS failed to Load");
        adIsRady = false;
        _retryAttempt++;
        double retryDelay = Math.Pow(2, Math.Min(6, _retryAttempt));

        Invoke("LoadRewardedAd", (float)retryDelay);
    }

    private void OnRewardedAdDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
    }

    private void OnRewardedAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
    {
        // Rewarded ad failed to display. AppLovin recommends that you load the next ad.
        LoadRewardedAd();
    }

    private void OnRewardedAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }

    private void OnRewardedAdHiddenEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Rewarded ad is hidden. Pre-load the next ad
        LoadRewardedAd();
    }

    private void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdk.Reward reward, MaxSdkBase.AdInfo adInfo)
    {
        //GameAnalitic.AdRevenue(adInfo.Revenue.ToString());
        completeEvent.Invoke(_currentId);
        GameAnalitic.AdCompleteEvent(_currentId);
        EventManager.AddCurrency(EnumsData.CurrencyType.adCoin, 1);
        //GameAnalitic.AdRewardComplete(_currentId);
    }

    private void OnRewardedAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {

        // Ad revenue paid. Use this callback to track user revenue.
    }

    private void CloseNotRady()
    {
        _notRady.SetActive(false);
    }
}
