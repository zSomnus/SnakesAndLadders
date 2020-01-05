using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitNinjaManager : MonoBehaviour
{
    public string[] wordsToSlice;
    
    // Singleton
    public static FruitNinjaManager instance;
    
    // reference
    public AlphabetCannon alphabetCannon;

    // tuneable values
    public float spawnInterval = 5f;
   
    
    // private indicators
    [HideInInspector]
    public int currentIndex = -1;
    [HideInInspector]
    public string wordCollected;
    private bool isCurrentTurnWin;
    private float timeLeftToSpawn;
    public MiniGameState gameState;

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
        gameState = MiniGameState.Pending;

    }

    // Start is called before the first frame update
    void Start()
    {
        timeLeftToSpawn = spawnInterval;
        isCurrentTurnWin = true;
        SpawnNextWord();
    }

    void Update()
    {
        timeLeftToSpawn -= Time.deltaTime;
        if (timeLeftToSpawn <= 0)
        {
            timeLeftToSpawn = spawnInterval;
            SpawnNextWord();
        }
    }

    public void SpawnNextWord()
    {
        if (!isCurrentTurnWin)
        {
            gameState = MiniGameState.Failure;
            SendMessageToMainGame(false);
        }
        else
        {
            currentIndex++;
            isCurrentTurnWin = false;
            wordCollected = "";
            if (currentIndex < wordsToSlice.Length)
            {
                print("spawn "+wordsToSlice[currentIndex]);

                alphabetCannon.ShootWord(wordsToSlice[currentIndex]);
            }
            else
            {
                gameState = MiniGameState.Success;
                SendMessageToMainGame(true);
            }
        }
        
    }
    
  
    
    public void CollectAlphabet(char alphabet)
    {
        wordCollected += alphabet;
        if (wordCollected == wordsToSlice[currentIndex])
        {
            isCurrentTurnWin = true;
        }
    }

    public void SendMessageToMainGame(bool success)
    {
        SaveSystem.UpdateMiniGameData(success);
        LevelLoader.Instance.LoadMainGame();
    }
    
    public enum MiniGameState{Pending, Success, Failure}
}
