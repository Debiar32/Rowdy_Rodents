using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class levelSelectionScript : MonoBehaviour
{
    public Scr_HubLevelSelector levelSelect;
    public InputSystem_Actions navigation;
    private InputAction exitButton;
    private InputAction navigateMap;
    private InputAction Selecting;

    public GameObject[] levelsMap; // Holds all tab panels (levels)
    private int currentLevelIndex = 0; // Track the currently selected level

    public GameObject outlinePrefab; // The prefab for the outline (optional)

    public Player_HubMovement playerHubMove;

    private void Awake()
    {
        navigation = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        currentLevelIndex = 0;
        UpdateLevelDisplay();
        exitButton = navigation.UI.GoBack;
        exitButton.Enable();
        exitButton.performed += CloseMenu;

        navigateMap = navigation.UI.Navigate;
        navigateMap.Enable();
        navigateMap.performed += MapNavigate;

        Selecting = navigation.UI.Selecting;
        Selecting.Enable();
        Selecting.performed += TriggerLevelOpen;

        levelsMap[currentLevelIndex].SetActive(true);
    }

    private void OnDisable()
    {
        exitButton.Disable();
        navigateMap.Disable();
        levelsMap[currentLevelIndex].SetActive(false);
    }

    void Start()
    {
        // Initialize the first level with an indicator
        UpdateLevelDisplay();
    }

    void Update() { }

    public void CloseMenu(InputAction.CallbackContext context)
    {
        gameObject.SetActive(false);
        levelSelect.cameraScript.Camera_Target = levelSelect.Camera_Target01;
        playerHubMove.HubIsInteracting = false;
    }

    // Open specific levels based on the index
    public void OpenLevel00() { OpenLevel("Lvl_00_TutorialNew"); }
    public void OpenLevel01() { OpenLevel("Lvl_01_Village"); }
    public void OpenLevel02() { OpenLevel("Lvl_02_Swamp"); }
    public void OpenLevel03() { OpenLevel("Lvl_03_Outpost"); }

    // General method to load a level by name
    private void OpenLevel(string sceneName)
    {
        gameObject.SetActive(false);
        levelSelect.cameraScript.Camera_Target = levelSelect.Camera_Target01;
        playerHubMove.HubIsInteracting = false;
        SceneManager.LoadScene(sceneName);
    }

    // Map navigation logic (left/right)
    public void MapNavigate(InputAction.CallbackContext context)
    {
        Vector2 navigationInput = context.ReadValue<Vector2>();

        if (navigationInput.x > 0) // Move right
        {
            NavigateRight();
        }
        else if (navigationInput.x < 0) // Move left
        {
            NavigateLeft();
        }

        // After navigating, trigger the OpenLevel function based on currentLevelIndex
    }

    // Navigate to the right (next level)
    private void NavigateRight()
    {
        currentLevelIndex++;

        // Loop around if we reach the end (level 3 -> level 0)
        if (currentLevelIndex >= levelsMap.Length)
        {
            currentLevelIndex = 0;
        }

        UpdateLevelDisplay();
    }

    // Navigate to the left (previous level)
    private void NavigateLeft()
    {
        currentLevelIndex--;

        // Loop around if we reach the beginning (level 0 -> level 3)
        if (currentLevelIndex < 0)
        {
            currentLevelIndex = levelsMap.Length - 1;
        }

        UpdateLevelDisplay();
    }

    // Update the UI to reflect the currently selected level and apply the outline
    private void UpdateLevelDisplay()
    {
        // Hide all level panels
        foreach (var level in levelsMap)
        {
            level.SetActive(false);
        }

        // Show the selected level
        levelsMap[currentLevelIndex].SetActive(true);
    }

    // Trigger the correct OpenLevel method based on the current level index
    public void TriggerLevelOpen(InputAction.CallbackContext context)
    {
        switch (currentLevelIndex)
        {
            case 0:
                OpenLevel00();
                break;
            case 1:
                OpenLevel01();
                break;
            case 2:
                OpenLevel02();
                break;
            case 3:
                OpenLevel03();
                break;
            default:
                Debug.LogWarning("Invalid level index!");
                break;
        }
    }
}
