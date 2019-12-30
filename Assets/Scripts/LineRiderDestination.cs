using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRiderDestination : MonoBehaviour
{
    public float timeToWin = 3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(VictoryTimer(timeToWin));
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StopAllCoroutines();
        }
    }

    IEnumerator VictoryTimer(float timeBeforeVictory)
    {
        yield return new WaitForSeconds(timeBeforeVictory);
        LineRiderGameManager.Instance.SendMessage(true);
    }
}
