using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] float mouseSensitivity = 100f;
    
    [SerializeField] float maxYRot = 80f;
    [SerializeField] float minYRot = -80;
    
    private float mouseX;
    private float mouseY;

    void LateUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            mouseX += Input.GetAxis("Mouse X") * mouseSensitivity;
            mouseY -= Input.GetAxis("Mouse Y") * mouseSensitivity;
            
            mouseY = Mathf.Clamp(mouseY, minYRot, maxYRot);
            
            Quaternion rotation = Quaternion.Euler(mouseY, mouseX, 0);
            
            transform.rotation = rotation;
        }
        
    }
}
