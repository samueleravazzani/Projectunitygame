using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyRubbish : MonoBehaviour

{
    public string tagToDestroy = "trash";
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("red"))
        {
            Debug.Log("One pice of rubbish collected!");
            Destroy(gameObject);
        }
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
     //   Debug.Log("Colisión detected with: " + collision.gameObject.name);
     //   if (collision.gameObject.CompareTag(tagToDestroy))
    //    {
     //       Debug.Log("Destroing: " + collision.gameObject.name);
     //       Destroy(collision.gameObject);
       // }
    //}
}