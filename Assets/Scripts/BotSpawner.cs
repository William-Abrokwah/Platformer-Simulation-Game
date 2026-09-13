using UnityEngine;

public class BotSpawner : MonoBehaviour
{
    public GameObject botPrefab;
    public float spawnDelay = 2f;
    public Vector3 spawnOffset = new Vector3(8f, 1.25f, 0f);

    private bool hasSpawned = false;

    void Start()
    {
        Invoke(nameof(SpawnBot), spawnDelay);
    }

    // Public for future spawning
    public void SpawnBot() 
    {
        if (hasSpawned || botPrefab == null) return;

        Vector3 spawnPos = transform.position + spawnOffset;
        Instantiate(botPrefab, spawnPos, Quaternion.identity);
        hasSpawned = true;
    }
}