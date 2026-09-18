using UnityEngine;

public class Projectile : MonoBehaviour
{
    private PlayerShooting ownerShooter;
    
    private void OnCollisionEnter(Collision collision)
    {
        // Destroy the root parent tree object
        if (collision.transform.parent != null && collision.transform.parent.CompareTag("Tree"))
        {
            Destroy(collision.transform.parent.gameObject);
            Debug.Log("Projectile hit tree!");
        }

        // Destroy projectile on hit
        Debug.Log("Projectile destroyed!");
        DestroyProjectile();
    }

    public void setOwnerShooter(PlayerShooting shooter) {
        if (shooter == null) {
            Debug.LogError("Projectile requires an owner!");
            return;
        }
        ownerShooter = shooter;
    }

    public void DestroyProjectile()
    {
        if (ownerShooter == null)
        {
            Debug.LogError("The ownerShooter of the projectile has not been set!");
            return;
        }

        ownerShooter.SetProjectileStatus(false);
        Destroy(gameObject);
        
        GameManager.Instance.CheckForGameOverDelayed();
    }
}
