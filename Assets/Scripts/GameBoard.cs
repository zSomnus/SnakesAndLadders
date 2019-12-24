using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    public Transform[] wayPoints;

    public Snake[] snakes;

    public Ladder[] ladders;

    /// <summary>
    /// Check if player needs to fall down to a tile
    /// </summary>
    /// <param name="playerIndex">the current tile player is standing upon</param>
    /// <returns>The tile that player should fall down to, if player is not on snake, return -1</returns>
    public int PlayerOnSnake(int playerIndex)
    {
        if (snakes.Length == 0)
            return -1;
        foreach (var snake in snakes)
        {
            if (snake.startIndex == playerIndex)
            {
                return snake.endIndex;
            }
        }

        return -1;
    }
    


}
