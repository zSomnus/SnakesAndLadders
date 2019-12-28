using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGamePlayer : MonoBehaviour
{
    public int maxHp = 100;
    private int hp;
    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;
    }
    

    public void TakeDamage(int damageToTake)
    {
        print("player takes damage");
        hp = Mathf.Clamp(hp - damageToTake, 0, maxHp);
        if (hp == 0)
        {
            MiniGameManager.Instance.SendFailureMessageToMainGame();
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;
            transform.position = touchPosition;
        }

        transform.Translate(Input.GetAxis("Horizontal")*Time.deltaTime*10, Input.GetAxis("Vertical")*Time.deltaTime*10,0);
    }
}
