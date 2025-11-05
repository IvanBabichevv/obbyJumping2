using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VictoryPointsCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text VictoryPointsCounterText;
    public string baseText_1;
    public int VictoryPoints;

    void Start()
    {
        baseText_1 = VictoryPointsCounterText.text;
        UpdateUI();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player collided");
            VictoryPoints++;
            UpdateUI();
        }
    }
    private void UpdateUI()
    {
        VictoryPointsCounterText.text = $"{baseText_1} {VictoryPoints}";
    }
}
