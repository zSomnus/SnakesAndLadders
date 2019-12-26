using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class is used in mini game to return to the main scene
/// </summary>
public class MainSceneLoader : MonoBehaviour
{
    
    public void LoadMainScene(bool isSuccessful)
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
        LevelLoader.Instance.LoadMainGameFromMiniGame();
    }
    

}
