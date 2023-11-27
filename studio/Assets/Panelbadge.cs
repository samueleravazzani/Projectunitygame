using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Panelbadge : MonoBehaviour
{
    public Image fireLockImage;
    public Image waterLockImage;
    public Image airLockImage;
    public Image plasticLockImage;

    public Image fireImage;
    public Image waterImage;
    public Image airImage;
    public Image plasticImage;

    public TextMeshProUGUI fireCounterText;
    public TextMeshProUGUI waterCounterText;
    public TextMeshProUGUI airCounterText;
    public TextMeshProUGUI plasticCounterText;

    private int fireCounter = 0;
    private int waterCounter = 0;
    private int airCounter = 0;
    private int plasticCounter = 0;

    private bool fireSet = false;
    private bool waterSet = false;
    private bool airSet = false;
    private bool plasticSet = false;

    void Start()
    {
        Debug.Log("Panelbadge Start() called.");

        // Initially, display the lock image and hide the new image with counter
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

    public void StartSimulation()
    {
        Debug.Log("StartSimulation() called.");
        // Simulating variable setting after a delay (for demonstration purposes)
        Invoke("SimulateVariableSetting", 2f);
    }

    void SimulateVariableSetting()
    {
        Debug.Log("SimulateVariableSetting() called.");

        // Here, you can call the SetVariable function with different variables
        SetVariable("fire");


        // Simulating setting 'fire' again after a delay
        Invoke("SimulateFireIncrement", 2f);
    }

    void SimulateFireIncrement()
    {
        Debug.Log("SimulateFireIncrement() called.");
        // Simulate incrementing 'fire'
        SetVariable("fire");
    }
    
    public void SetVariable(string variable)
    {
        switch (variable)
        {
            case "fire":
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
            case "water":
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
            case "air":
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
            case "plastic":
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