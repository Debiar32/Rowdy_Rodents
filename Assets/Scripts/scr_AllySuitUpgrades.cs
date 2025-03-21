using UnityEngine;
using UnityEngine.Events;

public class scr_AllySuitUpgrades : MonoBehaviour
{
    [field: SerializeField]
    public UnityEvent OnUse { get; private set; }

    public void Use(GameObject actor)
    {
        OnUse?.Invoke();
    }

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
            playerHubMove.allySuitUpgrades = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player left interaction range!");
            playerHubMove.canInteractWith = false;
            playerHubMove.allySuitUpgrades = false;
            interactionCanvas.SetActive(false);
        }
    }

    // Updated method signature to match interface requirement
    public void OpenMenuSuit()
    {
        Debug.Log("Upgrade Shop Works for");
        playerHubMove.HubIsInteracting = true;
        interactionCanvas.SetActive(true);
    }
}
