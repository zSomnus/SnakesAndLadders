using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class CutTheRopeUiManager : MonoBehaviour
{
    public RopeTimer timer;

    public TextMeshProUGUI TextRopeTimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TextRopeTimer.text = $"Time left: {(int)timer.timeLeft}";
    }
}
