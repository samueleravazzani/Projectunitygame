using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public Transform lastSpawnedNote;
    private static float noteHeight;
    private static float noteWidth;
    public Note notePrefab;
    private Vector3 noteLocalScale;
    private float noteSpawnStartPosX;
    public float noteSpeed = 3f;
    public const int NotesToSpawn = 20; //notes are spawned in groups of 20 
    private int prevRandomIndex = -1; //necessary to handle the generation of notes in different column to ensure not to have to consecutive
    public static GameController Instance { get; private set; }
    public Transform noteContainer;
    public ReactiveProperty<bool> GameStarted { get; set; }
    public ReactiveProperty<bool> GameOver { get; set; }
    public ReactiveProperty<int> Score { get; set; }
    private int lastNoteId = 1;
    public int LastPlayedNoteId { get; set; } = 0;
    public AudioSource audioSource;
    private Coroutine playSongSegmentCoroutine;
    private float songSegmentLength = 1.5f;
    private bool lastNote = false;
    private bool lastSpawn = false;
    public ReactiveProperty<bool> ShowGameOverScreen { get; set; }
    public bool PlayerWon { get; set; } = false;

    private float TimeGame; //time to last the game
    public bool missed = false; //bool to alert if he missed the tile
    public bool wrongbutton = false; //bool to alert if he pressed the wrong button
    
    
    //UI text to handle if a note is missed, or if the wrong button is pressed, or if the player has won
    public TextMeshProUGUI missedText;
    public TextMeshProUGUI wrongButtonText; 
    public TextMeshProUGUI outroText;
    
    public Button yourButton; //the retry button will be attached to it 
    public Button quitdoneButton;
    public Button quitfailedButton; 

    private float m = 7.0f; // coefficient of the calibration
    private float q = 23.0f; //for the calibration

    public Image A; 
    public Image S;
    public Image D;
    public Image F;
    
    
    private void Awake()
    {
        Instance = this; //Singleton
        GameStarted = new ReactiveProperty<bool>();
        // a ReactiveProperty<bool> represent a boolean value that can be observed by other parts of  code.
        // Changes to this boolean value would trigger notifications or reactions in other parts of  application that are subscribed to it
        GameOver = new ReactiveProperty<bool>();
        Score = new ReactiveProperty<int>();
        ShowGameOverScreen = new ReactiveProperty<bool>();
        //In the following are the game over objects that are activate according to successive logic if player wins or loses
        //In particular the loosing text is different according to if a note is missed or if the wrong button on the keyboard is pressed
        missedText.gameObject.SetActive(false);
        wrongButtonText.gameObject.SetActive(false);
        outroText.gameObject.SetActive(false);
        TimeGame = Mathf.RoundToInt( m * GameManager.instance.anxiety + q); //calibration according to the value of anxiety 
    }

    void Start()
    {
        A.gameObject.SetActive(false);
        F.gameObject.SetActive(false);
        D.gameObject.SetActive(false);
        S.gameObject.SetActive(false);
        SetDataForNoteGeneration();
        SpawnNotes();
        // Setup note generation and spawn initial notes
    }

    private void Update()
    {
        DetectNoteKeyPress();
        DetectStart();
        // Detect player input for note key presses and game start
    }

    private void DetectStart()
    {
        if (!GameController.Instance.GameStarted.Value && Input.GetMouseButtonDown(0))
        {
            GameController.Instance.GameStarted.Value = true;
            A.gameObject.SetActive(true);
            F.gameObject.SetActive(true);
            D.gameObject.SetActive(true);
            S.gameObject.SetActive(true);
        }
    }
    // Detects player input to start the game when the clicking the start button

    private void DetectNoteKeyPress()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            AttemptNotePlay(0); // Attempt to play note in the left position
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            AttemptNotePlay(1); // Attempt to play note in the middle left position
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            AttemptNotePlay(2); // Attempt to play note in the middle right position
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            AttemptNotePlay(3); // Attempt to play note in the right position
        }
    }
    
    // Detects specific key presses for playing notes, and attempts to play the note based on the pressed key

    private void AttemptNotePlay(int column)
    {
        bool correctNoteClicked = false;

        foreach (Transform note in noteContainer.transform)
        {
            if (note.CompareTag("Note"))
            {
                var noteComponent = note.GetComponent<Note>();
                if (noteComponent.Visible && noteComponent.Column == column && !noteComponent.Played)
                {
                    noteComponent.Play();
                    correctNoteClicked = true;
                    break;
                }
            }
        }
    }
    
    // This method encloses the gameplay logic by checking if the player's input matches the note
    // that can be played at that moment in the specified column. If such a note is found, it triggers its playback and marks
    // that the correct note was clicked.
    

    private void SetDataForNoteGeneration()
    {
        var topRight = new Vector3(Screen.width, Screen.height, 0);
        var topRightWorldPoint = Camera.main.ScreenToWorldPoint(topRight);
        var bottomLeftWorldPoint = Camera.main.ScreenToWorldPoint(Vector3.zero);
        var screenWidth = topRightWorldPoint.x - bottomLeftWorldPoint.x;
        var screenHeight = topRightWorldPoint.y - bottomLeftWorldPoint.y;
        noteHeight = screenHeight / 2;
        noteWidth = screenWidth / 4;
        var noteSpriteRenderer = notePrefab.GetComponent<SpriteRenderer>();
        noteLocalScale = new Vector3(
               noteWidth / noteSpriteRenderer.bounds.size.x * noteSpriteRenderer.transform.localScale.x,
               noteHeight / noteSpriteRenderer.bounds.size.y * noteSpriteRenderer.transform.localScale.y, 1);
        var leftmostPoint = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height / 2));
        var leftmostPointPivot = leftmostPoint.x + noteWidth / 2;
        noteSpawnStartPosX = leftmostPointPivot;
    }
    
    //ScreenToWorldPoint: It converts screen coordinates to world space coordinates using the camera's perspective.
    //Calculation of screen width and height: It determines the width and height of the game area in world space based on the screen size and camera's perspective.
    //Note height and width: It calculates the dimensions of a single note based on the screen size (dividing the screen height by 2 and screen width by 4).
    //Note local scale: It computes the local scale of the note prefab based on its sprite size and the calculated note dimensions to ensure proper scaling when spawning notes.
    //Note spawn position: It determines the starting X position for spawning notes. It calculates the left-most point in world space and adjusts it by half of the note's width to ensure proper positioning.
   
    //This method is used for setting up the dimensions and scaling of notes based on the screen size, ensuring that
    //notes spawn and display correctly within the game area.

    public void SpawnNotes()
    {
        if (lastSpawn) return;

        var noteSpawnStartPosY = lastSpawnedNote.position.y + noteHeight;
        Note note = null;
        var timeTillEnd = TimeGame - audioSource.time;
        int notesToSpawn = NotesToSpawn;
        if (timeTillEnd < NotesToSpawn)
        {
            notesToSpawn = Mathf.CeilToInt(timeTillEnd);
            lastSpawn = true;
        }
        for (int i = 0; i < notesToSpawn; i++)
        {
            var randomIndex = GetRandomIndex();
            for (int j = 0; j < 4; j++)
            {
                note = Instantiate(notePrefab, noteContainer.transform);
                note.Column = j;
                note.transform.localScale = noteLocalScale;
                note.transform.position = new Vector2(noteSpawnStartPosX + noteWidth * j, noteSpawnStartPosY);
                note.Visible = (j == randomIndex);
                if (note.Visible)
                {
                    note.Id = lastNoteId;
                    lastNoteId++;
                }
            }
            noteSpawnStartPosY += noteHeight;
            if (i == NotesToSpawn - 1) lastSpawnedNote = note.transform;
        }
    }
    
    //    Conditional check for spawning: Checks if notes have already been spawned (lastSpawn). If they have, the method exits.
    //Calculating Y position for spawning: Determines the starting Y position for spawning notes, positioned above the last spawned note.
    //Calculating the number of notes to spawn: Checks the remaining time compared to the default number of notes to spawn (NotesToSpawn). If there's not enough time left, adjust notesToSpawn to fit within the remaining time and marks that it's the last spawn (lastSpawn).
    //Loop to spawn notes: Iterates through the loop to spawn notes.
    //Random visibility of notes: Randomly determines the visibility of each note in a column (note.Visible = (j == randomIndex)).
    //Assigning IDs to visible notes: If a note is visible, it assigns an ID to the note and increments the lastNoteId.
    //Updating the Y position: Updates the Y position for the next set of notes to be spawned.
    //Updating the reference to the last spawned note: Keeps track of the last spawned note for reference in the next spawning cycle.

    private int GetRandomIndex()
    {
        var randomIndex = Random.Range(0, 4); //enerate a random integer between 0 and 3 (inclusive). This index determines the visibility of a note in one of the four columns.
        while (randomIndex == prevRandomIndex) randomIndex = Random.Range(0, 4); //Checks if the newly generated random index matches the previously generated one 
        // If they are the same, it generates a new random index until it finds one that is different.
        prevRandomIndex = randomIndex; //Updates prevRandomIndex to the newly generated random index for the next iteration.
        return randomIndex; //Once a different random index is obtained, it returns this index
    }
    
    //This method ensures that the visibility of notes in different columns is randomly set without consecutively repeating the same visibility pattern in subsequent calls.
    //It contributes to the randomness and diversity of note appearances during gameplay.

    public void PlaySomeOfSong()
    {
        if (!audioSource.isPlaying && !lastNote)
        {
            audioSource.Play();
        }
        if (TimeGame - audioSource.time <= songSegmentLength)
        {
            lastNote = true;
        }
        if (playSongSegmentCoroutine != null) StopCoroutine(playSongSegmentCoroutine);
        playSongSegmentCoroutine = StartCoroutine(PlaySomeOfSongCoroutine());
    }
    
    //Checks if the audio source is not playing and it's not the last note. If so, it starts playing the audio source.
    //Determines if the remaining time for the game is less than or equal to the song segment length, marking it as the last note.
    //Stops the existing coroutine if it's already running.
    //Initiates the coroutine PlaySomeOfSongCoroutine to play a segment of the song.

    private IEnumerator PlaySomeOfSongCoroutine()
    {
        yield return new WaitForSeconds(songSegmentLength);
        audioSource.Pause();
        if (lastNote)
        {
            PlayerWon = true;
            StartCoroutine(EndGame());
        }
    }
    //Waits for the specified song segment length using yield return new WaitForSeconds.
    //Pauses the audio source after the song segment length duration.
    //Checks if it's the last note and, if true, marks that the player won and initiates the EndGame coroutine.
    
    //The idea is that each note should play only a certain time of the song and then the audio is null
    //until next note is played

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    //Handle the retru button logic to restart thegame again

    public IEnumerator EndGame()
    {
        GameOver.Value = true; //Sets the GameOver.Value to true, indicating that the game is over.
        A.gameObject.SetActive(false);
        F.gameObject.SetActive(false);
        D.gameObject.SetActive(false);
        S.gameObject.SetActive(false);
        yield return new WaitForSeconds(0);
        ShowGameOverScreen.Value = true; //Sets the ShowGameOverScreen.Value to true, indicating that the game over screen should be shown
        if (missed)
        {
            missedText.gameObject.SetActive(true);
            quitdoneButton.gameObject.SetActive(false);
        }

        if (wrongbutton)
        {
            wrongButtonText.gameObject.SetActive(true);
            quitdoneButton.gameObject.SetActive(false);
        }

        if (PlayerWon)
        {
            yourButton.gameObject.SetActive(false);
            quitfailedButton.gameObject.SetActive(false);
            outroText.gameObject.SetActive(true);
        }
        // If missed is true, it activates the missedText UI element to inform the player about missing notes.
        // If wrongbutton is true, it activates the wrongButtonText UI element to inform the player about pressing the wrong button.
        // If PlayerWon is true, it hides the yourButton (which is the retry) UI element and activates the outroText UI element to display the game-ending text.
        //This is to distinguish cases in which the player loses and so he can retry the game or quit + the sepcific error text alert
        //and case in which the player wins and so the congratulation text + an outro 
        
    }
}

