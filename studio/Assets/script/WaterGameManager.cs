using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WaterGameManager : MonoBehaviour
{
    public Tilemap tilemap;
    public Tile tile_terrain;
    public Tile tile_house;
    public Tile tile_water;
    public Tile tile_fence;
    private Vector3Int housePosition;
    private List<Vector3Int> waterPositions = new List<Vector3Int>();
    private List<Vector3Int> nextwaterPositions = new List<Vector3Int>();
    private int N = 2;
    public bool ingame;

    void Start()
    {
        // Cambia N caselle casuali in "tile_house"
        for (int i = 0; i < N; i++)
        {
            housePosition = GetRandomTerrainTile();
            tilemap.SetTile(housePosition, tile_house);
        }

        
        CreateNewWater();
        ingame = true;
    }

    void Update()
    {
        if (ingame)
        {
            // Controlla l'input del mouse
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3Int clickPosition = tilemap.WorldToCell(mouseWorldPos);

                if (tilemap.GetTile(clickPosition) == tile_terrain)
                {
                    tilemap.SetTile(clickPosition, tile_fence);
                }
            }

            // Inizia la routine di espansione dell'acqua
            StartCoroutine(ExpandWater());
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
            yield return new WaitForSeconds(2);

            // Trova tutte le caselle adiacenti all'acqua che non sono ancora acqua
            GetAdjacentTiles(waterPositions);

            // Cambia le caselle adiacenti in "tile_water"
            foreach (Vector3Int pos in nextwaterPositions)
            {
                // probability
                int probability = Random.Range(0, 100);
                if (probability <= 50)
                {
                    if (tilemap.GetTile(pos) == tile_house)
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
        Debug.Log(vv.ToString());
        ConvertToWater(vv);
    }

    private void ConvertToWater(Vector3Int vv)
    {
        waterPositions.Add(vv);
        tilemap.SetTile(vv, tile_water);
    }

    private Vector3Int GetRandomTerrainTile()
    {
        // Ottieni tutte le caselle di terreno
        List<Vector3Int> terrainTiles = new List<Vector3Int>();

        for (int n = tilemap.cellBounds.xMin; n < tilemap.cellBounds.xMax; n++)
        {
            for (int p = tilemap.cellBounds.yMin; p < tilemap.cellBounds.yMax; p++)
            {
                Vector3Int localPlace = (new Vector3Int(n, p, (int)tilemap.transform.position.y));
                Vector3 place = tilemap.CellToWorld(localPlace);
                if (tilemap.HasTile(localPlace))
                {
                    if (tilemap.GetTile(localPlace) == tile_terrain)
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
            // check up
            Vector3Int up = el + new Vector3Int(0, 1, 0);
            if(tilemap.GetTile(up) != tile_water && tilemap.GetTile(up) !=tile_fence)
                nextwaterPositions.Add(up);
            
            // check down
            Vector3Int down = el + new Vector3Int(0, -1, 0);
            if(tilemap.GetTile(down) != tile_water && tilemap.GetTile(down) !=tile_fence)
                nextwaterPositions.Add(down);
            
            // check right
            Vector3Int right = el + new Vector3Int(1, 0, 0);
            if(tilemap.GetTile(right) != tile_water && tilemap.GetTile(right) !=tile_fence)
                nextwaterPositions.Add(right);
            
            // check down
            Vector3Int left = el + new Vector3Int(-1, 0, 0);
            if(tilemap.GetTile(left) != tile_water && tilemap.GetTile(left) !=tile_fence)
                nextwaterPositions.Add(left);
        }

    }
}
