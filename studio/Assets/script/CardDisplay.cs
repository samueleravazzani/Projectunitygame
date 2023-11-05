using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public static string medicine_name;
    public static bool showcard = false;
    public Medicine_Card[] cards;
    private Medicine_Card card;
    public TextMeshProUGUI drug_name;
    public TextMeshProUGUI drug_class;
    public TextMeshProUGUI drug_indication;
    public TextMeshProUGUI drug_warnings;
    public Image drug_image;
    public Image background;

    private void Start()
    {
        drug_name.gameObject.SetActive(false);
        drug_class.gameObject.SetActive(false);
        drug_indication.gameObject.SetActive(false);
        drug_warnings.gameObject.SetActive(false);
        drug_image.gameObject.SetActive((false));
        background.gameObject.SetActive(false);
    }

    void Update()
    {
        if (showcard)
        {
            ShowCard();
        }
    }

    public void ShowCard()
    {
        switch (medicine_name)
        {
            case "Paracetamol": card=cards[0];
                break;
        }
        
        
        drug_name.text = card.name;
        drug_class.text = card.drug_class;
        drug_indication.text = card.drug_indication;
        drug_warnings.text = card.drug_warnings;
        drug_image.sprite = card.drug_image;
        drug_name.gameObject.SetActive(true);
        drug_class.gameObject.SetActive(true);
        drug_indication.gameObject.SetActive(true);
        drug_warnings.gameObject.SetActive(true);
        drug_image.gameObject.SetActive((true));
        background.gameObject.SetActive(true);
    }
    
    public void HideCard()
    {
        drug_name.gameObject.SetActive(false);
        drug_class.gameObject.SetActive(false);
        drug_indication.gameObject.SetActive(false);
        drug_warnings.gameObject.SetActive(false);
        drug_image.gameObject.SetActive((false));
        background.gameObject.SetActive(true);
    }
}
