using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour, IPooledObject
{
    // Adjustable values
    public float xOffset = 10f;
    public float yOffset = 10f;
    public float zOffset = 10f;

    // Property
    public DrinkType drinkType;

    public void OnObjectSpawn(Vector3 forwardVector)
    {
        Vector3 spawnVector = new Vector3(forwardVector.x+Random.Range(-xOffset, xOffset), forwardVector.y+Random.Range(-yOffset, yOffset), forwardVector.z+Random.Range(-zOffset, zOffset));
        GetComponent<Rigidbody>().velocity = spawnVector*30;
    }
}

public enum DrinkType{Chocelate, Milk, RedBull}
