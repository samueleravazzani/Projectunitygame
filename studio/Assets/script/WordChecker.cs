using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordChecker : MonoBehaviour
{
    public GameData currentGameData;
    private int count=0;
    private string _word;
    private int _assignedPoint = 0;
    private int _completedWords = 0;
    private List<int> _correctSquareList = new List<int>();

    private void OnEnable()
    {
        GameEvents.OnCheckSquare += SquareSelected;
        GameEvents.OnClearSelection += ClearSelection;
    }

    private void OnDisable()
    {
        GameEvents.OnCheckSquare -= SquareSelected;
        GameEvents.OnClearSelection -= ClearSelection;
    }

    void Start()
    {
        _assignedPoint = 0;
        _completedWords = 0;
    }

    void Update()
    {
        
    }

    private void SquareSelected(string letter, Vector3 squarePosition, int squareIndex)
    {
        if (_assignedPoint == 0)
        {
            GameEvents.SelectSquareMethod(squarePosition);
            _word += letter;
            _correctSquareList.Add(squareIndex);
            CheckWord();
        }
    }

    private void CheckWord()
    {
        foreach (var searchingWord in currentGameData.selectedBoardData.SearchWords)
        {
            if (_word == searchingWord.Word)
            {
                count++;
                if (count == currentGameData.selectedBoardData.SearchWords.Count)
                {
                    GameEvents.CorrectWordMethod(_word, _correctSquareList);
                    ClearSelection();
                    GameEvents.OnGameOverMethod();
                    return;
                }
                GameEvents.CorrectWordMethod(_word, _correctSquareList);
                ClearSelection();
                return;
                
            }
        }
    }

    private void ClearSelection()
    {
        _assignedPoint = 0;
        _correctSquareList.Clear();
        _word = string.Empty;
    }
}
