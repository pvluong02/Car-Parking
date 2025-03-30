using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using System;
using Unity.Mathematics;


public class Car : MonoBehaviour
{
    public Transform carBottom;
    public Transform bodyTransform;
    [SerializeField] ParticleSystem fxSmoke;
    [SerializeField] MeshRenderer carMesh;
    [SerializeField] Rigidbody rb;
    [SerializeField] float danceValue;
    [SerializeField] float durationMultiplier;
    public Route route;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        bodyTransform.DOLocalMoveY(danceValue, .1f)
                     .SetLoops(-1, LoopType.Yoyo)
                     .SetEase(Ease.Linear);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out Car car)) {
            car.StopDancingAnim();
            rb.DOKill(false);

            Vector3 hitPoint = collision.contacts[0].point;
            AddExplosionForce(hitPoint);
            fxSmoke.Play();
            Game.Instance.onCarCollision?.Invoke();
        }
    }

    private void AddExplosionForce(Vector3 Point)
    {
        rb.AddExplosionForce(400f, Point, 3f);
        rb.AddForceAtPosition(Vector3.up*2f, Point, ForceMode.Impulse);
        rb.AddTorque(new Vector3(GetRandomAngle(), GetRandomAngle(), GetRandomAngle()));
    }

    private float GetRandomAngle()
    {
        float angle = 10f;
        float rand = UnityEngine.Random.value;
        return rand > .5f ? angle : -angle;
    }

    public void Move(Vector3[] parth)
    {
        rb.DOLocalPath(parth, durationMultiplier * parth.Length * 3f)
            .SetLookAt(.01f, false)
            .SetEase(Ease.Linear);
    }
    public void StopDancingAnim()
    {
        bodyTransform.DOKill(true) ;
    }

    public void setColor(Color color)
    {
        carMesh.sharedMaterials[0].color = color;
    }
}
