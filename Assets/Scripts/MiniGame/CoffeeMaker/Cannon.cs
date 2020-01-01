using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    // enum
    public bool isRunning;
    public bool isSelected;
    public string objectSpawn;
    public Transform spawnPlace;
    
    
    // reference
    public ObjectPooler objectPooler;
    private CannonManager cannonManager;

    private void Start()
    {
        cannonManager = transform.parent.GetComponent<CannonManager>();
        if (cannonManager == null)
        {
            print("can't find cannon Manager");
        }
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    print($"touch {name}");
                    cannonManager.Select(this);
                }
            }
            
        }
    }

    public void ToggleOpenState()
    {
        isRunning = !isRunning;
    }

    public void ToggleSelectState(bool newSelectState)
    {
        isSelected = newSelectState;
    }

    void FixedUpdate()
    {
        if (isRunning)
        {
            ObjectPooler.instance.SpawnFromPool(objectSpawn, spawnPlace);
        }
    }
    

    private void OnMouseDown()
    {
        cannonManager.Select(this);
    }
}

