using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using TMPro;
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

    public float fertileInterval = 2f; // Time interval to make a tile fertile

    private Vector3Int fertileTilePosition; // Position of the current fertile tile
    private bool ok = false;
    private bool firstDone = false;
    int score = 1;
    private int miss = 1;
    private int randomTile; //sceglie se compare una tile fertile oppure una talpa
    private int randomTalpa;
    private bool talpaIsPresent;
    private Vector3Int CentreTilePosition;


    private void Start()
    {
        
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
        while (true)
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
                if (groundTilemap.GetTile(position) == nonFertileTile || groundTilemap.GetTile(position) == missTile)
                {
                    nonFertileTiles.Add(position);
                }
            }
        }

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
                    scoreText.text=string.Format("Plant:{00}",score++);
                }
                else if(clickedTilePosition == fertileTilePosition && talpaIsPresent)

                {//se clicca sulla tile fertile e c'è la talpa
                    missText.text=string.Format("Miss:{00}",miss++);
                    talpaIsPresent = false;
                    Destroy(Talpa);
                    groundTilemap.SetTile(fertileTilePosition, missTile);
                }
                else if(talpaIsPresent)

                { //se non clicca su una tile fertile ma era comparsa la talpa
                    missText.text=string.Format("Miss:{00}",miss++);
                    talpaIsPresent = false;
                    Destroy(Talpa);
                    groundTilemap.SetTile(fertileTilePosition, missTile);
                }
                else
                {//se clicca non su una tile fertile
                    missText.text=string.Format("Miss:{00}",miss++);
                    groundTilemap.SetTile(fertileTilePosition, missTile);

                }

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
            scoreText.text=string.Format("Plant:{00}",score++);
            
        }
        else
        {
            missText.text = string.Format("Miss:{00}", miss++);
            groundTilemap.SetTile(fertileTilePosition, missTile);
        }
    }
}
