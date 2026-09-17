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
            Debug.Log("Projectile hit tree!");
        }

        // Destroy projectile on hit
        Debug.Log("Projectile destroyed!");
        DestroyProjectile();
    }

    public void DestroyProjectile()
    {
        if (ownerShooter != null)
        {
            ownerShooter.SetProjectileStatus(false);
        }
        Destroy(gameObject);

        if (GameManager.Instance != null)
        {
            GameManager.Instance.CheckForGameOverDelayed();
        }
    }
}
