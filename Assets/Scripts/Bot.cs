using UnityEngine;
using System.Collections;

public class Bot : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    
    private GameObject currentTargetPellet;
    private bool isSearching = true;

    private void Start()
    {
        StartCoroutine(FindNextPellet());
    }

    private void Update()
    {
        // Look for target if we don't have target and aren't alreadly looking
        if (currentTargetPellet == null)
        {
            if (!isSearching) 
            {
                StartCoroutine(FindNextPellet());
            }
            return;
        }

        // Move toward current target pellet in a straight line
        Vector3 targetPosition = new Vector3(currentTargetPellet.transform.position.x, transform.position.y, currentTargetPellet.transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Turn to face the target pellet
        transform.LookAt(targetPosition);
    }

    private IEnumerator FindNextPellet() 
    {
        isSearching = true;
        FoodPellet[] pellets = FindObjectsByType<FoodPellet>(FindObjectsSortMode.None);

        if (pellets.Length > 0)
        {
            // Pick a random pellet from the remaining
            int randomIndex = Random.Range(0, pellets.Length);
            currentTargetPellet = pellets[randomIndex].gameObject;
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
