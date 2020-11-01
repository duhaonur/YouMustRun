using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.SceneManagement;

public class RewardAds : MonoBehaviour
{
    public static RewardAds Instance { set; get; }

    private string appID = "";

    private RewardBasedVideoAd rewardBasedVideo;
    private string rewardBasedVideoId = "";

    void Start()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        MobileAds.Initialize(appID);

        rewardBasedVideo = RewardBasedVideoAd.Instance;

        RequestRewardAd();

        rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;

        rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;

        rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;

        rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
    }

    public void RequestRewardAd()
    {
        AdRequest request = new AdRequest.Builder().Build();

        rewardBasedVideo.LoadAd(request, rewardBasedVideoId);
    }

    public void ShowRewardAd()
    {
        if (rewardBasedVideo.IsLoaded())
        {
            rewardBasedVideo.Show();
        }
        else
        {
            PlayerPrefs.SetInt("isRevive", 0);
            PlayerPrefs.SetInt("isSlowTime", 0);
            PlayerPrefs.SetInt("isImmortal", 0);
        }
    }

    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        Debug.Log("HandleRewardBasedVideoLoaded event received");
    }

    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log("HandleRewardBasedVideoFailedToLoad event received with message: " + args.Message);
    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        string type = args.Type;
        int amount = (int)args.Amount;

        int reviveAmount = PlayerPrefs.GetInt("Revive");
        int immortalAmount = PlayerPrefs.GetInt("Immortal");
        int slowTimeAmount = PlayerPrefs.GetInt("SlowTime");

        Debug.Log("HandleRewardBasedVideoRewarded event received for " + amount.ToString() + " " + type);

        reviveAmount += amount;
        immortalAmount += amount;
        slowTimeAmount += amount;

        if (PlayerPrefs.GetInt("isRevive") == 1)
        {
            PlayerPrefs.SetInt("Revive", reviveAmount);
            PlayerPrefs.SetInt("isRevive", 0);
            CloudVariables.ImportantValues[0] = PlayerPrefs.GetInt("Revive");
            PlayGamesController.Instance.SaveCloud();

            if(PlayerPrefs.GetInt("WatchRevive") == 1)
            {
                SceneManager.LoadScene("OnReviveScene");
                PlayerPrefs.SetInt("Revive", 0);
                PlayerPrefs.SetInt("WatchRevive", 0);
                CloudVariables.ImportantValues[0] = PlayerPrefs.GetInt("Revive");
                PlayGamesController.Instance.SaveCloud();
            }
        }
        if (PlayerPrefs.GetInt("isSlowTime") == 1)
        {
            PlayerPrefs.SetInt("SlowTime", slowTimeAmount);
            PlayerPrefs.SetInt("isSlowTime", 0);
            CloudVariables.ImportantValues[1] = PlayerPrefs.GetInt("SlowTime");
            PlayGamesController.Instance.SaveCloud();
        }
        if (PlayerPrefs.GetInt("isImmortal") == 1)
        {
            PlayerPrefs.SetInt("Immortal", immortalAmount);
            PlayerPrefs.SetInt("isImmortal", 0);
            CloudVariables.ImportantValues[2] = PlayerPrefs.GetInt("Immortal");
            PlayGamesController.Instance.SaveCloud();
        }
    }

    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        Debug.Log("HandleRewardBasedVideoClosed event received");
        RequestRewardAd();
    }
}
