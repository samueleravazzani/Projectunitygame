using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WaterGameManagerNew : MonoBehaviour
{
    public Tilemap tilemap_water;
    public Tile tile_terrain;
    public Tile tile_house;
    public Tile tile_water;
    public Tile tile_fence;
    private Vector3Int housePosition;
    private List<Vector3Int> waterPositions = new List<Vector3Int>();
    private List<Vector3Int> nextwaterPositions = new List<Vector3Int>();
    private int N_water = 4;
    public bool ingame;
    private bool startcoroutine;

    void Start()
    {
        ingame = true;
        startcoroutine = true;
    }

    void Update()
    {
        if (ingame)
        {
            if (startcoroutine)
            {
                // Espansione dell'acqua
                StartCoroutine(ExpandWater());
                startcoroutine = false;
            }
        }
        else
        {
            StopCoroutine(ExpandWater());
        }
    }

    IEnumerator ExpandWater()
    {
        while (ingame)
        {
            yield return new WaitForSeconds(1f);

            // Trova tutte le caselle adiacenti all'acqua che non sono ancora acqua
            GetAdjacentTiles(waterPositions);

            // Cambia le caselle adiacenti in "tile_water"
            foreach (Vector3Int pos in nextwaterPositions)
            {
                // probability
                int probability = Random.Range(0, 100);
                if (probability <= 50)
                {
                    if (tilemap_water.GetTile(pos) == tile_house)
                    {
                        ingame = false;
                        Debug.Log("Game Over");
                    }
                    ConvertToWater(pos);
                }
            }
            nextwaterPositions.Clear();
        }
    }

    private void CreateNewWater()
    {
        // Cambia una casella casuale in "tile_water"
        Vector3Int vv = GetRandomTerrainTile();
        ConvertToWater(vv);
    }

    private void ConvertToWater(Vector3Int vv)
    {
        waterPositions.Add(vv);
        tilemap_water.SetTile(vv, tile_water);
    }

    private Vector3Int GetRandomTerrainTile()
    {
        // Ottieni tutte le caselle di terreno
        List<Vector3Int> terrainTiles = new List<Vector3Int>();

        for (int n = tilemap_water.cellBounds.xMin; n < tilemap_water.cellBounds.xMax; n++)
        {
            for (int p = tilemap_water.cellBounds.yMin; p < tilemap_water.cellBounds.yMax; p++)
            {
                Vector3Int localPlace = (new Vector3Int(n, p, (int)tilemap_water.transform.position.y));
                Vector3 place = tilemap_water.CellToWorld(localPlace);
                if (tilemap_water.HasTile(localPlace))
                {
                    if (tilemap_water.GetTile(localPlace) == tile_terrain)
                    {
                        terrainTiles.Add(localPlace);
                    }
                }
            }
        }

        // Scegli una casella casuale
        int randomIndex = Random.Range(0, terrainTiles.Count);
        return terrainTiles[randomIndex];
    }

    private void GetAdjacentTiles(List<Vector3Int> water)
    {
        foreach (Vector3Int el in water)
        {
           // check right
            Vector3Int right = el + new Vector3Int(1, 0, 0);
            if(tilemap_water.GetTile(right) != tile_water && tilemap_water.GetTile(right) !=tile_fence)
                nextwaterPositions.Add(right);
        }

    }
}
