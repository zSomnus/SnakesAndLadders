using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RollDice : MonoBehaviour
{
    private string pointTxt;
    private int points;
    private Animation anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Roll()
    {
        Debug.Log("Roll");
        anim.Play("Rotation");
    }

    public void RandomPoints()
    {
        points = Random.Range(1, 7);
        pointTxt = points.ToString();
        Debug.Log(pointTxt);
        GameObject.Find("DiceButton").GetComponentInChildren<Text>().text = pointTxt;
    }
}
