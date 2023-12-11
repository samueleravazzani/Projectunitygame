using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemManager : MonoBehaviour
{
    public static ParticleSystemManager instance;

    private ParticleSystem[] allParticleSystems;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void RestartParticleSystems()
    {
        allParticleSystems = FindObjectsOfType<ParticleSystem>();

        foreach (ParticleSystem particleSystem in allParticleSystems)
        {
            particleSystem.Stop();
            particleSystem.Play();
        }
    }

    public void PauseAllParticleSystems()
    {
        allParticleSystems = FindObjectsOfType<ParticleSystem>();

        foreach (ParticleSystem particleSystem in allParticleSystems)
        {
            var mainModule = particleSystem.main;
            mainModule.simulationSpeed = 0f;
        }
    }

    public void PlayAllParticleSystems()
    {
        allParticleSystems = FindObjectsOfType<ParticleSystem>();

        foreach (ParticleSystem particleSystem in allParticleSystems)
        {
            var mainModule = particleSystem.main;
            mainModule.simulationSpeed = 1f;
        }
    }
}