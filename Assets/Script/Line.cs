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
    [HideInInspector] public float length = 0f;
    private float pointFixedYAxis;
    private Vector3 prevPoint;

    private void Start()
    {
        pointFixedYAxis = lineRenderer.GetPosition(0).y; // cố định hình vẽ theo trục y tại vị trí 0
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
        length = 0;
    }

    public void AddPoint(Vector3 newPoint)
    {
        newPoint.y = pointFixedYAxis;

        if (pointCount >= 1 && Vector3.Distance(newPoint, GetLastPoint()) < minPointDistance) // nếu khoảng cách giữa 2 điểm chưa đủ thì sẽ vẫn vẽ
        {
            return;
        }

        if (pointCount == 0)
        {
            prevPoint = newPoint;
        }

        points.Add(newPoint);

        pointCount++;

        length += Vector3.Distance(prevPoint, newPoint);
        prevPoint = newPoint;
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
