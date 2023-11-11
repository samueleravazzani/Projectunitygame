using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class playerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;
    public CinemachineVirtualCamera Camera;
    public Collider2D worldBorders;
    private Vector2 movement;
    public static playerMovement instance;

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
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        if (DialogeManager.GetInstance().dialogueIsPlaying)
        {
            return;
        }
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("To_Cave"))
        {
            transform.position = new Vector3(1.8f, -4.23f);
            GameObject sceneLoaderCanvas = GameObject.Find("SceneLoaderCanvas");
            if (sceneLoaderCanvas != null)
            {
                sceneLoaderCanvas.SetActive(true);
                SceneManager.LoadScene("Cave");
            }
            else
            {
                Debug.LogError("SceneLoaderCanvas non trovato nella scena.");
            }
        }
        
        if (coll.gameObject.CompareTag("To_MainMap"))
        {
            SceneManager.LoadScene("MainMap");
            transform.position = new Vector3(5.89f, 19.50f);

            CinemachineVirtualCamera virtualCamera = Camera.GetComponent<CinemachineVirtualCamera>();
            
            if (virtualCamera != null)
            {
                CinemachineConfiner2D confiner = virtualCamera.GetComponent<CinemachineConfiner2D>();
                
                if (confiner != null)
                {
                    confiner.m_BoundingShape2D = worldBorders;
                }
                else
                {
                    Debug.LogError("CinemachineConfiner2D non trovato nella telecamera virtuale.");
                }
            }
            else
            {
                Debug.LogError("Componente CinemachineVirtualCamera non trovato.");
            }
        }
    }
}
