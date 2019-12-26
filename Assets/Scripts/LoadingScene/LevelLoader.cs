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
    
    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneIndex);
        loadingScreen.SetActive(true);
        // asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / .9f);
            asyncOperation.allowSceneActivation = false;
            slider.value = progress;
            progressText.text = (int)(progress*100) + "%"; 
            if (progress >= 1)
            {
                print("loading finished");
                yield return new WaitForSeconds(timeWaitBeforeLoading);
                asyncOperation.allowSceneActivation =true;
            }
            
            yield return null;
        }

    }
}
