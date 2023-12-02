using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryFishing : MonoBehaviour
{
    // Nombre de la escena actual
    private string currentScene;

    void Start()
    {
        // Guarda el nombre de la escena actual al inicio
        currentScene = SceneManager.GetActiveScene().name;
    }

    public void RestartGame()
    {
        // Reinicia la escena actual
        SceneManager.LoadScene(currentScene);
    }
}