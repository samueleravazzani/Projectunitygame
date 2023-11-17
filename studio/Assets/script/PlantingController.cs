using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using TMPro;

public class PlantingController : MonoBehaviour
{
    public Tilemap groundTilemap; // Reference to the ground Tilemap
    public Tilemap plantTilemap;
    public Tile nonFertileTile;   // Non-fertile tile
    public Tile fertileTile;       // Fertile tile
    [SerializeField] public Tile[] plantTile;  // Plant tile
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI missText;

    public float fertileInterval = 2f; // Time interval to make a tile fertile

    private Vector3Int fertileTilePosition; // Position of the current fertile tile
    private bool ok = false;
    private bool firstDone = false;
    int score = 1;
    private int miss = 1;

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
                if (groundTilemap.GetTile(position) == nonFertileTile)
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

            // Set the tile to be fertile
            groundTilemap.SetTile(fertileTilePosition, fertileTile);

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
                if (clickedTilePosition == fertileTilePosition)
                {
                    int randomIndex = Random.Range(0, plantTile.Length);
                    plantTilemap.SetTile(fertileTilePosition, plantTile[randomIndex]);
                    scoreText.text=string.Format("Plant:{00}",score++);
                }
                else
                {
                    missText.text=string.Format("Miss:{00}",miss++);
                    
                }

                // Exit the coroutine
                yield break;
            }

            yield return null;
        }

        // If the player didn't click within the time frame, reset the tile to non-fertile
        //groundTilemap.SetTile(fertileTilePosition, nonFertileTile);
        missText.text=string.Format("Miss:{00}",miss++);
    }
}
