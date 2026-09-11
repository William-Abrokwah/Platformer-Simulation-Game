using UnityEngine;

public class PlatformPelletSpawner : MonoBehaviour
{
    public GameObject pelletPrefab;
    public float pelletHeight = 0.5f;

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

        Bounds bounds = platformCollider.bounds;

        // Generating a random position within the platform's surface bounds
        float randomX = Random.Range(bounds.min.x + xPadding, bounds.max.x - xPadding);
        float randomZ = Random.Range(bounds.min.z + zPaddingMin, bounds.max.z - zPaddingMax); 
        Vector3 randomSpawnPosition = new Vector3(randomX, bounds.max.y + pelletHeight, randomZ);

        Instantiate(pelletPrefab, randomSpawnPosition, Quaternion.identity);
    }
}
