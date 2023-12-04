using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script manages individual note behavior in a music-related game.
//It handles note visibility, playing logic, animation, and actions when a note is missed or played incorrectly.
//It interacts with a GameController to manage game states and events.


public class Note : MonoBehaviour
{
    Animator animator; //AAnmator to handle the disappearing of the note whenn played 
    
    private bool visible; //Represents the visibility status of the note.
    public bool Visible
    {
        get => visible;
        set
        {
            visible = value;
            if (!visible) animator.Play("Invisible");
        }
    }
    //Controls the visibility of the note.
    //When set to false, triggers an animation to make the note invisible.

    public bool Played { get; set; } //Indicates whether the note has been played.
    public int Id { get; set; } //Stores the ID of the note.
    
    public int Column { get; set; } //Stores the column of the note.

    private void Awake()
    {
        animator = GetComponent<Animator>(); //Fetches the Animator component attached to the GameObject.
    }

    private void Update()
    {
        if (GameController.Instance.GameStarted.Value && !GameController.Instance.GameOver.Value)
        {
            transform.Translate(Vector2.down * GameController.Instance.noteSpeed * Time.deltaTime);
        }
    }
    //Moves the note downwards if the game is started and not over, controlled by the GameController.
    
    public void Play()
    {
        if (GameController.Instance.GameStarted.Value && !GameController.Instance.GameOver.Value)
        {
            if (Visible)
            {
                if (!Played && GameController.Instance.LastPlayedNoteId == Id - 1)
                {
                    Played = true;
                    GameController.Instance.LastPlayedNoteId = Id;
                    GameController.Instance.Score.Value++;
                    GameController.Instance.PlaySomeOfSong();
                    animator.Play("Played");
                }
                else
                {
                    StartCoroutine(GameController.Instance.EndGame());
                    Debug.Log("You pressed the wrong button");
                    GameController.Instance.wrongbutton = true;
                }
            }
        }
    }
    //  Checks if the game is ongoing and not over
    // Checks if the note is visible.
    // Verifies if the note has not been played and if the last played note ID matches the current note's ID minus one.
    // If conditions are met, marks the note as played, updates the last played note ID, increments the score, plays some of the song, and triggers a 'Played' animation.
    // Otherwise, it ends the game due to a wrong button press.
    

    public void OutOfScreen()
    {
        if (Visible && !Played)
        {
            StartCoroutine(GameController.Instance.EndGame());
            animator.Play("Missed");
            Debug.Log("You missed");
            GameController.Instance.missed = true;
        }
    }
    // Handles when a note moves out of the screen.
    // If the note is visible and not played, triggers an end game sequence due to a missed note,
    // triggers a 'Missed' animation, and sets a flag for a missed note.
    
}