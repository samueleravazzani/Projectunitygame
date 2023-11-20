using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collect : MonoBehaviour
{
    private ScoreUI scoreUI;
    
    private bool gameFinished = false;
    public ParticleSystem particleSystem; 
    public ParticleSystem particleSystemFriends;
    public GameObject gameOverCanvas; // Reference to the Canvas object that displays the game over message.
  

    private void Start()
    {
        // Find and assign a reference to the ScoreUI script in the scene.
        scoreUI = FindObjectOfType<ScoreUI>();
        
        // Deactivate the Canvas object at the start of the game.
        gameOverCanvas.SetActive(false);
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

            // Position and play the particle system at the location of the collected trash.
            particleSystem.transform.position = other.transform.position;
            particleSystem.Play();

            // Destroy the collected trash GameObject.
            Destroy(other.gameObject);
            scoreUI.UpdateCount(-1);
        }
        else if (other.CompareTag("friends"))
        {
            Debug.Log("Friend collected!");

            // Position and play the particle system at the location of the collected friend.
            particleSystemFriends.transform.position = other.transform.position;
            particleSystemFriends.Play();

            // Destroy the collected friend GameObject.
            Destroy(other.gameObject);
            scoreUI.UpdateCount(1);
            
        }

        if (scoreUI.collectedCount == 0)
        {
            Debug.Log("Counter reached 0! The game is finished.");
            gameFinished = true;
            
            // Activa el Canvas de Game Over y muestra el mensaje.
            gameOverCanvas.SetActive(true);
            Invoke("ChangeToMainMap", 5f);
        }

        
    }
    
    private void ChangeToMainMap()
    {
      
        // Cambia a la escena 'MainMap'
        SceneManager.LoadScene("MainMap");
    }
}
