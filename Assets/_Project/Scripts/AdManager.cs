using System;
using UnityEngine;
using YG;

public class AdManager : MonoBehaviour
{
    public static AdManager Instance { get; private set; }

    private void OnEnable()
    {
        YG2.onGetSDKData += Initialize;
    }

    private void OnDisable()
    {
        YG2.onGetSDKData -= Initialize;
    }

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

    private void Initialize()
    {
        if (YG2.saves.removeAd)
            BannerOff();
        else
        {
            BannerShow();
        }
    }

    public void ShowAd()
    {
        if (!YG2.saves.removeAd)
        {
            YG2.InterstitialAdvShow();
        }
        else
        {
            print("AD OFF");
        }
    }

    public void BannerShow()
    {
        YG2.StickyAdActivity(true);
    }
    public void BannerOff()
    {
        YG2.StickyAdActivity(false);
    }
}