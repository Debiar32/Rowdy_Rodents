using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class OptionsMenu : MonoBehaviour
{
    [Header("Basic Option Elements")]
    public GameObject[] tabs; // Holds all tab panels
    public Button[] tabButtons; // Buttons representing each tab on the bar
    public Button[] defaultTabButtons;  // Default buttons for each tab


    public InputSystem_Actions UIControls;
    public GameObject mainMenuCanvas;
    public MenuNavigationHandler menuNav;

    private InputAction tabLeft;
    private InputAction tabRight;
    private InputAction back;

    private int currentTabIndex = 0;

    public void Start()
    {
        currentTabIndex = 0;
        UpdateTabs();
    }

    private void Awake()
    {
        UIControls = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        tabLeft = UIControls.UI.SwitchTabLeft;
        tabLeft.Enable();
        tabLeft.performed += SwitchTabLeft;

        tabRight = UIControls.UI.SwitchTabRight;
        tabRight.Enable();
        tabRight.performed += SwitchTabRight;

        back = UIControls.UI.GoBack;
        back.Enable();
        back.performed += GoingBack;
    }

    private void OnDisable()
    {
        tabLeft.Disable();
        tabRight.Disable();
        back.Disable();
    }

    private void SwitchTabLeft(InputAction.CallbackContext context)
    {
        // Move to the previous tab, wrapping around if necessary
        currentTabIndex--;
        if (currentTabIndex < 0)
        {
            currentTabIndex = tabs.Length - 1; // Wrap to the last tab
        }

        UpdateTabs();
    }

    private void SwitchTabRight(InputAction.CallbackContext context)
    {
        // Move to the next tab, wrapping around if necessary
        currentTabIndex++;
        if (currentTabIndex >= tabs.Length)
        {
            currentTabIndex = 0; // Wrap to the first tab
        }

        UpdateTabs();
    }

    private void UpdateTabs()
    {
        // Deactivate all tabs first
        for (int i = 0; i < tabs.Length; i++)
        {
            tabs[i].SetActive(false);
            SetTabButtonUnselected(i);  // Set the button to unselected state
        }

        // Activate the current tab
        tabs[currentTabIndex].SetActive(true);
        SetTabButtonSelected(currentTabIndex);  // Set the button to selected state

        // Set the selected tab's button if you have buttons to interact with
        EventSystem.current.SetSelectedGameObject(null); // Reset selected object

        // Set the default button of the current tab as selected
        if (currentTabIndex < defaultTabButtons.Length)
        {
            EventSystem.current.SetSelectedGameObject(defaultTabButtons[currentTabIndex].gameObject);
        }
    }


    private void SetTabButtonSelected(int index)
    {
        // Change the color of the button when selected
        tabButtons[index].image.color = Color.green; // Example: Green for selected (change as needed)
    }

    private void SetTabButtonUnselected(int index)
    {
        // Change the color of the button when unselected
        tabButtons[index].image.color = Color.white; // Default unselected color (change as needed)
    }

    // Add these to handle hover effect changes:
    public void OnTabButtonHoverEnter(int index)
    {
        // Change the color when hovering over the tab button
        if (currentTabIndex != index)  // Don't change color of the selected tab
        {
            tabButtons[index].image.color = Color.yellow; // Example: Yellow on hover
        }
    }

    public void OnTabButtonHoverExit(int index)
    {
        // Reset the hover color when exiting the button
        if (currentTabIndex != index)  // Don't reset color of the selected tab
        {
            tabButtons[index].image.color = Color.white; // Default color when not selected
        }
    }

    public void BackToMainMenu()
    {
        currentTabIndex = 0; // Reset to first tab when exiting
        UpdateTabs();
        gameObject.SetActive(false);
        mainMenuCanvas.SetActive(true);
        EventSystem.current.SetSelectedGameObject(menuNav.mainMenuFirstButton);
    }

    public void GoingBack(InputAction.CallbackContext context)
    {
        currentTabIndex = 0; // Reset to first tab when exiting
        UpdateTabs();
        gameObject.SetActive(false);
        mainMenuCanvas.SetActive(true);
        EventSystem.current.SetSelectedGameObject(menuNav.mainMenuFirstButton);
    }

}







    //public GameObject mainMenuCanvas;
    //public MenuNavigationHandler menuNav;

    //public Button[] firstTabButtons; // First selectable buttons in each tab

    //private int currentTabIndex = 0; // Tracks active tab
    //private PlayerInput playerInput;
    /*
        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();

            // Bind input actions
            switchTabLeft = playerInput.actions["SwitchTabLeft"];
            switchTabRight = playerInput.actions["SwitchTabRight"];
        }

        private void OnEnable()
        {
            // Enable the UI Action Map
            playerInput.SwitchCurrentActionMap("UI");

            // Subscribe to events
            switchTabLeft.performed += _ => SwitchTab(-1);
            switchTabRight.performed += _ => SwitchTab(1);

            UpdateTabs(); // Ensure correct tab is displayed
        }

        private void OnDisable()
        {
            // Unsubscribe when menu is disabled
            switchTabLeft.performed -= _ => SwitchTab(-1);
            switchTabRight.performed -= _ => SwitchTab(1);
        }

        private void SwitchTab(int direction)
        {
            currentTabIndex += direction;
            if (currentTabIndex < 0) currentTabIndex = tabs.Length - 1;
            if (currentTabIndex >= tabs.Length) currentTabIndex = 0;

            UpdateTabs();
        }

        private void UpdateTabs()
        {
            for (int i = 0; i < tabs.Length; i++)
            {
                tabs[i].SetActive(i == currentTabIndex);
            }

            Set the selected button in the new tab
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(firstTabButtons[currentTabIndex].gameObject);
        }

        public void BackToMainMenu()
        {
            currentTabIndex = 0; // Reset to first tab when exiting
            UpdateTabs();
            gameObject.SetActive(false);
            mainMenuCanvas.SetActive(true);
            EventSystem.current.SetSelectedGameObject(menuNav.mainMenuFirstButton);
        }
        */



