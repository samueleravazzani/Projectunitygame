using UnityEngine;

public class ButtonState_CCF1 : MonoBehaviour
{
    public GameObject buttonToActivate;
    public GameObject buttonToDeactivate;
    

    void Start()
    {
        buttonToDeactivate.SetActive(true);
        buttonToActivate.SetActive(false);
        int currentState_CCF = ButtonState_CCF.GetButtonState_CCF();
        if (currentState_CCF==1)
        {
            buttonToDeactivate.SetActive(false);
            buttonToActivate.SetActive(true);
        }
    }
}