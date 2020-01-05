using System;
using System.Collections;
using System.Collections.Generic;
using CodeMonkey.Utils;
using UnityEngine;

public class DestinationPointer : MonoBehaviour
{
    // reference
    public Camera uiCamera;
    public Transform destination;
    private RectTransform pointerRectTransform;

    private Vector3 targetPosition;
    private void Awake()
    {
        targetPosition = destination.position;
        pointerRectTransform = transform.Find("Pointer").GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotateTowardsTarget();

        float borderSize = 100f;
        Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(targetPosition);
        bool isOffScreen = targetPositionScreenPoint.x <= borderSize || targetPositionScreenPoint.y <= borderSize || targetPositionScreenPoint.x >= Screen.width-borderSize || targetPositionScreenPoint.y >= Screen.height-borderSize;
        if (isOffScreen)
        {
            Vector3 cappedTargetScreenPosition = targetPositionScreenPoint;
            if (cappedTargetScreenPosition.x <= borderSize) cappedTargetScreenPosition.x = borderSize;
            if (cappedTargetScreenPosition.y <= borderSize) cappedTargetScreenPosition.y = borderSize;
            if (cappedTargetScreenPosition.x >= Screen.width-borderSize) cappedTargetScreenPosition.x = Screen.width -borderSize;
            if (cappedTargetScreenPosition.y >= Screen.height-borderSize) cappedTargetScreenPosition.y = Screen.height-borderSize;

            Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(cappedTargetScreenPosition); 
            pointerRectTransform.transform.position = pointerWorldPosition;
            pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x,
                pointerRectTransform.localPosition.y, 0f);
        }
        else
        {
            Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(targetPositionScreenPoint); 
            pointerRectTransform.transform.position = pointerWorldPosition;
            pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x,
                pointerRectTransform.localPosition.y, 0f);
            pointerRectTransform.localEulerAngles = Vector3.zero;
        }
    }

    private void RotateTowardsTarget()
    {
        Vector3 toPosition = targetPosition;
        Vector3 fromPosition = Camera.main.transform.position;
        fromPosition.z = 0;
        Vector3 dir = (toPosition - fromPosition).normalized;
        float angle = UtilsClass.GetAngleFromVectorFloat(dir);
        pointerRectTransform.localEulerAngles = new Vector3(0, 0, angle);
    }
}
