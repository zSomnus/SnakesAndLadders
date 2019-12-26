using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MiniGameData
{
    public int state;
    public int playerIndex;
    public int tileNum;

    public MiniGameData(int miniGameState, int playerIndex, int tileNum)
    {
        this.state = miniGameState;
        this.playerIndex = playerIndex;
        this.tileNum = tileNum;
    }
}

public enum MiniGameState{ Pending, Success, Failure}
