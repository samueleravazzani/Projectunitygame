using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSlave : MonoBehaviour
{
    public string scenetoload;
    public bool playeractive;
    public Vector3 playerposition;
    private bool teleport=false;

    private void Update()
    {
        if (teleport)
        {
            ActivateChangeScene();
        }
        
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.CompareTag("Player"))
        {
            teleport = true;
        }
    }

    public void ActivateChangeScene()
    {
        SceneMaster.instance.ChangeSchene(scenetoload, playeractive, playerposition);
        teleport = false;
    }
    
}