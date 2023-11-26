using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WaterTetris : MonoBehaviour
{
    public Transform[,] grid = new Transform[TetrisBlock.width, TetrisBlock.height]; // per creare/gestire i blocchi
    public string[,] grid_string = new string[TetrisBlock.width, TetrisBlock.height];
    // a sx crivo le quadre e la virgola per dire che è una matrice
    // a dx scrivo le dimensioni della matrice
    
    private List<Vector3Int> nextwaterPositions = new List<Vector3Int>();
    public GameObject water_prefab; // prefab da inserire
    private int N_water = 4;
    private int max_water_spawn_width = 5, max_water_spawn_height = TetrisBlock.height;
    public bool ingame;

    public static WaterTetris instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
    
        instance = this;
    }

    void Start()
    {
        for (int i = 0; i < N_water; i++)
        {
            CreateNewWater();
        }
        ingame = true;
        SpawnTetrominos.instance.NewTetromino();
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
        GetAdjacentPositions();

        // Cambia le caselle adiacenti in "tile_water"
        foreach (Vector3Int pos in nextwaterPositions)
        {
            // probability
            int probability = Random.Range(0, 100);
            if (probability <= 50)
            {
                ConvertToWater(pos);
                /* GAMEOVER
                if (tilemap_water.GetTile(pos) == tile_house)
                {
                    ingame = false;
                    Debug.Log("Game Over");
                } */
            }
        }
        nextwaterPositions.Clear();
    
    }

    private void CreateNewWater()
    {
        // Cambia una casella casuale in "tile_water"
        Vector3Int vv = GetRandomTerrainPositionInASubGrid();
        ConvertToWater(vv);
    }

    private void ConvertToWater(Vector3Int vv)
    {
        var obj = new GameObject();
        obj.transform.position = new Vector3(vv.x, vv.y, 0);
        grid[vv.x, vv.y] = obj.transform;
        Destroy(obj);
        grid_string[vv.x, vv.y] = "water";
        Instantiate(water_prefab, new Vector3(vv.x, vv.y, 0), Quaternion.identity);
        
    }

    private Vector3Int GetRandomTerrainPositionInASubGrid()
    {
        // Scegli una casella casuale
        int randomX = Random.Range(0, max_water_spawn_width);
        int randomY = Random.Range(0, max_water_spawn_height);
        Vector3Int pos = new Vector3Int(randomX, randomY, 0);
        Debug.Log(pos);
        return pos;
    }

    

    private void GetAdjacentPositions()
    {
        
        for (int i = 0; i < TetrisBlock.width; i++) // scorro le righe
        {
            for (int j = 0; j < TetrisBlock.height; j++)  // scorro le colonne
            {
                if (grid_string[i,j] == "water")
                {
                    /*Debug.Log("i: " + i.ToString());
                    Debug.Log("j: " + j.ToString());
                    Debug.Log("grid_string: " + TetrisBlock.width.ToString() + TetrisBlock.height.ToString());
                    */
                    
                    // check up
                    if (i > 0)
                    {
                        // controllo sopra se non sto guardando il blocco più in alto
                        if (grid_string[i - 1, j] != "water" && grid_string[i - 1, j] != "block")
                        {
                            Vector3Int up = new Vector3Int(i - 1, j, 0);
                            nextwaterPositions.Add(up);
                        }
                    }

                    if (i < TetrisBlock.height-1)
                    {
                        // check down
                        if (grid_string[i + 1, j] != "water" && grid_string[i + 1, j] != "block")
                        {
                            Vector3Int down = new Vector3Int(i + 1, j, 0);
                            nextwaterPositions.Add(down);
                        }
                    }

                    if (j < TetrisBlock.width)
                    {
                        // check right
                        if (grid_string[i, j + 1] != "water" && grid_string[i, j + 1] != "block")
                        {
                            Vector3Int right = new Vector3Int(i, j + 1, 0);
                            nextwaterPositions.Add(right);
                        }
                    }

                    if (j > 0)
                    {
                        // check left
                        if (grid_string[i, j - 1] != "water" && grid_string[i, j - 1] != "block")
                        {
                            Vector3Int left = new Vector3Int(i, j - 1, 0);
                            nextwaterPositions.Add(left);
                        }
                    }
                }
            }
        }
    }
}
