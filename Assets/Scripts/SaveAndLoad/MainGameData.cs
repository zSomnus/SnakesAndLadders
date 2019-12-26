﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MainGameData
{
    public int player1PositionIndex;
    public int player2PositionIndex;
    public bool isPlayerOneTurn;

    public MainGameData(int player1PositionIndex, int player2PositionIndex, bool isPlayerOneTurn)
    {
        this.player1PositionIndex = player1PositionIndex;
        this.player2PositionIndex = player2PositionIndex;
        this.isPlayerOneTurn = isPlayerOneTurn;
    }
}