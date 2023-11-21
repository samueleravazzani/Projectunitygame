using System.Collections;
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
            StartCoroutine(ExpandWaterRoutine());
        }

        if (Input.GetMouseButtonDown(0))
        {
            ConvertTerrainToFence();
        }
    }

    void InitializeGame()
    {
        SetRandomHouseTiles();
        SetInitialWaterTile();

        gameStarted = true;
    }

    void SetRandomHouseTiles()
    {
        int numberOfHouseTiles = 2; // Change this to the desired number
        for (int i = 0; i < numberOfHouseTiles; i++)
        {
            Vector3Int randomPosition = GetRandomTerrainPosition();
            terrainTilemap.SetTile(randomPosition, tileHouse);
        }
    }

    void SetInitialWaterTile()
    {
        Vector3Int randomPosition = GetRandomTerrainPosition();
        terrainTilemap.SetTile(randomPosition, tileWater);
    }

    Vector3Int GetRandomTerrainPosition()
    {
        Vector3Int randomPosition;
        do
        {
            randomPosition = new Vector3Int(
                Random.Range(terrainTilemap.cellBounds.x, terrainTilemap.cellBounds.xMax),
                Random.Range(terrainTilemap.cellBounds.y, terrainTilemap.cellBounds.yMax),
                0);
        } while (terrainTilemap.GetTile(randomPosition) != tileTerrain);

        return randomPosition;
    }

    IEnumerator ExpandWaterRoutine()
    {
        yield return new WaitForSeconds(3f);

        Vector3Int waterPosition = GetWaterPosition();
        ExpandWater(waterPosition);

        yield return null;
    }

    void ExpandWater(Vector3Int waterPosition)
    {
        Vector3Int[] adjacentTiles = new Vector3Int[]
        {
            waterPosition + new Vector3Int(0, 1, 0), // Up
            waterPosition + new Vector3Int(0, -1, 0), // Down
            waterPosition + new Vector3Int(1, 0, 0), // Right
            waterPosition + new Vector3Int(-1, 0, 0) // Left
        };

        foreach (Vector3Int adjacentTile in adjacentTiles)
        {
            TileBase tile = terrainTilemap.GetTile(adjacentTile);
            if (tile == tileTerrain || tile == tileHouse)
            {
                terrainTilemap.SetTile(adjacentTile, tileWater);
            }
        }
    }

    Vector3Int GetWaterPosition()
    {
        foreach (Vector3Int position in terrainTilemap.cellBounds.allPositionsWithin)
        {
            if (terrainTilemap.GetTile(position) == tileWater)
            {
                return position;
            }
        }

        return Vector3Int.zero;
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
            GameOver();
        }
    }

    void GameOver()
    {
        gameStarted = false;
        Debug.Log("Game Over!");
    }
}
