using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopSlot : MonoBehaviour
{
    [Header("UI элементы")]
    public Image icon;
    public TMP_Text nameText;
    public TMP_Text descriptiontext;
    public TMP_Text priceText;
    public Button buyButton;
    
    private ShopItem currentItem;
    private ShopManager shopManager;

    public void Setup(ShopItem item, ShopManager manager)
    {
        currentItem = item;
        shopManager = manager;
        
        icon.sprite = item.icon;
        nameText.text = item.name;
        descriptiontext.text = item.description;
        priceText.text = item.price.ToString();
        
        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(OnBuyButton);
        
        void OnBuyButton()
        {
            shopManager.TryBuyItem(currentItem);
        }
    }
}
