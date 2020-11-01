using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{
    public GameObject areYouSurePanel;

    public Text PanelText;
    public Text GoldText;

    private bool revive = false;
    private bool slowtime = false;
    private bool immortal = false;

    public void ReviveShopButton()
    {
        areYouSurePanel.SetActive(true);
        PanelText.text = "You are buying 1 Revive ability.";
        revive = true;
    }

    public void SlowTimeShopButton()
    {
        areYouSurePanel.SetActive(true);
        PanelText.text = "You are buying 1 SlowTime ability.";
        slowtime = true;
    }

    public void ImmortalShopButton()
    {
        areYouSurePanel.SetActive(true);
        PanelText.text = "You are buying 1 Immortal ability.";
        immortal = true;
    }

    public void YesButton()
    {
        if (revive)
        {
            if (PlayerPrefs.GetInt("Gold") >= 10)
            {
                PlayerPrefs.SetInt("Revive", PlayerPrefs.GetInt("Revive") + 1);
                PlayerPrefs.SetInt("Gold", PlayerPrefs.GetInt("Gold") - 10);
                GoldText.text = PlayerPrefs.GetInt("Gold").ToString();
                CloudVariables.ImportantValues[0] = PlayerPrefs.GetInt("Revive");
                areYouSurePanel.SetActive(false);
                revive = false;
                PlayGamesController.Instance.SaveCloud();
            }
        }
        else
        {
            PanelText.text = "Not enough gold";
        }

        if (slowtime)
        {
            if (PlayerPrefs.GetInt("Gold") >= 5)
            {
                PlayerPrefs.SetInt("SlowTime", PlayerPrefs.GetInt("SlowTime") + 1);
                PlayerPrefs.SetInt("Gold", PlayerPrefs.GetInt("Gold") - 5);
                GoldText.text = PlayerPrefs.GetInt("Gold").ToString();
                CloudVariables.ImportantValues[1] = PlayerPrefs.GetInt("SlowTime");
                areYouSurePanel.SetActive(false);
                slowtime = false;
                PlayGamesController.Instance.SaveCloud();
            }
        }
        else
        {
            PanelText.text = "Not enough gold";
        }

        if (immortal)
        {
            if (PlayerPrefs.GetInt("Gold") >= 5)
            {
                PlayerPrefs.SetInt("Immortal", PlayerPrefs.GetInt("Immortal") + 1);
                PlayerPrefs.SetInt("Gold", PlayerPrefs.GetInt("Gold") - 5);
                GoldText.text = PlayerPrefs.GetInt("Gold").ToString();
                CloudVariables.ImportantValues[2] = PlayerPrefs.GetInt("Immortal");
                areYouSurePanel.SetActive(false);
                immortal = false;
                PlayGamesController.Instance.SaveCloud();
            }
        }
        else
        {
            PanelText.text = "Not enough gold";
        }
    }

    public void NoButton()
    {
        if (revive)
        {
            areYouSurePanel.SetActive(false);
            revive = false;
        }
        if (slowtime)
        {
            areYouSurePanel.SetActive(false);
            slowtime = false;
        }
        if (immortal)
        {
            areYouSurePanel.SetActive(false);
            immortal = false;
        }
    }
}
