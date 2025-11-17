using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Shop Item", menuName = "Shop Item")]
public class ShopItem : ScriptableObject
{
    public string ItemName;
    public Sprite Icon;
    public string Description;
    public int Price;
    
    public PetItem PetItem;
}
