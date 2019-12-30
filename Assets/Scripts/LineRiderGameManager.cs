using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEngine;

public class LineRiderGameManager : MonoBehaviour
{
    // Singleton
    public static LineRiderGameManager Instance;
    
    // Reference
    [HideInInspector]
    public LineRiderPlayer player;
    public LineRiderUiController UiController;

    public List<Line> lines;
    
    // Adjustable value
    public int inkCapacity = 1000;
    public int attemptTimes = 3;
    public float TimeWaitToSpawnPlayer = 1f;
    
    
    [HideInInspector]
    public int inkLeft;

    public DrawState gameState;
    void Awake()    
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
    // Start is called before the first frame update
    void Start()
    {
        inkLeft = inkCapacity;
        gameState = DrawState.Pending;
    }

    public void LoseCurrentAttempt()
    {
        Destroy(player.gameObject);
        if (attemptTimes > 0)
        {
            attemptTimes--;
            inkLeft = inkCapacity;
            LineRiderPlayerSpawner.Instance.Spawn(TimeWaitToSpawnPlayer);
            foreach (Line line in lines)
            {
                line.Clear();
            }
            lines = new List<Line>();
        }
        else
        {
            
            print("send lose message");            
            gameState = DrawState.Failure;
            SendMessage(false);
        }
    }


    public bool DecreaseInk(int inkAmount)
    {
        if (gameState == DrawState.Pending)
        {
            if (inkLeft <= 0)
            {
                LoseCurrentAttempt();
                return false;
            }
            else
            {
                inkLeft -= inkAmount;
                return true;
            }
        }
        else
        {
            return false;
        }
    }

    public void Play()
    {
        player.Play();
    }

    public void SendMessage(bool success)
    {
        SaveSystem.UpdateMiniGameData(success);
        LevelLoader.Instance.LoadMainGame();
    }
}

public enum DrawState{Pending, Victory, Failure}
