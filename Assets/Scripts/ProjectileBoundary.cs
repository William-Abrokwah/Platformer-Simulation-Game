using UnityEngine;

public class ProjectileBoundary : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        // Destroy projectile if it exits platform bounds
        if (other.CompareTag("Projectile"))
        {
            Projectile proj = other.GetComponent<Projectile>();

            if (proj != null)
            {
                proj.DestroyProjectile();
            }
        }
    }
}
