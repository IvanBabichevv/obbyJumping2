using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

[CreateAssetMenu(fileName = "New PetItem Item", menuName = "Pet Item")]
public class PetItem : ScriptableObject
{
    public int petId;
    
    public string PetNameRu;
    public string PetNameEn;
    public string PetNameTr;
    public Sprite Icon;
    public bool isEquipped;
    
    public float coefficient;
    public GameObject petPrefab;
    
    public string GetLocalizedName()
    {
        string localized = YG2.envir.language switch
        {
            "ru" => PetNameRu,
            "tr" => PetNameTr,
            _ => PetNameEn,
        };
        return localized;
    }
}
