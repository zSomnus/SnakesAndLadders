using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    
    public static void InitiateMainGameData(int playerNum)
    {
        int playerOnePositionIndex = 0;
        int playerTwoPositionIndex = 0;
        bool isPlayerOneTurn = true;

        
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerData.fun";
        FileStream stream = new FileStream(path, FileMode.Create);
        MainGameData data = new MainGameData(playerOnePositionIndex, playerTwoPositionIndex, isPlayerOneTurn, playerNum);
        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static void SaveMainGameData()
    {
        int playerOnePositionIndex = GameManager.Instance.player1.PositionIndex;
        int playerTwoPositionIndex = GameManager.Instance.player2.PositionIndex;
        bool isPlayerOneTurn = true;
        if (GameManager.Instance.gameState == GameState.Player1Turn)
        {
            isPlayerOneTurn = true;
        }
        else if (GameManager.Instance.gameState == GameState.Player2Turn)
        {
            isPlayerOneTurn = false;
        }

        int playerNum = GameManager.Instance.gameMode == GameMode.OnePlayer ? 1 : 2;
        
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerData.fun";
        FileStream stream = new FileStream(path, FileMode.Create);
        MainGameData data = new MainGameData(playerOnePositionIndex, playerTwoPositionIndex, isPlayerOneTurn, playerNum);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static MainGameData LoadMainGameData()
    {
        string path = Application.persistentDataPath + "/playerData.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            MainGameData mainGameData = formatter.Deserialize(stream) as MainGameData;
            
            stream.Close();
            return mainGameData;
        }
        else
        {
            return null;
        }
    }

    public static void SaveMiniGameData(int state, int playerIndex, int tileNum)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/MiniGameData.fun";
        FileStream stream = new FileStream(path, FileMode.Create);
        MiniGameData miniGameData = new MiniGameData(state, playerIndex, tileNum);
        formatter.Serialize(stream, miniGameData);
        stream.Close();
    }
    
    public static void UpdateMiniGameData(bool isSuccessful)
    {
        MiniGameData miniGameData = SaveSystem.LoadMiniGameData();
        if (isSuccessful)
        {
            miniGameData.state = 1;
        }
        else
        {
            miniGameData.state = 2;
        }
        SaveSystem.SaveMiniGameData(miniGameData.state, miniGameData.playerIndex, miniGameData.tileNum);
    }

    
    
    
    public static MiniGameData LoadMiniGameData()
    {
        string path = Application.persistentDataPath + "/MiniGameData.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            MiniGameData miniGameData = formatter.Deserialize(stream) as MiniGameData;
            
            stream.Close();
            return miniGameData;
        }
        else
        {
            return null;
        }
    }
    
    public static void DeleteSaveFile()
    {
        string mainGameDataPath = Application.persistentDataPath + "/playerData.fun";
        string miniGameDataPath = Application.persistentDataPath + "/MiniGameData.fun";
        if (File.Exists(mainGameDataPath))
        {
            File.Delete(mainGameDataPath);
        }
        
        if (File.Exists(miniGameDataPath))
        {
            File.Delete(miniGameDataPath);
        }
        
        
    }
}
