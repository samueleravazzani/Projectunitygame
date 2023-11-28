using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//QUESTO SCRIPT IN REALTA' DEVE ESSERE IMPLEMENTATO NEL GAMEMANAGER PER GESTIRE GLI SLIDET 

public class Player : MonoBehaviour
{
    public float maxAnxiety = 10.0f;
    public float currentAnxiety; //Questo in realtà è il valore che viene preso dagli slider nella prima scena all'inizio
    public Anxietybar anxietyBar;
    public float maxLiteracy = 10.0f;
    public float currentLiteracy; //Questo in realtà è il valore che viene preso dagli slider nella prima scena all'inizio
    public Literacybar literacyBar;
    public float maxClimate = 10.0f;
    public float currentClimate; //Questo in realtà è il valore che viene preso dagli slider nella prima scena all'inizio
    public Climatebar climateBar;
    
    // Start is called before the first frame update
    void Start()
    {
        currentAnxiety = maxAnxiety; //In realtà all'inizio deve essere settato al valore dell'ansia preso dagli slider 
        anxietyBar.SetMaxAnxiety(maxAnxiety);
        anxietyBar.SetAnxiety(currentAnxiety);
        currentLiteracy = maxLiteracy; //In realtà all'inizio deve essere settato al valore dell'ansia preso dagli slider 
        literacyBar.SetMaxLiteracy(maxLiteracy);
        literacyBar.SetLiteracy(currentLiteracy);
        currentClimate = maxClimate; //In realtà all'inizio deve essere settato al valore dell'ansia preso dagli slider 
        climateBar.SetMaxClimate(maxClimate);
        climateBar.SetClimate(currentClimate);
    }

    // Update is called once per frame
    void Update()
    {
        //QUESTA IN REALTA' E' TUTTA LA LOGICA CHE DOVRA' ESSERE GESTITA CON LE TASK
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamageAnxiety(1.0f);
            TakeDamageLiteracy(2.0f);
            TakeDamageClimate(3.0f);
        }
        if (Input.GetKeyDown(KeyCode.Return)) // KeyCode.Return represents the Enter key
        {
            TakeIncrementAnxiety(1.0f);
            TakeIncrementLiteracy(2.0f);
            TakeIncrementClimate(3.0f);
        }
    }

    //DI SEGUITO INVECE QUESTE FUNZIONI POSSO ESSERE PRESE COSI' COME SONO
    
    void TakeDamageAnxiety(float damageanxiety)
    {
        currentAnxiety -= damageanxiety;
        if (currentAnxiety <= 1f)
        {
            currentAnxiety = 1f; 
        }
        anxietyBar.SetAnxiety(currentAnxiety);
    }
    
    void TakeDamageLiteracy(float damageliteracy)
    {
        currentLiteracy -= damageliteracy;
        if (currentLiteracy <= 1f)
        {
            currentLiteracy = 1f; 
        }
        literacyBar.SetLiteracy(currentLiteracy);
    }
    
    void TakeDamageClimate(float damageclimate)
    {
        currentClimate -= damageclimate;
        if (currentClimate <= 1f)
        {
            currentClimate = 1f; 
        }
        climateBar.SetClimate(currentClimate);
    }
    
    void TakeIncrementAnxiety(float incrementanxiety)
    {
        currentAnxiety += incrementanxiety;
        if (currentAnxiety >= 10f)
        {
            currentAnxiety = 10f; 
        }
        anxietyBar.SetAnxiety(currentAnxiety);
    }
    
    void TakeIncrementLiteracy(float incrementliteracy)
    {
        currentLiteracy += incrementliteracy;
        if (currentLiteracy >= 10f)
        {
            currentLiteracy = 10f; 
        }
        literacyBar.SetLiteracy(currentLiteracy);
    }
    
    void TakeIncrementClimate(float incrementclimate)
    {
        currentClimate += incrementclimate;
        if (currentClimate >= 10f)
        {
            currentClimate = 10f; 
        }
        climateBar.SetClimate(currentClimate);
    }
}
