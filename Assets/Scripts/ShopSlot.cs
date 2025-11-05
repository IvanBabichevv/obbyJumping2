using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class ShopSlot : MonoBehaviour
{
    [Header("UI элементы")] [SerializeField]
    private Image icon;

    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text descriptiontext;
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private Button buyButton;
    [SerializeField] private Color lockedColor;
    [SerializeField] private Color unlockedColor;

    private ShopItem currentItem;
    private ShopManager shopManager;

    private bool isLocked = false;

    void OnEnable()
    {
        PointsManager.OnVictoryPointsChanged += LockState;
        LockState();
    }

    void OnDisable()
    {
        PointsManager.OnVictoryPointsChanged -= LockState;
    }

    public void Setup(ShopItem item, ShopManager manager)
    {
        currentItem = item;
        shopManager = manager;

        icon.sprite = item.Icon;
        nameText.text = item.name;
        descriptiontext.text = item.Description;
        priceText.text = item.Price.ToString();

        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(OnBuyButton);

        void OnBuyButton()
        {
            shopManager.TryBuyItem(currentItem);
        }
        
        LockState();
    }

    void LockState()
    {
        if (!currentItem) return;
        
        if (!PointsManager.Instance.HasEnoughPoints(currentItem.Price))
        {
            priceText.color = lockedColor;
        }
        else
        {
            priceText.color = unlockedColor;
        }
    }
}