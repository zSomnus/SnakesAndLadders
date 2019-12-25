using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player1;
    public Player player2;

    public GameBoard gameBoard;
    
    
    public GameState gameState;

    public static GameManager Instance { get; private set; }
    
    // Event

    

    private void Awake()
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

    private void Start()
    {
        gameState = GameState.Preparing;

        player1.onPlayerMovementFinished += ToggleGameState;
        player2.onPlayerMovementFinished += ToggleGameState;
        // Preparation

        gameState = GameState.Player1Turn;
    }

    public void RollDiceAndMovePlayer()
    {
        switch (gameState)    // only accept player input if it is in player 1 turn or player 2 turn
        {
            case GameState.Player1Turn:
                if (player2.playerState == PlayerState.Moving) // Check if the other player is moving
                {
                    return;
                }

                player1.MoveRandomly();
                

                break;
            case GameState.Player2Turn:
                if (player1.playerState == PlayerState.Moving) // Check if the other player is moving
                {
                    return;
                }

                player2.MoveRandomly();
                

                break;
        }
    }

    public void ToggleGameState(Player previousPlayer)
    {
        if (!CheckIfAnyoneWin())
        {
            if (previousPlayer.playerIndex == 1)
            {
                gameState = GameState.Player2Turn;
            }
            else
            {
                gameState = GameState.Player1Turn;
            }
        }
        else
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
        }
    }

    private bool CheckIfAnyoneWin()
    {
        return player1.ReachEnd() || player2.ReachEnd();
    }
    
    
}

public enum GameState{Preparing, Player1Turn, Player2Turn, GameOver}

