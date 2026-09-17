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

    public void setPlatform(Platform platform) {
        currentPlatform = platform;
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
        // Reset time scale before reloading, otherwise the game remains paused
        Time.timeScale = 1f;
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
        Trees treesScript = currentPlatform.GetTreesScript();
        PelletSpawner pelletSpawner = currentPlatform.GetPelletSpawner();
        PlayerShooting playerShooter = FindFirstObjectByType<PlayerShooting>();

        if (treesScript == null || pelletSpawner == null || playerShooter == null) {Debug.Log("Error!!!!"); return;}

        // Check if a 2 tree gap is open
        if (treesScript.IsGapOpen()) return;

        // Get player ammo
        int currentAmmo = playerShooter.GetAmmoCount();

        // Get remaining uncollected pellets on platform
        List<GameObject> pellets = pelletSpawner.GetSpawnedPellets();
        pellets.RemoveAll(pellet => pellet == null);
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
