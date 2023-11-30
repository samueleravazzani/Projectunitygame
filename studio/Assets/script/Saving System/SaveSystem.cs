using UnityEngine;
using System.IO;
using System.Runtime.Serialization; // to work with files

public static class SaveSystem // static class can't be instantiated
{
    private static readonly string SAVE_FOLEDER = Application.persistentDataPath + "/Saves/"; // this will be our save folder

    public static void Initialize()
    {
        // check che esiste la riectory
        if (!Directory.Exists(SAVE_FOLEDER)) // se la cartella non esiste
        {
            // create save folder
            Directory.CreateDirectory(SAVE_FOLEDER);
        }
    }
    
    public static void Save(string saveString)
    {
            
    }
    
    
    
    /* public static void SavePlayer(GameManager gm, string name)
    {
        string path = Application.persistentDataPath + "/" + name + ".json";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(gm);

        Formatter.Serialize(stream, data);
        stream.Close(); 
        
        
    }

    public static PlayerData LoadPlayer(string name)
    {
        string path = Application.persistentDataPath + "/" + name + ".json";
        if (File.Exists(path))
        {
            FileStream stream = new FileStream(path, FileMode.Open);
            // PlayerData data = (PlayerData)stream;
            stream.Close();

            return data;
            return null;
        }
        else
        {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    } */
}
