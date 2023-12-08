using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSlave : MonoBehaviour
{
    public int category;
    public int minigame_int;
    public int problemspecific;
    public string scenetoload;
    public bool playeractive;
    public Vector3 playerposition;
    private bool teleport=false;

    private void Start()
    {
        // se l'elemento category del vettore tasks_picked (= la task di questa categoria) combacia con quello di questo teleport
        if (GameManager.instance.tasks_picked[category] == minigame_int)
        {
            // se questo teleport è per un problema specifico che combacia con questo
            if (category == 2 && GameManager.instance.problem_now == problemspecific)
            {
                gameObject.SetActive(true);
                transform.parent.Find("Circle").gameObject.SetActive(true);
                GetComponent<SpriteRenderer>().enabled = true;
            }
            else if (category != 2)
            {
                gameObject.SetActive(true);
                transform.parent.Find("Circle").gameObject.SetActive(true);
            }
        }
        else if (scenetoload == "Home" || scenetoload == "MainMap")
        {
            gameObject.SetActive(true);
            transform.parent.Find("Circle").gameObject.SetActive(false);
            GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            gameObject.SetActive(false);
            transform.parent.Find("Circle").gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (teleport)
        {
            // profilo appena creato -> inizializzo
            if (!GameManager.instance.profile_created) // se il profilo non è ancora stato creato
            {
                // VECCHIA CONDIZIONE: SceneManager.GetActiveScene().name == "Home" && GameManager.instance.n_world_saved == 0 && GameManager.instance.task_index == 0
                ActivateChangeScene();
                GameManager.instance.InitializeGameFirstTime();
                return;
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

    public void TaskDoneAndActivateChangeScene()
    {
        GameManager.instance.TaskDone(category);
        ActivateChangeScene();
    }

    public void TaskFailedAndActivateChangeScene()
    {
        GameManager.instance.TaskFailed(category); 
        ActivateChangeScene();
    }
    
     public void TaskRetry()
     {
         GameManager.instance.TaskFailed(category);      
    }
    
}