using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class NewPlayerMetrica : MonoBehaviour
{
    void Start()
    {
        if (!YG2.saves.newPlayerSend)
        {
            YG2.MetricaSend("new_player");
            YG2.saves.newPlayerSend = true;
            YG2.SaveProgress();
            
            print("new_player");
        }
    }
}
