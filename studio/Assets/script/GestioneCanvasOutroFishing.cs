using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GestioneCanvasOutroFishing : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI OutroText;
    [SerializeField] private Canvas OutroCanvas;
    [SerializeField] private Button Retry;
    [SerializeField] private Button Quit;
    [SerializeField] private Button Home;
    
    
    private static GestioneCanvasOutroFishing instance; //creazione dell'istance per il singleton
    
    
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("find more than one dialogue Manager in the scene");
        }
        instance = this;
        
    }
     
    public static GestioneCanvasOutroFishing GetInstance()
    {
        return instance;
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        OutroCanvas.enabled = false;
    }

    public void win()
    {
        LevelLoader.GetInstance().distruzione();
        OutroText.text = "Congratulation, you won!";
        Retry.gameObject.SetActive(false);
        Quit.gameObject.SetActive(false);
        OutroCanvas.enabled = true;
        Debug.Log("Hai vinto!");
    }

    public void lose()
    {
        LevelLoader.GetInstance().distruzione();
        OutroText.text = "Oh no, you lost!.";
        Home.gameObject.SetActive(false);
        OutroCanvas.enabled = true;
        Debug.Log("Hai perso");
      
    }
    
    public void retry()
    {
        SceneManager.LoadScene("Fishing 1");
    }
}
