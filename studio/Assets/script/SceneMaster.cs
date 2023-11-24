using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMaster : MonoBehaviour
{
    public GameObject player;
    
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
        SceneManager.LoadScene(scenetoload); // carico la scena
        player.gameObject.SetActive(playeractive); // attivo/disattivo il player
        
        if (playeractive) // se serve il player -> setto la posizione
        {
            player.transform.position = playerposition;
        }
        
    }
}