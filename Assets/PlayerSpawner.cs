using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{

    [SerializeField] Dot dot;
    // Start is called before the first frame update
    private void Start()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        Instantiate(dot, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
