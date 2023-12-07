using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WordChecker : MonoBehaviour
{
    public GameData currentGameData;
    public GameLevelData gameLevelData;
    private int count=0;
    private string _word;
    private int _assignedPoint = 0;
    private int _completedWords = 0;
    private List<int> _correctSquareList = new List<int>();

    private void OnEnable()
    {
        GameEvents.OnCheckSquare += SquareSelected;
        GameEvents.OnClearSelection += ClearSelection;
        GameEvents.OnLoadNextLevel += LoadNextGameLevel;
    }

    private void OnDisable()
    {
        GameEvents.OnCheckSquare -= SquareSelected;
        GameEvents.OnClearSelection -= ClearSelection;
        GameEvents.OnLoadNextLevel -= LoadNextGameLevel;
    }

    private void LoadNextGameLevel()
    {
        SceneManager.LoadScene("PuzzleGame1");
    }

    void Start()
    {
        currentGameData.selectedBoardData.ClearData();
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
            if (_word == searchingWord.Word && searchingWord.Found==false)
            {
                count++;
                if (count == currentGameData.selectedBoardData.SearchWords.Count)
                {
                    searchingWord.Found = true;
                    GameEvents.CorrectWordMethod(_word, _correctSquareList);
                    _completedWords++;
                    CheckBoardCompleted();
                    ClearSelection();
                    return;
                }
                searchingWord.Found = true;
                GameEvents.CorrectWordMethod(_word, _correctSquareList);
                _completedWords++;
                CheckBoardCompleted();
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

    private void CheckBoardCompleted()
    {
        
        bool loadNextCategory = false;
        if (currentGameData.selectedBoardData.SearchWords.Count == _completedWords)
        {
            var categoryName = currentGameData.selectedCategoryName;
            var currentBoardIndex = DataSaver.ReadCategoryCurrentIndexValues(categoryName);
            var nextBoardIndex = -1;
            var currentCategoryIndex = 0;
            bool readNextLevelName = false;

            for (int index = 0; index < gameLevelData.data.Count; index++)
            {
                if (readNextLevelName)
                {
                    nextBoardIndex = DataSaver.ReadCategoryCurrentIndexValues(gameLevelData.data[index].categoryName);
                    readNextLevelName = false;
                }

                if (gameLevelData.data[index].categoryName == categoryName)
                {
                    readNextLevelName = true;
                    currentCategoryIndex = index;
                }
            }

            var currentLevelSize = gameLevelData.data[currentCategoryIndex].boardData.Count;
            if (currentBoardIndex < currentLevelSize)
            {
                currentBoardIndex++;
            }
            DataSaver.SaveCategoryData(categoryName,currentBoardIndex);

            if (currentBoardIndex >= currentLevelSize)
            {
                currentCategoryIndex++;
                if (currentCategoryIndex < gameLevelData.data.Count)
                {
                    categoryName = gameLevelData.data[currentCategoryIndex].categoryName;
                    currentBoardIndex = 0;
                    loadNextCategory = true;

                    if (nextBoardIndex <= 0)
                    {
                        DataSaver.SaveCategoryData(categoryName, currentBoardIndex);
                    }
                }
                else
                {
                    // Carica la scena "PuzzleGame"
                    SceneManager.LoadScene("PuzzleGame");

                    // Trova il bottone "Quit" nella scena e lo attiva
                    GameObject quitButton = GameObject.Find("Quit_TaskDone");
                    GameObject quitButton1 = GameObject.Find("Quit");// Assicurati che il nome sia corretto
                    if (quitButton != null)
                    {
                        quitButton.SetActive(true);
                        quitButton.SetActive(false);
                    }
                }
            }

            else
            {
                GameEvents.BoardCompletedMethod();
            }

            if (loadNextCategory)
                GameEvents.UnlockNextCategoryMethod();
        }
    }
}
