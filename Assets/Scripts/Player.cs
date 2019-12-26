using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // To indicate whether the player is moving or standing still
    public PlayerState playerState = PlayerState.Idle;
    public int playerIndex = 1;
    
    // To indicate which direction the player is moving to
    public PlayerMovingDirection playerMovingDirection = PlayerMovingDirection.Right;
    
    public delegate void PlayerMovementFinishedDelegate(Player player);
    public event PlayerMovementFinishedDelegate onPlayerMovementFinished;
    
    private GameBoard _gameBoard;
    
    [Tooltip("How much time for the player to move one tile")]
    public float timeToMoveOneTile = 1f;    

    public int PositionIndex { get; set; }
    
    // Start is called before the first frame update
    void Start()
    {
        _gameBoard = GameManager.Instance.gameBoard;
        PositionIndex = 0;
    }

    public void MoveRandomly()    // player move because of dice rolling
    {
        int targetPositionIndex = PositionIndex + Random.Range(1, 7);

        playerState = PlayerState.Moving;
        StartCoroutine(MoveForward(targetPositionIndex));
        
    }
    
    public void MoveCertainTiles(int tile)    // player move because of dice rolling
    {
        print("Move " + tile + " tiles");
        int targetPositionIndex = PositionIndex + tile;

        playerState = PlayerState.Moving;
        if (tile > 0)
        {
            StopAllCoroutines();

            StartCoroutine(MoveForward(targetPositionIndex));
        }
        else
        {
            StopAllCoroutines();

            StartCoroutine(MoveBackward(targetPositionIndex));
        }
        
    }

    /// <summary>
    /// Move the player for X tiles forward
    /// </summary>
    /// <param name="targetPositionIndex">the destination tile index</param>
    /// <returns></returns>
    private IEnumerator MoveForward(int targetPositionIndex)
    {
        if (targetPositionIndex > _gameBoard.wayPoints.Length - 1)
        {
            targetPositionIndex = _gameBoard.wayPoints.Length - 1;
        }
        while (PositionIndex < targetPositionIndex)
        {
            PositionIndex++;
            StartCoroutine(MoveOneTile(transform, timeToMoveOneTile));
            
            yield return new WaitForSeconds(timeToMoveOneTile);
        }

        Snake snakeTile = PlayerPositionChecker.isPlayerOnSnakeHeadTile(this);
        Ladder ladderTile = PlayerPositionChecker.isPlayerOnLadderBottomTile(this);
        GameTile gameTile = PlayerPositionChecker.isPlayerOnGameTile(this);
        if (snakeTile)
        {
            StartCoroutine(Fall(transform, snakeTile.endIndex, 2));
        }
        else if (ladderTile)
        {
            StartCoroutine(Climb(transform, ladderTile.endIndex, 2));
        }
        else // Player doesn't need to move anymore
        {
            playerState = PlayerState.Idle;
            if (gameTile)
            {
                SaveSystem.SaveMiniGameData((int) MiniGameState.Pending, playerIndex, gameTile.tileNum);
                LevelLoader.Instance.LoadMiniGame(gameTile.gameSceneIndex);
            }
        }

        if (!snakeTile && !ladderTile && !gameTile)
        {
            onPlayerMovementFinished?.Invoke(this);
        }
    }
    
    private IEnumerator MoveBackward(int targetPositionIndex)
    {
        if (targetPositionIndex < 0)
        {
            targetPositionIndex = 0;
        }

        print("Move backward to the tile index " + targetPositionIndex);

        while (PositionIndex > targetPositionIndex)
        {
            PositionIndex--;
            StartCoroutine(MoveOneTile(transform, timeToMoveOneTile));
            
            yield return new WaitForSeconds(timeToMoveOneTile);
        }

        Snake snakeTile = PlayerPositionChecker.isPlayerOnSnakeHeadTile(this);
        Ladder ladderTile = PlayerPositionChecker.isPlayerOnLadderBottomTile(this);
        GameTile gameTile = PlayerPositionChecker.isPlayerOnGameTile(this);
        if (snakeTile)
        {
            StartCoroutine(Fall(transform, snakeTile.endIndex, 2));
        }
        else if (ladderTile)
        {
            StartCoroutine(Climb(transform, ladderTile.endIndex, 2));
        }
        else // Player doesn't need to move anymore
        {
            playerState = PlayerState.Idle;
        }
        
        if (gameTile)
        {
            SaveSystem.SaveMiniGameData((int)MiniGameState.Pending, playerIndex, gameTile.tileNum);
            LevelLoader.Instance.LoadMiniGame(gameTile.gameSceneIndex);
        }
        else
        {
            onPlayerMovementFinished?.Invoke(this);
        }

    }
    
    /// <summary>
    /// Move the player for one tile at a time
    /// </summary>
    /// <param name="playerTransform">the player transform</param>
    /// <param name="timeToMove">The time for player to move a tile</param>
    /// <returns></returns>

    private IEnumerator MoveOneTile(Transform playerTransform, float timeToMove)    // player movement implementation
    {
        Vector3 curentPos = playerTransform.position;
        if (curentPos.x<_gameBoard.wayPoints[PositionIndex].position.x)
        {
            playerMovingDirection = PlayerMovingDirection.Right;
        }
        if (curentPos.x>_gameBoard.wayPoints[PositionIndex].position.x)
        {
            playerMovingDirection = PlayerMovingDirection.Left;
        }
        if (curentPos.y<_gameBoard.wayPoints[PositionIndex].position.y)
        {
            playerMovingDirection = PlayerMovingDirection.Up;
        }
        if (curentPos.y>_gameBoard.wayPoints[PositionIndex].position.y)    // If case won't be used since players can't move down on their own
        {
            playerMovingDirection = PlayerMovingDirection.Down;
        }
        
        var t = 0f;
        while (t < 1)
        {
            t+=Time.deltaTime/timeToMove;
            transform.position = Vector3.Lerp(curentPos, _gameBoard.wayPoints[PositionIndex].position, t);
            yield return null;
        }    
        
    }
    
    private IEnumerator Fall(Transform playerTransform,int destinationIndex, float timeToMove)    // player movement implementation
    {
        // Update data
        // playerState = PlayerState.Falling;
        PositionIndex = destinationIndex;

        // Update graphics
        var curentPos = playerTransform.position;
        var t = 0f;
        while (t < 1)
        {
            t+=Time.deltaTime/timeToMove;
            transform.position = Vector3.Lerp(curentPos, _gameBoard.wayPoints[destinationIndex].position, t);
            yield return null;
        }    
        playerState = PlayerState.Idle;
        onPlayerMovementFinished?.Invoke(this);
        
    }
    
    private IEnumerator Climb(Transform playerTransform,int destinationIndex, float timeToMove)    // player movement implementation
    {
        // Update data
        // playerState = PlayerState.Climbing;
        PositionIndex = destinationIndex; 
        
        // Update graphics
        var curentPos = playerTransform.position;
        var t = 0f;
        while (t < 1)
        {
            t+=Time.deltaTime/timeToMove;
            transform.position = Vector3.Lerp(curentPos, _gameBoard.wayPoints[destinationIndex].position, t);
            yield return null;
        }    
        
        // Update data
           
        playerState = PlayerState.Idle;
        onPlayerMovementFinished?.Invoke(this);
        
    }


    /// <summary>
    /// Detect if player reaches end or not
    /// </summary>
    /// <returns></returns>
    public bool ReachEnd()
    {
        return PositionIndex == _gameBoard.wayPoints.Length - 1;
    }

}

public enum PlayerState{ Moving, Idle, Falling, Climbing }
public enum PlayerMovingDirection { Left, Right, Up, Down}

