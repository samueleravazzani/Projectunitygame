using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSaver : MonoBehaviour
{
    public static int ReadCategoryCurrentIndexValues(string profile, string categoryName)
    {
        string key = profile + "_" + categoryName;
        var value = -1;
        if (PlayerPrefs.HasKey(key))
            value = PlayerPrefs.GetInt(key);
        return value;
    }

    public static void SaveCategoryData(string profile, string categoryName, int currentIndex)
    {
        string key = profile + "_" + categoryName;
        Debug.Log(key);
        PlayerPrefs.SetInt(key, currentIndex);
        PlayerPrefs.Save();
    }

    public static void ClearGameData(string profile, GameLevelData levelData)
    {
        foreach (var data in levelData.data)
        {
            string key = profile + "_" + data.categoryName;
            PlayerPrefs.SetInt(key, -1);
        }
        
        // Imposta un valore specifico per il primo elemento della data
        string firstKey = profile + "_" + levelData.data[0].categoryName;
        PlayerPrefs.SetInt(firstKey, 0);
        PlayerPrefs.Save();
    }
}
