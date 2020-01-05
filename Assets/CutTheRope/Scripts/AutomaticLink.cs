using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticLink : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    // Tuneable values
    public float linkSpeed = 5f;

    public float timeInvalid = 3f;
    // Start is called before the first frame update
    void Start()
    {
            spriteRenderer = GetComponent<SpriteRenderer>();
            boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        timeInvalid-=Time.deltaTime;
        if (timeInvalid <= 0)
        {
            Destroy(gameObject);
        }
        Vector2 size = spriteRenderer.size;
        size.y+=linkSpeed*Time.deltaTime;
        spriteRenderer.size = size;

        Vector2 offset = boxCollider.offset;
        offset.y = size.y/2;
        Vector2 boxColliderSize = boxCollider.size;
        boxColliderSize.y = size.y;
        boxCollider.size = boxColliderSize;
        boxCollider.offset = offset;
    }
}
