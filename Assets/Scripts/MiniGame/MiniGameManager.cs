using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    // Reference
    public MiniGamePlayer player;
    public float TimeToWin = 15f;

    public static MiniGameManager Instance;
    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        TimeToWin -= Time.deltaTime;
        if (TimeToWin <= 0)
        {
            SendWinMessageToMainGame();
        }
    }

    public void SendWinMessageToMainGame()
    {
        SaveSystem.UpdateMiniGameData(true);
        LevelLoader.Instance.LoadMainGame();
    }

    public void SendFailureMessageToMainGame()
    {
        SaveSystem.UpdateMiniGameData(false);
        LevelLoader.Instance.LoadMainGame();
    }
}
