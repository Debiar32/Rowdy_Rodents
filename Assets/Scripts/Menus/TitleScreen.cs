using System.Xml.Serialization;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class TitleScreen : MonoBehaviour
{
    [Header("Base Navigation Setup")]
    public MenuNavigationHandler menuNav;
    public GameObject mainMenuCanvas;
    public GameObject CameraTitleScreen;

    public InputSystem_Actions TSControl;
    private InputAction ts_start;

    public void Start()
    {
        if (DeathScreen.isReturningFromDeath)
        {
            DeathScreen.isReturningFromDeath = false;
            ShowMainMenu();
            CameraTitleScreen.GetComponent<Animator>().Play("TitleScreenCamera_DownStill");
        }
        else if (PauseMenu.pauseToMenu)
        {
            PauseMenu.pauseToMenu = false;
            ShowMainMenu();
            CameraTitleScreen.GetComponent<Animator>().Play("TitleScreenCamera_DownStill");

        }
    }

    private void Awake()
    {
        TSControl = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        ts_start = TSControl.UI.tsStart;
        ts_start.Enable();
        ts_start.performed += StartMainMenuScreen;
    }

    private void OnDisable()
    {
        ts_start.Disable();
    }

    public void StartMainMenuScreen(InputAction.CallbackContext context)
    {
        StartCoroutine(MainMenuAnimations());
    }

    private IEnumerator MainMenuAnimations()
    {
        // Trigger the first animation (e.g., fade in or logo animation)

        yield return new WaitForSeconds(0.25f);
        CameraTitleScreen.GetComponent<Animator>().Play("TitleScreenCamera_GoingDown");
        yield return new WaitForSeconds(4.65f);
        CameraTitleScreen.GetComponent<Animator>().Play("TitleScreenCamera_DownStill");
        ShowMainMenu();
    }

    private void ShowMainMenu()
    {
        gameObject.SetActive(false);  // Hide the title screen
        mainMenuCanvas.SetActive(true);  // Show the main menu
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(menuNav.mainMenuFirstButton);
    }

}



    /*
    //public InputActionAsset actionAsset;  // Reference to the Input Action Asset
    //private InputActionMap mainMenuActions;  // The action map we use for main menu actions
    //private InputAction startGameAction;  // The action that listens for any key press

    public MenuNavigationHandler menuNav;

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
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(menuNav.mainMenuFirstButton);
    }
}
    */