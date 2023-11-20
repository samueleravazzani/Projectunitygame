using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
[CreateAssetMenu]
public class AlphabetData : ScriptableObject
{
    [System.Serializable]
    public class LetterData
    {
        public string letter;
        public Sprite image;

        public LetterData(string letter, Sprite image)
        {
            this.letter = letter;
            this.image = image;
        }
    }


    public List<LetterData> alphabetPlain = new List<LetterData>();

    public List<LetterData> alphabetNormal = new List<LetterData>();

    public List<LetterData> alphabetHighlighted = new List<LetterData>();

    public List<LetterData> alphabetWrong = new List<LetterData>();

    public AlphabetData()
    {
        // Inizializza le liste se sono null
        if (alphabetPlain == null)
            alphabetPlain = new List<LetterData>();

        if (alphabetNormal == null)
            alphabetNormal = new List<LetterData>();

        if (alphabetHighlighted == null)
            alphabetHighlighted = new List<LetterData>();

        if (alphabetWrong == null)
            alphabetWrong = new List<LetterData>();
    }
}
