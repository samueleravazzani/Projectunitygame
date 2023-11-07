using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redmovement : MonoBehaviour
{
    public float increment;

    public Vector2 targetPos;

    public float speed;

    private void Awake()
    {
        targetPos = transform.position;
    }

    private void Update()
    {
        // Limita la posición en el eje Y
        targetPos.y = Mathf.Clamp(targetPos.y, -3, 2);
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveDown();
            
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveUp();
            
        }
        
        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }

    public void MoveUp()
    {
        targetPos += new Vector2(0, increment);
    }

    public void MoveDown()
    {
        targetPos -= new Vector2(0, increment);
    }
}