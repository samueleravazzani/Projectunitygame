using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPopUp : MonoBehaviour
{
    public GameLevelData levelData;
  
    public void ClearGameData()
    {
        DataSaver.ClearGameData(levelData);
    }
    
}
