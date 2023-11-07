using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public Text scoreText;
    public int collectedCount = 0;

    private void Update()
    {
        // Actualiza el texto del contador con el valor actual de collectedCount.
        scoreText.text = "Rubbish collected: " + collectedCount;
    }

    public void UpdateCount(int amount)
    {
        // Esta función se llama desde otro script (como tu script "Collect") para actualizar el contador.
        collectedCount += amount;
    }
}