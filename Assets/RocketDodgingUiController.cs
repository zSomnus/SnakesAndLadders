using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RocketDodgingUiController : MonoBehaviour
{
    public TextMeshProUGUI timeRemainsText; 
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        timeRemainsText.text = $"Time remains: {(int)Mathf.Clamp(MiniGameManager.Instance.TimeToWin,0,100)}";
    }
}
