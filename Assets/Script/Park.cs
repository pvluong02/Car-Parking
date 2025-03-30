using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Park : MonoBehaviour
{
    [SerializeField] SpriteRenderer SrPark;
    [SerializeField] ParticleSystem fx;
    private ParticleSystem.MainModule fxMainModule;
    public Route route;

    private void Start()
    {
        fxMainModule = fx.main;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Car car))
        {
            if (car.route == route)
            {
                Game.Instance.onCarEnterPark?.Invoke(route);
                StartFX();
            }
        }
    }

    private void StartFX()
    {
        fxMainModule.startColor = route.carColor;
        fx.Play();
    }

    public void setColor(Color color)
    {
        SrPark.color = color;
    }
}
