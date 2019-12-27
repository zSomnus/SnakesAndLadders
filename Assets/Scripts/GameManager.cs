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
    private Camera camera;
    
    // Enum
    public GameTurnState gameTurnState;
    public GameState gameState;
    public GameMode gameMode;
    
    // Singleton
    public static GameManager Instance { get; private set; }
    
    // Event
    public delegate void GamePrepareDelegate();
    public event GamePrepareDelegate OnGamePrepareFinished;

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
        
        // Preparation
        gameState = GameState.Prepare;

        
    }
    

    private void Start()
    {
        camera = Camera.main;
        LoadGameProgress();
        InitializeCamera();
        OnGamePrepareFinished?.Invoke();
        gameState = GameState.InProgress;

    }

    private void InitializeCamera()
    {
        var multipleTargetCamera = camera.GetComponent<MultipleTargetCamera>();
        if (multipleTargetCamera != null)
        {
            multipleTargetCamera.UpdateCamera();
        }
    }

    private void Update()
    {
        GodMode();

    }

    public void RollDiceAndMovePlayer()
    {
        switch (gameTurnState)    // only accept player input if it is in player 1 turn or player 2 turn
        {
            case GameTurnState.Player1Turn:
                print("player 1 turn");
                players[0].MoveRandomNumOfTiles();
                break;
            case GameTurnState.Player2Turn:
                print("player 2 turn");

                players[1].MoveRandomNumOfTiles();
                break;
            case GameTurnState.Player3Turn:
                print("player 3 turn");

                players[2].MoveRandomNumOfTiles();
                break;
            case GameTurnState.Player4Turn:
                print("player 4 turn");

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
        switch (gameTurnState)    // only accept player input if it is in player 1 turn or player 2 turn
        {
            case GameTurnState.Player1Turn:
                return players[0];
            case GameTurnState.Player2Turn:
                return players[1];
            case GameTurnState.Player3Turn:
                return players[2];
            case GameTurnState.Player4Turn:
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
        if (!PlayerChecker.GetVictoryPlayer())
        {
            switch (previousPlayer.playerIndex)
            {
                case 1:
                    gameTurnState = GameTurnState.Player2Turn; 
                    break;
                case 2:
                    gameTurnState = GameTurnState.Player3Turn;
                    break;
                case 3:
                    gameTurnState = GameTurnState.Player4Turn;
                    break;
                case 4:
                    gameTurnState = GameTurnState.Player1Turn;
                    break;
            }
        }
        else     // There is a player winning
        {
            gameState = GameState.GameOver;
            if (PlayerChecker.IsPlayerWin(players[0]))
            {
                print("Player 1 won");
            }
            if(PlayerChecker.IsPlayerWin(players[1]))
            {
                print("Player 2 won");
            }

            if (PlayerChecker.IsPlayerWin(players[2]))
            {
                print("Player 3 won");
            }

            if (PlayerChecker.IsPlayerWin(players[3]))
            {
                print("Player 4 won");
            }
        }
    }

    /// <summary>
    /// This is used in the beginning of the GameManager to load game information to put players in place
    /// If there is no game progress, meaning that the player hasn't started the game, then load all the player to the first tile
    /// </summary>
    /// <returns></returns>
    private bool LoadGameProgress()
    {
        MainGameData mainGameData = SaveSystem.LoadMainGameData();

        if (mainGameData == null)
        {
            Debug.LogError("There is no main game data");
            // Player hasn't save anything yet
            return false;
        }
        
        gameMode = (GameMode) (mainGameData.playerNum-1);
        players = new Player[4];
        for (int i = 0; i < players.Length; i++)
        {
            GameObject player = Instantiate(ResourceManager.Instance.players[i], gameBoard.wayPoints[mainGameData.playersPositionsIndexes[i]].position, Quaternion.identity);
            players[i] = player.GetComponent<Player>();
            players[i].PositionIndex = mainGameData.playersPositionsIndexes[i];



        }


        switch (mainGameData.playerTurnIndex)
        {
            case 0:
                gameTurnState = GameTurnState.Player1Turn;
                break;
            case 1:
                gameTurnState = GameTurnState.Player2Turn;
                break;
            case 2:
                gameTurnState = GameTurnState.Player3Turn;
                break;
            case 3:
                gameTurnState = GameTurnState.Player4Turn;
                break;
        }

        

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
                    gameTurnState = GameTurnState.Player1Turn;
                    break;
                case 2:
                    gameTurnState = GameTurnState.Player2Turn;
                    break;
                case 3:
                    gameTurnState = GameTurnState.Player3Turn;
                    break;
                case 4:
                    gameTurnState = GameTurnState.Player4Turn;
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
    
    private void AiBeginMoving(Player playerJustStopMoving)
    {
        if ((int) gameMode != 3 && (playerJustStopMoving.playerIndex <= 3 && playerJustStopMoving.playerIndex >= (int) gameMode + 1))
        {
            print(playerJustStopMoving.playerIndex);
            print((int) gameMode);
            print("Ai move");
            StartCoroutine(AiMove());
        }

    }

    private IEnumerator AiMove()
    {
        yield return new WaitForSeconds(2);
        RollDiceAndMovePlayer();
    }

    
}


public enum GameState
{
    Prepare,
    InProgress,
    GameOver
}

public enum GameTurnState{Player1Turn, Player2Turn, Player3Turn, Player4Turn}
public enum GameMode{OnePlayer, TwoPlayers, ThreePlayers, FourPlayers}

