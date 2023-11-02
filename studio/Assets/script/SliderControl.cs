using System.Collections;
using System.Collections.Generic;
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
    
    // Start is called before the first frame update
    void Start()
    {
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
