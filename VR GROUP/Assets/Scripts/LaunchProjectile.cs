using UnityEngine;
using System.Collections;

public class LaunchProjectile : MonoBehaviour
{
    [Header("Launch Settings")]
    public Transform launcherTip;
    public GameObject projectilePrefab;
    public float launchForce = 3.0f;
    public float reloadTime = 1.0f;
    public bool autoDestroyProjectiles = true;
    public float projectileLifetime = 5f;

    [Header("Trail Settings")]
    public LineRenderer projectileTrail;
    public float trailDuration = 0.3f;
    public Color trailColor = Color.cyan;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip shootSound;
    private bool canShoot = true;

    void Start()
    {
        InitializeTrail();
    }

    void InitializeTrail()
    {
        if (projectileTrail != null)
        {
            projectileTrail.enabled = false;
            projectileTrail.startColor = trailColor;
            projectileTrail.endColor = trailColor;
            projectileTrail.positionCount = 2;
        }
    }

    public void Launch()
    {
        if (!canShoot) return;

        // Play shoot sound
        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }

        // Create projectile
        GameObject projectile = Instantiate(projectilePrefab, launcherTip.position, launcherTip.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        // Launch physics
        rb.AddForce(-launcherTip.forward * launchForce, ForceMode.Impulse);

        // Start trail effect
        StartCoroutine(ShowTrail(projectile));
        StartCoroutine(Reload());

        if (autoDestroyProjectiles)
            Destroy(projectile, projectileLifetime);
    }

    private IEnumerator ShowTrail(GameObject projectile)
    {
        if (projectileTrail == null) yield break;

        float startTime = Time.time;
        projectileTrail.enabled = true;

        while (Time.time < startTime + trailDuration && projectile != null)
        {
            projectileTrail.SetPosition(0, launcherTip.position);
            projectileTrail.SetPosition(1, projectile.transform.position);
            yield return null;
        }

        projectileTrail.enabled = false;
    }

    private IEnumerator Reload()
    {
        canShoot = false;
        yield return new WaitForSeconds(reloadTime);
        canShoot = true;
    }
}