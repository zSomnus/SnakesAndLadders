using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static void SavePlayer(int playerOnePositionIndex, int playerTwoPositionIndex, bool isPlayerOneTurn)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerData.fun";
        FileStream stream = new FileStream(path, FileMode.Create);
        PlayerData data = new PlayerData(playerOnePositionIndex, playerTwoPositionIndex, isPlayerOneTurn);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/playerData.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData playerData = formatter.Deserialize(stream) as PlayerData;
            
            stream.Close();
            return playerData;
        }
        else
        {
            return null;
        }
    }
    
    public static void DeleteSaveFile()
    {
        string path = Application.persistentDataPath + "/playerData.fun";
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        
    }
}
