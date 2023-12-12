using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    // singleton
    public static GameManager instance;
    public GameObject player;
    public GameObject Bever;
    public string profile;
    
    // per il managing dei profiles
    public List<string> profileNames = new List<string>();
    public List<int> n_worldSsaved = new List<int>(); //todo
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
    public int previous_problem;
    /* TASK ATTUALE */
    public int task_index = 0;
    public int n_world_saved = 0;
    public DateTime savingtime;

    public float oldanxiety;
    public float oldclimate;
    public float oldliteracy; 
    [Space]
    [Header("Per l'Environment")]
    public int[] N_tospawn; // questo cambia durante il gioco /!\
    public float[] smokeRot;
    public float[] windRot;
    public float[] rainRot;
    public float[] level_anxiety = new float[] {0,0,0,0};

    public bool questionnairedone = false;
    public string scene;
    
    //counter to display how many times solved the task of the world for each problem 
    public int fireCounter = 0; 
    public int waterCounter = 0;
    public int airCounter = 0;
    public int plasticCounter = 0;

    public int[] problemsEverSorted = new int[5] {0, 0, 0, 0, 0};
    
    public int[] anxietyEverSorted = new int[4]{0, 0, 0, 0};
    public int[] literacyEverSorted = new int[3]{0, 0, 0};
    public int[] climateEverSorted = new int[6]{0, 0, 0, 0, 0, 0};
    
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
        if (task_index == 0)
        {
            /* A GIOCO PRONTO */ 
            N_tospawn = new int[4] {(int) sum_parameters * 70, (int) sum_parameters * 60, (int) sum_parameters * 50, 0};
            
            smokeRot = new float[4] {sum_parameters * 5, sum_parameters * 3, sum_parameters * 1, 0};
            windRot = new float[4] {sum_parameters * 40, sum_parameters * 24, sum_parameters * 10, 0};
            rainRot = new float[4] {sum_parameters * 75, sum_parameters * 52, sum_parameters * 30, 0};

            level_anxiety = new float[4] {anxiety, anxiety*2/3, anxiety/3, 0 };
            if (anxiety == 1) // 1:minimo dell'ansia -> non ha ansia
            {
                level_anxiety = new float[4] {0,0,0,0};
            }
        }
        
        // set del problema
        do
        { 
            problem_now = Random.Range(1, 4+1);
            problemsEverSorted[problem_now] = problem_now;
        } while (problem_now == previous_problem); // faccio in modo che il problema del mondo sia diverso dal precedente
        //// EnvironmentControl.instance.update_camera_bool = true;
        
        // setto il futuro previous_problem
        previous_problem = problem_now;
        
        // rimetto le task a 0
        task_index = 0;
        tasks_picked[0] = Random.Range(1, 3+1); // ANXIETY: 0 fatta, 1 breathing, 2 music, 3 word puzzle
        anxietyEverSorted[tasks_picked[0]] = tasks_picked[0];
        tasks_picked[1] = Random.Range(1, 2+1); // LITERACY: 0 fatta, 1 postions, 2 sources
        literacyEverSorted[tasks_picked[1]] = tasks_picked[1];
        tasks_picked[2] = Random.Range(1, 2+1); // CCS: 0 fatta, 1 quiz, 2 minigioco legato al problema del mondo
        climateEverSorted[tasks_picked[2]] = tasks_picked[2];
        
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
            
            //The following switch case handles how many times that problem has already been done by the player so to update the badges counter 
            switch (problem_now)
            {
                case 1:
                    fireCounter++;
                    break;
                case 2:
                    plasticCounter++;
                    break;
                case 3:
                    waterCounter++;
                    break;
                case 4:
                    airCounter++;
                    break;
                default:
                    Debug.Log("Problem now value not found");
                    break;
            }
        }

        float tochange = -1f;
        UpdateGMParameters(category, tochange);
    }
    
    public void TaskFailed(int category) // funzione da chiamare dopo che una task è fallita
    {
        float tochange = +0.5f;
        UpdateGMParameters(category, tochange);
    }
    
    /*public void PostQuestionnaire(float changeanxiety, float changeliteracy, float changeclimate) // funzione da chiamare dopo che faccio il questionario
    {
        float tochange = -1;
        StartCoroutine(waitformainmap(category, tochange));
    }*/

    public void UpdateGMParameters(int category, float tochange)
    {
        if (category == 0)
        {
            anxiety += tochange;
            anxiety = Mathf.Clamp(anxiety, 1, 10);
            //oldanxiety = anxiety; // preparo il futuro vecchio valore
        }
        if (category == 1)
        {
            literacy_inverted += tochange;
            literacy_inverted = Mathf.Clamp(literacy_inverted, 1, 10);
            //oldliteracy = literacy_inverted; // preparo il futuro vecchio valore
        }
        if (category == 2)
        {
            climate_change_skept += tochange;
            climate_change_skept = Mathf.Clamp(climate_change_skept, 1, 10);
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
            N_tospawn = N_tospawn,
            smokeRot = smokeRot,
            windRot = windRot,
            rainRot = rainRot,
            level_anxiety = level_anxiety,
            savingtime = DateTime.Now,
            questionnairedone = questionnairedone,
            scene = SceneManager.GetActiveScene().name,
            firecounter = fireCounter,
            watercounter = waterCounter,
            aircounter = airCounter,
            plasticcounter = plasticCounter,
            problemsEverSorted = problemsEverSorted,
            anxietyEverSorted = anxietyEverSorted,
            literacyEverSorted = literacyEverSorted,
            climateEverSorted = climateEverSorted,
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
            N_tospawn = saveObject.N_tospawn;
            smokeRot = saveObject.smokeRot;
            windRot = saveObject.windRot;
            rainRot = saveObject.rainRot;
            level_anxiety = saveObject.level_anxiety;
            savingtime = saveObject.savingtime;
            questionnairedone = saveObject.questionnairedone;
            fireCounter = saveObject.firecounter;
            waterCounter = saveObject.watercounter;
            airCounter = saveObject.aircounter;
            plasticCounter = saveObject.plasticcounter;
            problemsEverSorted = saveObject.problemsEverSorted;
            anxietyEverSorted = saveObject.anxietyEverSorted;
            literacyEverSorted = saveObject.literacyEverSorted;
            climateEverSorted = saveObject.climateEverSorted;
            
            // cambio scena e attivo il player
            if (!questionnairedone) // se il questionario non è ancora stato fatto -> carico la MainMap
            {
                SceneMaster.instance.ChangeSchene("MainMap", true, player.transform.position);
            }
            else // se il questionario è stato fatto -> segno che non è da rifare (2° volta da salvare il mondo) e riporto player e Bever in casa
            {
                questionnairedone = false;
                SceneMaster.instance.ChangeSchene("Home", true, new Vector3(-9.75f, 11f,0)); // posizione nella casa
                player.transform.position = new Vector3(-9.75f, 11f, 0);
                Bever.transform.position = new Vector3(-7.12f, 13.1f, 0);
            }

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
        public int[] N_tospawn; // questo cambia durante il gioco /!\
        public float[] smokeRot;
        public float[] windRot;
        public float[] rainRot;
        public float[] level_anxiety = new float[] {0,0,0,0};
        public DateTime savingtime;
        public bool questionnairedone;
        public string scene;
        public int firecounter;
        public int watercounter;
        public int aircounter;
        public int plasticcounter;
        public int[] problemsEverSorted;
        public int[] anxietyEverSorted = new int[4]{0, 0, 0, 0};
        public int[] literacyEverSorted = new int[3]{0, 0, 0};
        public int[] climateEverSorted = new int[6]{0, 0, 0, 0, 0, 0};
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
