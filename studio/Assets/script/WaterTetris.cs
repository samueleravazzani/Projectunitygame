using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class WaterTetris : MonoBehaviour
{
    public Sprite[] objects;
    private int N_objects = 30;
    public GameObject obj_prefab;
    public Transform[,] grid = new Transform[TetrisBlock.width, TetrisBlock.height]; // per creare/gestire i blocchi
    public string[,] grid_string = new string[TetrisBlock.width, TetrisBlock.height];
    public string[,] grid_string_houses = new string[TetrisBlock.width, TetrisBlock.height];
    public GameObject[] houses;
    // a sx crivo le quadre e la virgola per dire che è una matrice
    // a dx scrivo le dimensioni della matrice
    private int probability_threshold = 80;
    private List<Vector3Int> nextwaterPositions = new List<Vector3Int>();
    public GameObject water_prefab; // prefab da inserire
    
    private int N_water;   /* PARAMETRIZATION!!!!!!!!!!*/
    private float calibratioNwater = 20 / 9f, minN = 30; 
    
    private int max_water_spawn_width, max_water_spawn_height = TetrisBlock.height; /* PARAMETRIZATION della width!!!!!!!!!!*/
    private float calibrationwaterspawn = 7 / 9f, min = 15;
    
    private int min_x_houses, max_x_houses = TetrisBlock.width; /* PARAMETRIZATION!!!!!!!!!!*/
    public float calibrationhouses = -10 / 9f;
    public int yminhouses = 35;
    
    public bool ingame;
    public ParticleSystem rain;
    public Image success;
    public Image fail;

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
        N_water = Mathf.RoundToInt(GameManager.instance.climate_change_skept * calibratioNwater +
            minN - calibratioNwater);
        max_water_spawn_width = Mathf.RoundToInt(GameManager.instance.climate_change_skept * calibrationwaterspawn +
            min - calibrationwaterspawn);
        min_x_houses = Mathf.RoundToInt(GameManager.instance.climate_change_skept * calibrationhouses +
            yminhouses - calibrationhouses);
        
        success.gameObject.SetActive(false);
        fail.gameObject.SetActive(false);
        InitializeGrid(grid_string);
        InitializeGrid(grid_string_houses);
        SetHouses();
        SetObjects();
        rain.gameObject.SetActive(true);
    }
    
    public void StartGame(){
        rain.gameObject.SetActive(false);
        for (int i = 0; i < N_water; i++)
        {
            CreateNewWater();
        }
        ingame = true;
        SpawnTetrominos.instance.NewTetromino();
    }

    private void InitializeGrid(string[,] grid_to_initialize)
    {
        for (int i = 0; i < TetrisBlock.width; i++)
        {
            for (int j = 0; j < TetrisBlock.height; j++)
            {
                grid_to_initialize[i, j] = "";
            }
        }

    }

    private void SetHouses()
    {
        // red house
        int x_red = Random.Range(min_x_houses, max_x_houses);
        houses[0].transform.position = new Vector3Int(x_red, 16, 0);
        AddToGrid(houses[0].transform);
        
        // bordeaux house
        int x_bordeaux = Random.Range(min_x_houses, max_x_houses);
        houses[1].transform.position = new Vector3Int(x_bordeaux, 8, 0);
        AddToGrid(houses[1].transform);
        
        // blue house
        int x_blue = Random.Range(min_x_houses, max_x_houses);
        houses[2].transform.position = new Vector3Int(x_blue, 0, 0);
        AddToGrid(houses[2].transform);
    }
    
    void AddToGrid(Transform subtransform)
    {
        foreach (Transform children in subtransform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            grid[roundedX, roundedY] = children; // popolo la griglia
            grid_string_houses[roundedX, roundedY] = "house";
        }
    }

    public void SetObjects()
    {
        for (int i = 0; i < N_objects; i++)
        {
            GameObject obj = Instantiate(obj_prefab, new Vector3(Random.Range(0, max_water_spawn_width), Random.Range(0, TetrisBlock.height), 0), Quaternion.identity);
            obj.GetComponent<SpriteRenderer>().sprite = objects[Random.Range(0, objects.Length)];
        }
    }

    public void ReLoadScene()
    {
        fail.gameObject.SetActive(false);
        SceneManager.LoadScene("Water NEW");
    }

    public void HideFailSuccess()
    {
        fail.gameObject.SetActive(false);
        success.gameObject.SetActive(false);
    }
    

    // /!\ gestisco l'espansione dell'acqua in TetrisBlock, dal blocco che è appena stato disabilitato
    

    public void ExpandWater()
    {
        // Trova tutte le caselle adiacenti all'acqua che non sono ancora acqua
        GetAdjacentPositions(); // riempie nextWaterPosition

        // SUCCESS
        if (!nextwaterPositions.Any())
        {
            ingame = false;
            success.gameObject.SetActive(true);
        }

        // Cambia le caselle adiacenti in "tile_water"
        foreach (Vector3Int pos in nextwaterPositions)
        {
            // probability
            int probability = Random.Range(0, 100);
            if (probability <= probability_threshold)
            {
                ConvertToWater(pos);
                // GAMEOVER  !!!!!!!!!!
                if (grid_string_houses[pos.x, pos.y] == "house")
                {
                    ingame = false;
                    fail.gameObject.SetActive(true);
                    rain.gameObject.SetActive(true);
                    Debug.Log("Game Over");
                    return;
                } 
            }
        }
        nextwaterPositions.Clear();
    
    }

    private void CreateNewWater()
    {
        // Cambia una casella casuale in "tile_water"
        Vector3 vv = GetRandomTerrainPositionInASubGrid();
        ConvertToWater(vv);
    }

    private void ConvertToWater(Vector3 vv)
    {
        var obj = new GameObject();
        obj.transform.position = new Vector3(vv.x, vv.y, 0);
        grid[Mathf.RoundToInt(vv.x), Mathf.RoundToInt(vv.y)] = obj.transform;
        Destroy(obj);
        grid_string[Mathf.RoundToInt(vv.x), Mathf.RoundToInt(vv.y)] = "water";
        Instantiate(water_prefab, new Vector3(Mathf.RoundToInt(vv.x), Mathf.RoundToInt(vv.y), 0), Quaternion.identity);
        
    }

    private Vector3 GetRandomTerrainPositionInASubGrid()
    {
        // Scegli una casella casuale
        int randomX = Random.Range(0, max_water_spawn_width); // perché Random.Range con int int esclude l'ultimo
        int randomY = Random.Range(0, max_water_spawn_height);
        Vector3 pos = new Vector3(randomX, randomY, 0);
        // Debug.Log(pos);
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
                    if (j < TetrisBlock.height-1)
                    {
                        // controllo sopra se non sto guardando il blocco più in alto
                        if (grid_string[i, j+1] != "water" && grid_string[i, j+1] != "block")
                        {
                            Vector3Int up = new Vector3Int(i, j+1, 0);
                            nextwaterPositions.Add(up);
                        }
                    }
                    
                    // check down
                    if (j > 0)
                    {
                        if (grid_string[i, j-1] != "water" && grid_string[i, j-1] != "block")
                        {
                            Vector3Int down = new Vector3Int(i, j-1, 0);
                            nextwaterPositions.Add(down);
                        }
                    }

                    // check right
                    if (i<TetrisBlock.width-1)
                    {
                        if (grid_string[i+1,j] != "water" && grid_string[i+1, j] != "block")
                        {
                            Vector3Int right = new Vector3Int(i+1, j, 0);
                            nextwaterPositions.Add(right);
                        }
                    }

                    // check left
                    if (i > 0)
                    {
                        if (grid_string[i-1, j] != "water" && grid_string[i-1, j] != "block")
                        {
                            Vector3Int left = new Vector3Int(i-1, j, 0);
                            nextwaterPositions.Add(left);
                        }
                    }
                }
            }
        }
    }
}
