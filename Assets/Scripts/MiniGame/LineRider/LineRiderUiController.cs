using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LineRiderUiController : MonoBehaviour
{
    // Reference
    public TextMeshProUGUI missionText;
    public TextMeshProUGUI subMissionText;
    public TextMeshProUGUI inkLeftText;
    public TextMeshProUGUI attemptLeftText;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        inkLeftText.text = $"Ink left: {LineRiderGameManager.Instance.inkLeft}";
        attemptLeftText.text = $"Attempt left: {LineRiderGameManager.Instance.attemptTimes}";
    }

    public void HideTipText()
    {
        missionText.gameObject.SetActive(false);
        subMissionText.gameObject.SetActive(false);
    }
}
