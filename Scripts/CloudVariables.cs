using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudVariables : MonoBehaviour
{
    //public static int highScore { get; set; }
    public static int[] ImportantValues { get; set; }
    
    private void Awake()
    {
        ImportantValues = new int[6];

        if (PlayerPrefs.HasKey("Immortal"))
        {
            Debug.Log("saved");
        }
        else
        {
            PlayerPrefs.SetInt("Revive", 3);
            PlayerPrefs.SetInt("SlowTime", 5);
            PlayerPrefs.SetInt("Immortal", 5);
        }

        ImportantValues[0] = PlayerPrefs.GetInt("Revive");
        ImportantValues[1] = PlayerPrefs.GetInt("SlowTime");
        ImportantValues[2] = PlayerPrefs.GetInt("Immortal");
        ImportantValues[3] = PlayerPrefs.GetInt("Gold");
        ImportantValues[4] = (int)PlayerPrefs.GetFloat("HighScore");
        ImportantValues[5] = PlayerPrefs.GetInt("Remove");
    }
}
