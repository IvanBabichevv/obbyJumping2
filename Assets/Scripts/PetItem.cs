using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PetItem Item", menuName = "Pet Item")]
public class PetItem : ScriptableObject
{
    public string PetName;
    public Sprite Icon;
    public bool isEquipped;
    
    public float coefficient;
    public GameObject petPrefab;
}
