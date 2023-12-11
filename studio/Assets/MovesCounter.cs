using UnityEngine;
using System.Collections.Generic;
using TMPro; 

public class MovesCounter : MonoBehaviour
{
    private int maxMoves = 1;
    private int remainingMoves;
    public TMPro.TextMeshProUGUI movesText; 
    public GameObject key1;
    public GameObject popup; 
    bool hasMoved = false;
    private List<AudioSource> audioSources = new List<AudioSource>();

    void Start()
    {
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioSource in allAudioSources)
        {
            audioSources.Add(audioSource);
        }
        remainingMoves = maxMoves;
        UpdateMovesText();
    }

    void Update()
    {
        if (key1 != null && Input.GetKeyDown(KeyCode.Mouse0) && !hasMoved)
        {
            remainingMoves--;
            UpdateMovesText();
            hasMoved = true;
            Invoke("ShowPopup", 1f);
        }
    }

    void UpdateMovesText()
    {
        if (movesText != null)
        {
            movesText.text =  remainingMoves.ToString();
        }
    }

    void ShowPopup()
    {
        if (popup != null)
        {
            popup.SetActive(true); 
            Time.timeScale = 0f;
        }
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.Pause();
        }
    }
    
    public void ClosePopup()
    {
        if (popup != null)
        {
            popup.SetActive(false); 
            Time.timeScale = 1f; 
        }
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.UnPause();
        }
    }
}