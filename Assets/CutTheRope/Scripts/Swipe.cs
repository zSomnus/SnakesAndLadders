using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    private bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
    private bool isDraging = false;
    
    // reference
    public GameObject automaticLink;
    private Camera cam;

    public Vector2 startTouch { get; set; }

    public Vector2 swipeDelta { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        tap = swipeLeft = swipeRight = swipeUp = swipeDown = false;
        
        # region Standalone Inputs

        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
                
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit)
            {
                if (hit.collider.gameObject == gameObject)
                {
                    tap = true;
                    isDraging = true;
                    startTouch = Input.mousePosition;
                }
            }
            
            
        }
        else if (Input.GetMouseButtonUp(1))
        {
            isDraging = false;
            Reset();
        }
        # endregion
        
        # region Mobile Inputs

        if (Input.touchCount > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                Vector3 touchPosInWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                
                Vector2 touchPosIn2DWorld = new Vector2(touchPosInWorld.x, touchPosInWorld.y);
                
                RaycastHit2D hit = Physics2D.Raycast(touchPosIn2DWorld, Vector2.zero);
                if (hit)
                {
                    if (hit.collider.gameObject == gameObject)
                    {
                        tap = true;
                        isDraging = true;
                        startTouch = Input.mousePosition;
                    }
                }
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                isDraging = false;
                Reset();
            }
        }
        # endregion
        
        // Calculate the distance
        swipeDelta = Vector2.zero;
        if (isDraging)
        {
            if (Input.touchCount > 0)
            {
                swipeDelta = Input.GetTouch(0).position - startTouch;
            }
            else if (Input.GetMouseButton(1))
            {
                swipeDelta = (Vector2) Input.mousePosition - startTouch;
            }
        }

        // Did we cross the deadzone?
        if (swipeDelta.magnitude > 125)
        {
            // which direction?
            float x = swipeDelta.x;
            float y = swipeDelta.y;
        }
    }

    public void Reset()
    {
        if (swipeDelta.magnitude > 125)
        {
            var link = Instantiate(automaticLink, transform.position, Quaternion.identity);
            link.transform.rotation = Quaternion.FromToRotation(Vector3.up, swipeDelta);
        }
        startTouch = swipeDelta = Vector2.zero;
    }
}
