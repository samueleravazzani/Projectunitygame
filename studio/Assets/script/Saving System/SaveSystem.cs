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
        int saveNumber = 1;
        while (File.Exists("save" + saveNumber + ".txt"))
        {
            saveNumber++;
        }
        File.WriteAllText(SAVE_FOLEDER + "/save" + saveNumber + ".txt", saveString);
    }

    public static string Load()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(SAVE_FOLEDER); // create directory info nel SAVE_FOLDER path
        FileInfo[] saveFiles = directoryInfo.GetFiles(".txt"); // returns an array of file info, all the files of type .txt
        FileInfo mostRecentFile = null;
        foreach (FileInfo fileInfo in saveFiles) // cerca il file + recente
        {
            if (mostRecentFile == null) // se è null (e.g. la prima volta (?) ) -> setto l'attuale file trovato come mostRecent
            {
                mostRecentFile = fileInfo; 
            }
            else
            {
                if (fileInfo.LastWriteTime > mostRecentFile.LastWriteTime) // se fileInfo non è null e sto guardando un file che è più recente -> lo setto come file
                {
                    mostRecentFile = fileInfo;
                }
            }
        }

        if (mostRecentFile != null) // se != null -> abbiamo un most recent file -> esiste il file più recenteda caricare
        {
            string saveString = File.ReadAllText(SAVE_FOLEDER + mostRecentFile.FullName);
            return saveString;
        }
        else
        {
            return null;
        }
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
