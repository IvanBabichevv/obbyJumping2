using System;
using System.Collections.Generic;
using UnityEngine;

public class ListOfItems : MonoBehaviour
{
    public static ListOfItems Instance;
    
    [SerializeField] private List<PetItem> petItems;


    private void Awake()
    {
        Instance = this;
    }
    
    private void OnValidate()
    {
        for (int i = 0; i < petItems.Count; i++)
        {
            petItems[i].petId = i;
        }
    }

    public PetItem GetPet(int id)
    {
        return petItems[id];
    }
}
