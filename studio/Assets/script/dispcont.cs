using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    // Reference to the UI Text component where the score will be displayed.
    public Text scoreText;

    // The current score that will be displayed and updated.
    public int collectedCount = 0;

    private void Update()
    {
        // Update the text of the counter with the current value of collectedCount.
        scoreText.text = "Rubbish to collect: " + collectedCount;
    }

    public void UpdateCount(int amount)
    {
        // This function is called from another script to update the counter.
        // It increases the collectedCount by the specified amount.
        collectedCount += amount;
    }
}