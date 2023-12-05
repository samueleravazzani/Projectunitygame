using UnityEngine;
using System.IO;
using System.Runtime.Serialization; // to work with files

public static class SaveSystem // static class can't be instantiated
{
    public static readonly string SAVE_FOLEDER = "/Users/samueleravazzani/LOCAL Politecnico di Milano/Local/2 anno M/E-health Methods and Applications/Project/Projectunitygame/studio/Assets" + "/Saves/"; // this will be our save folder
                                                // Application.persistentDataPath
    
    public static void InitializeSaveFolder()
    {
        // check che esiste la directory
        if (!Directory.Exists(SAVE_FOLEDER)) // se la cartella non esiste
        {
            // create save folder
            Directory.CreateDirectory(SAVE_FOLEDER);
        }
    }
    
    public static void Save(string profile, string saveString)
    {
        // pezzo di codice per salvare più versioni successive in file diversi
        /* while (File.Exists("save" + profile + ".txt"))
        {
            saveNumber++;
        } */
        
        File.WriteAllText(SAVE_FOLEDER + profile + ".json", saveString);
        // se esiste già quel file -> lo sovrascrive
    }

    public static string Load(string profile)
    {
        InitializeSaveFolder();
        DirectoryInfo directoryInfo = new DirectoryInfo(SAVE_FOLEDER); // create directory info nel SAVE_FOLDER path
        FileInfo[] saveFiles = directoryInfo.GetFiles(".json"); // returns an array of file info, all the files of type .txt
        FileInfo fileToLoad = null;
        foreach (FileInfo fileInfo in saveFiles) // cerca il file + recente
        {
            if (fileInfo.Name == profile)
            {
                fileToLoad = fileInfo;
            }
        }

        if (fileToLoad != null) // se != null -> abbiamo un most recent file -> esiste il file da caricare
        {
            string saveString = File.ReadAllText(SAVE_FOLEDER + fileToLoad.Name);
            return saveString;
        }
        else
        {
            return null;
        }
        
        
        // PARTE PER CARICARE IL FILE PIù recente
        /*
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
        } */
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
