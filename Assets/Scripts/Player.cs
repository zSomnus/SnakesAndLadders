using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameBoard _gameBoard;

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

    public void MoveTo(int newPositionIndex)
    {
        PositionIndex = newPositionIndex;
        transform.localPosition = _gameBoard.wayPoints[newPositionIndex].localPosition;
    }

    public void MoveRandomly()
    {
        int newPositionIndex = Random.Range(1, 7) + PositionIndex;
        StartCoroutine(MoveToDuringTime(newPositionIndex));
    }

    private IEnumerator MoveToDuringTime(int newPositionIndex)
    {
        while (PositionIndex < newPositionIndex)
        {
            MoveTo(PositionIndex+1);
            yield return new WaitForSeconds(1);
        }
    }

    public bool ReachEnd()
    {
        return PositionIndex == _gameBoard.wayPoints.Length - 1;
    }
}
