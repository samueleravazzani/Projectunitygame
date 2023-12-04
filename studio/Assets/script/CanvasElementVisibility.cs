using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
[ExecuteAlways]
public class CanvasElementVisibility : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    [SerializeField]
    private bool visible;
    public bool Visible
    {
        get => visible;
        set
        {
            visible = value;
            if (visible) ShowElement();
            else HideElement();
        }
    }

    private void OnValidate()
    {
        if (Visible) ShowElement();
        else HideElement();
    }

    private void ShowElement()
    {
        if (!canvasGroup) canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    private void HideElement()
    {
        if (!canvasGroup) canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}

// This script, intended for UI elements, controls the visibility state using a CanvasGroup.
// It allows toggling the visibility of the associated canvas element by adjusting alpha transparency, interaction, and raycast blocking.
// The property Visible controls the visibility state, and the ShowElement() and HideElement() methods modify the CanvasGroup properties to show or hide the associated UI element.
// The OnValidate() method ensures proper visibility state initialization when the script is loaded or when values are changed in the Inspector.
