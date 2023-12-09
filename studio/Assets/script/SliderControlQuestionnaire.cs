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
    public GameObject Questionnaire;
    public GameObject Conclusion;
    private float actual_value_a;
    private float actual_value_l;
    private float actual_value_c;
    
    
    private void Awake()
    {
        anxiety_final.gameObject.SetActive(false);
        literacy_final.gameObject.SetActive(false);
        climate_change_skept_final.gameObject.SetActive(false);
        Questionnaire.gameObject.SetActive(true);
        Conclusion.gameObject.SetActive(false);
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
        if (GameManager.instance.anxiety < anxiety_final.value)
        {
            GameManager.instance.anxiety = actual_value_a + 0.1f * anxiety_final.value;
        }
        else if (GameManager.instance.anxiety > anxiety_final.value)
        {
            GameManager.instance.anxiety = actual_value_a - 0.1f * anxiety_final.value;
        }

        if (GameManager.instance.literacy_inverted < (literacy_final.maxValue - literacy_final.value + 1))
        {
            GameManager.instance.literacy_inverted =
                actual_value_l + 0.1f * (literacy_final.maxValue - literacy_final.value + 1);
        }
        else if (GameManager.instance.literacy_inverted > (literacy_final.maxValue - literacy_final.value + 1))
        {
            GameManager.instance.literacy_inverted =
                actual_value_l - 0.1f * (literacy_final.maxValue - literacy_final.value + 1);
        }

        if (GameManager.instance.climate_change_skept < climate_change_skept_final.value)
        {
            GameManager.instance.climate_change_skept = actual_value_c + 0.1f * climate_change_skept_final.value;
        }
        else if (GameManager.instance.climate_change_skept > climate_change_skept_final.value)
        {
            GameManager.instance.climate_change_skept = actual_value_c - 0.1f * climate_change_skept_final.value;
        }

        GameManager.instance.sum_parameters = GameManager.instance.anxiety + GameManager.instance.literacy_inverted +
                                              GameManager.instance.climate_change_skept;

        Questionnaire.gameObject.SetActive(false);
        Conclusion.gameObject.SetActive(true);
        GameManager.instance.questionnairedone = true;
    }
}
