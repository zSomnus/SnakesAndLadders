using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimonSaysController : MonoBehaviour
{
    [SerializeField] SimonSaysButton[] buttons = new SimonSaysButton[4];
    [SerializeField] Text totalText;
    [SerializeField] int total = 5;

    List<int> sequenceList = new List<int>();

    float WaitDuration = 0.5f;
    float waitTimer;
    int waitIndex = 0;
    float initialWait = 3f;

    int verifyIndex;

    void Start()
    {
        IncrementSequence();
    }

    private void UpdateText()
    {
        totalText.text = $"Remaining: {total - sequenceList.Count + 1} left.";
    }

    void Update()
    {
        if (initialWait > 0)
        {
            initialWait -= Time.deltaTime;
            return;
        }

        if (waitIndex >= sequenceList.Count)
        {
            return;
        }

        if (waitTimer > 0)
        {
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0)
            {
                buttons[sequenceList[waitIndex++]].Flash();
                if (waitIndex < sequenceList.Count)
                {
                    waitTimer = WaitDuration; 
                }
            }
        }
    }

    void IncrementSequence()
    {
        sequenceList.Add(Random.Range(0, 4));
        waitTimer = WaitDuration;
        UpdateText();
    }

    public void ButtonClick(int index)
    {
        if (waitTimer > 0)
        {
            return;
        }

        buttons[index].Flash();

        if (index == sequenceList[verifyIndex++])
        {
            if (verifyIndex == total)
            {
                SaveSystem.UpdateMiniGameData(true);
                LevelLoader.Instance.LoadMainGame();
            }
            else
            {
                if (verifyIndex == sequenceList.Count)
                {
                    waitIndex = 0;
                    verifyIndex = 0;
                    IncrementSequence();
                }
            }
        }
        else
        {
            SaveSystem.UpdateMiniGameData(false);
            LevelLoader.Instance.LoadMainGame();
        }
    }
}
