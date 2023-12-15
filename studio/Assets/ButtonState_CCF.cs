using UnityEngine;

public class ButtonState_CCF : MonoBehaviour
{
    private static int buttonState = 0; 

    public void OnButtonClick()
    { 
        buttonState = 1; 
        UnityEngine.SceneManagement.SceneManager.LoadScene("Climate_ChangeINTRO");
    }

    public static int GetButtonState_CCF()
    {
        return buttonState; 
    }
}