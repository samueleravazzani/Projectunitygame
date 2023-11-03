using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class BackgroundMovement : MonoBehaviour
{
    public float speed;

    public float minX;
    
    private void Update()
    {
        transform.Translate(Vector2.left * (speed * Time.deltaTime));

        if (transform.position.x <= -16)
        {
            transform.position = new Vector2(16.06f, transform.position.y);
        }
    }
}