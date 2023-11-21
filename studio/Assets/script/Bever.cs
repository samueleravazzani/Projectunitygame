using UnityEngine;

public class Bever : MonoBehaviour
{
    public float triggerRadius = 5f; // Adjust the radius as needed
    public float moveSpeed = 2f; // Adjust the movement speed as needed
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > triggerRadius)
        {
            // Calculate the direction towards the player
            Vector3 directionToPlayer = player.position - transform.position;
            directionToPlayer.Normalize();

            // Move towards the player
            transform.position += directionToPlayer * moveSpeed * Time.deltaTime;
        }
    }
}
