using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float increment =0.1f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            MoveDown();
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            MoveUp();
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            MoveRight();
        }
    }
    
    public void MoveUp()
    {
        transform.position += new Vector3(0, increment, 0); // + per andare in alto
    }

    public void MoveDown()
    {
        transform.position -= new Vector3(0, increment, 0); // - per andare in basso
    }
    
    public void MoveLeft()
    {
        transform.position -= new Vector3(increment, 0, 0); // - per andare a sinistra
    }
    
    public void MoveRight()
    {
        transform.position += new Vector3(increment, 0, 0); // + per andare a destra
    }
}
