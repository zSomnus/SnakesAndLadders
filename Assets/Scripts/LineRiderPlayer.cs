using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRiderPlayer : MonoBehaviour
{
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Die()
    {
        print("You have died");
    }

    public void Play()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}
