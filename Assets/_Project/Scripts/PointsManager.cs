using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

    private string scoreBaseText;
    private string victoryScoreBaseText;

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
        victoryScoreBaseText = VictoryPointsCounterText.text;
        scoreBaseText = scoreText.text;
        UpdateUI();
    }

    public void AddPoints(int mount)
    {
        victoryPoints += mount;
        UpdateUI();
    }

    public bool HasEnoughPoints(int mount)
    {
        return victoryPoints >= mount;
    }

    public void SpendPoints(int mount)
    {
        victoryPoints -= mount;
        if (victoryPoints < 0) victoryPoints = 0;
        UpdateUI();
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
            VictoryPointsCounterText.text = victoryScoreBaseText + $" {victoryPoints}";

        scoreText.text = scoreBaseText + player.GetJumpPower();

        OnVictoryPointsChanged?.Invoke();
    }
}