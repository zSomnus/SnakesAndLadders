using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "GameTile", menuName = "GameBoardItem/GameTile")]
public class GameTile : ScriptableObject
{
    // Where should this tile sit on the map, (0 is the bottom left corner)    see the image "GameBoardWayPoints" under the project directory to check details
    public int positionIndex;

    // The scene index should this game tile load
    public int gameSceneIndex;

    // The number of tile the player will move forward if he/she wins, and move backwards if he/she loses
    public int tileNum;

    // The chance of AI succeeding in this game 
    public float successRate;
}
