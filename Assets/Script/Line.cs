using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Route route;
    [SerializeField] protected float minPointDistance;

    [HideInInspector] public List<Vector3> points = new();
    [HideInInspector] public int pointCount = 0;

    private float pointFixedYAxis;

    private void Start()
    {
        pointFixedYAxis = lineRenderer.GetPosition(0).y; // cố định hình vẽ theo trục y
        Clear();
    }
    public void Init()
    {
        gameObject.SetActive(true);
    }
    public void Clear()
    {
        gameObject.SetActive(false);
        lineRenderer.positionCount = 0;
        pointCount = 0;
        points.Clear();
    }

    public void AddPoint(Vector3 newPoint)
    {
        newPoint.y = pointFixedYAxis;

        if (pointCount >= 1 && Vector3.Distance(newPoint, GetLastPoint()) < minPointDistance) // nếu khoảng cách giữa 2 điểm chưa đủ thì sẽ vẫn vẽ
        {
            return;
        }
        else points.Add(newPoint);

        pointCount++;

        // cập nhật linerender
        lineRenderer.positionCount = pointCount;
        lineRenderer.SetPosition(pointCount-1, newPoint);
    }

    private Vector3 GetLastPoint()
    {
        return lineRenderer.GetPosition(pointCount - 1);
    }
    public void SetColor(Color color)
    {
        lineRenderer.sharedMaterials[0].color = color;
    }


}
