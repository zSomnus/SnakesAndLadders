using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    private AudioSource as_CoinGain;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            as_CoinGain.Play();
            SaveSystem.UpdateMiniGameData(true);
            LevelLoader.Instance.LoadMainGame();
            
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        as_CoinGain = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
