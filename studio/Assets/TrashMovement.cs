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
   
    
    
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * (speed * Time.deltaTime));
        if (transform.position.x <= minX || transform.position.y <= minY)
        {
            Destroy(gameObject);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Retino" && !collisionFondo)
        {
            Collect.GetInstance().checkforwin();
            Destroy(gameObject);
        }
        if ( collider.gameObject.tag == "Fondo")
        {
            Debug.Log("collisione trash-fondo");
            collisionFondo = true; 
            if (Collect.GetInstance().level == 3)
            {
                Debug.Log("collisione livello 3");
                Collect.GetInstance().checkforlose();
            }
        }
    }
}
