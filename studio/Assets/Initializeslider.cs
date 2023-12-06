using UnityEngine;
using UnityEngine.UI;

public class Initializeslider : MonoBehaviour
{
    //DICHIARO CHI SONO GLI SLIDER 
    public Slider anxietySlider;
    public Slider climateSlider;
    public Slider literacySlider;

    //VARIABILE MESSA COSI ADESSO PER PROVARE IN REALTA' SARA' SOSTITUITA DA GAMEMANAGER.INSTANCE.ANXIETY... PER OGNUNA DI LORO 
    private int initialValue = 10;

    private void Start()
    {
        InitializeSliders(); //ALLO START VENGONO INIZIALIZZATI GL SLIDER 
    }

    //FUNZIONE DI INIZIALIZZAZIONE DEI TRE SLIDER, DEVE ESSERE PER OGNUNO DEI TRE CON I PROPRI VALORI DAL GAMEMANAGER
    //L'INIZIALIZZAZIONE COMPRENDE UNA FUNZIONE PER INVERTIRE I PARAMETRI + QUELLA DI SETTAGGIO DELLO SLIDER
    private void InitializeSliders()
    {
        SetSliderValue(anxietySlider, InverseSliderValue(initialValue));
        SetSliderValue(climateSlider, InverseSliderValue(initialValue - 1));
        SetSliderValue(literacySlider, InverseSliderValue(initialValue - 2));
    }

    //INVERSIONE
    private float InverseSliderValue(int value)
    {
        return anxietySlider.maxValue - value + anxietySlider.minValue;
    }

    //SETTAGGIO
    private void SetSliderValue(Slider slider, float value)
    {
        slider.value = Mathf.Clamp(value, slider.minValue, slider.maxValue);
    }

    //QUESTA UPDATE E' LA FUNZIONE CHE DEVE ESSERE GESTITA O IN UN GAMEMANAGER O NEI GIOCHI, DA CAPIRE!
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            IncrementSliderValue(anxietySlider);
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            DecrementSliderValue(anxietySlider);
        }
    }

    //QUANDO SARA' FINITA UNA TASK SE FATTA BENE ALLORA DEVO INCREMENTARE IL VALORE DELLO SLIDER 
    //QUESTA FUNZIONE VIENE CHIAMATA PASSANDO IL NOME DELLO SLIDER DA CAMBIARE
    //L'INREMENTO E' FISSATO A UNO COME AVEVAMO DECISO
    //ALTRIMENTI SI PUO' GENERALIZZARE PASSANDO UN INT DI CHANGE E AL POSTO DI 1 METTERE CHANGE
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