using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cameranodestroy : MonoBehaviour
{
    public static Cameranodestroy instance;
    public static CinemachineConfiner2D confine;
    
    void Awake()
    {
        // assure this is the only instance that it has been created.
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad((this.gameObject)); // if I am changing this scene, do NOT destroy this object.
        // otherwise this object will be destroyed
    }

    void Update()
    {
        CinemachineConfiner2D confiner = GetComponent<CinemachineConfiner2D>();
        Collider2D boundingShape = confiner.m_BoundingShape2D;

        if (GameObject.Find("player") != null && boundingShape == null)
        {
            GameObject worldBorders = GameObject.Find("WorldBorders");
            if (worldBorders != null)
            {
                PolygonCollider2D polygonCollider = worldBorders.GetComponent<PolygonCollider2D>();
                if (polygonCollider != null)
                {
                    confiner.m_BoundingShape2D = polygonCollider;
                }
            }
        }
    
        /*
        // se il player è attivo (-> non è null, perché se è disattivato GameObject.Find ritorna null
        // e il confiner è nullo -> lo prendo
        if (GameObject.Find("player") != null && 
            GetComponent<CinemachineConfiner2D>().m_BoundingShape2D == null)
        {
            GetComponent<CinemachineConfiner2D>().m_BoundingShape2D =
                GameObject.Find("WorldBorders").GetComponent<PolygonCollider2D>();
        } */
    }

    
}
