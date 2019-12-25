using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "GameTile", menuName = "GameBoardItem/GameTile")]
public class GameTile : ScriptableObject
{
    // Where is the tile
    public int positionIndex;

    public int gameSceneIndex;
}
