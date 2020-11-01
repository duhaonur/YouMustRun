using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Preloader : MonoBehaviour
{
    public static bool removeads { set; get; }

    private void Start()
    {
        SceneManager.LoadScene(1);

        PlayerPrefs.SetInt("CloudSync", 0);

        GameObject admanager = GameObject.Find("AdManager");

        if (CloudVariables.ImportantValues[5] == 1)
        {
            Destroy(admanager);
            Debug.Log("removed");
        }
    }
}
