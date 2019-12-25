using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Player : MonoBehaviour
{
    // To indicate whether the player is moving or standing still
    public PlayerState playerState = PlayerState.Idle;
    
    // To indicate which direction the player is moving to
    public PlayerMovingDirection playerMovingDirection = PlayerMovingDirection.Right;
    
    
    
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

    /// <summary>
    /// Move the player for X tiles forward
    /// </summary>
    /// <param name="targetPositionIndex">the destination tile index</param>
    /// <returns></returns>
    private IEnumerator MoveForward(int targetPositionIndex)
    {
        while (PositionIndex < targetPositionIndex)
        {
            PositionIndex++;
            StartCoroutine(MoveOneTile(transform, timeToMoveOneTile));
            
            yield return new WaitForSeconds(1.5f);
        }

        if (_gameBoard.PlayerOnSnakeOrLadder(PositionIndex) >= 0)
        {
            StartCoroutine(MoveGeneral(transform, _gameBoard.PlayerOnSnakeOrLadder(PositionIndex), 2));
        }
        else
        {
            playerState = PlayerState.Idle;
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
    
    private IEnumerator MoveGeneral(Transform playerTransform,int destinationIndex, float timeToMove)    // player movement implementation
    {
        playerState = PlayerState.Moving;
        var curentPos = playerTransform.position;
        var t = 0f;
        while (t < 1)
        {
            t+=Time.deltaTime/timeToMove;
            transform.position = Vector3.Lerp(curentPos, _gameBoard.wayPoints[destinationIndex].position, t);
            yield return null;
        }    
        playerState = PlayerState.Idle;
        
    }


    /// <summary>
    /// Detect if player reaches end or not
    /// </summary>
    /// <returns></returns>
    public bool ReachEnd()
    {
        return PositionIndex == _gameBoard.wayPoints.Length - 1;
    }

    public void FallDown(int newPositionIndex)
    {
        print("player falls to " + newPositionIndex);
        
    }
}

public enum PlayerState{ Moving, Idle }
public enum PlayerMovingDirection { Left, Right, Up, Down}

