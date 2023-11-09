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
    public Sprite[] air; // 5
    public Sprite[] rain; // 6
    
    private float[] xlim = new float[] {-20f, 28f}, ylim = new float[] {-15f, 20.5f};

    public int[] problems = new int[] { 1, 2, 3, 4, 5, 6 };
    // 1 = fire, 2 = plastic, 3 = water, 4 = pollution, 5 = air, 6 = rain;
    private int problem_now = 1;
    private int N_tospawn = 200;
    
    // Start is called before the first frame update
    void Start()
    {
        
        switch (problem_now)
        {
            case 1:
                Spawn(fire, N_tospawn);
                postProcessingVolume.profile.GetSetting<ColorGrading>().colorFilter.value = new Color(1.0f, 0.0f, 0.0f);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
}

