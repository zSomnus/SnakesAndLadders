using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Balloon : MonoBehaviour
{
    [SerializeField] private GameObject balloon;
    [SerializeField] private string device;
    private AudioClip micRecord;
    [SerializeField] private float volume;
    [SerializeField] private float volumeScale;
    [SerializeField] private bool isBlowing;
    [SerializeField] private Vector3 increaseScale;
    [SerializeField] private Vector3 decreaseScale;
    [SerializeField] private float exploveScale;
    [SerializeField] private float timer;
    [SerializeField] private Text timerTxt;
    private bool isSuccess;
    private bool isUnseccess;
    [SerializeField] private Image image;
    private bool stop;
    // Start is called before the first frame update
    void Start()
    {
        balloon = GetComponent<GameObject>();
        device = Microphone.devices[0];
        micRecord = Microphone.Start(device, true, 999, 44100);
        isBlowing = false;
        increaseScale = new Vector3(0.02f, 0.02f, 0);
        decreaseScale = new Vector3(0.05f, 0.05f, 0);
        exploveScale = 5f;
        timer = 10f;
        timerTxt = GameObject.Find("BalloonTimer").GetComponent<Text>();
        image = GameObject.Find("BalloonImage").GetComponent<Image>();
        stop = false;
    }

    private void Update()
    {
        image.color = new Color(1f, 1f - (transform.localScale.x / exploveScale), 0, 1f);

        timer -= Time.deltaTime;
        timerTxt.text = Mathf.Round(timer).ToString();
        if(timer <= 0 && (transform.localScale.x < 5f))
        {
            if (!stop)
            {
                Debug.Log("Fail");
                isUnseccess = true;
                //timer = 10f;
                //timerTxt.text = "?";
                stop = true;
            }
        }
    }
    private void FixedUpdate()
    {
        volume = GetMaxVolume();//(float)Math.Round(GetMaxVolume(), 4) * volumeScale;
        if(volume > 0.9f)
        {
            isBlowing = true;
            Debug.Log("Blowing");
            transform.localScale += increaseScale;
        }
        else
        {
            isBlowing = false;
            Debug.Log("Blowing stop");
            if(transform.localScale.x > 1f && transform.localScale.y > 1f)
            {
                transform.localScale -= decreaseScale;
            }
        }

        if(transform.localScale.x > 5f && transform.localScale.y > exploveScale)
        {
            if (!stop)
            {
                Debug.Log("Success!!!");
                isSuccess = true;
                //transform.localScale = new Vector3(1f, 1f, 1f);
                stop = true;
            }
        }
    }

    private float GetMaxVolume()
    {
        float maxVolume = 0f;
        float[] volumeData = new float[128];
        int offset = Microphone.GetPosition(device) - 128 + 1;
        if (offset < 0)
        {
            return 0;
        }

        micRecord.GetData(volumeData, offset);
        for (int i = 0; i < volumeData.Length; i++)
        {
            float tempMax = volumeData[i];
            if (tempMax > maxVolume)
            {
                maxVolume = tempMax;
            }
        }
        return maxVolume;
    }

    public bool IsSuccess()
    {
        return isSuccess;
    }

    public void SetIsSuccess(bool b)
    {
        isSuccess = b;
    }

    public bool IsUnsuccess()
    {
        return isUnseccess;
    }

    public void SetIsUnsuccess(bool b)
    {
        isUnseccess = false;
    }

}
