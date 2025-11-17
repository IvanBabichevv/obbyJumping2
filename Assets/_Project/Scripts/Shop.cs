using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject shopWindow;
    private bool isOpen =  false;

    private void Start()
    {
        if (shopWindow != null)
            shopWindow.SetActive(false);
    }
    public void ToggleShop()
    {
        isOpen = !isOpen;
        shopWindow.SetActive(isOpen);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            shopWindow.SetActive(true);
        }
        
    }
}
