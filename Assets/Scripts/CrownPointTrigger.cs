using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CrownPointTrigger : MonoBehaviour
{
    public int VictoryPoints = 1;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player collided");
            PointsManager.Instance.AddPoints(VictoryPoints);
        }
    }
    
}
