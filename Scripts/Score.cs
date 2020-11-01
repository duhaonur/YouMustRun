using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    private float score = 0.0f;
    private float onReviveScore;

    private float diffucultyLevel = 0.65f;
    private int maxDifficultyLevel = 20;
    private int scoreToNextLevel = 20;
    private int scoreModifier = 2;

    private bool isDead = false;

    public Text scoreText;

    public EndGameMenu endGameMenu;

    private void Update()
    {
        if (isDead)
            return;

        if(score >= scoreToNextLevel)
        {
            LevelUp();
        }

        score += Time.deltaTime * diffucultyLevel * scoreModifier;
        scoreText.text = ((int)score).ToString();

    }

    private void LevelUp()
    {
        if (diffucultyLevel == maxDifficultyLevel)
            return;

        scoreToNextLevel *= 2;
        diffucultyLevel++;
        scoreModifier++;

        GetComponent<PlayerMove>().SetSpeed(diffucultyLevel);
    }
    public void OnDeath()
    {
        isDead = true;
        if(PlayerPrefs.GetFloat("HighScore") < score)
            PlayerPrefs.SetFloat("HighScore", score);
        endGameMenu.EndMenu(score);
        onReviveScore = score;
        PlayerPrefs.SetFloat("Score", onReviveScore);
        Debug.Log(PlayerPrefs.GetFloat("Score", score));
    }

    public void AchievementController()
    {
        if (score > 100)
        {
            Social.ReportProgress(GPGSIds.achievement_score_100, 100.0f, (bool success) => { });
        }
        if (score > 500)
        {
            Social.ReportProgress(GPGSIds.achievement_score_500, 100.0f, (bool success) => { });
        }
        if (score > 1000)
        {
            Social.ReportProgress(GPGSIds.achievement_score_1000, 100.0f, (bool success) => { });
        }
        if (score > 2000)
        {
            Social.ReportProgress(GPGSIds.achievement_score_2000, 100.0f, (bool success) => { });
        }
        if (score > 4000)
        {
            Social.ReportProgress(GPGSIds.achievement_score_4000, 100.0f, (bool success) => { });
        }
        if (score > 6000)
        {
            Social.ReportProgress(GPGSIds.achievement_score_6000, 100.0f, (bool success) => { });
        }
        if (score > 10000)
        {
            Social.ReportProgress(GPGSIds.achievement_score_10000, 100.0f, (bool success) => { });
        }
        if (PlayerPrefs.GetInt("Revive") == 0 && PlayerPrefs.GetInt("Immortal") == 0 && PlayerPrefs.GetInt("SlowTime") == 0)
        {
            Social.ReportProgress(GPGSIds.achievement_0_ability, 100.0f, (bool success) => { });
        }
    }
}
