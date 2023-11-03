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
    public string sceneName = "MainMap";
    public TextMeshProUGUI greetings;
    public TextMeshProUGUI anxiety_txt;
    public TextMeshProUGUI literacy_txt;
    public TextMeshProUGUI climate_change_txt;
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
        greetings.CrossFadeAlpha(1.0f, animationduration, true);
        anxiety_txt.CrossFadeAlpha(1.0f, animationduration, true);
        literacy_txt.CrossFadeAlpha(1.0f, animationduration, true);
        climate_change_txt.CrossFadeAlpha(1.0f, animationduration, true);
        anxiety.gameObject.SetActive(true);
        literacy.gameObject.SetActive(true);
        climate_change_skept.gameObject.SetActive(true);
        
        // set sliders to 0
        anxiety.value = 0;
        literacy.value = 0;
        climate_change_skept.value = 0;
    }

    // On button down -> save data + change scene
    public void changeScene()
    {
        PlayerPrefs.SetInt("anxiety", (int)anxiety.value);
        // /!\ inverto la literacy per farlo coerente con gli altri -> "quanto poco sei litterato?"
        PlayerPrefs.SetInt("literacy_inverted", (int)(literacy.maxValue-literacy.value));
        PlayerPrefs.SetInt("climate_change_skept", (int)anxiety.value);
        SceneManager.LoadScene(sceneName);
    }
}
