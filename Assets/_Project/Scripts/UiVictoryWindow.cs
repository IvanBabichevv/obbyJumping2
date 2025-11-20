using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using YG;

public class UiVictoryWindow : MonoBehaviour
{
    public static UiVictoryWindow instance;
    
    public bool victoryShow = false;

    [SerializeField] private GameObject victoryWindow;
    [SerializeField] private TMP_Text pointsText;
    [SerializeField] private TMP_Text rewardsText;
    [SerializeField] private GameObject x2Button;
    [SerializeField] private Button okButton;
    [SerializeField] private Button onGetX2;

    private int pointsToAdd;
    private CrownPointTrigger triggerRef;

    private bool wasX2;

    private void Awake()
    {
        instance = this;
        victoryWindow.SetActive(false);
        okButton.onClick.AddListener(OnOkPressed);
        onGetX2.onClick.AddListener(OnGetX2Pressed);
    }

    public void Show(int points, CrownPointTrigger trigger)
    {
        victoryShow = true;
        
        pointsToAdd = points;
        triggerRef = trigger;

        victoryWindow.SetActive(true);
        x2Button.SetActive(true);
        pointsText.text = $"+{pointsToAdd.ToString()}";
        rewardsText.text = $"+{(pointsToAdd * 2).ToString()}";

        SoundManager.instance.PlayVictory();

        PlayerMovement.Instance.HideStopButton();
        
        AdManager.Instance.ShowAd();
    }

    private void OnOkPressed()
    {
        triggerRef.TeleportPlayer();
        victoryWindow.SetActive(false);

        if (!wasX2)
            PointsManager.Instance.AddPoints(pointsToAdd);
        
        wasX2 = false;
        victoryShow = false;
    }

    private void OnGetX2Pressed()
    {
        YG2.RewardedAdvShow("x2cups", () =>
        {
            PointsManager.Instance.AddPoints(pointsToAdd * 2);
            pointsText.text = $"+{(pointsToAdd * 2).ToString()}";
            wasX2 = true;
            
            x2Button.SetActive(false);
        });
    }
}