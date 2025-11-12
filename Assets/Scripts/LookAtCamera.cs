using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Camera camera;

    private void Start()
    {
        camera = Camera.main;
    }
    
    private void Update()
    {
        transform.LookAt(camera.transform);
    }
}
