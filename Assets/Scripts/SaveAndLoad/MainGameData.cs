using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MainGameData
{
    public int[] playersPositionsIndexes;
    public int playerTurnIndex;
    public int playerNum;

    public MainGameData(int[] playersPositionsIndexes, int playerTurnIndex, int playerNum)
    {
        this.playersPositionsIndexes = playersPositionsIndexes;
        this.playerTurnIndex = playerTurnIndex;
        this.playerNum = playerNum;
    }
}
