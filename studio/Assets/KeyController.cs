using UnityEngine;

public class KeyController : MonoBehaviour
{
    public GameObject wall;
    public ParticleSystem particles;
    public MovesCounter movesCounter; // Riferimento al componente MovesCounter

    private bool isDragging = false;
    
    void Start()
    {
        particles = particles.GetComponent<ParticleSystem>();
    }

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
        if (movesCounter != null && !movesCounter.hasMoved)
        {
            movesCounter.remainingMoves--;
            movesCounter.UpdateMovesText();
            movesCounter.hasMoved = true;
            movesCounter.Invoke("ShowPopup", 1f);
        }
    }

    private void Update()
    {
        if (isDragging)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10; 
            Vector3 objectPos = Camera.main.ScreenToWorldPoint(mousePos);
            
            transform.position = objectPos;

            wall.SetActive(false);
            particles.Stop();
        }
    }
}