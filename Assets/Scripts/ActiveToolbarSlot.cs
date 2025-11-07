using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActiveToolbarSlot : MonoBehaviour, IPointerClickHandler
{
    public bool IsBusy => isBusy;

    [SerializeField] private Image icon;

    private PetItem currentItem;

    private bool isBusy;

    private void Start()
    {
        icon.gameObject.SetActive(false);
    }

    public void SetItem(PetItem item)
    {
        if (item)
        {
            icon.gameObject.SetActive(true);
            currentItem = item;
            icon.sprite = currentItem.Icon;
            
            PointsManager.Instance.IncreaseCoefficient(item.coefficient);
        }
        else
        {
            PointsManager.Instance.DecreaseCoefficient(currentItem.coefficient);
            icon.gameObject.SetActive(false);
            currentItem = null;
        }
    }

    public void SetBusy(bool value)
    {
        isBusy = value;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (currentItem)
            InventoryManager.Instance.UnequipItem(currentItem, this);
    }
}