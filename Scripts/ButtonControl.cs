using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonControl : MonoBehaviour
{
    public Text ImmortalText;
    public Text SlowTimeText;
    public Text ReviveText;
    public Text CountDown;
    public Text countDown2;

    public GameObject pausePanel;

    private int immortal;
    private int slowTime;
    private int revive;

    private float countDownTimerForImmortal = 16;// For Immortal
    private float countDownTimerForSlowTime = 11;// For SlowTime

    private bool iscountingForImmortal = false;// For Immortal
    private bool iscountingForSlowTime = false;// For SlowTime
    private bool isClicked = false;// For Immortal function
    private bool isClicked2 = false;// For SlowTime function
    private bool isPaused = false;
    private bool isSlowTimePressed = false;

    private void Start()
    {
        CountDown.GetComponent<Text>().enabled = false;
        countDown2.GetComponent<Text>().enabled = false;

        immortal = PlayerPrefs.GetInt("Immortal");
        slowTime = PlayerPrefs.GetInt("SlowTime");
        revive = PlayerPrefs.GetInt("Revive");

        ImmortalText.text = immortal.ToString();
        SlowTimeText.text = slowTime.ToString();
        ReviveText.text = revive.ToString();
    }
    private void LateUpdate()
    {
        if (iscountingForImmortal)
        {
            countDownTimerForImmortal -= Time.deltaTime;
            CountDown.text = ((int)countDownTimerForImmortal).ToString();
        }
        if (iscountingForSlowTime)
        {
            countDownTimerForSlowTime -= Time.deltaTime;
            countDown2.text = ((int)countDownTimerForSlowTime).ToString();
        }
    }

    public void ImmortalButton()
    {
        if (!isPaused)
        {
            StartCoroutine(ImmortalButtonActive());
        }
    }
    public void SlowTimeButton()
    {
        if (!isPaused)
        {
            StartCoroutine(SlowTimeActive());
        }
    }

    private IEnumerator ImmortalButtonActive()
    {
        if(PlayerPrefs.GetInt("Immortal") > 0 && !isClicked)
        {
            isClicked = true;
            Destroy(GetComponent<ParticleColl>());
            iscountingForImmortal = true;
            CountDown.GetComponent<Text>().enabled = true;
            immortal--;
            ImmortalText.text = immortal.ToString();
            PlayerPrefs.SetInt("Immortal", immortal);
            yield return new WaitForSeconds (16);
            iscountingForImmortal = false;
            CountDown.GetComponent<Text>().enabled = false;
            gameObject.AddComponent<ParticleColl>();
            countDownTimerForImmortal = 16;
            isClicked = false;
        }
    }
    private IEnumerator SlowTimeActive()
    {
        if(PlayerPrefs.GetInt("SlowTime") > 0 && !isClicked2)
        {
            isSlowTimePressed = true;
            isClicked2 = true;
            iscountingForSlowTime = true;
            countDown2.GetComponent<Text>().enabled = true;
            Time.timeScale = 0.5f;
            slowTime--;
            SlowTimeText.text = slowTime.ToString();
            PlayerPrefs.SetInt("SlowTime", slowTime);
            yield return new WaitForSeconds(10.2f);
            iscountingForSlowTime = false;
            countDown2.GetComponent<Text>().enabled = false;
            Time.timeScale = 1f;
            countDownTimerForSlowTime = 11;
            isClicked2 = false;
        }
    }
    public void ReviveButton()
    {
        if(PlayerPrefs.GetInt("Revive") > 0)
        {
            revive--;
            PlayerPrefs.SetInt("Revive", revive);
            SceneManager.LoadScene("OnReviveScene");
            Time.timeScale = 1f;
            AdManager.Instance.HideBanner();
        }
    }
    public void PauseButton()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        if(PlayerPrefs.GetInt("Remove") != 1)
        {
            AdManager.Instance.RequestBanner();
        }
    }
    public void ResumeButton()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        if (isSlowTimePressed || countDownTimerForSlowTime != 11)
        {
            Time.timeScale = 0.5f;
            isSlowTimePressed = false;
        }

        AdManager.Instance.HideBanner();
    }
    public void WatchAdForRevive()
    {
        PlayerPrefs.SetInt("isRevive", 1);
        PlayerPrefs.SetInt("WatchRevive", 1);
        RewardAds.Instance.ShowRewardAd();
        AdManager.Instance.HideBanner();
    }
    public void MenuButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        AdManager.Instance.HideBanner();
    }
}
