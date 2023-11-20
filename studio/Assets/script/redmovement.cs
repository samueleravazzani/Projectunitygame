using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redmovement : MonoBehaviour
{
    public float speed;

    private float minX = -7f;
    private float maxX = 3f;
    private float minY = -3f;
    private float maxY = 2.5f;

    private void Update()
    {
        // Mover con el mouse
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButton(0))
        {
            MoveTowardsMouse(mousePos);
        }

        // Limitar la posición del objeto
        LimitPosition();
    }

    public void MoveTowardsMouse(Vector3 targetPosition)
    {
        // Mueve el objeto hacia la posición del mouse con una velocidad específica
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    void LimitPosition()
    {
        // Limita la posición del objeto en los ejes x e y
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);

        // Asigna la posición limitada al objeto
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}