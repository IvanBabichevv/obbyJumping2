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
    [SerializeField] private string text;


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
        pointsText.text = $"{text} {pointsToAdd.ToString()}";
        
        SoundManager.instance.PlayVictory();
    }

    private void OnOkPressed()
    {
        PointsManager.Instance.AddPoints(pointsToAdd);
        triggerRef.TeleportPlayer();
        victoryWindow.SetActive(false);
    }
}
