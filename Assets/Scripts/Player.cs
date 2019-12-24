using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameBoard _gameBoard;
    public float timeToMoveOneTile = 1f;

    public int PositionIndex { get; set; }
    
    // Start is called before the first frame update
    void Start()
    {
        _gameBoard = GameManager.Instance.gameBoard;
        PositionIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void MoveRandomly()    // player move because of dice rolling
    {
        int targetPositionIndex = PositionIndex + Random.Range(1, 7);
        
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

        if (_gameBoard.PlayerOnSnake(PositionIndex) >= 0)
        {
            StartCoroutine(MoveGeneral(transform, _gameBoard.PlayerOnSnake(PositionIndex), 2));
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
        var curentPos = playerTransform.position;
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
        var curentPos = playerTransform.position;
        var t = 0f;
        while (t < 1)
        {
            t+=Time.deltaTime/timeToMove;
            transform.position = Vector3.Lerp(curentPos, _gameBoard.wayPoints[destinationIndex].position, t);
            yield return null;
        }    
        
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
