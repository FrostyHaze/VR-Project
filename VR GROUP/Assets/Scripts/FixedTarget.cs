using UnityEngine;

public class FixedTarget : MonoBehaviour
{
    public int numberOnTarget = 1; // Set this in Inspector (1 to 8)

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            FindObjectOfType<QuizManager>().HandleTargetShot(numberOnTarget);
            Destroy(other.gameObject); // Optional: remove bullet
        }
    }
}
