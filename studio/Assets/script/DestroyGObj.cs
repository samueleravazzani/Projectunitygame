using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGObj : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        if (EnvironmentControl.destroy_obj)
        {
            Destroy(gameObject);
        }
    }
}
