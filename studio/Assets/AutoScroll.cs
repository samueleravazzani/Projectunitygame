using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using TMPro; 
using UnityEngine.SceneManagement;

//This script automatically scrolls the specified text upwards within a TextMeshProUGUI component until it reaches a certain boundary, at which point it loads another scene

public class AutoScroll : MonoBehaviour
{
    float speed = 60.0f; // Controls the speed of the text scrolling.
    float textPosBegin = -550.0f; //Defines the starting position of the text.
    float boundaryTextEnd = 2400.0f; //Represents the position where the text will stop scrolling.

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
        myText =
            "<size=100><color=#94FF4A>ENCHANTED FOREST</color></size> is an ancient magic land where nature and fantasy have always coexisted in harmony.\n";
        myText = myText +
                 "\nHowever, now something is <color=red>wrong</color>. A <color=red><size=100>mysterious force</color></size> is causing natural disasters that threatens to <color=red>destroy</color> this wonderful land.\n";
        myText = myText +
                 "\n<size=100><color=#fff239>YOU ARE THE CHOSEN ONE</size></color>, who can <color=#fff239>stop this evil force</color> and restore the balance! \nYou will have to work on yourself, but you have the skills to explore the forest and its secrets, face challenges to fight this enemy and <color=#fff239>save the world.</color>\n";
        myText = myText + "\nForest creatures count on you. \n<color=#FF7B00><size=100>YOU CAN MAKE IT!</size></color>";
        mainText.text = myText;
    }
    //Constructs a multiline string containing formatted text using TextMeshPro tags.

    void LoadNextScene()
    {
        // Load the next scene here
        SceneManager.LoadScene("Home"); 
    }
}