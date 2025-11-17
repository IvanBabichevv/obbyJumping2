using System;
using UnityEngine.SceneManagement;
using UnityEngine;
using YG;

public class MobileController : MonoBehaviour
{
    public static MobileController Instance;

    [SerializeField] private GameObject mobileController;

    private void Awake()
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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLoadScene;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLoadScene;
    }

    private void OnLoadScene(Scene scene, LoadSceneMode loadMode)
    {
        if (scene.buildIndex == 1)
        {
            SetMobileActive(true);
        }
    }

    public void SetMobileActive(bool value)
    {
        if (YG2.envir.isDesktop) return;
        mobileController.SetActive(value);
    }
}