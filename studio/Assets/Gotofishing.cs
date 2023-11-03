using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string nombreDeEscena = "Fishing"; // Reemplaza "NuevaEscena" con el nombre de la escena a la que deseas cambiar.

    public void CambiarEscena()
    {
        SceneManager.LoadScene(nombreDeEscena);
    }
}
