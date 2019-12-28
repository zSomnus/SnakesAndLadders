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
        print("dwada");
        nextSpawnTimeLeft -= Time.deltaTime;
        if (nextSpawnTimeLeft <= 0f)
        {
            print("Spawn missile");
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
