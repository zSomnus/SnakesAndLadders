using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Link : MonoBehaviour
{
    // reference
    private SpriteRenderer spriteRenderer;
    
    // tuneable value
    public float fadingSpeed = 0.2f;

    private bool isFading;

    private float currentAlpha;
    private Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFading)
        {
            currentAlpha -= fadingSpeed*Time.deltaTime;
            var color = spriteRenderer.color;
            color.a = currentAlpha;
            spriteRenderer.color = color;
            if (currentAlpha <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void StartFading()
    {
        isFading = true;
        rb.mass /= 4;
        rb.drag = 0;
        currentAlpha = spriteRenderer.color.a;
    }
}
