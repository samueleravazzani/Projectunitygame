using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Quizintro : MonoBehaviour
{ //script attaccato al trigger dell'NPC
    
    [Header("Ink JSON")] [SerializeField] private TextAsset inkJSON;
    private bool first;
    
    public string sceneName;

    private void Start()
    {
        first = false;
    }

    private void Update()
    {
        if (!DialogeManager.GetInstance().dialogueIsPlaying && !first)
        {
            DialogeManager.GetInstance().EnterDialogueMode(inkJSON);
            first = true;
        }
        else if (!DialogeManager.GetInstance().dialogueIsPlaying && first)
        {
            // The dialogue has ended, change the scene here
            changeScene();
        }
    }
    
    public void changeScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}