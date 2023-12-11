using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Panelbadge : MonoBehaviour
{
    public GameObject panel; //BADGE PANEL
    
    public Image fireLockImage; //lock to the fire 
    public Image waterLockImage; //lock to the water
    public Image airLockImage; //lock to the air
    public Image plasticLockImage; //lock to the plastic

    public Image fireImage; //firebadge
    public Image waterImage; //waterebadge
    public Image airImage; //airbadge
    public Image plasticImage; //plasticbadge

    //tEXTS TO DISPLAY THE VALUE OF THE BADGE , HOW MANY TIMES ALREADY GAINED
    public TextMeshProUGUI fireCounterText;
    public TextMeshProUGUI waterCounterText;
    public TextMeshProUGUI airCounterText;
    public TextMeshProUGUI plasticCounterText;

    void Start()
    {
        panel.gameObject.SetActive(false);
        // Initially, display the lock image and hide the badge image with counter
        fireLockImage.gameObject.SetActive(true);
        fireImage.gameObject.SetActive(false);
        fireCounterText.gameObject.SetActive(false);
        waterLockImage.gameObject.SetActive(true);
        waterImage.gameObject.SetActive(false);
        waterCounterText.gameObject.SetActive(false);
        airLockImage.gameObject.SetActive(true);
        airImage.gameObject.SetActive(false);
        airCounterText.gameObject.SetActive(false);
        plasticLockImage.gameObject.SetActive(true);
        plasticImage.gameObject.SetActive(false);
        plasticCounterText.gameObject.SetActive(false);
    }

    //lOGIC TO HANDLE THE BUTTONS TO SHOW AND HIDE THE OANELBADGE 
    public void Showpanel()
    {
        panel.gameObject.SetActive(true);
    }

    public void Hidepanel()
    {
        panel.gameObject.SetActive(false);
    }
    
    //nECESSARY TO CONTINUOSLY BE DONE, SO TO UPDATE THE BADGE PANEL EVERYTIME I AM IN THE MAINMAP 
    public void Update()
    {
        SetVariable();
    }

    //Function called in the GameManager when solved the problem (QUINDI QUANDO TASK INDEX ARRIVA A 3) 
    //For example the problem extracted fire 1 -> when it is the first time  tolgo il lock
    //metto il badge e faccio vedere il counter che viene messo a 1 (chiamato dal gamemanager) +
    //le altre volte semplicemente incrementa il counter 
    //Il problema viene chiamato con previous problem perchè problem now una volta fatte tutte e tre le task si setta a zero 
    public void SetVariable()
    {
             switch (GameManager.instance.previous_problem)
                    {
                        case 1:
                            if (GameManager.instance.fireCounter >= 1)
                            {
                                fireLockImage.gameObject.SetActive(false);
                                fireImage.gameObject.SetActive(true);
                                fireCounterText.gameObject.SetActive(true);
                            }
                            fireCounterText.text = GameManager.instance.fireCounter.ToString();
                            break;
                        case 2:
                            if (GameManager.instance.plasticCounter >= 1)
                            {
                                plasticLockImage.gameObject.SetActive(false);
                                plasticImage.gameObject.SetActive(true);
                                plasticCounterText.gameObject.SetActive(true);
                            }
                            plasticCounterText.text = GameManager.instance.plasticCounter.ToString();
                            break;
                        case 3:
                            if (GameManager.instance.waterCounter >= 1)
                            {
                                waterLockImage.gameObject.SetActive(false);
                                waterImage.gameObject.SetActive(true);
                                waterCounterText.gameObject.SetActive(true);
                            }
                            waterCounterText.text = GameManager.instance.waterCounter.ToString();
                            break;
                        case 4:
                            if (GameManager.instance.airCounter >= 1)
                            {
                                airLockImage.gameObject.SetActive(false);
                                airImage.gameObject.SetActive(true);
                                airCounterText.gameObject.SetActive(true);
                            }
                            airCounterText.text = GameManager.instance.airCounter.ToString();
                            break;
                        default:
                            Debug.LogWarning("Invalid variable name" +GameManager.instance.problem_now);
                            break;
                    }
    }
}