using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float projectileSpeed = 30f;
    
    private int ammoCount = 0;
    private bool isProjectileInFlight = false;

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

    public int GetAmmoCount()
    {
        return ammoCount;
    }

    public void AddAmmo(int amount)
    {
        ammoCount += amount;

        // Send the updated total to the UI Manager
        GameManager.Instance.UpdateAmmoDisplay(ammoCount);
    }

    public void ResetAmmo()
    {
        ammoCount = 0;
        GameManager.Instance.UpdateAmmoDisplay(ammoCount);
    }

    public void SetProjectileStatus(bool status) {
        isProjectileInFlight = status;
    }

    private void FireProjectile() {
        if (ammoCount <= 0) return;

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

        ammoCount--;
        isProjectileInFlight = true;

        // Update the UI immediately after shooting
        GameManager.Instance.UpdateAmmoDisplay(ammoCount);
    }
}
