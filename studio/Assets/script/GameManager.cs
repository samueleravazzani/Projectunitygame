using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // singleton
    public static GameManager instance;
    public string profile;

    // parameters that control environment effects
    public float anxiety = 5; // settato in SliderControl
    public float literacy_inverted = 5; // settato in SliderControl
    public float climate_change_skept = 5; // settato in SliderControl
    public float sum_parameters = 15; // settato in SliderControl
    public static int[] tasks_picked = new int[3]; // vettore di 3 int: [0] per anxiety, [1] per literacy, [2] per climate change
    // in ciascuno di questi 3 elementi pesco un numero casuale che definisce la task
    // ANXIETY: 0 fatta, 1 breathing, 2 music, 3 word puzzle
    // LITERACY: 0 fatta, 1 postions, 2 sources
    // CCS: 0 fatta, 1 quiz, 2 minigioco legato al problema del mondo.
    
    /* PROBLEMA ATTUALE */
    public int problem_now;
    public int previous_problem;
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
        SaveSystem.InitializeSaveFolder();
    }


    public void InitializeGameFirstTime() // da chiamare solo quando si crea il profilo per la prima volta
    {
        InitializeGame();
    }

    public void InitializeGame()
    {
        // set del problema
        do
        { 
            problem_now = Random.Range(1, 4);
        } while (problem_now == previous_problem); // faccio in modo che il problema del mondo sia diverso dal precedente
        EnvironmentControl.instance.update_camera_bool = true;
        // setto il futuro previous_problem
        previous_problem = problem_now;
        
        // rimetto le task a 0
        task_index = 0;
        tasks_picked[0] = Random.Range(1, 3); // ANXIETY: 0 fatta, 1 breathing, 2 music, 3 word puzzle
        tasks_picked[1] = Random.Range(1, 2); // LITERACY: 0 fatta, 1 postions, 2 sources
        tasks_picked[2] = Random.Range(1, 2); // CCS: 0 fatta, 1 quiz, 2 minigioco legato al problema del mondo
        
        // initiliaze position of the player
        //todo: initialize position of the player
    }

    public void TaskDone(int task_done) // funzione da chiamare dopo che una task è completata
    {
        task_index++; // task fatta
        tasks_picked[task_done] = 0; // segno che l'ho fatta
        EnvironmentControl.instance.update_camera_bool = true; // aggiorno l'environment
    }
    
    public void Save()
    {
        // creo un SaveObject in cui setto le cose da salvare del SaveObject rispetto alle variabili del GameManager etc.;
        // che poi converto in un JSON con JsonUtility.ToJson
        // e poi chiamo la funzione SaveSystem.Save passandogli la stringa json da salvare
        
        // creo i dati da salvare
        SaveObject saveObject = new SaveObject
        {
            anxiety = anxiety,
            literacy_inverted = literacy_inverted,
            climate_change_skepticism = climate_change_skept,
            sum_parameters = anxiety+literacy_inverted+climate_change_skept,
            position = new Vector3(0,0,0), //todo /!\ position to change
            problem_now = problem_now,
            previous_problem = problem_now,
            task_index = task_index,
            tasks_picked = tasks_picked,
            
        };
        
        string json = JsonUtility.ToJson(saveObject);
        SaveSystem.Save(profile, json); // passo il profilo + i dati
    }

    public void Load(string profile)
    {
        // carico i dati dalla funzione SaveSystem.Load(), che mi restituisce una stringa ->
        // -> se non è nulla la converto in un SaveObject che contiene tutte le cose 
        string saveString = SaveSystem.Load(profile);
        if (saveString != null)
        {
            SaveObject saveObject =JsonUtility.FromJson<SaveObject>(saveString);
            anxiety = saveObject.anxiety;
            literacy_inverted = saveObject.literacy_inverted;
            climate_change_skept = saveObject.climate_change_skepticism;
            sum_parameters = anxiety + literacy_inverted + climate_change_skept;
            // position = new Vector3(0,0,0), //todo /!\ position to change
            problem_now = saveObject.problem_now;
            previous_problem = saveObject.problem_now;
            task_index = saveObject.task_index;
            tasks_picked = saveObject.tasks_picked;
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
        public int[] tasks_picked;
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
