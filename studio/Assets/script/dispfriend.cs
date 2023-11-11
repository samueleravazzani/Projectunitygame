using UnityEngine;
using UnityEngine.UI;

public class FriendsScoreUI : MonoBehaviour
{
    // Reference to the UI Text component where the friends' score will be displayed.
    public Text friendsScoreText;

    // The current score of friends that will be displayed and updated.
    public int friendsCollectedCount = 0;

    private void Update()
    {
        // Update the text of the "friends" counter with the current value of friendsCollectedCount.
        friendsScoreText.text = "Friends collected: " + friendsCollectedCount;
    }

    public void UpdateFriendsCount(int amount)
    {
        // This function is called from another script to update the "friends" counter.
        // It increases or decreases the friendsCollectedCount by the specified amount.
        friendsCollectedCount += amount;
    }
}