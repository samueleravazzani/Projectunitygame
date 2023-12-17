using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pausa : MonoBehaviour
{
    //This script is to handler a problem happened during the add of the pause menu option 
    //The music of the leaf continued although it's reproduction was 1.5f and so the game logic was lost since is all based on the songsegmentlenght reproduction
    //Therefore is added this additional system to pause the music when the button pause is pressed
    //and either if the home or the come back in play button is pressed the audio restarts
    
    public Button button1; 
    public Button button2;
    public Button button3;

    // Start is called before the first frame update
    void Start()
    {
        // Add listeners to the button click events
        button1.onClick.AddListener(PauseAudio);
        button2.onClick.AddListener(UnpauseAudio);
        button3.onClick.AddListener(UnpauseAudio);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseAudio()
    {
        // Pause the audio
        AudioListener.pause = true;
    }

    public void UnpauseAudio()
    {
        // Unpause the audio
        AudioListener.pause = false;
    }
}