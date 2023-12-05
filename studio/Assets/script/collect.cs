using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collect : MonoBehaviour
{
    private ScoreUI scoreUI;
    private ScoreUI scoreUIfriend;//
    private bool gameFinished = false;
    public ParticleSystem particleSystem; 
    public ParticleSystem particleSystemFriends;
    public GameObject gameOverCanvas; // Reference to the Canvas object that displays the game over message.
    public GameObject gameOverCanvas2;
    private int trashCount = 0;
    private int friendsCount = 0;
    //Nuevo: Variable para el límite de basura
    public int trashLimit = 6;


    private void Start()
    {
        // Find and assign a reference to the ScoreUI script in the scene.
        scoreUI = FindObjectOfType<ScoreUI>();
        scoreUIfriend = FindObjectOfType<ScoreUI>();//
        // Deactivate the Canvas object at the start of the game.
        gameOverCanvas.SetActive(false);
        gameOverCanvas2.SetActive(false);
        
        // Asigna trashLimit a collectedCount en el inicio.
        scoreUI.UpdateCount(trashLimit);
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
            trashCount++; // Increment the "trash" counter by 1.
            // Update the "trash" counter in the ScoreUI script.
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
            friendsCount++; // Increment the "friends" counter by 1.
            Debug.Log("Friends Count: " + friendsCount);
            //scoreUI.UpdateCount(1);

            // Call the function to update the "friends" counter in the FriendsScoreUI script.
            FindObjectOfType<FriendsScoreUI>().UpdateFriendsCount(1);
        }

        // Verifica si collectedCount es igual a trashLimit.
        if (scoreUI.GetCollectedCount() <= 0)
        {
            Debug.Log("You collected all the required rubbish! The game is finished.");
            gameFinished = true;
            gameOverCanvas.SetActive(true);
        }
       
        
        else if (friendsCount == 3)
        {
            Debug.Log("You either collected 3 friends or all the required rubbish! The game is finished.");
            gameFinished = true;
            //Time.timeScale = 0f; // Pause the game.
            gameOverCanvas2.SetActive(true); // Activate the game over Canvas.
            // Invoca la función para cambiar a la escena 'MainMap' después de 5 segundos.
            //Invoke("ChangeToMainMap", 5f);
        }
    }
    /*
    private void ChangeToMainMap()
    {
       // Restaura el tiempo del juego a su velocidad normal antes de cambiar a la escena 'MainMap'.
        //Time.timeScale = 1f;

        // Cambia a la escena 'MainMap'
        SceneManager.LoadScene("MainMap");
    }*/
}
