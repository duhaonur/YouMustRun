using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using System.Text;
using System;

public class PlayGamesController : MonoBehaviour
{
    public static PlayGamesController Instance { set; get; }

    private void Start()
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


        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();

        PlayGamesPlatform.InitializeInstance(config);

        PlayGamesPlatform.Activate();

        SignIn();
    }

    private void SignIn()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                LoadCloud();
            }
            else
            {
                LoadLocal();
            }
        });
    }

    public void OnAchievementClick()
    {
        if (Social.localUser.authenticated)
        {
            Social.ShowAchievementsUI();
        }
        if (!Social.localUser.authenticated)
        {
            SignIn();
        }
    }

    public void OnLeaderboardClick()
    {
        if (Social.localUser.authenticated)
        {
            Social.ShowLeaderboardUI();
        }
        if (!Social.localUser.authenticated)
        {
            SignIn();
        }
    }

    private bool isSaving;
    private bool LocalSave;
    //making a string out of game data (highscores...)
    string GameDataToString()
    {
        return JsonUtil.CollectionToJsonString(CloudVariables.ImportantValues, "myKey");
    }
    void StringToGameData(string CloudData)
    {
        int[] cloudArray = JsonUtil.JsonStringToArray(CloudData, "myKey", str => int.Parse(str));
        CloudVariables.ImportantValues = cloudArray;
    }
    public void SaveLocal()
    {
        CloudVariables.ImportantValues[0] = PlayerPrefs.GetInt("Revive");
        CloudVariables.ImportantValues[1] = PlayerPrefs.GetInt("SlowTime");
        CloudVariables.ImportantValues[2] = PlayerPrefs.GetInt("Immortal");
        CloudVariables.ImportantValues[3] = PlayerPrefs.GetInt("Gold");
        CloudVariables.ImportantValues[4] = (int)PlayerPrefs.GetFloat("HighScore");
        CloudVariables.ImportantValues[5] = PlayerPrefs.GetInt("Remove");
    }
    public void LoadLocal()
    {
        PlayerPrefs.SetInt("Revive", CloudVariables.ImportantValues[0]);
        PlayerPrefs.SetInt("SlowTime", CloudVariables.ImportantValues[1]);
        PlayerPrefs.SetInt("Immortal", CloudVariables.ImportantValues[2]);
        PlayerPrefs.SetInt("Gold", CloudVariables.ImportantValues[3]);
        PlayerPrefs.SetFloat("HighScore", CloudVariables.ImportantValues[4]);
        PlayerPrefs.SetInt("Remove", CloudVariables.ImportantValues[5]);
    }
    public void SaveCloud()
    {
        if (Social.localUser.authenticated)
        {
            isSaving = true;
            LocalSave = false;
            SaveLocal();
            ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution("GameSave", DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, SaveGameOpened);
        }
        else
        {
            SaveLocal();
        }
    }
    public void LoadCloud()
    {
        isSaving = false;
        ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution("GameSave", DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, SaveGameOpened);
    }
    public void SaveGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if(status == SavedGameRequestStatus.Success)
        {
            if (isSaving) // Writing data
            {
                byte[] data = System.Text.ASCIIEncoding.ASCII.GetBytes(GameDataToString());
                TimeSpan totalPlaytime;
                SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder().WithUpdatedPlayedTime(totalPlaytime).WithUpdatedDescription("Saved Game at" + DateTime.Now);
                SavedGameMetadataUpdate update = builder.Build();
                ((PlayGamesPlatform)Social.Active).SavedGame.CommitUpdate(game, update, data, SavedGameWritten);
            }
            else // Reading Data
            {
                ((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(game, SavedGameLoaded);
            }
        }
        else
        {
            
        }
    }
    public void SavedGameLoaded(SavedGameRequestStatus status, byte[] data)
    {
        if(status == SavedGameRequestStatus.Success)
        {
            StringToGameData(System.Text.ASCIIEncoding.ASCII.GetString(data));
        }
    }
    public void SavedGameWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        Debug.Log(status);
    }
    private void OnApplicationQuit()
    {
        SaveCloud();
    }
}
