using System.Collections;
using System.Collections.Generic;
using Assets;
using TMPro;
using UnityEngine;

public class FunkBasketballUiController : MonoBehaviour
{
    private BallSpawner ballSpawner;

    public TextMeshProUGUI ballText;
    // Start is called before the first frame update
    void Start()
    {
        ballSpawner = PhysicsSportManager.Instance.BallSpawner;
    }

    // Update is called once per frame
    void Update()
    { 
        ballText.text = $"Ball left: {ballSpawner.ballCapacity}";
    }
}
