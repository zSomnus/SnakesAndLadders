using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    // Reference
    public Player[] players;
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
        // Preparation
        gameState = GameState.Preparing;

        LoadGameProgress();



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
                players[0].MoveRandomNumOfTiles();
                break;
            case GameState.Player2Turn:
                players[1].MoveRandomNumOfTiles();
                break;
            case GameState.Player3Turn:
                players[2].MoveRandomNumOfTiles();
                break;
            case GameState.Player4Turn:
                players[3].MoveRandomNumOfTiles();
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

        if (Input.GetKey(KeyCode.F))
        {
            print(
                $"{GameManager.Instance.players[0].PositionIndex}, {GameManager.Instance.players[1].PositionIndex}, {GameManager.Instance.players[2].PositionIndex}, {GameManager.Instance.players[3].PositionIndex}");

        }

        if (Input.GetKey(KeyCode.G))
        {
            int[] testArray = new int[3];
            testArray[0] = 1;
            testArray[1] = 2;
            testArray[2] = 3;
            // MainGameData mainGameDataTest = new MainGameData(testArray, 1, 1);
            SaveSystem.SaveMainGameData(testArray,1,1);
            var mainGameDataRetrieved = SaveSystem.LoadMainGameData();
            foreach (var num in mainGameDataRetrieved.playersPositionsIndexes)
            {
                print(num);
            }
        }
    }

    private Player GetPlayerInTurn()
    {
        switch (gameState)    // only accept player input if it is in player 1 turn or player 2 turn
        {
            case GameState.Player1Turn:
                return players[0];
            case GameState.Player2Turn:
                return players[1];
            case GameState.Player3Turn:
                return players[2];
            case GameState.Player4Turn:
                return players[3];
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
            switch (previousPlayer.playerIndex)
            {
                case 1:
                        gameState = GameState.Player2Turn; 
                    break;
                case 2:
                    if (gameMode == GameMode.ThreePlayers ||
                        gameMode == GameMode.FourPlayers)
                    {
                        gameState = GameState.Player3Turn;
                    }
                    else
                    {
                        gameState = GameState.Player1Turn;
                    }
                    break;
                case 3:
                    if (gameMode == GameMode.FourPlayers)
                    {
                        gameState = GameState.Player4Turn;
                    }
                    else
                    {
                        gameState = GameState.Player1Turn;
                    }
                    break;
                case 4:
                    gameState = GameState.Player1Turn;
                    break;
            }
        }
        else
        {
            gameState = GameState.GameOver;
            if (PlayerPositionChecker.IsPlayerWin(players[0]))
            {
                print("Player 1 won");
            }
            if(PlayerPositionChecker.IsPlayerWin(players[1]))
            {
                print("Player 2 won");
            }

            if (PlayerPositionChecker.IsPlayerWin(players[2]))
            {
                print("Player 3 won");
            }

            if (PlayerPositionChecker.IsPlayerWin(players[3]))
            {
                print("Player 4 won");
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


        players = new Player[mainGameData.playerNum];
        for (int i = 0; i < players.Length; i++)
        {
            
            GameObject player = Instantiate(ResourceManager.Instance.players[i], gameBoard.wayPoints[mainGameData.playersPositionsIndexes[i]].position, Quaternion.identity);
            players[i] = player.GetComponent<Player>();
            players[i].PositionIndex = mainGameData.playersPositionsIndexes[i];



        }


        switch (mainGameData.playerTurnIndex)
        {
            case 0:
                gameState = GameState.Player1Turn;
                break;
            case 1:
                gameState = GameState.Player2Turn;
                break;
            case 2:
                gameState = GameState.Player3Turn;
                break;
            case 3:
                gameState = GameState.Player4Turn;
                break;
        }

        gameMode = (GameMode) (mainGameData.playerNum-1);

        for (int i = 0; i < players.Length; i++)
        {
            players[i].playerIndex = i + 1;
        }

        foreach (var player in players)
        {
            player.onPlayerMovementFinished += ToggleGameState;
            player.onPlayerMovementFinished += AiBeginMoving;
        }


        MiniGameData miniGameData = SaveSystem.LoadMiniGameData();
        if (miniGameData != null)    // If the player comes back from the mini game
        {

            switch (miniGameData.playerIndex)
            {
                case 1:
                    gameState = GameState.Player1Turn;
                    break;
                case 2:
                    gameState = GameState.Player2Turn;
                    break;
                case 3:
                    gameState = GameState.Player3Turn;
                    break;
                case 4:
                    gameState = GameState.Player4Turn;
                    break;
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

public enum GameState{Preparing, Player1Turn, Player2Turn, Player3Turn, Player4Turn, GameOver}
public enum GameMode{OnePlayer, TwoPlayers, ThreePlayers, FourPlayers}

