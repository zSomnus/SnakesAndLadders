using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MiniGameData
{
    public bool isSucceeded;

    public MiniGameData(bool isSucceeded)
    {
        this.isSucceeded = isSucceeded;
    }
}
