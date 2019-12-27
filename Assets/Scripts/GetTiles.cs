using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTiles : MonoBehaviour
{
    public Transform[] children;
    public Transform[] tiles;
    // Start is called before the first frame update
    void Start()
    {
        //childrenObj = GetComponentsInChildren<GameObject>();
        children = GetComponentsInChildren<Transform>();
        Array.Copy(children, 1, tiles, 0, tiles.Length);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
