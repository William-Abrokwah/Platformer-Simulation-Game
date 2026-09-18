using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance != null)
            {
                // Trigger the game over screen on fall
                GameManager.Instance.TriggerLoss("You fell off the platform!");
            }
        }
    }
}
