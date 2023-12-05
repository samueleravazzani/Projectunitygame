using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable] // thus able to save it as a file
public class PlayerData
{
    public float anxiety, literacy_inverted, climate_change_skepticism, sum_parameters;
    public float[] position;
    public int problem_now;
    public int task_index;
    public string[] task_done;
    public int[] task_picked;

    public PlayerData(GameManager gm) // CONSTRUCTOR: used to initialize
    {
        anxiety = GameManager.instance.anxiety;
        literacy_inverted = GameManager.instance.literacy_inverted;
        climate_change_skepticism = GameManager.instance.climate_change_skept;
        sum_parameters = anxiety + literacy_inverted + climate_change_skepticism;
        // position[0] = ;
        // position[1] = ;
        // position[2] = ;
        
        // problem_now = ;
        // task_index = ;
        // task_done = ;
        // task_picked = ;
    }
}
