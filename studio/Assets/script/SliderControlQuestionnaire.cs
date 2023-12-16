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
    private string sceneName = "MainMap";
    public TextMeshProUGUI anxiety_value_final;
    public TextMeshProUGUI literacy_value_final;
    public TextMeshProUGUI climate_change_skept_value_final;
    public float animationduration=2.0f;
    private float actual_value_a;
    private float actual_value_l;
    private float actual_value_c;
    
    
    private void Awake()
    {
        anxiety_final.gameObject.SetActive(false);
        literacy_final.gameObject.SetActive(false);
        climate_change_skept_final.gameObject.SetActive(false);
        actual_value_a = GameManager.instance.anxiety;
        actual_value_l = GameManager.instance.literacy_inverted;
        actual_value_c = GameManager.instance.climate_change_skept;
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
    public void conclusion()
    {
        float a_final;
        float l_final;
        float c_final;
        
        a_final = 10f - anxiety_final.value + 1f;
        l_final = 10f - literacy_final.value + 1f; 
        c_final = 10f - climate_change_skept_final.value + 1f;
        
        if (GameManager.instance.anxiety < a_final)
        {
            GameManager.instance.anxiety = actual_value_a + 0.1f * a_final;
        }
        else if (GameManager.instance.anxiety > a_final)
        {
            GameManager.instance.anxiety = actual_value_a - 0.1f * a_final;
        }

        if (GameManager.instance.literacy_inverted < l_final)
        {
            GameManager.instance.literacy_inverted =
                actual_value_l + 0.1f * l_final;
        }
        else if (GameManager.instance.literacy_inverted > l_final)
        {
            GameManager.instance.literacy_inverted =
                actual_value_l - 0.1f * l_final;
        }

        if (GameManager.instance.climate_change_skept <  c_final)
        {
            GameManager.instance.climate_change_skept = actual_value_c + 0.1f *  c_final;
        }
        else if (GameManager.instance.climate_change_skept >  c_final)
        {
            GameManager.instance.climate_change_skept = actual_value_c - 0.1f *  c_final;
        }

        GameManager.instance.sum_parameters = GameManager.instance.anxiety + GameManager.instance.literacy_inverted +
                                              GameManager.instance.climate_change_skept;
        
        GameManager.instance.questionnairedone = true;
    }
}
