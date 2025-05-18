using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;        // Speed of the projectile
    public float lifetime = 5f;      // Destroy after 5 seconds

    private void Start()
    {
        Destroy(gameObject, lifetime); // Auto-destroy to avoid clutter
    }

    private void Update()
    {
        // Move the projectile forward constantly
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Ignore collisions with the Player and other Projectiles
        if (!other.CompareTag("Player") && !other.CompareTag("Projectile"))
        {
            // Destroy the projectile on any valid hit
            Destroy(gameObject);
        }
    }
}
