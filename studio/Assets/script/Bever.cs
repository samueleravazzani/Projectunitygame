using UnityEngine;

public class Bever : MonoBehaviour
{
    public float movementSpeed = 5f;
    public Transform playerTransform;
    public Collider2D triggerCollider;
    
    public static Bever instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
    
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    
    void Update()
    {
        if (!IsPlayerInsideTrigger())
        {
            MoveTowardsPlayer();
        }
    }
    
    bool IsPlayerInsideTrigger()
    {
        return triggerCollider.OverlapPoint(playerTransform.position);
    }

    void MoveTowardsPlayer()
    {
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        transform.Translate(direction * movementSpeed * Time.deltaTime);
    }
}