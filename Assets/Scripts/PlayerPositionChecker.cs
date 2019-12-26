﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerPositionChecker 
{
    private static Player _player1;

    private static Player _player2;

    private static GameBoard _gameBoard;

    private static bool hasPrepared = false;
    // Start is called before the first frame update

    public static void Prepare()
    {
        _player1 = GameManager.Instance.player1;
        _player2 = GameManager.Instance.player2;
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

    
    
    
    
}