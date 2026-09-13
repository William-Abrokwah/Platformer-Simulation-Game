using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bot : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    
    private PelletSpawner currentSpawner;
    private GameObject currentTargetPellet;
    private bool isSearching = true;
    
    // Starts searching for pellets the moment spawner is set
    public void SetPelletSpawner(PelletSpawner spawner)
    {
        if (spawner == null) {Debug.LogError("Bot requires a Pellet spawner to be set!"); return;}

        currentSpawner = spawner;
        StartCoroutine(FindNextPellet()); 
    }

    private void Update()
    {
        if (currentSpawner == null) return;

        // Look for target if we don't have target and aren't alreadly looking
        if (currentTargetPellet == null)
        {
            if (!isSearching) StartCoroutine(FindNextPellet());
            return;
        }

        MoveAndLookAtTarget();
    }

    private void MoveAndLookAtTarget() {
        // Get target position (maintaining the the bot's current Y height)
        Vector3 targetPosition = new Vector3(currentTargetPellet.transform.position.x, transform.position.y, currentTargetPellet.transform.position.z);

        // Move towards current target
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Turn to face the target pellet
        transform.LookAt(targetPosition);
    }

    private IEnumerator FindNextPellet() 
    {
        isSearching = true;
        List<GameObject> pellets = currentSpawner.GetSpawnedPellets();

        // Removes all "destroyed" pellets from the list
        pellets.RemoveAll(pellet => pellet == null);

        if (pellets.Count > 0)
        {
            // Pick a random pellet from the remaining
            int randomIndex = Random.Range(0, pellets.Count);
            currentTargetPellet = pellets[randomIndex];
        } 
        else
        {
            // Stop moving if no pellets remain on the platform
            currentTargetPellet = null;
        }

        // Delay to prevent a crash from too many scans if no pellets exist
        yield return new WaitForSeconds(0.2f);

        // Reset to false after the timer finishes
        isSearching = false;
    }
}
