using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    public float speed;
    public float minX;
    public float mstart;

    private Vector3 StartPos;
    public float oscillationRange = 0.7f; // Range of oscillation
    // Start is called before the first frame update
    void Start()
    {
        StartPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float oscillation = Mathf.Sin(Time.time) * oscillationRange;
        // Move left with a constant speed
        transform.Translate(Vector3.left* (speed * Time.deltaTime));
        transform.position=new Vector3(transform.position.x, StartPos.y + oscillation, 0);

        // Oscillate up and down around the initial position
        if (transform.position.x <= minX)
        {
            transform.position = new Vector2(mstart, transform.position.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("collisione");
        
        if (collider.CompareTag("Retino"))
        {
            Debug.Log("collisioneRED");
            Collect.GetInstance().checkforlose();
            Destroy(gameObject);
        }

        if (collider.CompareTag("trash"))
        {
            Debug.Log("collisione trash");
        }
    }
}
