using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject shopWindow;
    [SerializeField] private Button closeButton;
    //private bool isOpen =  false;

    void Start()
    {
        shopWindow.SetActive(false);
        closeButton.onClick.AddListener(CloseShop);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OpenShop();
        }
    }
    private void OpenShop()
    {
        shopWindow.SetActive(true);
        //isOpen = true;
    }

    private void CloseShop()
    {
        shopWindow.SetActive(false);
        //isOpen = false;
    }
}
