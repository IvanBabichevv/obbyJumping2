using System;
using UnityEngine;
using YG;

public class AdRemove : MonoBehaviour
{
    /*
    private void OnEnable()
    {
        InAppManager.OnPurchaseSuccess += UpdateStatus;
        
        UpdateStatus();
    }

    private void OnDisable()
    {
        InAppManager.OnPurchaseSuccess -= UpdateStatus;
    }
    */

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            YG2.BuyPayments("AdRemove");
        }
    }
    /*void SetActiveTrigger(bool value)
    {
        transform.parent.gameObject.SetActive(value);
    }

    public void UpdateStatus()
    {
        SetActiveTrigger(!YG2.saves.removeAd);
    }*/
}