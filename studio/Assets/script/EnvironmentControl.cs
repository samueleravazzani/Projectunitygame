using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using Random = UnityEngine.Random;

public class EnvironmentControl : MonoBehaviour
{
    public PostProcessVolume postProcessingVolume;
    public GameObject effect_empty;
    public Sprite[] fire; // 1
    public Sprite[] plastic; // 2
    public ParticleSystem pollution; // 3
    public ParticleSystem air; // 4
    public ParticleSystem rain; // 4
    
    // vettore di colori
    // caso 1
    private Color[] fireColors = {new Color(0.91509f, 0.3151032f, 0.3151032f), new Color (0.9433962f, 0.3850481f,  0.3850481f), new Color (0.9433962f, 0.4850481f,  0.4850481f)};
    // caso 2
    private Color[] plasticColors = {new Color (1.0f,1.0f,1.0f), new Color (1.0f,1.0f,1.0f), new Color (1.0f,1.0f,1.0f)};
    // caso 3
    private Color[] pollutionColors = {new Color (0.3018868f,0.3018868f,0.3018868f), new Color (0.45f,0.45f,0.45f), new Color (0.5849056f,0.5849056f,0.5849056f)};
    // caso 4
    private Color[] rainColors = {new Color(0.3495016f, 0.6860042f, 0.9622642f), new Color (0.4189569f, 0.7025412f,  0.9245283f), new Color (0.5189569f, 0.7425412f,  0.9245283f)};
    
    private float[] xlim = new float[] {-20f, 28f}, ylim = new float[] {-15f, 20.5f};


    public PolygonCollider2D Locations;
    /* PROBLEMS */
    // 1 = fire, 2 = plastic, 3 = water, 4 = pollution, 5 = air, 6 = rain;
     // /!\ ATTUALE PROBLEMA
    /* private int[] N_tospawn; // questo cambia durante il gioco /!\
    private float[] smokeRot;
    private float[] windRot;
    private float[] rainRot;
    private float[] level_anxiety = new float[] {0,0,0,0}; */
    private  float calibration_anxiety = -70/9f;
    public bool update_camera_bool = true; // /!\ IMPORTANTE, lo devo aggiornare anche dove faccio GameManager.instance.task_index++
    public static bool destroy_obj;

    public static EnvironmentControl instance;
    void Awake()
    {
        // singleton
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
    }
    

    // ora non serve la Coroutine etc. perché aggiorna appena la MainMap è caricata
    void Start()
    {
        StopEnvironment();
        if (GameManager.instance.task_index == 3)
        {
            GameManager.instance.problem_now = 0; // tutto a posto
        }
        
        UpdateEnvironment();
    }

    public void UpdateEnvironment()
    {
        // Process another case (1, 2, or 3 or 4)
        switch (GameManager.instance.problem_now)
        {
            case 0: // tutto a posto
                destroy_obj = true;
                CameraEnvironment(new Color(1.0f, 1.0f, 1.0f));
                break;
            case 1: // FIRE
                SpawnWithinCollider(fire, GameManager.instance.N_tospawn[GameManager.instance.task_index]);
                CameraEnvironment(fireColors[GameManager.instance.task_index]);
                break;
            case 2: // PLASTIC
                SpawnWithinCollider(plastic, GameManager.instance.N_tospawn[GameManager.instance.task_index]);
                CameraEnvironment(plasticColors[GameManager.instance.task_index]);
                break;
            case 3: // POLLUTION
                ParticleSystem ptspoll = Instantiate(pollution, new Vector3(-31.5f, 4.5f, -1f),
                    Quaternion.Euler(0, 90, 90));
                var emissionpoll = ptspoll.emission; // /!\ PURTROPPO non si può modificare direttamente ma va estratto così
                emissionpoll.rateOverTime = GameManager.instance.smokeRot[GameManager.instance.task_index];
                CameraEnvironment(pollutionColors[GameManager.instance.task_index]);
                break;
            case 4: // AIR + RAIN
                ParticleSystem ptsair = Instantiate(air, new Vector3(-30, -2, -1), Quaternion.Euler(0, 90, 90));
                var emissionair = ptsair.emission;
                emissionair.rateOverTime = GameManager.instance.windRot[GameManager.instance.task_index];
                ParticleSystem ptsrain = Instantiate(rain, new Vector3(0, 30, -1), transform.rotation);
                var emissionrain = ptsrain.emission;
                emissionrain.rateOverTime = GameManager.instance.rainRot[GameManager.instance.task_index];
                CameraEnvironment(rainColors[GameManager.instance.task_index]);
                break;
        }

        // regulate saturation according to anxiety level
        CameraAnxiety(GameManager.instance.level_anxiety[GameManager.instance.task_index] * calibration_anxiety);
    }

    private void StopEnvironment()
    {
        rain.gameObject.SetActive(false);
        air.gameObject.SetActive(false);
        pollution.gameObject.SetActive(false);
        
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
            Vector3 rnd = new Vector3(Random.Range(xlim[0], xlim[1]+1), Random.Range(ylim[0], ylim[1]+1), 0);
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

            float randomX = Random.Range(minX, maxX+1);
            float randomY = Random.Range(minY, maxY+1);

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

