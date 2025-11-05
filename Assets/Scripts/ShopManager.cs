using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [Header("Настройки магазина")] 
    public List<ShopItem> itemsForSale = new List<ShopItem>();
    public GameObject shopSlotPrefab;
    public Transform contentParent;

    private VictoryPointsCounter victoryPoints;

    void Start()
    {
        victoryPoints = FindObjectOfType<VictoryPointsCounter>();
        if (victoryPoints == null)
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
        if (victoryPoints.HasEnoughPoints(item.price))
        {
            victoryPoints.SpendPoints(item.price);
            Debug.Log($"Куплен предмет: {item.itemName}. Осталось очков: {victoryPoints.VictoryPoints}");
        }
        else
        {
            Debug.Log("Недостаточно очков для покупки");
        }
    }
}
