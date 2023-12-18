using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

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
        Cursor.visible = true;
        LevelLoader.GetInstance().distruzione();
        OutroText.text = "Congratulation, you won!\nYour efforts make a positive impact on the environment and contribute to the well-being of our oceans. Gratitude for being a part of the solution!";
        Retry.gameObject.SetActive(false);
        Quit.gameObject.SetActive(false);
        OutroCanvas.enabled = true;
        Debug.Log("Hai vinto!");
    }

    public void lose()
    {
        Cursor.visible = true;
        LevelLoader.GetInstance().distruzione();
        OutroText.text = "Oh no, you lost!\nClean seas are crucial for marine life, biodiversity, and human well-being. Plastic pollution disrupts ecosystems, harms marine species, and affects the food chain. Beyond environmental impact, it threatens fisheries, tourism, and human health, emphasizing the global responsibility to curb plastic pollution for a sustainable future.";
        Home.gameObject.SetActive(false);
        OutroCanvas.enabled = true;
        Debug.Log("Hai perso");
      
    }
    
    public void retry()
    {
        SceneManager.LoadScene("Fishing 1");
    }
}
