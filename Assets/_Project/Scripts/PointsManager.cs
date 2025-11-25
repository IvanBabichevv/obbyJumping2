using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

public class PointsManager : MonoBehaviour
{
    public static event Action OnScoreChanged;
    public static event Action OnVictoryPointsChanged;

    public static PointsManager Instance;
    public float CurrentCoefficient => currentCoefficient;

    public int VictoryPoints => victoryPoints;
    [SerializeField] private TMP_Text VictoryPointsCounterText;
    [SerializeField] private PlayerMovement player;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private int victoryPoints;

    private float currentCoefficient = 1;

    private void OnEnable()
    {
        OnScoreChanged += UpdateUI;
    }

    private void OnDisable()
    {
        OnScoreChanged -= UpdateUI;
    }

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        victoryPoints = YG2.saves.cupsCount;
        
        UpdateUI();
    }

    public void AddPoints(int mount)
    {
        victoryPoints += mount;
        
        YG2.saves.cupsCount = victoryPoints;
        
        UpdateUI();
        
        YG2.SaveProgress();
    }

    public bool HasEnoughPoints(int mount)
    {
        return victoryPoints >= mount;
    }

    public void SpendPoints(int mount)
    {
        victoryPoints -= mount;
        if (victoryPoints < 0) victoryPoints = 0;
        
        YG2.saves.cupsCount = victoryPoints;
        
        UpdateUI();
        
        YG2.SaveProgress();
    }

    public void ScoreChangedInvoke()
    {
        OnScoreChanged?.Invoke();
    }

    public void IncreaseCoefficient(float coefficient)
    {
        currentCoefficient *= coefficient;
    }

    public void DecreaseCoefficient(float coefficient)
    {
        currentCoefficient /= coefficient;
        if (currentCoefficient < 1) currentCoefficient = 1;
    }

    private void UpdateUI()
    {
        if (VictoryPointsCounterText != null)
            VictoryPointsCounterText.text = $"{victoryPoints}";

        scoreText.text = player.GetJumpPower().ToString();

        OnVictoryPointsChanged?.Invoke();
    }
}