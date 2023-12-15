using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class BackgroundMovement : MonoBehaviour
{
    public float speed;

    private float minX=-17.96f;
    private float mstart=17.96f;
    
    private void Update()
    {
        transform.Translate(Vector2.left * (speed * Time.deltaTime));

        if (transform.position.x <= minX)
        {
            transform.position = new Vector2(mstart, transform.position.y);
        }
    }
}