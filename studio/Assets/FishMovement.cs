using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    public float speed;
    public float minX;
    public float mstart;

    public float amplitute = 0.5f;

    public float frequency = 1.0f;

    private Vector3 StartPos;
    // Start is called before the first frame update
    void Start()
    {
        StartPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Mathf.Sin(Time.time * frequency) * amplitute;
        float y = Mathf.Sin(Time.time * frequency * 2) * amplitute / 2;
        transform.Translate((StartPos + new Vector3(x, y, 0) )* speed * Time.deltaTime);
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
