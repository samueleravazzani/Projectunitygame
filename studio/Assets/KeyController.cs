using UnityEngine;

public class KeyController : MonoBehaviour
{
    public GameObject wall; 

    private bool isDragging = false;

    private void OnMouseDown()
    {
        isDragging = true;

        // Ottieni l'Animator o l'Animation collegata all'oggetto
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            // Disattiva l'animazione
            animator.enabled = false;
        }
        else
        {
            Animation animation = GetComponent<Animation>();
            if (animation != null)
            {
                // Disattiva l'animazione
                animation.enabled = false;
            }
            else
            {
                Debug.LogWarning("Nessun Animator o Animation trovato sull'oggetto.");
            }
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

    private void Update()
    {
        if (isDragging)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10; // Distanza dal piano rispetto alla camera
            Vector3 objectPos = Camera.main.ScreenToWorldPoint(mousePos);

            // Muovi l'oggetto corrente sotto il cursore del mouse
            transform.position = objectPos;

            // Disattiva l'oggetto "wall"
            wall.SetActive(false);
        }
    }
}