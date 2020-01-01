using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceCarCameraController : MonoBehaviour
{
    public Transform target;
    
    void FixedUpdate()
    {
        Vector3 newPosition = target.position;
        newPosition.z = -10f;
        transform.position = newPosition;
    }
}
