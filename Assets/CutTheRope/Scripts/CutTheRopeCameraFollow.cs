using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutTheRopeCameraFollow : MonoBehaviour
{
    public Transform target;

    
    // tuneable value
    public float smoothSpeed = 10f;
    public Vector3 offset;

    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed*Time.fixedDeltaTime);
        transform.position = smoothedPosition;
    }
}
