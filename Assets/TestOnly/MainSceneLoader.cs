using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneLoader : MonoBehaviour
{
    
    public void LoadMainScene(bool isSucceeded)
    {
        SaveSystem.SaveMiniGameData(isSucceeded);
        
        SceneManager.LoadScene(1);
    }
    

}
