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
    public static List<string> profileNames = new List<string>();
    public static ProfileMaster instance;
    public Button profile_prefab;
    public Transform scrollView_Content;

    public Image input;
    public Button create;

    private string newname;
    // private float[] button_position = new float[]  {376.5f, -543.5f};
    private float buttonWidth = 520f;
    private float button_offset = 460.0f;
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
    }

    public void Create()
    {
        newname = input.GetComponentInChildren<TMP_InputField>().text;
        GameManager.instance.profile = newname;
        profileNames.Add(newname);
        SaveProfileList();
        SceneManager.LoadScene("ParameterSliders");
    }

    public void SaveProfileList()
    {
        string[] array = profileNames.ToArray();
        string json = JsonUtility.ToJson(new Serialization<string>(array));
        SaveSystem.Save("profiles",json);
    }
    
    public void SpawnProfiles()
    {
        input.gameObject.SetActive(false);
        create.gameObject.SetActive(false);
        string profiles = SaveSystem.Load("profiles");
        if (profiles == null)
        {
            return;
        }
        
        // else: finisce
        string[] array = JsonUtility.FromJson<Serialization<string>>(profiles).ToArray();
        profileNames = new List<string>(array);

        /* Serialization<string> prof = JsonUtility.FromJson<Serialization<string>>(profiles);
        profileNames = prof.ToList(); */
        Debug.Log(profileNames); ///////// QUI STA

        int i = 0;
        foreach (string name in profileNames)
        {
            //todo fixare la posizione dei bottoni instantiati
            Button button = Instantiate(profile_prefab, new Vector3(), Quaternion.identity);
            button.GetComponent<TextMeshProUGUI>().text = name;
            button.transform.SetParent(scrollView_Content, false);
            Vector2 newPosition = new Vector2(buttonWidth * i, 0);
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
