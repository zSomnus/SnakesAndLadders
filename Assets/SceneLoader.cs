using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class will remain in every scene to fade in and fade out
/// </summary>
public class SceneLoader : MonoBehaviour
{
    public GameObject transitionCanvas;
    public GameObject transitionPanel;
    private Animator transitionAnim;
    
    public static SceneLoader Instance { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        transitionPanel.SetActive(true);
        transitionAnim = transitionPanel.GetComponent<Animator>();
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

    /// <summary>
    /// When leaving the current scene, call this method to turn the screen to black, then it will automatically fade in
    /// </summary>
    public void FadeOut()
    {
        transitionAnim.SetTrigger("end");
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(1);
        transitionAnim.SetTrigger("start");
    }
    
}
