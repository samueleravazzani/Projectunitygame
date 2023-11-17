using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DisplayEnigma : MonoBehaviour
{
    public Medicine_Card[] medicines;
    public Medicine_Card chosen_medicine;
    public Image paper;
    public TextMeshProUGUI enigma;
    public int chosen_number;
    
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
        ChooseEnigma();
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
            chosen_medicine = medicines[chosen_number];
        }
    }

    public void ShowEnigma()
    {
        enigma.text = "";
        paper.gameObject.SetActive(true);
    }
    public void HideEnigma()
    {
        paper.gameObject.SetActive(false);
    }
}
