using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class Scr_HubLevelSelector : MonoBehaviour
{
    public float interactionRadius = 10f; // Adjustable range per object
    private SphereCollider interactionCollider;

    public Camera_Follow cameraScript;
    [SerializeField] public Transform Camera_Target01;
    [SerializeField] public Transform Camera_Target02;

    public InputSystem_Actions navigation;
    private InputAction Interacting;

    public GameObject interactionCanvas; // Assign the UI canvas per object


    private Transform player;
    public Player_HubMovement playerHubMove;

    private void OnEnable()
    {
        Interacting = navigation.UI.Interact;
        Interacting.Enable();
        Interacting.performed += OpenMenuLevelSelector;
    }

    private void OnDisable()
    {
        
    }

    private void Awake()
    {
        navigation = new InputSystem_Actions();
    }

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
            playerHubMove.LevelSelectorOn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player left interaction range!");
            playerHubMove.canInteractWith = false;
            playerHubMove.LevelSelectorOn = false;
        }
    }

    public void OpenMenuLevelSelector(InputAction.CallbackContext context)
    {
        if (playerHubMove.LevelSelectorOn && playerHubMove.canInteractWith)
        {
            Debug.Log("Nerd Shop Works!");
            playerHubMove.HubIsInteracting = true;
            interactionCanvas.SetActive(true);
            cameraScript.Camera_Target = Camera_Target02;
        }
    }


}






















/*
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scr_HubLevelSelector : MonoBehaviour
{
    public Camera mainCamera;
    public Camera secondaryCamera;
    public GameObject[] spheres;  // Array to hold the 4 spheres
    public GameObject TriggerZone;  // Array to hold the 4 spheres

    public Material selectedMaterial;  // Material to highlight selected button
    public Material defaultMaterial;   // Material for non-selected buttons

    private int selectedIndex = 0;     // Index of selected button
    private bool isInTriggerZone = false;  // Flag to check if the player is in trigger zone
    public bool LevelSelectorOn = false;

    void Start()
    {
        // Set the default material to all buttons
        foreach (var sphere in spheres)
        {
            sphere.GetComponent<Renderer>().material = defaultMaterial;
        }

        // Highlight the first button by default
        if (spheres.Length > 0)
            spheres[0].GetComponent<Renderer>().material = selectedMaterial;
    }

void Update()
    {
        // Input for navigating left or right between spheres
        if (isInTriggerZone)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))  // Navigate left
            {
                ChangeSelectedButton(-1);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))  // Navigate right
            {
                ChangeSelectedButton(1);
            }

            // Input for interacting with the selected button
            if (Input.GetKeyDown(KeyCode.E))  // Interact with the button
            {
                LoadLevel(selectedIndex);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Assuming the player is tagged as "Player"
        {
            // Switch to camera 2 on collision
            mainCamera.gameObject.SetActive(false);
            secondaryCamera.gameObject.SetActive(true);

            isInTriggerZone = true;  // Enable navigation when in the trigger zone
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Switch back to camera 1 when exiting the trigger
            mainCamera.gameObject.SetActive(true);
            secondaryCamera.gameObject.SetActive(false);

            isInTriggerZone = false;  // Disable navigation when out of the trigger zone
        }
    }

    private void ChangeSelectedButton(int direction)
    {
        // Reset the material of the currently selected sphere
        spheres[selectedIndex].GetComponent<Renderer>().material = defaultMaterial;

        // Update the index based on the direction (left or right)
        selectedIndex = (selectedIndex + direction + spheres.Length) % spheres.Length;

        // Highlight the newly selected button
        spheres[selectedIndex].GetComponent<Renderer>().material = selectedMaterial;
    }

    private void LoadLevel(int levelIndex)
    {
        // Map the index to specific levels (e.g., level 1-4)
        string levelName = "Level" + (levelIndex + 1);  // Level names should match the scene names
        SceneManager.LoadScene(levelName);  // Load the selected level
    }
}
*/