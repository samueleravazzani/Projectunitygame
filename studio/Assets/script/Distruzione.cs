using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distruzione : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(GameObject.Find("player"));
        Destroy(GameObject.Find("Virtual Camera"));
        Destroy(GameObject.Find("GameManager"));
        Destroy(GameObject.Find("SceneMaster"));
        Destroy(GameObject.Find("Bever"));
    }

    
}
