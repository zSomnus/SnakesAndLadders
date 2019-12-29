using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSpawner : MonoBehaviour
{
    // Property
    public float spawnInterval = 5f;
    public float nextSpawnTimeLeft;

    // Refernece
    public GameObject rocket;

    // Start is called before the first frame update
    void Start()
    {
        nextSpawnTimeLeft = spawnInterval;
    }

    // Update is called once per frame
    void Update()
    {
        nextSpawnTimeLeft -= Time.deltaTime;
        if (nextSpawnTimeLeft <= 0f)
        {
            if (GameObject.FindGameObjectWithTag("Player") != null)
            {
                Spawn();
                
            }
            nextSpawnTimeLeft = spawnInterval;
        }
    }

    public void Spawn()
    {
        Instantiate(rocket, transform.position, Quaternion.identity);
    }
}
