using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class Main : MonoBehaviour
{
    public static Main Instance;

    private void OnEnable()
    {
        YG2.onGetSDKData += LoadGame;
    }

    private void OnDisable()
    {
        YG2.onGetSDKData -= LoadGame;
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void LoadGame()
    {
        SceneManager.LoadScene(1);
    }
}
