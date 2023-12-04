using UnityEngine;

public class ParticleCollisionHandler : MonoBehaviour
{
    private ParticleSystem particleSystem;

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
            collisionModule.dampen = 0.0f;

            ParticleCollisionEvent[] collisionEvents =
                new ParticleCollisionEvent[particleSystem.GetSafeCollisionEventSize()];

            int numCollisionEvents = particleSystem.GetCollisionEvents(other, collisionEvents);

            for (int i = 0; i < numCollisionEvents; i++)
            {
                Vector3 collisionHit = collisionEvents[i].intersection;
                Debug.Log("Particle collided with wall at: " + collisionHit);
            }

            // Trova l'oggetto "Fire system" nella scena e lo disattiva
            GameObject fireSystem = GameObject.Find("FireSystems");
            GameObject wall31 = GameObject.Find("Muro (31)");
            if (fireSystem != null)
            {
                fireSystem.SetActive(false);
                wall31.SetActive(false);
                particleSystem.gameObject.SetActive(false);
            }
        }
    } 
}
