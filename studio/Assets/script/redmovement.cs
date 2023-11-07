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
        // transform.position.y = Mathf.Clamp(transform.position.y, -3, 2);
        if (Input.GetKey(KeyCode.DownArrow) && transform.position.y > -3)
        {
            MoveDown();
            
        }
        else if (Input.GetKey(KeyCode.UpArrow) && transform.position.y < 2)
        {
            MoveUp();
            
        }
        
       // transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }

    public void MoveUp()
    {
        transform.position += new Vector3(0, increment,0);
    }

    public void MoveDown()
    {
        transform.position -= new Vector3(0, increment,0);
    }
}