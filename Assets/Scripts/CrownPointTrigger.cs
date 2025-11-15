using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CrownPointTrigger : MonoBehaviour
{
    public int VictoryPoints = 1;
    [SerializeField] private Vector3 telepotPosition;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player collided");
            PointsManager.Instance.AddPoints(VictoryPoints);
            SoundManager.instance.PlayVictory();
            
            CharacterController characterController = other.GetComponent<CharacterController>();

            if (characterController != null)
            {
                characterController.enabled = false;
                
                other.transform.position = telepotPosition;
                
                characterController.enabled = true;
            }
            else
            {
                other.transform.position = telepotPosition;

            }
        }
    }
    
}
