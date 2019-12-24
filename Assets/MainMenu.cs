using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The class stores every functionality of the main menu, including to start the main game, change the volume, exit the game, etc
/// </summary>
public class MainMenu : MonoBehaviour
{


    public void LoadGame(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
