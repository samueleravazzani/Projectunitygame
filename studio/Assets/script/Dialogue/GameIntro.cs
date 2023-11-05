using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameIntro : MonoBehaviour
{ //script attaccato al trigger dell'NPC
    
    [Header("Ink JSON")] [SerializeField] private TextAsset inkJSON;
    private bool first = false;
    private void Update()
    {
        if (!DialogeManager.GetInstance().dialogueIsPlaying && !first)
        {
            DialogeManager.GetInstance().EnterDialogueMode(inkJSON);
            first = true;
        }
    }
    
}