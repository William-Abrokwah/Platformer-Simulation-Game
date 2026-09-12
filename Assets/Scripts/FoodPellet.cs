using UnityEngine;

public class FoodPellet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the player collected the pellet
        if (other.CompareTag("Player"))
        {
            // Add ammo to player
            PlayerShooting shooter = other.GetComponent<PlayerShooting>();
            if (shooter != null)
            {
                shooter.ammoCount++;
                // Destroy the pellet object so it disappears
                Destroy(gameObject);
            }
        }

        // Check if the bot collected the pellet
        else if (other.CompareTag("Bot"))
        {
            Destroy(gameObject);
        }
    }
}
