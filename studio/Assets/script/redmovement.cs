using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redmovement : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        // Mover con las flechas del teclado
        if (Input.GetKey(KeyCode.DownArrow) && transform.position.y > -3)
        {
            MoveDown();
        }
        else if (Input.GetKey(KeyCode.UpArrow) && transform.position.y < 2)
        {
            MoveUp();
        }

        // Mover con el mouse
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButton(0) && Mathf.Abs(mousePos.y - transform.position.y) > 0.01f)
        {
            MoveTowardsMouse(mousePos);
        }
    }

    public void MoveUp()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
    }

    public void MoveDown()
    {
        transform.position -= Vector3.up * speed * Time.deltaTime;
    }

    public void MoveTowardsMouse(Vector3 targetPosition)
    {
        // Mueve el objeto hacia la posición del mouse con una velocidad específica
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
}