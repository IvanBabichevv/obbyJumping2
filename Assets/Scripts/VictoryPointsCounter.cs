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
            AddPoints(1);
        }
    }

    public void AddPoints(int mount)
    {
        VictoryPoints += mount;
        UpdateUI();
    }

    public bool HasEnoughPoints(int mount)
    {
        return VictoryPoints >= mount;
    }

    public void SpendPoints(int mount)
    {
        VictoryPoints -= mount;
        if(VictoryPoints < 0) VictoryPoints = 0;
        UpdateUI();
    }
    
    private void UpdateUI()
    {
        if(VictoryPointsCounterText != null)
        VictoryPointsCounterText.text = $"{baseText_1} {VictoryPoints}";
    }
}
