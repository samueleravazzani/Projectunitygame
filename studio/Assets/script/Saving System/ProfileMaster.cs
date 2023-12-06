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
        SpawnProfiles();
    }

    public void NewProfile()
    {
        input.gameObject.SetActive(true);
        create.gameObject.SetActive(true);
        delete.gameObject.SetActive(false);
    }

   public void Create()
   {
       newname = input.GetComponentInChildren<TMP_InputField>().text;
       foreach (string name in GameManager.instance.profileNames)
       {
           if (name == newname)
           {
               Debug.Log("Error: a profile with this name already exists. Change name");
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
       string name_to_delete = input.GetComponentInChildren<TMP_InputField>().text;
       foreach (string name in GameManager.instance.profileNames)
       {
           if (name == name_to_delete)
           {
               GameManager.instance.profileNames.Remove(name_to_delete);
           }
           else
           {
               Debug.Log("Name not found");
           }
       }
   }
   
    
    
   public void SpawnProfiles()
   {
        input.gameObject.SetActive(false);
        create.gameObject.SetActive(false);
        delete.gameObject.SetActive(false);
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
        Debug.Log(GameManager.instance.profileNames.ToString());

        int i = 0;
        foreach (string name in GameManager.instance.profileNames)
        {
            Debug.Log(name);
            //todo fixare la posizione dei bottoni instantiati
            Button button = Instantiate(profile_prefab, scrollView_Content);
            button.GetComponentInChildren<TextMeshProUGUI>().text = name;
            button.transform.SetParent(scrollView_Content, false);
            Vector2 newPosition = new Vector2((buttonWidth * i)-button_offset, 0);
            ((RectTransform)button.transform).anchoredPosition = newPosition;
            i++;
        }
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
   

    
}
