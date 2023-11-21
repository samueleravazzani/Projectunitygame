using UnityEngine;

public class Bever : MonoBehaviour
{
    public float movementSpeed = 5f;
    public Transform playerTransform;
    public Collider2D triggerCollider;
    public float startDelay = 2f; // Delay before the Bever starts moving


    private bool canMove = false;

    void Start()
    {
        Invoke("EnableMovement", startDelay);
    }

    void Update()
    {
        if (canMove)
        {
            if (!IsPlayerInsideTrigger())
            {
                MoveTowardsPlayer();
            }
        }
        
    }

    void EnableMovement()
    {
        canMove = true;
    }

    
    bool IsPlayerInsideTrigger()
    {
        return triggerCollider.bounds.Contains(playerTransform.position);
    }

    void MoveTowardsPlayer()
    {
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        transform.Translate(direction * movementSpeed * Time.deltaTime);
    }
}