using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dontdestroy : MonoBehaviour
{
    public static Dontdestroy instance;
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

    void Update()
    {
        if (SceneManager.GetActiveScene().name != "MainMap")
        {
            instance.gameObject.SetActive(false);
        }
        if(SceneManager.GetActiveScene().name != "MainMap")
        {
            instance.gameObject.SetActive(true);
            Debug.Log("Dovrei essere active");
        }
    }
}
