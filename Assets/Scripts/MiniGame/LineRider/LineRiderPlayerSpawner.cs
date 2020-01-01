using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRiderPlayerSpawner : MonoBehaviour
{
    public GameObject player;

    public static LineRiderPlayerSpawner Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Spawn(0);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn(float seconds)
    {
        StartCoroutine(SpawnAfterSeconds(seconds));
    }

    IEnumerator SpawnAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        var playerGameObject = Instantiate(player, transform.position, Quaternion.identity);
        LineRiderGameManager.Instance.player = playerGameObject.GetComponent<LineRiderPlayer>();
    }
}
