using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerPositionChecker 
{


    private static GameBoard _gameBoard;

    private static bool hasPrepared = false;
    // Start is called before the first frame update

    public static void Prepare()
    {

        _gameBoard = GameManager.Instance.gameBoard;
        hasPrepared = true;
    }

    public static Snake isPlayerOnSnakeHeadTile(Player player)
    {
        if (!hasPrepared)
        {
            Prepare();
        }
        if (_gameBoard.snakes.Length > 0)
        {
            foreach (var snake in _gameBoard.snakes)
            {
                if (snake.startIndex == player.PositionIndex)
                {
                    return snake;
                }
            }
        }

        return null;
    }
    
    public static Ladder isPlayerOnLadderBottomTile(Player player)
    {
        if (!hasPrepared)
        {
            Prepare();
        }
        if (_gameBoard.ladders.Length > 0)
        {
            foreach (Ladder ladder in _gameBoard.ladders)
            {
                if (ladder.startIndex == player.PositionIndex)
                {
                    return ladder;
                }
            }
        }

        return null;
    }

    public static GameTile isPlayerOnGameTile(Player player)
    {
        if (!hasPrepared)
        {
            Prepare();
        }
        if (_gameBoard.gameTiles.Length > 0)
        {
            foreach (var gameTile in _gameBoard.gameTiles)
            {
                if (gameTile.positionIndex == player.PositionIndex)
                {
                    return gameTile;
                }
            }
        }

        return null;
    }

    public static bool IsPlayerWin(Player player)
    {
        if (player.PositionIndex == _gameBoard.wayPoints.Length - 1)
        {
            return true;
        }

        return false;
    }

    public static Player GetVictoryPlayer()
    {
        Player player1 = GameManager.Instance.player1;
        Player player2 = GameManager.Instance.player2;
        if (IsPlayerWin(player1))
        {
            return player1;
        }

        if (IsPlayerWin(player2))
        {
            return player2;
        }

        return null;

    }
    
    
    
    
}
