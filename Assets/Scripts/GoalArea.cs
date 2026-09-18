using UnityEngine;

public class GoalArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance != null)
            {
                // Trigger the victory screen when reached
                GameManager.Instance.TriggerWin();
            }
        }
    }
}
