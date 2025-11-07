using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class ShopManager : MonoBehaviour
{
    public static event Action<PetItem, bool> OnItemBought;
    
    [Header("Настройки магазина")] 
    public List<ShopItem> itemsForSale = new List<ShopItem>();
    public GameObject shopSlotPrefab;
    public Transform contentParent;
    
    void Start()
    {
        if (PointsManager.Instance == null)
        {
            Debug.LogError("VictoryPointsCounter не найден на сцене");
            return;
        }

        PopulateShop();
    }

    void PopulateShop()
    {
        foreach(Transform child in contentParent)
            Destroy(child.gameObject);
        foreach (var item in itemsForSale)
        {
            GameObject slotObj = Instantiate(shopSlotPrefab, contentParent);
            ShopSlot slot = slotObj.GetComponent<ShopSlot>();
            slot.Setup(item, this);
        }
            
    }

    public void TryBuyItem(ShopItem item)
    {
        if (PointsManager.Instance.HasEnoughPoints(item.Price))
        {
            PointsManager.Instance.SpendPoints(item.Price);
            Debug.Log($"Куплен предмет: {item.ItemName}. Осталось очков: {PointsManager.Instance.VictoryPoints}");
            OnItemBought?.Invoke(item.PetItem, true);
        }
        else
        {
            Debug.Log("Недостаточно очков для покупки");
        }
    }
}
