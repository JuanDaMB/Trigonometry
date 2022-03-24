using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class LookAtTan : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int n;
    [SerializeField] private Vector2 space;
    private List<Transform> _transforms;
    private List<Vector2> _velocity, _acceleration;
    [SerializeField] private float speed;
    private Vector3 _targetPos;
    [SerializeField] private bool accelerate;
    [SerializeField] private bool move;
    private void Start()
    {
        _transforms = new List<Transform>();
        _velocity = new List<Vector2>();
        _acceleration = new List<Vector2>();
        for (int i = 0; i < n; i++)
        {
            Vector2 v = new Vector2(Random.Range(-space.x, space.x), Random.Range(-space.y, space.y));
            GameObject g = Instantiate(prefab, v, quaternion.identity, transform);
            g.GetComponent<SpriteRenderer>().color = Random.ColorHSV();
            _transforms.Add(g.transform);
            _velocity.Add(new Vector2());
            _acceleration.Add(new Vector2());
        }
    }

    private void Update()
    {
        Vector3 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (accelerate)
        {
            for (int i = 0; i < _transforms.Count; i++)
            {
                _acceleration[i] = Accelerate(mp, _transforms[i].position);
                _velocity[i] += _acceleration[i] * Time.deltaTime;
                float radians = LookAt(_transforms[i].position+(Vector3)_velocity[i], _transforms[i].position);
                _transforms[i].eulerAngles = RotateZ(radians);   
                _transforms[i].position += (Vector3)_velocity[i]*Time.deltaTime;
            }
        }
        else
        {
            foreach (Transform t in _transforms)
            {
                if (move)
                {
                    t.position += (Vector3)Move(mp, t.position);   
                }
                float radians = LookAt(mp, t.position);
                t.eulerAngles = RotateZ(radians);
            }
        }
    }

    private float LookAt(Vector2 target, Vector2 transformPosition)
    {
        Vector2 dir = target - transformPosition;
       return Mathf.Atan2(dir.y, dir.x);
    }

    private Vector2 Move(Vector2 target, Vector2 transformPosition)
    {
        Vector2 dir = (target - transformPosition).normalized;
       return dir * speed*Time.deltaTime;
    }

    private Vector2 Accelerate(Vector2 target, Vector2 transformPosition)
    {
        return (target - transformPosition);
    }

    private Vector3 RotateZ(float radians)
    {
        float angle = radians * Mathf.Rad2Deg;
       return new Vector3(0, 0, angle);
    }
}