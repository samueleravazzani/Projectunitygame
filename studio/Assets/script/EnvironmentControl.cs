using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class EnvironmentControl : MonoBehaviour
{
    public PostProcessVolume postProcessingVolume;
    public GameObject effect_empty;
    public Sprite[] fire; // 1
    public Sprite[] plastic; // 2
    public Sprite[] water; // 3
    public Sprite[] pollution; // 4
    public ParticleSystem air; // 5
    public ParticleSystem rain; // 6
    
    // vettore di colori
    private Color[] fireColors = {new Color(0.91509f, 0.3151032f, 0.3151032f), new Color (0.9433962f, 0.4850481f,  0.4850481f), new Color (1.0f,1.0f,1.0f)};
    private Color[] plasticColors = {new Color (1.0f,1.0f,1.0f), new Color (1.0f,1.0f,1.0f), new Color (1.0f,1.0f,1.0f)};
    private Color[] waterColors = {new Color(0.3495016f, 0.6860042f, 0.9622642f), new Color (0.5189569f, 0.7425412f,  0.9245283f), new Color (1.0f,1.0f,1.0f)};
    private Color[] pollutionColors = {new Color (0.3018868f,0.3018868f,0.3018868f), new Color (0.5849056f,0.5849056f,0.5849056f), new Color (1.0f,1.0f,1.0f)};
    private Color[] airColors = {new Color(0.7075472f, 0.7075472f, 0.7075472f), new Color (0.8679245f, 0.8679245f,  0.8679245f), new Color (1.0f,1.0f,1.0f)};
    private Color[] rainColors = {new Color(0.3495016f, 0.6860042f, 0.9622642f), new Color (0.5189569f, 0.7425412f,  0.9245283f), new Color (1.0f,1.0f,1.0f)};
    
    private float[] xlim = new float[] {-20f, 28f}, ylim = new float[] {-15f, 20.5f};


    public PolygonCollider2D Locations;
    private int[] problems = new int[] {0, 1, 2, 3, 4, 5, 6};
    // 1 = fire, 2 = plastic, 3 = water, 4 = pollution, 5 = air, 6 = rain;
    public int problem_now = 6; // /!\ ATTUALE PROBLEMA
    public int N_tospawn = 200; // questo cambia durante il gioco /!\
    public static float level_anxiety = 0f, calibration_anxiety = -7f;
    public bool update_camera_bool = true;
    public static int color_index=0;
    public static bool destroy_obj;


    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (update_camera_bool)
        {
            destroy_obj = false; // lo risetto false nella scena dopo
            UpdateEnvironment();
            
        }
    }

    private void UpdateEnvironment()
    {
        switch (problem_now)
        {
            case 0:
                destroy_obj = true;
                CameraEnvironment(new Color (1.0f,1.0f,1.0f));
                break;
            case 1: // FIRE
                SpawnWithinCollider(fire, N_tospawn);
                CameraEnvironment(fireColors[color_index]);
                break;
            case 2: // PLASTIC
                Spawn(plastic, N_tospawn);
                CameraEnvironment(plasticColors[color_index]);
                break;
            case 3: // WATER
                Spawn(water, N_tospawn);
                CameraEnvironment(waterColors[color_index]);
                break;
            case 4: // POLLUTION
                // Spawn(pollution, N_tospawn);
                CameraEnvironment(pollutionColors[color_index]);
                break;
            case 5: // AIR
                Instantiate(air, new Vector3(-30, -2, -1), Quaternion.Euler(0,90,90));
                CameraEnvironment(airColors[color_index]);
                break;
            case 6: // RAIN
                Instantiate(rain, new Vector3(0, 30, -1), transform.rotation);
                CameraEnvironment(rainColors[color_index]);
                break;
        }
        // regulate saturation according to anxiety level
        CameraAnxiety(level_anxiety * calibration_anxiety);
        update_camera_bool = false;
    }

    private void CameraAnxiety(float level) // changes saturation
    {
        postProcessingVolume.profile.GetSetting<ColorGrading>().saturation.value = level;
    }
    
    
    private void CameraEnvironment(Color color) // changes filter colour
    {
        postProcessingVolume.profile.GetSetting<ColorGrading>().colorFilter.value = color;
    }
    
    
    private void Spawn(Sprite[] obj_s, int N)
    {
        for (int i = 0; i < N; i++)
        {
            // random position and sprite
            Vector3 rnd = new Vector3(Random.Range(xlim[0], xlim[1]), Random.Range(ylim[0], ylim[1]), 0);
            int sprite_rnd = Random.Range(0, obj_s.Length);
            

            // creo l'oggetto in random position -> assegno random sprite tra quelli del vettori
            GameObject obj = Instantiate(effect_empty, rnd, transform.rotation);
            obj.GetComponent<SpriteRenderer>().sprite = obj_s[sprite_rnd];
            
        }
    }
    
    private void SpawnWithinCollider(Sprite[] sprite, int n)
    {
        bool ok = false;
        Vector3 rndPoint3D;
        Vector2 rndPoint2D;
        
        for (int i = 0; i < n; i++)
        {
            int sprite_rnd = Random.Range(0, sprite.Length);
            do
            {
                // punto random 3D -> 2D
                rndPoint3D = RandomPointInBounds(Locations);
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
            GameObject obj = Instantiate(effect_empty, new Vector3(rndPoint2D.x, rndPoint2D.y,0), transform.rotation);
            obj.GetComponent<SpriteRenderer>().sprite = sprite[sprite_rnd];
            
        }
    }
    
    private Vector3 RandomPointInBounds(PolygonCollider2D collider) // prende un punto qualsiasi nei confini
    {
        if (collider != null && collider.pathCount > 0)
        {
            Bounds bounds = collider.bounds;
            float minX = bounds.min.x;
            float maxX = bounds.max.x;
            float minY = bounds.min.y;
            float maxY = bounds.max.y;

            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);

            Vector3 randomPoint = new Vector3(randomX, randomY, 0);

            if (IsPointInPolygon(collider, randomPoint))
            {
                return randomPoint;
            }
        }

        // If no valid point is found, return Vector2.zero
        return Vector3.zero;
    }

    bool IsPointInPolygon(PolygonCollider2D collider, Vector3 point)
    {
        // Check if a point is inside the PolygonCollider2D
        return collider.OverlapPoint(point);
    }
}

