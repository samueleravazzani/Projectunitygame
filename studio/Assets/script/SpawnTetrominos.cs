using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTetrominos : MonoBehaviour
{
    public GameObject[] Tetrominoes;
    // Start is called before the first frame update
    void Start()
    {
        NewTetromino();
    }

    public void NewTetromino()
    {
        Instantiate(Tetrominoes[Random.Range(0, Tetrominoes.Length - 1)], transform.position, Quaternion.identity);
    }
}
