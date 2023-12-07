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
    }

    //FUNZIONE DI INIZIALIZZAZIONE DEI TRE SLIDER, DEVE ESSERE PER OGNUNO DEI TRE CON I PROPRI VALORI DAL GAMEMANAGER
    //L'INIZIALIZZAZIONE COMPRENDE UNA FUNZIONE PER INVERTIRE I PARAMETRI + QUELLA DI SETTAGGIO DELLO SLIDER
    private void InitializeSliders()
    {
        SetSliderValue(anxietySlider, InverseSliderValue(GameManager.instance.anxiety));
        SetSliderValue(climateSlider, InverseSliderValue(GameManager.instance.climate_change_skept));
        SetSliderValue(literacySlider, InverseSliderValue(GameManager.instance.literacy_inverted));
        
        
        
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
    
   /* public void UpdateSliders()
    {
        if (GameManager.instance.anxiety>anxietySlider.value)
        {
            IncrementSliderValue(anxietySlider);
        }
        else if (GameManager.instance.anxiety<anxietySlider.value)
        {
            DecrementSliderValue(anxietySlider);
        }
        
        if (GameManager.instance.literacy_inverted>literacySlider.value)
        {
            IncrementSliderValue(literacySlider);
        }
        else if (GameManager.instance.literacy_inverted<literacySlider.value)
        {
            DecrementSliderValue(literacySlider);
        }
        
        if (GameManager.instance.climate_change_skept>climateSlider.value)
        {
            IncrementSliderValue(climateSlider);
        }
        else if (GameManager.instance.climate_change_skept<climateSlider.value)
        {
            DecrementSliderValue(climateSlider);
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
    } */
}
