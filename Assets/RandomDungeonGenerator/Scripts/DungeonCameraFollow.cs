using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCameraFollow : MonoBehaviour
{
    public Transform target;

    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void LateUpdate()
    {
         transform.position = target.transform.position + offset;
    }
}
