using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Collect : MonoBehaviour
{
    //Booleani di fine gioco
    private bool win = false;
    private bool lose = false;
    
    //Booleano per inizializzare la Coroutine
    private bool start = false;
    
    //variabili per tenere traccia del punteggio
    private int score= 0;
    public int miss = 0;
    private int vite=3;
    //Canvas per tenere traccia del punteggio
    [SerializeField] private TextMeshProUGUI ScoreText;
    [SerializeField] private TextMeshProUGUI missText;
    
    
    //retta di calibrazione
    private int x;
    private int y1; //parametro per identificare 1/3 del punteggio totale da raggiungere
    private int y2; //parametro per identificare 2/3 del punteggio totale da raggiungere
    private int y; //parametro per identificare 3/3 del punteggio totale da raggiungere
    private float m=30/9f;
    private float q=240/9f;
    
    //pesci 
    public Sprite[] Fishes; 
    [SerializeField] public GameObject Fish_Prefab;
    private GameObject Fish;
    private int randomFishes;
    //Spazzature
    public Sprite[] Trashes; 
    [SerializeField] public GameObject Trash_Prefab;
    private GameObject Trash;
    private int randomTrashes;
    
   //altezza massima e minima dello screen per spawnare i pesci
    public int ymax;
    public int ymin;
    //largezza massima e minima dello screen per spawnare la spazzatura
    public float xmax=10f;
    public float xmin=0f;
    
    //vettore posizione pesci e spazzatura iniziale (per lo spawn)
    public Vector3 FishPosition = new Vector3 (10f,0,0);
    public Vector3 TrashPosition = new Vector3 (0,6,0);

    private int random; //variabile in cui vengono salvati i valori random per il calcolo della probabilità
    //varibili per tenere traccia della posizione del pesce spawnato per aumentare la variabilità della
    //posizione di spawn
    private int randomfishposition_new;
    private int randomfishposition_old;
    
    private static Collect instance; //creazione dell'istance per il singleton
    
    public int NumFishes=0;//valore che tiene traccia del numero di pesci in gioco.
    public int Trashold = 50;//valore parametrico del numero massimo di pesci che possono essere nella scena
    //aumentando il livello, aumenta la difficoltà e quindi il numero di pesci in gioco
    public int Trashold1=70;
    public int Trashold2=90;

    public int level; //variabile che tiene traccia del livello in cui sono

    public GameObject retino;
    
    
    
    //AWAKE, CREAZIONE DEL SINGLETON
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("find more than one dialogue Manager in the scene");
        }
        instance = this;
    }
    
    //FUNZIONE PER OTTENERE L'INSTANCE
    public static Collect GetInstance()
    {
        return instance;
    }
    


    //FUNZIONE DI START, RETTA DI CALIBRAZIONE E VIENE MOSTRATO IL PRIMO LIVELLO
    private void Start()
    {
       //retta di calibrazione da implementare
       x = Mathf.RoundToInt(GameManager.instance.climate_change_skept);
       Debug.Log("valore ccs: "+ x.ToString());
       y =Mathf.RoundToInt(m * x + q); //ottengo il valore di punti da prendere per vincere
       Debug.Log("valore punteggio da ottenere: "+ y.ToString());


        //Rende il cursore del mouse invisibile. per farlo avvenire devi essere in modalità playmode
        //per farlo devi fare doppio click sullo schermo di gioco.
        Cursor.visible = false;
        
        //SUDDIVISIONE DELL'OBIETTIVO
       // y = 12; //questo è per debug
        Debug.Log("valore y: "+ y.ToString());
        y1 = Mathf.RoundToInt(y/3);
        y2 = Mathf.RoundToInt(y/3 * 2);
        Debug.Log("valore y1: "+ y1.ToString());
        Debug.Log("valore y2: "+ y2.ToString());
        
        //GESTIONE DEL LIVELLO
        level = 1;
        LevelLoader.GetInstance().levelLoader(level);

    }


    //FUNZIONE DI UPDATE IN CUI VIENE CHIAMATA LA COROUTINE
    private void Update()
    {
        if (!start)
        {
            StartCoroutine(FishingGame());
            start = true;
        }
    }

    
    //COROUTINE CHE IMPLEMENTA LA LOGICA DI GIOCO
    IEnumerator FishingGame()
    {
        while (!win && !lose) //finchè non vinci o perdi spawna pesci e spazzature secondo una certa logica
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 2f));
            if (NumFishes <= Trashold) //trashold cambia a secondo del livello in cui si è
            {
                FishInstantiator();
                random = UnityEngine.Random.Range(0, 100);
                if (random >= 40)
                {
                    yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 2f));
                    TrashInstantiator();
                }
            }

            yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 2f));
            TrashInstantiator();
            random = UnityEngine.Random.Range(0, 100);
            if (random >= 40)
            {
                if (NumFishes <= Trashold)
                {
                    yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 2f));
                    FishInstantiator();
                }
            }

            yield return null;
        }
        LevelLoader.GetInstance().distruzione();
        
        yield return null;
    }

   //FUNZIONE DI SPAWN DELLA SPAZZATURA
    private void TrashInstantiator()
    {
        randomTrashes = UnityEngine.Random.Range(0, Trashes.Length - 1);
        TrashPosition = new Vector3(UnityEngine.Random.Range(xmin, xmax),6 , 0);
        Trash = Instantiate(Trash_Prefab, TrashPosition, Quaternion.identity);
        Trash.GetComponent<SpriteRenderer>().sprite = Trashes[randomTrashes];
        
    }
    
    //FUNZIONE DI SPAWN DEI PESCI
    private void FishInstantiator()
    {
        NumFishes++; //controllo del numero di pesci nella scena
        randomFishes = UnityEngine.Random.Range(0, Fishes.Length - 1);
        //controllo per cercare di avere più variabilità nella posizione dei pesci
        randomfishposition_old = randomfishposition_new;
        randomfishposition_new = UnityEngine.Random.Range(ymin, ymax);
        if (randomfishposition_new == randomfishposition_old)
        {
            randomfishposition_new=UnityEngine.Random.Range(ymin, ymax);
        }
        FishPosition = new Vector3(10, randomfishposition_new, 0);
        Fish = Instantiate(Fish_Prefab, FishPosition, Quaternion.identity);
        Fish.GetComponent<SpriteRenderer>().sprite = Fishes[randomFishes];
    }
    
    
    //FUNZIONE CHE CONTROLLA IL PUNTEGGIO
    public void checkforwin()
    {
        score++; //aumento lo score quando viene presa una spazzatura
        ScoreText.text=string.Format($"Score:{score}/{y}"); //aggiorno lo score
        //controllo per il cambio di livello 
        if (score == y1 || score==y2)
        {
            level++;
            LevelLoader.GetInstance().levelLoader(level);
            NumFishes = 0;
            if (score == y1) //qui aumenta solo il numero dei pesci
            {
                Trashold = Trashold1;
            }
            else if (score == y2) //qui aumenta sia il numero dei pesci + se la spazzatura tocca il fondo viene tolta una vita
            {
                Trashold = Trashold2;
            }
        }
        //controllo per la vittoria
        if (score == y)
        {
            retino.GetComponent<BoxCollider2D>().isTrigger = false;
            LevelLoader.GetInstance().distruzione();// distuggo gli oggetti in scena
            win = true;
            Cursor.visible = true;
           GestioneCanvasOutroFishing.GetInstance().win();
           LevelLoader.GetInstance().distruzione();
        }

    }
    
    //FUNZIONE PER LA SCONFITTA
    
    public void checkforlose()
    {
        miss++;
        missText.text=string.Format("Miss:{0:00}",miss);
        if (miss == vite)
        {
            LevelLoader.GetInstance().distruzione();// distuggo gli oggetti in scena
            lose = true;
            Cursor.visible = true;
            GestioneCanvasOutroFishing.GetInstance().lose();
            LevelLoader.GetInstance().distruzione();// distuggo gli oggetti in scena
        }

    }
    
}
