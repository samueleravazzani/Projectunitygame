using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using TMPro;
using UnityEngine.UI;

public class GestioneCanvasImpostor : MonoBehaviour
{
    // Start is called before the first frame update
    
    
    [SerializeField] private TextMeshProUGUI OutroText;
    [SerializeField] private Canvas OutroCanvas;
    [SerializeField] private Button Retry;
    [SerializeField] private Button Quit;
    [SerializeField] private Button Home;
    private bool lose;
    private bool win;
    private int vite = 3;
    
    //retta di calibrazione
    private int x;
    private int y;
    private float m = 2/9f;
    private float q= 34/9f;
    
    private static GestioneCanvasImpostor instance; //creazione dell'istance per il singleton
    
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("find more than one dialogue Manager in the scene");
        }
        instance = this;
        //DontDestroyOnLoad(this.gameObject);
    }
     
    public static GestioneCanvasImpostor GetInstance()
    {
        return instance;
    }
    
    
    
    
    void Start()
    {
        OutroCanvas.enabled = false;
        x = Mathf.RoundToInt(GameManager.instance.literacy_inverted);
        Debug.Log("valore literacy_inverted: "+ x.ToString());
        y =Mathf.RoundToInt( m * x + q); //ottengo il valore di punti da prendere per vincere
        Debug.Log("valore risposte corrette da dare: "+ y.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    public void checkScore(int score, int miss)
    {
        //controllo del punteggio
        if (score == y)
        {
            OutroText.text = "Congratulation, you win!";
            Retry.gameObject.SetActive(false);
            Quit.gameObject.SetActive(false);
            OutroCanvas.enabled = true;
            Debug.Log("Hai vinto!");
            win = true;
        }
        Debug.Log("miss: "+ miss.ToString());

        if (miss == vite)
        {
            
            OutroText.text = "Oh no, you lose!";
            Home.gameObject.SetActive(false);
            OutroCanvas.enabled = true;
            Debug.Log("Hai perso");
            lose = true;
        }
    }
    
    
}
