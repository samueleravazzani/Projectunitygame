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
    

    void Start()
    {
        for (int i = 0; i < N_water; i++)
        {
            CreateNewWater();
        }
        
        ingame = true;
        
    }
    
    public static WaterGameManagerNew instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
    
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    // /!\ gestisco l'espansione dell'acqua in TetrisBlock, dal blocco che è appena stato disabilitato
    void Update()
    {
        
        /*
        if (ingame)
        {
            // Espansione dell'acqua
            ExpandWater();
        } */
        
    }

    public void ExpandWater()
    {
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

    private void CreateNewWater()
    {
        // Cambia una casella casuale in "tile_water"
        Vector3Int vv = GetRandomTerrainTileInALine();
        ConvertToWater(vv);
    }

    private void ConvertToWater(Vector3Int vv)
    {
        waterPositions.Add(vv);
        tilemap_water.SetTile(vv, tile_water);
    }

    private Vector3Int GetRandomTerrainTileInALine()
    {
        // Ottieni tutte le caselle di terreno una linea
        List<Vector3Int> terrainTiles = new List<Vector3Int>();

    
        for (int p = 0; p < TetrisBlock.height; p++)
        {
            Vector3Int localPlace = (new Vector3Int(0, p, 0));
            Vector3 place = tilemap_water.CellToWorld(localPlace);
            if (tilemap_water.HasTile(localPlace))
            {
                if (tilemap_water.GetTile(localPlace) == tile_terrain)
                {
                    terrainTiles.Add(localPlace);
                }
            }
        }
    

        // Scegli una casella casuale
        int randomIndex = Random.Range(0, terrainTiles.Count);
        return terrainTiles[randomIndex];
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
            // check up
            Vector3Int up = el + new Vector3Int(0, 1, 0);
            if(tilemap_water.GetTile(up) != tile_water && tilemap_water.GetTile(up) !=tile_fence)
                nextwaterPositions.Add(up);
            
            // check down
            Vector3Int down = el + new Vector3Int(0, -1, 0);
            if(tilemap_water.GetTile(down) != tile_water && tilemap_water.GetTile(down) !=tile_fence)
                nextwaterPositions.Add(down);
            
            // check right
            Vector3Int right = el + new Vector3Int(1, 0, 0);
            if(tilemap_water.GetTile(right) != tile_water && tilemap_water.GetTile(right) !=tile_fence)
                nextwaterPositions.Add(right);
            
            // check down
            Vector3Int left = el + new Vector3Int(-1, 0, 0);
            if(tilemap_water.GetTile(left) != tile_water && tilemap_water.GetTile(left) !=tile_fence)
                nextwaterPositions.Add(left);
        }

    }
}
