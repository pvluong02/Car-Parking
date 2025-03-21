using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public struct ContactInfor
{
    public bool contacted;
    public Vector3 point;
    public Collider collider;
    public Transform transform;
}
public class RaycastDetector
{
    public ContactInfor RayCast(int layerMask)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool hit = Physics.Raycast(ray, out RaycastHit infor, float.PositiveInfinity, 1 << layerMask);

        return new ContactInfor
        {
            contacted = hit,
            point = infor.point,
            collider = infor.collider,
            transform = infor.transform
        };
    }
    
}
