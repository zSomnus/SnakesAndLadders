using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    public bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
    private bool isDragging = false;
    
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
                    isDragging = true;
                    startTouch = Input.mousePosition;
                }
            }
            
            
        }
        else if (Input.GetMouseButtonUp(1))
        {
            isDragging = false;
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
                        isDragging = true;
                        startTouch = Input.mousePosition;
                    }
                }
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                isDragging = false;
                Reset();
            }
        }
        # endregion
        
        // Calculate the distance
        swipeDelta = Vector2.zero;
        if (isDragging)
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
            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                if (x < 0)
                {
                    swipeLeft = true;
                }
                else
                {
                    swipeRight = true;
                }
            }
            else
            {
                if (y < 0)
                {
                    swipeDown = true;
                }
                else
                {
                    swipeUp = true;
                }
            }
        }
    }

    public void Reset()
    {
        if (swipeDelta.magnitude > 125)
        {
            if (automaticLink)
            {
                var link = Instantiate(automaticLink, transform.position, Quaternion.identity);
                link.transform.rotation = Quaternion.FromToRotation(Vector3.up, swipeDelta);
            }
            
        }
        startTouch = swipeDelta = Vector2.zero;
        isDragging = false;
    }
}
