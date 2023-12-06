using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMaster : MonoBehaviour
{
    public GameObject player;
    public GameObject Bever;
    public static SceneMaster instance;

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
        if (playeractive) // se serve il player -> setto la posizione
        {
            player.transform.position = playerposition;
            playerMovement.instance.worldBorders = GameObject.Find("WorldBorders").GetComponent<Collider2D>();
        }
        SceneManager.LoadScene(scenetoload); // carico la scena
        player.gameObject.SetActive(playeractive); // attivo/disattivo il player
        player.GetComponent<playerMovement>().enabled = playeractive; // attivo/disattivo il movimento del player
        Bever.gameObject.SetActive(playeractive);
        
        // se la scena da caricare è la MainMap -> salvo
        if (scenetoload == "MainMap")
        {
            GameManager.instance.Save();
        }
        
    }
}