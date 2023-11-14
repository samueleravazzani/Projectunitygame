using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // singleton
    public static GameManager instance;
    
    // parameters that control environment effects
    public float anxiety = 5; // settato in SliderControl
    public float literacy_inverted = 5; // settato in SliderControl
    public float climate_change_skept = 5; // settato in SliderControl
    public float sum_parameters = 15; // settato in SliderControl
    public int task_index = 0;
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
    
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        
        //
        sum_parameters = 15;
    }
}
