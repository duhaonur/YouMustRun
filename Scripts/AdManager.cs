using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdManager : MonoBehaviour
{
    public static AdManager Instance { set; get; }

    private string appID = "";

    private BannerView bannerView;
    private string bannerID = "";

    private InterstitialAd videoAd;
    private string videoID = "";



    private void Start()
    {
        if(Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        MobileAds.Initialize(appID);

        RequestVideoAd();
    }

    public void RequestBanner()
    {
        bannerView = new BannerView(bannerID, AdSize.Banner, AdPosition.Bottom);

        AdRequest request = new AdRequest.Builder().Build();

        bannerView.LoadAd(request);

        bannerView.Show();

    }

    public void HideBanner()
    {
        bannerView.Hide();
    }

    public void RequestVideoAd()
    {
        videoAd = new InterstitialAd(videoID);

        AdRequest request = new AdRequest.Builder().Build();

        videoAd.LoadAd(request);
    }

    public void ShowVideoAd()
    {
        if (videoAd.IsLoaded())
        {
            videoAd.Show();
        }
    }
}
