﻿using System;
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

    public UnityAction<Route, List<Vector3>> OnParkLinkedToLine;
    public UnityAction<Route> onBeginDraw;
    public UnityAction onDraw;
    public UnityAction onEndDraw;
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

                onBeginDraw?.Invoke(currentRoute);
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
                if (currentLine.length >= currentRoute.maxLineLength)
                {
                    currentLine.Clear();
                    OnMouseUpHandle();
                    return;
                }

                currentLine.AddPoint(newPoint);
                onDraw?.Invoke();

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
                    OnParkLinkedToLine?.Invoke(currentRoute, currentLine.points);
                    currentRoute.ChangeActive();
                }
            }
            else
            {
                currentLine.Clear();
            }

        }
        ResetDrawer();
        onEndDraw?.Invoke();
    }
    
    private void ResetDrawer()
    {
        currentLine = null;
        currentRoute = null;
    }
}
