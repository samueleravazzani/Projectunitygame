using UnityEngine;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;

public class MovesCounter : MonoBehaviour
{
    private int maxMoves;
    public int remainingMoves;
    public TMPro.TextMeshProUGUI movesText;
    public List<GameObject> keyObjects = new List<GameObject>(); 
    public GameObject popup;
    public bool hasMoved = false;
    private List<AudioSource> audioSources = new List<AudioSource>();

    void Start()
    {
        int ccsLevel = PlayerPrefs.GetInt("CCSLevel", 1);
        GameObject key = GameObject.Find("key");
        if (key != null)
        {
            keyObjects.Add(key);
        }
        GameObject key1 = GameObject.Find("key (1)");
        if (key1 != null)
        {
            keyObjects.Add(key1);
        }
        GameObject key2 = GameObject.Find("key (2)");
        if (key2 != null)
        {
            keyObjects.Add(key2);
        }
        GameObject key3 = GameObject.Find("key (3)");
        if (key3 != null)
        {
            keyObjects.Add(key3);
        }
        GameObject key4 = GameObject.Find("key (4)");
        if (key4 != null)
        {
            keyObjects.Add(key4);
        }
        
        switch (ccsLevel)
        {
            case 1:
                maxMoves = 1;
                break;
            case 2:
                maxMoves = 4;
                break;
            case 3:
                maxMoves = 7;
                break;
            default:
                break;
        }

        remainingMoves = maxMoves;
        UpdateMovesText();
    }

    public void UpdateMovesText()
    {
        if (movesText != null)
        {
            movesText.text = remainingMoves.ToString();
        }
    }

    void ShowPopup()
    {
        if (popup != null)
        {
            popup.SetActive(true);
        }
    }

    public void ClosePopup()
    {
        if (popup != null)
        {
            popup.SetActive(false);
        }
    }
}