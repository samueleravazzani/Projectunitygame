using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectPuzzleButton : MonoBehaviour
{
    public GameData gameData;
    public GameLevelData levelData;
    public Text categoryText;
    public Image progressBarFilling;

    private string gameSceneName = "PuzzleGame1";
    private bool _levelLocked;
    
    void Start()
    {
        _levelLocked = false;
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
        UpdateButtonInformation();
        if (_levelLocked)
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
        }
        
    }
    
    void Update()
    {
        
    }

    private void UpdateButtonInformation()
    {
        var currentIndex = -1;
        var totalBoards = 0;
        string profile = GameManager.instance.profile;

        foreach (var data in levelData.data)
        {
            if (data.categoryName == gameObject.name)
            {
                currentIndex = DataSaver.ReadCategoryCurrentIndexValues(profile, gameObject.name);
                totalBoards = data.boardData.Count;
            
                if (levelData.data[0].categoryName == gameObject.name && currentIndex < 0)
                {
                    DataSaver.SaveCategoryData(profile, levelData.data[0].categoryName, 0);
                    currentIndex = DataSaver.ReadCategoryCurrentIndexValues(profile, gameObject.name);
                    totalBoards = data.boardData.Count; 
                }
            }
        }

        if (currentIndex == -1)
            _levelLocked = true;

        categoryText.text = _levelLocked ? string.Empty : (currentIndex.ToString() + "/" + totalBoards.ToString());
        progressBarFilling.fillAmount = (currentIndex > 0 && totalBoards > 0) ? ((float)currentIndex / (float)totalBoards) : 0f;
    
        // Imposta l'interattività del bottone in base al livello bloccato o sbloccato
        var button = GetComponent<Button>();
        button.interactable = !_levelLocked;
    }


    private void OnButtonClick()
    {
        gameData.selectedCategoryName = gameObject.name;
        SceneManager.LoadScene(gameSceneName);
    }
}
