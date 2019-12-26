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

                if (miniGameData.isSucceeded)
                {
                    print("Player has won the mini game");
                }
                else
                {
                    print("Player has lost the mini game");
                }
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

    public void SaveGameProgress()
    {
        if (gameState == GameState.Player1Turn)
        {
            if (gameMode == GameMode.OnePlayer)
            {
                SaveSystem.SavePlayer(player1.PositionIndex, player2.PositionIndex, true, 1);
            }
            else
            {
                SaveSystem.SavePlayer(player1.PositionIndex, player2.PositionIndex, true, 2);

            }
        }
        else if(gameState == GameState.Player2Turn)
        {
            if (gameMode == GameMode.OnePlayer)
            {
                SaveSystem.SavePlayer(player1.PositionIndex, player2.PositionIndex, false, 1);
            }
            else
            {
                SaveSystem.SavePlayer(player1.PositionIndex, player2.PositionIndex, false,2);
            }
        }
        else
        {
            print("Game is not in progress");
        }
    }

    public bool LoadGameProgress()
    {
        MainGameData mainGameData = SaveSystem.LoadPlayer();
        if (mainGameData == null)
        {
            // Player hasn't save anything yet
            return false;
        }
        print("Loading game progress");
        player1.PositionIndex = mainGameData.player1PositionIndex;
        player2.PositionIndex = mainGameData.player2PositionIndex;
        player1.transform.position = gameBoard.wayPoints[player1.PositionIndex].position;
        player2.transform.position = gameBoard.wayPoints[player2.PositionIndex].position;
        if (mainGameData.isPlayerOneTurn)
        {
            print("set game to player 2 turn");
            gameState = GameState.Player2Turn;
        }
        else
        { 
            print("set game to player 1 turn");
            gameState = GameState.Player1Turn;
        }

        if (mainGameData.playerNum == 1)
        {
            gameMode = GameMode.OnePlayer;
        }
        else if (mainGameData.playerNum == 2)
        {
            gameMode = GameMode.TwoPlayers;
        }

        return true;
    }



    public void PlayMiniGame(int sceneIndex)
    {
        SaveGameProgress();
        SceneManager.LoadScene(sceneIndex);
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

