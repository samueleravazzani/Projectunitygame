using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    public float speed;
    public float minX;
    public float mstart;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * (speed * Time.deltaTime));
        if (transform.position.x <= minX)
        {
            transform.position = new Vector2(mstart, transform.position.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "red")
        {
            Collect.GetInstance().checkforlose();
            Destroy(gameObject);
        }
    }
}
