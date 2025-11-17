using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] GameObject settingsWindow;
    [Header("Sliders")]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider effectSlider;
    [SerializeField] private Slider cameraSensivitySlider;
    private bool isOpen = false;

    public static float Sensivity { get; private set; } = 1f;

    public void Start()
    {
        if (settingsWindow != null)
            settingsWindow.SetActive(false);
        
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        effectSlider.value = PlayerPrefs.GetFloat("EffectVolume", 0.7f);
        cameraSensivitySlider.value = PlayerPrefs.GetFloat("CameraSensivity", 1f);
        
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
        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    private void SetEffectVolume(float value)
    {
        SoundManager.instance.fxSource.volume = value;
        PlayerPrefs.SetFloat("EffectVolume", value);
    }

    private void SetCameraSensivity(float value)
    {
        Sensivity = value;
        PlayerPrefs.SetFloat("Sensivity", value);
    }

    public void ToggleSettings()
    {
        isOpen = !isOpen;
        settingsWindow.SetActive(isOpen);
    }
    
}
