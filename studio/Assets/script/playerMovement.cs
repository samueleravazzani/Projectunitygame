using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;

    private Vector2 movement;
    private float[] xlim = new float[] {-20f, 28f}, ylim = new float[] {-15f, 20.5f};
    
    public static playerMovement instance;
    // SINGLETON TYPE
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

    
    
    // Update is called once per frame
    void Update()
    {
        if (transform.position.x>xlim[0] && transform.position.x<xlim[1] && transform.position.y > ylim[0] && transform.position.y < ylim[1])
        {
            //input
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }
    }

    private void FixedUpdate()
    { //movement
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
            SceneManager.LoadScene("Cave");
        }
    }
    
    
}
