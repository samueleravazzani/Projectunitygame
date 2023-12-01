using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfileMaster : MonoBehaviour
{
    public static List<string> profileNames = new List<string>();
    public static ProfileMaster instance;
    public Button profile_prefab;
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

    public void SaveProfile()
    {
        string json = JsonUtility.ToJson(new Serialization<string>(profileNames));
        SaveSystem.Save("profiles",json);
    }
    
    public void SpawnProfiles()
    {
        string profiles = SaveSystem.Load("profiles");
        profileNames = JsonUtility.FromJson<Serialization<string>>(profiles).ToList();

        foreach (string name in profileNames)
        {
            //todo fixare la posizione dei bottoni instantiati
            Button button = Instantiate(profile_prefab, new Vector3(), Quaternion.identity);
            button.GetComponent<TextMeshProUGUI>().text = name;
        }
    }
    
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
    }
   

    
}
