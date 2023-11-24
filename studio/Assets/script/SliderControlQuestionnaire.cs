using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SliderControlQuestionnaire : MonoBehaviour
{
    public Slider anxiety_final;
    public Slider literacy_final;
    [FormerlySerializedAs("ccskepticism")] public Slider climate_change_skept_final;
    public string sceneName = "MainMap";
    public TextMeshProUGUI anxiety_value_final;
    public TextMeshProUGUI literacy_value_final;
    public TextMeshProUGUI climate_change_skept_value_final;
    public float animationduration=2.0f;
    public float anxietyValue_final;
    public float literacyValue_final;
    public float climateValue_final;
    
    private void Awake()
    {
        anxiety_final.gameObject.SetActive(false);
        literacy_final.gameObject.SetActive(false);
        climate_change_skept_final.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        anxiety_final.gameObject.SetActive(true);
        literacy_final.gameObject.SetActive(true);
        climate_change_skept_final.gameObject.SetActive(true);
        
        // set sliders to 1 (min value): 1 per non creare problemi poi con le task
        anxiety_final.value = 1;
        literacy_final.value = 1;
        climate_change_skept_final.value = 1;
    }
    
    void Update()
    {
        // aggiorno scritta esterna:
        // TextMeshPro.text (testo dell'oggett0) = slider.value (valore dello slider)
        anxiety_value_final.text = anxiety_final.value.ToString();
        literacy_value_final.text = literacy_final.value.ToString();
        climate_change_skept_value_final.text = climate_change_skept_final.value.ToString();
        
        // modifico la posizione del testo che rappresenta il valore dello slider
        anxiety_value_final.rectTransform.position = new Vector3(anxiety_final.handleRect.position.x,anxiety_value_final.rectTransform.position.y, anxiety_value_final.rectTransform.position.z);
        literacy_value_final.rectTransform.position = new Vector3(literacy_final.handleRect.position.x,literacy_value_final.rectTransform.position.y, literacy_value_final.rectTransform.position.z);
        climate_change_skept_value_final.rectTransform.position = new Vector3(climate_change_skept_final.handleRect.position.x,climate_change_skept_value_final.rectTransform.position.y, climate_change_skept_value_final.rectTransform.position.z);
    }
    
    // On button down -> save data + change scene
    public void SaveParameters()
    {
        
        // Multiply the slider values by 0.1 and store them in separate variables
        anxietyValue_final = anxiety_final.value * 0.1f;
        // /!\ inverto la literacy per farlo coerente con gli altri -> "quanto poco sei litterato?"
        literacyValue_final = (literacy_final.maxValue - literacy_final.value) * 0.1f;
        climateValue_final = climate_change_skept_final.value * 0.1f;
        
        // sum_parameters = anxietyValue_final + literacyValue_final + climateValue_final; ???????
        
        // Debug log statements to print the values
        Debug.Log("Anxiety Value: " + anxietyValue_final);
        Debug.Log("Literacy Value: " + literacyValue_final);
        Debug.Log("Climate Value: " + climateValue_final);
        
        //VALUTARE ANCORA SE FARE LA SCALATURA *0,1 DEL VALORE SCLELTO DA SOTTRARRE AI PARAMETRI DEL GAMEMANAGER

        //CAPIRE COME PRENDERE E USARE I PARAMETRI NEL GAME MANAGER SALVATI IN QUESTE VARIABILI!
    }
    
    public void ExitGame()
    {
        // Change scene or quit the application
        SceneManager.LoadScene(sceneName); // Replace with the scene you want to load
    }
    
}
