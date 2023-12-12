using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Home()
    {
        GameManager.instance.Save();
        Destroy(GameObject.Find("player"));
        Destroy(GameObject.Find("Virtual Camera"));
        Destroy(GameObject.Find("GameManager"));
        Destroy(GameObject.Find("SceneMaster"));
        Destroy(GameObject.Find("Bever"));
        
        Time.timeScale = 1f;
        
    }
}
