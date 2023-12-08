using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    public Image success;
    public Image fail;
    public Image[] errors;
    
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
        HideErrors();
        HideCard();
        HideResult();
        HideSuccess();
        HideFail();
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
        if (DisplayEnigma.instance.chosen_medicine == card_shown) // GIUSTO
        {
            result.text = "Congratulations! \nYou have chosen the right potion to save the world! \n It was " + card_shown.name;
            DisplayEnigma.instance.medicine_guessed++;
            DisplayEnigma.instance.HideLittleChest();
            if (DisplayEnigma.instance.medicine_guessed == DisplayEnigma.instance.medicines_to_guess){
                
            }
            else{
                DisplayEnigma.instance.ChooseEnigma(); // scelgo un altro enigma solo se ho altri enigmi da trovare
            }
            
        }
        else  // SBAGLIATO
        {
            result.text = "Oh no, you have picked the wrong potion. \nBe careful, it can be dangerous to take the wrong one! \nIt was not " + card_shown.name;
            Error(DisplayEnigma.instance.medicine_wrong);
            DisplayEnigma.instance.medicine_wrong++;
        }
        result_img.sprite = card_shown.drug_image; // /!\ image.sprite = sprite
        potions_tf.text = "Potions to find: " + (DisplayEnigma.instance.medicines_to_guess - DisplayEnigma.instance.medicine_guessed).ToString();
        HideCard();
        medicine_taken.gameObject.SetActive(true);
    }

    public void HideResult() // start + BACK (2)
    {
        medicine_taken.gameObject.SetActive(false);
        
        // SUCCESS
        if (DisplayEnigma.instance.medicine_guessed == DisplayEnigma.instance.medicines_to_guess){
            success.gameObject.SetActive(true);
        }
        
        // FAIL
        if (DisplayEnigma.instance.medicine_wrong == DisplayEnigma.instance.max_errors){
            fail.gameObject.SetActive(true);
        }
    }

    public void HideSuccess()
    {
        success.gameObject.SetActive(false);
    }
    public void HideFail()
    {
        fail.gameObject.SetActive(false);
    }

    public void Retry()
    {
        SceneManager.LoadScene("Home");
    }

    public void Quit()
    {
        HideSuccess();
        HideFail();
        DisplayEnigma.instance.HideLittleChest();
    }

    public void Error(int number)
    {
        errors[number].gameObject.SetActive(true);
    }

    public void HideErrors()
    {
        for (int i = 0; i < errors.Length; i++)
        {
            errors[i].gameObject.SetActive(false);
        }
    }
}
