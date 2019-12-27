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
    

    
    [Tooltip("How much time for the player to move one tile")]
    public float timeToMoveOneTile = 1f;    

    public int PositionIndex { get; set; }
    

    /// <summary>
    /// Move the player randomly from 1 tile to 6 tiles
    /// </summary>
    public void MoveRandomNumOfTiles()    // player move because of dice rolling
    {
        MoveTiles(Random.Range(1, 7));
    }

    /// <summary>
    /// test only
    /// </summary>
    /// <returns></returns>
    public IEnumerator WaitBeforeMove(int tileNum, float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        print("Tile number to move is "+tileNum);

        MoveTiles(tileNum);
    }
    
    public void MoveTiles(int numOfTiles)    
    {
        if (playerState == PlayerState.Moving)
        {
            return;
        }
        int targetPositionIndex = PositionIndex + numOfTiles;

        StopAllCoroutines();
        StartCoroutine(MoveToTile(targetPositionIndex));



    }

    /// <summary>
    /// Move the player for X tiles forward
    /// </summary>
    /// <param name="targetPositionIndex">the destination tile index</param>
    /// <returns></returns>
    private IEnumerator MoveToTile(int targetPositionIndex)
    {
        playerState = PlayerState.Moving;

        targetPositionIndex = Mathf.Clamp(targetPositionIndex, 0, GameManager.Instance.gameBoard.wayPoints.Length - 1);
        bool movingForward = targetPositionIndex > PositionIndex ? true : false;
        if (movingForward)
        {
            while (PositionIndex < targetPositionIndex)
            {
                PositionIndex++;
                StartCoroutine(MoveOneTile(transform, timeToMoveOneTile));
            
                yield return new WaitForSeconds(timeToMoveOneTile);
            }
        }
        else
        {
            while (PositionIndex > targetPositionIndex)
            {
                PositionIndex--;
                StartCoroutine(MoveOneTile(transform, timeToMoveOneTile));
            
                yield return new WaitForSeconds(timeToMoveOneTile);
            }
        }

        if (!ProcessSpecialTiles())
        {
            onPlayerMovementFinished?.Invoke(this);
        }
    }

    public bool ProcessSpecialTiles()
    {
        Snake snakeTile = PlayerChecker.isPlayerOnSnakeHeadTile(this);
        Ladder ladderTile = PlayerChecker.isPlayerOnLadderBottomTile(this);
        GameTile gameTile = PlayerChecker.isPlayerOnGameTile(this);
        if (snakeTile)
        {
            StartCoroutine(Fall(transform, snakeTile.endIndex, 2));
        }
        else if (ladderTile)
        {
            StartCoroutine(Climb(transform, ladderTile.endIndex, 2));
        }
        else 
        {
            playerState = PlayerState.Idle; // Player doesn't need to move temporarily 
            if (gameTile)
            {
                
                if (PlayerChecker.PlayerIsAi(this))    
                {
                    float chance = Random.Range(0f, 1.0f);
                    if (chance > gameTile.successRate)
                    {
                        print($"Ai loses the mini game, moves backwards for "+gameTile.tileNum+" tiles");
                        StartCoroutine(WaitBeforeMove(-gameTile.tileNum, 2));
                    }
                    else
                    {
                        print($"Ai wins the mini game, moves forwards for "+gameTile.tileNum+" tiles");
                        StartCoroutine(WaitBeforeMove(gameTile.tileNum, 2));

                    }
                    
                }
                else
                {
                    SaveSystem.SaveMiniGameData((int) MiniGameState.Pending, playerIndex, gameTile.tileNum);
                    SaveSystem.SaveMainGameData();
                    LevelLoader.Instance.LoadScene(gameTile.gameSceneIndex);

                }


            }
        }

        return (snakeTile || ladderTile || gameTile);
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
        if (curentPos.x<GameManager.Instance.gameBoard.wayPoints[PositionIndex].position.x)
        {
            playerMovingDirection = PlayerMovingDirection.Right;
        }
        if (curentPos.x>GameManager.Instance.gameBoard.wayPoints[PositionIndex].position.x)
        {
            playerMovingDirection = PlayerMovingDirection.Left;
        }
        if (curentPos.y<GameManager.Instance.gameBoard.wayPoints[PositionIndex].position.y)
        {
            playerMovingDirection = PlayerMovingDirection.Up;
        }
        if (curentPos.y>GameManager.Instance.gameBoard.wayPoints[PositionIndex].position.y)    // If case won't be used since players can't move down on their own
        {
            playerMovingDirection = PlayerMovingDirection.Down;
        }
        
        var t = 0f;
        while (t < 1)
        {
            t+=Time.deltaTime/timeToMove;
            transform.position = Vector3.Lerp(curentPos, GameManager.Instance.gameBoard.wayPoints[PositionIndex].position, t);
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
            transform.position = Vector3.Lerp(curentPos, GameManager.Instance.gameBoard.wayPoints[destinationIndex].position, t);
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
            transform.position = Vector3.Lerp(curentPos, GameManager.Instance.gameBoard.wayPoints[destinationIndex].position, t);
            yield return null;
        }    
        
        // Update data
           
        playerState = PlayerState.Idle;
        onPlayerMovementFinished?.Invoke(this);
        
    }


}

public enum PlayerState{ Moving, Idle }
public enum PlayerMovingDirection { Left, Right, Up, Down}

