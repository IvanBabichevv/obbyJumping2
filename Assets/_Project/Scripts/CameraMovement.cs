using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using YG;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] float mouseSensitivity = 100f;

    [SerializeField] float maxYRot = 80f;
    [SerializeField] float minYRot = -80;

    private float mouseX;
    private float mouseY;

    private void OnEnable()
    {
        TouchCameraController.CameraController = this;
    }

    private void OnDisable()
    {
        TouchCameraController.CameraController = null;
    }

    void LateUpdate()
    {
        if (YG2.envir.isDesktop)
        {
            if (Input.GetMouseButton(1))
            {
                mouseX += Input.GetAxis("Mouse X") * mouseSensitivity;
                mouseY -= Input.GetAxis("Mouse Y") * mouseSensitivity;
            }
        }

        mouseY = Mathf.Clamp(mouseY, minYRot, maxYRot);

        Quaternion rotation = Quaternion.Euler(mouseY, mouseX, 0);

        transform.rotation = rotation;
    }

    public void SetAxis(float x, float y)
    {
        mouseX += x * mouseSensitivity;
        mouseY -= y * mouseSensitivity;
    }

    public void SetMouseSensitivity(float sensitivity)
    {
        mouseSensitivity = sensitivity;
    }
}