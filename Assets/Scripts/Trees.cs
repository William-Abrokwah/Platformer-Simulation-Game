using UnityEngine;
using System.Collections.Generic;

public class Trees : MonoBehaviour
{
    [SerializeField] private List<GameObject> trees = new List<GameObject>();
    [SerializeField] private GameObject treeBarrier;

    private bool gapDetected = false;
    
    private void Update()
    {
        // Only run the check until a gap is found
        if (!gapDetected && IsGapOpen())
        {
            gapDetected = true;
            Debug.Log("Two adjacent trees are destroyed! Gap opened on " + gameObject.name);
            DisableBarrier();
        }
    }

    // Returns true if any two side-by-side trees are destroyed
    public bool IsGapOpen() 
    {
        for (int i = 0; i < trees.Count - 1; i++)
        {
            bool currentDestroyed = (trees[i] == null);
            bool nextDestroyed = (trees[i + 1] == null);

            if (currentDestroyed && nextDestroyed)
            {
                return true;
            }
        }
        return false;
    }

    public int GetMinShotsToOpenGap()
    {
        int minShotsNeeded = 2; // Maximum needed for any pair is 2 shots

        for (int i = 0; i < trees.Count - 1; i++)
        {
            int shotsForThisPair = 0;

            // If tree isn't destroyed, it requires 1 shot
            if (trees[i] != null) shotsForThisPair++;
            if (trees[i + 1] != null) shotsForThisPair++;

            // Track the lowest requirement across all pairs
            if (shotsForThisPair < minShotsNeeded)
            {
                minShotsNeeded = shotsForThisPair;
            }
        }
        
        if (minShotsNeeded == 1) Debug.Log("1 shot needed");

        return minShotsNeeded;
    }

    public void DisableBarrier() {
        // Disable the barrier once a valid 2-tree gap is opened
        if (treeBarrier != null && IsGapOpen())
        {
            // Disables the collider so the player can freely walk through
            Collider barrierCollider = treeBarrier.GetComponent<Collider>();
            if (barrierCollider != null && barrierCollider.enabled)
            {
                barrierCollider.enabled = false;
                Debug.Log("Tree gap opened! Barrier deactivated.");
            }
        }
    }
}
