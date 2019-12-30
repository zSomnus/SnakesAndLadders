using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity = new Vector3(0, -19.8f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
