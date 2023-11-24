using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSlave : MonoBehaviour
{
    public string scenetoload;
    public bool playeractive;
    public Vector3 playerposition;
 
    
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.CompareTag("Player"))
        {
            ActivateChangeScene();
        }
    }

    public void ActivateChangeScene()
    {
        SceneMaster.instance.ChangeSchene(scenetoload, playeractive, playerposition);
    }
    
    
}