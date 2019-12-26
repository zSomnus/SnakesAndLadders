using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static void SavePlayer(int playerOnePositionIndex, int playerTwoPositionIndex, bool isPlayerOneTurn, int playerNum)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerData.fun";
        FileStream stream = new FileStream(path, FileMode.Create);
        MainGameData data = new MainGameData(playerOnePositionIndex, playerTwoPositionIndex, isPlayerOneTurn, playerNum);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static MainGameData LoadPlayer()
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
