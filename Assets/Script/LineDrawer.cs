using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LineDrawer : MonoBehaviour
{
    //khu vực vẽ
    public UserInput userInput;

    private Line currentLine;
    private Route currentRoute;
    public RaycastDetector raycastDetector = new();

    public int layerMask;
    private void Start()
    {
        userInput.OnMouseDown += OnMouseDownHandle;
        userInput.OnMouseUp += OnMouseUpHandle;
        userInput.OnMouseMove += OnMouseMoveHandle;
    }
    // start draw
    private void OnMouseDownHandle()
    {
        ContactInfor contactInfor = raycastDetector.RayCast(layerMask);
        if (contactInfor.contacted) 
        {
            bool isCar = contactInfor.collider.TryGetComponent(out Car _car);
            if (isCar && _car.route.isActive)
            {
                currentRoute = _car.route;
                currentLine = currentRoute.line;
                currentLine.Init();
            }
        }
        
    }
    // draw
    private void OnMouseMoveHandle()
    {
        if (currentRoute != null)
        {
            ContactInfor contactInfor = raycastDetector.RayCast(layerMask);

            if (contactInfor.contacted)
            {
                Vector3 newPoint = contactInfor.point;
                currentLine.AddPoint(newPoint);

                bool isPark = contactInfor.collider.TryGetComponent(out Park _park);

                if (isPark)
                {
                    Route parkRoute = _park.route;
                    if(parkRoute == currentRoute)
                    {
                        currentLine.AddPoint(contactInfor.transform.position);
                    }
                    else
                    {
                        currentLine.Clear();
                    }
                    OnMouseUpHandle();
                }
            }
        }
    }

    // end draw
    private void OnMouseUpHandle()
    {
        if (currentRoute != null)
        {
            ContactInfor contactInfor = raycastDetector.RayCast(layerMask);

            if (contactInfor.contacted)
            {
                bool isPark = contactInfor.collider.TryGetComponent(out Park _park); 
                if (currentLine.pointCount < 2 || !isPark)
                {
                    currentLine.Clear();
                }
                else
                {
                    currentRoute.ChangeActive();
                }
            }
            else
            {
                currentLine.Clear();
            }

        }
        ResetDrawer();
    }
    
    private void ResetDrawer()
    {
        currentLine = null;
        currentRoute = null;
    }
}
