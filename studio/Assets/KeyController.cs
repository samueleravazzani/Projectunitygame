using UnityEngine;

public class KeyController : MonoBehaviour
{
    public GameObject wall;
    public MovesCounter movesCounter; 
    
    private bool isDragging = false;
    private bool hasCollided = false;

    private void Start()
    {
        movesCounter.hasMoved = false;
    }
    
    
    private void OnMouseDown()
    {
        isDragging = true;
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.enabled = false;
        }
        else
        {
            Animation animation = GetComponent<Animation>();
            if (animation != null)
            {
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
            Debug.Log(movesCounter.remainingMoves);
            if(movesCounter.remainingMoves==0)
                movesCounter.Invoke("ShowPopup",1f);
        }
        
        movesCounter.hasMoved = false;
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
        }
    }
    
}
