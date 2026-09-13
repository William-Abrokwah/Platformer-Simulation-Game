using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float projectileSpeed = 30f;
    
    [HideInInspector]
    public int ammoCount = 0;
    [HideInInspector]
    public bool isProjectileInFlight = false;

    private void Update()
    {
        // Check for left click
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (ammoCount > 0 && !isProjectileInFlight)
            {
                FireProjectile();
            }
        }
    }

    public void AddAmmo(int amount)
    {
        ammoCount += amount;
    }

    private void FireProjectile() {
        ammoCount--;
        isProjectileInFlight = true;

        // Instantiate the projectile in front of the player
        Vector3 spawnPosition = cameraTransform.position + cameraTransform.forward * 1f;
        GameObject proj = Instantiate(projectilePrefab, spawnPosition, cameraTransform.rotation);

        // Pass player reference to projectile script
        Projectile projScript = proj.GetComponent<Projectile>();
        if (projScript != null)
        {
            projScript.ownerShooter = this;
        }

        // Set projectile velocity
        Rigidbody rb = proj.GetComponent<Rigidbody>();
        if (rb != null) {
            rb.linearVelocity = cameraTransform.forward * projectileSpeed;
        }
    }
}
