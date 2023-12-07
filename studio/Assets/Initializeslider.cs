using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Initializeslider : MonoBehaviour
{
    //DICHIARO CHI SONO GLI SLIDER 
    public Slider anxietySlider;
    public Slider climateSlider;
    public Slider literacySlider;

    public float max = 10f; 
    
    private void Start()
    {
            InitializeSliders();
            StartCoroutine(UpdateSliders());
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
        return max - value;
    }

    //SETTAGGIO
    private void SetSliderValue(Slider slider, float value)
    {
        slider.value = Mathf.Clamp(value, slider.minValue, slider.maxValue);
    }
    
   IEnumerator UpdateSliders()
    {
        if (GameManager.instance.anxiety!=anxietySlider.value)
        {
            while (GameManager.instance.anxiety != anxietySlider.value)
            {
                anxietySlider.value = Mathf.MoveTowards(anxietySlider.value, GameManager.instance.anxiety, 1 / 50f);
                yield return null;
            }
        }
        if (GameManager.instance.literacy_inverted!=literacySlider.value)
        {
            while (GameManager.instance.literacy_inverted != literacySlider.value)
            {
                literacySlider.value = Mathf.MoveTowards(literacySlider.value, GameManager.instance.literacy_inverted, 1 / 50f);
                yield return null;
            }
        }
        if (GameManager.instance.climate_change_skept!=climateSlider.value)
        {
            while (GameManager.instance.anxiety != anxietySlider.value)
            {
                climateSlider.value = Mathf.MoveTowards(climateSlider.value, GameManager.instance.climate_change_skept, 1 / 50f);
                yield return null;
            }
        }
    }

    //QUANDO SARA' FINITA UNA TASK SE FATTA BENE ALLORA DEVO INCREMENTARE IL VALORE DELLO SLIDER 
    //QUESTA FUNZIONE VIENE CHIAMATA PASSANDO IL NOME DELLO SLIDER DA CAMBIARE
    //L'INREMENTO E' FISSATO A UNO COME AVEVAMO DECISO
    private void IncrementSliderValue(Slider slider)
   {
        slider.value = Mathf.Clamp(slider.value + 1, slider.minValue, slider.maxValue);
    }

    //ANALOGO PER GESTIRE IL CASO DI DECREMENT QUANDO PERDI UN GIOCO
    private void DecrementSliderValue(Slider slider)
    {
        slider.value = Mathf.Clamp(slider.value - 1, slider.minValue, slider.maxValue);
    } 
}
