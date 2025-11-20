using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class MobileUIEnable : MonoBehaviour
{
    private void OnEnable()
    {
        if (YG2.envir.isMobile)
        {
            gameObject.SetActive(false);
        }
    }
}