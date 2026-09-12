using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    [HideInInspector]
    public PlayerShooting ownerShooter;
    
    private void OnCollisionEnter(Collision collision)
    {
        // Destroy the root parent tree object
        if (collision.transform.parent != null && collision.transform.parent.CompareTag("Tree"))
        {
            Destroy(collision.transform.parent.gameObject);
        }

        // Destroy projectile on hit
        DestroyProjectile();
    }

    private void OnTriggerExit(Collider other)
    {
        // Destroy projectile if it exits platform bounds
        if (other.CompareTag("ProjectileBoundary"))
        {
            DestroyProjectile();
        }
    }

    void DestroyProjectile()
    {
        if (ownerShooter != null)
        {
            ownerShooter.isProjectileInFlight = false;
        }
        Destroy(gameObject);
    }
}
