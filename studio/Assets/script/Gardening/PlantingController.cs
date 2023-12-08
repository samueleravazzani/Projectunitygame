using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlantingController : MonoBehaviour
{
    public Tilemap groundTilemap; // Reference to the ground Tilemap
    public Tilemap plantTilemap;
    public Tile nonFertileTile;   // Non-fertile tile
    public Tile fertileTile;       // Fertile tile
    public Tile missTile;
    public Sprite[] Talpe; //aggiunta delle talpe da non schiacciare
    [SerializeField] public GameObject Talpa_prefab;
    private GameObject Talpa; // talpa instantiata, creata
    [SerializeField] public Tile[] plantTile;  // Plant tile
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI missText;
    [SerializeField] private TextMeshProUGUI OutroText;
    [SerializeField] private Canvas OutroCanvas;
    [SerializeField] private Button Retry;

    public float fertileInterval = 2f; // Time interval to make a tile fertile

    private Vector3Int fertileTilePosition; // Position of the current fertile tile
    private bool ok = false;
    private bool firstDone = false;
    int score = 0;
    private int miss = 0;
    private int randomTile; //sceglie se compare una tile fertile oppure una talpa
    private int randomTalpa;
    private bool talpaIsPresent;
    private Vector3Int CentreTilePosition;
    private int x; //valore di ccs per la retta di calibrazione
    private int y; //valore delle piante che il player deve riuscire a piantare
    private int m = -3; //coefficiente angolare della retta da me calcolata; voglio che il  massimo
    //di piante da piantare sia 78 e il minimo 51, più sei scettico (valore CCs più basso, più piante 
    //dovrai piantare.
    private int q = 81;//intercetta da me calcolata secondo i parametri precedenti
    private int vite = 3;
    private bool lose;
    private bool win;


    private void Start()
    {//creazione della retta di calibrazione prendendo i parametri
        //salvo il parametro del climate change che mi serve per la pareametrizzazione
        OutroCanvas.enabled = false;
        x = Mathf.RoundToInt(GameManager.instance.climate_change_skept);
        Debug.Log("valore ccs: "+ x.ToString());
        y = m * x + q; //ottengo il valore di punti da prendere per vincere
        Debug.Log("valore punteggio: "+ y.ToString());
    }

    void Update()
    {
        if (!DialogeManager.GetInstance().dialogueIsPlaying && ok && !firstDone)
        {
            // Start the coroutine to make tiles fertile periodically
            StartCoroutine(MakeTilesFertilePeriodically());
            firstDone = true;
        }

        ok = true;
    }

    IEnumerator MakeTilesFertilePeriodically()
    {
        while (!win && !lose)
        {
            // Make a random non-fertile tile fertile
            MakeTileFertile();

            // Wait for the next fertile interval
            yield return new WaitForSeconds(fertileInterval);
        }
    }

    void MakeTileFertile()
    {
        // Find all non-fertile tiles
        List<Vector3Int> nonFertileTiles = new List<Vector3Int>();

        BoundsInt bounds = groundTilemap.cellBounds;
        for (int x = bounds.x; x < bounds.x + bounds.size.x; x++)
        {
            for (int y = bounds.y; y < bounds.y + bounds.size.y; y++)
            {
                Vector3Int position = new Vector3Int(x, y, 0);

                // If the tile is non-fertile, add it to the list
                if (groundTilemap.GetTile(position) == nonFertileTile)
                {
                    nonFertileTiles.Add(position);
                }
            }
        }
        
        //|| groundTilemap.GetTile(position) == missTile

        // If there are non-fertile tiles, make one of them fertile
        if (nonFertileTiles.Count > 0)
        {
            // Randomly choose a non-fertile tile
            fertileTilePosition = nonFertileTiles[Random.Range(0, nonFertileTiles.Count)];

            randomTile = Random.Range(0, 100);

            // Set the tile to be fertile 80%
            if (randomTile <= 80)
            {
                groundTilemap.SetTile(fertileTilePosition, fertileTile);
            }
            else //restante 20% di possibilità
            {
                groundTilemap.SetTile(fertileTilePosition, fertileTile);
                randomTalpa = Random.Range(0, Talpe.Length-1);
                Talpa=Instantiate(Talpa_prefab, fertileTilePosition + new Vector3(0.51f,1.2f,0),Quaternion.identity);
                Talpa.GetComponent<SpriteRenderer>().sprite = Talpe[randomTalpa];
                talpaIsPresent = true;
            }

            // Start a coroutine to check if the player clicked on the fertile tile
            StartCoroutine(CheckForPlayerClick());
        }
    }

    IEnumerator CheckForPlayerClick()
    {
        float timer = 0f;

        while (timer < fertileInterval)
        {
            // Increment the timer
            timer += Time.deltaTime;

            // Check if the player clicked on the fertile tile
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3Int clickedTilePosition = groundTilemap.WorldToCell(clickPosition);

                // If the player clicked on the fertile tile, plant a plant
                if (clickedTilePosition == fertileTilePosition && !talpaIsPresent)
                {//se clicca sulla tile fertile e non c'è la talpa
                    
                    int randomIndex = Random.Range(0, plantTile.Length);
                    plantTilemap.SetTile(fertileTilePosition, plantTile[randomIndex]);
                    score++;
                    scoreText.text=string.Format("Plant:{0:00}",score);
                }
                else if(clickedTilePosition == fertileTilePosition && talpaIsPresent)

                {//se clicca sulla tile fertile e c'è la talpa
                    miss++;
                    missText.text=string.Format("Miss:{0:00}",miss);
                    talpaIsPresent = false;
                    Destroy(Talpa);
                    groundTilemap.SetTile(fertileTilePosition, missTile);
                }
                else if(talpaIsPresent)

                { //se non clicca su una tile fertile ma era comparsa la talpa
                    miss++;
                    missText.text=string.Format("Miss:{0:00}",miss);
                    talpaIsPresent = false;
                    Destroy(Talpa);
                    groundTilemap.SetTile(fertileTilePosition, missTile);
                }
                else
                {//se clicca non su una tile fertile
                    miss++;
                    missText.text=string.Format("Miss:{0:00}",miss);
                    groundTilemap.SetTile(fertileTilePosition, missTile);

                }

                checkScore(score, miss);

                // Exit the coroutine
                yield break;
            }

            yield return null;
        }

        // If the player didn't click within the time frame, reset the tile to non-fertile
        //groundTilemap.SetTile(fertileTilePosition, nonFertileTile);
        if (talpaIsPresent)
        {
            talpaIsPresent = false;
            Destroy(Talpa);
            groundTilemap.SetTile(fertileTilePosition, nonFertileTile);
            score++;
            scoreText.text=string.Format("Plant:{0:00}",score);
            
        }
        else
        {
            miss++;
            missText.text = string.Format("Miss:{0:00}", miss);
            groundTilemap.SetTile(fertileTilePosition, missTile);
        }
        checkScore(score, miss);
    }


    void checkScore(int score, int miss)
    {
        //controllo del punteggio
        if (score == y)
        {
            OutroText.text = "Congratulation, you win!";
            Retry.gameObject.SetActive(false);
            OutroCanvas.enabled = true;
            Debug.Log("Hai vinto!");
            win = true;
        }
        Debug.Log("miss: "+ miss.ToString());

        if (miss == vite)
        {
            OutroText.text = "Oh no, you lose!";
            OutroCanvas.enabled = true;
            Debug.Log("Hai perso");
            lose = true;
        }
    }
}
