using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class InAppManager : MonoBehaviour
{
    public static InAppManager Instance {get; private set;}

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void OnEnable()
    {
        YG2.onPurchaseSuccess += SuccessPurchased;
        YG2.onPurchaseFailed += FailedPurchased;
    }

    private void OnDisable()
    {
        YG2.onPurchaseSuccess -= SuccessPurchased;
        YG2.onPurchaseFailed -= FailedPurchased;
    }

    private void SuccessPurchased(string id)
    {
        if (id == "AdRemove")
        {
            YG2.saves.removeAd = true;
            AdManager.Instance?.BannerOff();
        }
    }

    private void FailedPurchased(string id)
    {
   
    }

}
