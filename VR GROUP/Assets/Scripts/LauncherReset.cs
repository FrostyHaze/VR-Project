using UnityEngine;

public class LauncherReset : MonoBehaviour
{
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    public string floorTag = "Floor"; // tag your floor with "Floor"

    private Rigidbody rb;

    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(floorTag))
        {
            ResetLauncher();
        }
    }

    void ResetLauncher()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Optionally make kinematic before resetting
        rb.isKinematic = true;

        transform.position = originalPosition;
        transform.rotation = originalRotation;

        // Re-enable physics
        rb.isKinematic = false;
    }
}
