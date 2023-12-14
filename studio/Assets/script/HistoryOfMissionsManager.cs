using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HistoryOfMissionsManager : MonoBehaviour
{
    public GameObject minigames; //BADGE PANEL
    
    // secondo implementazione della Fede: lucchetti
    public Image breathingLockImage; 
    public Image leafLockImage; 
    public Image puzzleLockImage;

    public Image potionsLockImage;
    public Image sourceLockImage;
    
    public Image quizLockImage;
    public Image firepuzzleLockImage;
    public Image fishingLockImage;
    public Image gardeningLockImage;
    public Image tetrisLockImage;
    
    // badges dei minigiochi
    public Image breathingImage; 
    public Image leafImage; 
    public Image puzzleImage;

    public Image potionsImage;
    public Image sourceImage;
    
    public Image quizImage;
    public Image firepuzzleImage;
    public Image fishingImage;
    public Image gardeningImage;
    public Image tetrisImage;

    void Start()
    {
        minigames.gameObject.SetActive(false);
        // Initially, display the lock image and hide the badge image with counter
        breathingLockImage.gameObject.SetActive(true);
        breathingImage.gameObject.SetActive(false);
        leafLockImage.gameObject.SetActive(true);
        leafImage.gameObject.SetActive(false);
        puzzleLockImage.gameObject.SetActive(true);
        puzzleImage.gameObject.SetActive(false);
        potionsLockImage.gameObject.SetActive(true);
        potionsImage.gameObject.SetActive(false);
        sourceLockImage.gameObject.SetActive(true);
        sourceImage.gameObject.SetActive(false);
        quizLockImage.gameObject.SetActive(true);
        quizImage.gameObject.SetActive(false);
        firepuzzleLockImage.gameObject.SetActive(true);
        firepuzzleImage.gameObject.SetActive(false);
        fishingLockImage.gameObject.SetActive(true);
        fishingImage.gameObject.SetActive(false);
        gardeningLockImage.gameObject.SetActive(true);
        gardeningImage.gameObject.SetActive(false);
        tetrisLockImage.gameObject.SetActive(true);
        tetrisImage.gameObject.SetActive(false);
    }

    //lOGIC TO HANDLE THE BUTTONS TO SHOW AND HIDE THE OANELBADGE 
    public void ShowMinigames()
    {
        minigames.gameObject.SetActive(true);
    }

    public void HideMinigames()
    {
        minigames.gameObject.SetActive(false);
    }
    
    //nECESSARY TO CONTINUOSLY BE DONE, SO TO UPDATE THE BADGE PANEL EVERYTIME I AM IN THE MAINMAP 
    public void Update()
    {
        SetVariable();
    }

    //Function called in the GameManager when solved the problem (QUINDI QUANDO TASK INDEX ARRIVA A 3) 
    //For example the problem extracted fire 1 -> when it is the first time  tolgo il lock
    //metto il badge e faccio vedere il counter che viene messo a 1 (chiamato dal gamemanager) +
    //le altre volte semplicemente incrementa il counter 
    //Il problema viene chiamato con previous problem perchè problem now una volta fatte tutte e tre le task si setta a zero 
    public void SetVariable()
    {
        // anxiety
         foreach (var mg in GameManager.instance.anxietyEverSorted)
        {
            switch (mg)
            { 
                case 1: // Breathing
                    if (true) // prima implementazione senza counter
                    {
                        breathingLockImage.gameObject.SetActive(false);
                        breathingImage.gameObject.SetActive(true);
                    }
                break;
                case 2: // Leaf
                     if (true) // prima implementazione senza counter
                     {
                         leafLockImage.gameObject.SetActive(false);
                         leafImage.gameObject.SetActive(true);
                     }
                    break;
                case 3: // Word puzzle
                    if (true) // prima implementazione senza counter
                    {
                        puzzleLockImage.gameObject.SetActive(false);
                        puzzleImage.gameObject.SetActive(true);
                    }
                    break;
                default:
                    Debug.LogWarning("Invalid variable name: " + mg);
                    break;
            }
        }
         // literacy
         foreach (var mg in GameManager.instance.literacyEverSorted)
         {
             switch (mg)
             { 
                 case 1: // Potions
                     if (true) // prima implementazione senza counter
                     {
                         potionsLockImage.gameObject.SetActive(false);
                         potionsImage.gameObject.SetActive(true);
                     }
                        
                     break;
                 case 2: // Sources
                     if (true) // prima implementazione senza counter
                     {
                         sourceLockImage.gameObject.SetActive(false);
                         sourceImage.gameObject.SetActive(true);
                     }
                        
                     break;
                 
                 default:
                     Debug.LogWarning("Invalid variable name: " + mg);
                     break;
             }
         }
         // climate
         foreach (var mg in GameManager.instance.climateEverSorted)
         {
             switch (mg)
             { 
                 case 1: // Breathing
                     if (true) // prima implementazione senza counter
                     {
                         quizLockImage.gameObject.SetActive(false);
                         quizImage.gameObject.SetActive(true);
                     }
                        
                     break;
                 case 2:
                     if (true) // prima implementazione senza counter
                     {
                         firepuzzleLockImage.gameObject.SetActive(false);
                         firepuzzleImage.gameObject.SetActive(true);
                     }
                        
                     break;
                 case 3:
                     if (true) // prima implementazione senza counter
                     {
                         fishingLockImage.gameObject.SetActive(false);
                         fishingImage.gameObject.SetActive(true);
                     }
                     break;
                 case 4:
                     if (true) // prima implementazione senza counter
                     {
                         gardeningLockImage.gameObject.SetActive(false);
                         gardeningImage.gameObject.SetActive(true);
                     }
                     break;
                 case 5:
                     if (true) // prima implementazione senza counter
                     {
                         tetrisLockImage.gameObject.SetActive(false);
                         tetrisImage.gameObject.SetActive(true);
                     }
                     break;
                    
                 default:
                     Debug.LogWarning("Invalid variable name: " + mg);
                     break;
             }
         }
             
    }
}