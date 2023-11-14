using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string sceneToLoad; // poner aca fishing asi me lleva a esa escena
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Guarda la posición del jugador antes de cambiar de escena
            PlayerPrefs.SetFloat("PlayerPosX", player.transform.position.x);
            PlayerPrefs.SetFloat("PlayerPosY", player.transform.position.y);

            // Desactiva el GameObject del jugador para que no sea visible
            player.SetActive(false);

            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
