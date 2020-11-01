using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class PurchaserScript : MonoBehaviour
{
    private int Gold;

    public Text GoldText;

    private void Start()
    {
        Gold = PlayerPrefs.GetInt("Gold");
        GoldText.text = PlayerPrefs.GetInt("Gold").ToString();
    }

    public void OnPurchaseCompleted(Product product)
    {
        if(product != null)
        {
            switch (product.definition.id)
            {
                case "go50.ld":
                    PlayerPrefs.SetInt("Gold", PlayerPrefs.GetInt("Gold") + 50);
                    GoldText.text = PlayerPrefs.GetInt("Gold").ToString();
                    CloudVariables.ImportantValues[3] = PlayerPrefs.GetInt("Gold");
                    PlayGamesController.Instance.SaveCloud();
                    Debug.Log("Completed");
                    break;
                case "gold.100":
                    PlayerPrefs.SetInt("Gold", PlayerPrefs.GetInt("Gold") + 100);
                    GoldText.text = PlayerPrefs.GetInt("Gold").ToString();
                    CloudVariables.ImportantValues[3] = PlayerPrefs.GetInt("Gold");
                    PlayGamesController.Instance.SaveCloud();
                    Debug.Log("Completed");
                    break;
                case "gold.250":
                    PlayerPrefs.SetInt("Gold", PlayerPrefs.GetInt("Gold") + 250);
                    GoldText.text = PlayerPrefs.GetInt("Gold").ToString();
                    CloudVariables.ImportantValues[3] = PlayerPrefs.GetInt("Gold");
                    PlayGamesController.Instance.SaveCloud();
                    Debug.Log("Completed");
                    break;
                case "gold.500":
                    PlayerPrefs.SetInt("Gold", PlayerPrefs.GetInt("Gold") + 500);
                    GoldText.text = PlayerPrefs.GetInt("Gold").ToString();
                    CloudVariables.ImportantValues[3] = PlayerPrefs.GetInt("Gold");
                    PlayGamesController.Instance.SaveCloud();
                    Debug.Log("Completed");
                    break;
                case "gold.1000":
                    PlayerPrefs.SetInt("Gold", PlayerPrefs.GetInt("Gold") + 1000);
                    GoldText.text = PlayerPrefs.GetInt("Gold").ToString();
                    CloudVariables.ImportantValues[3] = PlayerPrefs.GetInt("Gold");
                    PlayGamesController.Instance.SaveCloud();
                    Debug.Log("Completed");
                    break;
                case "remove.ads":
                    PlayerPrefs.SetInt("Remove", 1);
                    Destroy(AdManager.Instance.gameObject);
                    CloudVariables.ImportantValues[5] = PlayerPrefs.GetInt("Remove");
                    PlayGamesController.Instance.SaveCloud();
                    break;
                default:
                    Debug.Log("Failed");
                    break;
            }
        }
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
    {
        Debug.Log(string.Format(" ", product, reason));
    }

    public void IapListenerPurchaseComplete(Product product)
    {
        if (product.hasReceipt)
        {
            switch (product.definition.id)
            {
                case "remove.ads":
                    PlayerPrefs.SetInt("Remove", 1);
                    Destroy(AdManager.Instance.gameObject);
                    CloudVariables.ImportantValues[5] = PlayerPrefs.GetInt("Remove");
                    Debug.Log("Restore Successful");
                    break;
                default:
                    Debug.Log("Failed");
                    break;
            }
        }
    }
}
