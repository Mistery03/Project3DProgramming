using UnityEngine;

public class WaterCollisionHandler : MonoBehaviour
{
    public GameObject explosionPrefab;

    void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object is one of the specific items
        if (collision.gameObject.CompareTag("explosion"))
        {
            Vector3 offset = new Vector3(0, 20, 0);

            GameObject explosion = Instantiate(explosionPrefab, collision.contacts[0].point, Quaternion.identity);
            ParticleSystem explosionParticleSystem = explosion.GetComponent<ParticleSystem>();

            if (explosionParticleSystem != null)
            {
                // Check if the particle system is not already playing
                if (!explosionParticleSystem.isPlaying)
                {
                    // Start playing the particle system
                    explosionParticleSystem.Play();
                }
            }

            Destroy(collision.gameObject);

            Destroy(explosion, explosion.GetComponent<ParticleSystem>().main.duration);
        }
    }

}
