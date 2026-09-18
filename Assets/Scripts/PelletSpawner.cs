using UnityEngine;
using System.Collections.Generic;

public class PelletSpawner : MonoBehaviour
{
    [SerializeField] private GameObject pelletPrefab;
    [SerializeField] private int totalPellets = 8;
    [SerializeField] private float pelletHeight = 0.5f;
    [SerializeField] private float minDistanceBetweenPellets = 1f;
    [SerializeField] private float minDistanceFromPlayer = 2f;

    private List<GameObject> spawnedPellets = new List<GameObject>();

    [Header("Pellet padding from the edges of the platform")]
    [SerializeField] private float xPadding = 1f;
    [SerializeField] private float zPaddingMin = 1f;
    [SerializeField] private float zPaddingMax = 3f; // +2f to account for the size of the trees 

    public List<GameObject> GetSpawnedPellets()
    {
        if (spawnedPellets == null) {
            Debug.LogError("No spawnedPellets list has beed created!");
        }
        return spawnedPellets;
    }

    public void SpawnPellets()
    {
        Collider platformCollider = GetComponent<Collider>();
        if (platformCollider == null) {
            Debug.LogError("The platform needs a Collider!"); 
            return;
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) { 
            Debug.LogError("No GameObject with the Player tag found!"); 
            return; 
        }

        List<Vector3> spawnedPositions = new List<Vector3>();
        Bounds bounds = platformCollider.bounds;

        int attempts = 0;
        int maxAttempts = 1000; // Bound to prevent an infinite loop

        while (spawnedPellets.Count < totalPellets && attempts < maxAttempts) 
        {
            attempts++;

            // Generating a random position within the platform's surface bounds
            float randomX = Random.Range(bounds.min.x + xPadding, bounds.max.x - xPadding);
            float randomZ = Random.Range(bounds.min.z + zPaddingMin, bounds.max.z - zPaddingMax); 
            Vector3 randomSpawnPosition = new Vector3(randomX, bounds.min.y + pelletHeight, randomZ);
            
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
                GameObject pellet = Instantiate(pelletPrefab, randomSpawnPosition, Quaternion.identity);
                Pellet pelletScript = pellet.GetComponent<Pellet>();
                if (pelletScript == null) {
                    Debug.LogError("Pellet script missing from GameObject!"); 
                }
                pelletScript.SetSpawner(this);

                spawnedPellets.Add(pellet);
                spawnedPositions.Add(randomSpawnPosition);
            }
        }
    }

    public void CollectAllPellets() {
        foreach (GameObject pellet in spawnedPellets)
        {
            Destroy(pellet);
        }

        spawnedPellets.Clear();
    }

    public void PelletCollected(GameObject pellet)
    {
        if (spawnedPellets.Contains(pellet))
        {
            spawnedPellets.Remove(pellet);
        }

        // Trigger game over check immediately
        GameManager.Instance.CheckForGameOverDelayed();
    }
}