using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class RewardedCups : MonoBehaviour
{
    [SerializeField] private int rewardedCups = 10;
    [SerializeField] private float timer = 10f;
    [SerializeField] private Image adFillIcon;
    [SerializeField] private TMP_Text timeText;

    private bool canShow;
    
    private float currentTime;

    void Start()
    {
        currentTime = timer;
        canShow = true;
        adFillIcon.fillAmount = 1;
        timeText.gameObject.SetActive(false);
    }
    
    void Update()
    {
        if (!canShow)
        {
            currentTime -= Time.deltaTime;
            adFillIcon.fillAmount = (timer - currentTime) / timer;

            string secondText = YG2.envir.language switch
            {
                "ru" => "с",
                _ => "s",
            };
            
            timeText.text = $"{(int)(timer -  currentTime)}{secondText}";
            
            if (currentTime <= 0)
            {
                currentTime = timer;
                canShow = true;
                adFillIcon.fillAmount = 1;
                
                timeText.gameObject.SetActive(false);
            }
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (canShow)
            {
                YG2.RewardedAdvShow("addCups", () =>
                {
                    PointsManager.Instance.AddPoints(rewardedCups);
                    PointsManager.Instance.ScoreChangedInvoke();

                    timeText.gameObject.SetActive(true);
                    adFillIcon.fillAmount = 0;
                    canShow = false;
                });
            }
        }
    }
}
