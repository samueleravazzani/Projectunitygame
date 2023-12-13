using UnityEditor;
using UnityEngine;

public class ParticleCollisionDetector : MonoBehaviour
{
    private ParticleSystem particleSystem;
    private bool state = false;
    public MovesCounter movesCounter;
    

    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
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
            }
        }
        if (!other.CompareTag("Disattiva"))
        {
            Debug.Log(movesCounter.remainingMoves);
            if (movesCounter.remainingMoves == 0 && state==false)
            {
                movesCounter.Invoke("ShowPopup",1f);
            }

        }
    }

    
    
}
