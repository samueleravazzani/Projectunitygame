using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
    private ScoreUI scoreUI;
    private bool gameFinished = false; // Add a flag to track if the game has finished.

    private void Start()
    {
        scoreUI = FindObjectOfType<ScoreUI>(); // Encuentra el objeto con el script ScoreUI.
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
		if (gameFinished)
        {
            return; // If the game has finished, do not process further triggers.
        }		

        if (other.CompareTag("trash"))
        {
            
            Debug.Log("Pice of rubbish collected!");
            Destroy(other.gameObject);
            scoreUI.UpdateCount(1); // Llama a la función UpdateCount con el valor 1 para aumentar el contador.
            Debug.Log("Collected Count: " + scoreUI.collectedCount);
                       
        }

		else if (other.CompareTag("friends"))
        {
            Debug.Log("Friend collected!");
            Destroy(other.gameObject);
            scoreUI.UpdateCount(-1); // Resta 1 al contador cuando se recoja un amigo.
            Debug.Log("Collected Count: " + scoreUI.collectedCount);
        }

		if (scoreUI.collectedCount == 10)
        {
            // Execute your desired action when the counter reaches 10.
            Debug.Log("Counter reached 10! The game is finished.");
            gameFinished = true; // Set the gameFinished flag to true to prevent further processing.
			Time.timeScale = 0; // Pause the game by setting Time.timeScale to 0.
        }
    }
}
