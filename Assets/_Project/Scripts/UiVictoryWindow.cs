using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiVictoryWindow : MonoBehaviour
{
    public static UiVictoryWindow instance;
    
    [SerializeField] private GameObject victoryWindow;
    [SerializeField] private TMP_Text pointsText;
    [SerializeField] private Button okButton;
    
    private int pointsToAdd;
    private CrownPointTrigger triggerRef;

    private void Awake()
    {
        instance = this;
        victoryWindow.SetActive(false);
        okButton.onClick.AddListener(OnOkPressed);
    }

    public void Show(int points, CrownPointTrigger trigger)
    {
        pointsToAdd = points;
        triggerRef = trigger;   
        
        victoryWindow.SetActive(true);
        pointsText.text = $"+{pointsToAdd.ToString()}";
        
        PointsManager.Instance.AddPoints(pointsToAdd);
        SoundManager.instance.PlayVictory();
    }

    private void OnOkPressed()
    {
        triggerRef.TeleportPlayer();
        victoryWindow.SetActive(false);
    }
}
