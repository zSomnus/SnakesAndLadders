using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEngine;

public class BasketryChecker : MonoBehaviour
{
    public Basketry[] Basketries;
    // Start is called before the first frame update
    
    public void Check()
    {
        bool success;
        foreach (var basketry in Basketries)
        {
            if (basketry._hasBeenHit == false)
            {
                return;
            }
        }
        PhysicsSportManager.Instance.SendMessageToMainGame(true);
    }
}
