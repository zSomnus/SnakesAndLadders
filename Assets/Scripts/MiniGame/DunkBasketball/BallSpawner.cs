using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject ball;

    public int ballCapacity = 5;
    // Start is called before the first frame update
    void Start()
    {
        SpawnNewBall();
    }
    

    public void SpawnNewBall()
    {
        if (ballCapacity > 0)
        {
            ballCapacity--;
            Instantiate(ball, transform.position, Quaternion.identity);
        }
        else
        {
            PhysicsSportManager.Instance.SendMessageToMainGame(false);
        }
    }
}
