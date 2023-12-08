using UnityEngine;

public class ButtonState : MonoBehaviour
{
    private static int buttonState = 0; // Variabile statica per lo stato del pulsante

    public void OnButtonClick()
    { 
        buttonState = 1; // Imposta lo stato del pulsante a 1 quando viene cliccato

        // Carica la nuova scena
        UnityEngine.SceneManagement.SceneManager.LoadScene("PuzzleGame");
    }

    public static int GetButtonState()
    {
        return buttonState; // Metodo per ottenere lo stato del pulsante
    }
}


