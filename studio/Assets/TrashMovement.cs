using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashMovement : MonoBehaviour
{
    public float speed;
    private float minX = -12f;
    private float maxX = 7f;
    private float minY = -7f;

    private bool collisionFondo;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!collisionFondo)
        {
            transform.Translate(Vector2.left * (speed * Time.deltaTime));
        }
        else if (transform.position.x < minX || transform.position.y < minY)
        {
            Destroy(gameObject);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Retino")
        {
            Collect.GetInstance().checkforwin();
            Destroy(gameObject);
        }
        if (collider.gameObject.tag == "Fondo")
        {
            collisionFondo = true;
            collider.isTrigger = false;
        }
    }
}
