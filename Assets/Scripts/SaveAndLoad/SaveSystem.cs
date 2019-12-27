using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    
    public static void InitiateMainGameData(int playerNum)
    {
        int[] playersPositionsIndexes;
        if (playerNum == 1)
        {
            playersPositionsIndexes = new int[2];
        }
        else
        {
            playersPositionsIndexes = new int[playerNum];
        }


        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerData.fun";
        FileStream stream = new FileStream(path, FileMode.Create);
        MainGameData data = new MainGameData(playersPositionsIndexes,0 , playerNum);
        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static void SaveMainGameData()
    {
        int[] playersPositionsIndexes = new int[GameManager.Instance.players.Length];
        
        for (int i = 0; i < playersPositionsIndexes.Length; i++)
        {
             playersPositionsIndexes[i] = GameManager.Instance.players[i].PositionIndex;
        }
        

        int whoseTurn = 0;
        switch (GameManager.Instance.gameState)
        {
            case GameState.Player1Turn:
                whoseTurn = 0;
                break;
            case GameState.Player2Turn:
                whoseTurn = 1;
                break;
            case GameState.Player3Turn:
                whoseTurn = 2;
                break;
            case GameState.Player4Turn:
                whoseTurn = 3;
                break;
        }

        int playerNum = 0;
        switch (GameManager.Instance.gameMode)
        {
            case GameMode.OnePlayer:
                playerNum = 1;
                break;
            case GameMode.TwoPlayers:
                playerNum = 2;
                break;
            case GameMode.ThreePlayers:
                playerNum = 3;
                break;
            case GameMode.FourPlayers:
                playerNum = 4;
                break;
        }
        
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerData.fun";
        FileStream stream = new FileStream(path, FileMode.Create);
        MainGameData data = new MainGameData(playersPositionsIndexes, whoseTurn, playerNum);
        formatter.Serialize(stream, data);
        stream.Close();
    }
    
    public static void SaveMainGameData(int[] playersPositionsIndexes, int whoseTurn, int playerNum)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerData.fun";
        FileStream stream = new FileStream(path, FileMode.Create);
        MainGameData data = new MainGameData(playersPositionsIndexes, whoseTurn, playerNum);
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

    public static void SaveMiniGameData(int miniGameState, int playerIndex, int tileNum)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/MiniGameData.fun";
        FileStream stream = new FileStream(path, FileMode.Create);
        MiniGameData miniGameData = new MiniGameData(miniGameState, playerIndex, tileNum);
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
