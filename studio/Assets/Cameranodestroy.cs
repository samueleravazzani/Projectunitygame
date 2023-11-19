using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cameranodestroy : MonoBehaviour
{
    public static Cameranodestroy instance;
    public static CinemachineConfiner2D confine;
    
    void Awake()
    {
        // assure this is the only instance that it has been created.
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad((this.gameObject)); // if I am changing this scene, do NOT destroy this object.
        // otherwise this object will be destroyed
    }

    
}
