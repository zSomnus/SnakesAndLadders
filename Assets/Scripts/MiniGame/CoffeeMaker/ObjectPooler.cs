using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [Serializable]
    public class Pool{
        public string tag;
        public GameObject prefab;
        public int size;
    }
    
    // Singleton
    public static ObjectPooler instance;

    public List<Pool> pools;
    
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (var pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            
            poolDictionary.Add(pool.tag, objectPool);
        }
    }
    

    public GameObject SpawnFromPool(string objectTag, Transform spawnPlace)
    {
        if (!poolDictionary.ContainsKey(objectTag))
        {
            Debug.LogWarning("Pool with tag " + objectTag + " doesn't exist");
            return null;
        }
        
        GameObject objectToSpawn = poolDictionary[objectTag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = spawnPlace.position;
        objectToSpawn.transform.rotation = spawnPlace.rotation;
        
        IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();
        
        pooledObj?.OnObjectSpawn(spawnPlace.up);

        poolDictionary[objectTag].Enqueue(objectToSpawn);
        
        return objectToSpawn;

    }



}
