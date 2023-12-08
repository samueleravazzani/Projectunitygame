// Codice per la seconda scena (Scene2)
using UnityEngine;

public class ButtonState1 : MonoBehaviour
{
    public GameObject buttonToActivate;
    public GameObject buttonToDeactivate;
    

    void Start()
    {
        buttonToDeactivate.SetActive(true);
        buttonToActivate.SetActive(false);
        int currentState = ButtonState.GetButtonState();
        if (currentState==1)
        {
            buttonToDeactivate.SetActive(false);
            buttonToActivate.SetActive(true);
        }
    }
}