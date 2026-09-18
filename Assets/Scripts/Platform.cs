using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private PelletSpawner pelletSpawner;
    [SerializeField] private BotSpawner botSpawner;
    [SerializeField] private GameObject trees;

    public bool isFirstPlatform = false; // Checked true only for Platform 1
    private bool hasActivated = false;

    void Start()
    {
        // If it's the starting platform, activate right away
        if (isFirstPlatform)
        {
            ActivatePlatform();
            Debug.Log("Platform activated!");
            if (GameManager.Instance == null)
            {
                Debug.LogError("GameManager is missing from the scene!");
                return;
            }

            GameManager.Instance.SetPlatform(this);
        }
    }

    public PelletSpawner GetPelletSpawner() {
        if (pelletSpawner == null)
        {
            Debug.LogError("No pelletSpawner has been set!");
        }
        return pelletSpawner;
    }

    public BotSpawner GetBotSpawner() {
        if (botSpawner == null)
        {
            Debug.LogError("No botSpawner has been set!");
        }
        return botSpawner;
    }

    public Trees GetTreesScript() {
        if (trees == null)
        {
            Debug.LogError("No trees has been set!");
            return null;
        }
        Trees treesScript = trees.GetComponent<Trees>();

        if (treesScript == null)
        {
            Debug.LogError("Trees is missing treesScript!");
        }
        return treesScript; 
    }

    private void OnTriggerEnter(Collider other)
    {
        // Activate platfrom if player enters
        if (!hasActivated && other.CompareTag("Player"))
        {
            PlayerShooting shooter = other.GetComponent<PlayerShooting>();
            if (shooter == null) {
                Debug.LogError("Player is missing the shooting script!");
                return;
            }
            
            shooter.ResetAmmo();
            
            // Clear pellets on previous platform
            GameManager.Instance.GetPlatform().GetPelletSpawner().CollectAllPellets();

            ActivatePlatform();
            Debug.Log("Platform activated!");
            GameManager.Instance.SetPlatform(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Destroy projectile if it exits platform bounds
        if (other.CompareTag("Projectile"))
        {
            Projectile proj = other.GetComponent<Projectile>();

            if (proj != null)
            {
                Debug.Log("Projectile hit boundary!");
                proj.DestroyProjectile();
            }
        }
    }

    public void ActivatePlatform()
    {
        hasActivated = true;
        if (pelletSpawner != null) pelletSpawner.SpawnPellets();
        if (botSpawner != null) botSpawner.SpawnBot();
    }
}