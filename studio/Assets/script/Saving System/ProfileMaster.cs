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
    public Transform scrollView_Content;
    // private float[] button_position = new float[]  {376.5f, -543.5f};
    private float buttonWidth = 590f;
    private float button_offset = 604.0f;
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
        List<T> target;
        public List<T> ToList() { return target; }

        public Serialization(List<T> target)
        {
            this.target = target;
        }
    }
   

    
}
