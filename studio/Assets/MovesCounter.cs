using UnityEngine;
using System.Collections.Generic;
using TMPro; 

public class MovesCounter : MonoBehaviour
{
    private int maxMoves = 1;
    public int remainingMoves;
    public TMPro.TextMeshProUGUI movesText; 
    public GameObject key1;
    public GameObject popup;
    public bool hasMoved = false;
    private List<AudioSource> audioSources = new List<AudioSource>();

    void Start()
    {
        remainingMoves = maxMoves;
        UpdateMovesText();
    }

    void Update()
    {
        if (key1 != null && Input.GetKeyDown(KeyCode.Mouse0) && !hasMoved)
        {
            remainingMoves--;
            UpdateMovesText();
            hasMoved = true;
            Invoke("ShowPopup", 1f);
        }
    }

    public void UpdateMovesText()
    {
        if (movesText != null)
        {
            movesText.text =  remainingMoves.ToString();
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