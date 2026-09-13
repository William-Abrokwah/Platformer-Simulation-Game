using UnityEngine;

public class Projectile : MonoBehaviour
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

    public void DestroyProjectile()
    {
        if (ownerShooter != null)
        {
            ownerShooter.SetProjectileStatus(false);
        }
        Destroy(gameObject);
    }
}
