using UnityEngine;

public class ParticleControl : MonoBehaviour
{
    public ParticleSystem[] particleSystems; // Reference to the Particle System
    public float movementThreshold = 0.1f; // Minimum speed to consider as moving

    private Rigidbody2D rb2D;

    void Start()
    {
        // Attempt to get Rigidbody components
        rb2D = GetComponent<Rigidbody2D>();

        // Get all Particle Systems in child objects
        particleSystems = GetComponentsInChildren<ParticleSystem>();
    }

    void Update()
    {
        float speed = 0f;

        // Determine speed based on available Rigidbody
        if (rb2D != null)
        {
            speed = rb2D.velocity.magnitude;
        }
        else
        {
            Debug.LogWarning("No Rigidbody found on the GameObject.");
            return;
        }

        // Toggle Particle Systems based on movement
        foreach (var ps in particleSystems)
        {
            if (ps == null) continue;

            if (speed > movementThreshold && !ps.isPlaying)
            {
                Debug.Log("About to play");
                ps.Play();
            }
            else if (speed <= movementThreshold && ps.isPlaying)
            {
                Debug.Log("Stopped Playing");
                ps.Stop();
            }
        }
    }
}
