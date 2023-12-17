using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;
using Image = UnityEngine.UI.Image;

public class MainMapTutorial : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject IconBever;
    private static MainMapTutorial instance;
    
    private void Awake() //Creazione del Singleton
    {
        if (instance != null)
        {
            Debug.LogWarning("find more than one dialogue Manager in the scene");
        }
        instance = this;
    }

    public static MainMapTutorial GetInstance()
    {
        return instance;
    }
    
    
    void Start()
    {
        IconBever.gameObject.SetActive(true);
        Cursor.visible  = true;
    }

    public void talked()
    {
        IconBever.gameObject.SetActive(false);
    }
   
    
}
