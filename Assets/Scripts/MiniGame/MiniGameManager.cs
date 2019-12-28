using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    // Reference
    public float TimeToWin = 15f;
    [HideInInspector]
    public List<GameObject> homingMissiles;

    private miniGameRocketDodgeState gameState;

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

        gameState = miniGameRocketDodgeState.Pending;
    }

    // Update is called once per frame
    void Update()
    {
        TimeToWin -= Time.deltaTime;
        if (TimeToWin <= 0)
        {
            foreach (var homingMissile in homingMissiles)
            {
                Destroy(homingMissile);
            }
            
            SendWinMessageToMainGame();
        }
    }

    public void SendWinMessageToMainGame()
    {
        if (gameState == miniGameRocketDodgeState.Pending)
        {
            gameState = miniGameRocketDodgeState.Success;
            SaveSystem.UpdateMiniGameData(true);
            LevelLoader.Instance.LoadMainGame();    
        }
        
    }

    public void SendFailureMessageToMainGame()
    {
        if (gameState == miniGameRocketDodgeState.Pending)
        {
            gameState = miniGameRocketDodgeState.Failure;
            SaveSystem.UpdateMiniGameData(false);
            LevelLoader.Instance.LoadMainGame();    
        }
    }
}

public enum miniGameRocketDodgeState{Pending, Success, Failure}
