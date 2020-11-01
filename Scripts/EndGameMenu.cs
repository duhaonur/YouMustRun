using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGameMenu : MonoBehaviour
{
    public Text scoreText;

    public Image backgroundImage;

    public GameObject watchAdForRevive;
    public GameObject ReviveButton;
    public GameObject JumpButtonsPanel;

    private bool isDead = false;
    private bool isShown = false;

    private float transition = 0.0f;


    private void Start()
    {
        gameObject.SetActive(false);
        isShown = true;
        if(PlayerPrefs.GetInt("Revive") != 0)
        {
            watchAdForRevive.SetActive(false);
            ReviveButton.SetActive(true);
        }
        if(PlayerPrefs.GetInt("Revive") == 0)
        {
            ReviveButton.SetActive(false);
            watchAdForRevive.SetActive(true);
        }
    }
    private void Update()
    {
        if (!isDead)
            return;

        transition += Time.deltaTime;
        backgroundImage.color = Color.Lerp(new Color(0, 0, 0, 0), Color.black, transition);
    }
    public void EndMenu(float score)
    {
        gameObject.SetActive(true);
        scoreText.text = ((int)score).ToString();
        isDead = true;
        JumpButtonsPanel.SetActive(false);

        if (isShown)
        {
            isShown = false;

            if(PlayerPrefs.GetInt("Remove") != 1)
                AdManager.Instance.RequestBanner();
        }

        if (PlayerPrefs.GetInt("ShowAd") == 5 && PlayerPrefs.GetInt("Remove") != 1)
        {
            AdManager.Instance.ShowVideoAd();
            PlayerPrefs.SetInt("ShowAd", 0);
        }
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene("PlayLevel");
        Time.timeScale = 1f;
        AdManager.Instance.HideBanner();
        AdManager.Instance.RequestVideoAd();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
        AdManager.Instance.HideBanner();
    }

}
