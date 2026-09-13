using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI ammoText;

    private void Awake()
    {
        // Ensure only one UIManager exists
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
        // Initialize the ammo to 0 when the game starts
        UpdateAmmoDisplay(0);
    }

    public void UpdateAmmoDisplay(int currentAmmo)
    {
        if (ammoText != null)
        {
            ammoText.text = "Ammo: " + currentAmmo;
        }
    }
}