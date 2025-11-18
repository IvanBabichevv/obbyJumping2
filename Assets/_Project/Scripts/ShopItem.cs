using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

[CreateAssetMenu(fileName = "New Shop Item", menuName = "Shop Item")]
public class ShopItem : ScriptableObject
{
    public Sprite Icon;
    public int Price;

    public PetItem PetItem;

    public string GetLocalizedDescription()
    {
        string localized = YG2.envir.language switch
        {
            "ru" => $"Прыжок: +{PetItem.coefficient}",
            "tr" => $"Zıplamak: +{PetItem.coefficient}",
            _ => $"Jump: +{PetItem.coefficient}"
        };
        return localized;
    }
}