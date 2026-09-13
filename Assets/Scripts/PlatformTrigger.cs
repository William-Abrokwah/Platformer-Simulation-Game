using UnityEngine;
using System.Collections.Generic;

public class PlatformTrigger : MonoBehaviour
{
    public bool isFirstPlatform = false; // Checked true only for Platform 1
    private bool hasActivated = false;

    void Start()
    {
        // If it's the starting platform, activate right away
        if (isFirstPlatform)
        {
            ActivatePlatform();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Activate platfrom if player enters
        if (!hasActivated && other.CompareTag("Player"))
        {
            ActivatePlatform();
            Debug.Log("Platform activated!");
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
                proj.DestroyProjectile();
                Debug.Log("Projectile destroyed!");
            }
        }
    }

    public void ActivatePlatform()
    {
        hasActivated = true;

        GetComponent<PelletSpawner>().SpawnPellets();
        GetComponent<BotSpawner>().SpawnBot();
    }
}