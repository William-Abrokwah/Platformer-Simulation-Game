using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI gameOverText;

    private Platform currentPlatform;

    private void Awake()
    {
        // Ensure only one GameManager exists
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Lock and hide cursor for first-person gameplay
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Initialize the ammo to 0 when the game starts
        UpdateAmmoDisplay(0);

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    // Sets current Platform the player is on
    public void SetPlatform(Platform platform) {
        if (platform == null) {
            Debug.LogError("A platform is required!");
            return;
        }
        currentPlatform = platform;
    }

    public Platform GetPlatform() {
        if (currentPlatform == null)
        {
            Debug.LogError("No currentPlatform has been set!");
        }
        return currentPlatform;
    }

    public void UpdateAmmoDisplay(int currentAmmo)
    {
        if (ammoText != null)
        {
            ammoText.text = "Ammo: " + currentAmmo;
        }
    }

    public void TriggerLoss(string message) {
        EndGame(message);
    }

    public void TriggerWin() {
        EndGame("You Win! Goal Reached!");
    }

    public void EndGame(string message)
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        if (gameOverText != null)
        {
            gameOverText.text = message;
        }

        // Unlock mouse cursor so the player can interact with UI buttons
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Pause physics and movement
        Time.timeScale = 0f;
    }

    public void RestartGame() {
        Time.timeScale = 1f; // Resetting time scale
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void CheckForGameOverDelayed()
    {
        StartCoroutine(CheckForGameOverAfterDelay());
    }

    private IEnumerator CheckForGameOverAfterDelay()
    {
        yield return new WaitForSeconds(0.2f);

        CheckForGameOver();
    }

    public void CheckForGameOver()
    {
        if (currentPlatform == null) { 
            Debug.LogError("No current platform has been set!"); 
            return; 
        }

        Trees treesScript = currentPlatform.GetTreesScript();
        PelletSpawner pelletSpawner = currentPlatform.GetPelletSpawner();
        PlayerShooting playerShooter = FindFirstObjectByType<PlayerShooting>();

        if (playerShooter == null) {
            Debug.LogError("Can't find player shooter!"); 
            return;
        }

        // Check if a 2 tree gap is open
        if (treesScript.IsGapOpen()) return;

        // Get player ammo
        int currentAmmo = playerShooter.GetAmmoCount();

        // Get remaining uncollected pellets on platform
        List<GameObject> pellets = pelletSpawner.GetSpawnedPellets();
        
        // Check that all destroyed pellets have been removed from the list
        if (pellets.Exists(pellet => pellet == null))
        {
            Debug.LogError("A destroyed pellet is still in the pellet list!");
        }

        int remainingPellets = pellets.Count;

        // Trigger game over if progress is impossible
        int totalAvailableShots = currentAmmo + remainingPellets;
        int minShotsNeeded = treesScript.GetMinShotsToOpenGap();
        
        if (totalAvailableShots < minShotsNeeded)
        {
            TriggerLoss("Out of ammo and pellets! Cannot make progress.");
        }
    }
}
