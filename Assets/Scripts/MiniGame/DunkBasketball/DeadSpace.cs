using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEngine;

public class DeadSpace : MonoBehaviour
{
    // reference
    private BallSpawner ballSpawner;
    
    // Start is called before the first frame update
    void Start()
    {
        ballSpawner = PhysicsSportManager.Instance.BallSpawner;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            ballSpawner.SpawnNewBall();
        }
    }
}
