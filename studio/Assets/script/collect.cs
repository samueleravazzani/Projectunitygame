using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
    private ScoreUI scoreUI;
    private bool gameFinished = false;
    public ParticleSystem particleSystem; // Arrastra tu sistema de partículas en el Inspector.
    public ParticleSystem particleSystemFriends;

    private void Start()
    {
        scoreUI = FindObjectOfType<ScoreUI>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (gameFinished)
        {
            return;
        }      

        if (other.CompareTag("trash"))
        {
            Debug.Log("Piece of rubbish collected!");

            // Mueve el sistema de partículas a la posición del objeto destruido.
            particleSystem.transform.position = other.transform.position;
            // Activa el sistema de partículas.
            particleSystem.Play();
            
            Destroy(other.gameObject);
            scoreUI.UpdateCount(1);
            Debug.Log("Collected Count: " + scoreUI.collectedCount);
        }
        else if (other.CompareTag("friends"))
        {
            Debug.Log("Friend collected!");

            // Mueve el sistema de partículas a la posición del objeto destruido.
            particleSystemFriends.transform.position = other.transform.position;

            // Activa el sistema de partículas.
            particleSystemFriends.Play();

            Destroy(other.gameObject);
            scoreUI.UpdateCount(-1);
            Debug.Log("Collected Count: " + scoreUI.collectedCount);
        }

        if (scoreUI.collectedCount == 10)
        {
            Debug.Log("Counter reached 10! The game is finished.");
            gameFinished = true;
            Time.timeScale = 0;
        }
    }
}