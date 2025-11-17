using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchController : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        PlayerMovement.Instance.IncreaseJumpPower();
        Debug.Log("OnPointerClick");
        SoundManager.instance.PlayButtonClick();
    }
}
