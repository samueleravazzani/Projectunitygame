using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collect : MonoBehaviour
{
    //Booleani di fine gioco
    private bool win = false;
    private bool lose = false;

    private bool start = false;
    
    //sistemi di particelle
    public ParticleSystem particleSystem; 
    public ParticleSystem particleSystemFriends;
    
    //variabili per tenere traccia del punteggio
    private int score= 0;
    private int miss = 0;
    private int vite=3;
    
    //retta di calibrazione
    private int x;
    private int y;
    private float m;
    private float q;
    
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
    public float ymax;
    public float ymin;
    
    public float xmax=10f;
    public float xmin=2f;
    
    public int NumFishes=3;
    public int NumTrash=5;
    
    //vettore posizione pesci iniziale
    public Vector3 FishPosition = new Vector3 (10f,0,0);
    public Vector3 TrashPosition = new Vector3 (0,6,0);
   
    
    
    


    private void Start()
    {
       //retta di calibrazione 
       
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
            GameContinue();
            yield return new WaitForSeconds(UnityEngine.Random.Range(1f,5f));
        }
        
    }

    private void GameContinue()
    {
        for (int i = 0; i < NumFishes; i++)
        {
            randomFishes = UnityEngine.Random.Range(0, Fishes.Length - 1);
            FishPosition = new Vector3(10, UnityEngine.Random.Range(ymin, ymax), 0);
            Fish = Instantiate(Fish_Prefab, FishPosition, Quaternion.identity);
            Fish.GetComponent<SpriteRenderer>().sprite = Fishes[randomFishes];
        }
        
        for (int i = 0; i < NumTrash; i++)
        {
            randomTrashes = UnityEngine.Random.Range(0, Trashes.Length - 1);
            TrashPosition = new Vector3(10, UnityEngine.Random.Range(xmin, xmax), 0);
            Trash = Instantiate(Trash_Prefab, TrashPosition, Quaternion.identity);
            Trash.GetComponent<SpriteRenderer>().sprite = Trashes[randomTrashes];
        }

    }
    
}
