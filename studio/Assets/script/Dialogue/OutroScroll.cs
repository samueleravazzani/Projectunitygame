using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using TMPro; 
using UnityEngine.SceneManagement;

//This script automatically scrolls the specified text upwards within a TextMeshProUGUI component until it reaches a certain boundary, at which point it loads another scene

public class OutroScroll : MonoBehaviour
{
    float speed = 75.0f; // Controls the speed of the text scrolling.
    float textPosBegin = -550.0f; //Defines the starting position of the text.
    float boundaryTextEnd = 1450.0f; //Represents the position where the text will stop scrolling.

    private RectTransform myGorectTransform; //Reference to the RectTransform component attached to this GameObject.

    [SerializeField] private TextMeshProUGUI mainText; //Reference to the TextMeshProUGUI component to display text.

    private string myText;

    // Start is called before the first frame update
    void Start()
    {
        myGorectTransform = gameObject.GetComponent<RectTransform>();
        HandleCustomizeText();
        StartCoroutine(AutoscrolLText());
    }
    //Retrieves the RectTransform component attached to the GameObject.
    //Calls HandleCustomizeText() to set up the initial text.
    //Initiates the coroutine AutoscrolLText() for text scrolling

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
    
    // Scrolls the text upwards until it reaches the defined boundaryTextEnd
    // Uses myGorectTransform.Translate(Vector3.up * speed * Time.deltaTime) to move the text upwards.
    // Upon reaching the boundary, it initiates the LoadNextScene() method to load another scene.

    void HandleCustomizeText()
    {
        /* THANK YOU FOR PLAYING
           The Enchanted Forest!           
           
           Made by:
           Roberto di Giacomo
           Estanislao Medrano Campbell
           Samuele Ravazzani
           Stefano Rossi
           Federica Maria Storti*/
        myText = "<size=100><color=#fff239>THANK YOU FOR PLAYING</size></color>";
        myText += "\n<size=100><color=#94FF4A>The Enchanted Forest!</color></size>";
        myText += "\nContinue playing to discover new challenges, minigames and make the game evolve with you!";
        myText += "\n \n<color=#FF7B00><size=80>Made by:</size></color>";
        myText +=
            "\nRoberto di Giacomo\nEstanislao Medrano Campbell\nSamuele Ravazzani\nStefano Rossi\nFederica Maria Storti";
        
        mainText.text = myText;
    }
    //Constructs a multiline string containing formatted text using TextMeshPro tags.

    void LoadNextScene()
    {
        SceneManager.LoadScene("Start_Scene"); 
    }
}