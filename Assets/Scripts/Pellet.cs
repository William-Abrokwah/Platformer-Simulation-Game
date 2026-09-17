using UnityEngine;

public class Pellet : MonoBehaviour
{
    private PelletSpawner spawner;

    public void SetSpawner(PelletSpawner spawnerRef)
    {
        spawner = spawnerRef;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player collected the pellet
        if (other.CompareTag("Player"))
        {
            // Add ammo to player
            PlayerShooting shooter = other.GetComponent<PlayerShooting>();
            if (shooter != null)
            {
                shooter.AddAmmo(1);
            }
            CollectPellet();
        }

        // Check if the bot collected the pellet
        else if (other.CompareTag("Bot"))
        {
            CollectPellet();
        }
    }

    private void CollectPellet()
    {
        if (spawner != null)
        {
            // Notify spawner that pellet has been collected
            spawner.PelletCollected(gameObject);
        }

        // Destroy the pellet object so it disappears
        Destroy(gameObject);
    }
}
