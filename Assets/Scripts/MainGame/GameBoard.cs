using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class is used to contain game board items (Like the position of the snakes and ladders)
/// </summary>
public class GameBoard : MonoBehaviour
{
    public Transform[] wayPoints;

    // List to store waypoints
    public List<Transform> wayPointList;

    public Snake[] snakes;

    public Ladder[] ladders;

    public GameTile[] gameTiles;

    private void Start()
    {
        //GetWayPoints();
    }

    // Add every transform that has tag "Tile" to the waypoints array
    private void GetWayPoints()
    {
        foreach(var child in GetComponentsInChildren<Transform>())
        {
            if(child.tag == "Tile")
            {
                wayPointList.Add(child.transform);
            }
        }

        wayPoints = wayPointList.ToArray();
    }
    
    


}
