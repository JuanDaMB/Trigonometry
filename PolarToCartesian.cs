using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolarToCartesian : MonoBehaviour
{
    [SerializeField] private float r, t;

    [Header("Angular Speed")] 
    [SerializeField] private float angularSpeed;
    [SerializeField] private float angularAcceleration;
    
    [Header("Radial Speed")] 
    [SerializeField] private float radialSpeed;
    [SerializeField] private float radialAcceleration;

    [Header("Radial Limit")] 
    [SerializeField] private float rLimit;
    
    private Vector2 _pos = new Vector2();
    [SerializeField] private Transform obj;
    void Update()
    {
        IncrementValues();
        CheckLimit();
        FromPolarToCartesian();
        obj.position = _pos;
        // Debug.DrawLine(Vector3.zero, _pos, Color.yellow);
    }

    private void IncrementValues()
    {
        r += radialSpeed * Time.deltaTime;
        radialSpeed += radialAcceleration * Time.deltaTime;
        t += angularSpeed * Time.deltaTime;
        angularSpeed += angularAcceleration * Time.deltaTime;
    }

    private void CheckLimit()
    {
        if (Mathf.Abs(r)>=rLimit)
        {
            radialSpeed = -radialSpeed;
            radialAcceleration = -radialAcceleration;
            r = rLimit*Mathf.Sign(r);
        }
    }

    private void FromPolarToCartesian()
    {
        float x = r * Mathf.Cos(t);
        float y = r * Mathf.Sin(t);
        _pos = new Vector2(x, y);
    }
}