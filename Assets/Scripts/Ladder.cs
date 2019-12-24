using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ladder", menuName = "GameBoardItem/Ladder")]
public class Ladder : ScriptableObject
{
    public int startIndex;
    public int endIndex;
    public Sprite sprite;
}
