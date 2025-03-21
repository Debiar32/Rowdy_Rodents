
using UnityEngine;

public class scr_AllyNerd : MonoBehaviour
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
            playerHubMove.Nerd = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player left interaction range!");
            playerHubMove.canInteractWith = false;
            playerHubMove.Nerd = false;
            interactionCanvas.SetActive(false);
        }
    }

    public void OpenMenuNerd()
    {
        Debug.Log("Nerd Shop Works!");
        playerHubMove.HubIsInteracting = true;
        interactionCanvas.SetActive(true);
    }









}
