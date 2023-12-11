using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu1 : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    private List<AudioSource> audioSources = new List<AudioSource>();

    void Start()
    {
        // Trova tutti gli oggetti con componente AudioSource nella scena e aggiungili alla lista
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioSource in allAudioSources)
        {
            audioSources.Add(audioSource);
        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;

        // Disattiva tutte le tracce audio quando metti in pausa
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.Pause();
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;

        // Riattiva tutte le tracce audio quando riprendi il gioco
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.UnPause();
        }
    }

 
}