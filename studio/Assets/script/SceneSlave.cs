using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSlave : MonoBehaviour
{
    public int category;
    public int minigame_int;
    public string scenetoload;
    public bool playeractive;
    public Vector3 playerposition;
    private bool teleport=false;

    private void Start()
    {
        if (GameManager.instance.tasks_picked[category] == minigame_int)
        {
            gameObject.SetActive(true);
        }
        else if (scenetoload == "Home" || scenetoload == "MainMap")
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (teleport)
        {
            // profilo appena creato -> inizializzo
            if (SceneManager.GetActiveScene().name == "Home" && GameManager.instance.n_world_saved == 0 &&
                GameManager.instance.task_index == 0) ;
            {
                GameManager.instance.InitializeGameFirstTime();
            }
            ActivateChangeScene();
        }
        
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.CompareTag("Player"))
        {
            teleport = true;
        }
    }

    public void ActivateChangeScene()
    {
        SceneMaster.instance.ChangeSchene(scenetoload, playeractive, playerposition);
        teleport = false;
    }

    public void TaskDone()
    {
        GameManager.instance.TaskDone(category);
    }
    
}