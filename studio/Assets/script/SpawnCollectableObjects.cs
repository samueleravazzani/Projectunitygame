using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnCollectableObjects : MonoBehaviour
{
    [Header("Categories of Collectable Objects")]
    public int nCategory; // n di categorie
    public Sprite[] Mushrooms;

    [Header("SpawnLocations")] 
    public PolygonCollider2D Locations;
    
    [Header("Parameters")]
    public bool collectObjects;
    public int N_spawn_object;
    public GameObject CollectableObject;
    private int category_choice;
    private Sprite[] category;
    private int obj_choice;
    private GameObject obj;
    
    // Update is called once per frame
    void Update()
    {
        if (collectObjects)
        {
            // scelta della categoria da raccogliere (e.g. mushrooms)
            category_choice = Random.Range(0, nCategory-1); // scelto e.g. 0 Mushroom
            switch (category_choice)
            {
                case 0:
                    category = Mushrooms;
                    break;
            }
            
            // scelta oggetto da raccogliere all'interno della categoria
            obj_choice = Random.Range(0, category.Length);
            
            // oggetto da raccogliere
            foreach (Sprite obj in category)
            {
                SpawnObjects(obj, N_spawn_object);
            }

            collectObjects = false;
        }
    }

    private void SpawnObjects(Sprite sprite, int n)
    {
        bool ok = false;
        Vector3 rndPoint3D;
        Vector2 rndPoint2D;
        
        for (int i = 0; i < n; i++)
        {
            do
            {
                // punto random 3D -> 2D
                rndPoint3D = RandomPointInBounds(Locations.bounds, 1f);
                rndPoint2D = new Vector2(rndPoint3D.x, rndPoint3D.y);

                // trovo il punto più vicino a quello trovato interno
                Vector2 rndPointInside = Locations.ClosestPoint(new Vector2(rndPoint2D.x, rndPoint2D.y));

                // se il punto è effettivamente dentro e non sul bordo -> creo
                if (rndPointInside.x == rndPoint2D.x && rndPointInside.y == rndPoint2D.y)
                {
                    ok = true;
                }
            } while (ok == false);

            // creo l'oggetto
            obj = Instantiate(CollectableObject, new Vector3(rndPoint2D.x, rndPoint2D.y,0), transform.rotation);
            obj.GetComponent<SpriteRenderer>().sprite = sprite;
            
        }
    }
    
    private Vector3 RandomPointInBounds(Bounds bounds, float scale) // prende un punto qualsiasi nei confini
    {
        return new Vector3(
            Random.Range(bounds.min.x * scale, bounds.max.x * scale),
            Random.Range(bounds.min.y * scale, bounds.max.y * scale),
            0
        );
    }
}
