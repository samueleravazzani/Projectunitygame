using UnityEngine;

public class PlayerVisibilityController : MonoBehaviour
{
    private void Start()
    {
        // Verifica si existen coordenadas guardadas
        if (PlayerPrefs.HasKey("PlayerPosX") && PlayerPrefs.HasKey("PlayerPosY"))
        {
            // Busca el jugador en la escena actual
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
            {
                // Desactiva el Renderer y el Collider del jugador para que no sea visible ni interactivo
                player.GetComponent<Renderer>().enabled = false;
                player.GetComponent<Collider2D>().enabled = false;

                // Mueve el jugador a la posición guardada
                player.transform.position = new Vector2(PlayerPrefs.GetFloat("PlayerPosX"), PlayerPrefs.GetFloat("PlayerPosY"));
            }
        }
    }
}


