using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AlphabetCannon : MonoBehaviour
{
    public Transform[] spawnPlaces;
    public GameObject[] alphabetPrefabs;
    public Dictionary<char, GameObject> dictionary;
    
    public float spawnVerticalPushForce = 8f;
    public float spawnHorizontalPushForce = 2f;

    // Start is called before the first frame update
 

    private void Awake()
    {
        dictionary = new Dictionary<char, GameObject>();
        for (int i = 0; i < 26; i++)
        {
            dictionary.Add((char)('A' + i), alphabetPrefabs[i]);
        }
    }


    public void ShootAlphabet(char alphabetToSpawn)
    {
        var alphabetPrefabToSpawn = dictionary[alphabetToSpawn];
        var alphabet = Instantiate(alphabetPrefabToSpawn, spawnPlaces[Random.Range(0,spawnPlaces.Length)].position, Quaternion.identity);
        alphabet.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(8f, 11f));
    }

    public void ShootWord(string word)
    {
        foreach (var alphabet in word)
        {
            ShootAlphabet(alphabet);
        }
    }

   
}
