using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRootController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset;

    private void LateUpdate()
    {
        transform.position = player.position + offset;
    }
}