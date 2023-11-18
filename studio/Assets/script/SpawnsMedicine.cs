using System.Collections;
using UnityEngine;

public class SpawnsMedicine : MonoBehaviour
{
    public GameObject prefab; // Medicine prefab
    public Medicine_Card[] medicines; // Assign your Medicine_card attributes in the Unity Editor
    // Positions
    public Transform[] positions;
    
    public static SpawnsMedicine instance;
    private void Awake() //creation singleton
    {
        if (instance != null)
        {
            Debug.LogWarning("found more than one dialogue Manager in the scene");
        }
        instance = this;
    }

    private void Start()
    {
        
    }

    public void SpawnMedicines()
    {
        // shuffle medicines to randomize them and to then spawn them in random position
        ShuffleMedicines(medicines);
        // Instantiate the prefab at each position with the corresponding sprite and attribute
        for (int i = 0; i < positions.Length; i++)
        {
            GameObject obj = Instantiate(prefab, positions[i].position, Quaternion.identity);
            obj.GetComponent<SpriteRenderer>().sprite = medicines[i].drug_image;
            obj.GetComponent<MedicineTrigger>().medicine = medicines[i];
        }
    }
    
    private void ShuffleMedicines(Medicine_Card[] array)
    {
        int n = array.Length;
        for (int i = n - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            Medicine_Card temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }
}