using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeverTutorial : MonoBehaviour
{
    // Start is called before the first frame update
    
    private bool Tutorial=false;
    [Header("Tutorial")] [SerializeField] private TextAsset inkJSON;
    
    private void Update()
    {
        if (!GameManager.instance.profile_created && !Tutorial)
        {
            Tutorial = true;
            DialogeManager.GetInstance().EnterDialogueMode(inkJSON);
        }
    }
    
}
