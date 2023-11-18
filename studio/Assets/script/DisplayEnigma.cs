using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DisplayEnigma : MonoBehaviour
{
    /* ATTACCATO ALLA CHEST */
    public Medicine_Card[] medicines;
    public Medicine_Card chosen_medicine;
    public Image paper;
    public TextMeshProUGUI enigma;
    public TextMeshProUGUI potions_to_find;
    public int chosen_number;
    public string chosen_enigma;
    public int time_enigma_shown = 0;
    
    /* parametrization */
    public int medicines_to_guess;
    private static float calibration = 8/9f;
    public int medicine_guessed = 0;
    public int medicine_wrong = 0;
    
    public static DisplayEnigma instance;
    private void Awake() //creation singleton
    {
        if (instance != null)
        {
            Debug.LogWarning("found more than one dialogue Manager in the scene");
        }
        instance = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        HideEnigma();
        ChooseEnigma(); // devo fare in modo che questo avvenga solo quando viene cambiato il valore di medicines_to_guess
        // MA non ogni volta che guarda l'indizio
        medicines_to_guess = (int) Mathf.RoundToInt(GameManager.instance.literacy_inverted * calibration);
        Debug.Log(GameManager.instance.literacy_inverted.ToString());
        Debug.Log((GameManager.instance.literacy_inverted * calibration).ToString());
        Debug.Log(medicines_to_guess.ToString());
        
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ShowEnigma();
        }
    }

    public void ChooseEnigma()
    {
        chosen_number = Random.Range(0, medicines.Length - 1);
        {
            // medicina scelta
            chosen_medicine = medicines[chosen_number];
            // enigma scelto
            chosen_enigma = chosen_medicine.enigmas[Random.Range(0,chosen_medicine.enigmas.Length-1)];
        }
    }

    public void ShowEnigma()
    {
        enigma.text = chosen_enigma;
        paper.gameObject.SetActive(true);
        potions_to_find.text = "Potions to find: " + (medicines_to_guess - medicine_guessed).ToString();
        
        if (time_enigma_shown == 0)
        {
            SpawnsMedicine.instance.SpawnMedicines();
        }
        time_enigma_shown++;
    }
    public void HideEnigma()
    {
        paper.gameObject.SetActive(false);
    }
}
