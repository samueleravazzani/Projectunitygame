using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    private Vector3 playerposition = new Vector3(-0.5f,-5.35f,0);
    private GameObject player;
    private GameObject Bever;
    
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
        player = GameObject.Find("player");
        Bever = GameObject.Find("Bever");
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
            OutroText.text = "Congratulation, you win!\nUnderstanding digital health and having good health literacy is" +
                             " essential for making informed decisions about your well-being." +
                             " It empowers you to use technology for health purposes and navigate healthcare information," +
                             " ensuring you can actively manage and advocate for your health effectively.";
            Retry.gameObject.SetActive(false);
            Quit.gameObject.SetActive(false);
            OutroCanvas.enabled = true;
            Debug.Log("Hai vinto!");
            win = true;
        }
        Debug.Log("miss: "+ miss.ToString());

        if (miss == vite)
        {
            
            OutroText.text = "Oh no, you lose!\nBeing misinformed about digital health can lead to poor decision-making for your well-being." +
                             " Inaccurate information may result in using technology ineffectively or misunderstanding health advice, potentially compromising your health outcomes." +
                             " Staying well-informed is crucial for making sound choices in the digital age.";
            Home.gameObject.SetActive(false);
            OutroCanvas.enabled = true;
            Debug.Log("Hai perso");
            lose = true;
        }
    }

    public void retry()
    {
        player.gameObject.transform.position = playerposition;
        Bever.gameObject.transform.position = playerposition;
        SceneManager.LoadScene("Impostor");
    }
    
    
}
