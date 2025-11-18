using System;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] GameObject settingsWindow;
    [Header("Sliders")] [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider effectSlider;
    [SerializeField] private Slider cameraSensivitySlider;
    private bool isOpen = false;

    public static float Sensivity { get; private set; } = 1f;

    public void Start()
    {
        if (settingsWindow != null)
            settingsWindow.SetActive(false);

        float musicVolume = YG2.saves.musicVolume;
        float fxVolume = YG2.saves.fxVolume;
        float sensitivity = YG2.saves.cameraSensitivity;

        musicSlider.value = musicVolume;
        effectSlider.value = fxVolume;
        cameraSensivitySlider.value = sensitivity;

        SoundManager.instance.MusicVolume = musicSlider.value;
        SoundManager.instance.EffectsVolume = effectSlider.value;
        Sensivity = cameraSensivitySlider.value;

        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        effectSlider.onValueChanged.AddListener(SetEffectVolume);
        cameraSensivitySlider.onValueChanged.AddListener(SetCameraSensivity);
    }

    private void SetMusicVolume(float value)
    {
        SoundManager.instance.musicGame.volume = value;

        YG2.saves.musicVolume = value;

        YG2.SaveProgress();
    }

    private void SetEffectVolume(float value)
    {
        SoundManager.instance.fxSource.volume = value;

        YG2.saves.fxVolume = value;

        YG2.SaveProgress();
    }

    private void SetCameraSensivity(float value)
    {
        Sensivity = value;

        YG2.saves.cameraSensitivity = value;

        YG2.SaveProgress();
    }

    public void ToggleSettings()
    {
        isOpen = !isOpen;
        settingsWindow.SetActive(isOpen);
    }
}