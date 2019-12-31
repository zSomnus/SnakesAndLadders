using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlowBalloonController : MonoBehaviour
{
    [SerializeField] private GameObject intro;
    [SerializeField] private GameObject balloon;
    [SerializeField] private GameObject countdown;
    [SerializeField] private float timer;
    [SerializeField] private Text countdownTxt;
    [SerializeField] private Balloon balloonState;
    private bool start;
    private bool stop;
    // Start is called before the first frame update
    private void Awake()
    {
        intro = GameObject.Find("Intro");
        balloon = GameObject.Find("Balloon");
        countdown = GameObject.Find("Countdown");
        countdownTxt = GameObject.Find("CountdownNumber").GetComponent<Text>();
        balloonState = balloon.GetComponent<Balloon>();

    }

    void Start()
    {
        timer = 3f;
        intro.SetActive(true);
        balloon.SetActive(false);
        countdown.SetActive(false);
        start = false;

        Application.RequestUserAuthorization(UserAuthorization.Microphone);
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            countdownTxt.text = Mathf.Round(timer).ToString();
            timer -= Time.deltaTime;
        }
        if (timer <= 0)
        {
            countdown.SetActive(false);
            balloon.SetActive(true);

            if (balloonState.IsUnsuccess())
            {
                balloon.SetActive(false);
                //Debug.LogError("Fail");
                SaveSystem.UpdateMiniGameData(false);
                LevelLoader.Instance.LoadMainGame();
                balloonState.SetIsUnsuccess(false);
                start = false;
            }
            if (balloonState.IsSuccess())
            {
                balloon.SetActive(false);
                //Debug.LogError("Success!");
                SaveSystem.UpdateMiniGameData(true);
                LevelLoader.Instance.LoadMainGame();
                balloonState.SetIsSuccess(false);
                start = false;
            }
        }

    }

    public void StartButton()
    {
        start = true;
        intro.SetActive(false);
        countdown.SetActive(true);
    }
}
