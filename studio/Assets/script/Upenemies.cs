using UnityEngine;

public class SpawnersUP : MonoBehaviour
{
    public GameObject rubbish;  
    public float minTime = 5.0f;      // Tiempo mínimo entre spawns
    public float maxTime = 10.0f;      // Tiempo máximo entre spawns

    private float nextSpawnTime;

    private void Start()
    {
        // Initialize the time for the first random spawn
        nextSpawnTime = Time.time + Random.Range(minTime, maxTime);
    }

    private void Update()
    {
        // Check if it's time to spawn the object
        if (Time.time >= nextSpawnTime)
        {
            SpawnObject();
            // Update the time for the next random spawn
            nextSpawnTime = Time.time + Random.Range(minTime, maxTime);
        }
    }

    void SpawnObject()
    {
        // Generate a random position on the screen
        Vector2 randomPosition = new Vector2(Random.Range(1f, 5f), Random.Range(2f, 3f));

        // Instantiate the object at the random position
        GameObject spawnedObject = Instantiate(rubbish, randomPosition, Quaternion.identity);

        // Set the object to be active after instantiation
        spawnedObject.SetActive(true);

        // Destroy the object after 8 seconds
        Destroy(spawnedObject, 8f);
    }
}