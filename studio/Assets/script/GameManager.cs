using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // singleton
    public static GameManager instance;
    public GameObject player;
    public GameObject Bever;
    public string profile;
    
    // per il managing dei profiles
    public List<string> profileNames = new List<string>();
    public bool profile_created = false;

    // parameters that control environment effects
    public float anxiety = 5; // settato in SliderControl
    public float literacy_inverted = 5; // settato in SliderControl
    public float climate_change_skept = 5; // settato in SliderControl
    public float sum_parameters = 15; // settato in SliderControl
    [Header("Task da fare: usare per debuggare")]
    [Header("El 0: anxiety, El 1: literacy, El2: climatechange")]
    public int[] tasks_picked = new int[3] {0,0,0}; // vettore di 3 int: [0] per anxiety, [1] per literacy, [2] per climate change
    // in ciascuno di questi 3 elementi pesco un numero casuale che definisce la task
    // ANXIETY: 0 fatta, 1 breathing, 2 music, 3 word puzzle
    // LITERACY: 0 fatta, 1 postions, 2 sources
    // CCS: 0 fatta, 1 quiz, 2 minigioco legato al problema del mondo.
    
    [Space]
    /* PROBLEMA ATTUALE */
    public int problem_now;
    private int previous_problem;
    /* TASK ATTUALE */
    public int task_index = 0;
    public int n_world_saved = 0;

    public float oldanxiety;
    public float oldclimate;
    public float oldliteracy; 
    
    
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

        ActivatePlayer(false);
    }


    public void InitializeGameFirstTime() // da chiamare solo quando si crea il profilo per la prima volta
    {
        if (!profile_created)
        {
            // aggiungo il player alla lista dei players
            profileNames.Add(profile);
            n_world_saved = 0;
            SaveProfileList();
            profile_created = true;
            oldanxiety = anxiety;
            oldliteracy = literacy_inverted;
            oldclimate = climate_change_skept;
        }

        // inizializzo il gioco
        InitializeGame(); 
    }

    public void InitializeGame()
    {
        // set del problema
        do
        { 
            problem_now = Random.Range(1, 4);
        } while (problem_now == previous_problem); // faccio in modo che il problema del mondo sia diverso dal precedente
        //// EnvironmentControl.instance.update_camera_bool = true;
        
        // setto il futuro previous_problem
        previous_problem = problem_now;
        
        // rimetto le task a 0
        task_index = 0;
        tasks_picked[0] = Random.Range(1, 3); // ANXIETY: 0 fatta, 1 breathing, 2 music, 3 word puzzle
        tasks_picked[1] = Random.Range(1, 2); // LITERACY: 0 fatta, 1 postions, 2 sources
        tasks_picked[2] = Random.Range(1, 2); // CCS: 0 fatta, 1 quiz, 2 minigioco legato al problema del mondo
        
        // initiliaze position of the player in the MainMap /!\
        //player.transform.position = new Vector3(-1.79f, 1.79f, 0);
    }

    public void TaskDone(int category) // funzione da chiamare dopo che una task è completata
    {
        task_index++; // task fatta
        tasks_picked[category] = 0; // segno che l'ho fatta
        //EnvironmentControl.instance.update_camera_bool = true; // aggiorno l'environment
        if (task_index == 3)
        {
            n_world_saved++;
        }

        float tochange = -1f;
        waitformainmap(category, tochange);
    }
    
    public void TaskFailed(int category) // funzione da chiamare dopo che una task è fallita
    {
        float tochange = +0.2f;
        waitformainmap(category, tochange);
    }
    
    /*public void PostQuestionnaire(float changeanxiety, float changeliteracy, float changeclimate) // funzione da chiamare dopo che faccio il questionario
    {
        float tochange = -1;
        StartCoroutine(waitformainmap(category, tochange));
    }*/

    public void waitformainmap(int category, float tochange)
    {
        if (category == 0)
        {
            anxiety += tochange;
            //oldanxiety = anxiety; // preparo il futuro vecchio valore
        }
        if (category == 1)
        {
            literacy_inverted += tochange;
            //oldliteracy = literacy_inverted; // preparo il futuro vecchio valore
        }
        if (category == 2)
        {
            climate_change_skept += tochange;
            //oldclimate = climate_change_skept; // preparo il futuro vecchio valore
        }
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
            position = player.transform.position,
            problem_now = problem_now,
            previous_problem = previous_problem,
            task_index = task_index,
            tasks_picked = tasks_picked,
            n_world_saved = n_world_saved,
            profile_created = profile_created,
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
            // prendo i dati
            anxiety = saveObject.anxiety;
            oldanxiety = anxiety;
            literacy_inverted = saveObject.literacy_inverted;
            oldliteracy = literacy_inverted;
            climate_change_skept = saveObject.climate_change_skepticism;
            oldclimate = climate_change_skept;
            sum_parameters = anxiety + literacy_inverted + climate_change_skept;
            player.transform.position = saveObject.position;
            problem_now = saveObject.problem_now;
            previous_problem = saveObject.previous_problem;
            task_index = saveObject.task_index;
            tasks_picked = saveObject.tasks_picked;
            n_world_saved = saveObject.n_world_saved;
            profile_created = saveObject.profile_created;
            // cambio scena e attivo il player
            SceneMaster.instance.ChangeSchene("MainMap", true, player.transform.position);
            ActivatePlayer(true);
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
        public int n_world_saved;
        public bool profile_created;
    }

    public void ActivatePlayer(bool state)
    {
        player.GetComponent<playerMovement>().enabled = state;
        player.gameObject.SetActive(state);
        Bever.GetComponent<Bever>().enabled = state;
        Bever.gameObject.SetActive(state);
    }
    
    
    
    public void SaveProfileList()
    {
        string[] array = profileNames.ToArray();
        string json = JsonUtility.ToJson(new Serialization<string>(array));
        SaveSystem.Save("profiles",json);
    }
    
    
    
    [System.Serializable]
    public class Serialization<T>
    {
        [SerializeField]
        private T[] array;

        public Serialization(T[] array)
        {
            this.array = array;
        }

        public T[] ToArray()
        {
            return array;
        }
    }
    
    /*
    [System.Serializable]
    public class Serialization<T>
    {
        [SerializeField]
        List<T> target;
        public List<T> ToList() { return target; }

        public Serialization(List<T> target)
        {
            this.target = target;
        }
    } */
    
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
