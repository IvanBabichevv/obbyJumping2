using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetFollow : MonoBehaviour
{
    private Transform target;
    public float followDistance = 2f;
    public float moveSpeed = 5f;
    public float smoothTurn = 5f;
    
    Vector3 offset;
    
    public void SetTarget(Transform newTarget, Vector3 offset)
    {
        this.offset = offset;
        target = newTarget;
    }

    void Update()
    {
        if(target == null) return;
        
        Vector3 targetPos = target.position - target.forward * followDistance;
        transform.position = Vector3.Lerp(transform.position, targetPos + offset, Time.deltaTime * moveSpeed);
        Vector3 lookDir = (target.position - transform.position).normalized;
        if (lookDir.magnitude > 0.1f)
        {
            Quaternion lookRot =  Quaternion.LookRotation(lookDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, smoothTurn * Time.deltaTime);
        }
    }
}
