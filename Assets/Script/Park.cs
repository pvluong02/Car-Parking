using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Park : MonoBehaviour
{
    [SerializeField] SpriteRenderer SrPark;
    public Route route;
    public void setColor(Color color)
    {
        SrPark.color = color;
    }
}
