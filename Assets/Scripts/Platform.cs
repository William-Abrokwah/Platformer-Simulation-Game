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
            GameManager.Instance.setPlatform(this);
        }
    }

    public PelletSpawner GetPelletSpawner() {
        return pelletSpawner;
    }

    public BotSpawner GetBotSpawner() {
        return botSpawner;
    }

    public Trees GetTreesScript() {
        Trees treesScript = trees.GetComponent<Trees>();
        return treesScript; 
    }

    private void OnTriggerEnter(Collider other)
    {
        // Activate platfrom if player enters
        if (!hasActivated && other.CompareTag("Player"))
        {
            PlayerShooting shooter = other.GetComponent<PlayerShooting>();
            if (shooter != null) shooter.ResetAmmo();

            ActivatePlatform();
            Debug.Log("Platform activated!");
            GameManager.Instance.setPlatform(this);
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