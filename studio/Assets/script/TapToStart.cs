using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class TapToStart : MonoBehaviour
{
    CanvasElementVisibility visibility;
    void Start()
    {
        visibility = GetComponent<CanvasElementVisibility>();
        GameController.Instance.GameStarted.Where((value) => value).Subscribe((value) =>
        {
            visibility.Visible = false;
        }).AddTo(this);
    }
}

//This script utilizes UniRx to subscribe to changes in the GameStarted reactive property of the GameController instance.
//When the GameStarted property changes to true, it modifies the visibility state of the CanvasElementVisibility component attached to the same GameObject,
//hiding the associated canvas element. This mechanism could be used to toggle the visibility of an element based on the game's start state,
//such as displaying a "Tap to Start" canvas that disappears once the game begins.
