using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Initializeslider : MonoBehaviour
{
    
    //DICHIARO CHI SONO GLI SLIDER 
    public Slider anxietySlider;
    public Slider climateSlider;
    public Slider literacySlider;

    public float max = 10f;

    public bool start = false; 
    
    private void Start()
    {
        InitializeSliders();  
        Debug.Log("Enter: " + GameManager.instance.climate_change_skept);
       
    }

    //FUNZIONE DI INIZIALIZZAZIONE DEI TRE SLIDER, DEVE ESSERE PER OGNUNO DEI TRE CON I PROPRI VALORI DAL GAMEMANAGER
    //L'INIZIALIZZAZIONE COMPRENDE UNA FUNZIONE PER INVERTIRE I PARAMETRI + QUELLA DI SETTAGGIO DELLO SLIDER
    private void InitializeSliders()
    {
        SetSliderValue(anxietySlider, InverseSliderValue(GameManager.instance.oldanxiety));
        SetSliderValue(climateSlider, InverseSliderValue(GameManager.instance.oldclimate));
        SetSliderValue(literacySlider, InverseSliderValue(GameManager.instance.oldliteracy));
    }

    //INVERSIONE
    private float InverseSliderValue(float value)
    {
        return max - value + 1;
    }

    //SETTAGGIO
    private void SetSliderValue(Slider slider, float value)
    {
        slider.value = Mathf.Clamp(value, slider.minValue, slider.maxValue);
    }

    public void FixedUpdate()
    {
        UpdateSliders();
    }
    
   public void UpdateSliders()
    {
       /* Debug.Log("StartScene");
        Debug.Log("actualclimate: " + GameManager.instance.climate_change_skept.ToString());
        Debug.Log("oldclimate: " + GameManager.instance.oldclimate.ToString());
        Debug.Log("sliderclimate before: " + climateSlider.value);*/
       
        
        if (max-GameManager.instance.anxiety+1 !=anxietySlider.value)
        {
            anxietySlider.value = Mathf.MoveTowards(anxietySlider.value, max-GameManager.instance.anxiety +1, 1 / 50f);
        }
        if (max-GameManager.instance.literacy_inverted+1!=literacySlider.value)
        {
            literacySlider.value = Mathf.MoveTowards(literacySlider.value, max-GameManager.instance.literacy_inverted + 1, 1 / 50f);
        }
        if (max-GameManager.instance.climate_change_skept+1!=climateSlider.value)
        {
            climateSlider.value = Mathf.MoveTowards(climateSlider.value, max-GameManager.instance.climate_change_skept + 1, 1 / 50f);
           // Debug.Log("sliderclimate after: " + climateSlider.value);
        }

        GameManager.instance.oldclimate = GameManager.instance.climate_change_skept;
        GameManager.instance.oldanxiety = GameManager.instance.anxiety;
        GameManager.instance.oldliteracy = GameManager.instance.literacy_inverted;
    }
   
}
