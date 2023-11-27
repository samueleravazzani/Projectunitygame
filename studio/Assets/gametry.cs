using UnityEngine;

public class gametry : MonoBehaviour
{
    public Panelbadge lockPanelController;

    void Start()
    {
        // Find the GameObject with the Panelbadge script and assign it to lockPanelController
        GameObject panelbadgeObject = GameObject.Find("PanelManager");

        if (panelbadgeObject != null)
        {
            lockPanelController = panelbadgeObject.GetComponent<Panelbadge>();

            if (lockPanelController != null)
            {
                Debug.Log("lockPanelController found and assigned.");
                lockPanelController.StartSimulation();
            }
            else
            {
                Debug.LogError("Panelbadge script not found on the GameObject.");
            }
        }
        else
        {
            Debug.LogError("GameObject with Panelbadge script not found.");
        }
    }
}