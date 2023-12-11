using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu1 : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private List<AudioSource> audioSources = new List<AudioSource>();
    private Scene currentScene;
    private ParticleSystemManager particleSystemManager;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene();

        // Ottenere tutte le tracce audio nella scena
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioSource in allAudioSources)
        {
            audioSources.Add(audioSource);
        }

        // Ottieni il riferimento al ParticleSystemManager
        particleSystemManager = FindObjectOfType<ParticleSystemManager>();
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

        // Metti in pausa tutti i sistemi di particelle
        particleSystemManager.PauseAllParticleSystems();
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

        // Riprendi tutti i sistemi di particelle
        particleSystemManager.PlayAllParticleSystems();
    }

    public void ResetScene()
    {
        // Riavvia la scena corrente
        SceneManager.LoadScene(currentScene.buildIndex);

        // Chiamata al ParticleSystemManager per riavviare i sistemi di particelle
        particleSystemManager.RestartParticleSystems();
    }
}