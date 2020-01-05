using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeCutter : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Link"))
                {
                    var rope = hit.transform.parent;
                    Destroy(hit.collider.gameObject);
                    foreach (Transform child in rope)
                    {
                        if (child.gameObject.CompareTag("Link"))
                        {
                            child.GetComponent<Link>().StartFading();
                        }
                    }
                }
            }
        }
        
        if (Input.touchCount > 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Link"))
                {
                    var rope = hit.transform.parent;
                    Destroy(hit.collider.gameObject);
                    foreach (Transform child in rope)
                    {
                        if (child.gameObject.CompareTag("Link"))
                        {
                            child.GetComponent<Link>().StartFading();
                        }
                    }
                }
            }
        }

    }
}
