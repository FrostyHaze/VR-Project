using UnityEngine;

public class LaunchProjectile : MonoBehaviour
{
    public Transform launcherTip = null; // launcher projectiles from here
    public GameObject projectilePrefab = null; // prefab used to create projectiles 
    public float launchForce = 3.0f; // projectile launch force
    public float reloadTime = 1.0f;

    private bool canShoot = true;

    // creates and launches a projectile prefab
    public void Launch()
    {
        if (!canShoot)
            return;

        // create a projectile prefab with the position and rotation of the launcher's tip
        GameObject projectile = Instantiate(projectilePrefab, launcherTip.position, launcherTip.rotation);

        // add a force to launch the projectile
        projectile.GetComponent<Rigidbody>().AddForce(-launcherTip.forward * launchForce, ForceMode.Impulse);

        // start reload cooldown
        StartCoroutine(Reload());
    }

    private System.Collections.IEnumerator Reload()
    {
        canShoot = false;
        yield return new WaitForSeconds(reloadTime);
        canShoot = true;
    }
}
