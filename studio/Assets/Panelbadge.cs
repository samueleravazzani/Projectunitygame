using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Panelbadge : MonoBehaviour
{
    public Image fireLockImage; //lock to the fire 
    public Image waterLockImage; //lock to the water
    public Image airLockImage; //lock to the air
    public Image plasticLockImage; //lock to the plastic

    public Image fireImage; //firebadge
    public Image waterImage; //waterebadge
    public Image airImage; //airbadge
    public Image plasticImage; //plasticbadge

    public TextMeshProUGUI fireCounterText;
    public TextMeshProUGUI waterCounterText;
    public TextMeshProUGUI airCounterText;
    public TextMeshProUGUI plasticCounterText;

    
    //counter to display how many times solved the task of the world for each problem
    private int fireCounter = 0; 
    private int waterCounter = 0;
    private int airCounter = 0;
    private int plasticCounter = 0;

    //Variable set  to keep into account that is the first time the task is solved
    // so only the first time the lock will be deactivated and the badge activated,
    // other times instead the counter simply increments
    //Each one for each variable
    private bool fireSet = false;
    private bool waterSet = false;
    private bool airSet = false;
    private bool plasticSet = false;

    void Start()
    {
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
    
    //Function called in the GameManager when solved the problem 
    //For example the problem extracted fire 0 -> you call this function and so if false la prima volta tolgo il lock
    //metto il badge e faccio vedere il counter che viene messo a 1 + metto la variabile set a true perchè ho passato il caso della prima volta in vui c'è
    //il lucchetto
    public void SetVariable(int variable)
    {
        switch (variable)
        {
            case 1:
                if (!fireSet)
                {
                    fireLockImage.gameObject.SetActive(false);
                    fireImage.gameObject.SetActive(true);
                    fireCounterText.gameObject.SetActive(true);
                    fireSet = true;
                }
                fireCounter++;
                fireCounterText.text = fireCounter.ToString();
                break;
            case 3:
                if (!waterSet)
                {
                    waterLockImage.gameObject.SetActive(false);
                    waterImage.gameObject.SetActive(true);
                    waterCounterText.gameObject.SetActive(true);
                    waterSet = true;
                }
                waterCounter++;
                waterCounterText.text = waterCounter.ToString();
                break;
            case 4:
                if (!airSet)
                {
                    airLockImage.gameObject.SetActive(false);
                    airImage.gameObject.SetActive(true);
                    airCounterText.gameObject.SetActive(true);
                    airSet = true;
                }
                airCounter++;
                airCounterText.text = airCounter.ToString();
                break;
            case 2:
                if (!plasticSet)
                {
                    plasticLockImage.gameObject.SetActive(false);
                    plasticImage.gameObject.SetActive(true);
                    plasticCounterText.gameObject.SetActive(true);
                    plasticSet = true;
                }
                plasticCounter++;
                plasticCounterText.text = plasticCounter.ToString();
                break;
            default:
                Debug.LogWarning("Invalid variable name");
                break;
        }
    }
}