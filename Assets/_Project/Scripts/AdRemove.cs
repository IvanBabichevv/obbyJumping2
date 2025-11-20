using System;
using UnityEngine;
using YG;

public class AdRemove : MonoBehaviour
{
    private void OnEnable()
    {
        if (YG2.saves.removeAd)
            transform.parent.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            YG2.BuyPayments("AdRemove");
            transform.parent.gameObject.SetActive(false);
            
            YG2.SaveProgress();
        }
    }
}