using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public MeshRenderer carMesh;
    public GameObject carBottom;
    public Route route;
    public void setColor(Color color)
    {
        carMesh.sharedMaterials[0].color = color;
    }
}
