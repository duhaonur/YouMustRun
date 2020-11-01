using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class AchievementtController : MonoBehaviour
{
    void Start()
    {
        
    }

    private void Update()
    {
        if(PlayerPrefs.GetFloat("ForAchive") == 100)
        {
            Social.ReportProgress(GPGSIds.achievement_score_100, 100.0f, (bool success) => { });
        }
    }
}
