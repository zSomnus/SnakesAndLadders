using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeTimer : MonoBehaviour
{
    [Tooltip("Time limit of connecting to the next handle")]
    public float timeLimit = 5f;
 
    // singleton
    public static RopeTimer instance;
    
    // indicator
    private bool hasLost;
    
    [HideInInspector]
    public float timeLeft;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = timeLimit;
    }

    private void Update()
    {
        if (!hasLost)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                hasLost = true;
                SaveSystem.UpdateMiniGameData(false);
                LevelLoader.Instance.LoadMainGame();
            }
        }
       
    }


    public void RefreshTimer()
    {
        timeLeft = timeLimit;
    }
    
}
