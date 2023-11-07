using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
    private ScoreUI scoreUI;
    //private ScoreUI scoreUIfriend;
    private bool gameFinished = false;
    public ParticleSystem particleSystem; 
    public ParticleSystem particleSystemFriends;
    public GameObject gameOverCanvas; // Referencia al objeto Canvas que muestra el mensaje de juego terminado.

    private void Start()
    {
        scoreUI = FindObjectOfType<ScoreUI>();
        //scoreUIfriend = FindObjectOfType<ScoreUI>();
        gameOverCanvas.SetActive(false); // Desactiva el Canvas al inicio del juego
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
            particleSystem.Play(); // Activa el sistema de partículas.
            
            Destroy(other.gameObject);
            scoreUI.UpdateCount(1);
            Debug.Log("Collected Count: " + scoreUI.collectedCount);
        }
        else if (other.CompareTag("friends"))
        {
            Debug.Log("Friend collected!");

            // Mueve el sistema de partículas a la posición del objeto destruido.
            particleSystemFriends.transform.position = other.transform.position;
            particleSystemFriends.Play();

            Destroy(other.gameObject);
            scoreUI.UpdateCount(-1);
            Debug.Log("Collected Count: " + scoreUI.collectedCount);
        }

        if (scoreUI.collectedCount == 5)
        {
            Debug.Log("Counter reached 5! The game is finished.");
            gameFinished = true;
            Time.timeScale = 0;
            
            // Activa el Canvas de Game Over y muestra el mensaje.
            gameOverCanvas.SetActive(true);
        }
    }
}