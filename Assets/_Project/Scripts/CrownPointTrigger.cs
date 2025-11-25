using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using YG;

public class CrownPointTrigger : MonoBehaviour
{
    public int VictoryPoints = 1;
    [SerializeField] private Vector3 telepotPosition;
    [SerializeField] private string towerId;

    private Collider lastPlayer;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           lastPlayer =  other;
            
            UiVictoryWindow.instance.Show(VictoryPoints, this); 
            YG2.MetricaSend(towerId);
        }
    }

    public void TeleportPlayer()
    {
        if(lastPlayer == null) return;
        
        CharacterController characterController = lastPlayer.GetComponent<CharacterController>();

        if (characterController != null)
        {
            characterController.enabled = false;
            lastPlayer.transform.position = telepotPosition;
            characterController.enabled = true;
        }
        else
        {
            lastPlayer.transform.position = telepotPosition;
        }
    }
    
    
}
