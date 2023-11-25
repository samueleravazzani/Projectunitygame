using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    public Vector3 rotationPoint;
    private float previousTime;
    public float fallTime = 0.8f;
    public static int height = 18;
    public static int width = 35;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow)) // move down
        {
            transform.position += new Vector3(0, -1, 0);
            if (!ValidMove()) // if it is not valid: revert the movement
            {
                transform.position -= new Vector3(0, -1, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow)) // move up
        {
            transform.position += new Vector3(0, 1, 0);
            if (!ValidMove()) // if it is not valid: revert the movement
            {
                transform.position -= new Vector3(0, 1, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            // rotate
            // /!\ transform.TransformPoint(point) changes local position to global position
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0,0,1), 90);
            if (!ValidMove()) // if it is not valid: revert the movement
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0,0,1), -90);
            }
        }

        // each time diff. current time and previous time > falltime -> make it fall
        // /!\ VERY INTERESTING SHORTCUT 
        // if we press left key -> return fallTime/10 otherwise we return fallTime
        if (Time.time - previousTime > (Input.GetKeyDown(KeyCode.LeftArrow) ? fallTime/10:fallTime))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!ValidMove()) // if it is not valid: revert the movement
            {
                transform.position -= new Vector3(-1, 0, 0);
            }
            previousTime = Time.time;
        }
    }

    bool ValidMove() // check if the position of every square of the form is inside of the grid -> return true or false
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.x);

            if (roundedX < 0 || roundedX >= width || roundedY<0 || roundedY>height)
            {
                return false;
            }
        }

        return true;
    }
}
