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

    public Snake[] snakes;

    public Ladder[] ladders;

    public GameTile[] gameTiles;
}
