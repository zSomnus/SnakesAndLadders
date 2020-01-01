using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEngine;

public class LineCreator : MonoBehaviour
{

    public GameObject linePrefab;

    private Line activeLine;
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount>0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            LineRiderGameManager.Instance.UiController.HideTipText();
            GameObject lineGO = Instantiate(linePrefab);
            activeLine = lineGO.GetComponent<Line>();
            // print("touching");
        }
        if(Input.touchCount==0)
        {
            // print("Stop touching");
            activeLine = null;
            
        }
        //
        // if (Input.GetMouseButtonDown(0))
        // {
        //     GameObject lineGO = Instantiate(linePrefab);
        //     activeLine = lineGO.GetComponent<Line>();
        // }
        //
        // if (Input.GetMouseButtonUp(0))
        // {
        //     activeLine = null;
        // }
        
        if (activeLine != null)
        {
            
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            touchPos.z = 0;
            activeLine.UpdateLine(touchPos);
        }

        // if (activeLine != null)
        // {
        //     
        //     Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //     mousePos.z = 0;
        //     activeLine.UpdateLine(mousePos);
        // }
        
    }
}
