using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    /* ATTACCATO A Medicine_card CardDisplay Manager*/
    public Medicine_Card card_shown;
    public TextMeshProUGUI drug_name;
    public TextMeshProUGUI drug_class;
    public TextMeshProUGUI drug_indication;
    public TextMeshProUGUI drug_warnings;
    public Image drug_image;
    public Image background;
    
    public Image medicine_taken;
    public TextMeshProUGUI result;
    public Image result_img;
    public TextMeshProUGUI potions_tf;
    
    public static CardDisplay instance;
    private void Awake() //creation singleton
    {
        if (instance != null)
        {
            Debug.LogWarning("found more than one dialogue Manager in the scene");
        }
        instance = this;
    }
    
    private void Start()
    {
        HideCard();
        HideResult();
    }

    public void ShowCard(Medicine_Card card)
    {
        // Debug.Log(card.name);
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
        card_shown = card;
    }
    
    public void HideCard() // Button BACK (1)
    {
        drug_name.gameObject.SetActive(false);
        drug_class.gameObject.SetActive(false);
        drug_indication.gameObject.SetActive(false);
        drug_warnings.gameObject.SetActive(false);
        drug_image.gameObject.SetActive((false));
        background.gameObject.SetActive(false);
    }
    
    public void TakeMedicine() // button TAKE
    {
        // se la medicina scelta dal DisplayEnigma (che decide l'enigma) è uguale alla medicine che viene mostrata
        // (che viene passata qui dal ShowCard) e che cambia quella 
        if (DisplayEnigma.instance.chosen_medicine == card_shown)
        {
            result.text = "Congratulations! \nYou have chosen the right potion to save the world! \n It was " + card_shown.name;
            DisplayEnigma.instance.medicine_guessed++;
            if (DisplayEnigma.instance.medicine_guessed == DisplayEnigma.instance.medicines_to_guess){
                // GameManager.instance.task_index++;
                // EnvironmentControl.instance.update_environment = true;
                // GameManager.instance.literacy += GameManager.instance.incremento;
            }
            else{
                DisplayEnigma.instance.ChooseEnigma(); // scelgo un altro enigma solo se ho altri enigmi da trovare
            }
            
        }
        else
        {
            result.text = "Oh no, you have picked the wrong potion. \nBe careful, it can be dangerous to take the wrong one! \nIt was not " + card_shown.name;
            if (DisplayEnigma.instance.medicine_guessed == DisplayEnigma.instance.medicines_to_guess){
                
                // GameManager.instance.literacy -= GameManager.instance.incremento;
            }
        }
        result_img.sprite = card_shown.drug_image; // /!\ image.sprite = sprite
        potions_tf.text = "Potions to find: " + (DisplayEnigma.instance.medicines_to_guess - DisplayEnigma.instance.medicine_guessed).ToString();
        HideCard();
        medicine_taken.gameObject.SetActive(true);
    }

    public void HideResult() // start + BACK (2)
    {
        medicine_taken.gameObject.SetActive(false);
    }
}
