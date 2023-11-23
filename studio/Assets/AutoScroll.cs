using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using TMPro; 
using UnityEngine.SceneManagement;

public class AutoScroll : MonoBehaviour
{
    float speed = 80.0f;
    float textPosBegin = -550.0f;
    float boundaryTextEnd = 2400.0f;

    private RectTransform myGorectTransform;

    [SerializeField] private TextMeshProUGUI mainText;

    private string myText;

    // Start is called before the first frame update
    void Start()
    {
        myGorectTransform = gameObject.GetComponent<RectTransform>();
        HandleCustomizeText();
        StartCoroutine(AutoscrolLText());
    }

    IEnumerator AutoscrolLText()
    {
        while (myGorectTransform.localPosition.y < boundaryTextEnd)
        {
            myGorectTransform.Translate(Vector3.up * speed * Time.deltaTime);
            yield return null;
        }

        // Load another scene when the text reaches the boundary
        LoadNextScene();
    }

    void HandleCustomizeText()
    {
        myText =
            "<size=100><color=#94FF4A>ENCHANTED FOREST</color></size> is an ancient magic land where nature and fantasy have always coexisted in harmony.\n";
        myText = myText +
                 "\nHowever, now something is <color=red>wrong</color>. A <color=red><size=100>mysterious force</color></size> is causing natural disasters that threatens to <color=red>destroy</color> this wonderful land.\n";
        myText = myText +
                 "\n<size=100><color=#fff239>YOU ARE THE CHOSEN ONE</size></color>, who can <color=#fff239>stop this evil force</color> and restore the balance! \nYou will have to work on yourself, but you have the skills to explore the forest and its secrets, face challenges to fight this enemy and <color=#fff239>save the world.</color>\n";
        myText = myText + "\nForest creatures count on you. \n<color=#FF7B00><size=100>YOU CAN MAKE IT!</size></color>";
        mainText.text = myText;
    }

    void LoadNextScene()
    {
        // Load the next scene here
        SceneManager.LoadScene("MainMap"); // Replace "NextSceneName" with your scene name
    }
}