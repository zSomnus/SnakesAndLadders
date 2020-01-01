using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    // Reference
    public GameObject bladeTrailPrefab;
    private Rigidbody2D rb;
    private Camera cam;
    private GameObject currentBladeTrail;
    private CircleCollider2D circleCollider;
    private Vector2 previousPosition;
    private List<GameObject> gameObjectHasHit;
    
    // Tuneable values
    public float minCuttingVelocity = 0.001f;

    // State
    private bool isCutting;
    
    // Start is called before the first frame update
    void Start()
    {
        gameObjectHasHit = new List<GameObject>();
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        circleCollider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCutting();
        }

        if (Input.GetMouseButtonUp(0))
        {
            StopCutting();
        }

        if (isCutting)
        {
            UpdateCut();
        }
    }

    private void StopCutting()
    {
        isCutting = false;
        currentBladeTrail.transform.SetParent(null);
        Destroy(currentBladeTrail, 2f);
        circleCollider.enabled = false;
    }

    private void StartCutting()
    {
        isCutting = true;
        rb.position = cam.ScreenToWorldPoint(Input.mousePosition);
        transform.position = rb.position;
        currentBladeTrail = Instantiate(bladeTrailPrefab, transform);
        circleCollider.enabled = false;
        
        previousPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        // currentBladeTrail.transform.position = cam.ScreenToWorldPoint(Input.mousePosition); 
    }

    private void UpdateCut()
    {
        Vector2 newPosition = cam.ScreenToWorldPoint(Input.mousePosition); 
        rb.position = newPosition;

        float velocity = (newPosition - previousPosition).magnitude * Time.deltaTime;
        previousPosition = newPosition;
        circleCollider.enabled = velocity > minCuttingVelocity;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        
        if (other.gameObject.CompareTag("Alphabet"))
        {
            if (!gameObjectHasHit.Contains(other.gameObject))
            {
                gameObjectHasHit.Add(other.gameObject);
                string alphabetFullName = other.gameObject.name;
                char alphabet = alphabetFullName[4];
                FruitNinjaManager.instance.CollectAlphabet(alphabet);
                print($"Hit "+alphabet);
            }
            
        }
    }
}
