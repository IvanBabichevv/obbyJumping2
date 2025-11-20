using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YG;

public class TouchController : MonoBehaviour, IPointerClickHandler
{
    private void OnEnable()
    {
        if(YG2.envir.isMobile)
            gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PlayerMovement.Instance.IncreaseJumpPower();
        Debug.Log("OnPointerClick");
        FloatingTextSpawner.Instance.SpawnText();
        SoundManager.instance.PlayButtonClick();
    }
}
