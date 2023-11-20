using UnityEngine;
using UnityEngine.Tilemaps;

public class WaterGameManager : MonoBehaviour
{
    public Tilemap terrainTilemap;
    
    public TileBase tileTerrain;
    public TileBase tileHouse;
    public TileBase tileWater;
    public TileBase tileFence;

    private bool gameStarted = false;

    void Start()
    {
        InitializeGame();
    }

    void Update()
    {
        if (gameStarted)
        {
            // Expand water every second
            ExpandWater();
        }
        else if (Input.GetMouseButtonDown(0))
        {
            // On mouse click, convert terrain to fence
            ConvertTerrainToFence();
        }
    }

    void InitializeGame()
    {
        // Set initial house positions
        SetRandomHousePositions();

        // Set initial water position
        SetInitialWaterPosition();

        // Start the game
        gameStarted = true;
    }

    void SetRandomHousePositions()
    {
        for (int i = 0; i < 5; i++)
        {
            Vector3Int randomPosition = new Vector3Int(
                Random.Range(-10, 10),
                Random.Range(-10, 10),
                0);

            terrainTilemap.SetTile(randomPosition, tileHouse);
        }
    }

    void SetInitialWaterPosition()
    {
        Vector3Int randomPosition = new Vector3Int(
            Random.Range(-10, 10),
            Random.Range(-10, 10),
            0);

        terrainTilemap.SetTile(randomPosition, tileWater);
    }

    void ExpandWater()
    {
        // Your logic to expand water tiles
        // You would need to track the water tiles and expand them accordingly
    }

    void ConvertTerrainToFence()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPosition = terrainTilemap.WorldToCell(mousePosition);

        TileBase terrainTile = terrainTilemap.GetTile(cellPosition);

        if (terrainTile == tileTerrain)
        {
            terrainTilemap.SetTile(cellPosition, tileFence);
        }
        else if (terrainTile == tileHouse)
        {
            // Player lost if a house tile becomes water
            GameOver();
        }
    }

    void GameOver()
    {
        // Your logic for handling game over
        gameStarted = false;
        Debug.Log("Game Over!");
    }
}
