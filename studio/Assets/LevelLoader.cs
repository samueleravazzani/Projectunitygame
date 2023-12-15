using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public GameObject FishPrefabToDestroy;
    private GameObject[] FishprefabInstances;
    
    public GameObject TrashPrefabToDestroy;
    private GameObject[] TrashprefabInstances;
    
    [SerializeField] private TextMeshProUGUI Levelupdate;
    private static LevelLoader instance;
    
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("find more than one dialogue Manager in the scene");
        }
        instance = this;
        //DontDestroyOnLoad(this.gameObject);
    }
     
    public static LevelLoader GetInstance()
    {
        return instance;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Levelupdate.text = "";
    }
    
    
    
    
    public void levelLoader(int level)
    {
        StartCoroutine(loadlevel(level));
    }

    IEnumerator loadlevel(int level)
    {
        distruzione();
        
        //carico il nuovo livello ,l'animazione viene tolta per il primo
        if (level != 1)
        {
            transition.SetTrigger("Start");
            yield return new WaitForSeconds(1);
            transition.ResetTrigger("Start");
            transition.SetTrigger("End");
            yield return new WaitForSeconds(1);
            transition.ResetTrigger("End");
        }

        Levelupdate.text=string.Format($"Level:{level}/3");
        yield return new WaitForSeconds(2);
        Levelupdate.text = "";
        
    }

    public void distruzione()
    {
        //identifico gli oggetti in scena
        
        FishprefabInstances = GameObject.FindGameObjectsWithTag(FishPrefabToDestroy.tag);
        TrashprefabInstances = GameObject.FindGameObjectsWithTag(TrashPrefabToDestroy.tag);
        
        //li distruggo tutti
        if (FishprefabInstances != null)
        {
            foreach (GameObject instance in FishprefabInstances)
            {
                Destroy(instance);
            }
            if (TrashprefabInstances != null)
            {
                foreach (GameObject instance in TrashprefabInstances)
                { 
                    instance.GetComponent<Collider2D>().isTrigger=false;
                }
            }
            
        }

        if (TrashprefabInstances != null)
        {
            foreach (GameObject instance in TrashprefabInstances)
            {
                Destroy(instance);
            }
        }
        
    }
    
    
}
