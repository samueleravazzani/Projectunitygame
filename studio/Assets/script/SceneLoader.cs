using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GameObject loaderUI;
    [SerializeField] private Slider progressSlider;
    [SerializeField] private float sliderSpeed = 0.1f;

    private void Start()
    {
        if (loaderUI != null)
        {
            loaderUI.SetActive(false);
        }
    }

    public void LoadScene(int index)
    {
        if (loaderUI != null)
        {
            loaderUI.SetActive(true);
            StartCoroutine(LoadSceneCoroutine(index));
        }
    }

    private IEnumerator LoadSceneCoroutine(int index)
    {
        if (progressSlider != null)
        {
            progressSlider.value = 0;

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(index);
            asyncOperation.allowSceneActivation = false;
            float progress = 0;

            while (!asyncOperation.isDone)
            {
                progress = Mathf.MoveTowards(progress, asyncOperation.progress, Time.deltaTime * sliderSpeed);
                progressSlider.value = progress;

                if (progress >= 0.9f)
                {
                    progressSlider.value = 1;
                    asyncOperation.allowSceneActivation = true;
                    Debug.Log("Scene activation allowed.");
                }

                Debug.Log("Progress: " + progress); // Aggiunto il debug del progresso
                yield return null;
            }

            if (loaderUI != null)
            {
                loaderUI.SetActive(false);
            }
        }
    }
}