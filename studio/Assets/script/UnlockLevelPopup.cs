using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UnlockLevelPopup : MonoBehaviour
{
    [System.Serializable]
    public struct CategoryName
    {
        public string name;
        public Sprite sprite;
    };

    public GameData currentGameData;
    public List<CategoryName> categoryNames;
    public GameObject winPopup;
    public Image categoryNameImage;
    public GameObject gameOverPopup;

    private int anxietyLevel; // Aggiunto il riferimento al livello di ansia

    void Start()
    {
        winPopup.SetActive(false);
        gameOverPopup.SetActive(false);
        GameEvents.OnUnlockNextCategory += OnUnlockNextCategory;
        anxietyLevel = PlayerPrefs.GetInt("AnxietyLevel", 1);
    }

    private void OnDisable()
    {
        GameEvents.OnUnlockNextCategory -= OnUnlockNextCategory;
    }

    private void OnUnlockNextCategory()
    {
        // Verifica il livello di ansia
        if (anxietyLevel == 1)
        {
            // Se il livello di ansia è 1, controlla se la categoria corrente è Level 1
            if (currentGameData.selectedCategoryName == "Level 1")
            {
                gameOverPopup.SetActive(true);
                winPopup.SetActive(false);
            }
        }
        else if (anxietyLevel == 2)
        {
            // Se il livello di ansia è 2, controlla le categorie Level 1 e Level 2
            if (currentGameData.selectedCategoryName == "Level 1")
            {
                winPopup.SetActive(true);
                categoryNameImage.sprite = GetCategorySpriteByName("Level 1");
            }
            else if (currentGameData.selectedCategoryName == "Level 2")
            {
                winPopup.SetActive(false);
                gameOverPopup.SetActive(true);
            }
        }
        else if (anxietyLevel == 3)
        {
            // Se il livello di ansia è 3, controlla le categorie Level 1, Level 2 e Level 3
            if (currentGameData.selectedCategoryName == "Level 1")
            {
                winPopup.SetActive(true);
                categoryNameImage.sprite = GetCategorySpriteByName("Level 1");
            }
            else if (currentGameData.selectedCategoryName == "Level 2")
            {
                winPopup.SetActive(true);
                categoryNameImage.sprite = GetCategorySpriteByName("Level 2");
            }
            else if (currentGameData.selectedCategoryName == "Level 3")
            {
                winPopup.SetActive(false);
                gameOverPopup.SetActive(true);
            }
        }
    }

// Metodo per ottenere lo sprite della categoria in base al nome
    private Sprite GetCategorySpriteByName(string categoryName)
    {
        foreach (var category in categoryNames)
        {
            if (category.name == categoryName)
            {
                return category.sprite;
            }
        }

        return null;
    }
}
