using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player1;
    public Player player2;

    public GameBoard gameBoard;
    
    [HideInInspector]
    public GameState gameState;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        gameState = GameState.Preparing;
        // Preparation

        gameState = GameState.Player1Turn;
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState)
        {
            case GameState.Player1Turn:
                if (Input.GetKeyDown(KeyCode.A))
                {
                    player1.MoveRandomly();
                    if (!CheckIfAnyoneWin())
                    {
                        gameState = GameState.Player2Turn;
                    }
                }

                break;
            case GameState.Player2Turn:
                if (Input.GetKeyDown(KeyCode.A))
                {
                    player2.MoveRandomly();
                    if (!CheckIfAnyoneWin())
                    {
                        gameState = GameState.Player1Turn;
                    }
                }

                break;
        }
        
    }

    public bool CheckIfAnyoneWin()
    {
        if (player1.ReachEnd() || player2.ReachEnd())
        {
            gameState = GameState.GameOver;
            if (player1.ReachEnd())
            {
                print("Player 1 won");
            }
            if(player2.ReachEnd())
            {
                print("Player 2 won");
            }
            return true;
            
        }
        else
        {
            return false;
        }
        
    }
    
    
}

public enum GameState{Preparing, Player1Turn, Player2Turn, GameOver}

