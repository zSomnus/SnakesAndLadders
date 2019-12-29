using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    public TextMeshProUGUI progressText;
    public float timeWaitBeforeLoading = 1;
    private int mainGameSceneIndex = 1;

    public static LevelLoader Instance;
    private void Start()
    {
        SaveSystem.DeleteSaveFile();
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void InitiateMainGame(int playerNum)
    { 
        SaveSystem.InitiateMainGameData(playerNum);
        LoadMainGame();

    }
    
    public void LoadMainGame()
    {
        LoadScene(mainGameSceneIndex);
    }

    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneIndex);
        loadingScreen.SetActive(true);
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / .9f);
            slider.value = progress;
            progressText.text = (int)(progress*100) + "%"; 
            if (progress >= 1)
            {
                yield return new WaitForSeconds(0.4f);

                loadingScreen.SetActive(false);
                SceneLoader.Instance.FadeOut();
                yield return new WaitForSeconds(timeWaitBeforeLoading);
                asyncOperation.allowSceneActivation =true;
            }
            
            yield return null;
        }

    }
}