//## NEWEST OLD##
/*
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [Header("Basic Option Elements")]
    public GameObject[] tabs;
    public Button[] tabButton;

    public GameObject controlsTab, graphicsTab, audioTab;
    public GameObject mainMenuCanvas;
    public MenuNavigationHandler menuNav;


    private void Start()
    {
        controlsTab.SetActive(true); // Default to showing Controls Tab
        graphicsTab.SetActive(false);
        audioTab.SetActive(false);
    }


    public void ActiveTabs(int tab)
    {

        for (int i = 0; i < tabs.Length; i++)
        {
            tabs[i].SetActive(false);
        }
        tabs[tab - 1].SetActive(true);
    }


    public void BackToMainMenu()
    {
        controlsTab.SetActive(true); // Default to showing Controls Tab
        graphicsTab.SetActive(false);
        audioTab.SetActive(false);
        gameObject.SetActive(false);
        mainMenuCanvas.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(menuNav.mainMenuFirstButton);
    }


}  
*/





/*private void //Start()
{
    controlsTab.SetActive(true); // Default to showing Controls Tab
    graphicsTab.SetActive(false);
    audioTab.SetActive(false);
}

public void OpenControls()
{
    controlsTab.SetActive(true);
    graphicsTab.SetActive(false);
    audioTab.SetActive(false);
    EventSystem.current.SetSelectedGameObject(null);
    EventSystem.current.SetSelectedGameObject(menuNav.optionsFirstButton);
}

public void OpenGraphics()
{
    controlsTab.SetActive(false);
    graphicsTab.SetActive(true);
    audioTab.SetActive(false);
    EventSystem.current.SetSelectedGameObject(null);
    EventSystem.current.SetSelectedGameObject(menuNav.optionsFirstButton);
}

public void OpenAudio()
{
    controlsTab.SetActive(false);
    graphicsTab.SetActive(false);
    audioTab.SetActive(true);
    EventSystem.current.SetSelectedGameObject(null);
    EventSystem.current.SetSelectedGameObject(menuNav.optionsFirstButton);
}

public void BackToMainMenu()
{
    controlsTab.SetActive(true); // Default to showing Controls Tab
    graphicsTab.SetActive(false);
    audioTab.SetActive(false);
    gameObject.SetActive(false);
    mainMenuCanvas.SetActive(true);
    EventSystem.current.SetSelectedGameObject(null);
    EventSystem.current.SetSelectedGameObject(menuNav.mainMenuFirstButton);
}
}
*/