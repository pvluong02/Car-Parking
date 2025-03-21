using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Route : MonoBehaviour
{
    [HideInInspector] public bool isActive = true;
    public Car car;
    public Line line;
    public Park park;

    public Color carColor;
    public Color lineColor;

    public void ChangeActive()
    {
        isActive = false;
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying && car != null && line != null && park != null)
        {
            line.lineRenderer.SetPosition(0, car.carBottom.transform.position);
            line.lineRenderer.SetPosition(1, park.transform.position);

            car.setColor(carColor);
            park.setColor(carColor);
            line.SetColor(lineColor);
        }
    }
#endif
}
