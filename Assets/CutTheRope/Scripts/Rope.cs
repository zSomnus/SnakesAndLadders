using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public Rigidbody2D hook;

    public GameObject linkPrefab;
    public Weight weight;
    public bool connectWhenAwake;
    public int links = 7;
    // Start is called before the first frame update
    void Start()
    {
        if (connectWhenAwake)
        {
            GenerateRope();
        }
    }

    private void GenerateRope()
    {
        Rigidbody2D previousRb = hook.GetComponent<Rigidbody2D>();
        for (int i = 0; i < links; i++)
        {
            GameObject link = Instantiate(linkPrefab, transform);
            HingeJoint2D joint = link.GetComponent<HingeJoint2D>();
            joint.connectedBody = previousRb;

            if (i < links - 1)
            {
                previousRb = link.GetComponent<Rigidbody2D>();
            }
            else
            {
                weight.ConnectRopeEnd(link.GetComponent<Rigidbody2D>());
            }
            
        }
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("AutomaticLink"))
        {
            GenerateRope();
            RopeTimer.instance.RefreshTimer();
            Destroy(other.gameObject);
            
        }
    }
}
