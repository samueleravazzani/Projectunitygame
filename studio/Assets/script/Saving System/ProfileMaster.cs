using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProfileMaster : MonoBehaviour
{
    
    public static ProfileMaster instance;
    public Button profile_prefab;
    public Transform scrollView_Content;

    public Image input;
    public Button create;
    public Button delete;
    public Image blackBackground;
    public Image warning;

    private string newname;
    // private float[] button_position = new float[]  {376.5f, -543.5f};
    private float buttonWidth = 520f;
    private float button_offset = 405f;
    void Awake()
    {
        // singleton
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
    }

    private void Start()
    {
        // PER CANCELLARE TUTTO (PUZZLE_GAME): PlayerPrefs.DeleteAll();
        SpawnProfiles();
        blackBackground.gameObject.SetActive(false);
    }

    public void NewProfile()
    {
        input.gameObject.SetActive(true);
        create.gameObject.SetActive(true);
        delete.gameObject.SetActive(false);
    }

   public void Create()
   {
       // verifico che non ci siano profili con lo stesso nome
       newname = input.GetComponentInChildren<TMP_InputField>().text;
       foreach (string name in GameManager.instance.profileNames)
       {
           if (newname == "profiles")
           {
               Debug.Log("Name not usable");
               StartCoroutine(NameNotUsable());
               return;
           }
           if (name == newname)
           {
               Debug.Log("Error: a profile with this name already exists. Change name");
               StartCoroutine(ProfileNotAvailable());
               return;
           }
       }
        
       GameManager.instance.profile = newname; // passo al GameManager il nome del player.
       GameManager.instance.n_world_saved = 0; // setto a 0 il n° di volte in cui il mondo è stato salvato
       // Se il player finisce il tutorial: (esce dalla casa -> salvo il suo profilo nell'elenco dei profili e il suo profilo.
       SceneManager.LoadScene("ParameterSliders");
   }
   public void DeleteProfile()
   {
       input.gameObject.SetActive(true);
       create.gameObject.SetActive(false);
       delete.gameObject.SetActive(true);
   }


   public void Delete()
   {
       bool found = false;
       string name_to_delete = input.GetComponentInChildren<TMP_InputField>().text;

       // verifico che non si voglia cancellare l'elenco dei profili
       if (name_to_delete == "profiles")
       {
           Debug.Log("File not deletable");
           StartCoroutine(FileNotDeletable());
           return;
       }
       
       foreach (string name in GameManager.instance.profileNames)
       {
           if (name == name_to_delete)
           {
               found = true;
           }
       }

       if (found)
       {
           DeletePrefs(name_to_delete); //Cancella PrefsPuzzleGame
           GameManager.instance.profileNames.Remove(name_to_delete); //Rimuove dall'elenco dei giocatori
           SaveSystem.DeleteFile(name_to_delete); // Cancella il profilo
           GameManager.instance.SaveProfileList(); // Salva l'elenco nuovo dei giocatori
           SceneManager.LoadScene("Profiles");
       }
       else
       {
           Debug.Log("Name not found");
           StartCoroutine(ProfileileNotFound());
       }
   }

   IEnumerator ProfileNotAvailable()
   {
       warning.gameObject.SetActive(true);
       warning.GetComponentInChildren<TextMeshProUGUI>().text = "A profile with this name already exists";
       yield return new WaitForSeconds(3f);
       warning.gameObject.SetActive(false);
   }
   IEnumerator NameNotUsable()
   {
       warning.gameObject.SetActive(true);
       warning.GetComponentInChildren<TextMeshProUGUI>().text = "Name not usable";
       yield return new WaitForSeconds(3f);
       warning.gameObject.SetActive(false);
   }
   IEnumerator FileNotDeletable()
   {
       warning.gameObject.SetActive(true);
       warning.GetComponentInChildren<TextMeshProUGUI>().text = "File not deletable";
       yield return new WaitForSeconds(3f);
       warning.gameObject.SetActive(false);
   }
   IEnumerator ProfileileNotFound()
   {
       warning.gameObject.SetActive(true);
       warning.GetComponentInChildren<TextMeshProUGUI>().text = "Profile not found";
       yield return new WaitForSeconds(3f);
       warning.gameObject.SetActive(false);
   }
    
    
   public void SpawnProfiles()
   {
        input.gameObject.SetActive(false);
        create.gameObject.SetActive(false);
        delete.gameObject.SetActive(false);
        warning.gameObject.SetActive(false);
        string profiles = SaveSystem.Load("profiles");
        if (profiles == null)
        {
            return;
        }
        
        // else: finisce
        string[] array = JsonUtility.FromJson<Serialization<string>>(profiles).ToArray();
        if(array != null)
            GameManager.instance.profileNames = new List<string>(array);

        /* // OLD serialization
        Serialization<string> prof = JsonUtility.FromJson<Serialization<string>>(profiles);
        profileNames = prof.ToList(); */
        for (int j = 0; j < GameManager.instance.profileNames.Count; j++)
        {
            Debug.Log(GameManager.instance.profileNames[j]);
        }
        

        int i = 0;
        foreach (string name in GameManager.instance.profileNames)
        {
            Debug.Log(name);
            //todo fixare la posizione dei bottoni instantiati
            Button button = Instantiate(profile_prefab, scrollView_Content);
            button.GetComponentInChildren<TextMeshProUGUI>().text = name;
            button.transform.SetParent(scrollView_Content, false);
            SelectProfile selectProfile = button.GetComponent<SelectProfile>();
            button.onClick.AddListener(selectProfile.LoadProfile);

            /* //Mio modo di farlo prima di scoprire l'Horizontal Layout Group
             Vector2 newPosition = new Vector2((buttonWidth * i)-button_offset, 0);
            ((RectTransform)button.transform).anchoredPosition = newPosition; */
            i++;
        }
    }
   
   public void ActivateBlackScreen(string name)
   {
       blackBackground.gameObject.SetActive(true);
       blackBackground.GetComponentInChildren<TextMeshProUGUI>().text = "Loading profile of " + name + "...";
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
    public void DeletePrefs(string profile)
    {
        PlayerPrefs.DeleteKey(profile+"_"+"Level 1");
        PlayerPrefs.DeleteKey(profile+"_"+"Level 2");
        PlayerPrefs.DeleteKey(profile+"_"+"Level 3");
    }

    
}
