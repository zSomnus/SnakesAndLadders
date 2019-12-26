using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Player player1;
    public Player player2;

    public GameBoard gameBoard;
    
    
    public GameState gameState;
    public GameMode gameMode;

    public static GameManager Instance { get; private set; }
    
    
    
    // Event
    public delegate void MiniGameDelegate(Player player);
    public event  MiniGameDelegate onMiniGameFinished;

    

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
        player1.onPlayerMovementFinished += MoveAi;
        // Preparation
        if (!LoadGameProgress())
        {
            gameState = GameState.Player1Turn;
        }
        else
        {
            MiniGameData miniGameData = SaveSystem.LoadMiniGameData();
            
            
            if (miniGameData != null)
            {
                if (miniGameData.playerIndex == 1)
                {
                    gameState = GameState.Player1Turn;
                }
                else if (miniGameData.playerIndex == 2)
                {
                    gameState = GameState.Player2Turn;
                }
                
                // if (miniGameData.playerIndex == 1 && gameMode == GameMode.OnePlayer)    // if player plays the mini game
                // {
                    // RollDiceAndMovePlayer();
                // }
                if (miniGameData.state == 1)    // Success
                {
                    print($"Player {miniGameData.playerIndex} needs to move forward {miniGameData.tileNum} tiles");
                    MoveMiniGamePlayerForCertainTile(miniGameData.tileNum);
                }
                else if(miniGameData.state == 2)    // Failure
                {
                    print($"Player {miniGameData.playerIndex} needs to move backwards {miniGameData.tileNum} tiles");                    MoveMiniGamePlayerForCertainTile(miniGameData.tileNum);
                    MoveMiniGamePlayerForCertainTile(-miniGameData.tileNum);
                }
            }
            else
            {
                Debug.LogWarning("There is no mini game data");
            }
        }
        
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
    
    public void MoveMiniGamePlayerForCertainTile(int tile)
    {
        switch (gameState)    // only accept player input if it is in player 1 turn or player 2 turn
        {
            case GameState.Player1Turn:
                if (player2.playerState == PlayerState.Moving) // Check if the other player is moving
                {
                    return;
                }

                player1.MoveCertainTiles(tile);
                

                break;
            case GameState.Player2Turn:
                if (player1.playerState == PlayerState.Moving) // Check if the other player is moving
                {
                    return;
                }

                player2.MoveCertainTiles(tile);
                

                break;
        }
    }

    /// <summary>
    /// If previous player is player1, then change the state to Player2Turn, vice versa.
    /// </summary>
    /// <param name="previousPlayer"></param>
    public void ToggleGameState(Player previousPlayer)
    {
        if (!isTherePlayerWin())
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

    /// <summary>
    /// Return the player who wins the game, return none if there isn't any
    /// </summary>
    /// <returns></returns>
    private Player isTherePlayerWin()
    {
        if (player1.ReachEnd())
        {
            return player1;
        }
        else if (player2.ReachEnd())
        {
            return player2;
        }
        else
        {
            return null;
        }
    }

    public void SaveGameProgress()
    {
        bool isPlayerOneTurn = gameState == GameState.Player1Turn ? true : false;
        int playerNum = gameMode == GameMode.OnePlayer ? 1 : 2;
        SaveSystem.SavePlayer(player1.PositionIndex, player2.PositionIndex, isPlayerOneTurn, playerNum);
    }

    public bool LoadGameProgress()
    {
        MainGameData mainGameData = SaveSystem.LoadPlayer();
        if (mainGameData == null)
        {
            // Player hasn't save anything yet
            return false;
        }
        player1.PositionIndex = mainGameData.player1PositionIndex;
        player2.PositionIndex = mainGameData.player2PositionIndex;
        player1.transform.position = gameBoard.wayPoints[player1.PositionIndex].position;
        player2.transform.position = gameBoard.wayPoints[player2.PositionIndex].position;
        gameState = mainGameData.isPlayerOneTurn ? GameState.Player2Turn : GameState.Player1Turn;
        switch (mainGameData.playerNum)
        {
            case 1:
                gameMode = GameMode.OnePlayer;
                break;
            case 2:
                gameMode = GameMode.TwoPlayers;
                break;
        }

        return true;
    }
    

    private void OnApplicationQuit()
    {
        SaveSystem.DeleteSaveFile();
    }

    public void MoveAi(Player player)
    {
        if (player.playerIndex == 1 && gameMode == GameMode.OnePlayer)    // If the player who just stop the movement is player1
        {
            StartCoroutine(AiThinkAndMove());
        }
    }

    IEnumerator AiThinkAndMove()
    {
        yield return new WaitForSeconds(2);
        RollDiceAndMovePlayer();
    }
}

public enum GameState{Preparing, Player1Turn, Player2Turn, GameOver}
public enum GameMode{OnePlayer, TwoPlayers}

