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
        SaveSystem.UpdateMiniGameData(isSuccessful);
        LevelLoader.Instance.LoadMainGame();

    }
    

}
