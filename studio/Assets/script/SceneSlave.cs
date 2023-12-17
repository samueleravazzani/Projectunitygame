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
        if (GameManager.instance.tasks_picked[category] == minigame_int && GameManager.instance.task_index<3)
        {
            // se questo teleport è per un problema specifico che combacia con questo
            if (category == 2 && GameManager.instance.problem_now == problemspecific)
            {
                gameObject.SetActive(true);
                transform.parent.Find("Circle").gameObject.SetActive(true);
                GetComponent<SpriteRenderer>().enabled = true;
            }
            else if (category == 2 && minigame_int == 1)
            {
                gameObject.SetActive(true);
                transform.parent.Find("Circle").gameObject.SetActive(true);
            }
            else if (category != 2)
            {
                gameObject.SetActive(true);
                transform.parent.Find("Circle").gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
                transform.parent.Find("Circle").gameObject.SetActive(false);
            }
        }
        else if (scenetoload == "Home" && GameManager.instance.questionnairedone) // questionario fatto, devo andare a dormire
        {
            gameObject.SetActive(true);
            transform.parent.Find("Circle").gameObject.SetActive(true);
            transform.Find("Background").gameObject.SetActive(false);
            transform.Find("Category").gameObject.SetActive(false);
        }
        else if (scenetoload == "MainMap" && !GameManager.instance.questionnairedone) // se è per andare da casa alla MainMap
        {
            gameObject.SetActive(true);
            transform.parent.Find("Circle").gameObject.SetActive(true);
            transform.Find("Background").gameObject.SetActive(false);
            transform.Find("Category").gameObject.SetActive(false);
        }
        else if (scenetoload == "Home") // se è per andare a casa
        {
            gameObject.SetActive(true);
            transform.parent.Find("Circle").gameObject.SetActive(false);
            GetComponent<SpriteRenderer>().color = new Color(0.9069856f, 1f, 0f);
            transform.Find("Background").gameObject.SetActive(false);
            transform.Find("Category").gameObject.SetActive(false);
            transform.Find("MainMapCircle").gameObject.SetActive(false);
        }
        else if (GameManager.instance.task_index == 3 && minigame_int == 100 && !GameManager.instance.questionnairedone)
        {
            // questionnaire
            gameObject.SetActive(true);
            transform.parent.Find("Circle").gameObject.SetActive(true);
        }
        else if (GameManager.instance.questionnairedone && minigame_int == 200)
        {
            // To start
            gameObject.SetActive(true);
            transform.parent.Find("Circle").gameObject.SetActive(true);
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
        
        
        // se sono nella MainMap e viene attivata la mappa dall'alto
        if (SceneManager.GetActiveScene().name == "MainMap")
        {
           if (CameraSwitcher.isCamera1Active == false) // di solito entri qui
            {
                var spriteRenderer = transform.Find("MainMapCircle").GetComponent<SpriteRenderer>();
                Color newColor = spriteRenderer.color;
                newColor.a = 0.75f;
                transform.Find("MainMapCircle").GetComponent<SpriteRenderer>().color = newColor;
            }
            else
            {
                var spriteRenderer = transform.Find("MainMapCircle").GetComponent<SpriteRenderer>();
                Color newColor = spriteRenderer.color;
                newColor.a = 0;
                transform.Find("MainMapCircle").GetComponent<SpriteRenderer>().color = newColor;
            }
        }
        else
        {
            var spriteRenderer = transform.Find("MainMapCircle").GetComponent<SpriteRenderer>();
            Color newColor = spriteRenderer.color;
            newColor.a = 0;
            spriteRenderer.color = newColor;
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
        if (scenetoload == "Start_Scene" && GameManager.instance.questionnairedone &&
            GameManager.instance.n_world_saved == 1)
        {
            scenetoload = "Outro";
        }

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