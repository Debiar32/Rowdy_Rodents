using UnityEngine;
using UnityEngine.InputSystem;

public class TitleScreen : MonoBehaviour
{
    public InputActionAsset actionAsset;  // Reference to the Input Action Asset
    private InputActionMap mainMenuActions;  // The action map we use for main menu actions
    private InputAction startGameAction;  // The action that listens for any key press

    public GameObject mainMenuCanvas;

    private void OnEnable()
    {
        // Get the action map and the specific action that listens for any key/button press
        mainMenuActions = actionAsset.FindActionMap("MainMenuActions");
        startGameAction = mainMenuActions.FindAction("startGameAction");  // You should define this action in the Input Action Asset

        // Subscribe to the "performed" event, which triggers when any key is pressed
        startGameAction.performed += OnStartGame;

        // Enable the action map
        mainMenuActions.Enable();
    }

    private void OnDisable()
    {
        // Unsubscribe from the event and disable the action map
        startGameAction.performed -= OnStartGame;
        mainMenuActions.Disable();
    }

    private void OnStartGame(InputAction.CallbackContext context)
    {
        // This will be triggered when any key is pressed (keyboard, mouse, or gamepad button)
        gameObject.SetActive(false);  // Hide the title screen
        mainMenuCanvas.SetActive(true);  // Show the main menu
    }
}
