using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintDisplayScript : MonoBehaviour
{
    public int randNum;
    public GameObject hintDisp;
    public bool genHint = false;

    // Aggiungi una variabile privata per il riferimento all'Animator
    private Animator animator;
    
    void Awake()
    {
        // Otteni il riferimento all'Animator
        animator = hintDisp.GetComponent<Animator>();
        // Disabilita il GameObject all'inizio
        hintDisp.SetActive(false);
    }

    void Update()
    {
        if (genHint == false)
        {
            animator.Play("New State");
            genHint = true;
            StartCoroutine(HintTracker());
        }
    }

    IEnumerator HintTracker()
    {
        randNum = Random.Range(1, 4);
        if (randNum == 1)
        {
            hintDisp.GetComponent<Text>().text = "Press W,A,S,D for moving Up/Left/Down/Right respectively.";
        }
        if (randNum == 2)
        {
            hintDisp.GetComponent<Text>().text = "Find objects in the map to build poisons!";
        }
        if (randNum == 3)
        {
            hintDisp.GetComponent<Text>().text = "In the temple you are going to meditate and breath freely";
        }
        if (randNum == 4)
        {
            hintDisp.GetComponent<Text>().text = "Complete puzzles and find your best word and gain Badges!";
        }

        
        animator.Play("HintText");
        yield return new WaitForSeconds(0.8f);
        hintDisp.SetActive(false); // Disabilita il GameObject dopo l'animazione
        genHint = false;
    }

    public void HintDisplay()
    {
        hintDisp.SetActive(true); // Abilita il GameObject prima di iniziare l'animazione
    }
}