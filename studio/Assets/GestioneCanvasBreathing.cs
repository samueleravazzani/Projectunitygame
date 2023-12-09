using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GestioneCanvasBreathing : MonoBehaviour
{
    // Start is called before the first frame update
    
    
    [SerializeField] private TextMeshProUGUI OutroText;
    [SerializeField] private Canvas OutroCanvas;
    //[SerializeField] private Button Retry;
    //[SerializeField] private Button Quit;
    [SerializeField] private Button Home;
    private bool lose;
    private bool win;
    private int vite = 3;
    
    //retta di calibrazione
    private int x;
    public int y;
    private float m = 4/9f;
    private float q= 23/9f;
    
    private static GestioneCanvasBreathing instance; //creazione dell'istance per il singleton
    
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("find more than one dialogue Manager in the scene");
        }
        instance = this;
        //DontDestroyOnLoad(this.gameObject);
    }
     
    public static GestioneCanvasBreathing GetInstance()
    {
        return instance;
    }
    
    
    
    
    void Start()
    {
        OutroCanvas.enabled = false;
        x = Mathf.RoundToInt(GameManager.instance.anxiety);
        Debug.Log("valore anxiety: "+ x.ToString());
        y =Mathf.RoundToInt( m * x + q); //ottengo il valore di punti da prendere per vincere
        Debug.Log("round da fare: "+ y.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
   //gestione dello score nel caso serva 
    public void checkscore (int score, int miss)
    {
        //controllo del punteggio
        if (score == y)
        {
            OutroText.text = "Congratulation, you win!";
            //Retry.gameObject.SetActive(false);
            //Quit.gameObject.SetActive(false);
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
//funzione di retry nel caso serva
    public void retry()
    {
        SceneManager.LoadScene("Impostor");
    }

    public void winning()
    {
        OutroText.text = "Congratulation, you win!";
        OutroCanvas.enabled = true;
        
    }
    
    
}
