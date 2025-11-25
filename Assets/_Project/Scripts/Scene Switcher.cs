using System.Collections;
using System.Collections.Generic;
using UI.MobileJoystick;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private int sceneId;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Joystick.instance?.ResetJoystick();
            SceneManager.LoadScene(sceneId);
            YG2.InterstitialAdvShow();
        }
    }
}
