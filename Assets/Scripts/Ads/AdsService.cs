using System;
using GoogleMobileAds.Api;
using UnityEngine;
using Zenject;

public class AdsService : IInitializable, IDisposable 
{
    private const string REWARDED_ID = "ca-app-pub-4367747405568976/5062842764";
    private const string INTERSTITIAL_ID = "ca-app-pub-4367747405568976/8294105790";

    public void Initialize()
    {
        MobileAds.Initialize(initStatus =>
        {
            if (initStatus == null)
            {
                Debug.LogError("Ads not initialized");
                return;
            }
            
            Debug.Log("Ads initialized");
        });
    }


    public void RequestInterstitial()
    {
        var adRequest = new AdRequest();
        InterstitialAd.Load(INTERSTITIAL_ID, adRequest, (interstitialAd, error) =>
        {
            if (error != null)
            {
                Debug.LogError(error.GetResponseInfo());
                return;
            }
            Debug.Log("Interstitial loaded");
            if (interstitialAd != null && interstitialAd.CanShowAd())
            {
                ShowInterstitialAd(interstitialAd);
            }
        });
    }

    private void ShowInterstitialAd(InterstitialAd interstitialAd)
    {
        interstitialAd.Show();
        interstitialAd.Destroy();
    }

    public void RequestRewardedAd()
    {
        var adRequest = new AdRequest();
        RewardedAd.Load(REWARDED_ID, adRequest, (rewardedAd, error) =>
        {
            if (error != null)
            {
                Debug.LogError(error.GetResponseInfo());
                return;
            }
            Debug.Log("Rewarded ad loaded");
            if (rewardedAd != null && rewardedAd.CanShowAd())
            {
                ShowRewardedAd(rewardedAd);
            }
        });
    }
    
    private void ShowRewardedAd(RewardedAd rewardedAd)
    {
        rewardedAd.Show(OnRewardedAdShown);
        rewardedAd.Destroy();
    }

    private void OnRewardedAdShown(Reward reward)
    {
        Debug.Log($"Reward: {reward.Amount} {reward.Type}");
        
    }

    public void Dispose()
    {
        
    }
}
