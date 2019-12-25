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

    /// <summary>
    /// Check if player needs to fall down to a tile
    /// </summary>
    /// <param name="playerIndex">the current tile player is standing upon</param>
    /// <returns>The tile that player should fall down to, if player is not on snake, return -1</returns>
    public int PlayerOnSnakeOrLadder(int playerIndex)
    {
        if (snakes.Length > 0)
        {
            foreach (var snake in snakes)
            {
                if (snake.startIndex == playerIndex)
                {
                    return snake.endIndex;
                }
            }
        }


        if (ladders.Length > 0)
        {
            foreach (var ladder in ladders)
            {
                if (ladder.startIndex == playerIndex)
                {
                    return ladder.endIndex;
                }
            }  
        }
        

        return -1;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="player"></param>
    /// <returns>Return the game scene the player to go to, if there is not game tile, return -1</returns>
    public int CheckIfPlayerOnGameTile(Player player)
    {
        if (gameTiles.Length > 0)
        {
            foreach (var gameTile in gameTiles)
            {
                if (gameTile.positionIndex == player.PositionIndex)
                {
                    return gameTile.gameSceneIndex;
                }
            }  
        }

        return -1;
    }
    
    
    
    


}
