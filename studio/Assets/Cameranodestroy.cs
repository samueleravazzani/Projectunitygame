using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameranodestroy : MonoBehaviour
{
    public static Cameranodestroy instance;
    
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
