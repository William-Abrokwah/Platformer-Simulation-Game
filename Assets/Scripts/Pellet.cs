using UnityEngine;

public class Pellet : MonoBehaviour
{
    private PelletSpawner currentSpawner;

    public void SetSpawner(PelletSpawner spawner)
    {
        if (spawner == null) {
            Debug.LogError("Pellet requires a Pellet spawner to be set!"); 
            return;
        }
        currentSpawner = spawner;
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
        if (currentSpawner != null)
        {
            // Notify spawner that pellet has been collected
            currentSpawner.PelletCollected(gameObject);
        }

        // Destroy the pellet object so it disappears
        Destroy(gameObject);
    }
}
