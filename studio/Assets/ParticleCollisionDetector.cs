using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ParticleCollisionDetector : MonoBehaviour
{
    private ParticleSystem particleSystem;
    private bool state;
    public MovesCounter movesCounter;
    

    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        state = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Wall_30"))
        {
            
            ParticleSystem.CollisionModule collisionModule = particleSystem.collision;
            collisionModule.enabled = true;
            collisionModule.bounce = 0.8f;

            ParticleCollisionEvent[] collisionEvents =
                new ParticleCollisionEvent[particleSystem.GetSafeCollisionEventSize()];

            int numCollisionEvents = particleSystem.GetCollisionEvents(other, collisionEvents);
            
        }
        if (other.CompareTag("Disattiva"))
        {
            state = true;
            GameObject fireSystem = GameObject.Find("FireSystems");
            GameObject wall31 = GameObject.Find("Muro (31)");
            if (fireSystem != null)
            {
                fireSystem.SetActive(false);
                wall31.SetActive(false);
                particleSystem.gameObject.SetActive(false);
                SceneManager.LoadScene("Climate_Change_Fuoco"); 

            }
        }
        if (!other.CompareTag("Disattiva") && state==false)
        {
            if (movesCounter.remainingMoves == 0)
            {
                Debug.Log(state);
                movesCounter.Invoke("ShowPopup",1f);
            }

        }
    }

    
    
}
