using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // singleton
    public static GameManager instance;

    private static readonly string SAVE_FOLEDER = Application.persistentDataPath + "/Saves/"; // this will be our save folder

    // parameters that control environment effects
    public float anxiety = 5; // settato in SliderControl
    public float literacy_inverted = 5; // settato in SliderControl
    public float climate_change_skept = 5; // settato in SliderControl
    public float sum_parameters = 15; // settato in SliderControl
    public static int[] tasks_to_do = new int[3]; // vettore di 3 int: [0] per anxiety, [1] per literacy, [2] per climate change
    // in ciascuno di questi 3 elementi pesco un numero casuale che definisce la task
    // ANXIETY: 0 fatta, 1 breathing, 2 music, 3 word puzzle
    // LITERACY: 0 fatta, 1 postions, 2 sources
    // CCS: 0 fatta, 1 quiz, 2 minigioco legato al problema del mondo.
    
    /* PROBLEMA ATTUALE */
    public int problem_now;
    /* TASK ATTUALE */
    public int task_index = 0;
    void Awake()
    {
        // singleton
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        
        // Save
        SaveSystem.Initialize();
        
        /* TESTING CODE
        SaveObject saveObject = new SaveObject()
        {
            anxiety = 5f,
        };
        string json = JsonUtility.ToJson(saveObject);

        SaveObject loadedSaveObject = JsonUtility.FromJson<SaveObject>(json); */
        
        
        //
        sum_parameters = 15;
        /* PROBLEMA ATTUALE */
        if (task_index == 0)
        {
            problem_now = Random.Range(1, 4);
        }
    }

    public void Save()
    {
        // creo un SaveObject in cui setto le cose da salvare del SaveObject rispetto alle variabili del GameManager etc.;
        // che poi converto in un JSON con JsonUtility.ToJson
        // e poi chiamo la funzione SaveSystem.Save passandogli la stringa json da salvare
        
        SaveObject saveObject = new SaveObject
        {
            anxiety = anxiety,
        };
        
        string json = JsonUtility.ToJson(saveObject);
        SaveSystem.Save(json);
    }

    public void Load()
    {
        // carico i dati dalla funzione SaveSystem.Load(), che mi restituisce una stringa ->
        // -> se non è nulla la converto in un SaveObject che contiene tutte le cose 
        string saveString = SaveSystem.Load();
        if (saveString != null)
        {
            SaveObject saveObject =JsonUtility.FromJson<SaveObject>(saveString);
        }
        
        
        /* CODICE PER CARICARE LE VARIE COSE */
        
        
        /*if (File.Exists(SAVE_FOLEDER + "/save.txt"))
        {
            string saveString = File.ReadAllText(SAVE_FOLEDER + "/save.txt");

            SaveObject saveObject =JsonUtility.FromJson<SaveObject>(saveString);
            
        } */
    }

    private class SaveObject // è la classe che creo per contenere tutte le info che andranno salvate in un JSON
    {
        public float anxiety, literacy_inverted, climate_change_skepticism, sum_parameters;
        public Vector3 position;
        public int problem_now;
        public int previous_problem;
        public int task_index;
        public string[] task_done;
        public int[] task_picked;
    }

    /* 
    public void SaveData()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        anxiety = data.anxiety;
        literacy_inverted = data.literacy_inverted;
        climate_change_skept = data.climate_change_skepticism;
        sum_parameters = anxiety + literacy_inverted + climate_change_skept;
        Vector3 position;
        // position.x = data.position[0];
        // position.y = data.position[1];
        // position.z = data.position[2];

        player.transform.position = position;

        // problem_now = ;
        // task_index = ;
        // task_done = ;
        // task_picked = ;
    }
    */
}
