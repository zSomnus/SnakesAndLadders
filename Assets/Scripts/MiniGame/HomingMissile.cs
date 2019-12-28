using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HomingMissile : MonoBehaviour
{
    public Transform target;

    public GameObject explosionParticle;
    // 
    public float speed = 5f;
    public float rotateSpeed = 200f;
    

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
            
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target != null)
        {
            Vector2 direction = (Vector2)target.position - rb.position;

            direction.Normalize();

            float rotateAmount = Vector3.Cross(direction, transform.up).z;

            rb.angularVelocity = -rotateAmount * rotateSpeed;
        
            rb.velocity = transform.up * speed;
        }
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag($"Player") )
        {
            var particle = Instantiate(explosionParticle, transform.position,transform.rotation);
            Destroy(particle,1.8f);
            other.gameObject.GetComponent<MiniGamePlayer>().TakeDamage(10);
            Destroy(gameObject);
        }
    }
}
