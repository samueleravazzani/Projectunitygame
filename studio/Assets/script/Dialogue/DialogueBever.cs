using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueBever : MonoBehaviour
{
    
    
    private static DialogueBever instance;
    public string currentSceneName;
   
    
    private void Awake() //Creazione del Singleton
    {
        if (instance != null)
        {
            Debug.LogWarning("find more than one dialogue Manager in the scene");
        }
        instance = this;
    }

    public static DialogueBever GetInstance()
    {
        return instance;
    }

    public void DialogueBeverManager(TextAsset[] DialoghiBever)
    {
        currentSceneName = SceneManager.GetActiveScene().name;
        switch (currentSceneName)
        {
            case "MainMap":
                if (GameManager.instance.task_index == 3 && GameManager.instance.questionnairedone == false )
                {
                    DialogeManager.GetInstance().EnterDialogueMode(DialoghiBever[4]);
                }
                else if (GameManager.instance.task_index == 3 && GameManager.instance.questionnairedone)
                {
                    DialogeManager.GetInstance().EnterDialogueMode(DialoghiBever[5]);
                }
                else 
                    DialogeManager.GetInstance().EnterDialogueMode(DialoghiBever[0]);
                break;
            case "Cave":
                DialogeManager.GetInstance().EnterDialogueMode(DialoghiBever[1]);
                break;
            case "Impostor" :
                DialogeManager.GetInstance().EnterDialogueMode(DialoghiBever[2]);
                break;
            case "Home" :
                DialogeManager.GetInstance().EnterDialogueMode(DialoghiBever[3]);
                break;
        }
    }
}
