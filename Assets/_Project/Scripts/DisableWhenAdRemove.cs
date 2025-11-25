using UnityEngine;
using YG;

public class DisableWhenAdRemove : MonoBehaviour
{
    private void OnEnable()
    {
        InAppManager.OnPurchaseSuccess += UpdateStatus;
        
        UpdateStatus();
    }

    private void OnDisable()
    {
        InAppManager.OnPurchaseSuccess -= UpdateStatus;
    }

    public void UpdateStatus()
    {
        gameObject.SetActive(!YG2.saves.removeAd);
    }
}
