using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class MainMenu : MonoBehaviour
{
    public Text highScoreText;
    public Text reviveText;
    public Text slowTimeText;
    public Text immortalText;
    public Text goldText;
    public Text cloudSyncText;

    public GameObject ReviveButton;
    public GameObject SlowTimeButton;
    public GameObject ImmortalButton;
    public GameObject cloudSyncPanel;
    public GameObject cloudSyncCloseButton;

    private int revive = 3;
    private int slowTime = 5;
    private int immortal = 5;

    private int showAdMenu = 0;

    private void Start()
    {

        showAdMenu = PlayerPrefs.GetInt("ShowAdMenu");

        revive = PlayerPrefs.GetInt("Revive");
        slowTime = PlayerPrefs.GetInt("SlowTime");
        immortal = PlayerPrefs.GetInt("Immortal");

        highScoreText.text = "Best Score: " + ((int)PlayerPrefs.GetFloat("HighScore")).ToString();

        showAdMenu++;
        PlayerPrefs.SetInt("ShowAdMenu", showAdMenu);

        if (PlayerPrefs.GetInt("ShowAdMenu") == 3 && PlayerPrefs.GetInt("Remove") != 1)
        {
            AdManager.Instance.ShowVideoAd();
            PlayerPrefs.SetInt("ShowAdMenu", 0);
        }

        if(PlayerPrefs.GetInt("Remove") != 1)
        {
            AdManager.Instance.RequestBanner();
        }

        Social.ReportScore((long)PlayerPrefs.GetFloat("HighScore"), GPGSIds.leaderboard_high_score, (bool success) => {
            // handle success or failure
        });

        CloudVariables.ImportantValues[0] = PlayerPrefs.GetInt("Revive");
        CloudVariables.ImportantValues[1] = PlayerPrefs.GetInt("SlowTime");
        CloudVariables.ImportantValues[2] = PlayerPrefs.GetInt("Immortal");
        CloudVariables.ImportantValues[3] = PlayerPrefs.GetInt("Gold");
        CloudVariables.ImportantValues[4] = (int)PlayerPrefs.GetFloat("HighScore");

        if (Social.localUser.authenticated)
        {
            PlayGamesController.Instance.SaveCloud();
        }
        else
        {
            PlayGamesController.Instance.SaveLocal();
        }

        if(PlayerPrefs.GetInt("CloudSync") == 0)
        {
            StartCoroutine(CloudSync());
        }

        StartCoroutine(AfterSceneLoad());
    }

    private void Update()
    {
        slowTimeText.text = "+ " + CloudVariables.ImportantValues[1].ToString();
        reviveText.text = "+ " + CloudVariables.ImportantValues[0].ToString();
        immortalText.text = "+ " + CloudVariables.ImportantValues[2].ToString();
        goldText.text = CloudVariables.ImportantValues[3].ToString();
        highScoreText.text = "Best Score: " + CloudVariables.ImportantValues[4].ToString();
    }

    public void PlayGame()
    {
        PlayerPrefs.SetInt("Revive", CloudVariables.ImportantValues[0]);
        PlayerPrefs.SetInt("SlowTime", CloudVariables.ImportantValues[1]);
        PlayerPrefs.SetInt("Immortal", CloudVariables.ImportantValues[2]);
        PlayerPrefs.SetInt("Gold", CloudVariables.ImportantValues[3]);
        PlayerPrefs.SetFloat("HighScore", CloudVariables.ImportantValues[4]);
        PlayerPrefs.SetInt("Remove", CloudVariables.ImportantValues[5]);

        SceneManager.LoadScene("PlayLevel");
        PlayGamesController.Instance.SaveCloud();
        AdManager.Instance.HideBanner();
    }

    public void ReviveButtonReward()
    {
        RewardAds.Instance.ShowRewardAd();
        PlayerPrefs.SetInt("isRevive", 1);
    }
    public void TimeSlowButtonReward()
    {
        RewardAds.Instance.ShowRewardAd();
        PlayerPrefs.SetInt("isSlowTime", 1);
    }
    public void ImmortalButtonReward()
    {
        RewardAds.Instance.ShowRewardAd();
        PlayerPrefs.SetInt("isImmortal", 1);
    }
    public void AchievementUI()
    {
        PlayGamesController.Instance.OnAchievementClick();
    }
    public void LeaderboardUI()
    {
        PlayGamesController.Instance.OnLeaderboardClick();
    }

    public void LoadDataCloud()
    {
        PlayGamesController.Instance.LoadCloud();
    }

    IEnumerator AfterSceneLoad()
    {
        yield return new WaitForSeconds(5);

        PlayerPrefs.SetInt("Revive", CloudVariables.ImportantValues[0]);
        PlayerPrefs.SetInt("SlowTime", CloudVariables.ImportantValues[1]);
        PlayerPrefs.SetInt("Immortal", CloudVariables.ImportantValues[2]);
        PlayerPrefs.SetInt("Gold", CloudVariables.ImportantValues[3]);
        PlayerPrefs.SetFloat("HighScore", CloudVariables.ImportantValues[4]);
        PlayerPrefs.SetInt("Remove", CloudVariables.ImportantValues[5]);
    }

    IEnumerator CloudSync()
    {
        if (Social.localUser.authenticated)
        {
            cloudSyncPanel.SetActive(true);

            cloudSyncText.text = "Synchronizing with the cloud";

            yield return new WaitForSeconds(5);

            PlayerPrefs.SetInt("Revive", CloudVariables.ImportantValues[0]);
            PlayerPrefs.SetInt("SlowTime", CloudVariables.ImportantValues[1]);
            PlayerPrefs.SetInt("Immortal", CloudVariables.ImportantValues[2]);
            PlayerPrefs.SetInt("Gold", CloudVariables.ImportantValues[3]);
            PlayerPrefs.SetFloat("HighScore", CloudVariables.ImportantValues[4]);
            PlayerPrefs.SetInt("Remove", CloudVariables.ImportantValues[5]);

            PlayerPrefs.SetInt("CloudSync", 1);

            cloudSyncCloseButton.SetActive(true);
        }
        else
        {
            cloudSyncPanel.SetActive(true);

            cloudSyncText.text = "You're not signed in with play games account, local save is loaded.";

            yield return new WaitForSeconds(2);

            PlayerPrefs.SetInt("CloudSync", 1);

            cloudSyncCloseButton.SetActive(true);
        }
    }
}
