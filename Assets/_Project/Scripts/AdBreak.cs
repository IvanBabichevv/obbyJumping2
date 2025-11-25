using System;
using System.Collections;
using TMPro;
using UI.MobileJoystick;
using UnityEngine;
using YG;

public class AdBreak : MonoBehaviour
{
    public static AdBreak Instance;

    [SerializeField] private int timeInterval;
    [SerializeField] private GameObject adPanel;
    [SerializeField] private TMP_Text adText;

    private void Awake()
    {
        Instance = this;
        //DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        StartCoroutine(AdShow());
    }

    private void OnEnable()
    {
        YG2.onCloseInterAdvWasShow += OnClose;
    }

    private void OnDisable()
    {
        YG2.onCloseInterAdvWasShow -= OnClose;
    }

    IEnumerator AdShow()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(timeInterval);

            if (!YG2.saves.removeAd)
                ShowAd();
        }
    }

    public void ShowAd()
    {
        if (YG2.timerInterAdv <= 0)
        {
            StartCoroutine(TimerForAd());
        }
        else
        {
            print("Can't show");
        }
    }

    IEnumerator TimerForAd()
    {
        yield return new WaitForSecondsRealtime(0.25f);

        PlayerMovement.Instance.ForceStop(true);

        if (Joystick.instance)
            Joystick.instance.ResetJoystick();

        adPanel.SetActive(true);

        int step = 2;
        while (step > 0)
        {
            if (YG2.lang == "ru")
                adText.text = $"Реклама через: {step}...";
            else if (YG2.lang == "tr")
                adText.text = $"Reklam verme yolu: {step}...";
            else
                adText.text = $"Ad in: {step}...";

            yield return new WaitForSecondsRealtime(1);
            step--;
        }

        adPanel.SetActive(false);
        if (!UiVictoryWindow.instance.victoryShow)
            PlayerMovement.Instance.ForceStop(false);

        AdManager.Instance.ShowAd();
    }

    void OnClose(bool value)
    {
        if (Joystick.instance)
            Joystick.instance.ResetJoystick();
    }
}