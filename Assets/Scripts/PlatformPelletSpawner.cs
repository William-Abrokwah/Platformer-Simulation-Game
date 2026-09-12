using UnityEngine;
using System.Collections.Generic;

public class PlatformPelletSpawner : MonoBehaviour
{
    public GameObject pelletPrefab;
    public int totalPellets = 8;
    public float pelletHeight = 0.5f; // So pellet sits slightly above the platform
    public float minDistanceBetweenPellets = 1f; // To prevent overlap
    public float minDistanceFromPlayer = 2f;

    private List<Vector3> spawnedPositions = new List<Vector3>();

    // Spawn padding from the edges of the platform
    public float xPadding = 1f;
    public float zPaddingMin = 1f;
    public float zPaddingMax = 3f; // The 3f is to account for the size of the trees (2f)

    void Start()
    {
       SpawnPellets(); 
    }

    void SpawnPellets()
    {
        Collider platformCollider = GetComponent<Collider>();
        if (platformCollider == null)
        {
            Debug.LogError("The platform needs a Collider!");
            return;
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("No GameObject with the Player tag found!");
            return;
        }

        Bounds bounds = platformCollider.bounds;

        int attempts = 0;
        int maxAttempts = 1000; // Bound to prevent an infinite loop
        while (spawnedPositions.Count < totalPellets && attempts < maxAttempts) 
        {
            attempts++;

            // Generating a random position within the platform's surface bounds
            float randomX = Random.Range(bounds.min.x + xPadding, bounds.max.x - xPadding);
            float randomZ = Random.Range(bounds.min.z + zPaddingMin, bounds.max.z - zPaddingMax); 
            Vector3 randomSpawnPosition = new Vector3(randomX, bounds.max.y + pelletHeight, randomZ);
            
            // Check distance from player
            if (Vector3.Distance(player.transform.position, randomSpawnPosition) < minDistanceFromPlayer)
            {
                continue;
            }

            // Checking distance from other pellets
            bool isValidPos = true;
            foreach (Vector3 pos in spawnedPositions) 
            {
                if (Vector3.Distance(pos, randomSpawnPosition) < minDistanceBetweenPellets) 
                {
                    isValidPos = false;
                    break;
                }
            }

            // Spawn pellet only if position is valid
            if (isValidPos) 
            {
                Instantiate(pelletPrefab, randomSpawnPosition, Quaternion.identity);
                spawnedPositions.Add(randomSpawnPosition);
            }
        }
    }
}
