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

    private bool start = false;
    
    //variabili per tenere traccia del punteggio
    private int score= 0;
    private int miss = 0;
    private int vite=3;
    [SerializeField] private TextMeshProUGUI ScoreText;
    [SerializeField] private TextMeshProUGUI missText;
    
    
    //retta di calibrazione
    private int x;
    private int y;
    private float m=7;
    private float q=43;
    
    //pesci e spazzatura
    public Sprite[] Fishes; 
    [SerializeField] public GameObject Fish_Prefab;
    private GameObject Fish;
    public Sprite[] Trashes; 
    [SerializeField] public GameObject Trash_Prefab;
    private GameObject Trash;
    
    public float timeInterval = 3f;

    private int randomFishes;
    private int randomTrashes;
    public int ymax;
    public int ymin;
    
    public float xmax=10f;
    public float xmin=2f;
    
    public int NumFishes=0;
    public int NumTrash =0;
    
    //vettore posizione pesci iniziale
    public Vector3 FishPosition = new Vector3 (10f,0,0);
    public Vector3 TrashPosition = new Vector3 (0,6,0);

    private int random;
    private int randomfishposition_new;
    private int randomfishposition_old;
    
    private static Collect instance; //creazione dell'istance per il singleton
    public int Trashold = 50;
    
    
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("find more than one dialogue Manager in the scene");
        }
        instance = this;
        //DontDestroyOnLoad(this.gameObject);
    }
     
    public static Collect GetInstance()
    {
        return instance;
    }
    


    private void Start()
    {
       //retta di calibrazione 
       x = Mathf.RoundToInt(GameManager.instance.climate_change_skept);
       Debug.Log("valore ccs: "+ x.ToString());
       y =Mathf.RoundToInt(m * x + q); //ottengo il valore di punti da prendere per vincere
       Debug.Log("valore punteggio da ottenere: "+ y.ToString());

    }


    private void Update()
    {
        if (!start)
        {
            StartCoroutine(FishingGame());
            start = true;
        }
    }

    IEnumerator FishingGame()
    {
        while (!win && !lose)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 2f));
            if (NumFishes <= Trashold) //per non avere troppi pesci nella scena
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
                    yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 2f));
                    FishInstantiator();
            }

            yield return null;
        }
        
        yield return null;
    }

    private void TrashInstantiator()
    {
        randomTrashes = UnityEngine.Random.Range(0, Trashes.Length - 1);
        TrashPosition = new Vector3(UnityEngine.Random.Range(xmin, xmax),6 , 0);
        Trash = Instantiate(Trash_Prefab, TrashPosition, Quaternion.identity);
        Trash.GetComponent<SpriteRenderer>().sprite = Trashes[randomTrashes];
        
    }
    
    private void FishInstantiator()
    {
        NumFishes++; //controllo del numero di pesci nella scena
        randomFishes = UnityEngine.Random.Range(0, Fishes.Length - 1);
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

    public void checkforwin()
    {
        score++;
        ScoreText.text=string.Format("Score:{0:00}",score);
        if (score == y)
        {
            win = true;
           GestioneCanvasOutroFishing.GetInstance().win();
        }

    }
    
    public void checkforlose()
    {
        miss++;
        missText.text=string.Format("Miss:{0:00}",miss);
        if (miss == vite)
        {
            lose = true;
            GestioneCanvasOutroFishing.GetInstance().lose();
        }

    }
   
    
}
