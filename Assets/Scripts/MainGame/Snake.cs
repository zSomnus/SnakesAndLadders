using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Snake", menuName = "GameBoardItem/Snake")]
public class Snake : ScriptableObject
{
    public int startIndex;
    public int endIndex;
    public Sprite sprite;
}
