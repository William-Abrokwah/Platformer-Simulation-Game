using UnityEngine;

public class BotSpawner : MonoBehaviour
{
    [SerializeField] private GameObject botPrefab;
    [SerializeField] private Transform botSpawnPoint;

    public void SpawnBot()
    {
        if (botPrefab != null && botSpawnPoint != null)
        {
            // Create bot
            GameObject bot = Instantiate(botPrefab, botSpawnPoint.position, botSpawnPoint.rotation);

            Bot botScript = bot.GetComponent<Bot>();

            if (botScript != null)
            {
                botScript.SetPelletSpawner(GetComponent<PelletSpawner>());
            }
        }
    }
}