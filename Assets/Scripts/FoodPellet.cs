using UnityEngine;

public class FoodPellet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the player collected the pellet
        if (other.CompareTag("Player"))
        {
            // Add ammo to player 
            // To be added

            // Destroy the pellet object so it disappears
            Destroy(gameObject);
        }

        // Check if the bot collected the pellet
        else if (other.CompareTag("Bot"))
        {
            Destroy(gameObject);
        }
    }
}
