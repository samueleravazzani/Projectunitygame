using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class BackgroundMovement : MonoBehaviour
{
    public float speed;

    private float minX=-14.8f;
    private float mstart=20.58f;
    
    private void Update()
    {
        transform.Translate(Vector2.left * (speed * Time.deltaTime));

        if (transform.position.x <= minX)
        {
            transform.position = new Vector2(mstart, transform.position.y);
        }
    }
}