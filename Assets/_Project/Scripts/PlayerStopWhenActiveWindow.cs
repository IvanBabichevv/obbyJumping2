using System;
using UnityEngine;

namespace Player
{
    public class PlayerStopWhenActiveWindow: MonoBehaviour
    {
        private void OnEnable()
        {
            PlayerMovement.Instance?.ForceStop(true);
        }

        private void OnDisable()
        {
            PlayerMovement.Instance?.ForceStop(false);
        }
    }
}