using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEngine;

public class Basketry : MonoBehaviour
{
    public bool _hasBeenHit;
    private BasketryChecker basketryChecker;

    public Material material;

    void Start()
    {
        basketryChecker = PhysicsSportManager.Instance.basketryChecker;
        material = GetComponent<Renderer>().material;
    }
    void OnCollisionEnter(Collision other)
    {
        if (!_hasBeenHit)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                print("hit player");
                ToggleBasketryHitState();
                basketryChecker.Check();
            }
        }
        
    }

    public void ToggleBasketryHitState()
    {
        _hasBeenHit = true;
        material.SetColor("_Color", Color.red);
    }
}
