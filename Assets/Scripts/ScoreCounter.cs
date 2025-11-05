using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
   [SerializeField] private PlayerMovement player;
   [SerializeField] private TMP_Text scoreText;

   public string baseText;

   void Start()
   {
      baseText = scoreText.text;
   }
   
   void Update()
   {
      scoreText.text = baseText + player.GetJumpPower().ToString("F2");
   }
}
