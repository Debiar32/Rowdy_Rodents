using UnityEngine;
using UnityEngine.Events;

public class scr_AllyWeaponUpgrades : MonoBehaviour
{
    public float interactionRadius = 10f; // Adjustable range per object
    private SphereCollider interactionCollider;

    public GameObject interactionCanvas; // Assign the UI canvas per object

    private Transform player;
    public Player_HubMovement playerHubMove;

    private void Start()
    {
        // Create and configure the Sphere Collider
        interactionCollider = gameObject.AddComponent<SphereCollider>();
        interactionCollider.isTrigger = true; // Set it as a trigger
        interactionCollider.radius = interactionRadius; // Adjust radius dynamically
        interactionCanvas.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered interaction range!");
            playerHubMove.canInteractWith = true;
            playerHubMove.allyWeaponUpgrades = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player left interaction range!");
            playerHubMove.canInteractWith = false;
            playerHubMove.allyWeaponUpgrades = false;
            interactionCanvas.SetActive(false);
        }
    }

    // Implemented OpenMenu method with GameObject parameter
    public void OpenMenuWeapons()
    {
        Debug.Log("Upgrade Shop Works for");
        playerHubMove.HubIsInteracting = true;
        interactionCanvas.SetActive(true);
    }
    // Implemented OnUse action (can be invoked when the upgrade is used)
}
