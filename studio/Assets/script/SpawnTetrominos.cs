using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTetrominos : MonoBehaviour
{
    public GameObject[] Tetrominoes;

    public static SpawnTetrominos instance;
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
    
        instance = this;
    }

    public void NewTetromino()
    {
        Instantiate(Tetrominoes[Random.Range(0, Tetrominoes.Length - 1)], transform.position, Quaternion.identity);
    }
}
