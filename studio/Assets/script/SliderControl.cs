using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SliderControl : MonoBehaviour
{
    public Slider anxiety;
    public Slider literacy;
    [FormerlySerializedAs("ccskepticism")] public Slider climate_change_skept;
    private string sceneName = "Home";
    public TextMeshProUGUI anxiety_value;
    public TextMeshProUGUI literacy_value;
    public TextMeshProUGUI climate_change_skept_value;
    public float animationduration=2.0f;

    private void Awake()
    {
        anxiety.gameObject.SetActive(false);
        literacy.gameObject.SetActive(false);
        climate_change_skept.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        anxiety.gameObject.SetActive(true);
        literacy.gameObject.SetActive(true);
        climate_change_skept.gameObject.SetActive(true);
        
        // set sliders to 1 (min value): 1 per non creare problemi poi con le task
        anxiety.value = 1;
        literacy.value = 1;
        climate_change_skept.value = 1;
    }
    
    void Update()
    {
        // aggiorno scritta esterna:
        // TextMeshPro.text (testo dell'oggett0) = slider.value (valore dello slider)
        anxiety_value.text = anxiety.value.ToString();
        literacy_value.text = literacy.value.ToString();
        climate_change_skept_value.text = climate_change_skept.value.ToString();
        
        // modifico la posizione del testo che rappresenta il valore dello slider
        anxiety_value.rectTransform.position = new Vector3(anxiety.handleRect.position.x,anxiety_value.rectTransform.position.y, anxiety_value.rectTransform.position.z);
        literacy_value.rectTransform.position = new Vector3(literacy.handleRect.position.x,literacy_value.rectTransform.position.y, literacy_value.rectTransform.position.z);
        climate_change_skept_value.rectTransform.position = new Vector3(climate_change_skept.handleRect.position.x,climate_change_skept_value.rectTransform.position.y, climate_change_skept_value.rectTransform.position.z);
    }
    
    // On button down -> save data + change scene
    public void changeScene()
    {
        PlayerPrefs.SetFloat("anxiety", anxiety.value);
        // /!\ inverto la literacy per farlo coerente con gli altri -> "quanto poco sei litterato?"
        PlayerPrefs.SetFloat("literacy_inverted", (literacy.maxValue-literacy.value));
        PlayerPrefs.SetFloat("climate_change_skept", climate_change_skept.value);
        PlayerPrefs.SetFloat("sum_parameters", anxiety.value + literacy.maxValue - literacy.value + climate_change_skept.value);
        GameManager.instance.anxiety = anxiety.value;
        GameManager.instance.literacy_inverted = literacy.maxValue - literacy.value;
        GameManager.instance.climate_change_skept = climate_change_skept.value;
        GameManager.instance.sum_parameters = anxiety.value + literacy.maxValue - literacy.value + climate_change_skept.value;
        SceneManager.LoadScene(sceneName);
        GameManager.instance.ActivatePlayer(true);
        GameManager.instance.player.transform.position = new Vector3(-9.0f, 11.7f, 0);
    }
}
