using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class redmovement : MonoBehaviour
{
    public float speed;

    private float minX = -9f;
    private float maxX = 7f;
    private float minY = -4f;
    private float maxY = 4f;

    private float xbuttonarea=-6f;
    private float ybuttonarea=1f;
    
    public GameObject customPointer;
    public GameObject pause;

    private void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        // Get the mouse position in screen coordinates
        Vector3 mousePosition = Input.mousePosition;

        // Convert the screen coordinates to world coordinates
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10f));

        // Update the position of your custom GameObject to the calculated world position
        customPointer.transform.position = worldPosition;
        // Limitar la posición del objeto
        LimitPosition();
        checkposition(worldPosition);
    }
    
    void LimitPosition()
    {
        // Limita la posición del objeto en los ejes x e y
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);

        // Asigna la posición limitada al objeto
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

    void checkposition( Vector3 worldPosition)
    {
        if (worldPosition.x < xbuttonarea && worldPosition.y > ybuttonarea)
        {
            Cursor.visible = true;
        }
        else if (pause.gameObject.activeSelf)
        {
            Cursor.visible = true;
        }
        else if (Collect.GetInstance().win || Collect.GetInstance().lose)
        {
            Cursor.visible = true;
        }
        else
        {
            Cursor.visible=false;
        }

        
    }

    public void visiblecursor()
    {
        Cursor.visible = true;
    }
}