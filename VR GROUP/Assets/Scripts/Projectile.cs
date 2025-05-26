using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [Header("Settings")]
    public float speed = 60f;
    public float lifetime = 3f;
    public string targetTag = "Target";
    public LayerMask collisionLayers = ~0; // Detect all layers by default

    [Header("Collision Detection")]
    public float sphereCastRadius = 0.2f;
    public bool showDebugTrail = true;
    public Color debugColor = Color.red;

    [Header("Effects")]
    public GameObject hitEffectPrefab;

    private Rigidbody rb;
    private Vector3 previousPosition;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }

    void Start()
    {
        previousPosition = transform.position;
        rb.linearVelocity = transform.forward * speed;
        Destroy(gameObject, lifetime);
    }

    void FixedUpdate()
    {
        if (showDebugTrail)
        {
            Debug.DrawLine(previousPosition, transform.position, debugColor, 1f);
        }

        CheckContinuousCollision();
        previousPosition = transform.position;
    }

    void CheckContinuousCollision()
    {
        Vector3 direction = (transform.position - previousPosition).normalized;
        float distance = Vector3.Distance(previousPosition, transform.position);

        RaycastHit[] hits = Physics.SphereCastAll(
            previousPosition,
            sphereCastRadius,
            direction,
            distance,
            collisionLayers
        );

        foreach (RaycastHit hit in hits)
        {
            Debug.Log("Hit: " + hit.collider.name);

            if (hit.collider.CompareTag(targetTag))
            {
                FixedTarget target = hit.collider.GetComponent<FixedTarget>();
                if (target != null)
                {
                    target.HandleHit(); // Adjust if it requires parameters
                }

                if (hitEffectPrefab != null)
                {
                    Instantiate(hitEffectPrefab, hit.point, Quaternion.identity);
                }

                Destroy(gameObject);
                return;
            }
        }
    }
}
