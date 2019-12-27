using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Reference
    public Player player1;
    public Player player2;
    public GameBoard gameBoard;
    
    // Enum
    public GameState gameState;
    public GameMode gameMode;
    
    // Singleton
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
        player1.onPlayerMovementFinished += AiBeginMoving;
        // Preparation
        LoadGameProgress();
        
        MiniGameData miniGameData = SaveSystem.LoadMiniGameData();
        if (miniGameData != null)    // If the player comes back from the mini game
        {
            if (miniGameData.playerIndex == 1)    // If the player 1 just played the mini game, it is still in player 1's turn
            {
                gameState = GameState.Player1Turn;
            }
            else if (miniGameData.playerIndex == 2)
            {
                gameState = GameState.Player2Turn;
            }
            
            if (miniGameData.state == 1)    // Success
            {
                GetPlayerInTurn().MoveTiles(miniGameData.tileNum);
            }
            else if(miniGameData.state == 2)    // Failure
            {
                GetPlayerInTurn().MoveTiles(-miniGameData.tileNum);
            }
        }
        else
        {
            Debug.LogWarning("There is no mini game data yet (Ignore it if this shows in the beginning of the game)");
        }
        
    }

    private void Update()
    {
        GodMode();
    }

    public void RollDiceAndMovePlayer()
    {
        switch (gameState)    // only accept player input if it is in player 1 turn or player 2 turn
        {
            case GameState.Player1Turn:
                player1.MoveRandomNumOfTiles();
                break;
            case GameState.Player2Turn:
                player2.MoveRandomNumOfTiles();
                break;
        }
    }

    private void GodMode()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            GetPlayerInTurn().MoveTiles(1);
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            GetPlayerInTurn().MoveTiles(2);
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            GetPlayerInTurn().MoveTiles(3);
        }
        else if (Input.GetKey(KeyCode.Alpha4))
        {
            GetPlayerInTurn().MoveTiles(4);
        }
        else if (Input.GetKey(KeyCode.Alpha5))
        {
            GetPlayerInTurn().MoveTiles(5);
        }
        else if (Input.GetKey(KeyCode.Alpha6))
        {
            GetPlayerInTurn().MoveTiles(6);
        }
    }

    private Player GetPlayerInTurn()
    {
        switch (gameState)    // only accept player input if it is in player 1 turn or player 2 turn
        {
            case GameState.Player1Turn:
                return player1;
                break;
            case GameState.Player2Turn:
                return player2;
                break;
        }

        return null;
    }

    /// <summary>
    /// If previous player is player1, then change the state to Player2Turn, vice versa.
    /// </summary>
    /// <param name="previousPlayer"></param>
    private void ToggleGameState(Player previousPlayer)
    {
        if (!PlayerPositionChecker.GetVictoryPlayer())
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
            if (PlayerPositionChecker.IsPlayerWin(player1))
            {
                print("Player 1 won");
            }
            if(PlayerPositionChecker.IsPlayerWin(player2))
            {
                print("Player 2 won");
            }
        }
    }

    /// <summary>
    /// This is used in the beginning of the GameManager to load game information to put players in place
    /// </summary>
    /// <returns></returns>
    private bool LoadGameProgress()
    {
        MainGameData mainGameData = SaveSystem.LoadMainGameData();
        if (mainGameData == null)
        {
            // Player hasn't save anything yet
            return false;
        }
        player1.PositionIndex = mainGameData.player1PositionIndex;
        player2.PositionIndex = mainGameData.player2PositionIndex;
        player1.transform.position = gameBoard.wayPoints[player1.PositionIndex].position;
        player2.transform.position = gameBoard.wayPoints[player2.PositionIndex].position;
        gameState = mainGameData.isPlayerOneTurn ? GameState.Player1Turn : GameState.Player2Turn;
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

    private void AiBeginMoving(Player player)
    {
        if (player.playerIndex == 1 && gameMode == GameMode.OnePlayer)    // If the player who just stop the movement is player1
        {
            StartCoroutine(AiMove());
        }
    }

    private IEnumerator AiMove()
    {
        yield return new WaitForSeconds(2);
        RollDiceAndMovePlayer();
    }
}

public enum GameState{Preparing, Player1Turn, Player2Turn, GameOver}
public enum GameMode{OnePlayer, TwoPlayers}

