using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    
    [SerializeField] public AudioSource musicGame;
    [SerializeField] public AudioSource fxSource;
    [SerializeField] private AudioClip buttonClick;
    [SerializeField] private AudioClip footStep;
    [SerializeField] private AudioClip Victory;
    [SerializeField] private AudioClip backgroundMusicGame;
    
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic(backgroundMusicGame);
    }

    public void PlayMusic(AudioClip music)
    {
        musicGame.clip = music;
        musicGame.loop = true;
        musicGame.Play();
    }

    public void PlayFX(AudioClip clip)
    {
        fxSource.PlayOneShot(clip);
    }
    public void PlayButtonClick() => PlayFX(buttonClick);
    public void PlayFootStep() => PlayFX(footStep);
    public void PlayVictory() => PlayFX(Victory);

    public float MusicVolume
    {
        get => musicGame.volume;
        set => musicGame.volume = value;
    }

    public float EffectsVolume
    {
        get => fxSource.volume;
        set => fxSource.volume = value;
    }
}
