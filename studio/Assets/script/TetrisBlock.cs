using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    public Vector3 rotationPoint;
    private float previousTime;
    public float fallTime = 0.8f;
    public static int height = 20;
    public static int width = 27;
    private static Transform[,] grid = new Transform[width, height]; // static: makes the value to be the same among all tetrominoes
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow)) // move down
        {
            transform.position += new Vector3(0, -1, 0);
            if (!ValidMove()) // if it is not valid: revert the movement
            {
                transform.position -= new Vector3(0, -1, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow)) // move up
        {
            transform.position += new Vector3(0, 1, 0);
            if (!ValidMove()) // if it is not valid: revert the movement
            {
                transform.position -= new Vector3(0, 1, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            // rotate
            // /!\ transform.TransformPoint(point) changes local position to global position
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0,0,1), 90);
            if (!ValidMove()) // if it is not valid: revert the movement
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0,0,1), -90);
            }
        }

        // each time diff. current time and previous time > falltime -> make it fall
        // /!\ VERY INTERESTING SHORTCUT 
        // if we press left key -> return fallTime/10 otherwise we return fallTime
        if (Time.time - previousTime > (Input.GetKeyDown(KeyCode.LeftArrow) ? fallTime/10:fallTime))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!ValidMove()) // if it is not valid: revert the movement
            {
                transform.position -= new Vector3(-1, 0, 0);
                AddToGrid();
                CheckForLines();

                WaterGameManagerNew.instance.ExpandWater();
                this.enabled = false; // /!\ disabilita lo script
                FindObjectOfType<SpawnTetrominos>().NewTetromino();  // cerco l'oggetto di tipo SpawnTetrominos -> ne chiao la funzione
            }
            previousTime = Time.time;
        }
    }

    void AddToGrid()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            grid[roundedX, roundedY] = children; // popolo la griglia
        }
    }

    void CheckForLines()
    {
        for (int i = width-1; i >= 0; i--) // IMPORTANTE: da qui invertite righe e colonne rispetto al video, e width e height
        {
            if (HasLine(i))
            {
                DeleteLine(i);
                RowLeft(i);
            }
        }
    }

    bool HasLine(int i) 
    {
        for (int j = 0; j < height; j++)
        {
            if (grid[i, j] == null)
                return false;
        }

        return true;
    }

    void DeleteLine(int i)
    {
        for (int j = 0; j < height; j++)
        {
            Destroy(grid[i,j].gameObject);
            grid[i, j] = null;
        }
    }

    void RowLeft(int i)
    {
        for (int y = i; y < width; y++)
        {
            for (int j = 0; j > height; j++)
            {
                if (grid[y, j] != null)
                {
                    grid[y-1, j] = grid[y, j];
                    grid[y, j] = null;
                    grid[y-1, j].transform.position -= new Vector3(1, 0, 0);
                }
            }
        }
    }

    bool ValidMove() // check if the position of every square of the form is inside of the grid -> return true or false
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);
            /*Debug.Log("X: " + roundedX);
            Debug.Log("Y: " + roundedY);
            Debug.Log(roundedX<0);
            Debug.Log(roundedY<0);
            Debug.Log(roundedX>width);
            Debug.Log(roundedY>height);  */

            if (roundedX < 0 || roundedX >= width || roundedY<0 || roundedY>=height)
            {
                return false;
            }

            if (grid[roundedX, roundedY] != null)
            {
                return false;
            }
        }

        return true;
    }
}
