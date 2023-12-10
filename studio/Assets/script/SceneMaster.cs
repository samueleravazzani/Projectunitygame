using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneMaster : MonoBehaviour
{
    public GameObject player;
    public GameObject Bever;
    public static SceneMaster instance;
    public float transitionTime = 1f;
    public Animator transition;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
    
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void ChangeSchene(string scenetoload, bool playeractive, Vector3 playerposition)
    {
        transition = GameObject.Find("SceneLoaderTransition/Crossfade").GetComponent<Animator>();
        //SceneManager.LoadScene(scenetoload); // carico la scena
        StartCoroutine(LoadTransitionScene(scenetoload,playeractive,playerposition));
       /* player.gameObject.SetActive(playeractive); // attivo/disattivo il player
        player.GetComponent<playerMovement>().enabled = playeractive; // attivo/disattivo il movimento del player
        Bever.gameObject.SetActive(playeractive);
        
        if (playeractive) // se serve il player -> setto la posizione
        {
            player.transform.position = playerposition;
            // non funziona player.GetComponent<playerMovement>().worldBorders = GameObject.Find("WorldBorders").GetComponent<PolygonCollider2D>();
            Bever.transform.position = playerposition;
        }
        
        // se la scena da caricare è la MainMap -> salvo
        if (scenetoload == "MainMap")
        {
            GameManager.instance.Save();
        }
        */
        
    }

    IEnumerator LoadTransitionScene (string scenetoload,bool playeractive,Vector3 playerposition)
    {
        player.GetComponent<playerMovement>().enabled = false; 
        player.GetComponent<Animator>().enabled = false; 
        player.GetComponent<SpriteRenderer>().enabled = false; 
        Bever.GetComponent<SpriteRenderer>().enabled = false;
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(scenetoload);
        player.gameObject.SetActive(playeractive); // attivo/disattivo il player
        player.GetComponent<playerMovement>().enabled = playeractive;
        player.GetComponent<Animator>().enabled = playeractive; // attivo/disattivo il movimento del player
        Bever.gameObject.SetActive(playeractive);
        Bever.transform.Find("VisualCue").gameObject.SetActive(playeractive);
        
        if (playeractive) // se serve il player -> setto la posizione
        {
            player.transform.position = playerposition;
            // non funziona player.GetComponent<playerMovement>().worldBorders = GameObject.Find("WorldBorders").GetComponent<PolygonCollider2D>();
            Bever.transform.position = playerposition;
            player.GetComponent<SpriteRenderer>().enabled = playeractive; 
            Bever.GetComponent<SpriteRenderer>().enabled = playeractive;
        }
        
        // se la scena da caricare è la MainMap o la schermata home -> salvo
        if (scenetoload == "MainMap" || scenetoload == "Start_Scene")
        {
            Debug.Log(GameManager.instance.climate_change_skept);
            GameManager.instance.Save();
        }
        
    }

    
}